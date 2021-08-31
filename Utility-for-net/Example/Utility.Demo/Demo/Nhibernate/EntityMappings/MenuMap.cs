using NHibernate.Mapping.ByCode;
using Utility.Demo.Domain.Entities;
using Utility.Nhibernate.EntityMappings;

namespace Utility.Demo.Nhibernate.EntityMappings
{
    //NHibernate.MappingException:“Could not determine type for: Utility.Demo.Domain.Entities.MenuEntity, Utility.Demo, 
    //    for columns: NHibernate.Mapping.Column(id)”
    //还有个 版本 列 映射 有问题(查询时候 后来 不知道更新 解决了没有)
    //set list bag 必须写死？  没用 ISet ICollection IList
    //泛型报错 取消 泛型 还 尼玛报错  这个 版本 有问题 5.3.x 之前 版本 支持(难道我哪里写错了？) 搞不懂 到处挖坑? 更新后 坑 变 多了
    public class MenuMap : MenuMap<MenuEntity,long>
    {
        public MenuMap() : base("t_menu")
        {
            Id(x => x.Id, it => { it.Column("id"); it.Generator(Generators.Identity); });//Id
            //Id(x => x.Id, it => { it.Column("id"); });//Id
            //Property(x => x.ParentId, it => { it.Column("parent_id"); });//ParentId

            //Property(x => x.ParentId, map => { map.Column("parent_id");  });

            //ex Could not determine type for
            ManyToOne(x => x.Parent, map => { map.Column("parent_id"); map.ForeignKey("fk_parent_id");map.NotFound(NotFoundMode.Ignore);map.Cascade(Cascade.All); });

           // Set(x => x.Children, map => { map.Key(it => { it.Column("parent_id"); it.ForeignKey("fk_parent_id"); }); });

        }

    }
    /// <summary> MenuEntity nhibernate映射  </summary>
    public  class MenuMap<T, key> : BaseNhibernateMapp<T>
           where T : MenuEntity<T,key>
    {
        public MenuMap(string tableName) : base(tableName)
        {

        }

        protected override void Set()
        {
            Property(x => x.Soure, it => { it.Column("soure"); it.Length(255); });//Soure

            Property(x => x.Orders, it => { it.Column("orders"); });//Orders

            //ex Could not determine type for
            //ManyToOne(x => x.Parent, map => { map.Column("parent_id"); map.ForeignKey("fk_parent_id");map.NotFound(NotFoundMode.Ignore);map.Cascade(Cascade.All); });

            //Set(x => x.Children, map => { map.Key(it => { it.Column("parent_id"); it.ForeignKey("fk_parent_id"); }); });

            Property(x => x.CreationTime, it => { it.Column("creation_time"); });//CreationTime

            Property(x => x.LastModificationTime, it => { it.Column("last_modification_time"); });//LastModificationTime

            Property(x => x.IsDeleted, it => { it.Column("is_deleted"); });//IsDeleted

            Property(x => x.DeletionTime, it => { it.Column("deletion_time"); });//DeletionTime

         
            Property(x => x.Text, it => { it.Column("text"); it.Length(255); });//Text

            Property(x => x.State, it => { it.Column("state"); it.Length(255); });//State

            Property(x => x.Checked, it => { it.Column("checked"); });//Checked

            Property(x => x.AttributesJson, it => { it.Column("attributes_json"); it.Length(255); });//AttributesJson

            Property(x => x.IconCls, it => { it.Column("icon_cls"); it.Length(255); });//IconCls

            Property(x => x.Name, it => { it.Column("name"); it.Length(255); });//Name

            Property(x => x.Collpse, it => { it.Column("collpse"); });//Collpse

            Property(x => x.Groups, it => { it.Column("groups"); it.Length(255); });//Groups

            Property(x => x.Icon, it => { it.Column("icon"); it.Length(255); });//Icon

            Property(x => x.Href, it => { it.Column("href"); it.Length(255); });//Href

            Property(x => x.Description, it => { it.Column("description"); it.Length(255); });//Description

            Property(x => x.HuiIcon, it => { it.Column("hui_icon"); it.Length(255); });//HuiIcon

            Property(x => x.IdName, it => { it.Column("id_name"); it.Length(255); });//IdName

            Property(x => x.AceIcon, it => { it.Column("ace_icon"); it.Length(255); });//AceIcon

           // Id(x => x.Id, it => { it.Column("id"); });//Id

        }
    }
}
