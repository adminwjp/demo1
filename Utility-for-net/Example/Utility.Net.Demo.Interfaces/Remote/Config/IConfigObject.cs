#if NET10 || NET11 || NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
// 模型 注解 >=40
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471|| NET472  || NET48
using Config.Remote.Entities;
using System;
using System.Collections.Generic;
using Utility.Application.Services.Dtos;
using Utility.Domain.Entities;
using Utility.Remote;

namespace Config.Remote
{
    /// <summary>
    /// 配置 契约接口
    /// </summary>
    public interface IConfigObject : IObject<ConfigObjectResultEntity, ConfigObjectListEntity, ConfigObjectEntity, string>
    {
    }

    //public class ConfigObjectEntity : IEntity<string>
    //{
    //    public string Id { get; set; }
    //}

    //public class ConfigObjectResultEntity: ResultDto<ConfigObjectEntity>
    //{
    //}

    //public class ConfigObjectListEntity:List<ConfigObjectEntity>
    //{
    //}
}
#endif
#endif