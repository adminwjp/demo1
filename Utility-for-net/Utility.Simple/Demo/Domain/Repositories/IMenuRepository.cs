using Utility.Demo.Domain.Entities;
using System.Collections.Generic;
using Utility.Application.Services.Dtos;
using Utility.Domain.Entities;
using Utility.Domain.Repositories;

namespace Utility.Demo.Domain.Repositories
{
    /// <summary>菜单仓库 接口 外键实现方式不同 ef 需要实体 nhibernate 只需实体id dappper 只需列id(外键id) </summary>
    public interface IMenuRepository:IRepository<MenuEntity, long> 
    {

        List<MenuEntity> FindList();
        ResultDto<MenuEntity> FindList(int page, int size, string orderSort);

        //element-ui

        List<MenuEntity> FindCategory();
    }
}
