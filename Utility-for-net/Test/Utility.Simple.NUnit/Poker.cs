using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Test
{
    public class Poker
    {
        /// <summary>
        /// 玩家1
        /// </summary>
        public string PlayernOne { get; set; }

        /// <summary>
        /// 玩家2
        /// </summary>
        public string PlayerTwo { get; set; }

        /// <summary>
        /// 玩家1 所在行
        /// </summary>
        public int PonitOne { get; set; }

        /// <summary>
        /// 玩家2 所在行
        /// </summary>
        public int PonitTwo { get; set; }

        /// <summary>
        /// 15根牙签 
        /// </summary>
        public readonly List<int> Pokers = new List<int>(15);

        public readonly List<int> RowOne = new List<int>(3);


        public readonly List<int> RowTwo = new List<int>(5);



        public readonly List<int> RowThreee = new List<int>(7);

        public static void Start(PokerFlag flag)
        {
            Console.WriteLine(@"将15根牙签
分成三行
每行自上而下（其实方向不限）分别是3、5、7根
 
安排两个玩家，每人可以在一轮内，在任意行拿任意根牙签，但不能跨行
 
拿最后一根牙签的人即为输家
");
            Poker poker = new Poker();
            Console.WriteLine("初始化15根牙签 0-14 代表编号 0 第一根牙签 14 代表最后一根牙签,牙签如下:");
            //正常放 情况下 
            for (int i = 0; i < poker.Pokers.Capacity; i++)
            {
                poker.Pokers[i] = i;
                Console.Write($"{i}号牙签 \t");
                if (i < 3)
                {
                    poker.RowOne.Add(i);
                }
                else if(i<8)
                {
                    poker.RowTwo.Add(i);
                } else
                {
                    poker.RowThreee.Add(i);
                }
            }
            Console.WriteLine();

            //显示用的 实际不会告诉给玩家哪一根是最后一根
            Console.WriteLine("第1行牙签如下：");
            for (int i = 0; i < poker.RowOne.Count; i++)
            {
                Console.Write($"{i}号牙签 \t");
            }
            Console.WriteLine();

            Console.WriteLine("第2行牙签如下：");
            for (int i = 0; i < poker.RowTwo.Count; i++)
            {
                Console.Write($"{i}号牙签 \t");
            }
            Console.WriteLine();

            Console.WriteLine("第3行牙签如下：");
            for (int i = 0; i < poker.RowThreee.Count; i++)
            {
                Console.Write($"{i}号牙签 \t");
            }
            Console.WriteLine();

            //基于需求 信息

            //基于外卖 场景 正常商业场景下
            //假设规则1： 新用户 送礼 抽奖活动 
            //假设规则2: 充钱 无数次 玩 ,不公平 一般 来说 有限制次数
            //假设规则3： 多点  积累  积分 ？ 
            //... 你这是 什么题 让 别人 做项目 展示？


            //基于 题目理解
            //随机放情况下
            //单机 手动情况下 别告诉要用 其他技术
            //没固定答案
            //游戏只会玩一次 不会玩多次？ 短期玩？
            //哪一根是最后一根(拿完最后一根算最后一根? 3行 只有2人拿只能拿2行 总有一行没拿 永远拿不了咋办) 
            //一般正常场景下会打标签的   2个玩家可能拿的2 行永远不存在最后一根 也可能存在
            //顺序放 1 2 行 拿  第3行 永远拿不了 游戏无意义 人为设置障碍

            //玩家随便拿 ？ 没意义
            //通关 奖励  可多拿？ 是否允许
            //一次拿完？ 先拿的 输 率大 无意义
            //可以 作弊 ？
            Random random = new Random();//任意行 任意根 随机数

            while (true)
            {
                Console.WriteLine("规则");
                
                Console.WriteLine("玩家1拿取");
                string str=Console.ReadLine();
                switch (str)
                {
                    case "0":
                        Console.WriteLine();
                        break;
                    default:
                        break;
                }
            }
        }

        public static void Row(int row,List<int> points)
        {
            Console.WriteLine($"第{row}行牙签标签为: ");
            for (int i = 0; i < points.Count; i++)
            {
                Console.Write($"{points[i]}号牙签 \t");
            }
            Console.WriteLine();
        }


        /// <summary>
        /// 后台(游戏者)通知规则  还是玩家自定义规则
        /// </summary>
        public static void Notity()
        {

        }
    }

    /// <summary>
    /// 拿牙签规则
    /// </summary>
    public enum PokerFlag
    {
        /// <summary>
        /// 拿牙签 不准放回(放回无意义)
        /// </summary>
        None = 0x0
    }
}
