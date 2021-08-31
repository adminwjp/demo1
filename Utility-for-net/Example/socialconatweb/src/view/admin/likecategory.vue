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
    <tableList :tableData="tableData" ref="tableList" :baseUrl="baseUrl" :loading="loading" :operator="operator" />
    <page :page="page" />
    <dialogForm
      :disabled="disabled"
      :formSubmit="formSubmit"
      :admin_options="admin_options"
      :test="test"
      :dialog="dialog"
      :formRoleValidator="formRoleValidator" ref="dialogForm"
    />
  </div>
</template>
<script>
import query from "../../components/admin/likecategory/query";
import tableList from "../../components/admin/likecategory/tableList";
import operator from "../../components/admin/operator";
import page from "../../components/admin/page";
import dialogForm from "../../components/admin/likecategory/dialogForm";
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
obj.el = ".like_category_list";
obj.data.operatorUrl = { add: urls.likecategory.add, modify: urls.likecategory.edit, delete: urls.likecategory.delete, query: urls.likecategory.query };
obj.data.formQuery = { id: "", category: "", create_date: [], update_date: [] };
obj.data.formSubmit = { id: "", category: "", description: "这个人很懒,什么也没留下!" };
obj.data.controller = "likecategory";
obj.data.options = [];
obj.data.isOperatorRequest = true;
obj.data.formRoleValidator = {
  category: [
    { required: true, message: "请输入爱好分类名称", trigger: "blur" },
    { min: 2, max: 10, message: "长度在 2 到 10 个字符爱好分类名称", trigger: "blur" }
  ],
  description: [
    { required: true, message: "请输入描述", trigger: "blur" },
    { min: 10, max: 500, message: "长度在 10 到 500 个字符描述", trigger: "blur" }
  ]
};
// 覆盖此方法 重写
obj.methods.loadMounted = function () {
  this.queryCategoryEvent();
  if (this.test) this.adminCategoryQueryEvent();
};
obj.methods.reload = function () {
  this.queryCategoryEvent();
};
// 测试用的
setAdmin(obj);
// 覆盖此方法 重写
obj.methods.submitTextSelectChanngeEevent = function () {
  this.dialog.title = "查看爱好分类信息";
  this.disabled = true;
  this.dialog.submitText = "预览爱好分类信息";
};
// 覆盖此方法 重写
obj.methods.submitTextModifyChanngeEevent = function () {
  this.dialog.title = "编辑爱好分类信息";
  this.disabled = false;
  this.dialog.submitText = "立即编辑";
};
// 覆盖此方法 重写
obj.methods.submitTextInsertChanngeEevent = function () {
  this.dialog.title = "添加爱好分类信息";
  this.disabled = false;
  this.dialog.submitText = "立即创建";
};
// 覆盖此方法 重写
obj.methods.setValue = function (row) {
  for (var obj in this.formSubmit) {
    let str = obj.toString();
    if (str == "admin" || str == "parent") {
      continue;
    }
    if (row[obj]) this.formSubmit[obj] = row[obj];
  }
  if (this.test && row.admin) this.formSubmit.admin.id = row.admin.id;
};
export default {
  name: "likecategory",
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
  width: 70%;
  margin: 0px auto;
}

</style>