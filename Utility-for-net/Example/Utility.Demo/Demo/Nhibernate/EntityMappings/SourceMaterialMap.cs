using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Demo.Domain.Entities;
using Utility.Nhibernate.EntityMappings;

namespace Utility.Demo.Nhibernate.EntityMappings
{
    
    public class SourceMaterialMap : SourceMaterialMap<SourceMaterialEntity, long>
    {
        public SourceMaterialMap() : base("t_source_material")
        {
            Id(x => x.Id, it => { it.Column("id"); it.Generator(Generators.Identity); });//Id
        }

    }
    /// <summary> BankEntity nhibernate映射  </summary>
    public class SourceMaterialMap<T, Key> : BaseNhibernateMapp<T>
           where T : SourceMaterialEntity<Key>
    {
        public SourceMaterialMap(string tableName) : base(tableName)
        {

        }

        protected override void Set()
        {
          //  Id(x => x.Id, it => { it.Column("id"); });//Id

            Property(x => x.Src, it => { it.Column("src"); it.Length(255); });//Src

            Property(x => x.Key, it => { it.Column("key"); it.Length(255); });//Key

            //Property(x => x.Buffer, it => { it.Column("buffer"); it.Length(255); });//Buffer

            Property(x => x.Base64, it => { it.Column("base64"); it.Length(255); });//Base64

            Property(x => x.Description, it => { it.Column("description"); });//Description

            Property(x => x.Buket, it => { it.Column("buket"); });//Buket

            Property(x => x.ObjectName, it => { it.Column("object_name"); it.Length(255); });//ObjectName



        }
    }
}
