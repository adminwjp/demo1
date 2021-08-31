<template>
  <div>
    <query
      ref="query" :options="options" :role_options="role_options"
      :formQuery="formQuery"
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
      :formSubmit="formSubmit"
      :admin_options="admin_options" :role_options="role_options"
      :test="test"
      :dialog="dialog"
      :formRoleValidator="formRoleValidator" ref="dialogForm"
    />
  </div>
</template>
<script>
import query from "../../components/admin/role/query";
import tableList from "../../components/admin/role/tableList";
import operator from "../../components/admin/operator";
import page from "../../components/admin/page";
import dialogForm from "../../components/admin/role/dialogForm";
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
  setAdmin,
  setRoleCategory
} from "../../js/base";
import "../../js/auth";
import { urls, getUrl } from "../../js/url";
var obj = new Gibson();
obj.el = ".role_list";
obj.data.operatorUrl = { add: urls.role.add, modify: urls.role.edit, delete: urls.role.delete, query: urls.role.query };
obj.data.formQuery = { id: "", category: "", role_id: "", create_date: [], update_date: [] };
obj.data.formSubmit = { id: "", category: "", role: { id: "" }, description: "这个人很懒,什么也没留下!" };
obj.data.controller = "role";
obj.data.isOperatorRequest = true;
obj.data.roleRequired = false;
obj.data.formRoleValidator = {
  category: [
    { required: true, message: "请输入角色名称", trigger: "blur" },
    { min: 2, max: 10, message: "长度在 2 到 10 个字符角色名称", trigger: "blur" }
  ],
  role: {
    id: {
      // 自定义时点击事件 无法触发
      validator: (rule, value, callback) => {
        if (obj.data.roleRequired) {
          if (!value || (value && value instanceof Array && typeof value[value.length - 1] != "number")) {
            callback(new Error("请选择角色"));
          } else {
            callback();
          }
        } else {
          callback();
        }
      },
      trigger: "change"
    }
  },
  description: [
    { required: true, message: "请输入描述", trigger: "blur" },
    { min: 10, max: 500, message: "长度在 10 到 500 个字符描述", trigger: "blur" }
  ]
};
// 覆盖此方法 重写
obj.methods.loadMounted = function () {
  var $this = this;
  get(urls.role.required, function (response) { $this.roleRequired = response.data.data; });
  this.queryCategoryEvent();
  this.roleQueryEvent();
  if (this.test) this.adminCategoryQueryEvent();
};
obj.methods.reload = function () {
  this.queryCategoryEvent();
  this.roleQueryEvent();
};
setRoleCategory(obj);
// 测试用的
setAdmin(obj);
// 覆盖此方法 重写
obj.methods.submitTextSelectChanngeEevent = function () {
  this.dialog.title = "查看角色信息";
  this.dialog.submitText = "预览角色信息";
  this.disabled = true;
};
// 覆盖此方法 重写
obj.methods.submitTextModifyChanngeEevent = function () {
  this.dialog.title = "编辑角色信息";
  this.dialog.submitText = "立即编辑";
  this.disabled = false;
};
// 覆盖此方法 重写
obj.methods.submitTextInsertChanngeEevent = function () {
  this.dialog.title = "添加角色信息";
  this.dialog.submitText = "立即创建";
  this.disabled = false;
};
// 覆盖此方法 重写
obj.methods.setValue = function (row) {
  for (var obj in this.formSubmit) {
    if (obj == "admin" || obj == "parent") continue;
    if (row[obj]) {
      this.formSubmit[obj] = row[obj];
    }
  }
  if (this.test && row.admin) { this.formSubmit.admin.id = row.admin.id; }
  if (row.parent) { this.formSubmit.role.id = row.parent.id; }
};
export default {
  name: "role",
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