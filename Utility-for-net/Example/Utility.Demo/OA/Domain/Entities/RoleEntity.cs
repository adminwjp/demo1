using NHibernate.Mapping.Attributes;
using System.Collections.Generic;

namespace OA.Domain.Entities
{
    [Class(Table = "role_info", NameType = typeof(RoleEntity), Lazy = false)]
    public class RoleEntity:BaseEntity
    {
        private string _name;
        private long _parentId;
        private RoleEntity _parent;
        private ICollection<RoleEntity> _roles;
        private long _userId;
        [Property(Column = "name", NotNull = true, TypeType = typeof(string), Length = 50)]
        public string Name
        {
            get { return this._name; }
            set { Set(ref _name, value, "Name"); }
        }
        public long ParentId
        {
            get { return this._parentId; }
            set { Set(ref _parentId, value, "ParentId"); }
        }
        [ManyToOne(2, ClassType = typeof(RoleEntity),Column = "parent_id")]
        public RoleEntity Parent
        {
            get { return this._parent; }
            set { Set(ref _parent, value, "Parent"); }
        }
        [Bag( Table = "role_info",Lazy =CollectionLazy.False)]
        [Key(Column = "parent_id")]
        [OneToManyAttribute(2, ClassType = typeof(RoleEntity),NotFound = NotFoundMode.Ignore)]
        public ICollection<RoleEntity> Roles
        {
            get { return this._roles; }
            set { Set(ref _roles, value, "Roles"); }
        }
        public long UserId
        {
            get { return this._userId; }
            set { Set(ref _userId, value, "UserId"); }
        }
     
    }
}
