
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
    public class ProductTest : BaseTest<ProductEntity>
    {
        public ProductTest()
        {
            item = new ProductEntity() {  CreationTime = CommonHelper.TotalMilliseconds(), Name = "test_insert", CreateAccount = "100" };
            item1 = new ProductEntity() {  CreationTime = CommonHelper.TotalMilliseconds(), Name = "test_insert1", CreateAccount = "101" };
        }


        //[Fact]
        public override void Update()
        {
            item.Name = "test_update";
            item.LastModificationTime = CommonHelper.TotalMilliseconds();
            repository.Update(item);
            var t = CommonHelper.TotalMilliseconds();
            repository.Update(it => it.Id == item1.Id, it => new ProductEntity() { Name = "test_update1", LastModificationTime =t });
        }

       
    }
}
