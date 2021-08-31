
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Helpers;
using Xunit;

namespace Utility.XUnit.Product
{
    public class CatalogAttributeTest: BaseTest<ProductCatagoryAttribueEntity>
    {
        public CatalogAttributeTest()
        {
            item = new ProductCatagoryAttribueEntity() { CreationTime = CommonHelper.TotalMilliseconds(), Name = "test_insert", Orders = 100 };
            item1 = new ProductCatagoryAttribueEntity() {  CreationTime = CommonHelper.TotalMilliseconds(), Name = "test_insert1", Orders = 101 };
        }



       // [Fact]
        public override void Update()
        {
            item.Name = "test_update";
            item.LastModificationTime = CommonHelper.TotalMilliseconds();
            repository.Update(item);
            var t = CommonHelper.TotalMilliseconds();
            repository.Update(it => it.Id == item1.Id, it => new ProductCatagoryAttribueEntity() { Name = "test_update1", LastModificationTime = t});
        }
    }
}
