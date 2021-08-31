import axios from "axios";
import {baseUrl} from "../js/url";
import {
  urls,
  getUrl
} from "../js/url";
export const ContentTypeJson = "application/json";
export const ContentTypeForm = "application/x-www-form-urlencoded";
export const ContentTypeMultipart = "multipart/form-data";

export const createDatePickerOptions = {
  shortcuts: [{
    text: "最近一周",
    onClick(picker) {
      const end = new Date();
      const start = new Date();
      start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
      picker.$emit("pick", [start, end]);
    }
  }, {
    text: "最近一个月",
    onClick(picker) {
      const end = new Date();
      const start = new Date();
      start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
      picker.$emit("pick", [start, end]);
    }
  }, {
    text: "最近三个月",
    onClick(picker) {
      const end = new Date();
      const start = new Date();
      start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
      picker.$emit("pick", [start, end]);
    }
  }]
};

export function get(url, result, tip) {
  axios
    .get(url)
    .then(response => {
      if (response.data.success) result(response);
      else console.log(response.data);
      if (tip) tip(response);
    })
    .catch(function (error) {
      // 请求失败处理
      console.log(error);
    });
}

export function post(url, param, contenType, result, tip) {
  axios
    .post(url, param, {
      headers: {
        "Content-Type": contenType
      }
    })
    .then(response => {
      if (response.data.success) {
        result(response);
      } else console.log(response.data);
      if (tip) tip(response);
    })
    .catch(function (error) {
      // 请求失败处理
      console.log(error);
    });
}
export function tip(msg, title, confirmName, type, confirmTip, CancelTip) {
  this.$alert(msg, title, {
    confirmButtonText: confirmName,
    callback: action => {
      // if (action == 'confirm') {
      //    this.$message({
      //        type: type,
      //        message: confirmTip
      //    });
      // }
      // else {
      //    this.$message({
      //        type:type,
      //        message: CancelTip
      //    });
      // }
    }
  });
}
// 重置表单事件
export function resetFormEvent(formName) {
  if (this.$refs[formName]) {
    this.$refs[formName].resetFields();
  }
}
/****
 *@param options
@returns 父组件基本默认操作
 */
export function Gibson(options) {
  this.el = "";
  this.data = {
    request_content_type: 2, // 1 json 方式请求 2 from 请求方式 3 from data 请求 方式
    createDatePickerOptions: {
      shortcuts: [{
        text: "最近一周",
        onClick(picker) {
          const end = new Date();
          const start = new Date();
          start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
          picker.$emit("pick", [start, end]);
        }
      }, {
        text: "最近一个月",
        onClick(picker) {
          const end = new Date();
          const start = new Date();
          start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
          picker.$emit("pick", [start, end]);
        }
      }, {
        text: "最近三个月",
        onClick(picker) {
          const end = new Date();
          const start = new Date();
          start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
          picker.$emit("pick", [start, end]);
        }
      }]
    },
    test: true, // 测试用的
    // 操作地址
    operatorUrl: {
      query: "", // 查询数据地址
      add: "", // 提交数据url
      modify: "", // 编辑数据url
      delete: "" // 删除数据url
    },
    isOperatorRequest: false, // 允许操作请求访问
    controller: "", // 控制名称
    // 是否有增删改查权限
    operator: {
      add: true,
      modify: true,
      delete: true,
      query: true
    },
    tableData: [], // 查询列表数据
    loading: true, // 查询列表数据是否成功 false 成功
    disabled: false, // 表单是否禁用 预览时禁用
    multipleSelection: [], // 复选框多条删除记录或列表删除记录
    // dialog 弹出框信息
    dialog: {
      visible: false, // dialog 是否可用 true 可用
      title: "", // dialog 弹出框信息标题
      submitText: "", // dialog 弹出框 表单提交按钮文本
      resetText: "重置" // dialog 弹出框 表单重置按钮文本
    },
    // 查询表单信息
    formQuery: {},
    // 表单信息提交
    formSubmit: {},
    // 表单信息规则验证
    formRoleValidator: {},
    // 分页记录
    page: {
      page: 1,
      size: 10,
      current_page: 1,
      total: 10,
      sizes: [10, 20, 30, 40]
    },
    // 操作标识 1代表提交 2代表编辑 3 代表预览
    flag: 1,
    checkboxAllSelect: true, // 是否选中列表复选框 true 选中
    checkboxAllSelectText: "全选", // 是否选中列表复选框 提示文本 : 取消全选 或 全选
    labelPosition: "right", // 表单对齐方式 默认右对齐
    options: [], // 分类信息
	baseUrl:baseUrl
  };
  this.created = function () {

  };
  this.mounted = function (_self) {
    if (_self.isOperatorRequest) {
      // 增删改查权限
      get(getUrl(_self.controller + "/operator"), function (response) {
        _self.operator = response.data.data;
      });
    }
    _self.loadMounted();
    _self.loadTableDataOrQueryDataEvent();
  };
  this.watched = function () {

  };
  this.methods = {
    handleCategoryQueryChangeEvent: function (val) {
      if (this.dialog.visible) {
		 if(val instanceof Array){
			this.formSubmit.id = val[val.length - 1];
		 }
        else{
			this.formSubmit.id = val;
		}
        if (val) {
          if (this.$refs.dialogForm.$refs.refHandleForm) {
            this.$refs.dialogForm.$refs.refHandleForm.dropDownVisible = false; // 监听值发生变化就关闭它
          }
        }
      } else {
		  if(val instanceof Array){
			this.formQuery.id = val[val.length - 1];
		 }
        else{
			this.formQuery.id = val;
		}
        this.formQuery.id = val;
        if (val) {
          if (this.$refs.query.$refs.refHandleQuery) {
            this.$refs.query.$refs.refHandleQuery.dropDownVisible = false; // 监听值发生变化就关闭它
          }
        }
      }
    },
    handleCategoryQueryVisableChangeEvent: function (val) {
      if (val) {
        this.queryCategoryEvent();
      }
    },
    queryCategoryEvent: function () {
      var $this = this;
      get(getUrl(this.controller + "/category"), response => {
        if (response.data.success) {
          $this.options = response.data.data;
        }
      });
    },
    // 重新加载数据
    reload: function () {
      this.queryCategoryEvent();
    },
    // 取消其他操作
    quitOtherOperatorEvent: function () {

    },
    // vue装载完成
    loadMounted: function () {

    },
    // 提示
    tip: function (msg, title, confirmName, type, confirmTip, CancelTip) {
      this.$alert(msg, title, {
        confirmButtonText: confirmName,
        callback: action => {
          // if (action == 'confirm') {
          //    this.$message({
          //        type: type,
          //        message: confirmTip
          //    });
          // }
          // else {
          //    this.$message({
          //        type:type,
          //        message: CancelTip
          //    });
          // }
        }
      });
    },
    // 查询列表数据 或查询按钮事件
    loadTableDataOrQueryDataEvent: function () {
      var _self = this;
      var paramOptions = parseRequestParam(this.request_content_type, this.formQuery, undefined, this.queryTimeField());
      post(_self.operatorUrl.query + "?page=" + _self.page.current_page + "&size=" + _self.page.size, paramOptions.parem, paramOptions.contenType, function (response) {
        _self.tableData = !response.data.data.data ? [] : response.data.data.data;
        _self.page.size = response.data.data.result.size;
        _self.page.page = response.data.data.result.page;
        _self.page.total = response.data.data.result.total;
        _self.loading = false;
      });
    },
    // 表单查询时 时间需要转换
    queryTimeField: function () {
      if (this.request_content_type != 1) {
        // 时间转换
        return ["create_date", "update_date"];
      }
      return [];
    },
    // 提交表单事件 添加或编辑按钮提交事件
    submitFormEvent: function (formName) {
      this.$refs.dialogForm.$refs[formName].validate((valid) => {
        if (valid) {
          var result = false;
          if (this.flag == 3) return result;
          this.quitOtherOperatorEvent();
          var $this = this;
          var url = this.flag == 1 ? this.operatorUrl.add : this.operatorUrl.modify;
          var paramOptions = parseRequestParam(this.request_content_type, this.formSubmit);
          post(url, paramOptions.parem, paramOptions.contenType, function (response) {
            result = response.data.success;
            if (result) {
              $this.$nextTick(() => {
                $this.loadTableDataOrQueryDataEvent(); // 重新加载列表数据
                $this.reload();
                $this.dialog.visible = false;
              });
            }
          }, response => {
            $this.tip(response.data.message, "提示：", "确定", "info", "确定!", "关闭!");
          });
          return result;
        } else {
          console.log("error submit!!");
          return false;
        }
      });
    },
    // 重置表单事件
    resetFormEvent: function (formName) {
      if (this.dialog.visible) {
		  console.log(this.$refs.dialogForm.$refs[formName]);
        if (this.$refs.dialogForm.$refs[formName]) {
          this.$refs.dialogForm.$refs[formName].resetFields();
        }
      } else {
        if (this.$refs.query.$refs[formName]) {
          this.$refs.query.$refs[formName].resetFields();
        }
      }
    },
    // 禁用表单
    disabledFormEvent: function (formName) {
      if (this.$refs.dialogForm.$refs[formName]) {
        this.$refs.dialogForm.$refs[formName].disabled = this.disabled;
      }
    },
    // 切换表单复选框是否选中 事件 即取消全选或 全选 按钮事件
    toggleSelectionTableCheckBoxButtonEvent: function (rows) {
      var flag = !!rows;
      // console.log(rows);
      this.checkboxAllSelectTextChanngeEvent(flag);
      if (flag) {
        rows.forEach(row => {
          // this.$refs.multipleTable.toggleRowSelection(row);
          this.$refs.tableList.$refs.multipleTable.toggleRowSelection(row);
        });
      } else {
        // this.$refs.multipleTable.clearSelection();
        this.$refs.tableList.$refs.multipleTable.clearSelection();
      }
    },
    // 复选框多选或列表中编辑、删除、查询事件 即当选择项发生变化时会触发该事件
    handleSelectionChange: function (val) {
      var _self = this; // $this.data;
      _self.checkboxAllSelectTextChanngeEvent(val.length > 0);
      _self.multipleSelection = val;
    },
    // 取消全选或 全选 按钮 文本改变事件
    checkboxAllSelectTextChanngeEvent(val) {
      var _self = this; // $this.data;
      if (val) {
        _self.checkboxAllSelect = false;
        _self.checkboxAllSelectText = "取消全选";
      } else {
        _self.checkboxAllSelect = true;
        _self.checkboxAllSelectText = "全选";
      }
    },
    // 关闭dialog
    handleDialogClose: function (done) {
      var _self = this; // $this.data;
      // console.log(_self.dialog.visible);
      this.$confirm("确认关闭？")
        .then(_ => {
          _self.dialog.visible = false; // 手动关闭
          done();
          // console.log(_self.dialog.visible);
        })
        .catch(_ => {
          // console.log(_self.dialog.visible);
        });
    },
    // 预览事件(列表)  清空表单 重新赋值表单
    handleSelectClickEvent: function (row) {
      this.submitTextSelectChanngeEevent();
      this.flag = 3;
      this.cleanFormToSetValue(row);
    },
    // 清空表单 重新赋值表单
    cleanFormToSetValue: function (row) {
      this.dialog.visible = true;
      var $this = this;
      this.$nextTick(() => {
        $this.resetFormEvent("formSubmit");
        // $this.disabledFormEvent('formSubmit');
        $this.setValue(row);
      });
    },
    // 预览表单 dialog 提示改变 表单 按钮文本改变
    submitTextSelectChanngeEevent: function () {

    },
    // 表单编辑按钮事件(列表) 清空表单 重新赋值表单
    handleModifyClickEvent: function (row) {
      this.submitTextModifyChanngeEevent();
      this.flag = 2;
      this.cleanFormToSetValue(row);
    },
    // 编辑表单 dialog 提示改变 表单 按钮文本改变
    submitTextModifyChanngeEevent: function () {

    },
    // 删除按钮事件(列表)
    handleDeleteClickEvent: function (row) {
      if (row.id) {
        var $this = this;
        this.handleDialogClose(function () {
          $this.delete(row.id);
        });
      } else {
        this.tip("删除失败,id未找到!", "提示：", "确定",
          "info", "确定!", "关闭!");
      }
    },
    // 每页记录改变是触发事件
    handleSizeChangeEvent: function (val) {
      this.page.size = val;
      this.loadTableDataOrQueryDataEvent();
    },
    // 当前页数改变事件
    handleCurrentPageChangeEvent: function (val) {
      this.page.current_page = val;
      this.loadTableDataOrQueryDataEvent();
    },
    // 添加按钮事件 清空表单
    insertButtonClickEvent: function () {
      this.submitTextInsertChanngeEevent();
      this.flag = 1;
      this.dialog.visible = true;
	  var $this=this;
      this.$nextTick(() => {
        $this.resetFormEvent("formSubmit");
      });
    },
    // 添加表单 dialog 提示改变 表单 按钮文本改变
    submitTextInsertChanngeEevent: function () {

    },
    // 更新提交表单值
    setValue: function (row) {

    },
    // 编辑按钮事件  dialog 提示改变 表单 按钮文本改变
    modifyButtonClickEvent: function () {
      if (this.multipleSelection.length != 1) {
        this.tip("请选中一行数据进行编辑!", "提示：", "确定",
          "info", "确定!", "关闭!");
      } else {
        var row = this.multipleSelection[0];
        this.handleModifyClickEvent(row);
      }
    },
    // 查看按钮事件  dialog 提示改变 表单 按钮文本改变
    queryButtonClickEvent: function () {
      if (this.multipleSelection.length != 1) {
        this.tip("请选中一行数据进行预览!", "提示：", "确定",
          "info", "确定!", "关闭!");
      } else {
        var row = this.multipleSelection[0];
        this.handleSelectClickEvent(row);
      }
    },
    // 删除按钮点击事件
    deleteButtonClickEvent: function () {
      if (this.multipleSelection.length == 0) {
        this.tip("请选中至少一行数据进行删除!", "提示：", "确定",
          "info", "确定!", "关闭!");
      } else {
        if (this.multipleSelection.length == 1) {
          var row = this.multipleSelection[0];
          this.handleDeleteClickEvent(row);
        } else {
          var $this = this;
          this.handleDialogClose(() => {
            var ids = [];
            var rows = $this.multipleSelection;
            rows.forEach(row => {
              ids.push(row.id);
            });
            // var str = "<DeleteEntry><Ids>";
            // ids.forEach(it => { str += "<int>" + it + "</int>" });
            // str +="</Ids></DeleteEntry>"
            // post($this.operatorUrl.delete, str, ContentTypeTextXml,
            var paramOptions = parseRequestParam($this.request_content_type, {
              ids: ids
            });
            post($this.operatorUrl.delete, paramOptions.parem, paramOptions.contenType,
              (response) => {
                $this.loadTableDataOrQueryDataEvent();
                $this.reload();
              }, (response) => {
                $this.tip(response.data.message, "提示：", "确定", "info", "确定!", "关闭!");
              });
          });
        }
      }
    },
    // 根据id单删除
    delete: function (id) {
      var $this = this;
      get(this.operatorUrl.delete + "/" + id, function (response) {
        $this.loadTableDataOrQueryDataEvent();
        $this.reload();
      },
      function (response) {
        $this.tip(response.data.message, "提示：", "确定", "info", "确定!", "关闭!");
      });
    }
  }
}


/**
 * 网络图像文件转Base64
 */
export function getBase64Image(img) {
  var canvas = document.createElement("canvas");
  canvas.width = img.width;
  canvas.height = img.height;
  var ctx = canvas.getContext("2d");
  ctx.drawImage(img, 0, 0, img.width, img.height);
  var ext = img.src.substring(img.src.lastIndexOf(".") + 1).toLowerCase();
  var dataURL = canvas.toDataURL("image/" + "jpg");
  return dataURL;
}
/**
 *Base64字符串转二进制
 */
function dataURLtoBlob(dataurl) {
  var arr = dataurl.split(","),
    mime = arr[0].match(/:(.*?);/)[1],
    bstr = atob(arr[1]),
    n = bstr.length,
    u8arr = new Uint8Array(n);
  while (n--) {
    u8arr[n] = bstr.charCodeAt(n);
  }
  return new Blob([u8arr], {
    type: mime
  });
}

export function parseParam(obj, skipOptions, timeOptions) {
  return parse("", obj, skipOptions, timeOptions);
}

export function parse(prefix, obj, skipOptions, timeOptions) {
  var str = "";
  for (var i in obj) {
    if (skipOptions && skipOptions.indexOf(i) > -1) continue;
    if (obj[i] instanceof Date) {
            str += encodeURI(prefix + i.toString()) + "=";
            str += encodeURI(parseDate(obj[i.toString()]))+"&";
    }
	else if (obj[i] instanceof Array) {
      if (obj[i] && obj[i].length > 0) {
        obj[i].map(it => {
          str += encodeURI(prefix + i.toString()) + "=";
          if (it) {
            if (timeOptions && timeOptions.indexOf(i.toString()) > -1) {
              str += encodeURI(parseDate(it));
            } else {
              str += encodeURI(it);
            }
          }
          str += "&";
        });
      }
      else {
        //str += encodeURI(prefix + i.toString()) + "=" + encodeURI("[]") + "&";
        //str += encodeURI(prefix + i.toString()) + "[0]=&";
        //str += encodeURI(prefix + i.toString()) + "[1]=&";
      }
    } else if (obj[i] instanceof Object) {
      str += parse(prefix + i.toString() + ".", obj[i]) + "&";
    } else {
	  if(!obj[i.toString()])continue;
      str += encodeURI(prefix + i.toString()) + "=";
      if (obj[i.toString()] && timeOptions && timeOptions.indexOf(i.toString()) > -1) {
        str += encodeURI(parseDate(obj[i.toString()]));
      } else if (obj[i.toString()]) {
        str += encodeURI(obj[i.toString()]);
      }
      str += "&";
    }
  }
  str = str.substring(0, str.length - 1);
  return str;
}

export function parseRequestParam(requestContentType, param, skipOptions, timeOptions) {
  var paramOptions = {};
  if (requestContentType == 1) {
    paramOptions.parem = param;
    paramOptions.contenType = ContentTypeJson;
  } else if (requestContentType == 2) {
    paramOptions.parem = parseParam(param, skipOptions, timeOptions);
    paramOptions.contenType = ContentTypeForm;
  } else {
    var formData = new FormData();
    parseFormData(formData, "", param, skipOptions, timeOptions);
    paramOptions.parem = formData;
    paramOptions.contenType = ContentTypeMultipart;
  }
  return paramOptions;
}

export function parseDate(date) {
  var dateObj = new Date(date);
  var time = dateObj.getFullYear() + "-" + (dateObj.getMonth() + 1) + "-" + dateObj.getDate() + " " + dateObj.getHours() + ":" +
    dateObj.getMinutes() + ":" + dateObj.getSeconds();
  return time;
}
export function parseFormData(formData, prefix, obj, skipOptions, timeOptions) {
  var str = "";
  for (var i in obj) {
    if (skipOptions && skipOptions.indexOf(i) > -1) continue;
    if (obj[i] instanceof Date) {
            str += encodeURI(prefix + i.toString()) + "=";
            str += encodeURI(parseDate(obj[i.toString()]))+"&";
    }
	else if (obj[i] instanceof Array) {
      if (obj[i] && obj[i].length > 0) {
        obj[i].map(it => {
          if (timeOptions && timeOptions.indexOf(i.toString()) > -1) {
            formData.append(prefix + i.toString(), parseDate(it));
          } else {
            formData.append(prefix + i.toString(), it.toString());
          }
        });
      }
      // else {
      //   formData.append(prefix + i.toString() , encodeURI("[]"));
      //   formData.append(prefix + i.toString() , null);
      //   formData.append(prefix + i.toString(), null);
      // }
    } else if (obj[i] instanceof Object) {
      str += parseFormData(formData, prefix + i.toString() + ".", obj[i]);
    } else {
      if (obj[i.toString()] && timeOptions && timeOptions.indexOf(i.toString()) > -1) {
        formData.append(prefix + i.toString(), parseDate(obj[i.toString()]));
      } else if (obj[i.toString()]) {
        formData.append(prefix + i.toString(), obj[i.toString()]);
      }
    }
  }
  str = str.substring(0, str.length - 1);
  return str;
}

export function dataURLtoFile(dataurl, filename) { // 将base64转换为文件
  var arr = dataurl.split(","),
    mime = arr[0].match(/:(.*?);/)[1],
    bstr = atob(arr[1]),
    n = bstr.length,
    u8arr = new Uint8Array(n);
  while (n--) {
    u8arr[n] = bstr.charCodeAt(n);
  }
  return new File([u8arr], filename, {
    type: mime
  });
}

