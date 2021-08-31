//using Config.DAL;
//using Config.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Utility.IO;
//using Utility.Json;
//using Utility.ObjectMapping;
//using Utility.Wpf;
//using Utility.Wpf.Entries;
//using Wpf.Config;

//namespace Wpf
//{
//    public class ConfigManager
//    {
//        public static readonly string DateFormat = "yyyy-MM-dd HH:mm:ss";
//        public static void Load()
//        {
//            BindConfig();
//            BindConfigMethod();
//        }
//        private static void BindConfigMethod()
//        {
//            IConfigDAL configDAL=StartManager.IocManager.Get<IConfigDAL>();
//            IObjectMapper objectMapper = StartManager.IocManager.Get<IObjectMapper>();
//            CacheListModelManager.CacheFlagMethod["Config.Config"] = new MethodTemplateEntry()
//            {
//                Add = (it) => {
//                    ConfigModel configModel = objectMapper.Map<ConfigModel>(it);
//                    configModel.CreateDate = DateTime.Now;
//                   return configDAL.Insert(configModel);
//                },
//                Modify = (it) => {
//                    ConfigModel configModel = objectMapper.Map<ConfigModel>(it);
//                    configModel.LastDate = DateTime.Now;
//                    configDAL.Update(configModel);
//                },
//                Delete = (it) => {
//                    ConfigViewModel configViewModel = (ConfigViewModel)it;
//                    configDAL.Delete(configViewModel.Id);
//                },
//                DeleteList = (it) => {
//                    ConfigViewModel[] configViewModels = (ConfigViewModel[])it;
//                    string[] ids = configViewModels.Select(it1 => it1.Id).ToArray();
//                    configDAL.DeleteList(ids);
//                },
//                FindListByWhere = (it,page, size) => {
//                    ConfigModel configModel = it==null? ConfigViewModel .Empty: objectMapper.Map<ConfigModel>(it);
//                    return  configDAL.FindListByPage(configModel,page,size);
//                },
//            };
//        }

//        private static void BindConfig()
//        {
//            string json = FileHelper.ReadFile("Config/config.json");
//            List<MuilDataEntry> muilDataEntries = JsonHelper.ToObject<List<MuilDataEntry>>(json);
//            if (muilDataEntries != null)
//            {
//                foreach (var muilDataEntry in muilDataEntries)
//                {
//                    CacheListModelManager.CacheFlagMuilDataEntry[muilDataEntry.Flag] = muilDataEntry;
//                }
//            }
//        }
//    }
//}
