using System.Collections.Generic;
using System.Data;
using Utility.Database.Entities;

namespace Utility.Database.Factory
{

    public class IdentityFactory
    {
        public static readonly IdentityFactory Empty = new IdentityFactory();
        internal readonly List<IdentityEntity> identityEntities = new List<IdentityEntity>();
        internal DbFlag Dialect = DbFlag.MySql;
        private IDbConnection _connection;
        public IDbConnection Connection
        {
            get { return _connection; }
            set
            {
                _connection = value;
            }
        }
        /// <summary>自增id </summary>
        public virtual long GetId(string table)
        {
            IdentityEntity identity = this.identityEntities.Find(it => it.Table.ToLower() == table.Replace("`", "").ToLower());
            return identity == null ? -1 : identity.Id;
        }
        public virtual void SetId(string table, long id)
        {
            IdentityEntity identity = this.identityEntities.Find(it => it.Table.ToLower() == table.Replace("`", "").ToLower());
            if (identity == null)
            {
                identity = new IdentityEntity() { Table = table };
                identityEntities.Add(identity);
            }

            identity.Id = id;
        }
        /// <summary>自增id 自增 </summary>
        public virtual void Increment(string table)
        {
            IdentityEntity identity = this.identityEntities.Find(it => it.Table.ToLower() == table.Replace("`", "").ToLower());
            identity.Increment();
        }

    }
}
