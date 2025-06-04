using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpdReader.DataStruct.ZpdDataComponent;

namespace ZpdReader.ZpdDataSectionHandler.SectionHandlers
{
    internal interface ISectionHandler
    {
        Type SectionType { get; }
        IZpdDataSection Handle(IEnumerable<string> lines);
    }
}
