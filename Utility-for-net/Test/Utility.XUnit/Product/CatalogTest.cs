
using System;
using System.Collections.Generic;
using System.Text;
using Product.Domain.Entities;
using Product.Domain.Repositories;
using Product.Infrastructure.Repositories;
using Utility.Helpers;
using Xunit;

namespace Utility.XUnit.Product
{
    public class CatalogTest : BaseTest<ProductCatagoryEntity>
    {
        public CatalogTest()
        {
            item = new ProductCatagoryEntity() { CreationTime = CommonHelper.TotalMilliseconds(), Name = "test_insert", Code = "100" };
             item1 = new ProductCatagoryEntity() { CreationTime = CommonHelper.TotalMilliseconds(), Name = "test_insert1", Code = "101" };
        }

       // [Fact]
        public override void Update()
        {
            item.Name = "test_update";
            item.LastModificationTime = CommonHelper.TotalMilliseconds();
            repository.Update(item);
            var t = CommonHelper.TotalMilliseconds();
            repository.Update(it => it.Id == item1.Id, it => new ProductCatagoryEntity() { Name = "test_update1", LastModificationTime = t });
        }

    }
}
