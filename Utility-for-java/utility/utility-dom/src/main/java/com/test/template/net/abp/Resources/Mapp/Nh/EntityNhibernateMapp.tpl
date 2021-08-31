using Abp.NHibernate.EntityMappings;
using System;

namespace {#namespace}
{
    public class {#classMap} : BaseEntityMapp<{#CalssEntityName}>
    {
        public {#classMap}()
               : base("{#tableName}")
        {
          
        }
        protected override void Set()
        {
{#SetStringCode}
		}
    }
}
