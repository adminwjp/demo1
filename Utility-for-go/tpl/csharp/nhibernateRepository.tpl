//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
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
    #endif