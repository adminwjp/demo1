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
import query from "../../components/admin/filecategory/query";
import tableList from "../../components/admin/filecategory/tableList";
import operator from "../../components/admin/operator";
import page from "../../components/admin/page";
import dialogForm from "../../components/admin/filecategory/dialogForm";
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
obj.el = ".file_category_list";
obj.data.operatorUrl = { add: urls.filecategory.add, modify: urls.filecategory.edit, delete: urls.filecategory.delete, query: urls.filecategory.query };
obj.data.formQuery = { id: "", category: "", create_date: [], update_date: [] };
obj.data.formSubmit = { id: "", category: "", accept: "", description: "这个人很懒,什么也没留下!" };
obj.data.controller = "filecategory";
obj.data.isOperatorRequest = true;
obj.data.formRoleValidator = {
  category: [
    { required: true, message: "请输入文件分类名称", trigger: "blur" },
    { min: 2, max: 10, message: "长度在 2 到 10 个字符文件分类名称", trigger: "blur" }
  ],
  accept: [
    { required: true, message: "请输入文件后缀", trigger: "blur" },
    { min: 2, max: 50, message: "长度在 2 到 50 个字符文件后缀", trigger: "blur" }
  ],
  description: [
    { required: true, message: "请输入描述", trigger: "blur" },
    { min: 10, max: 500, message: "长度在 10 到 500 个字符文件分类描述", trigger: "blur" }
  ]
};
// 覆盖此方法 重写
obj.methods.loadMounted = function () {
  if (this.test) this.adminCategoryQueryEvent();
  this.queryCategoryEvent();
};
obj.methods.reload = function () {
  this.queryCategoryEvent();
};
// 测试用的
setAdmin(obj);
// 覆盖此方法 重写
obj.methods.submitTextSelectChanngeEevent = function () {
  this.dialog.title = "查看文件分类信息";
  this.dialog.submitText = "预览文件分类信息";
  this.disabled = true;
};
// 覆盖此方法 重写
obj.methods.submitTextModifyChanngeEevent = function () {
  this.dialog.title = "编辑文件分类信息";
  this.dialog.submitText = "立即编辑";
  this.disabled = false;
};
// 覆盖此方法 重写
obj.methods.submitTextInsertChanngeEevent = function () {
  this.dialog.title = "添加文件分类信息";
  this.dialog.submitText = "立即创建";
  this.disabled = false;
};
// 覆盖此方法 重写
obj.methods.setValue = function (row) {
  for (var obj in this.formSubmit) {
    if (obj.toString() == "admin") {
      continue;
    }
    if (row[obj]) this.formSubmit[obj] = row[obj];
  }
  if (this.test && row.admin) this.formSubmit.admin.id = row.admin.id;
};
export default {
  name: "filecategory",
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