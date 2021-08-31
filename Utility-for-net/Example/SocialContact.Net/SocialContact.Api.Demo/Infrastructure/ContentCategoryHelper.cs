using Microsoft.AspNetCore.Mvc.Rendering;
using NHibernate;
using SocialContact.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tunynet.CMS;
using Tunynet.Common;
using Tunynet.Spacebuilder;

namespace SocialContact.Infrastructure
{
    public class ContentCategoryHelper
    {
        /// <summary>
        /// 下拉栏目获取
        /// </summary>
        /// <param name="contentModelKeys"></param>
        /// <param name="contentCategoryId"></param>
        /// <returns></returns>
        public static List<SelectListItem> ParseCMSContentCategoriesToSelectListItem(ISession session,string contentModelKeys, long? contentCategoryId)
        {
            var categoryId = contentCategoryId.HasValue ? contentCategoryId.Value : 0;
            //栏目获取
            List<SelectListItem> valueCategorys = new List<SelectListItem>();
            var categorieInfos = ContentCategoryAppService.GetIndentedAllCategories(session, true);
            for (int i = 0; i < categorieInfos.Count(); i++)
            {
                var folder = categorieInfos.ElementAt(i);
                var headString = "";
                if (folder.Depth > 0)
                {
                    if (folder.ChildCount > 0)
                    {
                        headString += "├";
                    }
                    else
                    {
                        headString += "└";
                    }
                    for (int j = 0; j < folder.Depth; j++)
                    {
                        headString += "─";
                    }
                }
                var selecttext = string.Format("{0}{1}", headString, folder.CategoryName);
                if (folder.ContentModelKeys.Contains(contentModelKeys))
                {
                    valueCategorys.Add(new SelectListItem { Text = selecttext, Value = folder.CategoryId.ToString(), Selected = folder.CategoryId == categoryId });
                }
                else
                {
                    continue;
                    //valueCategorys.Add(new SelectListItem { Text = selecttext, Value = folder.CategoryId.ToString(), Disabled = true, Selected = folder.CategoryId == categoryId });

                }
            }
            return valueCategorys;
        }

        /// <summary>
        /// 栏目-Json数据
        /// </summary>
        /// <returns></returns>
        public static List<object> CategoryJsons(ISession session,int id = 0, int openZtree = 0)
        {
            List<object> categoriesjson = new List<object>();

            IEnumerable<ContentCategory> rootCategorys;
            if (id == 0)
            {
                rootCategorys = ContentCategoryAppService.GetIndentedAllCategories(session, true);
            }
            else
            {
                rootCategorys = ContentCategoryService.GetCategoryDescendants(session, id);
            }
            foreach (var folder in rootCategorys)
            {
                if (folder.IsEnabled)
                {
                    var open = folder.ChildCount > 0 ? ContentCategoryService.GetCategoryDescendants(session,folder.CategoryId)
                        .Select(n => n.CategoryId).Contains(openZtree) : false;
                    categoriesjson.Add(new
                    {
                        id = folder.CategoryId,
                        pId = folder.ParentId,
                        name = folder.CategoryName,
                        open = open,
                        isHidden = !folder.IsEnabled,
                        font = folder.CategoryId == openZtree ? new { color = "#337ab7" } : null,
                    });
                }
            }
            return categoriesjson;
        }

        /// <summary>
        ///创建栏目（包括创建子栏目）
        /// </summary>
        /// <returns></returns>
        public static ContentCategory _EditContentCategories(ISession session, int categoryId = 0, int parentId = 0)
        {

            var contentCategoryModel = new ContentCategoryModel();
            var contentCategory = new ContentCategory();
            var contentCategoryPortal = new ContentCategoryPortal();

            //栏目ID大于0时编辑，否则添加
            if (categoryId > 0)
            {
                contentCategory = ContentCategoryService.Get(session,categoryId);
                contentCategory.MapTo(contentCategoryPortal);

                if (contentCategory == null)
                    return null;
                contentCategoryPortal.MapTo(contentCategoryModel);
                contentCategoryModel.ContentModelKeys =
                    new List<string>(contentCategoryPortal.ContentModelKeys.Split(','));
                bool isReference = false;
                var contentCategoryAdmin = CategoryManagerService.GetCategoryManagerIds(TenantTypeIds.Instance().ContentItem(), categoryId, out isReference);
                contentCategoryModel.ContentCategoryAdmin = contentCategoryAdmin.Count() > 0 ? contentCategoryAdmin.Select(l => l.ToString()).ToList() : new List<string>();
                contentCategoryModel.IsInherit = isReference;
            }
            else
            {
                if (parentId > 0)
                    contentCategoryModel.ParentId = parentId;
            }
            return contentCategory;
        }
    }
}
