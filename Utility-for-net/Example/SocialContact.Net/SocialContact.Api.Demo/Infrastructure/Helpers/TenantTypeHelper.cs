using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tunynet.Common;

namespace SocialContact.Infrastructure.Helpers
{
    public class TenantTypeHelper
    {
        /// <summary>
        /// 推荐内容管理页面
        /// </summary>
        /// <param name="belong">推荐内容所属（租户Id）</param>
        /// <param name="typeId">推荐内容类型</param>
        /// <returns></returns>
        public static void ManageSpecialContentItems(string belong, int? typeId, ViewDataDictionary ViewData)
        {
            //推荐内容所属选择列表
            List<SelectListItem> contentTenantList = new List<SelectListItem>();
            contentTenantList.Add(new SelectListItem { Value = "", Text = "全部" });
            foreach (var tenant in TenantTypeService.Gets(MultiTenantServiceKeys.Instance().Recommend()))
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = tenant.Name;
                selectListItem.Value = tenant.TenantTypeId;
                if (tenant.TenantTypeId == belong)
                {
                    selectListItem.Selected = true;
                }
                contentTenantList.Add(selectListItem);
            }

            //推荐内容类型选择列表
            List<SelectListItem> contentTypeList = new List<SelectListItem>();
            contentTypeList.Add(new SelectListItem { Value = "0", Text = "全部" });
            foreach (var specialContentType in SpecialContentTypeService.GetAll())
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = specialContentType.Name;
                selectListItem.Value = specialContentType.TypeId.ToString();
                if (specialContentType.TypeId == typeId)
                {
                    selectListItem.Selected = true;
                }
                contentTypeList.Add(selectListItem);
            }
            ViewData["specialContentTypeList"] = contentTypeList;
            ViewData["contentTenantList"] = contentTenantList;
            ViewData["typeId"] = typeId;
            ViewData["belong"] = belong;
        }
    }
}
