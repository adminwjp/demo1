//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using Tunynet.CMS;
using Tunynet.Common;
using Tunynet.Post;
using Tunynet.Settings;

namespace Tunynet.Spacebuilder
{
    /// <summary>
    /// 全文检索
    /// </summary>
    public class SearchController : Controller
    {
        #region private


        /// <summary>
        /// 返回检索信息
        /// </summary>
        /// <param name="result"></param>
        /// <param name="page"></param>
        /// <param name="_desc"></param>
        /// <returns></returns>
        private JsonResult successResult(object result, object page = null, string _desc = "")
        {
            return Json(new { Data = result, Page = page, Description = _desc });
        }

        #endregion private

        #region 全文检索 主要提供给web端使用

        /// <summary>
        /// 搜索全部
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult SearchAll(string keyword, int pageSize = 5)
        {
            AllData allData = new AllData();
            Dictionary<string, long> pages = new Dictionary<string, long>();
            long count = 0;

            var url = ConfigurationManager.AppSettings["Search"];

            try
            {
                //计时开始
                Stopwatch sw = new Stopwatch();
                sw.Start();

                #region 资讯

                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}/Search/CmsSearch?keyword={1}&pageSize={2}", url, keyword, pageSize));
                myRequest.Method = "GET";

                HttpWebResponse myResponse = null;

                myResponse = (HttpWebResponse)myRequest.GetResponse();
                if (myResponse.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                    string content = reader.ReadToEnd();

                    var results = JsonConvert.DeserializeObject<SearchResultModel>(content);

                    allData.CmsResults = results.Data;
                    pages.Add("Cms", results.Page.TotalRecords);
                    count += results.Page.TotalRecords;
                }

                #endregion 资讯

                #region 贴子

                myRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}/Search/ThreadSearch?keyword={1}&pageSize={2}", url, keyword, pageSize));
                myRequest.Method = "GET";

                myResponse = null;

                myResponse = (HttpWebResponse)myRequest.GetResponse();
                if (myResponse.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                    string content = reader.ReadToEnd();

                    var results = JsonConvert.DeserializeObject<SearchResultModel>(content);

                    allData.ThreadResults = results.Data;
                    pages.Add("Thread", results.Page.TotalRecords);
                    count += results.Page.TotalRecords;
                }

                #endregion 贴子

                #region 问答

                if (Tunynet.Common.Utility.CheckApplication("Ask"))
                {
                    myRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}/AskSearch/QuickAskSearch?keyword={1}&pageSize={2}", url, keyword, pageSize));
                    myRequest.Method = "GET";

                    myResponse = null;

                    myResponse = (HttpWebResponse)myRequest.GetResponse();
                    if (myResponse.StatusCode == HttpStatusCode.OK)
                    {
                        StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                        string content = reader.ReadToEnd();

                        var results = JsonConvert.DeserializeObject<SearchResultModel>(content);

                        allData.AskResults = results.Data;
                        pages.Add("Ask", results.Page.TotalRecords);
                        count += results.Page.TotalRecords;
                    }
                }
                else
                {
                    allData.AskResults = null;
                }

                #endregion 问答

                #region 文库

                if (Tunynet.Common.Utility.CheckApplication("Document"))
                {
                    myRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}/DocSearch/DocSearch?keyword={1}&pageSize={2}", url, keyword, pageSize));
                    myRequest.Method = "GET";

                    myResponse = null;

                    myResponse = (HttpWebResponse)myRequest.GetResponse();
                    if (myResponse.StatusCode == HttpStatusCode.OK)
                    {
                        StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                        string content = reader.ReadToEnd();

                        var results = JsonConvert.DeserializeObject<SearchResultModel>(content);

                        allData.DocResults = results.Data;
                        pages.Add("Document", results.Page.TotalRecords);
                        count += results.Page.TotalRecords;
                    }
                }
                else
                {
                    allData.DocResults = null;
                }

                #endregion 文库

                sw.Stop();
                TimeSpan timespan = sw.Elapsed;

                AllResultModel model = new AllResultModel
                {
                    Type = 1,
                    Data = allData,
                    Page = pages,
                    Description = string.Format("约有 {0} 个搜索结果（用时 {1:F3} 秒）", count, timespan.TotalSeconds)
                };

                return Json(model);
            }
            //异常请求
            catch (WebException)
            {
                return Json(new AllResultModel());
            }
        }

        /// <summary>
        /// 资讯搜索
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="minDate">起始时间</param>
        /// <param name="isDefaultOrder">是否默认排序</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult CmsSearch(string keyword, DateTime? minDate, bool isDefaultOrder = true, int pageIndex = 1, int pageSize = 10)
        {
            return Json("");
            CmsFullTextQuery query = new CmsFullTextQuery();

            query.Keyword = Utilities.WebUtility.UrlDecode(keyword);
            query.PageSize = pageSize;//每页记录数
            query.PageIndex = pageIndex;
            query.PubliclyAuditStatus = siteSetting.AuditStatus;

            if (minDate.HasValue)
            {
                query.MaxDate = DateTime.Now;
                query.MinDate = minDate.Value;
            }

            //排序
            query.IsDefaultOrder = isDefaultOrder;

            //计时开始
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //调用搜索器进行搜索
            //CmsSearcher cmsSearcher = (CmsSearcher)SearcherFactory.GetSearcher(CmsSearcher.CODE);
            //PagingDataSet<ContentItem> contentItems = cmsSearcher.Search(query);

            //sw.Stop();
            //TimeSpan timespan = sw.Elapsed;

            //return successResult(contentItems.Select(n => new
            //{
            //    Id = n.ContentItemId,
            //    Subject = SearchEngine.Highlight(keyword, HtmlUtility.TrimHtml(n.Subject, 100), 100),
            //    Body = string.IsNullOrEmpty(n.Summary) ? SearchEngine.Highlight(keyword, HtmlUtility.TrimHtml(n.Body, 500), 500) : SearchEngine.Highlight(keyword, HtmlUtility.TrimHtml(n.Summary, 500), 500),
            //    UserId = n.UserId,
            //    Author = n.Author,
            //    DateString = n.DatePublished.ToFriendlyDate()
            //}), new
            //{
            //    PageCount = contentItems.PageCount,
            //    PageIndex = contentItems.PageIndex,
            //    PageSize = contentItems.PageSize,
            //    TotalRecords = contentItems.TotalRecords,
            //}, string.Format("约有 {0} 个搜索结果（用时 {1:F3} 秒）", contentItems.TotalRecords, timespan.TotalSeconds));
        }

        /// <summary>
        /// 贴子搜索
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="minDate">起始时间</param>
        /// <param name="isDefaultOrder">是否默认排序</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult ThreadSearch(string keyword, DateTime? minDate, bool isDefaultOrder = true, int pageIndex = 1, int pageSize = 10)
        {
            return Json("");
            ThreadFullTextQuery query = new ThreadFullTextQuery();

            query.Keyword = Utilities.WebUtility.UrlDecode(keyword);
            query.PageSize = pageSize;//每页记录数
            query.PageIndex = pageIndex;
            query.PubliclyAuditStatus = siteSetting.AuditStatus;

            if (minDate.HasValue)
            {
                query.MaxDate = DateTime.Now;
                query.MinDate = minDate.Value;
            }

            //排序
            query.IsDefaultOrder = isDefaultOrder;

            //计时开始
            Stopwatch sw = new Stopwatch();
            sw.Start();

            ////调用搜索器进行搜索
            //ThreadSearcher threadSearcher = (ThreadSearcher)SearcherFactory.GetSearcher(ThreadSearcher.CODE);
            //PagingDataSet<Thread> threads = threadSearcher.Search(query);

            //sw.Stop();
            //TimeSpan timespan = sw.Elapsed;

            //return successResult(threads.Select(n => new
            //{
            //    Id = n.ThreadId,
            //    Subject = SearchEngine.Highlight(keyword, HtmlUtility.TrimHtml(n.Subject, 100), 100),
            //    Body = SearchEngine.Highlight(keyword, HtmlUtility.TrimHtml(n.GetBody(), 500), 500),
            //    UserId = n.UserId,
            //    Author = n.Author,
            //    DateString = n.DateCreated.ToFriendlyDate()
            //}), new
            //{
            //    PageCount = threads.PageCount,
            //    PageIndex = threads.PageIndex,
            //    PageSize = threads.PageSize,
            //    TotalRecords = threads.TotalRecords,
            //}, string.Format("约有 {0} 个搜索结果（用时 {1:F3} 秒）", threads.TotalRecords, timespan.TotalSeconds));
        }

        /// <summary>
        /// 贴吧内搜索
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="keyword"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult ThreadSearchInSection(long sectionId, string keyword, int pageIndex = 1, int pageSize = 10)
        {
            return Json("");
            ThreadFullTextQuery query = new ThreadFullTextQuery();

            query.Keyword = Utilities.WebUtility.UrlDecode(keyword);
            query.SectionId = sectionId.ToString();
            query.PageSize = pageSize;//每页记录数
            query.PageIndex = pageIndex;
            query.PubliclyAuditStatus = siteSetting.AuditStatus;

            //调用搜索器进行搜索
            //ThreadSearcher threadSearcher = (ThreadSearcher)SearcherFactory.GetSearcher(ThreadSearcher.CODE);
            //PagingDataSet<long> threads = threadSearcher.SearchThreadIds(query);

            //return successResult(threads.Select(n => n),
            //    new
            //    {
            //        PageCount = threads.PageCount,
            //        PageIndex = threads.PageIndex,
            //        PageSize = threads.PageSize,
            //        TotalRecords = threads.TotalRecords,
            //    });
        }

        /// <summary>
        /// 评论搜索
        /// </summary>
        /// <param name="publicAuditStatus"></param>
        /// <param name="keyword"></param>
        /// <param name="tenantTypeId"></param>
        /// <param name="minDate"></param>
        /// <param name="maxDate"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult CommentSearch(PubliclyAuditStatus? publicAuditStatus, string keyword = null, string tenantTypeId = null, DateTime? minDate = null, DateTime? maxDate = null, int pageSize = 20, int pageIndex = 1)
        {
            return Json("");
            CommentFullTextQuery query = new CommentFullTextQuery();
            query.PageSize = pageSize;
            query.PageIndex = pageIndex;
            if (publicAuditStatus.HasValue)
            {
                query.PubliclyAuditStatus = publicAuditStatus;
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query.Keyword = Utilities.WebUtility.UrlDecode(keyword);
            }

            if (maxDate.HasValue && minDate.HasValue)
            {
                query.MaxDate = maxDate.Value;
                query.MinDate = minDate.Value;
            }

            if (!string.IsNullOrWhiteSpace(tenantTypeId))
            {
                query.TenantTypeId = tenantTypeId;
            }

            //CommentSearcher commentSearcher = (CommentSearcher)SearcherFactory.GetSearcher(CommentSearcher.CODE);
            //PagingDataSet<long> comments = commentSearcher.Search(query);

            //return successResult(comments.Select(n => n),
            //    new
            //    {
            //        PageCount = comments.PageCount,
            //        PageIndex = comments.PageIndex,
            //        PageSize = comments.PageSize,
            //        TotalRecords = comments.TotalRecords,
            //    });
        }

        ///// <summary>
        ///// 索引任务运行
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public HttpResponseMessage RunAskTask(int id)
        //{
        //    var secret = ConfigurationManager.AppSettings["ApiAccessSecret"];
        //    //从http请求的头里面获取身份验证信息，验证是否是请求发起方的ticket
        //    var authorization = Request.Headers.Authorization;

        //    if ((authorization != null) && (authorization.Parameter != null))
        //    {
        //        TaskDetail td = taskService.Get(id);
        //        if (td == null)
        //        {
        //            return errorResult("执行失败!");
        //        }
        //        TaskSchedulerFactory.GetScheduler().Run(id);

        //        return successResult("执行成功!");
        //    }
        //    return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
        //}

        #endregion 全文检索 主要提供给web端使用

        #region 手机端搜索

        /// <summary>
        /// 搜索全部
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult MobileSearchAll(string keyword, int pageSize = 5)
        {
            AllData allData = new AllData();
            Dictionary<string, long> pages = new Dictionary<string, long>();

            var url = ConfigurationManager.AppSettings["Search"];
            string contentItems = "";
            string threads = "";
            string questions = "";
            string docResults = "";
            try
            {
                #region 资讯

                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}/Search/MobileCmsSearchAll?keyword={1}&pageSize={2}", url, keyword, pageSize));
                myRequest.Method = "GET";

                HttpWebResponse myResponse = null;

                myResponse = (HttpWebResponse)myRequest.GetResponse();
                if (myResponse.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                    contentItems = reader.ReadToEnd();
                }

                #endregion 资讯

                #region 贴子

                myRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}/Search/MobileThreadSearchAll?keyword={1}&pageSize={2}", url, keyword, pageSize));
                myRequest.Method = "GET";

                myResponse = null;

                myResponse = (HttpWebResponse)myRequest.GetResponse();
                if (myResponse.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                    threads = reader.ReadToEnd();
                }

                #endregion 贴子

                #region 问答

                if (Tunynet.Common.Utility.CheckApplication("Ask"))
                {
                    myRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}/AskSearch/MobileAskSearchAll?keyword={1}&pageSize={2}", url, keyword, pageSize));
                    myRequest.Method = "GET";

                    myResponse = null;

                    myResponse = (HttpWebResponse)myRequest.GetResponse();
                    if (myResponse.StatusCode == HttpStatusCode.OK)
                    {
                        StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                        questions = reader.ReadToEnd();
                    }
                }

                #endregion 问答

                #region 文库

                if (Tunynet.Common.Utility.CheckApplication("Document"))
                {
                    myRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}/DocSearch/MobileDocSearchAll?keyword={1}&pageSize={2}", url, keyword, pageSize));
                    myRequest.Method = "GET";

                    myResponse = null;

                    myResponse = (HttpWebResponse)myRequest.GetResponse();
                    if (myResponse.StatusCode == HttpStatusCode.OK)
                    {
                        StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                        docResults = reader.ReadToEnd();
                    }
                }

                #endregion 文库

                return Json(new
                {
                    ContentItem = contentItems,
                    Thread = threads,
                    Question = questions,
                    DocResults = docResults
                });
            }
            //异常请求
            catch (WebException)
            {
                return Json(null);
            }
        }

        /// <summary>
        /// 手机端搜索资讯
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult MobileCmsSearch(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            return Json("");
            CmsFullTextQuery query = new CmsFullTextQuery();

            query.Keyword = Utilities.WebUtility.UrlDecode(keyword);
            query.PageSize = pageSize;//每页记录数
            query.PageIndex = pageIndex;

            ////调用搜索器进行搜索
            //CmsSearcher cmsSearcher = (CmsSearcher)SearcherFactory.GetSearcher(CmsSearcher.CODE);
            //PagingDataSet<ContentItem> contentItems = cmsSearcher.Search(query);

            //return successResult(contentItems != null && contentItems.Any() ? contentItems.Select(n => new
            //{
            //    Id = n.ContentItemId,
            //    Subject = SearchEngine.Highlight(keyword, HtmlUtility.TrimHtml(n.Subject, 100), 100),
            //    Body = SearchEngine.Highlight(keyword, HtmlUtility.TrimHtml(n.Body, 500), 500),
            //    DateCreated = n.DateCreated.ToFriendlyDate(),
            //    ModelName = n.ContentModel.ModelName,
            //    Category = new
            //    {
            //        Id = n.ContentCategory.ParentId > 0 ? n.ContentCategory.ParentId : n.ContentCategory.CategoryId,
            //        Name = n.ContentCategory.CategoryName
            //    }
            //}) : null, new
            //{
            //    PageIndex = pageIndex,
            //    PageSize = pageSize,
            //    TotalRecords = contentItems.TotalRecords
            //});
        }

        /// <summary>
        /// 手机端搜索贴子
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult MobileThreadSearch(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            return Json("");
            ThreadFullTextQuery query = new ThreadFullTextQuery();

            query.Keyword = Utilities.WebUtility.UrlDecode(keyword);
            query.PageSize = pageSize;//每页记录数
            query.PageIndex = pageIndex;

            //调用搜索器进行搜索
            //ThreadSearcher threadSearcher = (ThreadSearcher)SearcherFactory.GetSearcher(ThreadSearcher.CODE);
            //PagingDataSet<Thread> threads = threadSearcher.Search(query);

            //return successResult(threads != null & threads.Any() ? threads.Select(n => new
            //{
            //    Id = n.ThreadId,
            //    Subject = SearchEngine.Highlight(keyword, HtmlUtility.TrimHtml(n.Subject, 100), 100),
            //    Body = SearchEngine.Highlight(keyword, HtmlUtility.TrimHtml(n.GetBody(), 500), 500),
            //    DateCreated = n.DateCreated.ToFriendlyDate(),
            //    BarSection = new
            //    {
            //        Id = n.BarSection.SectionId,
            //        Name = n.BarSection.Name
            //    }
            //}) : null, new
            //{
            //    PageIndex = pageIndex,
            //    PageSize = pageSize,
            //    TotalRecords = threads.TotalRecords
            //});
        }

        /// <summary>
        /// 手机端搜索资讯(全部)
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult MobileCmsSearchAll(string keyword, int pageSize = 5)
        {
            return Json("");
            CmsFullTextQuery query = new CmsFullTextQuery();

            query.Keyword = Utilities.WebUtility.UrlDecode(keyword);
            query.PageSize = pageSize;//每页记录数
            query.PageIndex = 1;

            ////调用搜索器进行搜索
            //CmsSearcher cmsSearcher = (CmsSearcher)SearcherFactory.GetSearcher(CmsSearcher.CODE);
            //PagingDataSet<ContentItem> contentItems = cmsSearcher.Search(query);

            //return Json(new
            //{
            //    ContentItemList = contentItems != null && contentItems.Any() ? contentItems.Select(n => new
            //    {
            //        Id = n.ContentItemId,
            //        Subject = SearchEngine.Highlight(keyword, HtmlUtility.TrimHtml(n.Subject, 100), 100),
            //        Body = SearchEngine.Highlight(keyword, HtmlUtility.TrimHtml(n.Body, 500), 500),
            //        DateCreated = n.DateCreated.ToFriendlyDate(),
            //        ModelName = n.ContentModel.ModelName,
            //        Category = new
            //        {
            //            Id = n.ContentCategory.ParentId > 0 ? n.ContentCategory.ParentId : n.ContentCategory.CategoryId,
            //            Name = n.ContentCategory.CategoryName
            //        }
            //    }) : null,
            //    TotalRecords = contentItems.TotalRecords
            //});
        }

        /// <summary>
        /// 手机端搜索贴子(全部)
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult MobileThreadSearchAll(string keyword, int pageSize = 5)
        {
            return Json("");
            ThreadFullTextQuery query = new ThreadFullTextQuery();

            query.Keyword = Utilities.WebUtility.UrlDecode(keyword);
            query.PageSize = pageSize;//每页记录数
            query.PageIndex = 1;

            ////调用搜索器进行搜索
            //ThreadSearcher threadSearcher = (ThreadSearcher)SearcherFactory.GetSearcher(ThreadSearcher.CODE);
            //PagingDataSet<Thread> threads = threadSearcher.Search(query);

            //return Json(new
            //{
            //    ThreadList = threads != null & threads.Any() ? threads.Select(n => new
            //    {
            //        Id = n.ThreadId,
            //        Subject = SearchEngine.Highlight(keyword, HtmlUtility.TrimHtml(n.Subject, 100), 100),
            //        Body = SearchEngine.Highlight(keyword, HtmlUtility.TrimHtml(n.GetBody(), 500), 500),
            //        DateCreated = n.DateCreated.ToFriendlyDate(),
            //        BarSection = new
            //        {
            //            Id = n.BarSection.SectionId,
            //            Name = n.BarSection.Name
            //        }
            //    }) : null,
            //    TotalRecords = threads.TotalRecords
            //});
        }

        #endregion 手机端搜索
    }
}