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
obj.el = ".skill_category_list";
obj.data.operatorUrl = { add: urls.skillcategory.add, modify: urls.skillcategory.edit, delete: urls.skillcategory.delete, query: urls.skillcategory.query };
obj.data.formQuery = { id: "", category: "", create_date: [], update_date: [] };
obj.data.formSubmit = { id: "", category: "", description: "这个人很懒,什么也没留下!" };
obj.data.controller = "skillcategory";
obj.data.formRoleValidator = {
  category: [
    { required: true, message: "请输入技能分类名称", trigger: "blur" },
    { min: 2, max: 10, message: "长度在 2 到 10 个字符技能分类名称", trigger: "blur" }
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
obj.methods.queryCategoryEvent = function () {
  var $this = this;
  get(urls.skillcategory.category, response => { $this.options = response.data.data; });
};
// 测试用的
setAdmin(obj);
// 覆盖此方法 重写
obj.methods.submitTextSelectChanngeEevent = function () {
  this.dialog.title = "查看技能分类信息";
  this.disabled = true;
  this.dialog.submitText = "预览技能分类信息";
};
// 覆盖此方法 重写
obj.methods.submitTextModifyChanngeEevent = function () {
  this.dialog.title = "编辑技能分类信息";
  this.disabled = false;
  this.dialog.submitText = "立即编辑";
};
// 覆盖此方法 重写
obj.methods.submitTextInsertChanngeEevent = function () {
  this.dialog.title = "添加技能分类信息";
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
  name: "skillcategory",
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