+ function (factory) {
  if (typeof define === "function" && define.amd) {
    require(['jquery', 'daterangepickerjs', 'livequery'], factory);
  } else if (typeof module === "object" && module.exports) {
    module.exports = factory(require('jquery'), require('daterangepicker'));
  } else {
    factory(jQuery);
  }
}(function ($) {
  $('[data-plugin=daterangepicker]').livequery(function () {
    var $this = $(this);
    var daterangepicker = $('#'+$this.attr('id')+'.date_range');
  
    var dateFormat = $this.data('date-format')|| 'YYYY-MM-DD'; // 日期格式
    var startdate = $this.data('date-startdate'); // 开始日期
    var enddate = $this.data('date-enddate'); // 结束日期
    var pickerPosition = $this.data('picker-position') || 'bottom-left'; // 选择器位置
    //var lazyload = !!$this.data('lazyload'); // 是否延迟加载
    var lazyload = (startdate === undefined || enddate === undefined) ? true : false; // 是否延迟加载

    var posConfig = pickerPosition.split('-');
    var drops;
    var opens;
    if (posConfig[0] === 'top') drops = 'up';
    if (posConfig[1] === 'left') opens = 'left';
    if (lazyload) {
      $this.on('click', function(e) {
        $this.off('click');
        init();
        $this.siblings('div.input-group-addon').find('.fa-calendar').parent().click();
      });
    } else {
      init();
    }
    function init() {
      daterangepicker.daterangepicker({
        "startDate": startdate,
        "endDate": enddate,
        "autoApply": true,
        "linkedCalendars": false,
        "opens": opens,
        "drops": drops,
        "locale": {
          "direction": "ltr",
          "format": dateFormat,
          "separator": " - ",
          "applyLabel": "确认",
          "cancelLabel": "取消",
          "fromLabel": "从",
          "toLabel": "到",
          "customRangeLabel": "自定义",
          "daysOfWeek": [
            "日",
            "一",
            "二",
            "三",
            "四",
            "五",
            "六"
          ],
          "monthNames": [
            "一月",
            "二月",
            "三月",
            "四月",
            "五月",
            "六月",
            "七月",
            "八月",
            "九月",
            "十月",
            "十一月",
            "十二月"
          ],
          "firstDay": 1
        },
      }, function (start, end, label) {
        // console.log('New date range selected: ' + start.format('YYYY-MM-DD HH:mm:ss') + ' to ' + end.format('YYYY-MM-DD HH:mm:ss') + ' (predefined range: ' + label + ')');
      });
      daterangepicker.siblings('div.input-group-addon').find('.fa-times').parent().click(function () {
        daterangepicker.val('');
      });
      daterangepicker.siblings('div.input-group-addon').find('.fa-calendar').parent().click(function () {
        daterangepicker.click();
      });
    }
  });
});