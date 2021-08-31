using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    using System.Net.Http;
    public class HttpCollect:HttpClient
    {
        public string GetString(string url)
        {
            try
            {
                return base.GetStringAsync(url).Result;
            }
            catch (Exception)
            {

                return string.Empty;
            }
        }
        public string PostString(string url,string param,string encoding="utf-8", string mediaType= "application/x-www-form-urlencoded")
        {
            try
            {
                StringContent stringContent = new StringContent(param,Encoding.GetEncoding(encoding),mediaType);
                return base.PostAsync(url, stringContent).Result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
