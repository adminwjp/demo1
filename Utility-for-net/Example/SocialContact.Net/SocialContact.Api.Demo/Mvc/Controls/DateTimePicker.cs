//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;

namespace Tunynet.Common
{
    /// <summary>
    /// 日期时间选择器
    /// </summary>
    /// <remarks>
    /// <list type="number">
    /// <item>http://jqueryui.com/demos/datepicker/</item>
    /// <item>http://trentrichardson.com/examples/timepicker/</item>
    /// </list>
    /// <para>如果需要更多option设置，可以通过设置AdditionalParameters属性来实现</para>
    /// <para>依赖文件：</para>
    /// <list type="number">
    /// <item>jquery-ui.js</item>
    /// <item>jquery-ui-timepicker-addon.js（V0.9.7）</item>
    /// <item>jquery.ui.datepicker-zh-CN.js</item>
    /// </list>
    /// </remarks>
    public class DateTimePicker
    {

        /// <summary>
        /// 构造器
        /// </summary>
        /// <remarks>在构造器中为属性赋默认值</remarks>
        public DateTimePicker()
        {
            this.CurrentValue = null;
            this.StartDate = null;
            this.EndDate = null;
            this.DateFormat = "yyyy-MM-dd HH:mm";
            this.WeekStart = 0;
            this.AutoClose = true;
            this.StartView = DateTimeView.Month;
            this.MinView = DateTimeView.Hour;
            this.MaxView = DateTimeView.Decade;
            this.MinuteStep = 1;
            this.PickerPosition = PickerPositions.ButtomRight;
        }

        /// <summary>
        /// 默认日期 
        /// </summary>
        /// <returns></returns>
        public DateTime? CurrentValue { get; set; }

        /// <summary>
        /// 开始日期 
        /// </summary>
        /// <returns></returns>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束日期 
        /// </summary>
        /// <returns></returns>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 日期格式 (区间选择器无法自定义此参数) 此处参数为C#中的日期时间格式
        /// </summary>
        /// <example>"yyyy-MM-dd HH:mm"</example>
        /// <value>"yyyy-MM-dd HH:mm"</value>
        /// <returns></returns>
        /// var dateFormat = $this.data('date-format').replace(/m/g, 'i').toLowerCase() || 'yyyy-mm-dd hh:ii'; // 日期格式
        public string DateFormat { get; set; }

        /// <summary>
        /// 一周从哪一天开始 默认(0:星期日)
        /// </summary>
        /// <returns></returns>
        /// var weekStart = +$this.data('date-weekstart') || 0; // 一周从哪一天开始。0（星期日）到6（星期六）
        public int WeekStart { get; set; }

        /// <summary>
        /// 当选择一个日期之后是否立即关闭日期时间选择器 
        /// </summary>
        /// <returns></returns>
        /// var autoclose = ('' + $this.data('date-autoclose') === 'false') || true; // 当选择一个日期之后是否立即关闭此日期时间选择器
        public bool AutoClose { get; set; }

        /// <summary>
        /// 日期时间选择器打开之后首先显示的视图 0-4:hour,day,month,year,decade 默认 month
        /// </summary>
        /// <returns></returns>
        /// var startView = $this.data('start-view') || 'month'; // // 日期时间选择器打开之后首先显示的视图 0-4:hour,day,month,year,decade
        public DateTimeView StartView { get; set; }

        /// <summary>
        /// 日期时间选择器所能够提供的最精确的时间选择视图 默认 hour
        /// </summary>
        /// <returns></returns>
        /// var minView = $this.data('min-view') || 'hour'; 
        public DateTimeView MinView { get; set; }

        /// <summary>
        /// 日期时间选择器最高能展示的选择范围视图 默认 decade
        /// </summary>
        /// <remark>最大设置为hour时为小时选择器</remark>
        /// <returns></returns>
        /// var maxView = $this.data('max-view') || 'decade'; 
        public DateTimeView MaxView { get; set; }

        /// <summary>
        /// 小时视图步进值
        /// </summary>
        /// <returns></returns>
        /// var minuteStep = +$this.data('minute-step') || 1; // 小时视图步进值
        public int MinuteStep { get; set; }

        /// <summary>
        /// 选择器位置 
        /// </summary>
        /// <returns></returns>
        /// var pickerPosition = $this.data('picker-position') || 'bottom-left'; // 选择器位置
        public string PickerPosition { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <returns></returns>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 文本框的html属性集合
        /// </summary>
        private Dictionary<string, object> HtmlAttributes { get; set; }

        /// <summary>
        /// 是否等待加载
        /// </summary>
        /// <returns></returns>
        public bool Lazyload { get; set; }

        /// <summary>
        /// 默认时间值 (处理后的)
        /// </summary>
        private string InitialDate
        {
            get
            {
                if (CurrentValue == null || CurrentValue == DateTime.MinValue)
                {
                    return string.Empty;
                }
                if (MinView == DateTimeView.Hour)
                {
                    return CurrentValue.Value.ToString(this.DateFormat + " HH:mm");
                }
                return CurrentValue.Value.ToString(this.DateFormat);
            }
        }


        #region 方法

        /// <summary>
        /// 添加html属性
        /// </summary>
        /// <param name="attributeName">属性名</param>
        /// <param name="attributeValue">属性值</param>
        /// <remarks>如果存在，则覆盖</remarks>
        public DateTimePicker MergeHtmlAttribute(string attributeName, object attributeValue)
        {
            if (this.HtmlAttributes == null)
                this.HtmlAttributes = new Dictionary<string, object>();
            this.HtmlAttributes[attributeName] = attributeValue;
            return this;
        }

        #endregion 方法

        /// <summary>
        /// 转为Html属性集合
        /// </summary>
        public IDictionary<string, object> ToHtmlAttributes(bool isDateRangePicker)
        {
            var attrs = new Dictionary<string, object>();

            if (HtmlAttributes != null)
            {
                attrs = new Dictionary<string, object>(HtmlAttributes);
            }
            if (isDateRangePicker)
            {
                //因为DateRangePicker使用了moment.js进行日期时间格式化，无法应用C#的dateFormat，所以此处写死
                DateFormat = "YYYY-MM-DD";
            }

            attrs.TryAdd("data-date-format", DateFormat);
            attrs.TryAdd("data-date-weekstart", WeekStart);
            attrs.TryAdd("data-date-autoclose", AutoClose);
            attrs.TryAdd("data-start-view", StartView.ToString().ToLower());
            attrs.TryAdd("data-min-view", MinView.ToString().ToLower());
            attrs.TryAdd("data-max-view", MaxView.ToString().ToLower());
            attrs.TryAdd("data-minute-step", MinuteStep);
            attrs.TryAdd("data-picker-position", PickerPosition);
            if (CurrentValue.HasValue)
            {
                attrs.TryAdd("value", CurrentValue.Value.ToString(DateFormat));
            }

            if (StartDate.HasValue)
            {
                attrs.TryAdd("data-date-startdate", StartDate.Value.ToString(DateFormat));
            }

            if (EndDate.HasValue)
            {
                attrs.TryAdd("data-date-enddate", EndDate.Value.ToString(DateFormat));
            }

            if (!string.IsNullOrWhiteSpace(ErrorMessage))
            {
                attrs["data-msg-required"] = ErrorMessage;
                attrs["data-rule-required"] = "true";
            }

            return attrs;
        }

        /// <summary>
        /// 呈现控件
        /// </summary>
        public virtual HtmlString Render(HtmlHelper htmlHelper, string name)
        {

            //日期时间选择器 input
            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("class", "form-control datepickertime date");
            input.MergeAttribute("type", "text");
            input.MergeAttribute("placeholder", "请选择日期时间");
            input.MergeAttribute("id", name);
            input.MergeAttribute("name", name);

            //取消和日历图标
            TagBuilder iCancel = new TagBuilder("div");
            iCancel.MergeAttribute("class", "fa fa-times");
            TagBuilder iCalendar = new TagBuilder("div");
            iCalendar.MergeAttribute("class", "fa fa-calendar");

            //取消和日历图标,外层div
            TagBuilder divCancel = new TagBuilder("div");
            divCancel.MergeAttribute("class", "input-group-addon");
            //divCancel.InnerHtml = iCancel.ToString();

            TagBuilder divCalendar = new TagBuilder("div");
            divCalendar.MergeAttribute("class", "input-group-addon");
            //divCalendar.InnerHtml = iCalendar.ToString();

            IDictionary<string, object> attrs = this.ToHtmlAttributes(false);
            if (attrs != null)
            {
                foreach (var attr in attrs)
                {
                    input.MergeAttribute(attr.Key, attr.Value.ToString());
                }
            }
            return new HtmlString(input.ToString() + divCancel.ToString() + divCalendar.ToString());
        }

        /// <summary>
        /// 呈现控件
        /// </summary>
        public virtual HtmlString RenderDateRangePicker(HtmlHelper htmlHelper, string name)
        {
            //日期时间选择器 input
            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("class", "form-control date_range");
            input.MergeAttribute("type", "text");
            input.MergeAttribute("placeholder", "请选择日期区间");
            input.MergeAttribute("id", name);
            input.MergeAttribute("name", name);
            input.MergeAttribute("data-plugin", "daterangepicker");
            input.MergeAttribute("data-lazyload", this.Lazyload.ToString().ToLower());

            //取消和日历图标
            TagBuilder iCancel = new TagBuilder("div");
            iCancel.MergeAttribute("class", "fa fa-times");
            TagBuilder iCalendar = new TagBuilder("div");
            iCalendar.MergeAttribute("class", "fa fa-calendar");

            //取消和日历图标,外层div
            TagBuilder divCancel = new TagBuilder("div");
            divCancel.MergeAttribute("class", "input-group-addon");

            //divCancel.InnerHtml = iCancel.ToString();

            TagBuilder divCalendar = new TagBuilder("div");
            divCalendar.MergeAttribute("class", "input-group-addon");
           // divCalendar.InnerHtml = iCalendar.ToString();

            IDictionary<string, object> attrs = this.ToHtmlAttributes(true);
            if (attrs != null)
            {
                foreach (var attr in attrs)
                {
                    input.MergeAttribute(attr.Key, attr.Value.ToString());
                }
            }
            return new HtmlString(input.ToString() + divCancel.ToString() + divCalendar.ToString());
        }

        /// <summary>
        /// 转换为客户端可用的日期格式
        /// </summary>
        /// <param name="dateFormat">服务器端的日期格式</param>
        /// <returns>客户端的日期格式</returns>
        private static string ConvertToClientDateFormat(string dateFormat)
        {
            //译码表
            //客户端编码|服务器端编码|含义
            //d --------|d-----------|天，不足两位不会在前面补零，比如：1
            //dd--------|dd----------|天，不足两位需要在前面补零，比如：01
            //D---------|ddd---------|一周的第几天，简写形式，比如：周一
            //DD--------|dddd--------|一周的第几天，全写形式，比如：星期一
            //m---------|M-----------|月，不足两位不会在前面补零，比如：1
            //mm--------|MM----------|月，不足两位需要在前面补零，比如：01
            //M---------|MMM---------|一年的第几个月，简写形式，比如：十二
            //MM--------|MMMM--------|一年的第几个月，全写形式，比如：十二月
            //y---------|yy----------|天，不足两位不会在前面补零，比如：1
            //yy--------|yyyy--------|天，不足两位需要在前面补零，比如：01
            string result = dateFormat;
            //替换天
            if (result.Contains("dddd"))
                result = result.Replace("dddd", "DD");
            else if (result.Contains("ddd"))
                result = result.Replace("ddd", "D");
            //替换月
            if (result.Contains("MMMM"))
                result = result.Replace("MMMM", "MM");
            else if (result.Contains("MMM"))
                result = result.Replace("MMM", "M");
            else if (result.Contains("MM"))
                result = result.Replace("MM", "mm");
            else if (result.Contains("M"))
                result = result.Replace("M", "m");
            //替换年
            if (result.Contains("yyyy"))
                result = result.Replace("yyyy", "yy");
            else if (result.Contains("yy"))
                result = result.Replace("yy", "y");
            return result;
        }

        /// <summary>
        /// 转换为客户端可用的时间格式
        /// </summary>
        /// <param name="timeFormat">服务器端的时间格式</param>
        /// <param name="ampm">输出参数，是否为12小时制</param>
        /// <returns>客户端的时间格式</returns>
        private static string ConvertToClientTimeFormat(string timeFormat, out bool ampm)
        {
            //译码表
            //客户端编码|服务器端编码|含义
            //h --------|h-----------|时，不足两位不会在前面补零，比如：1
            //hh--------|hh----------|时，不足两位需要在前面补零，比如：01

            //m --------|m-----------|分，不足两位不会在前面补零，比如：1
            //mm--------|mm----------|分，不足两位需要在前面补零，比如：01

            //s---------|s-----------|秒，不足两位不会在前面补零，比如：1
            //ss--------|ss----------|秒，不足两位需要在前面补零，比如：01

            //t---------|t-----------|12小时制的简写形式，比如：上
            //tt--------|tt----------|12小时制的全写形式，比如：上午

            ampm = true;
            if (timeFormat.Contains("H"))
                ampm = false;

            string result = timeFormat;
            //替换时
            if (result.Contains("HH"))
                result = result.Replace("HH", "hh");
            else if (result.Contains("H"))
                result = result.Replace("H", "h");
            return result;
        }
    }

    /// <summary>
    /// 日期时间View分类 用户 StartView MinView 和 MaxView
    /// </summary>
    public enum DateTimeView
    {

        /// <summary>
        /// 小时
        /// </summary>
        Hour = 0,

        /// <summary>
        /// 天
        /// </summary>
        Day = 1,

        /// <summary>
        /// 月
        /// </summary>
        Month = 2,

        /// <summary>
        /// 年
        /// </summary>
        Year = 3,

        /// <summary>
        /// 十年
        /// </summary>
        Decade = 4
    }

    /// <summary>
    /// 日期时间选择器弹出位置
    /// </summary>
    public static class PickerPositions
    {

        /// <summary>
        /// 左下
        /// </summary>
        public const string ButtomLeft = "bottom-left";

        /// <summary>
        /// 右下
        /// </summary>
        public const string ButtomRight = "bottom-right";

        /// <summary>
        /// 左上
        /// </summary>
        public const string TopLeft = "top-left";

        /// <summary>
        /// 右上
        /// </summary>
        public const string TopRight = "top-right";

    }

}