!(function () {
    var utility = {};
    
    /**客户端检测 */
    var clientUtils = {
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
        var self = this;
        var userAgent = navigator.userAgent;
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
            var safariVerion = 1;
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
        var platform = navigator.platform;
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
     function getClass(element, css) {
        var classs = element.className.split(/\s+/);
         var pos = -1,
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
    }
    function checkClasss(element) {
        if (!element ) {
             console.warn(element+" element undefined ");
            return false;
        }
        else if ( !element.classList) {
             console.warn(element+" element attr classList not exists");
            return false;
        }
        return true;
    }
    utility.getElement = function (selector, flag) {
        if (flag) {
            flag = SelectorFlag.id;
        }
        //<ie5 不支持该方法 document.getElementById <=ie8 不区分大小写 <=ie7 返回表单 name=id 的元素非标准模式下的属性 document.all
        if (flag == SelectorFlag.id) {
            if (document.getElementById) {
                return document.getElementById(selector);
            } else if (document.all) {
                return document.all[selector];
            }
            else {
                console.warn(selector + " element attr all or method getElementById  not exists");
                return undefined;
            }
        } else if (flag == SelectorFlag.class) {
            if (document.getElementsByClassName !== "undefined") {
                return document.getElementsByClassName(selector);
            }
            console.warn(selector + " element  method getElementsByClassName not exists");
            return undefined;
        } else if (flag == SelectorFlag.tag) {
            if (document.getElementsByTagName !== "undefined") {
                return document.getElementsByTagName(selector);
            } else if (document.querySelectorAll) {
                  return document.querySelectorAll(selector);
            }
            console.warn(selector + " element  method getElementsByTagName or querySelectorAll not exists");
            return undefined;
        }
        else if (flag == SelectorFlag.img) {
            var imgs = document.getElementsByTagName("img");
            if (imgs) {
                if (imgs.namedItem) {
                    return imgs.namedItem(selector);//name
                } else if (imgs.item) {
                    return imgs.item[selector];//index
                }
                return imgs[selector];//indexOrName
            } else {
                console.warn("img element get fail");
                return undefined;
            }
        } else if (flag == SelectorFlag.selector) {
            if (document.matchesSelector) {
                return document.matchesSelector(selector);
            } else if (document.msMatchesSelector) {
                return document.msMatchesSelector(selector);
            } else if (document.mozMatchesSelector) {
                return document.mozMatchesSelector(selector);
            } else if (document.webkitMatchesSelector) {
                return document.webkitMatchesSelector(selector);
            }
            console.warn("matchesSelector method not exists");
            return undefined;
        }
    };
  
    utility.addClass = function (element, clas) {
        if (checkClasss(element)) {
            if (element.classList.add) {
                element.classList.add(css);
            } else {
                var cla = getClass(element, clas);
                cla.className.push(clas);
                element.className = cla.className.join(" ");
            }
        }
    }
    utility.removeClass = function (element, clas) {
        if (checkClasss(element)) {
            if (element.classList.remove) {
                element.classList.remove(clas);
            } else {
                var cla = this.getClass(element, clas);
                if (cla.pos != -1) {
                    cla.className.slice(cla.pos, 1);
                    element.className = cla.className.join(" ");
                }
            }
        }
    };
    utility.toggleClass = function (element, clas) {
       if (checkClasss(element))  {
            if (element.classList.toggle) {
                element.classList.toggle(clas);
            } else {
                var cla = this.getClass(element, clas);
                if (cla.pos != -1) {
                    cla.className.slice(cla.pos, 1);
                } else {
                    cla.className.push(clas);
                }
                element.className = cla.className.join(" ");
                console.log(element.className);
            }
        } 
    };
    utility.activeElement = document.activeElement;
    utility.loading = function (loadFunc) {
        if (document.readyState == "loading") {
            loadFunc();
        }
    };
    utility.charset = function (charset) {
        if (charset) {
            if (document.charset) {
                document.charset = charset;
            }
            else if (document.defaultCharset) {
                document.defaultCharset = charset;
            }
            else if (document.characterset) {
                document.characterset = charset;
            } else {
                console.warn("document element attr charset not exists ");
            }
        } else {
            if (document.charset) {
                return document.charset;
            }
            else if (document.defaultCharset) {
                return document.defaultCharset;
            }
            else if (document.characterset) {
                return document.characterset;
            } else {
                console.warn("document element attr charset not exists ");
            }
        }
    };
    utility.attributes = function (element) {
        if (element.dataset) {
            return element.dataset;
        } else {
            return element.attributes;
        }
    };
    var SelectorFlag = {
        id: 1,
        class: 2,
        tag:3,
        img: 4,
        selector:5,
    };
    var domUtils = {
        complete: function (func) {
            if (document.readyState == "complete") {
                func();
            }
        },
        html: function (element, str) {
            if (str) {
                if (element.innerHTML) {
                    element.innerHTML = str;
                }
            } else {
                return element.innerHTML;
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
    utility.client = clientUtils;
    clientUtils.init();
    utility.event = domUtils.event;
    utility.html = domUtils.html;
     utility.text = domUtils.text;
    utility.onmouseout = function (ele, func) {
        utility.event.addEvent(ele, "mouseout", function (e) {
                e = e || window.event || event;
                var curr = e.currentTarget || originalTarget; //定义了hover事件的元素
                var relaElementto = e.toElement || e.relatedTarget; //移出事件的目标
                if (!curr.contains(relaElementto) && e.type == "mouseout") { //移出事件，即，当移出的目标不是它的子元素中的任一个，我们就确定它的确是移出了
                    func(e); //定义移出引发的事件
                } 
            });
    };
     utility.onmouseover = function (ele, func) {
        utility.event.addEvent(ele, "mouseover",  function (e) {
                e = e || window.event || event;
                var curr = e.currentTarget || originalTarget; //定义了hover事件的元素
            var relaElementfr = e.fromElement || e.relatedTarget; //移入事件的目标
            if (!curr.contains(relaElementfr) && e.type == "mouseover") { //移入事件，即，当移入的目标不是它的子元素中的任一个，我们就确定它的确是移入了
                    func(e); //定义移入引发事件
                }
            });
    };
    this.Utility = window.Utility = utility;
    
 })();