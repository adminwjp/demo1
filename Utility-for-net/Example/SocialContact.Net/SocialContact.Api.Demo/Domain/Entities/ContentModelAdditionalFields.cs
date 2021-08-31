//<tunynetcopyright>
//--------------------------------------------------------------
//<version>v0.5</verion>
//<createdate>2017-01-20</createdate>
//<author>wanglei</author>
//<email>wanglei@tunynet.com</email>
//<log date="2017-01-20" version="0.5">新建</log>
//--------------------------------------------------------------
//</tunynetcopyright>

using PetaPoco;
using System;
using Tunynet.Caching;

namespace Tunynet.CMS
{
    /// <summary>
    /// 内容模型额外字段
    /// </summary>
    [TableName("tn_ContentModelAdditionalFields")]
    [PrimaryKey("FieIdId", autoIncrement = true)]
    [CacheSetting(true)]
    [Serializable]
    public class ContentModelAdditionalFields : IEntity
    {
        /// <summary>
        ///
        /// </summary>
        public static ContentModelAdditionalFields New()
        {
            ContentModelAdditionalFields contentModelAdditionalField = new ContentModelAdditionalFields()
            {
                FieldName = string.Empty,
                FieldLabel = string.Empty,
                DataType = string.Empty,
                DefaultValue = string.Empty
            };
            return contentModelAdditionalField;
        }

        #region 需持久化属性

        /// <summary>
        ///FieIdId
        /// </summary>
        public int FieIdId { get; set; }

        /// <summary>
        ///模型Id
        /// </summary>
        public int ModelId { get; set; }

        /// <summary>
        ///字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        ///字段标签
        /// </summary>
        public string FieldLabel { get; set; }

        /// <summary>
        ///字段对应的C#类型，可选值：int,long,float,decimal,string,datetime,bool
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        ///默认值
        /// </summary>
        public string DefaultValue { get; set; }

        #endregion 需持久化属性

        #region IEntity 成员

        object IEntity.EntityId { get { return this.FieIdId; } }

        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion IEntity 成员
    }
}