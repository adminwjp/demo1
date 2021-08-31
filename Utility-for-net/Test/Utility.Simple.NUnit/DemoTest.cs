
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Demo.Domain.Entities;
using Utility.Template;
using Utility.IO;
using NUnit.Framework;
using Tool;

namespace Utility.Test
{
    public class DemoTest
    {
        public static string path
             //= "E:/work/social_contact/SocialContact-for-netcore";
             = "E:/work/utility/Utility-for-net";
        List<Type> types = new List<Type>();
        public void UpdateVersion()
        {
            StringHelper.UpdateTargetFramework(path, "<Version>1.0.0.0</Version>", "<Version>1.0.0.0</Version>");
        }
        void Init()
        {
            types.Clear();
            Type type = typeof(UserEntity);
            foreach (var item in type.Assembly.Modules)
            {
                foreach (var t in item.GetTypes())
                {
                    if (t.Namespace == type.Namespace
                        && !t.IsAbstract && !t.IsEnum && !t.IsValueType
                        && !t.IsInterface
                        && !t.IsGenericType
                        )
                    {
                        types.Add(t);
                    }
                }
            }
            CsharpTemplateHelper.AssemblyName = "Utility.Simple";
            CsharpTemplateHelper.EntityNamespace = types[0].Namespace;
            CsharpTemplateHelper.EfMappNamespace = "Utility.Demo.Ef.EntityMappings";
            CsharpTemplateHelper.ProgramName = "Demo";
            CsharpTemplateHelper.NhMappNamespace = "Utility.Demo.Nhibernate.EntityMappings";
        }
       // [Test]
        public void GeneratoEfMap()
        {
            Init();
            List<ClassModel> classModels = new List<ClassModel>();
            foreach (var item in types)
            {
                ClassModel classModel = new ClassModel() { ClassType = item };
                classModels.Add(classModel);
                classModel.GeneraotrByType();
               var str = EfFactory.Default.GetMappString(classModel);
              
                FileHelper.WriteFile(path + "/Example/Utility.Ef.Demo/Demo/Demo/Ef/EntityMappings/" + classModel.Prefix + "Map.cs", str);
            }
        }

        //[Test]
        public void GeneratorNhMapAndXml()
        {
            Init();
            var str = "";
          List <ClassModel> classModels = new List<ClassModel>();
            Dictionary<string, string> maps = new Dictionary<string, string>();
            foreach (var item in types)
            {
                ClassModel classModel = new ClassModel() { ClassType=item};
                classModels.Add(classModel);
                classModel.GeneraotrByType();
                str+=NhibernateFactory.Default.GeneratorHbm(classModel);
               var strNh = NhibernateFactory.Default.GeneratorMap(classModel);
                maps[classModel.Prefix] = strNh;
                FileHelper.WriteFile(path + "/Utility.Nhibernate.Simple/Demo/Nhibernate/EntityMappings/"+classModel.Prefix+"Map.cs", strNh);
            }
            //FileHelper.WriteFile(path + "/Utility.Nhibernate.Simple/Config/hbm/demo.hbm.xml", str);
          
            //CsharepGeneratorHelper.NameSpace = "Utility.Template.Infrastructure.EntityMappings";
            //CsharepGeneratorHelper.ParseEntity(types);
            //CsharepGeneratorHelper.EntityFrameworkCoreMap.EntityToMap();
            //CsharepGeneratorHelper.EntityFrameworkCoreMap.EntityToMapStringCode("" + "/Utility.Ef.Simple/Company/Ef/EntityMappings");
        }
    }
}
