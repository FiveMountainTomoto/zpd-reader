using ZpdFile.DataStruct.ZpdDataComponent;
using ZpdFile.DataStruct.ZpdDataComponent.Site;
using static ZpdFile.ZpdDataSection.HeaderHandler;

namespace ZpdFile.ZpdDataSection.SectionHandlers
{
    internal class SiteIdHandler : ISectionHandler
    {
        // Sample:
        //*CODE PT __DOMES__ T _STATION DESCRIPTION__ APPROX_LON_ APPROX_LAT_ _APP_H_
        // ABMF  A 97103M001 P Les Abymes, Guadeloupe 298 28 20.9  16 15 44.3   -25.6
        public Type SectionType => typeof(SiteId);
        public IZpdDataSection Handle(IEnumerable<string> lines)
        {
            List<string> lineList = lines.ToList();
            // Check header
            string[] header = GetHeader(lineList[0]);
            if (lineList.Count != 2 || !IsHeaderValid(header, SectionType))
                throw new ArgumentException("Invalid format of SITE/ID section:\n" + string.Join(Environment.NewLine, lineList));
            string[] data = lineList[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            // get station description
            IEnumerable<string> staDesArr = data.Skip(4).Take(data.Length - 11);// Skip the first 4 elements and take the rest witout the last 7 elements.
            string staDes = string.Join(" ", staDesArr);
            // get approx coor
            double[] coorArr = data[7..].Select(double.Parse).ToArray();
            SiteId siteId = new()
            {
                Code = data[0],
                PT = data[1],
                Domes = data[2],
                Type = data[3],
                StationDescription = staDes,
                ApproxLon = new(coorArr[0], coorArr[1], coorArr[2]),
                ApproxLat = new(coorArr[3], coorArr[4], coorArr[5]),
                ApproxH = coorArr[6]
            };
            return siteId;
        }
    }
}
