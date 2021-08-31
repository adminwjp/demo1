//using Microsoft.Data.SqlClient;
//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Diagnostics;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Utility;
//using Utility.Ef;
//using Utility.Net.Sockets;
//using Utility.Utils;

//namespace Example
//{
//    public class UserCache
//    {
//        protected IDictionary<long, long> FailCountByUserId = new ConcurrentDictionary<long, long>();
//        protected IDictionary<string, long> FailCountByAccount = new ConcurrentDictionary<string, long>();
        
//        protected IDictionary<string, long> Ids= new ConcurrentDictionary<string, long>();
//        public UserCache()
//        {
//            Ids.Add("Users", 1);
//            Ids.Add("UserLogs", 1);
//            Ids.Add("Types", 1);
//            Ids.Add("PivotTables", 1); 
//            Ids.Add("Tests", 1);
//        }

//        public virtual long GetId(string table)
//        {
//            long id = Ids[table];
//            Interlocked.Increment(ref id);
//            Ids[table] = id;
//            return id - 1;
//        }

//        public virtual bool Exists(string account)
//        {
//            return FailCountByAccount.ContainsKey(account);
//        }

//        public virtual bool Register(string account)
//        {
//            FailCountByAccount.Add(account, 1);
//            return true;
//        }

//        public virtual bool Exists(long userId)
//        {
//            return FailCountByUserId.ContainsKey(userId);
//        }

//        public virtual long GetFailCount(long userId)
//        {
//            return FailCountByUserId[userId];
//        }

//        public virtual long FailCountIncrement(string account)
//        {
//            long count = FailCountByAccount[account];
//            Interlocked.Increment(ref count);
//            FailCountByAccount[account] = count;
//            return count;
//        }

//        public virtual long GetFailCount(string account)
//        {
//            long count = FailCountByAccount[account];
//            return count;
//        }

//        public virtual long FailCountIncrement(long userId)
//        {
//            long count = FailCountByUserId[userId];
//            Interlocked.Increment(ref count);
//            FailCountByUserId[userId] = count;
//            return count;
//        }


//        public virtual long FailCountInit(long userId)
//        {
//            FailCountByUserId[userId] = 0;
//            return 0;
//        }
//    }
//    public class UserRepository
//    {
//        protected DbConnection Write;
//        protected DbConnection Read;
//        private bool single = true;
//        protected UserCache UserCache;
//        protected string Table = "Users";
//        protected long Fail = 3;
//        public UserRepository(DbConnection connection, UserCache userCache):this(connection,connection,userCache)
//        {
//            single = true;
//        }
//        public UserRepository(DbConnection write, DbConnection read, UserCache userCache)
//        {
//            Write = write;
//            Read = read;
//            UserCache = userCache;
//            single = false;
//        }

//        //防止 并发 查询 修改状态
//        //要么 都 成功 要么 都 失败
//        public virtual bool Login(string account,string pwd, long loginIp)
//        {
//            if (!UserCache.Exists(account))
//            {
//                Console.WriteLine("{0} 账号 不存在",account);
//                return false;
//            }
//            long fail = UserCache.GetFailCount(account);
//            if (fail > Fail)
//            {
//                Console.WriteLine("{0} 账号 登录错误次数过多,已锁定 ", account);
//                return true;
//            }
//            var tx = Write.BeginTransaction();
//            var command= Write.CreateCommand();
//            command.CommandText = "select Id,Account,Pwd,RegIp,RegDate,ModifyDate,LoginDate,LoginFailCount,LoginIp,TimeSpan,Status,Token from users where account=@Account and pwd=@Pwd ;";
           
//            var param = command.CreateParameter();
//            command.Parameters.Add(param);
//            param.ParameterName = "@Account";
//            param.Value = account;

//            param = command.CreateParameter();
//            command.Parameters.Add(param);
//            param.ParameterName = "@Pwd";
//            param.Value = pwd;
//            var reader = command.ExecuteReader();
//            bool login = false;
//            UserInfo user = new UserInfo();
//            try
//            {
//                if (reader.Read())
//                {
//                    user.Id = reader.GetInt64(0);
//                    user.Account = reader.GetString(1);
//                    user.Pwd = reader.GetString(2);
//                    user.RegIp = reader.GetInt64(3);
//                    user.RegDate = reader.GetInt64(4);
//                    user.ModifyDate = reader.GetInt64(5);
//                    user.LoginDate = reader.GetInt64(6);
//                    user.LoginFailCount = reader.GetInt64(7);
//                    user.LoginIp = reader.GetInt64(8);
//                    user.TimeSpan = reader.GetInt64(9);
//                    user.Status = reader.GetInt32(10);
//                    user.Token = reader.GetString(11);
//                    login = true;
//                }
//            }
//            finally
//            {
//                reader.Close();
//                command.Dispose();
//            }
//            //if (login)
//            {
//                command.Parameters.Clear();
//                command.CommandText = "update user set TimeSpan=@TimeSpan,LoginDate=@LoginDate,LoginIp=@LoginIp,Status=@Status,Token=@Token，LoginFailCount=@LoginFailCount where Account=@Account;";

//                param = command.CreateParameter();
//                command.Parameters.Add(param);
//                param.ParameterName = "@Account";
//                param.Value = account;

//                param = command.CreateParameter();
//                command.Parameters.Add(param);
//                param.ParameterName = "@TimeSpan";
//                param.Value = DateUtils.TotalMilliseconds();

//                param = command.CreateParameter();
//                command.Parameters.Add(param);
//                param.ParameterName = "@LoginDate";
//                param.Value = DateUtils.TotalMilliseconds();

//                param = command.CreateParameter();
//                command.Parameters.Add(param);
//                param.ParameterName = "@LoginIp";
//                param.Value = loginIp;

//                param = command.CreateParameter();
//                command.Parameters.Add(param);
//                param.ParameterName = "@Status";
//                param.Value = login? 1:-1;
               

//                param = command.CreateParameter();
//                command.Parameters.Add(param);
//                param.ParameterName = "@Token";
//                param.Value = "";

//                param = command.CreateParameter();
//                command.Parameters.Add(param);
//                param.ParameterName = "@LoginFailCount";
//                param.Value = login ? fail : (fail + 1);

//                int res = command.ExecuteNonQuery();
//                if (res == 1)
//                {
//                    command.Parameters.Clear();
//                    command.CommandText = string.Format("select timespen from users where account='{0}';",account);
                    
//                    reader = command.ExecuteReader();
//                    long timespen = reader.GetInt64(0);
//                    //防止 并发 操作 不一致
//                    //修改前 和修改 后 时间是否一致
//                    if(user.TimeSpan==timespen)
//                    {
//                        tx.Commit();
//                        if (login)
//                        {
//                            Console.WriteLine("{0} 登录成功,修改 状态 成功", account);
//                        }
//                        else
//                        {
//                            UserCache.FailCountIncrement(account); //update
//                            Console.WriteLine("{0} 登录失败,修改 状态 成功", account);
//                        }
//                        login = login? true:false;
                       
//                    }
//                    else
//                    {
//                        tx.Rollback();
//                        login = false;
//                        Console.WriteLine("同一 时间 该 账号 {0} 在多个 地方登录，该 登录 地点 登录失败", account);
//                    }
//                    reader.Close();
//                }
//                else
//                {
//                    if (login)
//                    {
//                        Console.WriteLine("{0} 登录成功,修改 状态失败", account);
//                    }
//                    else
//                    {
//                        Console.WriteLine("{0} 登录失败,修改 状态 失败", account);
//                    }
//                    login = false;
//                }
//            }
//            return login;
//        }

//        public virtual bool Register(string account, string pwd, long regIp)
//        {
//            if (!UserCache.Exists(account))
//            {
//                Console.WriteLine("{0} 账号 不存在", account);
//                return false;
//            }
//            var command = Write.CreateCommand();
//            command.CommandText = "insert into users( Id,Account,Pwd,RegDate,RegIp) values( @Id,@Account,@Pwd,@RegDate,@RegIp) ;";

//            var param = command.CreateParameter();
//            command.Parameters.Add(param);
//            param.ParameterName = "@Id";
//            param.Value = UserCache.GetId(Table);

//            param = command.CreateParameter();
//            command.Parameters.Add(param);
//            param.ParameterName = "@Account";
//            param.Value = account;

//            param = command.CreateParameter();
//            command.Parameters.Add(param);
//            param.ParameterName = "@Pwd";
//            param.Value = pwd;


//            param = command.CreateParameter();
//            command.Parameters.Add(param);
//            param.ParameterName = "@RegDate";
//            param.Value = DateUtils.TotalMilliseconds();

//            param = command.CreateParameter();
//            command.Parameters.Add(param);
//            param.ParameterName = "@RegIp";
//            param.Value = regIp;
//            int res = command.ExecuteNonQuery();
//            command.Dispose();
//            if (res > 0)
//            {
//                UserCache.Register(account);
//            }
//            return res>0;
//        }
//    }

//    /// <summary>
//    /// 问题 ： 如何 处理十几万 并发 数据
//    /// 假设 同 一时间 有 20w 用户 注册 或 20w 用户 登录
//    /// 用 时间戳 记录 开始前 完成前  时间是否一致
//    /// 时间一致 说明 没有 用户操作 
//    /// 时间不一致 取消 操作 防止脏读
//    /// </summary>
//    public class Question1
//    {
//        public static DbFlag Flag = DbFlag.SqlServer;

//        public static string ConnectionString =
//            "server=192.168.1.4;database=Test;user=sa;pwd=wjp930514.";
//        public static long Nums = 100 * 10000;
//        public static UserCache userCache = new UserCache();
//        //static long ip = IPAddress.Parse("127.0.0.1").Address;
//        static long ip = NetworkHelper.GetIp("127.0.0.1");
//        static string[] pwds = { "123456", "admin", "admin123" };
//        static int len = pwds.Length - 1;
//        static Random random = new Random();
//        public static void LoginTask()
//        {
//            Stopwatch stopwatch = new Stopwatch();
//            stopwatch.Start();
//            CancellationTokenSource cancellation = new CancellationTokenSource();
//            List<Task> tasks = new List<Task>(5);
//            Console.WriteLine("模拟5个 线程 并发 登录 starting ");
//            for (int i = 0; i < 5; i++)
//            {
//                long l = Nums / 5;

//                Task task = Task.Factory.StartNew(() => {
//                    long num = 10000 * (i + 1);
//                    var conn = ConnectionHelper.GetConnection(ConnectionString, Flag);
//                    UserRepository userRepository = new UserRepository(conn, userCache);

//                    for (int j = 0; j < l; j++)
//                    {
//                        userRepository.Login((num + l).ToString(), pwds[random.Next(len)], ip);
//                    }
//                }, cancellation.Token);
//                tasks.Add(task);
//                Thread.Sleep(500);// 防止 线程 启动 过慢 线程 变量 i 错误
//            }
//            Task.WaitAll(tasks.ToArray());
//            stopwatch.Stop();
//            Console.WriteLine("模拟5个 线程 并发 登录 finish ,花费 {0}", stopwatch.ElapsedMilliseconds);
//        }

//        public static void RegisterTask()
//        {
//            Stopwatch stopwatch = new Stopwatch();
//            stopwatch.Start();
//            CancellationTokenSource cancellation = new CancellationTokenSource();
//            List<Task> tasks = new List<Task>(5);
//            Console.WriteLine("模拟5个 线程 并发 注册 starting ");
//            //long ip = IPAddress.Parse("127.0.0.1").Address;
//            for (int i = 0; i < 5; i++)
//            {
//                long l = Nums / 5;
                
//                Task task = Task.Factory.StartNew(() => {
//                    long num = 10000 * (i + 1);
//                    var conn = ConnectionHelper.GetConnection(ConnectionString, Flag);
//                    UserRepository userRepository=new UserRepository(conn,userCache);
                
//                    for (int j = 0; j < l; j++)
//                    {
//                        string account = (num + l).ToString();
//                        bool reg=  userRepository.Register(account, pwds[random.Next(len)],ip);
//                        if (reg)
//                        {
//                            Console.WriteLine("{0} 账号 注册成功", account);
//                        }
//                        else
//                        {
//                            Console.WriteLine("{0} 账号 注册失败", account);
//                        }
//                    }
//                }, cancellation.Token);
//                tasks.Add(task);
//                Thread.Sleep(500);// 防止 线程 启动 过慢 线程 变量 i 错误
//            }
//            Task.WaitAll(tasks.ToArray());
//            stopwatch.Stop();
//            Console.WriteLine("模拟5个 线程 并发 注册 finish ,花费 {0}",stopwatch.ElapsedMilliseconds);
//        }
//    }
//}
