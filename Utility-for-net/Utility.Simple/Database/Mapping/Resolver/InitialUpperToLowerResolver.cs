#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System;
using System.IO;
using System.Reflection;
using Utility.Helpers;
using Utility.Xml;
using System.Text;
using System.Text.RegularExpressions;


namespace Utility.Database.Mapping.Resolver
{


    /// <summary> 名称 映射 </summary>
    public class InitialUpperToLowerResolver : EmptyResolver
    {
        public new static  readonly InitialUpperToLowerResolver Empty = new InitialUpperToLowerResolver();
        public override string ResolverTable(string className)
        {
            return $"t_{StringHelper.Parse(className, StringFormat.InitialLetterUpperCaseLower)}";
        }

        public override string ResolverColumn(string propertyName)
        {
            return StringHelper.Parse(propertyName, StringFormat.InitialLetterUpperCaseLower);
        }

        public override string ResolverFk(string propertyName)
        {
            return $"fk_{ ResolverColumn(propertyName)}";
        }
    }
}
#endif