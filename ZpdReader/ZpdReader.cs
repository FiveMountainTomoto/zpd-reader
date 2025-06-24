using ZpdFile.DataStruct;
using ZpdFile.DataStruct.ZpdDataComponent;
using ZpdFile.DataStruct.ZpdDataComponent.Site;
using ZpdFile.DataStruct.ZpdDataComponent.Trop;
using ZpdFile.ZpdDataSection;

namespace ZpdFile
{
    public class ZpdReader
    {
        private static readonly Dictionary<string, Type> MarkerTypeDict = new()
        {
            {"SITE/ID" , typeof(SiteId) },
            {"SITE/RECEIVER", typeof(SiteReceiver) },
            {"SITE/ANTENNA", typeof(SiteAntenna) },
            {"SITE/GPS_PHASE_CENTER", typeof(SiteGpsPhaseCenter) },
            {"SITE/ECCENTRICITY", typeof(SiteEccentricity) },
            {"TROP/DESCRIPTION", typeof(TropDescription) },
            {"TROP/STA_COORDINATES", typeof(TropStaCoordinates) },
            {"TROP/SOLUTION", typeof(TropSolution) }
        };
        private ZpdDataSectionsHandler _secHandl;
        private ZpdReader()
        {
            _secHandl = new();
        }
        private static readonly ZpdReader _instance = new();
        public static ZpdReader Instance => _instance;
        public ZpdData Read(string filePath)
        {
            using StreamReader reader = new(filePath);
            ZpdData zpdData = new();
            // 读取数据头
            if (!TryReadDataHead(reader, out DataHead? dataHead))
                throw new FormatException("Invalid ZPD file format: Data head not found.");
            zpdData.DataHead = dataHead;
            // 读取数据段
            while (!reader.EndOfStream)
            {
                string? line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (IsEndLine(line)) break;
                // 检测数据段起始行
                if (line.StartsWith('+'))
                {
                    // 判断数据段类型
                    string marker = line[1..];
                    if (MarkerTypeDict.TryGetValue(marker, out Type? markerType))
                    {
                        // 创建数据段实例
                        try
                        {
                            IZpdDataSection prop = _secHandl.Handle(markerType, ReadDataSectionAllLines(reader));
                            zpdData.FillProperty(prop);
                        }
                        catch (ArgumentException)
                        {
                            continue;
                        }
                    }
                    else throw new ArgumentException($"Invalid marker type: {marker}");
                }
            }
            return zpdData;
        }
        public static IEnumerable<string> GetZpdFilesRecursive(string path)
        {
            var dirs = Directory.GetDirectories(path);
            var files = Directory.GetFiles(path).Where(f => f.EndsWith("zpd.txt"));
            return dirs.SelectMany(GetZpdFilesRecursive).Concat(files);
        }
        private static bool TryReadDataHead(StreamReader reader, out DataHead? dataHead)
        {
            // Sample:
            // %=TRO 0.01 XYZ 19:022:15608 IGS 18:365:75600 19:002:03600 P  ABMF
            while (!reader.EndOfStream)
            {
                // 跳过空行
                string? line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                // 判断是否为数据头
                if (line.StartsWith("%=TRO"))
                {
                    string[] lineSplit = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (lineSplit.Length != 9) break;
                    // 读取数据头信息
                    dataHead = new()
                    {
                        Version = lineSplit[1],
                        CoorClass = lineSplit[2],
                        // 3、5、6 不知道是什么时间
                        RefFramework = lineSplit[4],
                        HandleType = lineSplit[7],
                        SiteName = lineSplit[8]
                    };
                    return true;
                }
                else
                {
                    break;
                }
            }
            dataHead = null;
            return false;
        }
        private static IEnumerable<string> ReadDataSectionAllLines(StreamReader reader)
        {
            while (!reader.EndOfStream)
            {
                string? line = reader.ReadLine();
                if (string.IsNullOrEmpty(line)) continue;
                if (line.StartsWith('-')) break;
                yield return line;
            }
        }
        private static bool IsEndLine(string line)
        {
            return line.StartsWith("%=ENDTRO");
        }
    }
}
