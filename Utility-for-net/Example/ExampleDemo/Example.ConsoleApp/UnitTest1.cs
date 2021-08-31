//using Elasticsearch.Net;
//using System;
//using Utility;
//using Utility.Es;
//using Utility.Json.Extensions;

////using Xunit;
////using Xunit.Abstractions;

//namespace XUnitTest
//{
//    public class UnitTest1
//    {
//        private readonly ITestOutputHelper _testOutputHelper;

//        public UnitTest1(ITestOutputHelper testOutputHelper)
//        {
//            _testOutputHelper = testOutputHelper ?? new TestOutputHelper();
//        }

//        // [Fact]
//        public void Test1()
//        {

//        }
//        //[Fact]
//        public void TestEs()
//        {
//            ElasticsearchHelper elasticsearchDatabase = new ElasticsearchHelper();
//            try
//            {
//                var res = elasticsearchDatabase.CreateDatabase("a");
//                _testOutputHelper.WriteLine("create index {0}", res);

//                res = elasticsearchDatabase.InsertByTable<dynamic>("crawl", "aa", new { id = "2" });
//                _testOutputHelper.WriteLine("add data {0}", res);

//                elasticsearchDatabase.Delete("crawl", "aa");
//                _testOutputHelper.WriteLine("delete index id data {0}", res);

//                elasticsearchDatabase.InsertByTable<dynamic>("crawl", "bb", new { id = "44" });
//                _testOutputHelper.WriteLine("add data {0}", res);

//                elasticsearchDatabase.Delete("crawl", "_doc", "bb");
//                _testOutputHelper.WriteLine("delete index type id {0}", res);

//                res = elasticsearchDatabase.Delete("crawl", "_doc");
//                _testOutputHelper.WriteLine("delete index type all data {0}", res);

//                res = elasticsearchDatabase.DeleteDatabase("a");
//                _testOutputHelper.WriteLine("delete index  {0}", res);

//                res = elasticsearchDatabase.Insert(new { id = "2" });
//                _testOutputHelper.WriteLine("add data {0}", res);

//                res = elasticsearchDatabase.CreateDatabase("a1");

//                res = elasticsearchDatabase.InsertByTable("a1", "cc", new { id = "2" });
//                _testOutputHelper.WriteLine("add data {0}", res);

//                res = elasticsearchDatabase.InsertByTable("a1", "_doc", new { id = "2444", aa = "444" });
//                _testOutputHelper.WriteLine("add data {0}", res);

//                res = elasticsearchDatabase.InsertByTable("a1", "11aa", new { id = "2" });
//                _testOutputHelper.WriteLine("add data {0}", res);//fail

//                res = elasticsearchDatabase.InsertByTable("a1", "_doc", "cc", new { id = "2" });
//                _testOutputHelper.WriteLine("add data {0}", res);

//                res = elasticsearchDatabase.InsertByTable("a1", "11aa", "cc", new { id = "2" });
//                _testOutputHelper.WriteLine("add data {0}", res);//fail

//                //var obj = elasticsearchDatabase.Find("a1","_doc", new { _id = "cc" });
//                //_testOutputHelper.WriteLine("get data {0}", obj);//Exception

//                //var obj1 = elasticsearchDatabase.Find("", "_doc","2444");//Exception
//                //_testOutputHelper.WriteLine("get data {0}", obj1);

//                //var obj1 = elasticsearchDatabase.FindList<dynamic>("","",new { id = "2444", aa = "444" });
//                //_testOutputHelper.WriteLine("get data {0}", obj1.ToJson());//Exception

//                //var obj1 = elasticsearchDatabase.FindList<dynamic>("","",new { id = "2" });
//                //_testOutputHelper.WriteLine("get data {0}", obj1.ToJson());//存在 多个  则 异常

//                var c = elasticsearchDatabase.Count("a1", PostData.String("{}"));
//                _testOutputHelper.WriteLine("Count data {0}", c);
//            }

//            catch (Exception ex)
//            {
//                _testOutputHelper.WriteLine("Exception {0}", ex);
//            }
//        }
//    }
//}
