using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tunynet;

namespace SocialContact.Infrastructure.Helpers
{
    public class ControlPanelHelper
    {

        /// <summary>
        /// 根据实体名称生成ClassType
        /// </summary>
        /// <param name="tenantTypeId"></param>
        /// <returns></returns>
        public static string GetClassType(string className)
        {
            var classType = string.Empty;
            var classNames = new Dictionary<string, string>();
            var interfaseClassType = typeof(IEntity);
            //Assembly a = Assembly.GetAssembly(superClassType);
            Assembly spb = Assembly.Load("Tunynet.Spacebuilder");
            Assembly core = Assembly.Load("Tunynet.Core");
            Assembly modules = Assembly.Load("Tunynet.Modules");
            Assembly presentation = Assembly.Load("Tunynet.Presentation");
            Assembly ask = Assembly.Load("Spacebuilder.Ask");
            Assembly doc = Assembly.Load("Spacebuilder.Doc");
            Assembly events = Assembly.Load("Spacebuilder.Event");
            Assembly pointMall = Assembly.Load("Spacebuilder.PointMall");
            Assembly vote = Assembly.Load("Spacebuilder.Vote");

            var assemblys = new List<Assembly>() { spb, core, modules, presentation, ask, doc, events, pointMall, vote };

            foreach (var a in assemblys)
            {
                foreach (Type t in a.GetTypes())
                {
                    if (t.IsClass)
                    {
                        //判断类是否实现接口
                        if (interfaseClassType.IsAssignableFrom(t))
                        {
                            classNames.Add(t.Name, t.AssemblyQualifiedName);
                        }
                    }
                }
            }
            if (classNames != null && classNames.Any())
            {
                classType = classNames.Where(n => n.Key == className).FirstOrDefault().Value;
            }
            return classType;
        }
    }
}
