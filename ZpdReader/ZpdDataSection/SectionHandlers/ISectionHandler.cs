using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZpdFile.DataStruct.ZpdDataComponent;

namespace ZpdFile.ZpdDataSection.SectionHandlers
{
    internal interface ISectionHandler
    {
        Type SectionType { get; }
        IZpdDataSection Handle(IEnumerable<string> lines);
    }
}
