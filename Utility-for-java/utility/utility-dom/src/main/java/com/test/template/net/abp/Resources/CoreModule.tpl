using System.Reflection;
using Abp.Modules;

namespace {#programName}
{
    public class {#programName}CoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}