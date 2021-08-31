using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Database.Entities
{
    /// <summary>
    /// 数据库 信息
    /// </summary>
    public class DatabaseEntity
    {
        private string _database;
        /// <summary>
        /// 数据库 表
        /// </summary>
        public string Database { get; set; }
        /// <summary>
        /// mysql 有效 utf8
        /// </summary>
        public string Charset { get; set; } = "utf8";
        /// <summary>
        /// mysql 有效 utf8_general_ci
        /// </summary>
        public string Collate { get; set; } = "utf8_general_ci";

        /// <summary>
        /// sqlserver 有效  给了 该 参数 以下 参数 必须 给值
        /// </summary>
        public string Path { get; set; }

        //主文件
        /// <summary>
        /// sqlserver 有效
        /// </summary>
        public string MdfSize { get; set; } = "1024KB";//1024KB 10M(语法报错)
        /// <summary>
        /// sqlserver 有效 UNLIMITED
        /// </summary>
        public string MdfMaxSize { get; set; } = "UNLIMITED";
        /// <summary>
        /// sqlserver 有效 1024KB
        /// </summary>

        public string MdfFileGrowth { get; set; } = "1024KB";

        //日志文件
        /// <summary>
        /// sqlserver 有效 2048KB
        /// </summary>
        public string LdfSize { get; set; } = "2048KB";
        /// <summary>
        /// sqlserver 有效 2048GB
        /// </summary>
        public string LdfMaxSize { get; set; } = "2048GB";
        /// <summary>
        /// sqlserver 有效 10%
        /// </summary>

        public string LdfFileGrowth { get; set; } = "10%";

    }
}
