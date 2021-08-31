#if NET40 || NET45 || NET451 || NET452|| NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1

using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Utility.Application.Services.Dtos;
using Utility.Demo.Domain.Entities;
using Utility.Domain.Repositories;
using Utility.Extensions;
using Utility.Nhibernate.Repositories;

namespace Utility.Demo.Nhibernate.Repositories
{
    using Utility.Demo.Domain.Repositories;
    using Utility.Domain.Entities;

#if NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1

    using Utility.Nhibernate;

    //[Component(typeof(IMenuRepository), AutofacScope = AutofacScope.InstancePerLifetimeScope)]
#endif
    public class MenuRepository : BaseNhibernateRepository<MenuEntity, long>, IMenuRepository
    {
        public MenuRepository(SessionProvider session)
           : base(session)
        {
        }

        //element-ui
        public virtual List<MenuEntity> FindCategory()
        {
            return base.UnitWork.Query<MenuEntity>().OrderBy(it => it.Orders).OrderByDescending(it => it.CreationTime)
                .Select(it => new MenuEntity() { Id = it.Id, Name = it.Name, Icon = it.Icon, parent_id = it.Parent.Id })
                .ToList();
        }
        protected virtual string GetTableName()
        {
            return string.Empty;
        }

        public override int Insert(MenuEntity entity)
        {
            if (entity.parent_id>0)
            {
                entity.Parent = new MenuEntity() { Id = entity.parent_id };
            }
          return  base.Insert(entity);
        }


        public override int Update(MenuEntity entity)
        {
            if (entity.parent_id > 0)
            {
                entity.Parent = new MenuEntity() { Id = entity.parent_id };
            }
           return base.Update(entity);
        }



        public void DeleteList(DeleteEntity<string> deleteEntity)
        {

            string sql = $"delete from {GetTableName()} ";
            string id = string.Empty;
            for (int i = 0; i < deleteEntity.Ids.Length; i++)
            {
                var it = deleteEntity.Ids[i];
                
                id += "?";
                if (i != deleteEntity.Ids.Length - 1)
                {
                    id += ",";
                }
            }
            if (id != string.Empty)
            {
                sql += $" where id in ({id})";
            }
            ISession session = base.UnitWork.Write.Session;
            ISQLQuery query = session.CreateSQLQuery(sql);
            IQuery query1=null;
            for (int i = 0; i < deleteEntity.Ids.Length; i++)
            {
                var it = deleteEntity.Ids[i];
                query1 = query1!=null? query1.SetString(i, it):query.SetString(i, it);
            }
            if(query!=null)
            {
                query.ExecuteUpdate();
            }
            else
            {
                query1.ExecuteUpdate();
            }
            //linq 复杂语法不支持
            // Expression<Func<Entity, bool>> where = null;
            //Expression expression1=null;
            //ParameterExpression parameterExpression = Expression.Parameter(typeof(Entity));
            //foreach (var item in deleteEntity.Ids)
            //{
            //    MemberExpression memberExpression = Expression.PropertyOrField(parameterExpression, "Id");
            //    UnaryExpression right = Expression.Convert(((Expression<Func<object>>)(() => item)).Body, memberExpression.Type);
            //    var expression = Expression.Equal(memberExpression,right);
            //    if (expression1 == null)
            //    {
            //        expression1 = expression;
            //    }
            //    else
            //    {
            //         expression1 = Expression.Or(expression1, expression);
            //    }

            //}
            //if (expression1 != null)
            //{
            //    where = Expression.Lambda<Func<Entity, bool>>(expression1,new ParameterExpression[] { parameterExpression });
            //}
            //base.Delete(where);
        }

        public virtual List<MenuEntity> FindList()
        {
            var datas = base.Query(it => it.Parent == null);//树形
            IOrderedQueryable<MenuEntity> orderDatas = datas.OrderBy(it => it.Orders).OrderByDescending(it => it.CreationTime);
            var list = orderDatas.ToList();
            return list;
        }
        public virtual ResultDto<MenuEntity> FindList(int page, int size,string orderSort)
        {
            var datas = base.Query(it => it.Parent == null);//树形
            IOrderedQueryable<MenuEntity> orderDatas = datas.OrderBy(it=>it.Orders).OrderByDescending(it=>it.CreationTime);

            if (!string.IsNullOrEmpty(orderSort))
            {
                Utility.Collections.Collection<string,int> orders = new Utility.Collections.Collection<string, int>();

                Utility.Collections.Array<string> orderSorts = new Utility.Collections.Array<string>(EqualityComparer<string>.Default);
                orderSorts.InsertRange(orderSort.Split(','));

                for (int i = 0; i < orderSorts.Count; i++)
                {
                    string[] val = orderSorts[i].Split(':');
                    if(val!=null&&val.Length==2)
                    {
                        orders[val[0]] = "desc".Equals(val[1].ToLower())?1:0;
                    }
                }
                if (orders.IndexOf("id") > -1)
                {
                    if (orders["id"]==0)
                    {
                        orderDatas = orderDatas.OrderBy(it => it.Id);
                    }
                    else
                    {
                        orderDatas = orderDatas.OrderByDescending(it => it.Id);
                    }
                }
                if (orders.IndexOf("orderid") > -1)
                {
                    if (orders["orderid"] == 0)
                    {
                        orderDatas = orderDatas.OrderBy(it => it.Orders);
                    }
                    else
                    {
                        orderDatas = orderDatas.OrderByDescending(it => it.Orders);
                    }
                }
            }
            var list = orderDatas.Skip((page - 1) * size).Take(size).ToList() ;
            int count = datas.Count();//子集不算 上
            return new ResultDto<MenuEntity>(list, page, size, count);
        }

    }
}
#endif