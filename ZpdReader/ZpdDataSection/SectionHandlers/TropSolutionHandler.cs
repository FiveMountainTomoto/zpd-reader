using geodesy_data_struct.DataClass.Gnss;
using ZpdFile.DataStruct.ZpdDataComponent;
using ZpdFile.DataStruct.ZpdDataComponent.Trop;

namespace ZpdFile.ZpdDataSection.SectionHandlers
{
    internal class TropSolutionHandler : ISectionHandler
    {
        // Sample:
        // *SITE ____EPOCH___ TROTOT STDDEV  TGNTOT STDDEV  TGETOT STDDEV
        //  ABMF 19:001:00000 2486.1    5.3   0.501  0.776  -1.616  0.954
        //  ABMF 19:001:00300 2486.3    5.1   0.502  0.756  -1.617  0.935
        //  ABMF 19:001:00600 2486.7    4.9   0.502  0.750  -1.618  0.927
        public Type SectionType => typeof(TropSolution);

        public IZpdDataSection Handle(IEnumerable<string> lines)
        {
            string[] header = HeaderHandler.GetHeader(lines.First());
            if(!HeaderHandler.IsHeaderValid(header, SectionType))
                throw new FormatException("Invalid format for TROP/SOLUTION section:\n"+string.Join(Environment.NewLine, lines));
            IEnumerable<TropSolutionData> datas = lines.Skip(1).Select(line =>
            {
                string[] values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                return new TropSolutionData
                {
                    Site = values[0],
                    Epoch = new Epoch(values[1]),
                    TotalDelay = double.Parse(values[2]),
                    TotalDelayStandardDeviation = double.Parse(values[3]),
                    Tgntot = double.Parse(values[4]),
                    TgntotStandardDeviation = double.Parse(values[5]),
                    Tgetot = double.Parse(values[6]),
                    TgetotStandardDeviation = double.Parse(values[7])
                };
            });
            return new TropSolution { Datas = datas };
        }
    }
}
