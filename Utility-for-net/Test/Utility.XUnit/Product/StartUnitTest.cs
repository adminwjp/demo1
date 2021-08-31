
using System.Data;
using Dapper;
using Utility.IO;
using Utility.Json;
using System.Collections.Generic;
using System;
using Product.Infrastructure;
using Xunit;
using Product.Domain.Entities;
using Utility.Helpers;

namespace Utility.XUnit.Product
{
    public class StartUnitTest
    {
        static DesignTimeDbContextFactory designTimeDbContextFactory = new DesignTimeDbContextFactory();
        public static Ef.DbContextProvider<ProductDbContext> DbContext;
        static StartUnitTest()
        {
             DbContext =new Ef.DbContextProvider<ProductDbContext>(designTimeDbContextFactory.CreateDbContext(null)) ;
        }
        //[Fact]
        public void Setup()
        {
            LoadData();
        }

        private void LoadData()
        {
           
            string originConnectionString = "Database=jeeshop;Data Source=localhost;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8;";

            IDbConnection connection = null;// new MySqlConnection(originConnectionString);
            //{"id":20,"name":"图书","pid":0,"order1":1,"type":"p","code":"tushu","showInNav":"y"}

            var catalogs = connection.Query("select * from t_catalog");
            List<ProductCatagoryEntity> catalogEntities = new List<ProductCatagoryEntity>();
            Dictionary<int,int> catalogIds = new Dictionary<int, int>();
            foreach (var item in catalogs)
            {
                ProductCatagoryEntity catalogEntity = new ProductCatagoryEntity() {  CreationTime = CommonHelper.TotalMilliseconds() };
                int id = item.id;
                catalogIds.Add(id,0);
                if(item.pid is DBNull)
                {

                }
                else
                {
                    int pid = item.pid;
                    catalogIds[id] = pid;
                }
                catalogEntities.Add(catalogEntity);
                catalogEntity.Name = item.name;
                catalogEntity.Orders = item.order1??0;
               string type = item.type;
                catalogEntity.Code = item.code;
                catalogEntity.ParentId = 0;
                string nav= item.showInNav;
                if (type.Equals("p"))
                {
                    catalogEntity.Flag = nav.Equals("y")? ProductCatagoryFlag.Nav: ProductCatagoryFlag.ChildrenNav;
                }
                else
                {
                    catalogEntity.Flag = ProductCatagoryFlag.BottomNav;
                }
            }
            var ids = catalogIds.Keys.AsList();
            var pids = catalogIds.Values.AsList();
            for (int i = 0; i < catalogEntities.Count; i++)
            {
                if (catalogIds[ids[i]] != 0)
                {
                    int index = ids.IndexOf(pids[i]);
                    catalogEntities[i].ParentId = catalogEntities[index].Id;
                }

            }
            DbContext.DbContext.AddRange(catalogEntities);
            DbContext.DbContext.SaveChanges();

            //       {
            //           "id": 1,
            // "name": "味道",
            // "catalogID": 57,
            // "pid": 0,
            // "order1": null
            //}


            var attributes = connection.Query("select * from t_attribute");
            List<ProductCatagoryAttribueEntity> catalogAttribueEntities = new List<ProductCatagoryAttribueEntity>();
            Dictionary<int, Tuple<int,int>> catalogAttribueIds = new Dictionary<int, Tuple<int, int>>();
            foreach (var item in attributes)
            {
                ProductCatagoryAttribueEntity catalogAttribueEntity = new ProductCatagoryAttribueEntity() {  CreationTime =CommonHelper.TotalMilliseconds() };
                int id = item.id;
                int catalogID = item.catalogID??0;
                int pid= item.pid??0;
                catalogAttribueIds.Add(id, new Tuple<int, int>(catalogID,pid));
                catalogAttribueEntities.Add(catalogAttribueEntity);
                catalogAttribueEntity.Name = item.name;
                catalogAttribueEntity.Orders = item.order1??0;
            }

            //        {
            //            "id": 51,
            //  "attrID": 159,
            //  "productID": 10010,
            //  "value": null
            //}
            var attributeLinks = connection.Query("select * from t_attribute_link");
    //        {
    //            "id": 10001,
    //  "name": "一座城池",
    //  "introduce": "韩寒，一座城池",
    //  "price": 123.00,
    //  "nowPrice": 111.00,
    //  "picture": "/attached/image/20140304/1393902455326_1.jpg",
    //  "createtime": "2013-10-25T00:33:37",
    //  "createAccount": null,
    //  "updateAccount": null,
    //  "updatetime": "2014-03-20T17:54:24",
    //  "isnew": "y",
    //  "sale": "y",
    //  "hit": 30,
    //  "status": 2,
    //  "productHTML": "<img src=\"http://myshopxx.oss.aliyuncs.com/attached/image/20140304/1393902438216_3.jpg\" alt=\"\" /><img src=\"http://myshopxx.oss.aliyuncs.com/attached/image/20140304/1393902455326_3.jpg\" alt=\"\" /><img src=\"http://myshopxx.oss.aliyuncs.com/attached/image/20140304/1393902463373_3.jpg\" alt=\"\" /><img src=\"http://myshopxx.oss.aliyuncs.com/attached/image/20140304/1393902469170_3.jpg\" alt=\"\" />",
    //  "maxPicture": "/jeeshop/attached/image/20130928/20130928233856_374.jpg",
    //  "images": null,
    //  "catalogID": "60",
    //  "sellcount": 13,
    //  "stock": 22,
    //  "searchKey": null,
    //  "title": null,
    //  "description": null,
    //  "keywords": null,
    //  "activityID": null,
    //  "unit": "item",
    //  "score": 0,
    //  "isTimePromotion": "n",
    //  "giftID": null
    //}
            var products = connection.Query("select * from t_product");
    //        {
    //            "id": 1,
    //  "productID": "10316",
    //  "specColor": "红色",
    //  "specSize": "红色2.1",
    //  "specStock": "123",
    //  "specPrice": 122.00,
    //  "specStatus": "n"
    //}
            var specs = connection.Query("select * from t_spec");
           // FileHelper.WriteFile(@"E:\work\csharp\src\Shop\Shop.Product\Shop.Product.NUnitTes\origin.json", JsonHelper.ToJson(new { catalogs, attributes, attributeLinks, products, specs }));
            string connectionString = "Database=Shop;Data Source=localhost;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8;";
        }

      // [Fact]
        public void Test1()
        {
        }
    }
}