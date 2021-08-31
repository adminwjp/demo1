using OA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility.Domain.Uow;
using Utility.Ioc;
using Utility.Mappers;
using Utility.Wpf;
using Utility.Wpf.ViewModels;
using OA.Wpf.ViewModels;

namespace Wpf.OA
{
    public sealed class OAService
    {
        public static void RegisterDataSource()
        {
            CacheListModelManager.CacheDataSource["OA.Role"] = GetRole;
            CacheListModelManager.CacheDataSource["OA.Content"] = GetContent;
            CacheListModelManager.CacheDataSource["OA.Record"] = GetRecord; 
            CacheListModelManager.CacheDataSource["OA.ParentModule"] = GetParentModules;
            CacheListModelManager.CacheDataSource["OA.User"] = GetUsers;
            CacheListModelManager.CacheDataSource["OA.ReckoningName"] = GetReckoningNames;
            CacheListModelManager.CacheDataSource["OA.AccountItem"] = GetAccountItems;
            CacheListModelManager.CacheDataSource["OA.ParentRole"] = GetAccountItems;
            //父模块 自关联  无法 更新 bug
        }
        
        public static object FindList(OAFlag flag,int page, int size)
        {
            switch (flag)
            {
                case OAFlag.AccountItem:
                    return AutoMapperMapper.Empty.Map<List<AccountItemViewModel>>(OANHibernateManager.FindList<AccountItemEntity>(page, size));
                case OAFlag.AuthorityOperator:
                    return AutoMapperMapper.Empty.Map<List<AuthorityOperatorViewModel>>(OANHibernateManager.FindList<AuthorityOperatorEntity>(page, size));
                case OAFlag.BringUpContent:
                    return AutoMapperMapper.Empty.Map<List<BringUpContentViewModel>>(OANHibernateManager.FindList<BringUpContentEntity>(page, size));
                case OAFlag.BringUpPerson:
                    return AutoMapperMapper.Empty.Map<List<BringUpPersonViewModel>>(OANHibernateManager.FindList<BringUpPersonEntity>(page, size));
                case OAFlag.Duty:
                    return AutoMapperMapper.Empty.Map<List<DutyViewModel>>(OANHibernateManager.FindList<DutyEntity>(page, size));
                case OAFlag.FamousRace:
                    return AutoMapperMapper.Empty.Map<List<FamousRaceViewModel>>(OANHibernateManager.FindList<FamousRaceEntity>(page, size));
                case OAFlag.Module:
                    return AutoMapperMapper.Empty.Map<List<ModuleViewModel>>(OANHibernateManager.FindList<ModuleEntity>(page, size));
                case OAFlag.Person: 
                    return AutoMapperMapper.Empty.Map<List<PersonViewModel>>(OANHibernateManager.FindList<PersonEntity>(page, size));
                case OAFlag.ReawrdsAndPunishment:
                    return AutoMapperMapper.Empty.Map<List<ReawrdsAndPunishmentViewModel>>(OANHibernateManager.FindList<ReawrdsAndPunishmentEntity>(page, size));
                case OAFlag.ReckoningList:
                    return AutoMapperMapper.Empty.Map<List<ReckoningListViewModel>>(OANHibernateManager.FindList<ReckoningListEntity>(page, size));
                case OAFlag.ReckoningName:
                    return AutoMapperMapper.Empty.Map<List<ReckoningNameViewModel>>(OANHibernateManager.FindList<ReckoningNameEntity>(page, size));
                case OAFlag.Reckoning:
                    return AutoMapperMapper.Empty.Map<List<ReckoningViewModel>>(OANHibernateManager.FindList<ReckoningEntity>(page, size));
                case OAFlag.Role:
                    return AutoMapperMapper.Empty.Map<List<RoleViewModel>>(OANHibernateManager.FindList<RoleEntity>(page, size));
                case OAFlag.TimeCard:
                    return AutoMapperMapper.Empty.Map<List<TimeCardViewModel>>(OANHibernateManager.FindList<TimeCardEntity>(page, size));
                case OAFlag.User:
                    //return AutoMapperMapper.Empty.Map<List<UserViewModel>>(OANHibernateManager.FindList<OAUserEntity>(page, size));
                default:
                    throw new NotSupportedException();
            }
        }
        public static long Count(OAFlag flag)
        {
            switch (flag)
            {
                case OAFlag.AccountItem:
                    return OANHibernateManager.Count<AccountItemEntity>();
                case OAFlag.AuthorityOperator:
                    return OANHibernateManager.Count<AuthorityOperatorEntity>();
                case OAFlag.BringUpContent:
                    return OANHibernateManager.Count<BringUpContentEntity>();
                case OAFlag.BringUpPerson:
                    return OANHibernateManager.Count<BringUpPersonEntity>();
                case OAFlag.Duty:
                    return OANHibernateManager.Count<DutyEntity>();
                case OAFlag.FamousRace:
                    return OANHibernateManager.Count<FamousRaceEntity>();
                case OAFlag.Module:
                    return OANHibernateManager.Count<ModuleEntity>();
                case OAFlag.Person:
                    return OANHibernateManager.Count<PersonEntity>();
                case OAFlag.ReawrdsAndPunishment:
                    return OANHibernateManager.Count<ReawrdsAndPunishmentEntity>();
                case OAFlag.ReckoningList:
                    return OANHibernateManager.Count<ReckoningListEntity>();
                case OAFlag.ReckoningName:
                    return OANHibernateManager.Count<ReckoningNameEntity>();
                case OAFlag.Reckoning:
                    return OANHibernateManager.Count<ReckoningEntity>();
                case OAFlag.Role:
                    return OANHibernateManager.Count<RoleEntity>();
                case OAFlag.TimeCard:
                    return OANHibernateManager.Count<TimeCardEntity>();
                case OAFlag.User:
                   // return OANHibernateManager.Count<OAUserEntity>();
                default:
                    throw new NotSupportedException();
            }
        }
        public static int Add(OAFlag flag,object  obj) 
        {
            switch (flag)
            {
                case OAFlag.AccountItem:
                    return OANHibernateManager.Add(AutoMapperMapper.Empty.Map<AccountItemEntity>(obj));
                case OAFlag.AuthorityOperator:
                    return OANHibernateManager.Add(AutoMapperMapper.Empty.Map<AuthorityOperatorEntity>(obj));
                case OAFlag.BringUpContent:
                    return OANHibernateManager.Add(AutoMapperMapper.Empty.Map<BringUpContentEntity>(obj));
                case OAFlag.BringUpPerson:
                    return OANHibernateManager.Add(AutoMapperMapper.Empty.Map<BringUpPersonEntity>(obj));
                case OAFlag.Duty:
                    return OANHibernateManager.Add(AutoMapperMapper.Empty.Map<DutyEntity>(obj));
                case OAFlag.FamousRace:
                    return OANHibernateManager.Add(AutoMapperMapper.Empty.Map<FamousRaceEntity>(obj));
                case OAFlag.Module:
                    return OANHibernateManager.Add(AutoMapperMapper.Empty.Map<ModuleEntity>(obj));
                case OAFlag.Person:
                    return OANHibernateManager.Add(AutoMapperMapper.Empty.Map<PersonEntity>(obj));
                case OAFlag.ReawrdsAndPunishment:
                    return OANHibernateManager.Add(AutoMapperMapper.Empty.Map<ReawrdsAndPunishmentEntity>(obj));
                case OAFlag.ReckoningList:
                    return OANHibernateManager.Add(AutoMapperMapper.Empty.Map<ReckoningListEntity>(obj));
                case OAFlag.ReckoningName:
                    return OANHibernateManager.Add(AutoMapperMapper.Empty.Map<ReckoningNameEntity>(obj));
                case OAFlag.Reckoning:
                    return OANHibernateManager.Add(AutoMapperMapper.Empty.Map<ReckoningEntity>(obj));
                case OAFlag.Role:
                    return OANHibernateManager.Add(AutoMapperMapper.Empty.Map<RoleEntity>(obj));
                case OAFlag.TimeCard:
                    return OANHibernateManager.Add(AutoMapperMapper.Empty.Map<TimeCardEntity>(obj));
                case OAFlag.User:
                    //return OANHibernateManager.Add(AutoMapperMapper.Empty.Map<OAUserEntity>(obj));
                default:
                    throw new NotSupportedException();
            }
        }

        public static void Save(OAFlag flag,Object  obj) 
        {
            switch (flag)
            {
                case OAFlag.AccountItem:
                    OANHibernateManager.Save(AutoMapperMapper.Empty.Map<AccountItemEntity>(obj));
                    break;
                case OAFlag.AuthorityOperator:
                    OANHibernateManager.Save(AutoMapperMapper.Empty.Map<AuthorityOperatorEntity>(obj));
                    break;
                case OAFlag.BringUpContent:
                    OANHibernateManager.Save(AutoMapperMapper.Empty.Map<BringUpContentEntity>(obj));
                    break;
                case OAFlag.BringUpPerson:
                    OANHibernateManager.Save(AutoMapperMapper.Empty.Map<BringUpPersonEntity>(obj));
                    break;
                case OAFlag.Duty:
                    OANHibernateManager.Save(AutoMapperMapper.Empty.Map<DutyEntity>(obj));
                    break;
                case OAFlag.FamousRace:
                    OANHibernateManager.Save(AutoMapperMapper.Empty.Map<FamousRaceEntity>(obj));
                    break;
                case OAFlag.Module:
                    OANHibernateManager.Save(AutoMapperMapper.Empty.Map<ModuleEntity>(obj));
                    break;
                case OAFlag.Person:
                    OANHibernateManager.Save(AutoMapperMapper.Empty.Map<PersonEntity>(obj));
                    break;
                case OAFlag.ReawrdsAndPunishment:
                    OANHibernateManager.Save(AutoMapperMapper.Empty.Map<ReawrdsAndPunishmentEntity>(obj));
                    break;
                case OAFlag.ReckoningList:
                    OANHibernateManager.Save(AutoMapperMapper.Empty.Map<ReckoningListEntity>(obj));
                    break;
                case OAFlag.ReckoningName:
                    OANHibernateManager.Save(AutoMapperMapper.Empty.Map<ReckoningNameEntity>(obj));
                    break;
                case OAFlag.Reckoning:
                    OANHibernateManager.Save(AutoMapperMapper.Empty.Map<ReckoningEntity>(obj));
                    break;
                case OAFlag.Role:
                    OANHibernateManager.Save(AutoMapperMapper.Empty.Map<RoleEntity>(obj));
                    break;
                case OAFlag.TimeCard:
                    OANHibernateManager.Save(AutoMapperMapper.Empty.Map<TimeCardEntity>(obj));
                    break;
                case OAFlag.User:
                    //OANHibernateManager.Save(AutoMapperMapper.Empty.Map<OAUserEntity>(obj));
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public static void Delete(OAFlag flag,object obj) 
        {
            switch (flag)
            {
                case OAFlag.AccountItem:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<AccountItemEntity>(obj));
                    break;
                case OAFlag.AuthorityOperator:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<AuthorityOperatorEntity>(obj));
                    break;
                case OAFlag.BringUpContent:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<BringUpContentEntity>(obj));
                    break;
                case OAFlag.BringUpPerson:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<BringUpPersonEntity>(obj));
                    break;
                case OAFlag.Duty:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<DutyEntity>(obj));
                    break;
                case OAFlag.FamousRace:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<FamousRaceEntity>(obj));
                    break;
                case OAFlag.Module:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<ModuleEntity>(obj));
                    break;
                case OAFlag.Person:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<PersonEntity>(obj));
                    break;
                case OAFlag.ReawrdsAndPunishment:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<ReawrdsAndPunishmentEntity>(obj));
                    break;
                case OAFlag.ReckoningList:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<ReckoningListEntity>(obj));
                    break;
                case OAFlag.ReckoningName:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<ReckoningNameEntity>(obj));
                    break;
                case OAFlag.Reckoning:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<ReckoningEntity>(obj));
                    break;
                case OAFlag.Role:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<RoleEntity>(obj));
                    break;
                case OAFlag.TimeCard:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<TimeCardEntity>(obj));
                    break;
                case OAFlag.User:
                    //OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<OAUserEntity>(obj));
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public static void Delete(OAFlag flag, object[] objs)
        {
            switch (flag)
            {
                case OAFlag.AccountItem:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<AccountItemEntity[]>(objs));
                    break;
                case OAFlag.AuthorityOperator:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<AuthorityOperatorEntity[]>(objs));
                    break;
                case OAFlag.BringUpContent:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<BringUpContentEntity[]>(objs));
                    break;
                case OAFlag.BringUpPerson:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<BringUpPersonEntity[]>(objs));
                    break;
                case OAFlag.Duty:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<DutyEntity[]>(objs));
                    break;
                case OAFlag.FamousRace:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<FamousRaceEntity[]>(objs));
                    break;
                case OAFlag.Module:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<ModuleEntity[]>(objs));
                    break;
                case OAFlag.Person:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<PersonEntity[]>(objs));
                    break;
                case OAFlag.ReawrdsAndPunishment:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<ReawrdsAndPunishmentEntity[]>(objs));
                    break;
                case OAFlag.ReckoningList:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<ReckoningListEntity[]>(objs));
                    break;
                case OAFlag.ReckoningName:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<ReckoningNameEntity[]>(objs));
                    break;
                case OAFlag.Reckoning:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<ReckoningEntity[]>(objs));
                    break;
                case OAFlag.Role:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<RoleEntity[]>(objs));
                    break;
                case OAFlag.TimeCard:
                    OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<TimeCardEntity[]>(objs));
                    break;
                case OAFlag.User:
                   // OANHibernateManager.Delete(AutoMapperMapper.Empty.Map<OAUserEntity[]>(objs));
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        private static List<ItemViewModel> Roles;
        private static List<ItemViewModel> Contents;
        private static List<ItemViewModel> Records;
        private static List<ItemViewModel> ParentModules;
        private static List<ItemViewModel> Users;
        private static List<ItemViewModel> ReckoningNames;
        private static List<ItemViewModel> AccountItems;
        private static List<ItemViewModel> ParentRoles;
        public static List<ItemViewModel> GetRole(bool load = false)
        {
            if (load || Roles == null)
            {
                IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
                var data = unitWork.Query<RoleEntity>(null).Select(it => new ItemViewModel() { Label = it.Name, Value = it.Id }).ToList();
                Roles = data;
            }
            return Roles;
        }
        public static List<ItemViewModel> GetContent(bool load = false)
        {
            if (load || Contents == null)
            {
                IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
                var data = unitWork.Query<BringUpContentEntity>(null).Select(it => new ItemViewModel() { Label = it.Name, Value = it.Id }).ToList();
                Contents = data;
            }
            return Contents;
        }
        public static List<ItemViewModel> GetRecord(bool load = false)
        {
            if (load || Records == null)
            {
                IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
                var data = unitWork.Query<RecordEntity>(null).Select(it => new ItemViewModel() { Label = it.Name, Value = it.Id }).ToList();
                Records = data;
            }
            return Records;
        }
        public static List<ItemViewModel> GetParentModules(bool load = false)
        {
            if (load || ParentModules == null)
            {
                IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
                var data = unitWork.Query<ModuleEntity>(it=>it.Parent==null).Select(it => new ItemViewModel() { Label = it.Name, Value = it.Id }).ToList();
                ParentModules = data;
            }
            return ParentModules;
        }
        public static List<ItemViewModel> GetUsers(bool load = false)
        {
            if (load || Users == null)
            {
                IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
                //var data = unitWork.Query<OAUserEntity>(null).Select(it => new ItemViewModel() { Label = it.Account, Value = it.Id }).ToList();
               // Users = data;
            }
            return Users;
        }
        public static List<ItemViewModel> GetReckoningNames(bool load = false)
        {
            if (load || ReckoningNames == null)
            {
                IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
                var data = unitWork.Query<ReckoningNameEntity>(null).Select(it => new ItemViewModel() { Label = it.Name, Value = it.Id }).ToList();
                ReckoningNames = data;
            }
            return ReckoningNames;
        }
        public static List<ItemViewModel> GetAccountItems(bool load = false)
        {
            if (load || AccountItems == null)
            {
                IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
                var data = unitWork.Query<AccountItemEntity>(null).Select(it => new ItemViewModel() { Label = it.Name, Value = it.Id }).ToList();
                AccountItems = data;
            }
            return AccountItems;
        }
        public static List<ItemViewModel> GetParentRoles(bool load = false)
        {
            if (load || ParentRoles == null)
            {
                IUnitWork unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
                var data = unitWork.Query<RoleEntity>(it=>it.Roles==null||it.Roles.Count==0).Select(it => new ItemViewModel() { Label = it.Name, Value = it.Id }).ToList();
                ParentRoles = data;
            }
            return ParentRoles;
        }
    }
}
