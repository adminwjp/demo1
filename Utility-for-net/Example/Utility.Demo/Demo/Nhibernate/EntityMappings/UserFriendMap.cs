using NHibernate.Mapping.ByCode;
using Utility.Demo.Domain.Entities;
using Utility.Nhibernate.EntityMappings;

namespace Utility.Demo.Nhibernate.EntityMappings
{
    public class UserFriendMap : UserFriendMap<UserFriendEntity, long>
    {
        public UserFriendMap() : base("t_user_friend")
        {
            Id(x => x.Id, it => { it.Column("id"); it.Generator(Generators.Identity); });//Id
        }

    }
    /// <summary> UserFriendEntity nhibernate映射  </summary>
    public class UserFriendMap<T, Key> : BaseNhibernateMapp<T>
           where T : UserFriendEntity<Key>
    {
        public UserFriendMap(string tableName) : base(tableName)
        {

        }

        protected override void Set()
        {
           // Id(x => x.Id, it => { it.Column("id"); });//Id

            Property(x => x.UserId, it => { it.Column("user_id"); });//UserId
            
            Property(x => x.FriendId, it => { it.Column("friend_id"); });//FriendId

            Property(x => x.Agree, it => { it.Column("agree"); });//Agree

            Property(x => x.DeleteFlag, it => { it.Column("delete_flag"); });//DeleteFlag

        }
    }
}
