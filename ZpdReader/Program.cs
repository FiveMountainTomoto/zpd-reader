using System.Text.Json;
using ZpdFile.DataStruct;

namespace ZpdFile
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ZpdReader reader = ZpdReader.Instance;
            string dir = @"E:\ZPDcurl\2019";
            var allFiles = ZpdReader.GetZpdFilesRecursive(dir);
            List<ZpdData> datas = [];
            foreach (var file in allFiles)
            {
                try
                {
                    var data = reader.Read(file);
                    datas.Add(data); 
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            var goodDatas = datas.Where(d => d.SiteId is not null).GroupBy(d => d.SiteId!.Code).Where(g => g.Count() > 100000).Select(g => g.ElementAt(0)).ToList();
            string outputDir = @"C:\Users\86159\Desktop\毕业论文\good_data";
            foreach (var data in goodDatas)
            {
                using StreamWriter sw = new(Path.Combine(outputDir, data.SiteId!.Code + ".json"), false);
                string json = JsonSerializer.Serialize(data);
                sw.WriteLine(json);
            }
        }
    }
}
