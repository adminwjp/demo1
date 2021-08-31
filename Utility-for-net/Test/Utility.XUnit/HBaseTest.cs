//using Microsoft.HBase.Client;
//using Microsoft.HBase.Client.LoadBalancing;
//using NUnit.Framework;
//using org.apache.hadoop.hbase.rest.protobuf.generated;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Utility.XUnit
//{
//    public class HBaseTest
//    {
//        //[Test]
//        public void TestConnection()
//        {
//            //完全 使用 不了 地址 访问 不了  具体 啥 原因 未知
//            //https://github.com/hdinsight/hbase-sdk-for-net
//            string clusterURL = "http://127.0.0.1:16030/";//"https://<yourHBaseClusterName>.azurehdinsight.net";
//            string hadoopUsername = "";
//            string hadoopUserPassword = "";

//            string hbaseTableName = "sampleHbaseTable";
//            // Create a new instance of an HBase client.
//            //ClusterCredentials creds = new ClusterCredentials(new Uri(clusterURL), hadoopUsername, hadoopUserPassword);
//            // HBaseClient hbaseClient = new HBaseClient(creds);
//            RequestOptions requestOptions = RequestOptions.GetDefaultOptions();
//            requestOptions.Port = 16030;
//            HBaseClient hbaseClient = //new HBaseClient(1);
//                 new HBaseClient(null, requestOptions, new LoadBalancerRoundRobin(new List<string>() { "127.0.0.1" }) {});
//            // Retrieve the cluster version
//            var version = hbaseClient.GetVersionAsync().Result;
//            Console.WriteLine("The HBase cluster version is " + version);
//            // Create a new HBase table.
//            TableSchema testTableSchema = new TableSchema();
//            testTableSchema.name = hbaseTableName;
//            testTableSchema.columns.Add(new ColumnSchema() { name = "d" });
//            testTableSchema.columns.Add(new ColumnSchema() { name = "f" });
//            hbaseClient.CreateTableAsync(testTableSchema);

//            // Insert data into the HBase table.
//            string testKey = "content";
//            string testValue = "the force is strong in this column";
//            CellSet cellSet = new CellSet();
//            CellSet.Row cellSetRow = new CellSet.Row { key = Encoding.UTF8.GetBytes(testKey) };
//            cellSet.rows.Add(cellSetRow);

//            Cell value = new Cell { column = Encoding.UTF8.GetBytes("d:starwars"), data = Encoding.UTF8.GetBytes(testValue) };
//            cellSetRow.values.Add(value);
//            hbaseClient.StoreCellsAsync(hbaseTableName, cellSet);

//            // Retrieve a cell by its key.
//            cellSet = hbaseClient.GetCellsAsync(hbaseTableName, testKey).Result;
//            Console.WriteLine("The data with the key '" + testKey + "' is: " + Encoding.UTF8.GetString(cellSet.rows[0].values[0].data));
//            // with the previous insert, it should yield: "the force is strong in this column"

//            //Scan over rows in a table. Assume the table has integer keys and you want data between keys 25 and 35. 
//            Scanner scanSettings = new Scanner()
//            {
//                batch = 10,
//                startRow = BitConverter.GetBytes(25),
//                endRow = BitConverter.GetBytes(35)
//            };
//            ScannerInformation scannerInfo = hbaseClient.CreateScannerAsync(hbaseTableName, scanSettings,requestOptions).Result;
//            CellSet next = null;
//            Console.WriteLine("Scan results");

//            while ((next = hbaseClient.ScannerGetNextAsync(scannerInfo, requestOptions).Result) != null)
//            {
//                foreach (CellSet.Row row in next.rows)
//                {
//                    Console.WriteLine(row.key + " : " + Encoding.UTF8.GetString(row.values[0].data));
//                }
//            }

//            Console.WriteLine("Press ENTER to continue ...");
//            Console.ReadLine();
//        }
//    }
//}
