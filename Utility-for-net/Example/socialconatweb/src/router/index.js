import Vue from "vue";
import Router from "vue-router";

import HelloWorld from "@/components/HelloWorld";
import admin from "@/view/admin/admin";
import icon from "@/view/admin/icon";
import edutioncategory from "@/view/admin/edutioncategory";
import file from "@/view/admin/file";
import filecategory from "@/view/admin/filecategory";
import jobcategory from "@/view/admin/jobcategory";
import like from "@/view/admin/like";
import likecategory from "@/view/admin/likecategory";
import maritalstatus from "@/view/admin/maritalstatus";
import menu from "@/view/admin/menu";
import role from "@/view/admin/role";
import skill from "@/view/admin/skill";
import skillcategory from "@/view/admin/skillcategory";
import userlevel from "@/view/admin/userlevel";
import userstatus from "@/view/admin/userstatus";
import usertag from "@/view/admin/usertag";
import usertagcategory from "@/view/admin/usertagcategory";
import work from "@/view/admin/work";
import user from "@/view/admin/user";
import usermenu from "@/view/admin/usermenu";
import index from "@/view/admin/index";
import login from "@/view/admin/login";
Vue.use(Router);
const routerPush = Router.prototype.push;
Router.prototype.push = function push(location) {
  return routerPush.call(this, location).catch(error => {
    //console.log(error);
  })
};

export default new Router({
  routes: [
  { 
	path: "/login",
	name: "login",
	component: login,
  },
  {
    path: "/",
    name: "index",
    component: index,
    children: [{
      path: "/admin",
      name: "admin",
      component: admin
    }, {
      path: "/icon",
      name: "icon",
      component: icon
    }, {
      path: "/edutioncategory",
      name: "edutioncategory",
      component: edutioncategory
    }, {
      path: "/userfile",
      name: "userfile",
      component: file
    }, {
      path: "/filecategory",
      name: "filecategory",
      component: filecategory
    }, {
      path: "/jobcategory",
      name: "jobcategory",
      component: jobcategory
    }, {
      path: "/like",
      name: "like",
      component: like
    }, {
      path: "/likecategory",
      name: "likecategory",
      component: likecategory
    }, {
      path: "/maritalstatus",
      name: "maritalstatus",
      component: maritalstatus
    }, {
      path: "/menu",
      name: "menu",
      component: menu
    }, {
      path: "/role",
      name: "role",
      component: role
    }, {
      path: "/skill",
      name: "skill",
      component: skill
    }, {
      path: "/skillcategory",
      name: "skillcategory",
      component: skillcategory
    }, {
      path: "/userlevel",
      name: "userlevel",
      component: userlevel
    }, {
      path: "/userstatus",
      name: "userstatus",
      component: userstatus
    }, {
      path: "/usertag",
      name: "usertag",
      component: usertag
    }, {
      path: "/usertagcategory",
      name: "usertagcategory",
      component: usertagcategory
    }, {
      path: "/work",
      name: "work",
      component: work
    }, {
      path: "/user",
      name: "user",
      component: user
    }, {
      path: "/usermenu",
      name: "usermenu",
      component: usermenu
    }]
  }]
})
