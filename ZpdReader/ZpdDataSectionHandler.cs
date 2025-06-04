using ZpdReader.DataStruct.ZpdDataComponent;
using ZpdReader.DataStruct.ZpdDataComponent.Site;
using ZpdReader.DataStruct.ZpdDataComponent.Trop;

namespace ZpdReader
{
    internal class ZpdDataSectionHandler
    {
        public ZpdDataSectionHandler()
        {
        }
        private static string[] GetHeader(string line)
        {
            if (!line.StartsWith('*'))
                throw new ArgumentException($"Invalid header line: {line}");
            string[] parts = line[1..].Split(' ').Select(p => p.Trim('_')).ToArray();
            return parts;
        }
        private static bool IsHeaderEqual(string[] header1, string[] header2)
        {
            if (header1.Length != header2.Length) return false;
            for (int i = 0; i < header1.Length; i++)
            {
                if (header1[i] != header2[i]) return false;
            }
            return true;
        }
        private Dictionary<Type, string[]> HeadersDict = new()
        {
            {typeof(SiteAntenna), ["SITE","PT","SOLN","T","DATA_START","DATA_END","DESCRIPTION","S/N"] },
            {typeof(SiteId), ["CODE","PT","DOMES","T","STATION","DESCRIPTION","APPROX_LON","APPROX_LAT","APP_H"] }
        };
        private static readonly Func<IEnumerable<string>, IZpdDataSection> SiteAntennaHandler = (lines) =>
        {
            // Sample:
            //*SITE PT SOLN T DATA_START__ DATA_END____ DESCRIPTION_________ S/N__
            // ABMF  A    1 P 18:365:75600 19:001:86100 TRM57971.00     NONE -----
            throw new NotImplementedException();
        };
        private static readonly Func<IEnumerable<string>, IZpdDataSection> SiteEccentricityHandler = (lines) =>
        {
            // Sample:
            //
            //
            throw new NotImplementedException();
        };
        private static readonly Func<IEnumerable<string>, IZpdDataSection> SiteGpsPhaseCenterHandler = (lines) =>
        {
            throw new NotImplementedException();
        };
        private static readonly Func<IEnumerable<string>, IZpdDataSection> SiteIdHandler = (lines) =>
        {
            // Sample:
            //*CODE PT __DOMES__ T _STATION DESCRIPTION__ APPROX_LON_ APPROX_LAT_ _APP_H_
            // ABMF  A 97103M001 P Les Abymes, Guadeloupe 298 28 20.9  16 15 44.3   -25.6
            List<string> lineList = lines.ToList();
            string[] header = GetHeader(lineList[0]);
            if (lineList.Count != 2 ||!IsHeaderEqual(header, HeadersDict[typeof(SiteId)]))
                throw new ArgumentException("Invalid format of SITE/ID section");
            SiteId siteId = new()
            {

            };
            throw new NotImplementedException();
        };
        private static readonly Func<IEnumerable<string>, IZpdDataSection> SiteReceiverHandler = (lines) =>
        {
            throw new NotImplementedException();
        };
        private static readonly Func<IEnumerable<string>, IZpdDataSection> TropDescriptionHandler = (lines) =>
        {
            throw new NotImplementedException();
        };
        private static readonly Func<IEnumerable<string>, IZpdDataSection> TropSolutionHandler = (lines) =>
        {
            throw new NotImplementedException();
        };
        private static readonly Func<IEnumerable<string>, IZpdDataSection> TropStaCoordinatesHandler = (lines) =>
        {
            // Sample:
            // *SITE PT SOLN T __STA_X_____ __STA_Y_____ __STA_Z_____ SYSTEM REMRK
            //  ABMF  A    1 P  2919785.800 -5383744.950  1774604.843 IGS14_ XYZ
            throw new NotImplementedException();
        };
        private Dictionary<Type, Func<IEnumerable<string>, IZpdDataSection>> HandlersDict = new()
        {
            {typeof(SiteAntenna), SiteAntennaHandler },
            {typeof(SiteEccentricity), SiteEccentricityHandler },
            {typeof(SiteGpsPhaseCenter), SiteGpsPhaseCenterHandler },
            {typeof(SiteId), SiteIdHandler },
            {typeof(SiteReceiver), SiteReceiverHandler },
            {typeof(TropDescription), TropDescriptionHandler },
            {typeof(TropSolution), TropSolutionHandler },
            {typeof(TropStaCoordinates), TropStaCoordinatesHandler }
        };
        public IZpdDataSection Handle(Type sectionType, IEnumerable<string> lines)
        {
            if (HandlersDict.TryGetValue(sectionType, out Func<IEnumerable<string>, IZpdDataSection>? handler))
            {
                return handler(lines);
            }
            else throw new ArgumentException($"Invalid section type:{sectionType.FullName}");
        }
    }
}
