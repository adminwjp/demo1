using SocialContact.Domain.Entities;
using Utility.Application.Services.Dtos;

namespace SocialContact.Domain.Repositories
{
    /// <summary>
    /// 基类 仓库接口 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>: Utility.Domain.Repositories.IRepository<T, long?> where T:Entity
    {
       //ResultDto<T> FindResultByPage(T entity, int page, int size);
    }
}
