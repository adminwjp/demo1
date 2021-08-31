namespace Config.Domain.Entities
{
    /// <summary>
    /// 服务状态
    /// </summary>
    public enum ServiceStatus
    {   
        /// <summary>
        /// 上线
        /// </summary>
        OnLine = 0x1,

        /// <summary>
        ///删除
        /// </summary>
        Delete = 0x0,

     

        /// <summary>
        /// 下线
        /// </summary>
        OffLine=-0x1,

        /// <summary>
        /// 未知
        /// </summary>
        Unkow = 0x2
    }
}
