using Product.Domain.Entities;
using Product.Domain.Repositories;
using Product.Infrastructure.Repositories;
using System;
using System.Linq;
using Xunit;

namespace Utility.XUnit.Product
{
    public abstract class BaseTest<Entity>
        where Entity:BaseEntity,new()
    {
        protected Entity item;
        protected Entity item1;
        protected IBaseRepository<Entity> repository;
        public BaseTest()
        {
            repository = new BaseEfRepository<Entity>(StartUnitTest.DbContext);
        }
        public virtual void Operator()
        {
            Add();
            Update();
            Delete();
            Find();
            DeleteList();
            FindList();
        }
        //[Fact]
        public void Add()
        {
            repository.BatchInsert(new[] { item, item1 });
        }

        //[Fact]
        public virtual void Update()
        {
        }

        //[Fact]
        public void Delete()
        {
            repository.Delete(item.Id);
        }

       // [Fact]
        public void Find()
        {
            var temp = repository.FindSingle(it => it.Id == item.Id);
            Assert.Null(temp);
            var temp1 = repository.FindSingle(it => it.Id == item1.Id);
            Assert.NotNull(temp);
        }

       // [Fact]
        public void DeleteList()
        {
            repository.DeleteList(new long[] { item.Id, item1.Id });
            //repository.Delete(it => it.Id == item.Id || it.Id == item1.Id);
        }

       // [Fact]
        public void FindList()
        {
            var datas = repository.FindByPage(new Entity(), 1, 10);
            Console.WriteLine("find list count:{0}", datas.Count);
        }
    }
}
