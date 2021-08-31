using NHibernate.Mapping.ByCode;
using Utility.Demo.Domain.Entities;
using Utility.Nhibernate.EntityMappings;

namespace Utility.Demo.Nhibernate.EntityMappings
{
    public class TokenMap : TokenMap<TokenEntity, long>
    {
        public TokenMap() : base("t_token")
        {
            Id(x => x.Id, it => { it.Column("id"); it.Generator(Generators.Identity); });//Id
        }

    }
    /// <summary> TokenEntity nhibernate映射  </summary>
    public class TokenMap<T, Key> : BaseNhibernateMapp<T>
       where T : TokenEntity<Key>
    {
        public TokenMap(string tableName) : base(tableName)
        {

        }

        protected override void Set()
        {
           // Id(x => x.Id, it => { it.Column("id"); });//Id

            Property(x => x.Token, it => { it.Column("token"); it.Length(255); });//Token

            Property(x => x.TokenExpried, it => { it.Column("token_expried"); });//TokenExpried

            Property(x => x.RefreshToken, it => { it.Column("refresh_token"); it.Length(255); });//RefreshToken

            Property(x => x.RefreshTokenExpried, it => { it.Column("refresh_token_expried"); });//RefreshTokenExpried

            Property(x => x.CreateDate, it => { it.Column("create_date"); });//CreateDate

            Property(x => x.UserId, it => { it.Column("user_id"); });//UserId

            Property(x => x.Flag, it => { it.Column("flag"); });//Flag

        }
    }
}
