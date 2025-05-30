namespace ZpdReader.DataStruct.ZpdDataComponent
{
    public record DataHead
    {
        public required string Version { get; set; }
        public required string CoorClass { get; set; }
        public required string RefFramework { get; set; }
        public required string HandleType { get; set; }
        public required string SiteName { get; set; }
    }
}
