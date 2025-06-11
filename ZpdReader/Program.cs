using ZpdFile.DataStruct;

namespace ZpdFile
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ZpdReader reader = ZpdReader.Instance;
            string dir = @"E:\ZPDcurl\2019";
            string[] allFiles = Directory.GetDirectories(dir)
                .SelectMany(Directory.GetFiles).Where(f => f.EndsWith(".txt")).ToArray();
            var testFiles = allFiles.Take(10);
            List<ZpdData> datas = testFiles.Select(reader.Read).ToList();
            foreach(var data in datas)
            {
                Console.WriteLine(data); Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
