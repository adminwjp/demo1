using Utility.Wpf.Demo.Template.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utility.Application.Services.Dtos;
using Utility.Json;
using Utility.Wpf.ViewModels;

namespace Utility.Wpf.Demo.Template.Service
{
    public  class TemplateService
    {
        public class ApiResult
        {
            public virtual bool Status { get; set; }
            public virtual int Code { get; set; }
        }
        public class ApiResult<T>: ApiResult
        {
            public virtual T Data { get; set; }
        }

        public static  TemplateService Empty = new TemplateService();
        public static string ApiUrl = "http://127.0.0.1:5000";
        public const string Add = "insert";
        public const string AddMany = "insert_many";
        public const string Edit = "update";
        public const string EditMany = "update_many";
        public const string Remove = "delete";
        public const string Find = "find";
        protected HttpClient Client;

        public TemplateService()
        {
            this.Client = new HttpClient() { Timeout=TimeSpan.FromSeconds(2)};
        }

        protected virtual string PostJson(string url,string json)
        {
            try
            {
                var response = Client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {

            }
            return "";
        }

        protected virtual string Get(string url)
        {
            try
            {
                var response = Client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {

            }
            return "";
        }

        protected virtual bool Operator(bool post,string url,string json="")
        {
            string result = string.Empty;
            if (post)
            {
                result = PostJson(url, json);
            }
            else
            {
                result = Get(url);
            }
            if (result == "")
            {
                return false;
            }
            ApiResult apiResult = JsonHelper.ToObject<ApiResult>(result, JsonHelper.JsonSerializerSettings);
            return apiResult.Status;
        }

        protected virtual List<ItemViewModel> GetItems(string url)
        {
            string result = Get(url);
            if (result == "")
            {
                return null;
            }
            ApiResult<List<ItemViewModel>> apiResult = JsonHelper.ToObject<ApiResult<List<ItemViewModel>>>(result, JsonHelper.JsonSerializerSettings);
            if (apiResult.Status)
            {
                return apiResult.Data;
            }
            return null;
        }

        protected virtual ResultDto<T> GetList<T>(string url,string json)
        {
            string result = PostJson(url, json);
            if (result == "")
            {
                return null;
            }
            ApiResult<ResultDto<T>> apiResult = JsonHelper.ToObject<ApiResult<ResultDto<T>>>(result, JsonHelper.JsonSerializerSettings);
            if (apiResult.Status)
            {
                return apiResult.Data;
            }
            return null;
        }
        public virtual List<ItemViewModel> GetDb(DatabaseViewModel db)
        {
            string json = JsonHelper.ToJson(db, JsonHelper.JsonSerializerSettings);
            return GetItems( $"{ApiUrl}/database/get");
        }

        public virtual List<ItemViewModel> GetTab(DatabaseViewModel db)
        {
            string json = JsonHelper.ToJson(db, JsonHelper.JsonSerializerSettings);
            return GetItems($"{ApiUrl}/database/get");
        }

        public virtual bool AddDb(DatabaseViewModel db)
        {
            string json = JsonHelper.ToJson(db, JsonHelper.JsonSerializerSettings);
            return Operator(true, $"{ApiUrl}/database/{Add}",json);
        }
        public virtual bool EditDb(DatabaseViewModel db)
        {
            string json = JsonHelper.ToJson(db, JsonHelper.JsonSerializerSettings);
            return Operator(true, $"{ApiUrl}/database/{Edit}", json);
        }
        public virtual bool AddDb(ListDto<DatabaseViewModel> db)
        {
            string json = JsonHelper.ToJson(db, JsonHelper.JsonSerializerSettings);
            return Operator(true, $"{ApiUrl}/database/{AddMany}", json);
        }
        public virtual bool EditDb(ListDto<DatabaseViewModel> db)
        {
            string json = JsonHelper.ToJson(db, JsonHelper.JsonSerializerSettings);
            return Operator(true, $"{ApiUrl}/database/{EditMany}", json);
        }
        public virtual bool RemoveDb(long id)
        {
            return Operator(false, $"{ApiUrl}/database/{Remove}/{id}");
        }

        public virtual bool RemoveDb(long[] ids)
        {
            string json = JsonHelper.ToJson(new { ids}, JsonHelper.JsonSerializerSettings);
            return Operator(true, $"{ApiUrl}/database/{Remove}", json);
        }
        public virtual ResultDto<DatabaseViewModel> GetDbs(DatabaseViewModel db ,int page,int size)
        {
            string json = JsonHelper.ToJson(db, JsonHelper.JsonSerializerSettings);
            return GetList<DatabaseViewModel>( $"{ApiUrl}/database/{Find}/{page}{size}", json);
        }

        public virtual bool AddTab(TableViewModel table)
        {
            string json = JsonHelper.ToJson(table, JsonHelper.JsonSerializerSettings);
            return Operator(true, $"{ApiUrl}/table/{Add}", json);
        }
        public virtual bool EditTab(TableViewModel table)
        {
            string json = JsonHelper.ToJson(table, JsonHelper.JsonSerializerSettings);
            return Operator(true, $"{ApiUrl}/table/{Edit}", json);
        }


        public virtual bool AddTab(ListDto<TableViewModel> table)
        {
            string json = JsonHelper.ToJson(table, JsonHelper.JsonSerializerSettings);
            return Operator(true, $"{ApiUrl}/table/{AddMany}", json);
        }
        public virtual bool EditTab(ListDto<TableViewModel> table)
        {
            string json = JsonHelper.ToJson(table, JsonHelper.JsonSerializerSettings);
            return Operator(true, $"{ApiUrl}/table/{EditMany}", json);
        }
        public virtual bool RemoveTab(long id)
        {
            return Operator(false, $"{ApiUrl}/table/{Remove}/{id}");
        }

        public virtual bool RemoveTab(long[] ids)
        {
            string json = JsonHelper.ToJson(new { ids }, JsonHelper.JsonSerializerSettings);
            return Operator(true, $"{ApiUrl}/table/{Remove}", json);
        }
        public virtual ResultDto<TableViewModel> GetTabs(TableViewModel table,int page, int size)
        {
            string json = JsonHelper.ToJson(table, JsonHelper.JsonSerializerSettings);
            return GetList<TableViewModel>($"{ApiUrl}/table/{Find}/{page}{size}", json);
        }

        public virtual bool AddCol(ColumnViewModel column)
        {
            string json = JsonHelper.ToJson(column, JsonHelper.JsonSerializerSettings);
            return Operator(true, $"{ApiUrl}/column/{Add}", json);
        }
        public virtual bool EditCol(ColumnViewModel column)
        {
            string json = JsonHelper.ToJson(column, JsonHelper.JsonSerializerSettings);
            return Operator(true, $"{ApiUrl}/column/{Edit}", json);
        }

        public virtual bool AddCol(ListDto<ColumnViewModel> column)
        {
            string json = JsonHelper.ToJson(column, JsonHelper.JsonSerializerSettings);
            return Operator(true, $"{ApiUrl}/column/{AddMany}", json);
        }
        public virtual bool EditCol(ListDto<ColumnViewModel> column)
        {
            string json = JsonHelper.ToJson(column, JsonHelper.JsonSerializerSettings);
            return Operator(true, $"{ApiUrl}/column/{EditMany}", json);
        }
        public virtual bool RemoveCol(long id)
        {
            return Operator(false, $"{ApiUrl}/column/{Remove}/{id}");
        }

        public virtual bool RemoveCol(long[] ids)
        {
            string json = JsonHelper.ToJson(new { ids }, JsonHelper.JsonSerializerSettings);
            return Operator(true, $"{ApiUrl}/column/{Remove}", json);
        }
        public virtual ResultDto<ColumnViewModel> GetCols(ColumnViewModel query,int page, int size)
        {
            string json = JsonHelper.ToJson(query, JsonHelper.JsonSerializerSettings);
            return GetList<ColumnViewModel>($"{ApiUrl}/column/{Find}/{page}{size}", json);
        }
    }
}
