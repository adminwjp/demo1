<template>
  <div>
    <query
      ref="query" :options="options"
      :formQuery="formQuery"
      @loadTableDataOrQueryDataEvent="loadTableDataOrQueryDataEvent" :createDatePickerOptions="createDatePickerOptions"
    />
    <operator
      :checkboxAllSelect="checkboxAllSelect"
      :tableData="tableData"
      :checkboxAllSelectText="checkboxAllSelectText"
      :operator="operator"
    />
    <tableList :tableData="tableData" ref="tableList" :baseUrl="baseUrl"  :loading="loading" :operator="operator" />
    <page :page="page" />
    <dialogForm
      :disabled="disabled"
      :formSubmit="formSubmit"
      :user_options="user_options" :category_options="category_options"
      :test="test"
      :dialog="dialog"
      :formRoleValidator="formRoleValidator" ref="dialogForm"
    />
  </div>
</template>
<script>
import query from "../../components/admin/like/query";
import tableList from "../../components/admin/like/tableList";
import operator from "../../components/admin/operator";
import page from "../../components/admin/page";
import dialogForm from "../../components/admin/like/dialogForm";
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
obj.el = ".like_list";
obj.data.operatorUrl = { add: urls.like.add, modify: urls.like.edit, delete: urls.like.delete, query: urls.like.query };
obj.data.formQuery = { id: "", category: "", create_date: [], update_date: [] };
obj.data.formSubmit = { id: "", category: { id: "" }, user: {id: ""} };
obj.data.controller = "like";
obj.data.options = [];
obj.data.isOperatorRequest = true;
obj.data.formRoleValidator = {

};
// 覆盖此方法 重写
obj.methods.loadMounted = function () {
  this.queryCategoryEvent();
  this.userCategoryQueryEvent();
  this.likeCategoryQueryEvent();
};
obj.methods.reload = function () {
  this.queryCategoryEvent();
};
// 测试用的
// if (obj.data.test) {
obj.data.formSubmit.user = { id: "" };
obj.data.user_options = [];
obj.methods.userCategoryQueryEvent = function () {
  var $this = this;
  get(urls.user.category, function (response) {
    if (response.data.success) {
      $this.user_options = response.data.data;
    }
  });
};
obj.methods.handleUserChangeEvent = function (val) {
  this.formSubmit.user.id = val;
};
obj.methods.handleUserVisableChangeEvent = function (val) {
  if (val) {
    this.userCategoryQueryEvent();
  }
};
// }
obj.data.category_options = [];
obj.methods.likeCategoryQueryEvent = function () {
  var $this = this;
  get(urls.likecategory.category, function (response) {
    if (response.data.success) {
      $this.category_options = response.data.data;
    }
  });
};
obj.methods.handleLikeCategoryChangeEvent = function (val) {
  this.formSubmit.category.id = val;
};
obj.methods.handleLikeCategoryVisableChangeEvent = function (val) {
  if (val) {
    this.likeCategoryQueryEvent();
  }
};
// 覆盖此方法 重写
obj.methods.submitTextSelectChanngeEevent = function () {
  this.dialog.title = "查看爱好信息";
  this.disabled = true;
  this.dialog.submitText = "预览爱好信息";
};
// 覆盖此方法 重写
obj.methods.submitTextModifyChanngeEevent = function () {
  this.dialog.title = "编辑爱好信息";
  this.disabled = false;
  this.dialog.submitText = "立即编辑";
};
// 覆盖此方法 重写
obj.methods.submitTextInsertChanngeEevent = function () {
  this.dialog.title = "添加爱好信息";
  this.disabled = false;
  this.dialog.submitText = "立即创建";
};
// 覆盖此方法 重写
obj.methods.setValue = function (row) {
  for (var obj in this.formSubmit) {
    let str = obj.toString();
    if (str == "category" || str == "user") {
      continue;
    }
    if (row[obj]) this.formSubmit[obj] = row[obj];
  }
  if (this.test && row.user) this.formSubmit.user.id = row.user.id;
  if (row.category) this.formSubmit.category.id = row.category.id;
};
export default {
  name: "like",
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
  width: 60%;
  margin: 0px auto;
}

</style>