using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverts.Dto
{
    /// <summary>
    ///添加 礼物 实体
    /// </summary>
    [AutoMap(typeof(Advert))]
    public class CreateAdvertInput : BaseAdvertInput
    {
    }
}
