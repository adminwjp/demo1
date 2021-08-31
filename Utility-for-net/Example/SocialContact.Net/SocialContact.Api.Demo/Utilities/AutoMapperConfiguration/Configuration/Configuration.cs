//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using AutoMapper;
using Utility.ObjectMapping;

namespace Tunynet.Common
{
    public class AutoMapperConfiguration
    {
        /// <summary>
        /// 初始化autompper 配置
        /// </summary>
        public static void Initialize()
        {
            AutoMapperObjectMapper.Empty.Init(cfg =>
            {
                cfg.AddProfile<SourceProfile>();

            });
            //Mapper.Initialize(cfg =>
            //{
            //    cfg.AddProfile<SourceProfile>();
            //});
        }
    }
}