#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utility.Demo.Domain.Entities;
using Utility.Ef.EntityMappings;
#elif NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
#endif

namespace Utility.Demo.Ef.EntityMappings
{
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
    /// <summary>UserFriendEntity  </summary>
    public  class UserFriendMap : BaseEfMapp<UserFriendEntity>
    {
        public UserFriendMap()
        {
            this.TableName = "t_user_friend";
         }

        protected override void Set(EntityTypeBuilder<UserFriendEntity> builder)
        {
            builder.HasKey(it => it.Id);//Id
            builder.Property(it => it.Id).HasColumnName("id");//Id

            builder.Property(it => it.UserId).HasColumnName("user_id");//UserId

            builder.Property(it => it.FriendId).HasColumnName("friend_id");//FriendId

            builder.Property(it => it.Agree).HasColumnName("agree");//Agree

            builder.Property(it => it.DeleteFlag).HasColumnName("delete_flag");//DeleteFlag

        }
    }

#elif NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462|| NET47 || NET471 || NET472|| NET48 
    /// <summary>UserFriendEntity  </summary>
    public  class UserFriendMap : BaseEfMapp<UserFriendEntity>
    {
        public UserFriendMap(): base("t_user_friend")
        {
         }

        protected override void Set()
        {
            Property(it => it.Id).HasColumnName("id");//Id

            Property(it => it.UserId).HasColumnName("user_id");//UserId

            Property(it => it.FriendId).HasColumnName("friend_id");//FriendId

            Property(it => it.Agree).HasColumnName("agree");//Agree

            Property(it => it.DeleteFlag).HasColumnName("delete_flag");//DeleteFlag

        }
    }

#endif
}
