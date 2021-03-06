//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tunynet.Settings;

namespace Tunynet.Common
{
    /// <summary>
    /// 地区下列表
    /// </summary>
    public static class HtmlHelperAreaDropDownListExtensions
    {
        /// <summary>
        /// 地区下拉列表
        /// </summary>
        /// <param name="htmlHelper">被扩展的htmlHelper实例</param>
        /// <param name="expression">选择实体中类别属性的lamda表达式</param>
        /// <param name="areaLevel">地区层级(默认取站点地区配置）</param>
        /// <param name="rootAreaCode">根级地区（默认取站点地区配置）</param>
        public static HtmlString AreaDropDownListFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, string>> expression, int? areaLevel = null, string rootAreaCode = null)
        {
            string getChildAreasUrl = "GetChildAreas/Common";
            AreaServiceHelper areaServiceHelper = new AreaServiceHelper();
            if (areaLevel == null)
            {
                AreaSettings areaSettings = DIContainer.Resolve<ISettingsManager<AreaSettings>>().Get();
                areaLevel = areaSettings.AreaLevel;
            }
            return htmlHelper.LinkageDropDownListFor<TModel, string>(expression, string.Empty, areaLevel.Value, areaServiceHelper.GetRootAreaDictionary(rootAreaCode), areaServiceHelper.GetParentCode, areaServiceHelper.GetChildrenDictionary, getChildAreasUrl);
        }

        /// <summary>
        /// 地区下拉列表
        /// </summary>
        /// <param name="htmlHelper">被扩展的htmlHelper实例</param>
        /// <param name="name">控件name属性</param>
        /// <param name="name">选中的地区编码</param>
        /// <param name="areaLevel">地区层级(默认取站点配置）</param>
        /// <param name="rootAreaCode">根级地区（默认取站点地区配置）</param>
        public static HtmlString AreaDropDownList(this HtmlHelper htmlHelper, string name, string value, int? areaLevel = null, string rootAreaCode = null)
        {
            string getChildAreasUrl = "GetChildAreas/Common";
            AreaServiceHelper areaServiceHelper = new AreaServiceHelper();
            if (areaLevel == null)
            {
                AreaSettings areaSettings = DIContainer.Resolve<ISettingsManager<AreaSettings>>().Get();
                areaLevel = areaSettings.AreaLevel;
            }
            return htmlHelper.LinkageDropDownList<string>(name, value, string.Empty, areaLevel.Value, areaServiceHelper.GetRootAreaDictionary(rootAreaCode), areaServiceHelper.GetParentCode, areaServiceHelper.GetChildrenDictionary, getChildAreasUrl);
        }

        ///// <summary>
        ///// 地区下拉列表
        ///// </summary>
        ///// <param name="htmlHelper">被扩展的htmlHelper实例</param>
        ///// <param name="expression">选择实体中类别属性的lamda表达式</param>
        ///// <param name="areaLevel">地区层级(默认取站点地区配置）</param>
        ///// <param name="rootAreaCode">根级地区（默认取站点地区配置）</param>
        public static HtmlString LN_AreaDropDownListFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, string>> expression, int? areaLevel = null, string rootAreaCode = null)
        {
            string getChildAreasUrl ="GetChildAreas/Mall";
            AreaServiceHelper areaServiceHelper = new AreaServiceHelper();
            if (areaLevel == null)
            {
                AreaSettings areaSettings = DIContainer.Resolve<ISettingsManager<AreaSettings>>().Get();
                areaLevel = areaSettings.AreaLevel;
            }
            return null;
            //return htmlHelper.LN_LinkageDropDownListFor<TModel, string>(expression, string.Empty, areaLevel.Value, areaServiceHelper.GetRootAreaDictionary(rootAreaCode), areaServiceHelper.GetParentCode, areaServiceHelper.GetChildrenDictionary, getChildAreasUrl);
        }

        ///// <summary>
        ///// 地区下拉列表
        ///// </summary>
        ///// <param name="htmlHelper">被扩展的htmlHelper实例</param>
        ///// <param name="name">控件name属性</param>
        ///// <param name="name">选中的地区编码</param>
        ///// <param name="areaLevel">地区层级(默认取站点配置）</param>
        ///// <param name="rootAreaCode">根级地区（默认取站点地区配置）</param>
        public static HtmlString LN_AreaDropDownList(this HtmlHelper htmlHelper, string name, string value, int? areaLevel = null, string rootAreaCode = null)
        {
            string getChildAreasUrl = "GetChildAreas/Mall";
            AreaServiceHelper areaServiceHelper = new AreaServiceHelper();
            if (areaLevel == null)
            {
                AreaSettings areaSettings = DIContainer.Resolve<ISettingsManager<AreaSettings>>().Get();
                areaLevel = areaSettings.AreaLevel;
            }
            return null;
            //return htmlHelper.LN_LinkageDropDownList<string>(name, value, string.Empty, areaLevel.Value, areaServiceHelper.GetRootAreaDictionary(rootAreaCode), areaServiceHelper.GetParentCode, areaServiceHelper.GetChildrenDictionary, getChildAreasUrl);
        }
    }

    /// <summary>
    /// 地区业务逻辑扩展类
    /// </summary>
    internal class AreaServiceHelper
    {
        private AreaService areaService = DIContainer.Resolve<AreaService>();

        /// <summary>
        /// 获取父地区编码
        /// </summary>
        public string GetParentCode(string areaCode)
        {
            Area area = areaService.Get(areaCode);
            if (area != null)
                return area.ParentCode;
            return string.Empty;
        }

        /// <summary>
        /// 获取子地区
        /// </summary>
        public Dictionary<string, string> GetChildrenDictionary(string areaCode)
        {
            Area area = areaService.Get(areaCode);
            if (area != null)
                return area.Children.ToDictionary(n => n.AreaCode, n => n.Name);
            return null;
        }

        /// <summary>
        /// 获取一级地区
        /// </summary>
        /// <param name="rootAreaCode">根级地区（默认取站点地区配置）</param>
        public Dictionary<string, string> GetRootAreaDictionary(string rootAreaCode = null)
        {
            if (rootAreaCode == null)
            {
                ISettingsManager<AreaSettings> areaSettingsManager = DIContainer.Resolve<ISettingsManager<AreaSettings>>();
                if (areaSettingsManager != null)
                {
                    AreaSettings areaSettings = areaSettingsManager.Get();
                    rootAreaCode = areaSettings.RootAreaCode;
                }
            }
            //获取根级地区
            IEnumerable<Area> areas = null;
            if (!string.IsNullOrEmpty(rootAreaCode))
            {
                Area area = areaService.Get(rootAreaCode);
                if (area != null)
                    areas = area.Children;
            }
            else
                areas = areaService.GetRoots();
            if (areas == null)
                return null;

            return areas.ToDictionary(n => n.AreaCode, n => n.Name);
        }
    }
}