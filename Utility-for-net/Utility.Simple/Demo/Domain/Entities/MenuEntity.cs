
using System;
using Utility.Domain.Entities;
using Utility.Helpers;

namespace Utility.Demo.Domain.Entities
{    
    /// <summary>菜单信息 </summary>
    public class MenuEntity: MenuEntity<MenuEntity, long>, IEntity<long>, ICascade<MenuEntity, long>
    {
      
        public virtual new MenuEntity Create()
        {
            //this.Id = Guid.NewGuid().ToString("N");
            this.CreationTime = CommonHelper.TotalMilliseconds();
            return this;
        }
    }
    //nh ex Could not determine type for 
    /// <summary>菜单信息 继承 Entity </summary>
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
    [Serializable]
#endif
    public  class MenuEntity<Menu,Key>:Entity<Key>,
        IHasCreationTime,IHasModificationTime,IHasDeletionTime//,ICascade<Menu, Key>
        where Menu: MenuEntity<Menu, Key>
    {
        public const string Source = "menu";
        public const string TabelName = "Menu";
        //easyui
        public const string StateOpen = "open";
        public const string StateClosed = "closed";

        private string _text;//显示节点文本。
        private string _state;// state：节点状态，'open' 或 'closed'，默认：'open'。如果为'closed'的时候，将不自动展开该节点。
        private bool _checked;//表示该节点是否被选中。
        //private  IDictionary<string, object> _attributes;
        private string _attributesJson;
        private string _iconCls;

        //element-ui
        private string _name;//名称
        private bool _collpse;//是否折叠
        private string _group;//分组名称
        private string _icon;//element-ui的图标
        private string _href;//地址
        private string _description;//描述


        //private string _name;//名称
        private string _huiIcon;//hui框架的 图标
        //private string _href;//地址
        private string _idName;//hui框架 html id选择器名称

        //private string _name;//名称
        private string _aceIcon;//ace 框架的 图标
        //private string _href;//地址

        public const int MaxNameLength = 10;
        public const int MaxDescriptionLength = 500;
        public const int MaxIconLength = 50;

        private string _source;//来源
        private int _orderId;//排序
        //private Menu _parent;//父菜单
        //private System.Collections.Generic.ICollection<Menu> _children;//菜单子集
        private long _creationTime;//创建时间
        private long _lastModificationTime;//更新时间
        private long _deletionTime;//软删除时间
    
        private bool _isDeleted;//ASP.NET Boilerplate开箱即用地实现了软删除模式。删除软删除实体时，ASP.NET Boilerplate会检测到此情况，防止删除，
                                //将IsDeleted设置为true，然后更新数据库中的实体。它还不会通过自动过滤从数据库中检索（选择）软删除的实体
        /// <summary>菜单信息 </summary>
        public MenuEntity()
        {

        }
        /// <summary>菜单信息 </summary>
        public MenuEntity(bool create = true)
        {
            if (create)
            {
                Create();
            }
        }
        //  nhibernate mapp 报错 必须虚方法 xml 不需要
        public virtual MenuEntity<Menu,Key>
            Create()
        {
            //this.Id = Guid.NewGuid().ToString("N");
            this.CreationTime = CommonHelper.TotalMilliseconds();
            return this;
        }


        /// <summary>来源 </summary>
        public virtual string Soure { get { return this._source; } set { Set(ref _source, value, "Soure"); } }

        /// <summary>排序</summary>
        public virtual int Orders { get { return this._orderId; } set { Set(ref _orderId, value, "Orders"); } }

       
        /// <summary>创建时间</summary>
        public virtual long CreationTime { get { return this._creationTime; } set { Set(ref _creationTime, value, "CreationTime"); } }
        /// <summary>更新时间</summary>
        public virtual long LastModificationTime { get { return this._lastModificationTime; } set { Set(ref _lastModificationTime, value, "LastModificationTime"); } }
        /// <summary>软删除标识 </summary>
        public virtual bool IsDeleted { get { return this._isDeleted; } set { Set(ref _isDeleted, value, "IsDeleted"); } }

    

        /// <summary>软删除时间 </summary>
        public virtual long DeletionTime { get { return this._deletionTime; } set { Set(ref _deletionTime, value, "DeletionTime"); } }



        //easyui 
        /// <summary>
        /// text：显示节点文本。
        /// </summary>
        public string Text { get { return this._text; } set { Set(ref _text, value, "Text"); } }
        /// <summary>
        /// state：节点状态，'open' 或 'closed'，默认：'open'。如果为'closed'的时候，将不自动展开该节点。
        /// </summary>
        public string State { get { return this._state; } set { Set(ref _state, value, "State"); } }
        /// <summary>
        /// checked：表示该节点是否被选中。
        /// </summary>
        public bool Checked { get { return this._checked; } set { Set(ref _checked, value, "Checked"); } }
        /// <summary>
        /// attributes: 被添加到节点的自定义属性。
        /// </summary>
       // public IDictionary<string, object> Attributes { get { return this._attributes; } set { Set(ref _attributes, value, "Attributes"); } }

        public string AttributesJson { get { return this._attributesJson; } set { Set(ref _attributesJson, value, "AttributesJson"); } }
        /// <summary>
        /// 图标
        /// </summary>
        [Newtonsoft.Json.JsonProperty("IconCls")]
        public string IconCls { get { return this._iconCls; } set { Set(ref _iconCls, value, "IconCls"); } }


        //element-ui

        /// <summary>名称</summary>
        public virtual string Name { get { return this._name; } set { Set(ref _name, value, "Name"); } }
        /// <summary>
        /// 是否折叠
        /// </summary>
        public virtual bool Collpse { get { return this._collpse; } set { Set(ref _collpse, value, "Collpse"); } }
        /// <summary>
        /// 分组名称
        /// </summary>
        public virtual string Groups { get { return this._group; } set { Set(ref _group, value, "Groups"); } }
        /// <summary>图标</summary>
        public virtual string Icon { get { return this._icon; } set { Set(ref _icon, value, "Icon"); } }
        /// <summary>地址</summary>
        public virtual string Href { get { return this._href; } set { Set(ref _href, value, "Href"); } }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get { return this._description; } set { Set(ref _description, value, "Description"); } }


        /// <summary>图标</summary>
        public virtual string HuiIcon { get { return this._huiIcon; } set { Set(ref _huiIcon, value, "Icon"); } }
        /// <summary>hui框架 html id选择器名称</summary>
        public virtual string IdName { get { return this._idName; } set { Set(ref _idName, value, "IdName"); } }

        /// <summary>图标</summary>
        public virtual string AceIcon { get { return this._aceIcon; } set { Set(ref _aceIcon, value, "AceIcon"); } }

        /// <summary>父菜单 Id</summary>
        public virtual long parent_id { get; set; }
        private Menu _parent;//父菜单
        private System.Collections.Generic.ISet<Menu> _children;//菜单子集

        /// <summary>父菜单 </summary>
        //[ForeignKey("ParentId")]
        public virtual Menu Parent { get { return this._parent; } set { Set(ref _parent, value, "Parent"); } }
        /// <summary>子菜单 </summary>
        //[ForeignKey("ParentId")]
        public virtual System.Collections.Generic.ISet<Menu> Children { get { return this._children; } set { Set(ref _children, value, "Children"); } }

    }


}
