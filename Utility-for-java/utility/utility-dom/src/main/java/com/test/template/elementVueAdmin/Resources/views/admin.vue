<template>
  <div>
    <query
      ref="query"
      :formQuery="formQuery"  :role_options="role_options"  :options="options"
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
      :role_options="role_options" :submit_url="submit_url"
      :dialog="dialog" :account_disabled="account_disabled" :head_pic="head_pic"
      :formRoleValidator="formRoleValidator" ref="dialogForm"
    />
  </div>
</template>
<script>
import query from "../../components/admin/admin/query";
import tableList from "../../components/admin/admin/tableList";
import operator from "../../components/admin/operator";
import page from "../../components/admin/page";
import dialogForm from "../../components/admin/admin/dialogForm";
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
  getBase64Image,
  dataURLtoFile,
  setRoleCategory,
  parseRequestParam
} from "../../js/base";
import "../../js/auth";
import { urls, getUrl } from "../../js/url";
var obj = new Gibson();
obj.el = ".admin_list";
obj.data.controller = "admin";
obj.data.isOperatorRequest = true;
obj.data.operatorUrl = { add: urls.admin.add, modify: urls.admin.edit, delete: urls.admin.delete, query: urls.admin.query };
obj.data.formQuery = { id: "", token: "", account: "", nick_name: "", birthday_date: [], role_id: "", real_name: "", phone: "", sex: "", email: "", login_date: [], create_date: [], update_date: [] };
obj.data.submit_url = obj.data.baseUrl+"file/upload";
obj.data.account_disabled = false;
obj.data.old_pwd = "";
obj.data.required_role = true;
obj.data.head_pic = "";
obj.data.head_pic_name = "";
    obj.data.formSubmit = {
            id: '', account: '', nick_name: '', role: { id: '' }, real_name: '', birthday: '', phone: '', sex: '', admin_pic: { file_id: '' }, email: '', description: '这个人很懒,什么也没留下!', password: '', enter_password: ''
        };
obj.data.formRoleValidator = {
  account: [
    { required: true, message: "请输入账户", trigger: "blur" },
    { min: 5, max: 15, message: "长度在 5 到 15 个字符账户", trigger: "blur" }
  ],
  nick_name: [
    { required: true, message: "请输入昵称", trigger: "blur" },
    { min: 2, max: 10, message: "长度在 2 到 10 个字符昵称", trigger: "blur" }
  ],
  role: {
    id: {
      validator: (rule, value, callback) => {
        if (obj.data.required_role) {
          if (!value) {
            callback(new Error("请选择角色"));
          } else {
            callback();
          }
        } else {
          callback();
        }
      },
      trigger: "blur"
    }
  },
  real_name: [
    { required: true, message: "请输入姓名", trigger: "blur" },
    { min: 2, max: 10, message: "长度在 2 到 10 个字符姓名", trigger: "blur" }
  ],
  user_file: {id: [
    { required: true, message: "请上传头像", trigger: "change" }
  ]},
  // https://blog.csdn.net/weixin_39897287/article/details/94442776
  birthday: { required: true, message: "请选择日期", trigger: "change" },
  phone: [
    { required: true, message: "请输入手机号", trigger: "blur" },
    { min: 11, max: 11, message: "长度在 11 个数字手机号", trigger: "blur" }
  ],
  sex: { required: true, message: "请选择性别", trigger: "change" },
  email: [
    { required: true, type: "email", message: "请输入邮箱", trigger: "blur" },
    { min: 10, max: 20, message: "长度在 10 到 20 个字符邮箱", trigger: "blur" }
  ],
  description: [
    { required: true, message: "请输入描述", trigger: "blur" },
    { min: 10, max: 500, message: "长度在 10 到 500 个字符描述", trigger: "blur" }
  ],
  password: [
    { validator: (rule, value, callback) => {
      if (!value) {
        callback(new Error("请输入密码"));
      } else if (obj.data.old_pwd != value && (value.length < 6 || value.length > 20)) {
        callback(new Error("长度在 6 到 20 个字符"));
      } else if (value.length < 6) {
        callback(new Error("长度在 6 个字符以上"));
      } else {
        callback();
      }
    },
    trigger: "blur" }
  ],
  enter_password: [
    { validator: (rule, value, callback) => {
      if (!value) {
        callback(new Error("请输入确认密码"));
      } else if (obj.data.old_pwd != value && (value.length < 6 || value.length > 20)) {
        callback(new Error("长度在 6 到 20 个字符"));
      } else if (value.length < 6) {
        callback(new Error("长度在 6 个字符以上"));
      } else if (obj.data.formSubmit.password !== value) {
        callback(new Error("两次输入密码不一致!"));
      } else {
        callback();
      }
    },
    trigger: "blur" }
  ]
};
obj.data.fileInfo = null;
obj.methods.reload = function () {
  this.queryCategoryEvent();
};
// 表单查询时 时间需要转换
obj.methods.queryTimeField = function () {
  if (this.request_content_type != 1) {
    // 时间转换
    return ["create_date", "update_date", "birthday_date", "login_date"];
  }
  return [];
};
// 提交表单事件 添加或编辑按钮提交事件
obj.methods.submitFormEvent = function (formName) {
  this.$refs.dialogForm.$refs[formName].validate((valid) => {
    if (valid) {
      this.upload(this.fileInfo);
      // var img = this.head_pic;
      // var image = new Image();
      // image.crossOrigin="anonymous"; //关键
      // image.src = img;
      // var $this = this;
      // image.onload = function () {
      //    var base64 = getBase64Image(image);
      //    var file =dataURLtoFile(base64,$this.head_pic_name);
      //    $this.upload(file);
      // }
      return true;
    } else {
      console.log("error submit!!");
      return false;
    }
  });
};
// 自定义上传操作
obj.methods.upload = function (file) {
  var paramOptions = parseRequestParam(3, this.formSubmit, undefined, ["birthday"]);
  if (file) {
    paramOptions.parem.append("head_pic", file.file ? file.file : file);
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
// 覆盖此方法 重写
obj.methods.loadMounted = function () {
  this.roleQueryEvent();
  this.queryCategoryEvent();
  var $this = this;
  get(urls.admin.role_required, function (response) { $this.required_role = response.data.data; });
};
obj.methods.onChange = function (file, fileList) {
  var $this = this;
  this.fileInfo = file;
  this.head_pic_name = file.name;
  if (window.createObjectURL != undefined) {
    $this.head_pic = window.createObjectURL(file.raw);
  } else if (window.URL != undefined) {
    $this.head_pic = window.URL.createObjectURL(file.raw);
  } else if (window.webkitURL != undefined) {
    $this.head_pic = window.webkitURL.createObjectURL(file.raw);
  }
};
// 角色分类查询事件
setRoleCategory(obj);
// 覆盖此方法 重写
obj.methods.submitTextSelectChanngeEevent = function () {
  this.dialog.title = "查看管理员信息";
  this.dialog.submitText = "预览管理员信息";
  this.disabled = true;
  this.head_pic = "";
};
// 覆盖此方法 重写
obj.methods.submitTextModifyChanngeEevent = function () {
  this.dialog.title = "编辑管理员信息";
  this.dialog.submitText = "立即编辑";
  this.submit_url = urls.admin.edit;
  this.disabled = false;
  this.account_disabled = true;
  this.head_pic = "";
};
// 覆盖此方法 重写
obj.methods.submitTextInsertChanngeEevent = function () {
  this.dialog.title = "添加管理员信息";
  this.dialog.submitText = "立即创建";
  this.submit_url = urls.admin.add;
  this.old_pwd = "";
  this.head_pic = "";
  this.disabled = false;
  this.account_disabled = false;
};
// 覆盖此方法 重写
obj.methods.setValue = function (row) {
console.log(this.baseUrl);
  for (var obj in this.formSubmit) {
    if (obj.toString() == "role" && row.role) {
      this.formSubmit[obj].id = row.role.id;
      continue;
    } else if (row[obj]) this.formSubmit[obj] = row[obj];
  }
  if (row.head_pic) {
    this.head_pic = this.baseUrl+"img/" + row.head_pic;
	this.formSubmit.admin_pic.file_id = row.head_pic;
  }
  this.formSubmit.enter_password = this.formSubmit["password"];
  this.old_pwd = this.formSubmit["password"];
};
export default {
  name: "admin",
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