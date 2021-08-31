package template


EfDALStringCode := `//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
//#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System.Linq.Expressions;
using System;
using Utility.Extensions;
using Utility.Domain.Uow;

namespace ${ProgramName}.Ef.DAL
{
    /// <summary>${Comment} ef 数据访问层接口 实现  </summary>
    public    class ${ClassName} : Utility.Ef.DAL.BaseEfDAL<Entity, string>, 
       IDAL<Entity, string>, ${ProgramName}.DAL.I${ClassName}
        where Entity : class,IModel<Entity, string>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name='content'></param>
        public ConfigEfDAL(ConfigDbContext content) : base(content)
        {
        }
        /// <summary>查询wehere sql </summary>
        /// <param name='obj'>${Comment} 信息</param>
        /// <returns></returns>
        protected override Expression<Func<${ProgramName}.Model.${ClassModelName}, bool>> QueryWhere(${ProgramName}.Model.${ClassModelName} obj)
        {
            Expression<Func<${ProgramName}.Model.${ClassModelName}, bool>> where = base.QueryWhere(obj);
            ${WhereStringCode}
            return where;
        }


    }
}
#endif`


BaseModelWpfStringCode := `        /// <summary>
        /// 设置属性值 wpf 使用时直接继承 viewModel
        /// </summary>
        /// <typeparam name='T'></typeparam>
        /// <param name='oldValue'>旧值</param>
        /// <param name='newValue'>新值</param>
        /// <param name='propertyName'>属性名称 wpf 有效</param>

        protected virtual void Set<T>(ref T oldValue, T newValue, string propertyName)
        {
            oldValue = newValue;
        }";

        public const string ModelTemplateStringCode = @"//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )
using D = Dapper;


namespace ${Namespace}
{
    /// <summary>${ClassComment}</summary>
    public class ${ClassName} 
    {
        #region 私有变量 start.......
        ${PrivateStringCode}
        #endregion 私有变量 end......


        #region 公共变量 start.......

        ${PublicStringCode}

#endregion 公共变量 end...... 

         ${WpfStringCode}
    }
}
#endif`


NHibernateDALStringCode = `//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )
using NHibernate.Criterion;
using System.Collections.Generic;
using Utility.DAL;
using NHibernate;

namespace ${ProgramName}.Nhibernate.DAL
{
    /// <summary>${Comment} nhibernate 数据访问层接口 实现   </summary>
    public    class ${ClassName} : Utility.Nhibernate.DAL.BaseNhibernateDAL<${ProgramName}.Model.${ClassModelName}, string>, ${ProgramName}.DAL.I${ClassName}, 
        IDAL<Config.Model.ConfigModel, string> 
    {
        /// <summary>
        /// ${Comment} nhibernate 数据访问层
        /// </summary>
        /// <param name='session'></param>
        public {ClassName}(ISession session) : base(session)
        {
        }
        /// <summary>
        /// 模糊查询 通用查询 默认实现
        /// </summary>
        /// <param name='criterias'></param>
        /// <param name='obj'>${Comment}</param>
        /// <returns></returns>
        protected override void QueryFilterByOr(List<NHibernate.Criterion.AbstractCriterion> criterias, ${ProgramName}.Model.${ClassModelName} obj)
        {
            ${WhereStringCode}
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns></returns>
        protected override string GetTable()
        {
            return ${ProgramName}.Model.${ClassModelName}.TableName;
        }

    }
}
#endif`


NHibernateMappStringCode = `//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )
using Config.Model;

namespace ${Namespace}
{
    /// <summary>nhibernate ${ClassComment} xml mapp 必须虚方法(属性)不然错误 ,注解可以不需要 虚方法(属性) </summary>
    public  class ${ClassName} 
    {
       ${StringCode}
    }
}
#endif`