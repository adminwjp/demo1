
using Product.Domain.Entities;
using Product.Domain.Repositories;
using Product.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Helpers;
using Xunit;

namespace Utility.XUnit.Product
{
    public class ProductAttributeTest : BaseTest<ProductAttribueEntity>
    {
        public ProductAttributeTest()
        {
            item = new ProductAttribueEntity() { CreationTime = CommonHelper.TotalMilliseconds(), Value = "test_insert" };
            item1 = new ProductAttribueEntity() {CreationTime = CommonHelper.TotalMilliseconds(), Value = "test_insert1" };
        }


        //[Fact]
        public override void Update()
        {
            item.Value = "test_update";
            item.LastModificationTime = CommonHelper.TotalMilliseconds();
            repository.Update(item);
            var t = CommonHelper.TotalMilliseconds();
            repository.Update(it => it.Id == item1.Id, it => new ProductAttribueEntity() { Value = "test_update1", LastModificationTime = t });
        }

   
    }
}
