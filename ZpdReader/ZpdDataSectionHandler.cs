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
        private static readonly Func<IEnumerable<string>, IZpdDataSection> SiteAntennaHandler = (lines) =>
        {
            throw new NotImplementedException();
        };
        private static readonly Func<IEnumerable<string>, IZpdDataSection> SiteEccentricityHandler = (lines) =>
        {
            throw new NotImplementedException();
        };
        private static readonly Func<IEnumerable<string>, IZpdDataSection> SiteGpsPhaseCenterHandler = (lines) =>
        {
            throw new NotImplementedException();
        };
        private static readonly Func<IEnumerable<string>, IZpdDataSection> SiteIdHandler = (lines) =>
        {
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
