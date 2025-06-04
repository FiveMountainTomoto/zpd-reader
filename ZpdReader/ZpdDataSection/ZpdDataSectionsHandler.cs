using ZpdFile.DataStruct.ZpdDataComponent;
using ZpdFile.ZpdDataSection.SectionHandlers;

namespace ZpdFile.ZpdDataSection
{
    public class ZpdDataSectionsHandler
    {
        public ZpdDataSectionsHandler()
        {
            IEnumerable<Type> handlerTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(ISectionHandler).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);
            foreach (Type? handlerType in handlerTypes)
            {
                var handler = (ISectionHandler)Activator.CreateInstance(handlerType)!;
                Type sectionType = handler.SectionType;
                _handlersDict.Add(sectionType, handler.Handle);
            }
        }
        private Dictionary<Type, Func<IEnumerable<string>, IZpdDataSection>> _handlersDict = [];
        public IZpdDataSection Handle(Type sectionType, IEnumerable<string> lines)
        {
            return _handlersDict.TryGetValue(sectionType, out Func<IEnumerable<string>, IZpdDataSection>? handler)
                ? handler(lines)
                : throw new ArgumentException($"No handler found for section type:{sectionType.FullName}");
        }
    }
}
