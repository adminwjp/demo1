+ function (factory) {
  if (typeof define === "function" && define.amd) {
    require(['jquery', 'livequery', 'datetimepicker', 'datetimepicker.zh-CN'], factory);
  } else if (typeof module === "object" && module.exports) {
    module.exports = factory(require('jquery'), require('livequery'), require('datetimepicker'), require('datetimepicker.zh-CN'));
  } else {
    factory(jQuery);
  }
}(function ($) {
  $('.form_time, input.datepickertime').livequery(function() {
    var $this = $(this);
    // var value; // 初始值通过直接设置输入框value来指定
    var dateFormat = $this.data('date-format').replace(/m/g, 'i').toLowerCase() || 'yyyy-mm-dd hh:ii'; // 日期格式
    var weekStart = +$this.data('date-weekstart') || 0; // 一周从哪一天开始。0（星期日）到6（星期六）
    var autoclose = ('' + $this.data('date-autoclose') === 'false') || true; // 当选择一个日期之后是否立即关闭此日期时间选择器
    var startView = $this.data('start-view') || 'month'; // // 日期时间选择器打开之后首先显示的视图 0-4:hour,day,month,year,decade
    var minView = $this.data('min-view') || 'hour'; // 日期时间选择器所能够提供的最精确的时间选择视图
    var maxView = $this.data('max-view') || 'decade'; // 日期时间选择器最高能展示的选择范围视图
    var minuteStep = +$this.data('minute-step') || 1; // 小时视图步进值
    var pickerPosition = $this.data('picker-position') || 'bottom-left'; // 选择器位置
    $this.datetimepicker({
      format: dateFormat,
      weekStart: weekStart,
      // startDate: '2013-02-14 10:00', // {Date|string}=(new Date(-8639968443048000)) 可选择的最早时间
      // endDate: '2030-02-14 10:00', // {Date|string}=(new Date(8639968443048000)) 可选择的最晚时间
      // daysOfWeekDisabled: [0, 6], // {number[]|string}=[] 禁止选择的星期
      autoclose: autoclose, 
      startView: startView,
      minView: minView,
      maxView: maxView, 
      todayBtn: true, // {boolean}=false 显示选择当前时间按钮
      todayHighlight: true, // {boolean}=false 是否高亮当前日期
      keyboardNavigation: true, // {boolean} 是否允许通过方向键改变日期
      language: 'zh-CN', // {string}='en' 语言
      forceParse: true, // {boolean}=true 是否强制解析输入值
      minuteStep: minuteStep, 
      pickerPosition: pickerPosition,
      viewSelect: 'decade', // {number|string}={minView} 最低选择精度，从该精度开始每次选择都会更新输入框值
      showMeridian: false, // {boolean}=false day和hour选择器视图按上午/下午分开
      // initialDate: new Date(), // {Date|string}=(new Date()) 初始日期
    });
    if ($this.is('input')) {
      $this.siblings().find('.fa-times').parent().off('click').on('click', function() { $this.val(''); });
      $this.siblings().find('.fa-calendar').parent().off('click').on('click', function() { $this.focus(); });
    }
  });
  
});