using geodesy_data_struct.DataClass.Gnss;

namespace ZpdFile.DataStruct.ZpdDataComponent.Trop
{
    public record TropSolution : IZpdDataSection
    {
        public required IEnumerable<TropSolutionData> Datas { get; set; }
    }
    public record TropSolutionData
    {
        public required string Site { get; set; }
        public required Epoch Epoch { get; set; }
        public double TotalDelay { get; set; }
        public double TotalDelayStandardDeviation { get; set; }
        public double Tgntot { get; set; }
        public double TgntotStandardDeviation { get; set; }
        public double Tgetot { get; set; }
        public double TgetotStandardDeviation { get; set; }
    }
}
