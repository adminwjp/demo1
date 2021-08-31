using Dapper;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Cap.Api.Infrastracture;
using Shop.Cap.Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.Domain.Entities;
using Utility;
using Utility.Extensions;
using Z.EntityFramework.Plus;
using Utility.Json.Extensions;
using MySqlConnector;
using Utility.IO;
using System.IO;
using Utility.Application.Services.Dtos;

namespace Shop.Cap.Api.Controllers
{
    public class CarouselDto
    {
        /// <summary>
        /// 素材背景图片
        /// </summary>
        public string Background { get; set; }

        /// <summary>
        /// 素材地址
        /// </summary>
        public string Src { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class CarouselController : ControllerBase
    {
        private readonly ICapPublisher _capBus;

        //public CarouselController(ICapPublisher capBus)
        //{
        //    _capBus = capBus;
        //}
        [HttpPost("add_many")]
        public dynamic AddMany([FromServices] CarouselDbContext dbContext,[FromForm,FromBody] ListDto<CarouselDto> data)
        {
            if (data!=null && data.Data !=null&& data.Data.Count > 0)
            {
                using (var trans = dbContext.Database.BeginTransaction()
                    //(_capBus, autoCommit: false)
                    )
                {
                    List<CarouselModel> carousels = new List<CarouselModel>();
                    foreach (var item in data.Data)
                    {
                        var carouse = new CarouselModel()
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            ImageId = item.Src,
                            Src = item.Src,
                            Background = item.Background,
                            Enable = true,
                            CreationTime = DateTime.Now,
                            Flag = Request.Query["t"].ToString() == "m"
                            ? CarouselFlag.Mobile : CarouselFlag.Pc
                        };
                        dbContext.Carousels.Add(carouse);
                        carousels.Add(carouse);
                    }


                    dbContext.SaveChanges();
                    trans.Commit();
                }
                return new { code = 2000, status = true };
            }
            return new { code = 4007, status = false };
        }
       
        [HttpPost("upload_many")]
        public dynamic UploadMany([FromServices] CarouselDbContext dbContext)
        {
            if (Request.Form.Files != null&& Request.Form.Files.Count>0)
            {
                if(Request.Form.Files.Count > 10)
                {
                    return new { code = 4006, status = false };
                }
                const string src = "src";
                const string background = "background";
                string suffix = "";
                int i = 1;
                List<Tuple<string, string>> res = new List<Tuple<string, string>>(10);
                foreach (var f in Request.Form.Files)
                {
                    if (f.Name.ToLower().Equals($"{src}{suffix}"))
                    {
                        var fileName = f.FileName;
                        var val=Request.Form[$"{background}{suffix}"].ToString();
                        FileHelper.WriteFile(Path.Combine(Environment.CurrentDirectory , fileName),
                            StreamHelper.GetBuffer(f.OpenReadStream()));
                        res.Add(new Tuple<string, string>(fileName,val));
                    }
                    suffix = i.ToString();
                    i++;
                }
                if (res.Count > 0)
                {
                    using (var trans = dbContext.Database.BeginTransaction()
                        //(_capBus, autoCommit: false)
                        )
                    {
                        List<CarouselModel> carousels = new List<CarouselModel>();
                        foreach (var item in res)
                        {
                            var carouse = new CarouselModel()
                            {
                                Id = Guid.NewGuid().ToString("N"),
                                ImageId = item.Item1,
                                Enable=true,
                                CreationTime = DateTime.Now,
                                Flag = Request.Form["t"].ToString()=="m"
                                ?CarouselFlag.Mobile  : CarouselFlag.Pc
                            };
                            dbContext.Carousels.Add(carouse);
                            carousels.Add(carouse);
                        }


                        dbContext.SaveChanges();
                        trans.Commit();
                    }
                }
                return new { code = 2000, status = true };
            }
            return new { code=4007,status=false};
        }

        [HttpGet()]
        [ResponseCache(Location= ResponseCacheLocation.Any,NoStore =false,Duration =5)]
        public dynamic Get([FromServices] CarouselDbContext dbContext)
        {
            var data=dbContext.Carousels.AsQueryable().Where(it => it.Enable).OrderByDescending(it => it.CreationTime).ToList();
            
            return new { code = 2000, status = true, data };
        }

        [HttpPost("add")]
        public ResponseApi Add([FromServices] CarouselDbContext dbContext,[FromBody]DeleteEntity<string> imgIds)
        {
            using (var trans = dbContext.Database.BeginTransaction(_capBus, autoCommit: false))
            {
                List<CarouselModel> carousels = new List<CarouselModel>();
                foreach (var item in imgIds.Ids)
                {
                    var carouse = new CarouselModel()
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        ImageId = item,
                        CreationTime = DateTime.Now,
                        Flag = CarouselFlag.Mobile
                    };
                    dbContext.Carousels.Add(carouse);
                    carousels.Add(carouse);
                }

                for (int i = 0; i < imgIds.Ids.Length; i++)
                {
                    _capBus?.Publish("sample.rabbitmq.mysql.add", carousels[i]);
                }

                dbContext.SaveChanges();

                trans.Commit();
            }
            return ResponseApi.Create(Language.Chinese, Code.AddSuccess );
        }

        [HttpGet("enable/{id}/{status}")]
        public ResponseApi Add(string id,int status)
        {
            using (var connection = new MySqlConnection(CarouselDbContext.ConnectionString))
            {
                using (var transaction = connection.BeginTransaction(_capBus, false))
                {
                    //your business code
                    var param = new { id = id, enable = status == 1 };
                    connection.Execute(sql: "update  t_carousel set enable=@enable where id=@id", param: param, transaction: (IDbTransaction)transaction.DbTransaction);

                    _capBus?.Publish("sample.rabbitmq.mysql.update", param);
                    transaction.Commit();
                }
            }
            return ResponseApi.Create(Language.Chinese, Code.ModifyStateSuccess);
        }

        [HttpGet("delete/{id}")]
        public ResponseApi Delete(string id)
        {
            using (var connection = new MySqlConnection(CarouselDbContext.ConnectionString))
            {
                using (var transaction = connection.BeginTransaction(_capBus, true))
                {
                    //your business code
                    connection.Execute(sql: "DELETE from t_carousel where id=@id",param:new { id=id}, transaction: (IDbTransaction)transaction.DbTransaction);

                    _capBus?.Publish("sample.rabbitmq.mysql.delete", id);
                }
            }
            return ResponseApi.Create(Language.Chinese, Code.DeleteSuccess);
        }

        [HttpPost("delete")]
        public ResponseApi Delete([FromServices] CarouselDbContext dbContext,[FromBody] DeleteEntity<string> ids)
        {
            using (var trans = dbContext.Database.BeginTransaction(_capBus, autoCommit: true))
            {
                Expression<Func<CarouselModel, bool>> where = null;
                foreach (var item in ids.Ids)
                {
                    where = where.Or(it => it.Id == item);
                }
                where = where.And(it=>!it.IsDeleted);
                dbContext.Carousels.Where(where).Update(it=>new CarouselModel { IsDeleted=true,LastModificationTime=DateTime.Now});
                dbContext.SaveChanges();
                for (int i = 0; i < ids.Ids.Length; i++)
                {
                    _capBus?.Publish("sample.rabbitmq.mysql.delete", ids.Ids[i]);
                }
            }
            return ResponseApi.Create(Language.Chinese, Code.DeleteSuccess);

        }

        [NonAction]
        [CapSubscribe("sample.rabbitmq.mysql.add")]
        public void SubscriberAdd(CarouselModel p)
        {
            Console.WriteLine($@"{DateTime.Now} Subscriber add invoked, Info: {p.ToJson()}");
        }

        [NonAction]
        [CapSubscribe("sample.rabbitmq.mysql.update")]
        public void SubscriberUpdate(dynamic p)
        {
            Console.WriteLine($@"{DateTime.Now} Subscriber enable invoked, Info: {p.ToJson()}");
        }


        [NonAction]
        [CapSubscribe("sample.rabbitmq.mysql.delete")]
        public void SubscriberDelete(string p)
        {
            Console.WriteLine($@"{DateTime.Now} Subscriber enable invoked, Info: {p}");
        }

        [NonAction]
        [CapSubscribe("sample.rabbitmq.mysql.delete", Group = "group.test2")]
        public void Subscriber2(string id, [FromCap] CapHeader header)
        {
            Console.WriteLine($@"{DateTime.Now} Subscriber invoked, Info: {id}");
        }
    }
}
