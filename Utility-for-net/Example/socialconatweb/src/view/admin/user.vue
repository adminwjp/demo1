<template>
  <div>
    <query
      ref="query" :options="options"
      :edution_options="edution_options" :level_options="level_options" :marital_options="marital_options"
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
    <dialogForm :baseUrl="baseUrl"
      :disabled="disabled" :data_head_pic="data_head_pic" :data_card_photo1="data_card_photo1" :data_card_photo2="data_card_photo2"
      :data_hand_card_photo1="data_hand_card_photo1" :data_hand_card_photo2="data_hand_card_photo2"
      :formSubmit="formSubmit"
      :status_options="status_options" :edution_options="edution_options" :level_options="level_options" :marital_options="marital_options"
      :test="test"
      :dialog="dialog"
      :formRoleValidator="formRoleValidator" ref="dialogForm"
    />
  </div>
</template>
<script>
import query from "../../components/admin/user/query";
import tableList from "../../components/admin/user/tableList";
import operator from "../../components/admin/operator";
import page from "../../components/admin/page";
import dialogForm from "../../components/admin/user/dialogForm";
import {
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
obj.el = ".user_list";
obj.data.data_head_pic = { type: "head_pic" };
obj.data.data_card_photo1 = { type: "card_photo1" };
obj.data.data_card_photo2 = { type: "card_photo2" };
obj.data.data_hand_card_photo1 = { type: "hand_card_photo1" };
obj.data.data_hand_card_photo2 = { type: "hand_card_photo2" };
obj.data.controller = "user";
obj.data.isOperatorRequest = true;
obj.data.operatorUrl = { add: urls.user.add, modify: urls.user.edit, delete: urls.user.delete, query: urls.user.query };
obj.data.formQuery = {
  id: "",
  token: "",
  account: "",
  nick_name: "",
  birthday_date: [],
  card_id: "",
  real_name: "",
  phone: "",
  sex: "",
  email: "",
  bank_id: "",
  status_id: "",
  login_ip: "",
  level_id: "",
  login_date: [],
  create_date: [],
  update_date: []
};
obj.data.old_pwd = "";
obj.data.formSubmit = {
  id: "",
  account: "",
  nick_name: "",
  real_name: "",
  head_pic: { file_id: "" },
  birthday: "",
  phone: "",
  sex: "",
  email: "",
  card_id: "",
  bank_id: "",
  card_photo1: { file_id: "" },
  card_photo2: { file_id: "" },
  hand_card_photo1: { file_id: "" },
  hand_card_photo2: { file_id: "" },
  description: "这个人很懒,什么也没留下!",
  password: "",
  enter_password: "",
  edution: { id: "" },
  marital: {id: ""},
  level: {id: ""}
};
obj.data.formRoleValidator = {
  account: [
    { required: true, message: "请输入账户", trigger: "blur" },
    { min: 5, max: 15, message: "请输入长度在 5 到 15 个字符账户", trigger: "blur" }
  ],
  nick_name: [
    { required: true, message: "请输入昵称", trigger: "blur" },
    { min: 2, max: 10, message: "请输入长度在 2 到 10 个字符昵称", trigger: "blur" }
  ],
  real_name: [
    { required: true, message: "请输入姓名", trigger: "blur" },
    { min: 2, max: 10, message: "长度在 2 到 10 个字符姓名", trigger: "blur" }
  ],
  head_pic: {
    file_id: [
      { required: true, message: "请上传头像", trigger: "change" }
    ]
  },
  // https://blog.csdn.net/weixin_39897287/article/details/94442776
  birthday: { required: true, message: "请选择日期", trigger: "change" },
  phone: [
    { required: true, message: "请输入手机号", trigger: "blur" },
    { min: 11, max: 11, message: "长度在 11 个数字手机号", trigger: "blur" }
  ],
  sex: { required: true, message: "请选择性别", trigger: "change" },
  edution: {
    id:
                    { required: true, message: "请选择学历", trigger: "change" }
  },
  marital: {
    id:
                    { required: true, message: "请选择婚姻状态", trigger: "change" }
  },
  email: [
    { required: true, type: "email", message: "请输入邮箱", trigger: "blur" },
    { min: 10, max: 20, message: "长度在 10 到 20 个字符邮箱", trigger: "blur" }
  ],
  card_id: [
    { required: true, message: "请输入身份证号", trigger: "blur" },
    { min: 17, max: 18, message: "请输入长度在 17 到 18 个字符身份证号", trigger: "blur" }
  ],
  card_photo1: { file_id: { required: true, message: "请上传身份证正面", trigger: "change" } },
  card_photo2: { file_id: { required: true, message: "请上传身份证反面", trigger: "change" } },
  hand_card_photo1: { file_id: { required: true, message: "请上传手持身份证正面", trigger: "change" } },
  hand_card_photo2: {file_id: { required: true, message: "请上传手持身份证反面", trigger: "change" }},
  bank_id: [
    { required: true, message: "请输入银行卡号", trigger: "blur" },
    { min: 20, max: 20, message: "请输入长度在 20 位数字银行卡号", trigger: "blur" }
  ],
  description: [
    { required: true, message: "请输入描述", trigger: "blur" },
    { min: 10, max: 500, message: "长度在 10 到 500 个字符描述", trigger: "blur" }
  ],
  level: {id: { required: true, message: "请选择用户等级", trigger: "change" }},
  password: [
    {
      validator: (rule, value, callback) => {
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
      trigger: "blur"
    }
  ],
  enter_password: [
    {
      validator: (rule, value, callback) => {
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
      trigger: "blur"
    }
  ]
};
// 覆盖此方法 重写
obj.methods.loadMounted = function () {
  this.queryCategoryEvent();
  this.queryEdutionEvent();
  this.queryMaritalEvent();
  this.queryLevelEvent();
  this.queryStatusEvent();
};
// 取消其他操作
obj.methods.quitOtherOperatorEvent = function () {
  this.$refs.dialogForm.$refs.uploadHeadPic.abort();
  this.$refs.dialogForm.$refs.uploadCardPhoto1.abort();
  this.$refs.dialogForm.$refs.uploadCardPhoto2.abort();
  this.$refs.dialogForm.$refs.uploadHandCardPhoto1.abort();
  this.$refs.dialogForm.$refs.uploadHandCardPhoto2.abort();
};
function setEdution(obj) {
  obj.data.edution_options = [];
  obj.methods.queryEdutionEvent = function () {
    var $this = this;
    get(urls.edutioncategory.category, response => { $this.edution_options = response.data.data; });
  };
  obj.methods.handleEdutionCategoryChangeEvent = function (val) {
    if (this.dialog.visiable) {
      this.formSubmit.edution.id = val;
    } else {
      this.formQuery.edution_id = val;
    }
  };
  obj.methods.handleEdutionCategoryVisableChangeEvent = function (val) {
    if (val) this.queryEdutionEvent();
  };
}
setEdution(obj);
function setMarital(obj) {
  obj.data.marital_options = [];
  obj.methods.queryMaritalEvent = function () {
    var $this = this;
    get(urls.usermaritalstatus.category, response => { $this.marital_options = response.data.data; });
  };
  obj.methods.handleMaritalCategoryChangeEvent = function (val) {
    if (this.dialog.visiable) {
      this.formSubmit.marital.id = val;
    } else {
      this.formQuery.marital_id = val;
    }
  };
  obj.methods.handleMaritalCategoryVisableChangeEvent = function (val) {
    if (val) this.queryMaritalEvent();
  };
}
setMarital(obj);
function setLevel(obj) {
  obj.data.level_options = [];
  obj.methods.queryLevelEvent = function () {
    var $this = this;
    get(urls.userlevel.category, response => { $this.level_options = response.data.data; });
  };
  obj.methods.handleLevelCategoryChangeEvent = function (val) {
    if (this.dialog.visiable) {
      this.formSubmit.level.id = val;
    } else {
      this.formQuery.level_id = val;
    }
  };
  obj.methods.handleLevelCategoryVisableChangeEvent = function (val) {
    if (val) this.queryLevelEvent();
  };
}
setLevel(obj);
function setStatus(obj) {
  obj.data.status_options = [];
  obj.methods.queryStatusEvent = function () {
    var $this = this;
    get(urls.userstatus.category, response => { $this.status_options = response.data.data; });
  };
  obj.methods.handleStatusCategoryChangeEvent = function (val) {
    this.formQuery.status_id = val;
  };
  obj.methods.handleStatusCategoryVisableChangeEvent = function (val) {
    if (val) this.queryStatusEvent();
  };
}
setStatus(obj);
// 文件上传操作
obj.methods.handleRemove = function (file, fileList) {
  console.log(file, fileList);
};
obj.methods.handlePictureCardPreview = function (file) {
  console.log(file);
};
obj.methods.handleHeadPicSuccess = function (res, file) {
  this.formSubmit.head_pic.file_id = res.data.id;
};
obj.methods.beforeHeadPicUpload = function (file) {
  const isJPG = file.type === "image/jpeg" || file.type === "image/png" || file.type === "image/jpg" || file.type === "image/gif";
  const isLt2M = file.size / 1024 / 1024 < 2;

  if (!isJPG) {
    this.$message.error("上传头像图片只能是 JPG 格式!");
  }
  if (!isLt2M) {
    this.$message.error("上传头像图片大小不能超过 2MB!");
  }
  return isJPG && isLt2M;
};
obj.methods.handleCardPhoto1Success = function (res, file) {
  this.formSubmit.card_photo1.file_id = res.data.id;
};
obj.methods.beforeCardPhoto1Upload = function (file) {
  return true;
};
obj.methods.handleCardPhoto2Success = function (res, file) {
  this.formSubmit.card_photo2.file_id = res.data.id;
};
obj.methods.beforeCardPhoto2Upload = function (file) {
  return true;
};
obj.methods.handleHandCardPhoto1Success = function (res, file) {
  this.formSubmit.hand_card_photo1.file_id = res.data.id;
};
obj.methods.beforeHandCardPhoto1Upload = function (file) {
  return true;
};
obj.methods.handleHandCardPhoto2Success = function (res, file) {
  this.formSubmit.hand_card_photo2.file_id = res.data.id;
};
obj.methods.beforeHandCardPhoto2Upload = function (file) {
  return true;
};
// 覆盖此方法 重写
obj.methods.submitTextSelectChanngeEevent = function () {
  this.dialog.title = "查看用户信息";
  this.dialog.submitText = "预览用户信息";
  this.disabled = true;
};
// 覆盖此方法 重写
obj.methods.submitTextModifyChanngeEevent = function () {
  this.dialog.title = "编辑用户信息";
  this.dialog.submitText = "立即编辑";
  this.disabled = false;
};
// 覆盖此方法 重写
obj.methods.submitTextInsertChanngeEevent = function () {
  this.dialog.title = "添加用户信息";
  this.dialog.submitText = "立即创建";
  this.disabled = false;
  this.old_pwd = "";
};
// 覆盖此方法 重写
obj.methods.setValue = function (row) {
  for (var obj in this.formSubmit) {
    if (obj.toString() == "role" && row.role) {
      this.formSubmit[obj].id = row.role.id;
      continue;
    } else if (obj.toString() == "head_pic" || obj.toString() == "card_photo1" ||
                    obj.toString() == "card_photo2" || obj.toString() == "hand_card_photo1" || obj.toString() == "hand_card_photo2") {
      if (row[obj]) { this.formSubmit[obj].file_id = row[obj]; }
      continue;
    } else if (obj == "status" || obj == "marital" || obj == "edution" || obj == "level") continue;
    else this.formSubmit[obj] = row[obj];
  }
  this.formSubmit.enter_password = this.formSubmit["password"];
  this.old_pwd = this.formSubmit["password"];
  // if (row.status) this.formSubmit.status.id = getId(row.status, this.status_options);
  if (row.marital) this.formSubmit.marital.id = getId(row.marital, this.marital_options);
  if (row.edution) this.formSubmit.edution.id = getId(row.edution, this.edution_options);
  if (row.level) this.formSubmit.level.id = getId(row.level, this.level_options);
};
function getId(val, options) {
  for (var item in options) {
    if (options[item].category == val) return options[item].id;
  }
  return undefined;
}
export default {
  name: "user",
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