using System;

namespace Utility.Database.Attributes
{
  
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public abstract class BaseAttribute : Attribute
    {

    }
   
}
