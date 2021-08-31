#if NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48

namespace Utility.Remote
{

    /// <summary>
    /// remote  基类
    /// remote 不支持 System.Threading.CancellationToken  task  nullable list
    /// </summary>
    /// <typeparam name="ResultDto"></typeparam>
    /// <typeparam name="ListDto"></typeparam>
    /// <typeparam name="Entity">remote 模型</typeparam>
    /// <typeparam name="Key"></typeparam>
    public interface IObject<ResultDto, ListDto, Entity, Key> 
		where ResultDto : Utility.Application.Services.Dtos.ResultDto<Entity>, new()
		where ListDto : System.Collections.Generic.List<Entity> , new()
		where Entity : class
    {
        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
       int Insert(Entity obj);

        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        int Update(Entity obj);

        /// <summary>根据id删除实体类信息 </summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        int Delete(Key id);

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        int DeleteList(Key[] ids);

        /// <summary>根据条件查询实体类数据集信息 不支持 会出现异常 System.Collections.Generic.List 序列化失败</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集信息 </return>
        ListDto FindList(Entity obj);

        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集数量信息</return>
       long Count(Entity obj);

        /// <summary>根据条件及分页查询实体类数据集信息  不支持 会出现异常  序列化失败</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息</return>
        ListDto FindListByPage(Entity obj, int page, int size);

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息  不支持 会出现异常 序列化失败</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        ResultDto FindResultByPage(Entity obj, int page, int size);
    }

    /// <summary>
    /// remote  基类
    /// remote 不支持 System.Threading.CancellationToken  task  nullable list
    /// 全部用 string, json 转换
    /// </summary>
    public interface IObject 
    {
        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        int Insert(string obj);

        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        int Update(string obj);

        /// <summary>根据id删除实体类信息 </summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        int Delete(string id);

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        int DeleteList(string ids);

        /// <summary>根据条件查询实体类数据集信息 不支持 会出现异常 System.Collections.Generic.List 序列化失败</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集信息 </return>
        string FindList(string obj);

        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集数量信息</return>
        long Count(string obj);

        /// <summary>根据条件及分页查询实体类数据集信息  不支持 会出现异常  序列化失败</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息</return>
        string FindListByPage(string obj, int page, int size);

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息  不支持 会出现异常 序列化失败</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        string FindResultByPage(string obj, int page, int size);
    }

}
#endif

