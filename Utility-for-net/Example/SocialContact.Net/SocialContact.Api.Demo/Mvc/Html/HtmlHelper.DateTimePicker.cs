//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Tunynet.Common
{
    /// <summary>
    /// 日期时间选择器HtmlHelper
    /// </summary>
    public static class HtmlHelperDateTimePickerExtensions
    {

        /// <summary>
        /// 日期时间选择器
        /// </summary>
        /// <returns></returns>
        public static HtmlString DateTimePicker(this HtmlHelper htmlHelper, string name, DateTime? initDate, Dictionary<string, object> htmlAttributes = null, string dateFormat = "yyyy-MM-dd HH:mm", int weekStart = 0, bool autoClose = true, DateTimeView startView = DateTimeView.Month, DateTimeView minView = DateTimeView.Hour, DateTimeView maxView = DateTimeView.Decade, int minuteStep = 1, string pickerPosition = PickerPositions.ButtomRight)
        {
            return htmlHelper.DateTimePicker(setting =>
             {
                 setting.Name = name;
                 setting.InitDate = initDate;
                 setting.DateFormat = dateFormat;
                 setting.WeekStart = weekStart;
                 setting.AutoClose = autoClose;
                 setting.StartView = startView;
                 setting.MinView = minView;
                 setting.MaxView = maxView;
                 setting.MinuteStep = minuteStep;
                 setting.PickerPosition = pickerPosition;
             });
        }

        /// <summary>
        /// 日期时间选择器
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static HtmlString DateTimePicker(this HtmlHelper htmlHelper, Action<DateTimePickerSetting> method)
        {
            var setting = new DateTimePickerSetting();
            method(setting);

            var dateTimePicker = new DateTimePicker();
            dateTimePicker.CurrentValue = setting.InitDate;
            dateTimePicker.DateFormat = setting.DateFormat;
            dateTimePicker.WeekStart = setting.WeekStart;
            dateTimePicker.AutoClose = setting.AutoClose;
            dateTimePicker.StartView = setting.StartView;
            dateTimePicker.MinView = setting.MinView;
            dateTimePicker.MaxView = setting.MaxView;
            dateTimePicker.MinuteStep = setting.MinuteStep;
            dateTimePicker.PickerPosition = setting.PickerPosition;
            return dateTimePicker.Render(htmlHelper, setting.Name);
        }

        /// <summary>
        /// 日期时间选择器
        /// </summary>
        /// <returns></returns>
        public static HtmlString DateRangePicker(this HtmlHelper htmlHelper, string name, DateTime? startDate = null, DateTime? endDate = null, Dictionary<string, object> htmlAttributes = null, int weekStart = 0, bool autoClose = true, DateTimeView startView = DateTimeView.Month, DateTimeView minView = DateTimeView.Hour, DateTimeView maxView = DateTimeView.Decade, int minuteStep = 1, string pickerPosition = PickerPositions.ButtomRight, bool lazyload = true)
        {
            return htmlHelper.DateRangePicker(setting =>
            {
                setting.Name = name;
                setting.StartDate = startDate;
                setting.EndDate = endDate;
                setting.WeekStart = weekStart;
                setting.AutoClose = autoClose;
                setting.StartView = startView;
                setting.MinView = minView;
                setting.MaxView = maxView;
                setting.MinuteStep = minuteStep;
                setting.PickerPosition = pickerPosition;
                setting.LazyLoad = lazyload;
            });
        }

        /// <summary>
        /// 日期区间选择器
        /// </summary>
        /// <returns></returns>
        public static HtmlString DateRangePicker(this HtmlHelper htmlHelper, Action<DateRangePickerSetting> method)
        {
            var setting = new DateRangePickerSetting();
            method(setting);

            var dateTimePicker = new DateTimePicker();
            dateTimePicker.StartDate = setting.StartDate;
            dateTimePicker.EndDate = setting.EndDate;
            dateTimePicker.WeekStart = setting.WeekStart;
            dateTimePicker.AutoClose = setting.AutoClose;
            dateTimePicker.StartView = setting.StartView;
            dateTimePicker.MinView = setting.MinView;
            dateTimePicker.MaxView = setting.MaxView;
            dateTimePicker.MinuteStep = setting.MinuteStep;
            dateTimePicker.PickerPosition = setting.PickerPosition;
            dateTimePicker.Lazyload = setting.LazyLoad;
            return dateTimePicker.RenderDateRangePicker(htmlHelper, setting.Name);
        }

        /// <summary>
        /// 日期时间选择器
        /// </summary>
        /// <returns></returns>
        public static HtmlString DateTimePickerFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, DateTime>> expression, Dictionary<string, object> htmlAttributes = null, string dateFormat = "YYYY-MM-DD HH:mm", int weekStart = 0, bool autoClose = true, DateTimeView startView = DateTimeView.Month, DateTimeView minView = DateTimeView.Hour, DateTimeView maxView = DateTimeView.Decade, int minuteStep = 1, string pickerPosition = PickerPositions.ButtomRight)
        {
            return htmlHelper.DateTimePickerFor(expression, setting =>
            {
                setting.DateFormat = dateFormat;
                setting.WeekStart = weekStart;
                setting.AutoClose = autoClose;
                setting.StartView = startView;
                setting.MinView = minView;
                setting.MaxView = maxView;
                setting.MinuteStep = minuteStep;
                setting.PickerPosition = pickerPosition;
            });
        }

        /// <summary>
        /// 日期时间选择器
        /// </summary>
        /// <returns></returns>
        public static HtmlString DateTimePickerFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, DateTime>> expression, Action<DateTimePickerSetting> method)
        {
            var setting = new DateTimePickerSetting();
            method(setting);

            var dateTimePicker = new DateTimePicker();

            //ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            //if (metadata.Model != null)
            //{
            //    dateTimePicker.CurrentValue = (DateTime)metadata.Model;
            //}

            //添加数据注解验证
            return null;
            //PropertyInfo propertyInfo = metadata.ContainerType.GetProperty(metadata.PropertyName);
            //RequiredAttribute requiredAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(RequiredAttribute)) as RequiredAttribute;

            //if (requiredAttribute != null && !string.IsNullOrWhiteSpace(requiredAttribute.ErrorMessage))
            //{
            //    dateTimePicker.ErrorMessage = requiredAttribute.ErrorMessage;
            //}

            //dateTimePicker.DateFormat = setting.DateFormat;
            //dateTimePicker.WeekStart = setting.WeekStart;
            //dateTimePicker.AutoClose = setting.AutoClose;
            //dateTimePicker.StartView = setting.StartView;
            //dateTimePicker.MinView = setting.MinView;
            //dateTimePicker.MaxView = setting.MaxView;
            //dateTimePicker.MinuteStep = setting.MinuteStep;
            //dateTimePicker.PickerPosition = setting.PickerPosition;
            //return dateTimePicker.Render(htmlHelper, ExpressionHelper.GetExpressionText(expression));
        }
    }

    /// <summary>
    /// 日期选择器设置
    /// </summary>
    public abstract class IDateTimePickerSetting
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// html属性
        /// </summary>
        public Dictionary<string, object> HtmlAttributes { get; set; } = null;

        /// <summary>
        /// 日期格式
        /// </summary>
        public string DateFormat { get; set; } = "yyyy-MM-dd HH:mm";

        /// <summary>
        ///  一周从哪一天开始 默认(0:星期日)
        /// </summary>
        public int WeekStart { get; set; } = 0;

        /// <summary>
        ///  当选择一个日期之后是否立即关闭日期时间选择器  
        /// </summary>
        public bool AutoClose { get; set; } = true;

        /// <summary>
        /// 日期时间选择器打开之后首先显示的视图
        /// </summary>
        public DateTimeView StartView { get; set; } = DateTimeView.Month;

        /// <summary>
        /// 最小视图
        /// </summary>
        public DateTimeView MinView { get; set; } = DateTimeView.Hour;

        /// <summary>
        /// 最大视图
        /// </summary>
        public DateTimeView MaxView { get; set; } = DateTimeView.Decade;

        /// <summary>
        /// 小时视图步进值
        /// </summary>
        public int MinuteStep { get; set; } = 1;

        /// <summary>
        /// 选择器位置 
        /// </summary>
        public string PickerPosition { get; set; } = PickerPositions.ButtomRight;
    }

    /// <summary>
    /// 日期时间选择器设置
    /// </summary>
    public class DateTimePickerSetting : IDateTimePickerSetting
    {
        /// <summary>
        /// 初始日期
        /// </summary>
        public DateTime? InitDate { get; set; }
    }

    /// <summary>
    /// 日期区间选择器设置
    /// </summary>
    public class DateRangePickerSetting : IDateTimePickerSetting
    {
        /// <summary>
        /// 起始时间 初始值
        /// </summary>
        public DateTime? StartDate { get; set; } = null;

        /// <summary>
        /// 结束时间 初始值
        /// </summary>
        public DateTime? EndDate { get; set; } = null;

        /// <summary>
        /// 是否为懒加载
        /// </summary>
        public bool LazyLoad { get; set; } = true;
    }
}