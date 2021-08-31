#if  NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.AspNetCore
{
    /// <summary>
    /// https://www.it1352.com/1944925.html asp.net core >=3.1
    /// </summary>
    public class NullObjectModelValidator : IObjectModelValidator
    {
        /// <summary>
        /// 不验证
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="validationState"></param>
        /// <param name="prefix"></param>
        /// <param name="model"></param>
        public void Validate(ActionContext actionContext, ValidationStateDictionary validationState, string prefix, object model)
        {
            
        }
    }
}
#endif