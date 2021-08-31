using System;
using System.Collections.Generic;

namespace Utility.Demo.Application.Services.Dtos
{
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
    [Serializable]
#endif
    public class ElementMenuCategoryDto
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public string Style { get; set; }
        public ICollection<ElementMenuCategoryDto> Children { get; set; }
    }
}
