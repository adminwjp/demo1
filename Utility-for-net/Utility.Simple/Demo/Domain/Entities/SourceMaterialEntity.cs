using System;
using System.Collections.Generic;
using System.Text;
using Utility.Domain.Entities;

namespace Utility.Demo.Domain.Entities
{
    /// <summary>素材</summary>
    public class SourceMaterialEntity: SourceMaterialEntity<long>
    {

    }
    /// <summary>素材</summary>
    public class SourceMaterialEntity<K> : Entity<K>
    {
        private string src;
        private string key;
        private byte[] buffer;
        private string base64;
        private string description = "这个人很懒,什么也没留下!";
        private string buket;
        private string objectName;

        /// <summary>素材 地址</summary>
        public virtual string Src { get => src; set { Set(ref src, value, "Src"); } }
        /// <summary>素材 id</summary>
        public virtual string Key { get => key; set { Set(ref key, value, "Key"); } }
        /// <summary>素材 </summary>
        public virtual byte[] Buffer { get => buffer; set { Set(ref buffer, value, "Buffer"); } }
        /// <summary>素材 Base64</summary>
        public virtual string Base64 { get => base64; set { Set(ref base64, value, "Base64"); } }
        /// <summary>素材 描述</summary>
        public virtual string Description { get => description; set { Set(ref description, value, "Description"); } }
        /// <summary>素材 桶</summary>
        public virtual string Buket { get => buket; set { Set(ref buket, value, "Buket"); } }
        /// <summary>素材 对象名称</summary>
        public virtual string ObjectName { get => objectName; set { Set(ref objectName, value, "ObjectName"); } }
    }
}
