(function(n){function e(e){for(var o,r,u=e[0],c=e[1],l=e[2],s=0,p=[];s<u.length;s++)r=u[s],Object.prototype.hasOwnProperty.call(a,r)&&a[r]&&p.push(a[r][0]),a[r]=0;for(o in c)Object.prototype.hasOwnProperty.call(c,o)&&(n[o]=c[o]);d&&d(e);while(p.length)p.shift()();return i.push.apply(i,l||[]),t()}function t(){for(var n,e=0;e<i.length;e++){for(var t=i[e],o=!0,r=1;r<t.length;r++){var c=t[r];0!==a[c]&&(o=!1)}o&&(i.splice(e--,1),n=u(u.s=t[0]))}return n}var o={},a={index:0},i=[];function r(n){return u.p+"static/js/"+({"pages-detail-detail":"pages-detail-detail","pages-news-index":"pages-news-index"}[n]||n)+"."+{"pages-detail-detail":"09362ccc","pages-news-index":"3e341c9f"}[n]+".js"}function u(e){if(o[e])return o[e].exports;var t=o[e]={i:e,l:!1,exports:{}};return n[e].call(t.exports,t,t.exports,u),t.l=!0,t.exports}u.e=function(n){var e=[],t=a[n];if(0!==t)if(t)e.push(t[2]);else{var o=new Promise((function(e,o){t=a[n]=[e,o]}));e.push(t[2]=o);var i,c=document.createElement("script");c.charset="utf-8",c.timeout=120,u.nc&&c.setAttribute("nonce",u.nc),c.src=r(n);var l=new Error;i=function(e){c.onerror=c.onload=null,clearTimeout(s);var t=a[n];if(0!==t){if(t){var o=e&&("load"===e.type?"missing":e.type),i=e&&e.target&&e.target.src;l.message="Loading chunk "+n+" failed.\n("+o+": "+i+")",l.name="ChunkLoadError",l.type=o,l.request=i,t[1](l)}a[n]=void 0}};var s=setTimeout((function(){i({type:"timeout",target:c})}),12e4);c.onerror=c.onload=i,document.head.appendChild(c)}return Promise.all(e)},u.m=n,u.c=o,u.d=function(n,e,t){u.o(n,e)||Object.defineProperty(n,e,{enumerable:!0,get:t})},u.r=function(n){"undefined"!==typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(n,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(n,"__esModule",{value:!0})},u.t=function(n,e){if(1&e&&(n=u(n)),8&e)return n;if(4&e&&"object"===typeof n&&n&&n.__esModule)return n;var t=Object.create(null);if(u.r(t),Object.defineProperty(t,"default",{enumerable:!0,value:n}),2&e&&"string"!=typeof n)for(var o in n)u.d(t,o,function(e){return n[e]}.bind(null,o));return t},u.n=function(n){var e=n&&n.__esModule?function(){return n["default"]}:function(){return n};return u.d(e,"a",e),e},u.o=function(n,e){return Object.prototype.hasOwnProperty.call(n,e)},u.p="/",u.oe=function(n){throw console.error(n),n};var c=window["webpackJsonp"]=window["webpackJsonp"]||[],l=c.push.bind(c);c.push=e,c=c.slice();for(var s=0;s<c.length;s++)e(c[s]);var d=l;i.push([0,"chunk-vendors"]),t()})({0:function(n,e,t){n.exports=t("aea3")},"0943":function(n,e,t){"use strict";t.r(e);var o=t("eeea"),a=t("6794");for(var i in a)"default"!==i&&function(n){t.d(e,n,(function(){return a[n]}))}(i);t("9df7");var r,u=t("f0c5"),c=Object(u["a"])(a["default"],o["b"],o["c"],!1,null,null,null,!1,o["a"],r);e["default"]=c.exports},"3bab":function(n,e,t){var o=t("24fb");e=o(!1),e.push([n.i,"\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n/*每个页面公共css */",""]),n.exports=e},"42c6":function(n,e,t){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var o={appid:""};e.default=o},"50a0":function(n,e,t){var o=t("3bab");"string"===typeof o&&(o=[[n.i,o,""]]),o.locals&&(n.exports=o.locals);var a=t("4f06").default;a("31b058fc",o,!0,{sourceMap:!1,shadowMode:!1})},6794:function(n,e,t){"use strict";t.r(e);var o=t("c73f"),a=t.n(o);for(var i in o)"default"!==i&&function(n){t.d(e,n,(function(){return o[n]}))}(i);e["default"]=a.a},"782a":function(n,e,t){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var o={pages:{"pages/news/index":{},"pages/detail/detail":{titleNView:{type:"transparent"}}},globalStyle:{navigationBarTextStyle:"white",navigationBarTitleText:"新闻资讯",navigationBarBackgroundColor:"#2F85FC",backgroundColor:"#FFFFFF"}};e.default=o},"9df7":function(n,e,t){"use strict";var o=t("50a0"),a=t.n(o);a.a},a927:function(n,e,t){"use strict";(function(n){var e=t("4ea4"),o=e(t("e143"));n["________"]=!0,delete n["________"],n.__uniConfig={globalStyle:{navigationBarTextStyle:"white",navigationBarTitleText:"新闻资讯",navigationBarBackgroundColor:"#2F85FC",backgroundColor:"#FFFFFF"}},n.__uniConfig.compilerVersion="3.1.8",n.__uniConfig.router={mode:"hash",base:"/"},n.__uniConfig.publicPath="/",n.__uniConfig["async"]={loading:"AsyncLoading",error:"AsyncError",delay:200,timeout:6e4},n.__uniConfig.debug=!1,n.__uniConfig.networkTimeout={request:6e4,connectSocket:6e4,uploadFile:6e4,downloadFile:6e4},n.__uniConfig.sdkConfigs={},n.__uniConfig.qqMapKey="XVXBZ-NDMC4-JOGUS-XGIEE-QVHDZ-AMFV2",n.__uniConfig.nvue={"flex-direction":"column"},n.__uniConfig.__webpack_chunk_load__=t.e,o.default.component("pages-news-index",(function(n){var e={component:t.e("pages-news-index").then(function(){return n(t("12e2"))}.bind(null,t)).catch(t.oe),delay:__uniConfig["async"].delay,timeout:__uniConfig["async"].timeout};return __uniConfig["async"]["loading"]&&(e.loading={name:"SystemAsyncLoading",render:function(n){return n(__uniConfig["async"]["loading"])}}),__uniConfig["async"]["error"]&&(e.error={name:"SystemAsyncError",render:function(n){return n(__uniConfig["async"]["error"])}}),e})),o.default.component("pages-detail-detail",(function(n){var e={component:t.e("pages-detail-detail").then(function(){return n(t("036d"))}.bind(null,t)).catch(t.oe),delay:__uniConfig["async"].delay,timeout:__uniConfig["async"].timeout};return __uniConfig["async"]["loading"]&&(e.loading={name:"SystemAsyncLoading",render:function(n){return n(__uniConfig["async"]["loading"])}}),__uniConfig["async"]["error"]&&(e.error={name:"SystemAsyncError",render:function(n){return n(__uniConfig["async"]["error"])}}),e})),n.__uniRoutes=[{path:"/",alias:"/pages/news/index",component:{render:function(n){return n("Page",{props:Object.assign({isQuit:!0,isEntry:!0},__uniConfig.globalStyle,{})},[n("pages-news-index",{slot:"page"})])}},meta:{id:1,name:"pages-news-index",isNVue:!0,maxWidth:0,pagePath:"pages/news/index",isQuit:!0,isEntry:!0,windowTop:44}},{path:"/pages/detail/detail",component:{render:function(n){return n("Page",{props:Object.assign({},__uniConfig.globalStyle,{titleNView:{type:"transparent"}})},[n("pages-detail-detail",{slot:"page"})])}},meta:{name:"pages-detail-detail",isNVue:!0,maxWidth:0,pagePath:"pages/detail/detail",windowTop:0}},{path:"/preview-image",component:{render:function(n){return n("Page",{props:{navigationStyle:"custom"}},[n("system-preview-image",{slot:"page"})])}},meta:{name:"preview-image",pagePath:"/preview-image"}},{path:"/choose-location",component:{render:function(n){return n("Page",{props:{navigationStyle:"custom"}},[n("system-choose-location",{slot:"page"})])}},meta:{name:"choose-location",pagePath:"/choose-location"}},{path:"/open-location",component:{render:function(n){return n("Page",{props:{navigationStyle:"custom"}},[n("system-open-location",{slot:"page"})])}},meta:{name:"open-location",pagePath:"/open-location"}}],n.UniApp&&new n.UniApp}).call(this,t("c8ba"))},aea3:function(n,e,t){"use strict";var o=t("4ea4"),a=o(t("5530"));t("e260"),t("e6cf"),t("cca6"),t("a79d"),t("a927"),t("06b9"),t("921b");var i=o(t("e143")),r=o(t("0943"));i.default.config.productionTip=!1,i.default.prototype.$host="https://unidemo.dcloud.net.cn/",r.default.mpType="app";var u=new i.default((0,a.default)({},r.default));u.$mount()},c73f:function(n,e,t){"use strict";(function(n){Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var t={onLaunch:function(){n.log("App Launch")},onShow:function(){n.log("App Show")},onHide:function(){n.log("App Hide")}};e.default=t}).call(this,t("5a52")["default"])},eeea:function(n,e,t){"use strict";var o;t.d(e,"b",(function(){return a})),t.d(e,"c",(function(){return i})),t.d(e,"a",(function(){return o}));var a=function(){var n=this,e=n.$createElement,t=n._self._c||e;return t("App",{attrs:{keepAliveInclude:n.keepAliveInclude}})},i=[]}});