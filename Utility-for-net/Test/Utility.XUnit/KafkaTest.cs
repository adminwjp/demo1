#if  true//NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
//using Confluent.Kafka;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Utility.MessageQueue;

namespace Utility.XUnit
{


    /// <summary>kafka帮助类 </summary>
    public class KafkaTest
    {
        #region kafka core
        //   [Fact]
        public void Test()
        {
            CancellationTokenSource cancellationToken = new CancellationTokenSource();//默认 死循环 阻塞 
            CancellationTokenSource cancellationToken1 = new CancellationTokenSource();//默认 死循环 阻塞 
            Task.Factory.StartNew(() =>
            {
                //只能 开 异步取消 (同步过不去) 
                Thread.Sleep(20000);
                cancellationToken.Cancel();//消费者处理完成  自动 提交偏移量 (取消的是这个 BlockingCollection)
                cancellationToken1.Cancel();
            }
          );


            ConfluentKafkaHelper confluentKafkaHelper = new ConfluentKafkaHelper(null);
            //如何 确定 消费者 处理完 数据 ,不然一直 死循环 (默认没有提交偏移量 查询又开始, 消费者处理完成  自动 提交偏移量)
            //Task.Factory.StartNew(() =>
            //{
            //    for (int i = 0; i < 30; i++)
            //    {
            //        confluentKafkaHelper.Pull("MyTopic", it =>
            //        {
            //            Debug.WriteLine("1" + it.Message.Value + it.Offset.Value);
            //        });
            //    }
            //});

            //死线程
            try
            {
                confluentKafkaHelper.Pull("MyTopic", it =>
                {
                    Debug.WriteLine("2" + it.Message.Value + it.Offset.Value);
                }, cancellationToken.Token);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

            }
#if KafkaNet
               //怎么解决已处理过的数据了, 数据一直存在
               KafkaNetHelper kafkaNetHelper = new KafkaNetHelper();
            //不能用同一个  CancellationTokenSource
      
           // Task.Factory.StartNew(() =>
            {
                //进程 提前 退出 数据没保存 则 不会 保存 数据
                //for (int i = 0; i < 10; i++)
                //{
                //    kafkaNetHelper.Push("MyTopic", "你好");
                //    // kafkaNetHelper.Push("MyTopic1", "你好");
                //}
                //Thread.Sleep(10 * 500);//等待数据插入完成 不然 没 数据 pull 没有 意义
            }
            // );
            //Thread.Sleep( 5000);

            //多个消费者处理 重新消费 数据还会处理

            //如何 确定 消费者 处理完 数据 ,不然一直 死循环 (默认没有提交偏移量 查询又开始, 消费者处理完成  自动 提交偏移量)
            //Task.Factory.StartNew(() => {
            //    kafkaNetHelper.Pull("MyTopic", it => {
            //        Debug.WriteLine("1" + Encoding.UTF8.GetString(it.Value));
            //    }, cancellationToken);
            //});

           

            kafkaNetHelper.Pull("MyTopic", it => {
                Debug.WriteLine("2"+Encoding.UTF8.GetString(it.Value)+it.Meta.Offset);
            }, cancellationToken);
#endif
        }



        #endregion   kafka core

    }
}
#endif