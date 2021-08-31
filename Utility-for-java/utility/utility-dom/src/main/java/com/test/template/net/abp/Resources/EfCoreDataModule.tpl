using Abp.Dependency;
using Abp.EntityFrameworkCore;
using Abp.Modules;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace {#programName}
{
    [DependsOn(typeof({#programName}CoreModule), typeof(AbpEntityFrameworkCoreModule))]//netframework AbpEntityFrameworkModule
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