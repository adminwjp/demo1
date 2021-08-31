using AutoMapper;
using SocialContact.Application.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Helpers;

namespace SocialContact.Application.Services
{
    public class MapperHelper
    {
        //前提 数据 类型相同 不然 需要手动转换
        public static void Mapper(IMapperConfigurationExpression configure)
        {
            var type = typeof(WorkDto);
            foreach (var module in type.Assembly.Modules)
            {
                foreach (var t in module.GetTypes())
                {
                    var attr = AttributeHelper.Get<AutoMapAttribute>(t.GetCustomAttributes(false));
                    if (attr!=null)
                    {
                        //IMappingExpression map = null;
                        if (t.Name.EndsWith("Dto")|| t.Name.EndsWith("Output"))
                        {
                           // map =
                                configure.CreateMap(attr.SourceType,t);
                        }
                        else
                        {
                            // map=
                            configure.CreateMap(t,attr.SourceType);
                            //只能手动转换
                            //if (t.Namespace == type.Namespace)
                            {
                              //  map.ForMember("CreateDate", it => it.MapFrom(it => 
                               // CommonHelper.TotalMilliseconds(((CreateWorkInput)it).CreateDate),true )
                               // );
                            }
                        }
                        
                    }
                }
            }

        }
        public static void MapperWork()
        {

        }
    }
}
