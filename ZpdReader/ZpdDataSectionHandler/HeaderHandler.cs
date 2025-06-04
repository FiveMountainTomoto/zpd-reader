using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpdReader.DataStruct.ZpdDataComponent.Site;

namespace ZpdReader.ZpdDataSectionHandler
{
    internal static class HeaderHandler
    {
        internal static string[] GetHeader(string line)
        {
            if (!line.StartsWith('*'))
                throw new ArgumentException($"Invalid header line: {line}");
            string[] parts = line[1..].Split(' ').Select(p => p.Trim('_')).ToArray();
            return parts;
        }
        internal static bool IsHeaderEqual(string[] header1, string[] header2)
        {
            if (header1.Length != header2.Length) return false;
            for (int i = 0; i < header1.Length; i++)
                if (header1[i] != header2[i]) return false;
            return true;
        }
        internal static readonly Dictionary<Type, string[]> HeadersDict = new()
        {
            {typeof(SiteAntenna), ["SITE","PT","SOLN","T","DATA_START","DATA_END","DESCRIPTION","S/N"] },
            {typeof(SiteId), ["CODE","PT","DOMES","T","STATION","DESCRIPTION","APPROX_LON","APPROX_LAT","APP_H"] }
        };
    }
}
