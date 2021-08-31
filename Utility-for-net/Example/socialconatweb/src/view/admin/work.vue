<template>
  <div>
    <query
      ref="query" :options="options" :job_options="job_options"
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
      :user_options="user_options" :job_options="job_options"
      :test="test"
      :dialog="dialog"
      :formRoleValidator="formRoleValidator" ref="dialogForm"
    />
  </div>
</template>
<script>
import query from "../../components/admin/work/query";
import tableList from "../../components/admin/work/tableList";
import operator from "../../components/admin/operator";
import page from "../../components/admin/page";
import dialogForm from "../../components/admin/work/dialogForm";
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
  setUser
} from "../../js/base";
import "../../js/auth";
import { urls, getUrl } from "../../js/url";
var obj = new Gibson();
obj.el = ".work_list";
obj.data.operatorUrl = { add: urls.work.add, modify: urls.work.edit, delete: urls.work.delete, query: urls.work.query };
obj.data.formQuery = { id: "", company_name: "", job: "", category_id: "", options: [], create_date: [], update_date: [], work_date: [] };
obj.data.formSubmit = { id: "", company_name: "", job: "", category: { id: "" }, work_date: [], start_date: "", end_date: "", options: [], description: "这个人很懒,什么也没留下!" };
obj.data.controller = "work";
obj.data.work_date = [];
obj.data.formRoleValidator = {
  company_name: [
    { required: true, message: "请输入公司名称", trigger: "blur" },
    { min: 5, max: 50, message: "长度在 5 到 50 个字符公司名称", trigger: "blur" }
  ],
  job: [
    { required: true, message: "请输入公司职位", trigger: "blur" },
    { min: 2, max: 50, message: "长度在 2 到 50 个字符公司职位", trigger: "blur" }
  ],
  category: {
    id: [
      { required: true, message: "请选择公司分类", trigger: "change" }
    ]
  },
  work_date: {
    required: true,
    type: "array",
    message: "请选择工作时间",
    trigger: "change",
    fields: {
      0: { required: true, message: "请选择工作时间" },
      1: { required: true, message: "请选择工作时间" }
    }
  },
  description: [
    { required: true, message: "请输入描述", trigger: "blur" },
    { min: 10, max: 500, message: "长度在 10 到 500 个字公司符描述", trigger: "blur" }
  ]
};
obj.methods.quitOtherOperatorEvent = function () {
  this.formSubmit.start_date = this.formSubmit.work_date[0];
  this.formSubmit.end_date = this.formSubmit.work_date[1];
};
// 覆盖此方法 重写
obj.methods.loadMounted = function () {
  this.queryCategoryEvent();
  this.userCategoryQueryEvent();
  this.categoryCategoryQueryEvent();
};
obj.methods.reload = function () {
  this.queryCategoryEvent();
};
// 测试用的
setUser(obj);
obj.data.job_options = [];
obj.methods.categoryCategoryQueryEvent = function () {
  var $this = this;
  get(urls.jobcategory.category, function (response) {
    if (response.data.success) {
      $this.job_options = response.data.data;
    }
  });
};
obj.methods.handleWorkFormChangeEvent = function (val) {
  this.formSubmit.category.id = val;
};
obj.methods.handleWorkFormVisableChangeEvent = function (val) {
  if (val) {
    this.categoryCategoryQueryEvent();
  }
};
obj.methods.handleWorkQueryChangeEvent = function (val) {
  this.formQuery.category_id = val;
};
obj.methods.handleWorkQueryVisableChangeEvent = function (val) {
  if (val) {
    this.categoryCategoryQueryEvent();
  }
};
// 覆盖此方法 重写
obj.methods.submitTextSelectChanngeEevent = function () {
  this.dialog.title = "查看工作信息";
  this.disabled = true;
  this.dialog.submitText = "预览工作信息";
};
// 覆盖此方法 重写
obj.methods.submitTextModifyChanngeEevent = function () {
  this.dialog.title = "编辑工作信息";
  this.disabled = false;
  this.dialog.submitText = "立即编辑";
};
// 覆盖此方法 重写
obj.methods.submitTextInsertChanngeEevent = function () {
  this.dialog.title = "添加工作信息";
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
  this.formSubmit.work_date[0] = row.start_date;
  this.formSubmit.work_date[1] = row.end_date;
};
export default {
  name: "work",
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