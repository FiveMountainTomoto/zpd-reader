using ZpdFile.DataStruct.ZpdDataComponent.Site;
using ZpdFile.DataStruct.ZpdDataComponent.Trop;

namespace ZpdFile.ZpdDataSection
{
    internal static class HeaderHandler
    {
        internal static string[] GetHeader(string line)
        {
            if (!line.StartsWith('*'))
                throw new ArgumentException($"Invalid header line: {line}");
            string[] parts = line[1..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim('_')).ToArray();
            return parts;
        }
        internal static bool IsHeaderValid(string[] header, Type dataType)
        {
            return IsHeaderEqual(header, _headersDict[dataType]);
        }
        private static bool IsHeaderEqual(string[] header1, string[] header2)
        {
            if (header1.Length != header2.Length) return false;
            for (int i = 0; i < header1.Length; i++)
                if (header1[i] != header2[i]) return false;
            return true;
        }
        private static readonly Dictionary<Type, string[]> _headersDict = new()
        {
            {typeof(SiteAntenna), ["SITE","PT","SOLN","T","DATA_START","DATA_END","DESCRIPTION","S/N"] },
            {typeof(SiteId), ["CODE","PT","DOMES","T","STATION","DESCRIPTION","APPROX_LON","APPROX_LAT","APP_H"] },
            {typeof(TropStaCoordinates),["SITE","PT","SOLN","T","STA_X","STA_Y","STA_Z","SYSTEM","REMRK"] },
            {typeof(TropSolution),["SITE","EPOCH","TROTOT","STDDEV","TGNTOT","STDDEV","TGETOT","STDDEV"] }
        };
    }
}
