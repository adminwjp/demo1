//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )
{{.reference_namespace}}
namespace {{.namespace}}
{
    /// <summary>{{.comment}}</summary>
    public class {{.class}} {{.implement}}
    {
        /// <summary>是否选中 </summary>
        {{if  ef }}
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        {{end}}
        {{if  dapper }}
        [D.NotMapped]
        {{end}}
        public  virtual bool IsSelected { get; set; }

        #region 私有变量 start.......
        {{range  pro := .pros}}
        private {{pro.PropertyType}} {{pro.PrivatePropertyName}};//{{pro.Comment}}
        {{end}}
        #endregion 私有变量 end......


        #region 公共变量 start.......
        {{range pro := .pros}}
        /// <summary>{{pro.Comment}}</summary>
        {{if eq pro.MappFlag 6 and ef }}
        [System.ComponentModel.DataAnnotations.Key]
        {{if gt pro.Length 0 }}
        [System.ComponentModel.DataAnnotations.StringLength(36)]
        {{end}}
        {{end}}

        {{if eq pro.MappFlag 6 and nh }}
        {{if gt pro.Length 0 }}
        [NHibernate.Mapping.Attributes.Id(Length = 36)]
        {{else}}
        [NHibernate.Mapping.Attributes.Id]
        {{end}}
        {{end}}

        {{if eq pro.MappFlag 6 and dapper }}
        [D.Key]
        {{end}}


        {{if eq pro.MappFlag 5 and ef }}
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("{{pro.FkColumnName}}")]
        {{if gt pro.Length 0 }}
        [System.ComponentModel.DataAnnotations.StringLength(36)]
        {{end}}
        {{end}}

        {{if eq pro.MappFlag 5 and nh }}
        {{if gt pro.Length 0 }}
        [NHibernate.Mapping.Attributes.Id(Length = 36)]
        {{else}}
        [NHibernate.Mapping.Attributes.Id]
        {{end}}
        {{end}}

        {{if eq pro.MappFlag 5 and dapper }}
        [D.NotMapped]
        {{end}}

        public virtual {{pro.PropertyType}} {{pro.PropertyName}} { get { return this.{{pro.PrivatePropertyName}}; } set { Set(ref {{pro.PrivatePropertyName}}, value, "{{pro.PropertyName}}"); } }




        #endregion 公共变量 end......

        /// <summary>
        /// 设置属性值 wpf 使用时直接继承 viewModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        /// <param name="propertyName">属性名称 wpf 有效</param>

        protected virtual void Set<T>(ref T oldValue, T newValue, string propertyName)
        {
            oldValue = newValue;
        }

        /// <summary>
        /// 设置属性值 wpf 使用时直接继承 viewModel
        /// </summary>
        /// <param name="propertyName">属性名称 wpf 有效</param>

        protected virtual void Set<T>(string propertyName)
        {
        }
    }
}
#endif