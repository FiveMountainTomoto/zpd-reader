namespace ZpdFile.DataStruct.ZpdDataComponent.Trop
{
    public record TropStaCoordinates : IZpdDataSection
    {
        public string? Site { get; set; }
        public string? Pt { get; set; }
        public string? Soln { get; set; }
        public string? T { get; set; }
        public double StaX { get; set; }
        public double StaY { get; set; }
        public double StaZ { get; set; }
        public string? System { get; set; }
        public string? Remrk { get; set; }
    }
}
