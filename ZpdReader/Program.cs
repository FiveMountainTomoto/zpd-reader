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
            var allFiles = Directory.GetDirectories(dir)
                .SelectMany(Directory.GetFiles).Where(f => f.EndsWith(".txt"));
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
            var goodDatas = datas.GroupBy(d => d.SiteId!.Code).Where(g => g.Count() > 100000).Select(g => g.ElementAt(0)).ToList();
            using StreamWriter sw = new StreamWriter("goodDatas.json");
            foreach (var data in goodDatas)
            {
                string json = JsonSerializer.Serialize(data);
                sw.WriteLine(json);
            }
            Console.ReadKey();
        }
    }
}
