using System.Collections.Generic;
using Utility.IIS;

namespace Utility.XUnit
{
    public class IISTest
    {
        //    [Fact]
        public void Test()
        {
#if NET48
            string appPoolName = "test";
            string version=IISHelper.GetVersion();
            IISHelper.Remove(appPoolName, appPoolName);
            IISHelper.Create(new IISEntity() { Name = appPoolName, ApplicationName = appPoolName, Port = "8009", PhysicalPath = @"E:\work\tool" });
            List<IISEntity> iISEntities = IISHelper.GetList();

#endif
        }

        //   [Fact]
        public void TestAdministration()
        {
            
            string appPoolName = "test";
            IISAdministrationHelper.Create(new IISEntity() { Name=appPoolName,ApplicationName= appPoolName,Port="8009",PhysicalPath= @"E:\work\tool" });
            IISAdministrationHelper.Remove(appPoolName, appPoolName);
            List<IISEntity> iISEntities = IISAdministrationHelper.GetList();

        }
      
    }
   
}
