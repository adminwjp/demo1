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
    /// <summary>TokenEntity  </summary>
    public  class TokenMap : BaseEfMapp<TokenEntity>
    {
        public TokenMap()
        {
            this.TableName = "t_token";
         }

        protected override void Set(EntityTypeBuilder<TokenEntity> builder)
        {
            builder.HasKey(it => it.Id);//Id
            builder.Property(it => it.Id).HasColumnName("id");//Id

            builder.Property(it => it.Token).HasColumnName("token").HasMaxLength(255);//Token

            builder.Property(it => it.TokenExpried).HasColumnName("token_expried");//TokenExpried

            builder.Property(it => it.RefreshToken).HasColumnName("refresh_token").HasMaxLength(255);//RefreshToken

            builder.Property(it => it.RefreshTokenExpried).HasColumnName("refresh_token_expried");//RefreshTokenExpried

            builder.Property(it => it.CreateDate).HasColumnName("create_date");//CreateDate

            builder.Property(it => it.UserId).HasColumnName("user_id");//UserId

            builder.Property(it => it.Flag).HasColumnName("flag");//Flag

        }
    }

#elif NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462|| NET47 || NET471 || NET472|| NET48 
    /// <summary>TokenEntity  </summary>
    public  class TokenMap : BaseEfMapp<TokenEntity>
    {
        public TokenMap(): base("t_token")
        {
         }

        protected override void Set()
        {
            Property(it => it.Id).HasColumnName("id");//Id

            Property(it => it.Token).HasColumnName("token").HasMaxLength(255);//Token

            Property(it => it.TokenExpried).HasColumnName("token_expried");//TokenExpried

            Property(it => it.RefreshToken).HasColumnName("refresh_token").HasMaxLength(255);//RefreshToken

            Property(it => it.RefreshTokenExpried).HasColumnName("refresh_token_expried");//RefreshTokenExpried

            Property(it => it.CreateDate).HasColumnName("create_date");//CreateDate

            Property(it => it.UserId).HasColumnName("user_id");//UserId

            Property(it => it.Flag).HasColumnName("flag");//Flag

        }
    }

#endif
}
