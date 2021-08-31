using NHibernate.Mapping.Attributes;
using System.Collections.Generic;

namespace OA.Domain.Entities
{
    [Class(Table = "module_info", Name = "ModuleInfo", NameType = typeof(ModuleEntity), Lazy = false)]
    public class ModuleEntity:BaseEntity
    {
        private string _name;
        private string _href;
        private long _parentId;
        private ModuleEntity _parent;
        private IList<ModuleEntity> _modules;
        private long _userId;
        [Property(Name = "Name", Column  = "name", NotNull = true, TypeType = typeof(string),Length =50)]
        public string Name
        {
            get { return this._name; }
            set { Set(ref _name, value, "Name"); }
        }
        [Property(Column = "href", TypeType = typeof(string),Length =100)]
        public string Href
        {
            get { return this._href; }
            set { Set(ref _href, value, "Href"); }
        }
        public long ParentId
        {
            get { return this._parentId; }
            set { Set(ref _parentId, value, "ParentId"); }
        }
        [ManyToOne(Column = "parent_id", ClassType = typeof(ModuleEntity),Lazy = Laziness.Unspecified)]
		public ModuleEntity Parent
        {
            get { return this._parent; }
            set { Set(ref _parent, value, "Parent"); }
        }
        [Bag(0, Table = "module_info",Lazy = CollectionLazy.False)]
        [Key(1, Column = "parent_id")]
        [OneToManyAttribute(2, ClassType = typeof(ModuleEntity))]
        public IList<ModuleEntity> Modules
        {
            get { return this._modules; }
            set { Set(ref _modules, value, "Modules"); }
        }
        public long UserId 
        {
            get { return this._userId; }
            set { Set(ref _userId, value, "UserId"); }
        }
     
    }
}
