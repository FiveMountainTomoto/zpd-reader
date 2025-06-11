using static ZpdFile.ZpdDataSection.HeaderHandler;
using ZpdFile.DataStruct.ZpdDataComponent;
using ZpdFile.DataStruct.ZpdDataComponent.Site;
using ZpdFile.DataStruct.ZpdDataComponent.Trop;

namespace ZpdFile.ZpdDataSection.SectionHandlers
{
    internal class TropStaCoordinatesHandler : ISectionHandler
    {
        // Sample:
        // *SITE PT SOLN T __STA_X_____ __STA_Y_____ __STA_Z_____ SYSTEM REMRK
        //  ABMF  A    1 P  2919785.800 -5383744.950  1774604.843 IGS14_ XYZ
        public Type SectionType => typeof(TropStaCoordinates);

        public IZpdDataSection Handle(IEnumerable<string> lines)
        {
            List<string> lineList = lines.ToList();
            // Check header
            string[] header = GetHeader(lineList[0]);
            if (lineList.Count != 2 || !IsHeaderValid(header, SectionType))
                throw new FormatException("Invalid format of TROP/STA_COORDINATES section:\n" + string.Join(Environment.NewLine, lineList));
            // Parse data
            string[] data = lineList[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            TropStaCoordinates staCor = new()
            {
                Site = data[0],
                Pt = data[1],
                Soln = data[2],
                T = data[3],
                StaX = double.Parse(data[4]),
                StaY = double.Parse(data[5]),
                StaZ = double.Parse(data[6]),
                System = data[7],
                Remrk = data[8]
            };
            return staCor;
        }
    }
}
