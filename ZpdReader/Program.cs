using ZpdFile.DataStruct;

namespace ZpdFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"E:\ZPDcurl\2019\001\abmf0010.19zpd.txt";
            ZpdData data = ZpdReader.Read(filePath);
            Console.WriteLine(data.ToString());
            Console.ReadKey();
        }
    }
}
