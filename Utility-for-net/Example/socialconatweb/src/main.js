// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from "vue";
import Element from "element-ui";
import App from "./App";
import router from "./router";
import {init,getToken}  from "./js/auth.js";
import "element-ui/lib/theme-chalk/index.css";
import "normalize.css/normalize.css"; // a modern alternative to CSS resets
init();
// 引入 vue-ele-form
// import EleForm from "vue-ele-form";
Vue.use(Element);
// 注册 vue-ele-form
// Vue.use(EleForm);
Vue.config.productionTip = false;
router.beforeEach((to, from ,next) => {
    const token = getToken();
    if(to.matched.some(record => record.meta.requireAuth || record.meta.homePages)){
        //处理拦截至登录页，然后点去注册页，完善信息页，再回登录页，再登录进去
        if(Object.keys(from.query).length === 0){//不是这种目标拦截的情况（就from.query是空对象）直接next()
            next()
        }else{
            let redirect = from.query.redirect//是目标拦截的情况，记录redirect
            if(to.path === redirect){//这个是处理无限循环的问题
                next()
            }else{
                if(Object.keys(to.query).length > 0){//加上query之后，就判断它有了query，就next()跳转进去
                    next()
                }else{//第一次跳转to路由是没有query的，我们需要加上query来记录我们需要的目标路由
                    next({
                          path:to.path,
                          query: {redirect: redirect}
                    })
                }
            }
        }
    }else{
        if(token){
            if(Object.keys(from.query).length === 0){
                next()
            }else{
                let redirect = from.query.redirect
                if(to.path === redirect){
                    next()
                }else{
                    next({path:redirect})
                }
            }
        }else{
            if(to.path==="/login"){
                next()
            }else{
                next({
                      path:"/login",
                      query: {redirect: to.fullPath}
                })
            }
        }
    }
    return
})

/* eslint-disable no-new */
new Vue({
  el: "#app",
  router,
  components: {
    App
  },
  template: "<App/>"
});
