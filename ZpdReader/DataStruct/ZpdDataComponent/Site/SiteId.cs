using geodesy_data_struct.DataClass;

namespace ZpdReader.DataStruct.ZpdDataComponent.Site
{
    public record SiteId
    {
        public string? CodePointType { get; set; }
        public string? Domes { get; set; }
        public string? Type { get; set; }
        public string? StationDescription { get; set; }
        public Angle? ApproxLon { get; set; }
        public Angle? ApproxLat { get; set; }
        public double ApproxH { get; set; }
    }
}
