<template>
  <div>
    <query
      ref="query" :options="options"
      :formQuery="formQuery" :category_options="category_options" 
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
       :submit_url="submit_url" :file="file" :category_options="category_options"
      :formRoleValidator="formRoleValidator" ref="dialogForm"
    />
  </div>
</template>
<script>
import query from "../../components/admin/file/query";
import tableList from "../../components/admin/file/tableList";
import operator from "../../components/admin/operator";
import page from "../../components/admin/page";
import dialogForm from "../../components/admin/file/dialogForm";
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
  getBase64Image,
  dataURLtoFile,
  parseRequestParam
} from "../../js/base";
import "../../js/auth";
import { urls, getUrl } from "../../js/url";
var obj = new Gibson();
obj.el = ".file_list";
obj.data.operatorUrl = { add: urls.file.add, modify: urls.file.edit, delete: urls.file.delete, query: urls.file.query };
obj.data.formQuery = { id: "", category_id: "", src: "", create_date: [], update_date: [] };
obj.data.formSubmit = { id: "", category: { id: "" }, src: "", description: "这个人很懒,什么也没留下!" };
obj.data.category_options = [];
obj.data.controller = "file";
obj.data.isOperatorRequest = true;
obj.data.formRoleValidator = {
  file_category: {
    id: [
      { required: true, message: "请选择文件分类", trigger: ["blur", "channge"] }
    ]
  },
  description: [
    { required: true, message: "请输入文件描述", trigger: "blur" },
    { min: 10, max: 500, message: "长度在 10 到 500 个字符文件描述", trigger: "blur" }
  ]
};
obj.data.fileInfo = null;
// 自定义上传操作
obj.methods.upload = function (file) {
  var paramOptions = parseRequestParam(3, this.formSubmit);
  if (file) {
    paramOptions.parem.append("file", file.raw ? file.raw : file.file ? file.file : file);
    this.fileInfo = null;
  }
  var $this = this;
  post(this.submit_url, paramOptions.parem, paramOptions.contenType, response => {
    $this.dialog.visible = false;
    if (response.data.success) {
      $this.$nextTick(() => {
        $this.dialog.visible = false;
        $this.loadTableDataOrQueryDataEvent(); // 重新加载列表数据
        $this.reload();
      });
    }
  }, (response) => { $this.tip(response.data.message, "提示：", "确定", "info", "确定!", "关闭!"); });
};
// 提交表单事件 添加或编辑按钮提交事件
obj.methods.submitFormEvent = function (formName) {
  this.$refs.dialogForm.$refs[formName].validate((valid) => {
    if (valid) {
      this.upload(this.fileInfo);
      // var img = this.file;
      // var image = new Image();
      // image.crossOrigin = "anonymous"; //关键
      // image.src = img;
      // var $this = this;
      // image.onload = function () {
      //    //这样就获取到了文件的Base64字符串
      //    var base64 = getBase64Image(image);
      //    //Base64字符串转二进制
      //    //var file = dataURLtoBlob(base64);
      //    var file = dataURLtoFile(base64, $this.file_name);
      //    $this.upload(file);
      // }
      return true;
    } else {
      console.log("error submit!!");
      return false;
    }
  });
};
obj.methods.handleAvatarSuccess = function (res, file) {
  this.file = this.baseUrl+"img/" + res.id;
  this.src = res.id;
  this.tip(res.message, "提示：", "确定", "info", "确定!", "关闭!");
};
// 文件上传操作
obj.data.submit_url = obj.data.baseUrl+"file/upload";
obj.data.file = "";
obj.data.file_name = "";
obj.methods.handleUploadSuccess = function (file) {
  var $this = this;
  this.fileInfo = file;
  this.file_name = file.name;
  if (window.createObjectURL != undefined) {
    $this.file = window.createObjectURL(file.raw);
  } else if (window.URL != undefined) {
    $this.file = window.URL.createObjectURL(file.raw);
  } else if (window.webkitURL != undefined) {
    $this.file = window.webkitURL.createObjectURL(file.raw);
  }
  this.fileChange = true;
};
// 覆盖此方法 重写
obj.methods.loadMounted = function () {
  this.fileCategoryQueryEvent();
  if (this.test) this.adminCategoryQueryEvent();
  this.queryCategoryEvent();
};
obj.methods.reload = function () {
  this.queryCategoryEvent();
};

// 测试用的
setAdmin(obj);
// 查询文件分类信息
obj.methods.fileCategoryQueryEvent = function () {
  var $this = this;
  get(urls.filecategory.category, response => {
    $this.category_options = response.data.data;
  });
};
// 下拉框选项值改变事件
obj.methods.handleFileCategoryQueryChangeEvent = function (value) {
  this.formQuery.category_id = value;
};
// 下拉框选项值改变事件
obj.methods.handleFileCategoryFormChangeEvent = function (value) {
  this.formSubmit.category.id = value;
};
// 下拉框显示隐藏改变事件
obj.methods.handleFileCategoryVisableChangeEvent = function (visable) {
  if (visable) {
    this.fileCategoryQueryEvent();
  }
};
// 覆盖此方法 重写
obj.methods.submitTextSelectChanngeEevent = function () {
  this.dialog.title = "查看文件信息";
  this.dialog.submitText = "预览文件信息";
  this.disabled = true;
};
// 覆盖此方法 重写
obj.methods.submitTextModifyChanngeEevent = function () {
  this.dialog.title = "编辑文件信息";
  this.dialog.submitText = "立即编辑";
  this.submit_url = urls.file.edit;
  this.disabled = false;
};
// 覆盖此方法 重写
obj.methods.submitTextInsertChanngeEevent = function () {
  this.dialog.title = "添加文件信息";
  this.dialog.submitText = "立即创建";
  this.submit_url = urls.file.add;
  this.file = "";
  this.file_name = "";
  this.disabled = false;
};
// 覆盖此方法 重写
obj.methods.setValue = function (row) {
  for (var obj in this.formSubmit) {
    if (obj.toString() == "admin" || obj.toString() == "user" || obj.toString() == "category") {
      continue;
    }
    if (row[obj]) this.formSubmit[obj] = row[obj];
  }
  if (this.test && row.admin) this.formSubmit["admin"].id = row["admin"].id;
  if (row["category"]) this.formSubmit["category"].id = row["category"].id;
  if (row.file_id) {
    this.file = this.baseUrl+"file/get/" + row.file_id;
    this.file_name = row.src;
    this.formSubmit["src"] = row.file_id;
  }
};
export default {
  name: "file",
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