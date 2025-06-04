using System.Reflection;
using ZpdFile.DataStruct.ZpdDataComponent;
using ZpdFile.DataStruct.ZpdDataComponent.Site;
using ZpdFile.DataStruct.ZpdDataComponent.Trop;

namespace ZpdFile.DataStruct
{
    public class ZpdData
    {
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
    }
}
