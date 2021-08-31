#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using FluentValidation;
using Product.Application.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Application.Services.Validations
{
    public class NumberChanngeCommandValidator : AbstractValidator<NumberChanngeCommand>
    {
        public NumberChanngeCommandValidator()
        {
            RuleFor(item => item.ProductId).Empty().WithMessage("No productId found");
        }
    }
}
#endif