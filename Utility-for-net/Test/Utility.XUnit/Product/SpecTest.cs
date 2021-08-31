
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
    public class SpecTest : BaseTest<SpecEntity>
    {
        public SpecTest()
        {
            item = new SpecEntity() {  CreationTime = CommonHelper.TotalMilliseconds(), Size = "test_insert", Color = "red" };
            item1 = new SpecEntity() {  CreationTime = CommonHelper.TotalMilliseconds(), Size = "test_insert1", Color = "green" };
        }

 

       // [Fact]
        public override void Update()
        {
            item.Size = "test_update";
            item.LastModificationTime = CommonHelper.TotalMilliseconds();
            repository.Update(item);
            var t = CommonHelper.TotalMilliseconds();
            repository.Update(it => it.Id == item1.Id, it => new SpecEntity() { Size = "test_update1", LastModificationTime =t  });
        }

       
    }
}
