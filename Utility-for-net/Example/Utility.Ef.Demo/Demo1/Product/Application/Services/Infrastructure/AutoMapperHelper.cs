#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
//using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Mappers;
using Utility.Helpers;
using AutoMapper;

namespace Product.Application.Services.Infrastructure
{
    public  class AutoMapperHelper
    {
        public static void Load(IMapperConfigurationExpression it)//(IServiceCollection services)
        {
           // AutoMapperMapper autoMapperObjectMapper = AutoMapperMapper.Empty;
            foreach (var module in typeof(AutoMapperHelper).Assembly.Modules)
            {
                foreach (var type in module.GetTypes())
                {
                    AutoMapper.AutoMapAttribute autofacMap = AttributeHelper.Get<AutoMapper.AutoMapAttribute>(type.GetCustomAttributes(false));
                    if (autofacMap != null)
                    {
                        it.CreateMap(autofacMap.SourceType, type);
                        it.CreateMap(type, autofacMap.SourceType);
                    }
                }
            }
            //  services.AddSingleton<IObjectMapper>(autoMapperObjectMapper);
        }
    }
}
#endif