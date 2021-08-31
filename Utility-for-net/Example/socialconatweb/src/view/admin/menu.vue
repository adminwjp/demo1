<template>
  <div>
    <query
      ref="query" :options="options"
      :formQuery="formQuery" :parent_options="parent_options"
      @loadTableDataOrQueryDataEvent="loadTableDataOrQueryDataEvent" :createDatePickerOptions="createDatePickerOptions"
    />
    <operator
      :checkboxAllSelect="checkboxAllSelect"
      :tableData="tableData"
      :checkboxAllSelectText="checkboxAllSelectText"
      :operator="operator"
    />
    <tableList :tableData="tableData" :baseUrl="baseUrl" ref="tableList" :loading="loading" :operator="operator" />
    <page :page="page" />
    <dialogForm
      :disabled="disabled"
      :formSubmit="formSubmit" :options="options"
      :admin_options="admin_options" :icon_options="icon_options"
      :test="test"
      :dialog="dialog"
      :formRoleValidator="formRoleValidator" ref="dialogForm"
    />
  </div>
</template>
<script>
import query from "../../components/admin/menu/query";
import tableList from "../../components/admin/menu/tableList";
import operator from "../../components/admin/operator";
import page from "../../components/admin/page";
import dialogForm from "../../components/admin/menu/dialogForm";
import {
  createDatePickerOptions,
  ContentTypeJson,
  ContentTypeForm,
  ContentTypeMultipart,
  get,
  post,
  resetFormEvent,
  tip,
  Gibson,
  setAdmin
} from "../../js/base";
import "../../js/auth";
import { urls, getUrl } from "../../js/url";
var obj = new Gibson();
obj.el = ".menu_list";
obj.data.operatorUrl = { add: urls.menu.add, modify: urls.menu.edit, delete: urls.menu.delete, query: urls.menu.query };
obj.data.formQuery = { id: "", admin_id: "", menu_name: "", menu_group: "", parent_id: "", create_date: [], update_date: [] };
obj.data.formSubmit = { id: "", menu_name: "", menu_group: "", href: "", parent: {id: ""}, icon: {id: ""}, description: "这个人很懒,什么也没留下!" };
obj.data.controller = "menu";
obj.data.parent_options = [];
obj.data.icon_options = [];
obj.data.isOperatorRequest = true;
obj.data.formRoleValidator = {
  category: [
    { required: true, message: "请输入分类名称", trigger: "blur" },
    { min: 2, max: 10, message: "长度在 2 到 10 个字符分类名称", trigger: "blur" }
  ],
  description: [
    { required: true, message: "请输入描述", trigger: "blur" },
    { min: 10, max: 500, message: "长度在 10 到 500 个字符描述", trigger: "blur" }
  ]
};
// 覆盖此方法 重写
obj.methods.loadMounted = function () {
  this.queryIconCategoryEvent();
  this.queryCategoryEvent();
  this.queryParentCategoryEvent();
  if (this.test) this.adminCategoryQueryEvent();
};
// 图标信息
obj.methods.queryIconCategoryEvent = function () {
  var $this = this;
  get(urls.icon.category, response => { $this.icon_options = response.data.data; });
};
// 测试用的
setAdmin(obj);

// 父菜单分类信息
obj.methods.queryParentCategoryEvent = function () {
  var $this = this;
  get(urls.menu.parent_category, response => { $this.parent_options = response.data.data; });
};
// 图标分类改变值时触发事件
obj.methods.handleIconChangeEvent = function (val) {
  this.formSubmit.icon.id = val;
};
// 图标分类打开时触发事件
obj.methods.handleIconVisableChangeEvent = function (val) {
  if (val) {
    this.queryIconCategoryEvent();
  }
};
// 父菜单分类改变值时触发事件
obj.methods.handleParentCategoryQueryChangeEvent = function (val) {
  this.formQuery.parent_id = val;
};
// 父菜单分类打开时触发事件
obj.methods.handleParentCategoryQueryVisableChangeEvent = function (val) {
  if (val) {
    this.queryParentCategoryEvent();
  }
};
// 菜单分类改变值时触发事件
obj.methods.handleCategoryFormChangeEvent = function (val) {
  if (val instanceof Array) {
    this.formSubmit.parent.id = val[val.length - 1];
  } else {
    this.formSubmit.parent.id = val;
  }
};
// 菜单分类点击触发事件
obj.methods.handleCategotyFormClickEvent = function (val) {
	this.handleCategoryFormChangeEvent(val);
	 if (val) {
		if (this.$refs.dialogForm.$refs.refHandleForm) {
			this.$refs.dialogForm.$refs.refHandleForm.dropDownVisible = false; //监听值发生变化就关闭它
		}
	}
};
// 菜单分类点击触发事件
obj.methods.handleCategotyQueryClickEvent = function (val) {
  this.handleCategoryQueryChangeEvent(val);
};
// 覆盖此方法 重写
obj.methods.submitTextSelectChanngeEevent = function () {
  this.dialog.title = "查看菜单信息";
  this.dialog.submitText = "预览菜单信息";
  this.disabled = true;
};
// 覆盖此方法 重写
obj.methods.submitTextModifyChanngeEevent = function () {
  this.dialog.title = "编辑菜单信息";
  this.dialog.submitText = "立即编辑";
  this.disabled = false;
};
// 覆盖此方法 重写
obj.methods.submitTextInsertChanngeEevent = function () {
  this.dialog.title = "添加菜单信息";
  this.dialog.submitText = "立即创建";
  this.disabled = false;
};
// 覆盖此方法 重写
obj.methods.setValue = function (row) {
  for (var obj in this.formSubmit) {
    if (obj.toString() == "admin" || obj.toString() == "icon" || obj.toString() == "parent" || obj.toString() == "children") {
      continue;
    }
    this.formSubmit[obj] = row[obj];
  }
  if (row.icon) this.formSubmit["icon"].id = row.icon.id;
  if (row.parent) this.formSubmit["parent"].id = row.parent.id;
};
export default {
  name: "menus",
  components: { query, operator, tableList, page, dialogForm },
  props: {},
  data: function() {
    return obj.data;
  },
  mounted: function() {
    obj.mounted(this); // 该方式作用域变了
  },
  methods: obj.methods
};
</script>
<style scoped>
.query,.operator,.table{
  width: 90%;
  margin: 0px auto;
}

</style>