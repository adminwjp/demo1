#if  NET461 || NET462|| NET47 || NET471 || NET472|| NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1

using Utility.Demo.Domain.Entities;
using Utility.Demo.Domain.Repositories;
using System.Collections.Generic;
using Utility.Json.Extensions;
using Utility.Json;

namespace Utility.Demo.Application.Services
{
    public class MenuManager
    {
        public static void InitData(IMenuRepository menuRepository,bool nhibernate=false)
        {
            if(menuRepository.Count(it => it.Soure == MenuEntity.Source) == 0)
            {

                var parent = new MenuEntity() { Name = "菜单",Soure= MenuEntity.Source }.Create();
                var chil= new MenuEntity() { Name = "菜单管理", Soure = MenuEntity.Source }.Create();

                if (nhibernate)
                {
                    //nhibernate 自带不关联外键
                    //chil.ParentId = parent.Id;
                    chil.Parent = parent;//这样才有效
                    //chil.Parent = new MenuEntity() { Id=parent.Id};//这样才有效
                }


                parent.Children = new HashSet<MenuEntity>() { chil };
                MenuEntity[] menuEntities = new MenuEntity[] {
                    parent
                };

                //MenuEntity[] menuEntities = new MenuEntity[] {
                //    parent,
                //    chil
                //};
                //if (!nhibernate)
                //{
                //    chil.ParentId = parent.Id;
                //}

                menuRepository.BatchInsert(menuEntities);
            }
        }

        public static void InitDataByNhibernateAndEasyUI(IMenuRepository repository)
        {
            if (repository.Count(it => it.Soure == MenuEntity.Source) == 0)
            {
                var parent = new MenuEntity() { Text = "菜单", Orders = 0, Soure = MenuEntity.Source }.Create();
                Dictionary<string, string> attrs = new Dictionary<string, string>() { ["url"] = "menu/index.html" };
                var chil = new MenuEntity() { Text = "菜单管理", Orders = 1, Soure = MenuEntity.Source, AttributesJson = JsonHelper.ToJson(attrs) }.Create();
                //外键不关联 需要手动
                //chil.ParentId = parent.Id; //nhibernate 自带不关联外键
                //方案1
                chil.Parent = parent;//这样才有效
                //方案2
                //chil.Parent = new MenuEntity() { Id=parent.Id};//这样才有效

                //1.使用方式1：
                parent.Children = new HashSet<MenuEntity>() { chil };
                MenuEntity[] menuEntities = new MenuEntity[] {
                    parent
                };

                //2.使用方式2：

                //MenuEntity[] menuEntities = new MenuEntity[] {
                //    parent,
                //    chil
                //};

                repository.BatchInsert(menuEntities);
            }
        }
        public static void InitDataByEfAndEasyUI(IMenuRepository repository)
        {
            if (repository.Count(it => it.Soure == MenuEntity.Source) == 0)
            {
                var parent = new MenuEntity() { Text = "菜单", Orders = 0, Soure = MenuEntity.Source }.Create();
                Dictionary<string, string> attrs = new Dictionary<string, string>() { ["url"] = "menu/index.html" };
                var chil = new MenuEntity() { Text = "菜单管理", Orders = 1, Soure = MenuEntity.Source, AttributesJson = JsonHelper.ToJson(attrs) }.Create();
                //方案1 
                parent.Children = new HashSet<MenuEntity>() { chil };
                MenuEntity[] menuEntities = new MenuEntity[] {
                    parent
                };

                //方案2 
                // chil.ParentId = parent.Id;
                //EasyUIMenuEntity[] menuEntities = new EasyUIMenuEntity[] {
                //    parent,
                //    chil
                //};

                repository.BatchInsert(menuEntities);
            }
        }

        public static void InitDataByNhibernateAndElement(IMenuRepository repository)
        {
            if (repository.Count(it => it.Soure == MenuEntity.Source) == 0)
            {
                var parent = new MenuEntity() { Name = "菜单", Orders = 0, Soure = MenuEntity.Source }.Create();
                var chil = new MenuEntity() { Name = "菜单管理", Orders = 1, Soure = MenuEntity.Source, Href = "menu/index.html" }.Create();

                //外键不关联 需要手动
                //chil.ParentId = parent.Id; //nhibernate 自带不关联外键
                //方案1
                chil.Parent = parent;//这样才有效
                //方案2
                //chil.Parent = new EasyUIMenuEntity() { Id=parent.Id};//这样才有效

                //1.使用方式1：
                parent.Children = new HashSet<MenuEntity>() { chil };
                MenuEntity[] menuEntities = new MenuEntity[] {
                    parent
                };

                //2.使用方式2：
                //MenuEntity[] menuEntities = new MenuEntity[] {
                //    parent,
                //    chil
                //};

                repository.BatchInsert(menuEntities);
            }
        }

        public static void InitDataByEfAndElement(IMenuRepository repository)
        {
            if (repository.Count(it => it.Soure == MenuEntity.Source) == 0)
            {
                var parent = new MenuEntity() { Name = "菜单", Orders = 0, Soure = MenuEntity.Source }.Create();
                var chil = new MenuEntity() { Name = "菜单管理", Orders = 1, Soure = MenuEntity.Source, Href = "menu/index.html" }.Create();
                //1.使用方式1：
                parent.Children = new HashSet<MenuEntity>() { chil };
                MenuEntity[] menuEntities = new MenuEntity[] {
                    parent
                };

                //2.使用方式2：
                //chil.ParentId = parent.Id;
                //MenuEntity[] menuEntities = new MenuEntity[] {
                //    parent,
                //    chil
                //};

                repository.BatchInsert(menuEntities);
            }
        }

    }
}
#endif