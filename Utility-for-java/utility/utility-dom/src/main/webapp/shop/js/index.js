var javascriptType = {
  text: "text/javascript"
};
const events = [
  //UI事件
  "DOMActivate", //元素已经被用户操作(鼠标或键盘)激活 dom3 废弃 >=firefox2 chrome支持 不建议使用
  "load", //页面完全加载后window触发 当所有框架都加载完毕再框架上面触发 当图像加载完毕时 img上触发 当嵌入内容加载完毕时在<object>元素上面触发
  "unload", //同理 完全卸载
  "abort", //用户停止下载过程 嵌入内容没加载完 则在<object>元素上触发
  "error", //当发生js发生错误 window上触发 无法加载图像 无法加载嵌入内容 当一个或多个框架无法加载在框架集上触发
  "select", //当用户选择文本框 input texterea 一个或多个字符触发
  "resize", //当窗口或框架大小变化时在window 或frame上触发
  "scroll", //当用户滚动带滚动条的那日苏时 在该元素上触发
  //dom2 中这些事件属于html事件 DOMActivate dom2中扔处于ui事件
  //html5 事件
  "contextmenu", // 右键上下文事件
  "beforeunload", //页面卸载前阻止这一操作 浏览器卸载页面之前触发
  "DOMContentLoaded", //形成完整的dom数之后触发 不理会css js img 其他资源十分下载完毕
  "readystatechange",
  "pageshow",
  "pagehide",
  "hashchange",
  //设备事件
  "orienttationchange", //横向模式切换纵向模式
  "MozOrientation", //window 对象触发的 firefox3.6 引入 一个方向平面 移动
  "deviceorientation", //window 对象触发的 空间朝向哪
  "devicemotion", //什么时候一到 不是哪个方向
  //触屏与手势事件
  //触摸
  //事件发生顺序 touuchstart - touchmover - touchmove - mousedown
  //-mouseup -click-touched
  "touuchstart", //当手指触屏触发
  "touchmove", //连续地触发
  "touchend", //移开时触发
  "touchcancel", //当系统停止跟踪触摸时触发
  "touches", //当前跟踪的触摸操作Touch对象的数组
  "targetTouchs", //特定于事件目标的Touch对象数组
  "changeTouches", //自上次触摸以来发生什么改变Touch对象数组
  //手势
  "gesturestart", //当一个手指已经按在屏幕上 另一个手指又触屏时触发
  "gesturechange", //当触屏任何一个位置发生变化时触发
  "gestureend" //当一个手指从屏幕移开时触发
];
/**
 * 注意:将页面的MIME类型指定为"application/xhmtl+xml"情况下回触发XHTML模式,并不是所有浏览器都支持方式提供XHTML文档
 *script:\现实中不一定会按顺序价值,也不一定在DOMContentLoaded事件触发前执行,因此最好只包含一个延迟脚本
 * defer 属性只适用于外部脚本 html5规定的,因此支持html5的实现会忽略嵌入脚本的设置的Defer属性
 *ie4-ie7还支持对嵌入脚本的defer属性 >ie8之后则完全支持html5规定的行为
 ie4、firefox3.5、safari5、chrome最早支持defer属性的浏览器
 *注意:在xhtml中要把defer属性设置为 defer="defer"
 *async  异步脚本一定会在load事件之前执行 ,但可能会 DOMContentLoaded事件触发前执行火触发之后执行
 *firefox3.6、safari5、chrome
 *注意：xhtml中要把async属性设置为 async="async"
 *异步脚本,只适用于外部脚本 并不保证加载先后顺序
 *  /
// //注意：ECMAScript5引入了严格模式 >ie10、> firefox4、>safari5.1、>opera12、chrome
// class Gibson {
//     constructor() {}
// }
/**数据操作公共类 */
const dataUtils = {
  /**
   * @param val boolean、非空字符串、非0数值(0、nan false)、object(null false),n/a(undeined flase)等
   * 注意：if 情况下会自动调用 boolean 转换
   * @returns 返回 true 或 false
   */
  parseBoolean: function (val) {
    return Boolean(val);
  },
  /**
   * @param val 八进制数据 0开头数据 其他数字0-7否则以10进制数字
   * @returns 是否是八进制  返回 true false
   */
  eightNumber: function (val) {
    let str = val.toString();
    if (str.length > 1) {
      if (str[0] != "0") return false;
      for (const key in str) {
        let num = Number(str[key]);
        if (num >= 0 && num <= 7) {
          continue;
        }
        return false;
      }
      return true;
    }
    return false;
  },
  /**
   * @param val 16进制数据 0x开头数据 其他数字0-9 A-F或a-f
   * @returns 是否是16进制  返回 true false
   */
  tenSixNumber: function (val) {
    let str = val.toString();
    if (Number(val) && str.length > 2 && str.subStr(0, 2) == "0x") return true;
    return false;
  },
  /**
   * @param val 数字是否超出范围 min:5e-324 max:1.7976931348623157e+308
   * 超出范围 + 则 Infinity -则 -Infinity
   * @returns 返回 true false
   */
  IsNumberFinite: function (val) {
    //return val <Number.MIN_VALUE&&val>Number.MAX_VALUE;
    return isFinite(val);
  },
  /**
   * @param val 非数值 NaN==NaN  false(NaN与任何值不相等) NaN:true 10:false
   * "10": false ture:false
   * @returns 返回  true、false
   */
  isNan: function (val) {
    return isNaN(val);
  },
  /***
   * @param val boolean : true 1 false 0,null 0,undefined NaN,
   * string: "" 0 有数字则转换为数字(16进制则转换为10进制)否则 NaN
   * @returns 返回 数字或 NaN
   */
  parseNumber: function (val) {
    return Number(val);
  },
  /***
   * @param val ECMAScript3 或 ECMAScript5 解析字符串存在歧义
   * "":NaN 8进制 ECMAScript3识别为8进制 ECMAScript5 识别为10进制 parseInt()2个参数消除歧义
   * @param num  转换的进制数 10 8 16
   * @returns 返回结果 数字或NaN
   */
  parseIntV1: function (val, num) {
    return num ? parseInt(val, num) : parseInt(val);
  }
};
/**插件公共类 */
const pluginUtils = {
  /**检测插件 对应非ie 浏览器 可以使用plugins 数组达到这个目的
   * 每一项包含属性 ： name 插件的名称 description 插件描述 filename 插件文件名 length 插件所出口的MIME的类型数量
   * ie  new ActiveXObject 检测
   */
  hasPlugin: function (name, ieName) {
    if (navigator.plugins) {
      name = name.toLowerCase();
      for (const key in navigator.plugins) {
        var plugin = navigator.plugins[key];
        if (plugin.name.toLowerCase().indexOf(name) > -1) return true;
      }
      return false;
    } else {
      try {
        new ActiveXObject(ieName);
        return true;
      } catch (error) {
        return false;
      }
    }
  },
  /**用于刷新plugins 以反映最新安装的插件 true 则会重新加载包含插件的所有页面 否则只更新plugins集合,不重新加载页面 */
  refreshPlugin: function (update) {
    if (navigator.plugins) {
      navigator.plugins.refresh(update);
    }
  },
  /**注册处理程序 firefox2 支持 html5支持 接收3个参数 媒体类型 url 应用程序名称
   * <=firefox4 只允许使用MIME 3个类型 application/rss+xml application/atom+xml applicaiton/wnd.mozilla.maybe. feed
   */

  registerContentHandler: function (options) {
    if (navigator.registerContentHandler) {
      for (const i in options) {
        navigator.registerContentHandler(
          options[i].type,
          options[i].url,
          options[i].name
        );
      }
    }
  },
  /**注册处理程序 firefox2 实现不支持 firefox3 完整实现 可以使用 html5支持 接收3个参数 协议类型 url 应用程序名称
   * <=firefox4 只允许使用MIME 3个类型 application/rss+xml application/atom+xml applicaiton/wnd.mozilla.maybe. feed
   */
  registerProtocolHandler: function (options) {
    if (navigator.registerProtocolHandler) {
      for (const i in options) {
        navigator.registerProtocolHandler(
          options[i].protocol,
          options[i].url,
          options[i].name
        );
      }
    }
  },
  hasFlash: function () {
    return this.hasPlugin("Flash", "ShockwaveFlash.ShockwaveFlash");
  },
  hasQuickTime: function () {
    return this.hasPlugin("QuickTime", "QuickTime.QuickTime");
  }
};
/***
 * 制表符 字符
 */
const eacapeChar = {
  /**换行 */
  n: "\n",
  /**制表 */
  t: "\t",
  /**退格 */
  b: "\b",
  /**回车 */
  r: "\r",
  /**进纸 */
  f: "\f",
  /**斜杠 */
  _: "\\",
  /**单引号 */
  singn: "'",
  /**双引号 */
  double: '"',
  /**16进制代码nn表示一个字符(其中n为0-F)例如:\x41 表示A */
  nn: "\\xnn",
  /**16进制代码nnn表示Unicode字符(其中n为0-F)例如\u03a3表示希腊字符 */
  nnn: "\\unnn"
};
/***
 * 数据类型判断公共类 typeof
 */
const typeUtils = {
  /**
   * @param val undefined或未定义(非严格模式下 不存在该变量) undefined 派生于null undefined==null true
   * undefined===null false 不会转换数据类型
   * @returns 返回 true 或 false
   */
  isUndefined: function (val) {
    return typeof val === "undefined";
  },
  /**
   * @param val true、false
   * @returns 返回 true 或 false
   */
  isBoolean: function (val) {
    return typeof val === "boolean";
  },
  /**
   * @param val 字符串
   * @returns 返回 true 或 false
   */
  isString: function (val) {
    return typeof val === "string";
  },
  /**
   * @param val 数字
   * @returns 返回 true 或 false
   */
  isNumber: function (val) {
    return typeof val === "number";
  },
  /**
   * @param val 对象或null
   * 注意：null <safari5、<chrome7 会返回function 其他浏览器则object
   * @returns 返回 true 或 false
   */
  isObject: function (val) {
    return typeof val === "object";
  },
  /**
   * @param val function对象或null
   * 注意：null <safari5、<chrome7 会返回function 其他浏览器则object
   * @returns 返回 true 或 false
   */
  isFunction: function (val) {
    return typeof val === "function";
  }
};
/**数据类型判断公共类 prototype */
const prototypeUtils = {
  /**
   * @param val undefined或未定义(非严格模式下 不存在该变量) undefined 派生于null undefined==null true
   * undefined===null false 不会转换数据类型
   * @returns 返回 true 或 false
   */
  isUndefined: function (val) {
    return prototype.call(val) === "[object Undefined]";
  },
  /**
   * @param val true、false
   * @returns 返回 true 或 false
   */
  isBoolean: function (val) {
    return prototype.call(val) === "[object Boolean]";
  },
  /**
   * @param val 字符串
   * @returns 返回 true 或 false
   */
  isString: function (val) {
    return prototype.call(val) === "[object String]";
  },
  /**
   * @param val 数字
   * @returns 返回 true 或 false
   */
  isNumber: function (val) {
    return prototype.call(val) === "[object Number]";
  },
  /**
   * @param val 对象或null
   * 注意：null <safari5、<chrome7 会返回function 其他浏览器则object
   * @returns 返回 true 或 false
   */
  isObject: function (val) {
    return prototype.call(val) === "[object Object]";
  },
  /**
   * @param val function对象或null
   * 注意：null <safari5、<chrome7 会返回function 其他浏览器则object
   * @returns 返回 true 或 false
   */
  isFunction: function (val) {
    return prototype.call(val) === "[object Function]";
  },
  isDate: function (val) {
    return prototype.call(val) === "[object Date]";
  },
  isArray: function (val) {
    return prototype.call(val) === "[object Array]";
  },
  isRegexp: function (val) {
    return prototype.call(val) === "[object RegExp]";
  }
};

/***
 * @param regex   正则表达式 typeof <safari5 <chrome7 返回function
 * ECMA-262规定任何内部实现[Call]方法的对象应该在typeof 操作符返回function.由于上述浏览器实现了该方法
 * ie、firefox 返回object
 */
function isRegex(regex) {
  return regex instanceof RegExp;
}
/** 手动调用垃圾收集*/
function collect() {
  if (window.collectGarbage) {
    window.collectGarbage(); //ie
  }
  if (window.opera && window.opera.collect) {
    window.opera.collect(); //>opera7
  }
}
/**
 * 注意ECMAScript262 中对象不一定适用于javascript其他对象.
 * 浏览器中对象,比如BOM、Dom对象 都属性宿主对象 它们将宿主提供和实现
 * 用于保存创建当前对象的控制器
 */
var constructor = Object.constructor;
/**用于检查给定的属性在当前对象实例中是否存在(而不是实例的原型中) */
var hasProperty = Object.hasOwnProperty;
/**用于检查传入的对象是否是当前对象的原型 */
var isPrototypeOf = Object.isPrototypeOf;
var prototype = Object.prototype.toString;
/**用于给定的属性是否能够使用for-in语句枚举 */
var prototypeIsEnumerable = Object.prototypeIsEnumerable;
// Gibson.prototype.createScriptTag = createScriptTag;
// Gibson.prototype.javascriptType = javascriptType;

/**
 * 栈 后进先出 末端添加返回长度或移除返回该项
 */
var stack = {
  data: [],
  push: function (item) {
    return this.data.push(item);
  },
  pop: function () {
    return this.data.pop();
  }
};
/**
 * 队列 先进先出 末端添加返回长度 起端移除返回该项
 */
var queue = {
  data: [],
  push: function (item) {
    return this.data.push(item);
  },
  shift: function () {
    return this.data.shift();
  },
  unshift: function (item) {
    return this.data.unshift(item);
  }
};
/**
 * 数组公共操作类
 */
const arrayUtils = {
  /***
   * @param array <ie8 之前 [1,2,] 3其他浏览器返回2
   * @returns 返回数据长度
   */
  arrayLength: function (array) {
    return array.length;
  },
  /***
   * @param ECMAScript5 支持检查 Array.isArray 网页包含多个框架 导致可能存在不同Array构造函数
   * @returns 返回 true false
   */
  isArray: function (array) {
    if (Array.isArray) {
      return Array.isArray(array);
    }
    return array instanceof Array;
  },
  /***
   * @param 数组排序
   */
  sort: function (array, compare) {
    if (compare) {
      array.sort(compare);
    } else {
      array.sort();
    }
  },
  /***
   * @param 数组反转
   */
  reverse: function (array) {
    return array.reverse();
  },
  /***
   * @param num1 第一个数
   * @param num2 第2个数
   * @returns 比较 相等 0 第一个数小于第二个数 -1 否则 1
   */
  numberCompare: function (num1, num2) {
    // if(num1<num2){
    //     return -1;
    // }
    // else if(num1>num2){
    //     return 1;
    // }
    // else{
    //     return 0;
    // }
    return num1 - num2;
  },
  /**
   *
   * @param {*} array 数组
   * @param {*} item 单项或数组
   * @returns 创建一个新数组 创建当前数组的副本
   */
  concat: function (array, item) {
    return array.concat(item);
  },
  /**
   * @param array 数组
   * @param {*} start >-1 例如 [1,2,3] 1 [2,3]
   * @param {*} end start>-1 例如 [1,2,3,4] 1,3 [2,3]
   * @returns 创建一个新数组 复制数据 负数 +arrar.length [1,2] -1即1
   */
  create: function (array, start, end) {
    if (end) return array.slice(start, end);
    else return array.slice(start);
  },
  /**
   *
   * @param {*} array 数组
   * @param {*} start 起始位置
   * @param {*} num  删除数量
   * @returns 返回删除的项
   */
  delete: function (array, start, num) {
    return array.splice(start, num);
  },
  /**
   *
   * @param {*} array 数组
   * @param {*} start 位置
   * @param {*} delelteNum 删除数量
   * @param {*} item 添加项
   * @returns 返回删除项 数组 deleteNum= 0 返回空数组 添加项可以添加多个
   */
  addAndDelete: function (array, start, delelteNum, item) {
    return array.splice(start, delelteNum, item);
  },
  //ECMAScript5 的方法
  indexOf: function (array, item) {
    return array.indexOf(item);
  },
  lastIndexOf: function (array, item) {
    return array.lastIndexOf(item);
  },
  //迭代方法
  /**
   * 都符合条件则符合
   * @param {*} array 数组
   * @param {*} func 方法 有3个参数 item index array 返回boolean
   * @returns 都是true 则返回true
   */
  every: function (array, func) {
    return array.every(func);
  },
  /**
   * 是否符合条件
   * @param {*} array 数组
   * @param {*} func 方法 有3个参数 item  index array 返回boolean
   * @returns 一项true 则返回true
   */
  some: function (arrar, func) {
    return array.some(func);
  },
  /**
   * 过滤结果
   * @param {*} array 数组
   * @param {*} func 方法 有3个参数 item index array 返回boolean
   * @returns 该项为true 则返回该项
   */
  filter: function (arrar, func) {
    return array.filter(func);
  },
  /**
   * 改变结果
   * @param {*} array 数组
   * @param {*} func 方法 有3个参数 item index array 返回该项
   * @returns  返回数组
   */
  map: function (array, func) {
    return array.map(func);
  },
  /**
   * 等同于 for
   * @param {*} array 数组
   * @param {*} func 方法 有3个参数 item index array
   */
  forEach: function (arrar, func) {
    arrar.forEach(func);
  },
  //归并方法 支持的浏览器 >ie9、>firefox3、>safari4、>opera10.5、chrome
  /**
   * 从左往右遍历
   * @param {*} array 数组
   * @param {*} func 方法  4个参数 prev,cur,index,array 返回结果
   * @returns  返回方法中结果
   */
  reduce: function (array, func) {
    return array.reduce(func);
  },
  /**
   * 从左往右遍历
   * @param {*} array 数组
   * @param {*} func 方法  4个参数 prev,cur,index,array 返回结果
   * @returns  返回方法中结果
   */
  reduceRight: function (array, func) {
    return array.reduceRight(func);
  }
};

/**
 * date 时间公共操作类
 * 不同地区浏览器支持的时间格式不同
 * toDateString 显示星期几、月、日和年
 * toTimeString 显示 时、分、秒和时区
 * toLocaleDateString 特定地区显示星期几、月、日和年
 * toLocaleTimeString 特定地区显示 时、分、秒和时区
 * toUTCString UTC日期
 * toLocaleString  == toString
 */
var dateUtils = {
  /**
   * 美国浏览器通常支持的时间格式：
   * "月/日/年":"6/13/2020"
   * "英文月名 日.年":"January 12.2020"
   * "英文星期几 英文月名 日 年 时：分：秒 时区":"Tue May 25 2020 00:00:00 GMT-0700"
   * ISO 8601 扩展格式 YYYY-MM-DDTHH:mm:ss:sssZ :"2020-10-20T00:00:00" 只用兼容ECMAScript5的实现支持这种格式
   */
  parse: function (date) {
    //return Date.parse(date);
    return Date.UTC(date);
    //return new Date(date);
  },
  /**
   * ECMAScript5 支持Date.now() >ie9、>firefox3、> safari3、>opera10.5、chrome
   * 其他浏览器可以用 +new Date()
   * @returns 获取时间戳
   */
  now: function () {
    var start;
    if (Date.now) {
      start = Date.now();
    } else {
      start = +new Date();
    }
    return start;
  }
};

var regexExpresstion = {
  /**手机号正则表达式- */
  phone: /^[13|15|18][0-9]{9,9}$/
  ///^[13|15|18]\d{9,9}$/,
};
/**
 * 正则 公共类
 * input $_ 最近一次匹配的字符串 opera 未实现此属性
 * lastMatch $& 最近一次匹配的项 opera 未实现此属性
 * lastPattern $+ 最近一次匹配的捕获项 opera 未实现此属性
 * leftContext $` input字符串和lastMatch 之前的文本
 * multiline $* 布尔值 表示是否所有表达式都使用多行模式 ie、opera 未实现此属性
 * rightContext $' input字符串和lastMatch 之后的文本
 */
const regexUtils = {
  isPhone: function (phone) {
    return regexExpresstion.phone.test(phone);
  },
  exec: function (val, express) {
    return new RegExp(express).exec(val);
  },
  test: function (val, express) {
    return new RegExp(express).test(val);
  },
  regex: function (express) {
    return new RegExp(express);
  }
};
/**
 * string公共类
 */
const stringUtils = {
  isEmpty: function (val) {
    if (val) {
      return typeof val === "string" ? val.trim() : false;
    }
    return true;
  },
  isReference: function (val) {
    return val instanceof String;
  },
  charAt: function (val, index) {
    return val.charAt(index);
  },
  charCodeAt: function (val, index) {
    return val.charCodeAt(index);
  },
  length: function (val) {
    return val.length;
  },
  concat: function (val, str) {
    return val.concat(str);
  },
  /**
   * @returns 截取字符串 "1234567" 1 234567 3,3  4
   * 负数 则+字符串长度 -6=1 234567 3,-4=3,3 4
   */
  slice: function (val, start, end) {
    return !end ? val.slice(start) : val.slice(start, end);
  },
  /**
   * @returns 截取字符串 "1234567" 1 234567 3,3  4
   * 负数 转换为0 -1=0 1234567 3,-1=3,0 123
   */
  substring: function (val, start, end) {
    return !end ? val.substring(start) : val.substring(start, end);
  },
  /**
   * @returns 截取字符串 "1234567" 1 234567 3,3  456
   *  负数 则+字符串长度 -6=1 234567 3,-4=3,0 第二个参数转换为0
   */
  substr: function (val, start, end) {
    return !end ? val.substr(start) : val.substr(start, end);
  },
  indexOf: function (val, str, index) {
    return index ? val.indexOf(str, index) : val.indexOf(str);
  },
  lastIndexOf: function (val, str, index) {
    return index ? val.lastIndexOf(str, index) : val.lastIndexOf(str);
  },
  toUpperCase: function (val) {
    return val.toUpperCase();
  },
  toLocalUpperCase: function (val) {
    return val.toLocalUpperCase();
  },
  toLowerCase: function (val) {
    return val.toLowerCase();
  },
  toLocalLowerCase: function (val) {
    return val.toLocalLowerCase();
  },
  /**
   * =new RegExp(pattern).exec(val)
   * */
  match: function (val, pattern) {
    return val.match(pattern);
  },
  /**
   *
   * @param {*} val
   * @param {*} pattern
   * @returns 找到 返回索引位置 否则 -1
   */
  search: function (val, pattern) {
    return val.search(pattern);
  },
  /**
   * 替换文本 $$ $
   * $& 匹配整个模式的子字符串 =Regexp.lastMath ....
   * $n
   * $nn
   * @param {*} val
   * @param {*} oldStrOrPattern
   * @param {*} newStrOrFunc
   */
  replace: function (val, oldStrOrPattern, newStrOrFunc) {
    return val.replace(oldStrOrPattern, newStrOrFunc);
  },
  /**
   *
   * @param {*} val
   * @param {*} strOrPattern
   * @param {*} count 可选 返回数组不会超过指定的大小
   */
  split: function (val, strOrPattern, count) {
    return count ? val.split(strOrPattern, count) : val.split(strOrPattern);
  },
  /**
   *
   * @param {*} str1
   * @param {*} str2
   * @returns str1 字母表 在 str2之后 返回 1 具体实现情况而定 相等0 否则 -1
   */
  localeCompare: function (str1, str2) {
    return str1.localeCompare(str2);
  },
  fromCharCode: function (item) {
    return string.fromCharCode(item);
  },
  startWith: function (val, str) {
    return val.startWith(str);
  },
  endWith: function (val, str) {
    return val.endWith(str);
  }
};
const htmlUtils = {
  htmlEscape: function (text) {
    return text.replace(/[<>"&]/g, function (match, pos, originalText) {
      switch (match) {
        case "<":
          return "&lt;";
        case ">":
          return "&gt;";
        case "&":
          return "&amp;";
        case '"':
          return "&quot;";
      }
    });
  }
};

/**定时期公共类
 * 超时调用 函数中this 在非严格模式下 指向window 严格模型下undefined
 */
const timeUtils = {
  /**
   * 超时调用
   * @param {*} func
   * @param {*} millSconed
   */
  timeout: function (func, millSconed) {
    return setTimeout(func, millSconed);
  },
  clear: function (timeId) {
    clearTimeout(timeId);
  },
  /**间歇调用 */
  interval: function (func, millSconed) {
    return setInterval(func, millSconed);
  },
  clearInterval: function (intervalId) {
    clearInterval(intervalId);
  }
};

/**对话框公共类 */
const dialogUtils = {
  alert: function (msg) {
    alert(msg);
  },
  confirm: function (msg, enterFunc, cancelFun) {
    if (confirm(msg)) {
      enterFunc();
    } else {
      cancelFun();
    }
  },
  /**
   * 点击ok 返回输入的文本 关闭则返回null
   * @param {*} msg
   */
  prompt: function (msg, tip) {
    return this.prompt(msg, tip);
  },
  /**显示打印对话框 */
  print: function () {
    window.print();
  },
  /**显示查找对话框 */
  find: function () {
    window.find();
  }
};

/**url公共类
 * hash 属性除外 修改后重新加载
 * ie8、firefix、safari2、opera9、chrome hash值会在浏览器的历史记录中生成一条记录
 * 在ie早期版本中 hash属性不会随用户点击 后退 和 前进 按钮时被更新 ，而只会在用户在点击包含hash url时更新
 */
const urlUtils = {
  /**返回#后面的字符 不包含#则返回空字符串 */
  hash: function () {
    return location.hash;
  },
  /**返回服务器名称和端口号(有) www.baidu.com:80 */
  host: function () {
    return location.host;
  },
  /**返回不带端口号的服务器名称 www.baidu.com */
  hostname: function () {
    return location.hostname;
  },
  /**返回当前页面的完整路劲 */
  href: function () {
    return location.href;
  },
  /**返回url 中目录或文件名 /a/b */
  pathname: function () {
    return location.pathname;
  },
  /**返回url指定的端口号 */
  port: function () {
    return location.port;
  },
  /**返回url中的协议 http: */
  protocol: function () {
    return location.protocol;
  },
  /**返回url中查询字符之歌字符以？号开头 ?a=1 */
  search: function () {
    return location.search;
  },
  queryString: function (url) {
    var query = url ?
      url.indexOf("?") > -1 ?
      url.substring(url.indexOf("?") + 1) :
      "" :
      location.search.length > 0 ?
      location.search.substring(1) :
      "";
    if (query.length > 0) {
      let args = {};
      let strs = query.split("&");
      for (const key in strs) {
        let str = strs[key];
        let kv = str.split("=");
        args[decodeURIComponent(kv[0])] = encodeURIComponent(kv[1]);
      }
      return args;
    } else {
      return {};
    }
  }
};

function createScript(scripts) {
  for (const i in scripts) {
    var sc = scripts[i];
    var script = document.createElement("script");
    script.type = sc.type;
    script.src = sc.src;
    if (sc.defer) {
      script.defer = sc.defer;
    }
    if (sc.async) {
      script.async = sc.async;
    }
    document.body.appendChild(script);
  }
}

/**注意:safari3.0 不支持 text 属性 除了ie出现异常  */
function loadScriptString(code) {
  var script = document.createElement("script");
  script.type = "text/javascript";
  try {
    script.appendChild(document.createTextNode(code));
  } catch (error) {
    script.text = code;
  }
  document.body.appendChild(script);
}

function createStyle(styles) {
  for (const i in styles) {
    var css = styles[i];
    var link = document.createElement("link");
    link.type = css.type ? css.type : "text/css";
    link.rel = css.rel ? css.rel : "stylesheet";
    link.href = css.href;
    (document.head || document.getElementsByTagName("head")[0]).appendChild(
      link
    );
  }
}
/** 注意:ie不支持标签节点 */
function loadStyleString(css) {
  var link = document.createElement("link");
  link.type = "text/css";
  link.rel = "stylesheet";
  try {
    link.appendChild(document.createTextNode(css));
  } catch (error) {
    link.styleSheet.cssText = css;
  }
  (document.head || document.getElementsByTagName("head")[0]).appendChild(link);
}
/**frame 公共类*/
const frameUtils = {
  getFrame: function (indexOrName) {
    // return window.frames[indexOrName];
    // return top.frames[indexOrName];
    return frames[indexOrName];
  },
  init: function (frameInfos) {
    if (frameInfos instanceof Array) {
      var childs = [];
      for (const key in frameInfos) {
        let frameInfo = FrameInfos[key];
        if (
          frameInfo.position &&
          (frameInfo.position == "top" || frameInfo.position == "left")
        ) {
          var parent = init.call(frameInfo);
          childs.push(parent);
        } else {
          var frame = document.createElement("frame");
          frame.setAttribute("src", frameInfo.src);
          frame.setAttribute("name", frameInfo.name);
          childs.push(frame);
        }
      }
    } else {
      let frameInfo = FrameInfos;
      var frameSet = document.createElement("framset");
      if (frameInfo.position == "top") {
        frameSet.setAttribute("rows", frameInfo.rows);
      } else {
        frameSet.setAttribute("cols", frameInfo.cols);
      }
      if (frameInfo.child && frameInfo.child.length > 0) {
        var childs = init.call(frameInfo.child);
        frameSet.append(childs);
      }
      return frameSet;
    }
  }
};
const domUtils = {
  /**
   * <ie5 不支持该方法 document.getElementById <=ie8 不区分大小写 <=ie7 返回表单 name=id 的元素
   * 非标准模式下的属性 document.all
   * @param {*} id
   */
  getElement: function (id) {
    if (document.getElementById) {
      return document.getElementById(id);
    } else if (document.all) {
      return document.all[id];
    } else {
      return undefined;
    }
  },
  get: function (cssSelector) {
    if (/#(.*?)\s*/.test(cssSelector)) {}
  },
  getImg: function (indexOrName) {
    let imgs = document.getElementsByTagName("img");
    return imgs[indexOrName];
    //return imgs.item[0];
    //return imgs.namedItem("name");
  },
  matchesSelector: function (element, selector) {
    if (element.matchesSelector) {
      return element.matchesSelector(selector);
    } else if (element.msMatchesSelector) {
      return element.msMatchesSelector(selector);
    } else if (element.mozMatchesSelector) {
      return element.mozMatchesSelector(selector);
    } else if (element.webkitMatchesSelector) {
      return element.webkitMatchesSelector(selector);
    }
    return undefined;
  },
  getClass: function (element, css) {
    var classs = element.className.split(/\s+/);
    let pos = -1,
      i,
      len;
    for (i = 0, len = classs.length; i < len; i++) {
      if (classs[i] == css) {
        pos = i;
        break;
      }
    }
    return {
      className: classs,
      pos: i
    };
  },
  addClass: function (element, css) {
    if (element.classList && element.classList.add) {
      element.classList.add(css);
    } else {
      let cla = this.getClass(element, css);
      cla.className.push(css);
      element.className = cla.className.join(" ");
    }
  },
  removeClass: function (element, css) {
    if (element.classList && element.classList.remove) {
      element.classList.remove(css);
    } else {
      let cla = this.getClass(element, css);
      if (cla.pos != -1) {
        cla.className.slice(cla.pos, 1);
      }
      element.className = cla.className.join(" ");
    }
  },
  toggleClass: function (element, css) {
    if (element.classList && element.classList.toggle) {
      element.classList.toggle(css);
    } else {
      let cla = this.getClass(element, css);
      if (cla.pos != -1) {
        cla.className.slice(cla.pos, 1);
      } else {
        cla.className.push(css);
      }
      element.className = cla.className.join(" ");
    }
  },
  activeName: function () {
    return document.activeElement;
  },
  loading: function (func) {
    if (document.readyState == "loading") {
      func();
    }
  },
  complete: function (func) {
    if (document.readyState == "complete") {
      func();
    }
  },
  setCharset: function (charset) {
    if (document.charset) {
      document.charset = charset;
    }
    if (document.defaultCharset) {
      document.defaultCharset = charset;
    }
    if (document.characterset) {
      document.characterset = charset;
    }
    if (document.characterSet) {
      document.characterSet = charset;
    }
  },
  data: function (element) {
    if (element.dataset) {
      return element.dataset;
    } else {
      return element.attributes;
    }
  },
  html: function (element, str) {
    if (str) {
      if (element.innerHtml) {
        element.innerHtml = str;
      }
    } else {
      return element.innerHtml;
    }
  },
  text: function (element, str) {
    if (str) {
      if (typeof element.textContent == "string") {
        element.textContent = str;
      } else {
        element.innerText = str;
      }
    } else {
      return element.innerText || element.textContent;
    }
  },
  scroll: function (element) {
    if (element.scrollIntoView) {
      element.scrollIntoView();
    }
  },
  event: {
    addEvent: function (element, eventName, func) {
      //dom2
      if (element.addEventListener) {
        element.addEventListener(eventName, func, false);
      }
      //ie
      else if (element.attachEvent) {
        element.attachEvent("on" + eventName, func);
      }
      //dmom
      else {
        element["on" + eventName] = func;
      }
    },
    removeEvent: function (element, eventName, func) {
      //dom2
      if (element.removeEventListener) {
        element.removeEventListener(eventName, func, false);
      }
      //ie
      else if (element.detachEvent) {
        element.detachEvent("on" + eventName, func);
      }
      //dom
      else {
        element["on" + eventName] = null;
      }
    },
    /**事件是否冒泡 只读 boolean
     * @summary ie 可读可写  默认false true 取消事件冒泡 dom中stopPropagation()方法作用相同 */
    isBubbules: function (event, bubbles) {
      if (typeUtils.isBoolean(event.cancelBubble)) {
        if (typeUtils.isBoolean(bubbles)) {
          event.cancelBubble = bubbles;
        }
        return event.cancelBubble;
      }
      return event.bubbles;
    },
    /**是否可用取消事件的默认值行为 可读 boolean*/
    isCancelable: function (event) {
      return event.cancelable;
    },
    /**事件处理程序当前正在处理事件的那个元素 可读 element */
    currentTarget: function (event) {
      return event.currentTarget;
    },
    /**为true 表示已调用 preventDefault() dom3 事件 可读 boolean */
    defaultPrevented: function (event) {
      return event.defaultPrevented;
    },
    /**事件相关的细节信息 integer*/
    detail: function (event) {
      return event.detail;
    },
    /**调用事件处理程序的阶段:1表示捕获阶段，2表示处于目标，2表示冒泡阶段 可读 integer */
    eventPhase: function (event) {
      return event.eventPhase;
    },
    /**取消冒泡的默认行为 如果cancelable是true 则可以使用该方法 可读 function
     * @summary ie returnView 默认为 true 但将其设置false 就可以取消事件的默认行为(与dom中preventDefault()方法作用相同) 可读可写 boolean
     */
    preventDefault: function (event) {
      if (event.preventDefault) {
        event.preventDefault();
      } else {
        event.returnView = false;
      }
    },
    /**取消事件的进一步捕获或冒泡 同时阻止任何处理程序被调用(dom3级事件账户新增) 可读 function */
    stopImmedilatePropagation: function (event) {
      if (event.stopImmedilatePropagation) {
        event.stopImmedilatePropagation();
      }
    },
    /**取消事件的进一步捕获或冒泡 bubbles 为true 则可以使用该方法 可读 function
     * @summary ie 可读可写  默认false true 取消事件冒泡 dom中stopPropagation()方法作用相同
     */
    stopPropagation: function (event) {
      if (event.stopPropagation) {
        event.stopPropagation();
      } else {
        event.cancelBubble = bubbles;
      }
    },
    /**事件的目标 可读 element ie srcElement事件的目标与dom中的target属性相同 可读 element */
    target: function (event) {
      return event.target || event.srcElement;
    },
    /**true 表示事件是浏览器生成的 false事件是由开发人员通过脚本创建的(dom3) 可读 boolean */
    trusted: function (event) {
      return event.trusted;
    },
    /**被触发的事件类型 可读 string
     * @summary ie */
    type: function (event) {
      return event.type;
    },
    /**与事件相关的抽象视图 等同于发生事件的window对象 只读 AbatractView */
    view: function (event) {
      return event.view;
    },
    /**
     * 是否支持 dom version 版本事件
     * @param eventName UIEvent
     * @param {*} version 版本号 3.0
     */
    isSupport: function (eventName, version) {
      if (document.implementation && document.implementation.hasFeature) {
        return document.implementation.hasFeature(eventName, version);
      }
      return false;
    }
  }
};
/**客户端检测 */
const clientUtils = {
  /**呈现引擎 */
  engine: {
    ie: 0,
    gecko: 0,
    webkit: 0,
    khtml: 0,
    opera: 0,
    version: null
  },
  /**浏览器 */
  browser: {
    ie: 0,
    firefox: 0,
    safari: 0,
    konq: 0,
    opera: 0,
    chrome: 0,
    version: null
  },
  system: {
    //电脑操作系统
    win: false,
    mac: false,
    linux: false,
    //移动设备
    iphone: false,
    ipod: false,
    ipad: false,
    ios: false,
    android: false,
    nokiaN: false,
    winMobile: false,
    //游戏系统
    wil: false,
    ps: false
  },
  init: function () {
    let self = this;
    let userAgent = navigator.userAgent;
    if (window.opera) {
      self.engine.version = window.opera.version();
      self.engine.opera = parseFloat(self.engine.version);
    } else if (/AppleWebKit\/(\S+)/.test(userAgent)) {
      self.engine.version = RegExp["$1"];
      self.engine.webkit = parseFloat(self.engine.version);
      //确定是chrome 还是 safari
      if (/Chrome\/(\S+)/.test(userAgent)) {
        self.browser.version = RegExp["$1"];
        self.browser.chrome = parseFloat(self.browser.version);
      } else if (/Verion\/(\S+)/.test(userAgent)) {
        self.browser.version = RegExp["$1"];
        self.browser.safari = parseFloat(self.browser.version);
      } else {
        let safariVerion = 1;
        if (self.engine.version < 100) {
          safariVerion = 1;
        } else if (self.engine.version < 312) {
          safariVerion = 1.2;
        } else if (self.engine.version < 412) {
          safariVerion = 1.3;
        } else {
          safariVerion = 2;
        }
        self.browser.version = self.engine.version = safariVerion;
      }
    } else if (
      /KHTML\/(\S+)/.test(userAgent) ||
      /Konqueror\/([^;]+)/.test(userAgent)
    ) {
      self.engine.version = browser.version = RegExp["$1"];
      self.engine.khtml = browser.konq = parseFloat(self.engine.version);
    } else if (/rv:([^\)]+)\) Gecko\/\d(8)/.test(userAgent)) {
      self.engine.version = RegExp["$1"];
      self.engine.gecko = parseFloat(self.engine.version);
      if (/Firefox\/(\S+)/.test(userAgent)) {
        self.browser.version = self.browser.version = RegExp["$1"];
        self.browser.firefox = parseFloat(self.browser.version);
      }
    } else if (/MSIE ([^;]+)/.test(userAgent)) {
      self.engine.version = RegExp["$1"];
      self.engine.ie = parseFloat(self.engine.version);
    }
    self.browser.ie = self.engine.ie;
    self.browser.opera = self.engine.opera;
    let platform = navigator.platform;
    self.system.win = platform.indexOf("Win") == 0;
    self.system.mac = platform.indexOf("Mac") == 0;
    self.system.linux = platform == "X11" || platform.indexOf("Linux") == 0;
    if (self.system.win) {
      if (/Win(?:dows )?([^do](2))\s?(\d+\.\d+)?/.test(userAgent)) {
        switch (RegExp["$1"]) {
          case "NT": {
            switch (RegExp["$2"]) {
              case "5.0":
                self.system.win = "2000";
                break;
              case "5.1":
                self.system.win = "XP";
                break;
              case "6.0":
                self.system.win = "Vista";
                break;
              case "6.1":
                self.system.win = "7";
                break;
              default:
                self.system.win = "NT";
                break;
            }
          }
          break;
        case "9x":
          self.system.win = "ME";
          break;
        default:
          self.system.win = RegExp["$1"];
          break;
        }
      }
    }
    self.system.iphone = platform.indexOf("iPhone") > -1;
    self.system.ipod = platform.indexOf("iPod") > -1;
    self.system.ipad = platform.indexOf("iPad") > -1;
    self.system.nokiaN = userAgent.indexOf("NokiaN") > -1;
    if (self.system.win == "CE") {
      self.system.winMobie = self.system.win;
    } else if ((self.system.win = "Ph")) {
      if (/Windows Phone OS (\d+\.\d+)/.test(userAgent)) {
        self.system.win = "Phone";
        self.system.winMobile = parseFloat(RegExp.$1);
      }
    }
    if (self.system.mac && userAgent.indexOf("Mobile") > -1) {
      if (/CPU (?:iPhone )?OS (\d+_\d+)/.test(userAgent)) {
        self.system.ios = parseFloat(RegExp.$1.replace("_", "."));
      } else {
        self.system.ios = 2; //猜测,不能真正检测出来
      }
    }
    if (/Android (\d+\.\d+)/.test(userAgent)) {
      self.system.android = parseFloat(RegExp.$1);
    }

    self.system.wil = userAgent.indexOf("Wil") > -1;
    self.system.ps = /playstation/i.test(userAgent);
  },
  exec: function (
    chromeFunc,
    safariFunc,
    geckoFunc,
    firefoxFunc,
    xpFunc,
    vistaFunc,
    iosFunc,
    androidFunc,
    nokiaNFunc
  ) {
    if (this.engine.webkit) {
      if (this.browser.chrome) {
        chromeFunc && chromeFunc();
      } else {
        safariFunc();
      }
    } else if (this.engine.gecko) {
      if (this.browser.firefox) {
        firefoxFunc && firefoxFunc();
      } else {
        geckoFunc && geckoFunc();
      }
    }
    if (this.system.win) {
      if (this.system.win == "XP") {
        xpFunc && xpFunc();
      } else if (this.system.win == "Vista") {
        vistaFunc && vistaFunc();
      }
    }
    if (this.engine.webkit) {
      if (this.system.ios) {
        iosFunc && iosFunc();
      } else if (this.system.android) {
        androidFunc && androidFunc();
      } else if (this.system.nokiaN) {
        nokiaNFunc && nokiaNFunc();
      }
    }
  }
};
clientUtils.init();
/**
 * window 公共类
 * ie、safari、opera、chrome 提供了 screentLeft  ,safari、chrome、opera(与screenLeft属性病不对应)、firefox 提供了screenX
 */
const windowUtils = {
  leftPos: function () {
    return typeof window.screenLeft == "number" ?
      window.screenLeft :
      window.screenX;
  },
  topPos: function () {
    return typeof window.screenTop == "number" ?
      window.screenTop :
      window.screenY;
  },
  /**
   * 屏幕移动
   * @param {*} x
   * @param {*} y
   */
  moveTo: function (x, y) {
    window.moveTo(x, y);
  },
  /**
   * 屏幕水平和垂直方向移动
   * @param {*} left
   * @param {*} top
   */
  moveBy: function (left, top) {
    window.moveBy(left, top);
  },
  /**
   * >ie9、firefox、safari、opera、chrome 提供了 innerWidth、innerHeight outerWidth、outerHeight
   * outerWidth 、outerHeight在opera 中表示页面视图容器大小
   * innerWidth、innerHeight 表示容器页面视图大小减去边框 宽度
   * chrome inner和outer 返回相同的值 即视图大小而非浏览器窗口大小 <=ie8 没有提供窗口尺寸的属性  不过DOM提供了可见区域的信息
   * ie、firefox、safari、opera、chrome中 document.documentElement.clientWidth document.documentElement.clientHeight 保存了
   * 页面视图的信息 ie6这些属性必须在标准模式下有效  document.body.clientWidth document.body.clientHeight
   * 混杂模式下有效 混杂模式下的chrome   document.documentElement document.body都可以获取窗口视图信息
   */
  size: function () {
    var pageWidth = window.innerWidth;
    var pageHeight = window.innerHeight;
    if (typeof pageWidth != "number") {
      if (document.compatMode == "CSS1Compat") {
        pageWidth = document.documentElement.clientWidth;
        pageHeight = document.documentElement.clientHeight;
      } else {
        pageWidth = document.body.clientWidth;
        pageHeight = document.body.clientHeight;
      }
    }
    return {
      width: pageWidth,
      height: pageHeight
    };
  },
  /**
   * 接受浏览器窗口的新宽度和新高度 可能被浏览器禁用 opera、>=ie7 默认是禁用
   *  @param win
   * @param {*} width
   * @param {*} height
   */
  resizeTo: function (win, width, height) {
    win.resizeTo(width, height);
  },
  /**
   * 接收新窗口和旧窗口的宽度之差和高度之差  可能被浏览器禁用 opera、>=ie7 默认是禁用
   * @param win
   * @param {*} width
   * @param {*} height
   */
  resizeBy: function (win, width, height) {
    win.resizeBy(width, height);
  },
  /**
   * 导航和打开窗口
   * @param {*} url  要加载的url
   * @param {*} target 窗口目标 页面存在的窗口名称 也可以是_self、_parent、_top、_blank
   * @param {*} str 一个特性字符串 例如 fullscreen=yes,height=100
   * fullscren=yes|no 表示浏览器窗口是否最大化 仅限 ie
   * height=number 表示窗口的高度 不能小于100
   * left=number 表示新窗口的左坐标,不能是负值
   * location=yes|no 表示是否在浏览器窗口显示地址 不同浏览器默认值不同 no地址栏可能隐藏，也可能禁用(取决浏览器)
   * menubar=yes|no 表示是否在浏览器中显示菜单栏 默认值no
   * resizeable=yes|no 表示是否可通过拖动浏览器窗口的边框改变大小 默认值no
   * scrollbars=yes|no 表示如果内容在视口显示不下，是否允许滚动 默认值No
   * status=yes|no 显示状态栏 默认no
   * toolbar=yes|no 显示工具栏 默认no
   * top=number 新窗口上坐标 不能是负值
   * width=number 新窗口宽度 不能小于100
   * @param {*} falg 一个表示新页面是否取代浏览器历史记录中当前加载页面的布尔值
   * window.open("url","target")==<a href="url" target="target"></a>
   */
  open: function (url, target, str, falg) {
    return window.open(url, target, str, falg);
  },
  close: function (win) {
    win.close();
  },
  /**
   * 窗口引用还在 window.closed 检测window 是否关闭
   * @param {*} win
   */
  closed: function (win) {
    return win.closed;
  },
  /**
   * 获取打开窗口的原始对象 window.opera window.opera=null 告诉浏览器创建的标签页不需要与打开的标签页通讯,因此可以在独立进程中运行。一旦切断,将没有办法恢复
   * ie8、chrome 会独立进程中运行每个标签页
   * @param {*} win
   */
  opera: function (win) {
    return win.opera;
  },
  /**
   * 检测弹出窗口是否被屏蔽
   * @param {*} url
   */
  blocked: function (url) {
    try {
      var win = window.open(url, "_blank");
      if (win == null) return true;
      else return false;
    } catch (error) {
      return true;
    }
  }
};

function convertToArray(nodes) {
  try {
    let array = Array.prototype.slice.call(nodes, 0); //< ie8 不支持 COM实现的
    return array;
  } catch (error) {
    let array = [];
    for (const i in nodes) {
      let node = nodes[i];
      array.push(node);
    }
    return array;
  }
}

function compareObject(property, sort) {
  return function (obj1, obj2) {
    let val1 = obj1[property];
    let val2 = obj2[property];
    if (val1 < val2) {
      return prototypeUtils.isBoolean(sort) ?
        sort ? -1 : 1 :
        -1;
    } else if (val1 > val2) {
      return prototypeUtils.isBoolean(sort) ?
        sort ? 1 : -1 :
        1;
    } else {
      return 0;
    }
  };
}