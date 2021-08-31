using System;
using System.Collections.Generic;
using System.IO;
using Wpf.Crawl;
using Wpf.OA;
using OA.Domain.Entities;
using Utility.Json;
using Utility.Wpf.Attributes;
using Utility.Wpf.Entries;
using Utility.Wpf;
using Utility.Helpers;

namespace Example.WpfApp.Common
{
    public class ConfigManager
    {
        public static readonly IDictionary<Type, List<ListEntry>> CacheListModel = new Dictionary<Type, List<ListEntry>>();
        public static readonly IDictionary<string, ListEntry> CacheFlagListModel = CacheListModelManager.CacheFlagListEntry;
        public static readonly IDictionary<string, int> CacheFlagValues = new Dictionary<string, int>();
        public static readonly string DateFormat = "yyyy-MM-dd HH:mm:ss";
        public static void Load()
        {
            var type = typeof(OAFlag);
            var menuGroupAttribute=AttributeHelper.Get<MenuGroupAttribute>(type.GetCustomAttributes(false));
            if (menuGroupAttribute != null)
            {
                var data = JsonHelper.ToObject<List<ListEntry>>(File.ReadAllText(menuGroupAttribute.Config));
                CacheListModel.Add(type, data);
                foreach (var item in data)
                {
                    if (item.Id == null)
                    {
                        item.Id = "Id";
                    }
                    item.Columns.Insert(0, new ColumnEntry() {Header="编号", ColumnType= ColumnType.TextBox,Name="Id",Flag= ColumnEditFlag.Disabled });
                    item.Columns.Insert(item.Columns.Count, new ColumnEntry() { Header = "创建时间", ColumnType =ColumnType.TextBox, Name = "CreateDate",StringFormat= DateFormat, Flag = ColumnEditFlag.Disabled });
                    item.Columns.Insert(item.Columns.Count, new ColumnEntry() { Header = "修改时间", ColumnType = ColumnType.TextBox, Name = "UpdateDate", StringFormat = DateFormat, Flag = ColumnEditFlag.Disabled });
                }
                LoadFlag(type, OAFlag.AccountItem);
                BindOAMethod();
            }

            type = typeof(CrawlFlag);
            menuGroupAttribute = AttributeHelper.Get<MenuGroupAttribute>(type.GetCustomAttributes(false));
            if (menuGroupAttribute != null)
            {
                var data = JsonHelper.ToObject<List<ListEntry>>(File.ReadAllText(menuGroupAttribute.Config));
                CacheListModel.Add(type, data);
                foreach (var item in data)
                {
                    if (item.Id == null)
                    {
                        item.Id = "Id";
                    }
                }
                LoadFlag(type, CrawlFlag.Task);
            }
        }
        private static void BindOAMethod()
        {
            foreach (var item in CacheFlagValues)
            {
                Func<int, int, object> findList = (page, size) => { return OAService.FindList((OAFlag)item.Value, page, size); };
                Func<object, object> add = (obj) => { 
                    if(obj is BaseEntity baseEntity)
                    {
                        baseEntity.CreateDate = DateTime.Now;
                    }
                    return OAService.Add((OAFlag)item.Value, obj); 
                };
                Action<object> edit = (obj) => {
                    if (obj is BaseEntity baseEntity)
                    {
                        baseEntity.UpdateDate = DateTime.Now;
                    }
                    OAService.Save((OAFlag)item.Value, obj); 
                };
                Action<object> delete = (obj) => { OAService.Delete((OAFlag)item.Value, obj); };

                var method = new MethodTemplateEntry();
                method.FindList -= findList;
                method.Add -= add;
                method.Modify -= edit;
                method.Delete -= delete;

                method.FindList += findList;
                method.Add += add;
                method.Modify += edit;
                method.Delete += delete;
                CacheListModelManager.CacheFlagMethod[item.Key] = method;
            }
        }
        private static void LoadFlag(Type type,object obj)
        {
            foreach (var item in type.GetFields())
            {
                var val = item.GetValue(obj);
                var name = $"{type.FullName}.{item.Name}";
                int v = (int)val;
                CacheFlagValues[name] = v;
                CacheFlagListModel[name] = GetListModel(type, v);
            }
        }
        private static ListEntry GetListModel(Type type,int flag)
        {
            List<ListEntry> listModels = CacheListModel[type];
            foreach (var item in listModels)
            {
                if (item.Flag == flag)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
