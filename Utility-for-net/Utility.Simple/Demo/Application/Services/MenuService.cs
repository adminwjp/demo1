using Utility.Application.Services;
using Utility.Domain.Entities;
using Utility.Mappers;
using System;
using System.Collections.Generic;
using Utility.Application.Services.Dtos;

namespace Utility.Demo.Application.Services
{
    using Utility.Demo.Application.Services.Dtos;
    using Utility.Demo.Domain.Entities;
    using Utility.Demo.Domain.Repositories;
    using Utility.Json;
#if NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
    // using Autofac.Annotation;
    using Utility.Helpers;
    using Utility.Attributes;

    //[Component(typeof(MenuService<,,,>), AutofacScope = AutofacScope.InstancePerLifetimeScope)]
#endif
    [Transtation]
    public class MenuService : 
        CrudAppService<MenuEntity, long>
    {
        protected new IMenuRepository Repository;
        public MenuService():base(null)
        {

        }
        //#if NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
        //[Autowired]
        //protected IMenuRepository repository;
        // [Autowired]
        // public  new IObjectMapper ObjectMapper { get; set; }
        //#else
        public MenuService(IMenuRepository repository, IMapper objectMapper):base(repository)
        {
            this.Repository = repository;
            
            this.Mapper = objectMapper;
        }
//#endif
     
        public virtual List<Dto> FindList<Dto>()
        {
            var data = Repository.FindList();
            var result = Mapper.Map<List<Dto>>(data);
            return result;
        }

        public virtual ResultDto<AllDto> FindList<AllDto>(int page, int size,string orderSort)
  
        {
            var data = Repository.FindList(page, size, orderSort);
            var result = Mapper.Map<ResultDto<AllDto>>(data);
            return result;
        }

        public virtual List<EasyUIMenuDto> FindList()
        {
            var data = Repository.FindList();
            var result = Mapper.Map<List<EasyUIMenuDto>>(data);
            if (result != null && result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    //result[i].Attributes = JsonHelper.ToObject<Dictionary<string, object>>(data[i].AttributesJson);
                    CursionAttr(result[i], data[i]);
                }
            }
            //return base.FindList(page, size);
            return result;
        }

        private void CursionAttr(EasyUIMenuDto easyUIMenuDto, MenuEntity easyUIMenuEntity)
        {
            if (!string.IsNullOrEmpty(easyUIMenuEntity.AttributesJson))
            {
                easyUIMenuDto.Attributes = JsonHelper.ToObject<Dictionary<string, object>>(easyUIMenuEntity.AttributesJson);
            }
            if (easyUIMenuEntity.Children != null && easyUIMenuEntity.Children.Count > 0)
            {
                var entities = new MenuEntity[easyUIMenuEntity.Children.Count];
                var dtos = new EasyUIMenuDto[easyUIMenuEntity.Children.Count];
                easyUIMenuEntity.Children.CopyTo(entities, 0);
                easyUIMenuDto.Children.CopyTo(dtos, 0);
                for (int i = 0; i < entities.Length; i++)
                {
                    CursionAttr(dtos[i], entities[i]);
                }
            }
        }



        public virtual ResultDto<EasyUIMenuDto> FindList(int page, int size, string orderSort)
        {
            var data = Repository.FindList(page, size, orderSort);
            var result = Mapper.Map<ResultDto<EasyUIMenuDto>>(data);
            if (result != null && result.Data != null)
            {
                for (int i = 0; i < result.Data.Count; i++)
                {
                    // result.Data[i].Attributes = JsonHelper.ToObject<Dictionary<string, object>>(data.Data[i].AttributesJson);
                    CursionAttr(result.Data[i], data.Data[i]);
                }
            }
            // return base.FindList(page, size, orderSort);
            return result;
        }

        public virtual List<ElementMenuCategoryDto> FindCategoryByEle()
        {
            var data = Repository.FindCategory();
            //整理混乱的数据
            data = CascadeHelper.DataParseIfWhileReference<MenuEntity, long>(data, true);
            var res = Mapper.Map<List<ElementMenuCategoryDto>>(data);
            return res;
        }

        private MenuEntity CursionData(List<MenuEntity> temps, long parentId)
        {
            foreach (var item in temps)
            {
                if (item.Id == parentId)
                {
                    return item;
                }
                MenuEntity temp = null;
                if (item.Children != null)
                {
                    temp = CursionData((List<MenuEntity>)item.Children, parentId);
                    if (temp != null)
                    {
                        return temp;
                    }
                }
            }
            return null;
        }
    }

#if NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
    // using Autofac.Annotation;
    // [Component(typeof(MenuResponse<,,,,>), AutofacScope = AutofacScope.InstancePerLifetimeScope)]
#endif
    public class MenuResponse : ResponseApiService<MenuService, MenuEntity, long>
    {
        //#if NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
        // [Autowired]
        //protected Service Service;
        //#else

        public MenuResponse(MenuService service) : base(service)
        {

        }
        //#endif



        public ResponseApi<List<Dto>> FindList<Dto>(Language language = Language.Chinese)
        {
            return ResponseApi<List<Dto>>.Create(language, Code.QuerySuccess).SetData(this.Service.FindList<Dto>());
        }
        public ResponseApi<ResultDto<AllDto>> FindList<AllDto>(int page, int size, string orderSort, Language language = Language.Chinese)
        {
            return ResponseApi<ResultDto<AllDto>>.Create(language, Code.QuerySuccess).SetData(this.Service.FindList<AllDto>(page, size, orderSort));
        }
        public ResponseApi<List<EasyUIMenuDto>> FindList(Language language = Language.Chinese)
        {
            return ResponseApi<List<EasyUIMenuDto>>.Create(language, Code.QuerySuccess).SetData(this.Service.FindList());
        }
        public ResponseApi<ResultDto<EasyUIMenuDto>> FindList(int page, int size, string orderSort, Language language = Language.Chinese)
        {
            return ResponseApi<ResultDto<EasyUIMenuDto>>.Create(language, Code.QuerySuccess).SetData(this.Service.FindList(page, size, orderSort));
        }
        public virtual ResponseApi<List<ElementMenuCategoryDto>> FindCategoryByEle(Language language = Language.Chinese)
        {
            return ResponseApi<List<ElementMenuCategoryDto>>.Create(language, Code.QuerySuccess).SetData(Service.FindCategoryByEle());
        }
    }
}
