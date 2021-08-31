using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;

namespace {#programName}
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof({#programName}CoreModule))]
    public class {#programName}DataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
