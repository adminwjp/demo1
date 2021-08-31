#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Utility.IO;
using Utility.Oos;

namespace Utility.AspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private MinioHelper _minioHelper;

        public UploadController(MinioHelper minioHelper)
        {
            _minioHelper = minioHelper;
        }

        [HttpPost]
        public IActionResult Upload()
        {
            if (Request.Form.Files.Count > 0)
            {
                List<string> ids = new List<string>(20);
                foreach (var item in Request.Form.Files)
                {
                    //_minioHelper.GetId()
                    var id = $"{Guid.NewGuid().ToString("n")}.{item.FileName.Split('.').LastOrDefault()}";
                    ids.Add(id);
                    _minioHelper.Upload(StartHelper.BucktName, id, item.OpenReadStream(),item.ContentType);
                }
                return new JsonResult(new { code =200, success = true, data = ids, message = "upload success" });
            }
            return new JsonResult(new { code=400,success=false,data=(object)null,message="upload fail"});
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new NotFoundResult();
            }
            string fileName = string.Empty;// $"{Environment.CurrentDirectory}\\wwroot\\shop\\{id}";
           // if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                //fileName = fileName.Replace("\\","/");
                fileName = $"{Environment.CurrentDirectory}/wwroot/shop/{id}";
            }
            if (System.IO.File.Exists(fileName))
            {
                return new FileContentResult(FileHelper.ReadByte(fileName), "*/*");
            }
            var  res= await  _minioHelper.Get(StartHelper.BucktName, id, fileName);
            if (!res)
            {
                return new NotFoundResult();
            }
           
            // 0 字节 不会显示 图片
            //contentType 获取不到 需要自己转换? 还是显示不出来
            return new FileContentResult(FileHelper.ReadByte(fileName), "*/*");
            //内存流 读取不到数据
            //var buffer = StreamHelper.GetBuffer(res);
             //FileHelper.WriteFile("e:\\" + id, buffer);

            // return new FileStreamResult(res, "image/jpeg");

            //return new FileContentResult(buffer, "image/jpeg");
        }

    }
}
#endif