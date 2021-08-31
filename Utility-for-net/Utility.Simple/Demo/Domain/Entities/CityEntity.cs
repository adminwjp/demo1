using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Domain.Entities;

namespace Utility.Demo.Domain.Entities
{
    /// <summary>地区 </summary>
    public class CityEntity : CityEntity<CityEntity, long>
    {
        private long? parentId;
        private CityEntity parent;
        private ICollection<CityEntity> children;

        /// <summary>父id </summary>
        public virtual long? ParentId { get => parentId; set => parentId = value; }
        /// <summary>父地区 </summary>
        public virtual CityEntity Parent { get => parent; set => parent = value; }
        /// <summary>子地区 </summary>
        public virtual ICollection<CityEntity> Children { get => children; set => children = value; }
    }
    /// <summary>地区 </summary>
    public class CityEntity<CityModel, Key> : Entity<Key>
        where CityModel : CityEntity<CityModel, Key>
    {
        private string prodive;
        private string city;
        private string area;
        private string prodiveCode;
        private string cityCode;
        private string areaCode;

        /// <summary>省 </summary>
        public virtual string Prodive { get => prodive; set { Set(ref prodive, value, "Prodive"); } }
        /// <summary>市 </summary>
        public virtual string City { get => city; set { Set(ref city, value, "City"); } }
        /// <summary>区 </summary>
        public virtual string Area { get => area; set { Set(ref area, value, "Area"); } }
        /// <summary>省 编码 </summary>
        public virtual string ProdiveCode { get => prodiveCode; set { Set(ref prodiveCode, value, "ProdiveCode"); } }
        /// <summary>市 编码 </summary>
        public virtual string CityCode { get => cityCode; set { Set(ref cityCode, value, "CityCode"); } }
        /// <summary>区 编码</summary>
        public virtual string AreaCode { get => areaCode; set { Set(ref areaCode, value, "AreaCode"); } }


    }
}
