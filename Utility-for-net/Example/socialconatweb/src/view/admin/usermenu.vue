<template>
  <div>
    <query
      ref="query" :role_options="role_options"  :menu_options="menu_options" :level_options="level_options"
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
  </div>
</template>
<script>
import query from "../../components/admin/usermenu/query";
import tableList from "../../components/admin/usermenu/tableList";
import operator from "../../components/admin/operator";
import page from "../../components/admin/page";
import {
  ContentTypeJson,
  ContentTypeForm,
  ContentTypeMultipart,
  get,
  post,
  resetFormEvent,
  tip,
  Gibson,
  setAdmin,
  setRoleCategory,
  parseRequestParam
} from "../../js/base";
import "../../js/auth";
import { urls, getUrl } from "../../js/url";
var obj = new Gibson();
obj.el = ".user_menu_list";
obj.data.operatorUrl = { add: urls.usermenu.add, modify: urls.usermenu.edit, delete: urls.usermenu.delete, query: urls.usermenu.query };
obj.data.formQuery = { role_id: "", create_date: [], update_date: [] };
obj.data.formSubmit = { id: "", category: "", role: { id: "" }, description: "这个人很懒,什么也没留下!" };
obj.data.controller = "usermenu";
obj.data.isOperatorRequest = true;
obj.data.roleRequired = false;
obj.data.formRoleValidator = {
};
obj.methods.checkEd = function (id, val, type) {
  console.log(id + " " + val + " " + type);
  var $this = this;
  var paramOptions = parseRequestParam($this.request_content_type, { id: id, val: val.toString(), type: type });
  post(urls.usermenu.edit, paramOptions.parem, paramOptions.contenType, function (response) {
    $this.loadTableDataOrQueryDataEvent();
    $this.dialog.visible = false;
  }, response => $this.tip(response.data.message, "提示：", "确定"));
}
obj.methods.reload = function () {
  this.queryCategoryEvent();
};
setRoleCategory(obj);
function setLevel(obj) {
  obj.data.level_options = [];
  obj.data.formQuery.level_id = "";
  obj.data.formSubmit.level = {id: ""};
  obj.methods.levelQueryEvent = function () {
    var $this = this;
    get(urls.userlevel.category, function (response) { $this.level_options = response.data.data; });
  };
  obj.methods.handleLevelVisableChangeEvent = function (value) {
    if (value) {
      this.levelQueryEvent();
    }
  };
  obj.methods.handleLevelFormChangeEvent = function (value) {
    if (value instanceof Array) {
      this.formSubmit.level.id = value[value.length - 1];
    } else {
      this.formSubmit.level.id = value;
    }
  };
  obj.methods.handleLevelQueryChangeEvent = function (value) {
    if (value instanceof Array) {
      this.formQuery.level_id = value[value.length - 1];
    } else {
      this.formQuery.level_id = value;
    }
  };
}
setLevel(obj);
function setMenu(obj) {
  obj.data.menu_options = [];
  obj.data.formQuery.menu_id = "";
  obj.data.formSubmit.menu = {id: ""};
  obj.methods.menuQueryEvent = function () {
    var $this = this;
    get(urls.menu.category, function (response) { $this.menu_options = response.data.data; });
  };
  obj.methods.handleMenuVisableChangeEvent = function (value) {
    if (value) {
      this.menuQueryEvent();
    }
  };
  obj.methods.handleMenuFormChangeEvent = function (value) {
    if (value instanceof Array) {
      this.formSubmit.menu.id = value[value.length - 1];
    } else {
      this.formSubmit.menu.id = value;
    }
  };
  obj.methods.handleMenuQueryChangeEvent = function (value) {
    if (value instanceof Array) {
      this.formQuery.menu_id = value[value.length - 1];
    } else {
      this.formQuery.menu_id = value;
    }
  };
}
setMenu(obj);
// 测试用的
setAdmin(obj);
// 覆盖此方法 重写
obj.methods.submitTextSelectChanngeEevent = function () {
  this.dialog.title = "查看用户菜单信息";
  this.dialog.submitText = "预览用户菜单信息";
  this.disabled = true;
};
// 覆盖此方法 重写
obj.methods.submitTextModifyChanngeEevent = function () {
  this.dialog.title = "编辑用户菜单信息";
  this.dialog.submitText = "立即编辑";
  this.disabled = false;
};
// 覆盖此方法 重写
obj.methods.submitTextInsertChanngeEevent = function () {
  this.dialog.title = "添加用户菜单信息";
  this.dialog.submitText = "立即创建";
  this.disabled = false;
};
// 覆盖此方法 重写
obj.methods.setValue = function (row) {
  for (var obj in this.formSubmit) {
    if (row[obj]) {
      this.formSubmit[obj] = row[obj];
    }
  }
};
export default {
  name: "usermenu",
  components: { query, operator, tableList, page },
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
  width: 80%;
  margin: 0px auto;
}

</style>