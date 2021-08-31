using Minio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Oos
{
    /// <summary>
    /// minio 免费的 oos 存储
    /// https://docs.min.io/cn/dotnet-client-quickstart-guide.html
    /// </summary>
    public class MinioHelper
    {
        /// <summary>
        /// 初始化 地址 账户 密码
        /// </summary>
        /// <param name="host">地址</param>
        /// <param name="user">账户</param>
        /// <param name="pwd">密码</param>
        public MinioHelper(string host, string user, string pwd)
        {
            Host = host;
            User = user;
            Pwd = pwd;
            this.Client = new MinioClient(Host, User, Pwd);
        }
        /// <summary>
        /// 初始化 地址
        /// 账户 minioadmin 密码 minioadmin
        /// </summary>
        /// <param name="host">地址</param>
        public MinioHelper(string host)
        {
            Host = host;
            this.Client = new MinioClient(Host, User, Pwd);
        }
        /// <summary>
        /// 初始化 
        /// 地址 http://127.0.0.1:9000
        /// 账户 minioadmin 密码 minioadmin
        /// </summary>
        public MinioHelper()
        {
            this.Client = new MinioClient(Host, User, Pwd);
        }

        /// <summary>
        /// 地址
        /// </summary> 
        public string Host { get; private set; } = "http://127.0.0.1:9000";
        /// <summary>
        /// 账户
        /// </summary>
        public string User { get; set; } = "minioadmin";
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; } = "minioadmin";
        /// <summary>
        /// MinioClient 对象
        /// </summary>
        public MinioClient Client { get;private set; }

        /// <summary>
        /// 上传 文件 
        /// </summary>
        /// <param name="bucketName">桶名称</param>
        /// <param name="objectName">文件对象名称</param>
        /// <param name="stream">文件流</param>
        /// <param name="contentType">上传类型</param>
        /// <returns></returns>
        public virtual bool Upload(string bucketName,string objectName,Stream stream,string contentType = "application/zip")
        {
           // Make a bucket on the server, if not already present.
            bool found = Client.BucketExistsAsync(bucketName).Result;
            if (!found)
            {
                Client.MakeBucketAsync(bucketName).Wait();
            }

            //if(Get(bucketName, objectName) != null)
            //{
            //    Console.WriteLine("exists update");
            //}

            // Upload a file to bucket.
            Client.PutObjectAsync(bucketName, objectName, stream, stream.Length).Wait();
            return true;
        }

        /// <summary>
        /// 获取 文件流对象 
        /// </summary>
        /// <param name="bucketName">桶名称</param>
        /// <param name="objectName">文件对象名称</param>
        /// <returns></returns>
        public virtual Stream Get(string bucketName, string objectName)
        {
           // Make a bucket on the server, if not already present.
            bool found = Client.BucketExistsAsync(bucketName).Result;
            if (!found)
            {
                return null;
            }
            Stream stream=null;
            Client.GetObjectAsync(bucketName, objectName, it => { stream = it; }).Wait();
            return stream;
        }

        /// <summary>
        /// 获取 文件流对象 
        /// </summary>
        /// <param name="bucketName">桶名称</param>
        /// <param name="objectName">文件对象名称</param>
        /// <param name="fileName">下载存储路径</param>
        /// <returns></returns>
        public virtual async Task<bool> Get(string bucketName, string objectName,string fileName)
        {
            // Make a bucket on the server, if not already present.
            bool found =await Client.BucketExistsAsync(bucketName);
            if (!found)
            {
                return false;
            }
            await Client.GetObjectAsync(bucketName, objectName, fileName);
            return true;
        }
    }
}
