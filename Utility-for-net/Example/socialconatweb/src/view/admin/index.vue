<template>
  <div v-loading.fullscreen.lock="fullscreenLoading">
    <el-container style="height: 100%;min-height: 100%;border: 1px solid #eee">
      <div :class="isActive ? 'el-aside' : 'hideSidebar'">
        <el-menu
          class="el-menu-vertical-demo"
          :default-openeds="openeds"
          :collapse="!isActive"
          @select="menuSelectChange"
        >
          <slidermenu
            v-if="menus && menus.length > 0"
            :menu_list="this.menus"
          />
        </el-menu>
      </div>
      <el-container>
        <el-header style="height:56px;">
          <div
            style="float: left; width: 100px;padding: 0 15px;"
            @click="toggleClick"
          >
            <svg
              :class="{ 'is-active': isActive }"
              class="hamburger"
              viewBox="0 0 1024 1024"
              xmlns="http://www.w3.org/2000/svg"
              width="64"
              height="64"
            >
              <path
                d="M408 442h480c4.4 0 8-3.6 8-8v-56c0-4.4-3.6-8-8-8H408c-4.4 0-8 3.6-8 8v56c0 4.4 3.6 8 8 8zm-8 204c0 4.4 3.6 8 8 8h480c4.4 0 8-3.6 8-8v-56c0-4.4-3.6-8-8-8H408c-4.4 0-8 3.6-8 8v56zm504-486H120c-4.4 0-8 3.6-8 8v56c0 4.4 3.6 8 8 8h784c4.4 0 8-3.6 8-8v-56c0-4.4-3.6-8-8-8zm0 632H120c-4.4 0-8 3.6-8 8v56c0 4.4 3.6 8 8 8h784c4.4 0 8-3.6 8-8v-56c0-4.4-3.6-8-8-8zM142.4 642.1L298.7 519a8.84 8.84 0 0 0 0-13.9L142.4 381.9c-5.8-4.6-14.4-.5-14.4 6.9v246.3a8.9 8.9 0 0 0 14.4 7z"
              />
            </svg>
          </div>
          <div
            style="float: right;width: 200px; text-align: right;font-size: 12px;"
          >
            <el-dropdown @click="userClick">
              <i class="el-icon-setting" style="margin-right: 15px"></i>
              <el-dropdown-menu slot="dropdown">
                <el-dropdown-item>查看</el-dropdown-item>
                <el-dropdown-item>新增</el-dropdown-item>
                <el-dropdown-item>退出</el-dropdown-item>
              </el-dropdown-menu>
            </el-dropdown>
            <span><i class="el-icon-caret-bottom" @click="userClick"></i></span>
          </div>
        </el-header>

        <el-main>
          <el-tabs
            v-model="activeName"
            closable
            @tab-remove="handleTableRemove"
            @tab-add="handleTableAdd"
            @tab-click="handleTableClick"
          >
            <el-tab-pane
              v-for="item in tabs"
              :key="item.name"
              :label="item.title"
              :name="item.id"
            >
              <span slot="label"  @contextmenu.prevent="closeCick">
                <i :class="item.style" style="padding-right:5px;"></i>{{ item.title }}
                </span>
            </el-tab-pane>
          </el-tabs>
          <section class="app-main">
            <transition name="fade-transform" mode="out-in">
              <router-view :key="this.$route.path"></router-view>
            </transition>
          </section>
        </el-main>
      </el-container>
    </el-container>
	<div id="right_context" style="display:none" >
            <div> <el-button type="primary" @click="closeCurrent">关闭当前</el-button></div>
            <div><el-button type="primary" @click="closeAll">关闭所有</el-button></div>
	</div>
  </div>
</template>
<script>
import {
  createDatePickerOptions,
  ContentTypeJson,
  ContentTypeForm,
  ContentTypeMultipart,
  get,
  post,
  resetFormEvent,
  tip
} from "../../js/base";

import "../../js/auth";
import { urls, getUrl } from "../../js/url";
import slidermenu from "../admin/navbar.vue";
export default {
  name: "index",
  components: { slidermenu },
  data: function() {
    return {
      openeds: [],
      menus: [],
      fullscreenLoading: true,
      isActive: true,
      activeName: "",
      tabs: [],
      tabIndex: 0
    };
  },
  watch: {
    $route(to, from) {
    }
  },
  computed: {
  },
  mounted: function() {
    this.$router.push("/");
    this.menuCategory();
  },
  methods: {
	closeCick(val) {
		//console.log(val);
	   // console.log(val.path[1].id);
		this.activeName = val.path[1].id.replace("tab-", "");
		//var id = "div#" + val.path[1].id;
		//没办法定位  定位话需要多次 在相对定位内绝对定位 麻烦 
		document.querySelector("div#right_context").style.display = "block";
		document.querySelector("div#right_context").style.top = // (document.querySelector(id).clientTop + document.querySelector(id).clientHeight) + "px";
		(val.clientY+20)+"px";
		document.querySelector("div#right_context").style.left =//( document.querySelector(id).clientLeft + (document.querySelector(id).clientWidth - 64) / 2) + "px";
			val.clientX + "px";
		//显示后才有效
		document.querySelector("div#right_context").onmouseleave = function(){
		   document.querySelector("div#right_context").style.display = "none";
		};
	},
	closeCurrent (val) {
		console.log(val);
		this.handleTableRemove(this.activeName);
		document.querySelector("div#right_context").style.display = "none";
	},
	closeAll() {
		this.tabs = [];
		document.querySelector("div#right_context").style.display = "none";
		this.$router.push("/");
	},
    handleTableRemove(val) {
      this.clostTab(val);
    },
    handleTableAdd(val) {},
    // 菜单点击事件
    menuSelectChange(index, indexPath) {
      console.log(index + "  " + indexPath);
      var strs = indexPath.toString().split(",");
      var title = strs[strs.length - 3];
      var icon = strs[strs.length - 2];
      var href = index.toString().split(",")[2];
               console.log(href);
      for (var item in this.tabs) {
        if (this.tabs[item].href == href) {
          this.activeName = href.split("/")[1];
          this.$router.push("/" + this.activeName);
          return;
        }
      }
      let newTabName = ++this.tabIndex + "";
      this.activeName = href.split("/")[1];
      // console.log(this.activeName);
      this.$router.push(this.activeName);
      // console.log(this.$router);
      this.tabs.push({
        style: icon,
        title: title,
        href: href,
        id: this.activeName,
        name: newTabName
      });
    },
    clostTab(val) {
      console.log(val);
      var name = "";
      var $this = this;
      for (var i = 0; i < this.tabs.length; i++) {
        if (this.tabs[i].id === val) {
          this.tabs.splice(i, 1);
          break;
        }
      }
      if (this.tabs.length > 0) name = this.tabs[this.tabs.length - 1].id;
      //  console.log(this.tabs.length + " " + name);
      name = name && name != "" ? "/" + name : "/";
      // console.log(name);
      // this.$router.replace(name);
      this.$router.push(name);
    },
    // tab处理
    handleTableClick(tab, event) {
      // console.log(this.activeName + "3333");
      this.$router.push(this.activeName);
    },
    userClick() {},
    toggleClick() {
      this.isActive = !this.isActive;
    },
    queryNavbar() {},
    // 菜单信息
    menuCategory() {
      var $this = this;
      get(urls.menu.category, response => {
        $this.menus = response.data.data;
        $this.$nextTick(() => {
          if (this.menus && this.menus.length > 0) {
            setTimeout(() => {
              this.fullscreenLoading = false;
            }, 500);
          }
        });
      });
    }
  }
};
</script>
<style scoped>
.app-main{
margin-top: 50px;
}

.el-header {
  background-color: #b3c0d1;
  color: #333;
  line-height: 56px;
}
.hamburger {
  display: inline-block;
  vertical-align: middle;
  width: 20px;
  height: 20px;
}

.hamburger.is-active {
  transform: rotate(180deg);
}

.el-aside {
  transition: 0.3s;
  width: 200px;
}

.hideSidebar {
  transition: 0.3s;
  width: 64px;
}
.el-aside,
.hideSidebar {
  color: #333;
  min-height: 400px;
  background-color: rgb(238, 241, 246);
  overflow: hidden;
}
.el-menu-vertical-demo {
  background-color: rgb(238, 241, 246);
}
.el-menu-vertical-demo:not(.el-menu--collapse) {
  width: 200px;
  min-height: 400px;
  height: 100%;
}
 #right_context{
	position:absolute;
	/*opacity:0.5;*/
	width:80px;
	text-align:center;
	z-index:999;
	background-color:#B3C0D1;
	top:0;
	left:0;
}
</style>
