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
    <tableList :tableData="tableData" ref="tableList"  :baseUrl="baseUrl" :loading="loading" :operator="operator" />
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
import query from "../../components/admin/icon/query";
import tableList from "../../components/admin/icon/tableList";
import operator from "../../components/admin/operator";
import page from "../../components/admin/page";
import dialogForm from "../../components/admin/icon/dialogForm";
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
obj.data.operatorUrl = {
  add: urls.icon.add,
  modify: urls.icon.edit,
  delete: urls.icon.delete,
  query: urls.icon.query
};
obj.data.formQuery = { id: "", name: "", create_date: [], update_date: [] };
obj.data.formSubmit = {
  id: "",
  name: "",
  style: "",
  description: "这个人很懒,什么也没留下!"
};
obj.data.controller = "icon";
obj.data.isOperatorRequest = true;
obj.data.formRoleValidator = {
  name: [
    { required: true, message: "请输入图标名称", trigger: "blur" },
    {
      min: 2,
      max: 10,
      message: "长度在 2 到 10 个字符图标名称",
      trigger: "blur"
    }
  ],
  style: { required: true, message: "请输入图标样式", trigger: "blur" },
  description: [
    { required: true, message: "请输入图标描述", trigger: "blur" },
    {
      min: 10,
      max: 500,
      message: "长度在 10 到 500 个字符图标描述",
      trigger: "blur"
    }
  ]
};
obj.methods.reload = function() {
  this.queryCategoryEvent();
};
// 覆盖此方法 重写
obj.methods.loadMounted = function() {
  this.queryCategoryEvent();
  if (this.test) this.adminCategoryQueryEvent();
};
// 测试用的
setAdmin(obj);
// 覆盖此方法 重写
obj.methods.submitTextSelectChanngeEevent = function() {
  this.dialog.title = "查看图标信息";
  this.dialog.submitText = "预览图标信息";
  this.disabled = true;
};
// 覆盖此方法 重写
obj.methods.submitTextModifyChanngeEevent = function() {
  this.dialog.title = "编辑图标信息";
  this.dialog.submitText = "立即编辑";
  this.disabled = false;
};
// 覆盖此方法 重写
obj.methods.submitTextInsertChanngeEevent = function() {
  this.dialog.title = "添加图标信息";
  this.dialog.submitText = "立即创建";
  this.disabled = false;
};
// 覆盖此方法 重写
obj.methods.setValue = function(row) {
  for (var obj in this.formSubmit) {
    if (obj.toString() == "admin") {
      continue;
    }
    if (row[obj]) this.formSubmit[obj] = row[obj];
  }
  if (this.test && row.admin) this.formSubmit.admin.id = row.admin.id;
};
export default {
  name: "icon",
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