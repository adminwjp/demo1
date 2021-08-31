#if   NET40 ||NET45 || NET451 || NET452 || NET46 ||NET461 || NET462|| NET47 || NET471 || NET472|| NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.EntityFrameworkCore;
#endif
#if   NET40 ||NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System.Data.Entity;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using Utility.Ef.Repositories;
using Utility.Domain.Entities;
using System.Linq.Expressions;
using Utility.Extensions;
using Utility.Application.Services.Dtos;
using Utility.Demo.Domain.Entities;
using Utility.Demo.Domain.Repositories;
using Utility.Ef;

namespace Utility.Demo.Ef.Repositories
{
    public class MenuRepository: BaseEfRepository<DemoDbContext,MenuEntity,long> , IMenuRepository
    {
        public MenuRepository(DbContextProvider<DemoDbContext> session)
          : base(session)
        {
        }

      

        public virtual void DeleteList(DeleteEntity<string> deleteEntity)
        {
            base.UnitWork.DeleteList<MenuEntity, string>(deleteEntity.Ids);
        }
        public override IQueryable<MenuEntity> Query(Expression<Func<MenuEntity, bool>> exp = null)
        {
            return base.Query(exp).Include(it=>it.Children);
        }

        //element-ui
        public virtual List<MenuEntity> FindCategory()
        {
            return base.UnitWork.Query<MenuEntity>().OrderBy(it => it.Orders).OrderByDescending(it => it.CreationTime)
                .Select(it => new MenuEntity() { Id = it.Id, Name = it.Name, Icon = it.Icon, parent_id = it.parent_id })
                .ToList();
        }

        public virtual List<MenuEntity> FindList()
        {
            var datas = base.Query(it => it.parent_id >0);//树形
            var list = datas.OrderBy(it => it.Orders).OrderByDescending(it => it.CreationTime).Include(it => it.Children).ToList();
            return list;
        }



        public virtual ResultDto<MenuEntity> FindList(int page, int size, string orderSort)
        {
            var datas = base.Query(it => it.parent_id==0);//树形
            IOrderedQueryable<MenuEntity> orderDatas = datas.OrderBy(it => it.Orders).Include(it=>it.Children).OrderByDescending(it => it.CreationTime);

            if (!string.IsNullOrEmpty(orderSort))
            {
                Utility.Collections.Collection<string, int> orders = new Utility.Collections.Collection<string, int>();

                Utility.Collections.Array<string> orderSorts = new Utility.Collections.Array<string>(EqualityComparer<string>.Default);
                orderSorts.InsertRange(orderSort.Split(','));

                for (int i = 0; i < orderSorts.Count; i++)
                {
                    string[] val = orderSorts[i].Split(':');
                    if (val != null && val.Length == 2)
                    {
                        orders[val[0]] = "desc".Equals(val[1].ToLower()) ? 1 : 0;
                    }
                }
                if (orders.IndexOf("id") > -1)
                {
                    if (orders["id"] == 0)
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
            var list = orderDatas.Skip((page - 1) * size).Take(size).ToList();
            int count = datas.Count();//子集不算 上
            return new ResultDto<MenuEntity>(list, page, size, count);
        }
    }
}
#endif