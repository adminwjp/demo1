using NHibernate.Criterion;
using Spring.Stereotype;

namespace Adverts
{
    /// <summary>
    /// 广告 仓库 
    /// </summary>
    [Repository("repository")]
    public class AdvertRepository : ShopRepositoryBase<Advert>,IAdvertRepository
    {
        public AdvertRepository() 
        {

        }
        protected override string GetTable()
        {
            return Advert.TableName;
        }
        protected override NHibernate.Criterion.AbstractCriterion GetWhere(Advert entity)
        {
            AbstractCriterion where =base.GetWhere(entity);
            if (!string.IsNullOrEmpty(entity.Title))
            {
                where |= NHibernate.Criterion.Expression.Like("Title", entity.Title);
            }
            return where;
        }
    }
}
