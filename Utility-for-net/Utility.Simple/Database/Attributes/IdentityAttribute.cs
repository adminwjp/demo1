using System;

namespace Utility.Database.Attributes
{

    /// <summary>主键自增 </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class IdentityAttribute : Attribute
    {
    }

}
