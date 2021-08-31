//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Tunynet.Common
{
    public class DateMoreToAttribute : ValidationAttribute//, IClientValidatable
    {
        public string OtherProperty { get; set; }

        public DateMoreToAttribute(string otherProperty)
        {
            OtherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //从验证上下文中可以获取我们想要的的属性
            var property = validationContext.ObjectType.GetProperty(OtherProperty);
            if (property == null)
            {
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "{0} 不存在", OtherProperty));
            }

            //获取属性的值
            var otherValue = property.GetValue(validationContext.ObjectInstance, null);
            if (Convert.ToDateTime(value) <= Convert.ToDateTime(otherValue))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }

        //public System.Collections.Generic.IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        //{
        //    var rule = new ModelClientValidationRule
        //    {
        //        ValidationType = "notequalto",
        //        ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
        //    };
        //    rule.ValidationParameters["other"] = OtherProperty;
        //    yield return rule;
        //}
    }
}