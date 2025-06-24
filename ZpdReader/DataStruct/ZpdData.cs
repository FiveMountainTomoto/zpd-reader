using System.Reflection;
using System.Text.Json;
using ZpdFile.DataStruct.ZpdDataComponent;
using ZpdFile.DataStruct.ZpdDataComponent.Site;
using ZpdFile.DataStruct.ZpdDataComponent.Trop;

namespace ZpdFile.DataStruct
{
    public class ZpdData
    {
        public string? RawFileName { get; set; }
        public string? SiteName { get; set; }
        public DataHead? DataHead { get; set; }
        public SiteAntenna? SiteAntenna { get; set; }
        public SiteEccentricity? SiteEccentricity { get; set; }
        public SiteGpsPhaseCenter? SiteGpsPhaseCenter { get; set; }
        public SiteId? SiteId { get; set; }
        public SiteReceiver? SiteReceiver { get; set; }
        public TropDescription? TropDescription { get; set; }
        public TropStaCoordinates? TropStaCoordinates { get; set; }
        public TropSolution? TropSolution { get; set; }

        private PropertyInfo[] _properties = typeof(ZpdData).GetProperties();
        internal void FillProperty(IZpdDataSection section)
        {
            Type type = section.GetType();
            foreach (PropertyInfo property in _properties)
            {
                if (property.PropertyType == type)
                {
                    property.SetValue(this, section);
                    return;
                }
            }
            throw new ArgumentException($"Invalid section type: {type}");
        }
        public override string ToString()
        {
            var properties = _properties.Where(p => p.GetValue(this) != null)
                .Select(p => $"{p.GetValue(this)}");
            return string.Join("\n", properties);
        }
        public void CheckSiteIdConsistent()
        {
            string?[] names = [
                DataHead?.SiteName,
                SiteId?.Code,
                TropSolution?.Datas.First().Site,
                TropStaCoordinates?.Site
                ];
            List<string?> namesNotNull = names.Where(n => n is not null).ToList();
            if (namesNotNull.Count > 0 && namesNotNull.All(n => n == namesNotNull[0]))
            {
                SiteName = namesNotNull[0];
                return;
            }
            else
            {
                throw new ArgumentException($"\'{RawFileName}\': SiteId is not consistent.");
            }
        }
        public void SaveJsonToFile(string filePath)
        {
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText(filePath, json);
        }
    }
}
