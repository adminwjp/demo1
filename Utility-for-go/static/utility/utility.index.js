/// <reference path="js.cookie.js" />
//var newscript = document.createElement('script');
//newscript.setAttribute('type', 'text/javascript');
//newscript.setAttribute('src', '/js/js.cookie.js');
//var head = document.getElementsByTagName('head')[0];
//head.appendChild(newscript);
/*5.加载文件*/
/* 已加载文件缓存列表,用于判断文件是否已加载过，若已加载则不再次加载*/
var classcodes = [];
window.Import = {
    /*加载一批文件，_files:文件路径数组,可包括js,css,less文件,succes:加载成功回调函数*/
    LoadFileList: function (_files, succes) {
        var FileArray = [];
        if (typeof _files === "object") {
            FileArray = _files;
        } else {
            /*如果文件列表是字符串，则用,切分成数组*/
            if (typeof _files === "string") {
                FileArray = _files.split(",");
            }
        }
        if (FileArray != null && FileArray.length > 0) {
            var LoadedCount = 0;
            for (var i = 0; i < FileArray.length; i++) {
                loadFile(FileArray[i], function () {
                    LoadedCount++;
                    if (LoadedCount == FileArray.length) {
                        succes();
                    }
                })
            }
        }
        /*加载JS文件,url:文件路径,success:加载成功回调函数*/
        function loadFile(url, success) {
            if (!FileIsExt(classcodes, url)) {
                var ThisType = GetFileType(url);
                var fileObj = null;
                if (ThisType == ".js") {
                    fileObj = document.createElement('script');
                    fileObj.src = url;
                } else if (ThisType == ".css") {
                    fileObj = document.createElement('link');
                    fileObj.href = url;
                    fileObj.type = "text/css";
                    fileObj.rel = "stylesheet";
                } else if (ThisType == ".less") {
                    fileObj = document.createElement('link');
                    fileObj.href = url;
                    fileObj.type = "text/css";
                    fileObj.rel = "stylesheet/less";
                }
                success = success || function () { };
                fileObj.onload = fileObj.onreadystatechange = function () {
                    if (!this.readyState || 'loaded' === this.readyState || 'complete' === this.readyState) {
                        success();
                        classcodes.push(url)
                    }
                }
                document.getElementsByTagName('head')[0].appendChild(fileObj);
            } else {
                success();
            }
        }
        /*获取文件类型,后缀名，小写*/
        function GetFileType(url) {
            if (url != null && url.length > 0) {
                return url.substr(url.lastIndexOf(".")).toLowerCase();
            }
            return "";
        }
        /*文件是否已加载*/
        function FileIsExt(FileArray, _url) {
            if (FileArray != null && FileArray.length > 0) {
                var len = FileArray.length;
                for (var i = 0; i < len; i++) {
                    if (FileArray[i] == _url) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

var FilesArray = [];
Import.LoadFileList(FilesArray, function () {
    /*这里写加载完成后需要执行的代码或方法*/
});
const baseUrl = "http://localhost:6002/social_contact/admin/api/v1/";
//const baseUrl = "http://localhost:5002/admin/api/v1/";
//通用vue 方法
const service = axios.create({
    baseURL: baseUrl, // url = base url + request url
    // withCredentials: true, // send cookies when cross-domain requests
    timeout: 5000 // request timeout
});
//request interceptor
axios.interceptors.request.use(
    config => {
        // do something before request is sent
        var to =utility.token.get() ;
        if (to) config.headers['token'] = to;
        return config;
    },
    error => {
        // do something with request error
        console.log(error); // for debug
        return Promise.reject(error);
    }
);
/***
 * 
 * */
const ContentTypeJson = "application/json";
const ContentTypeForm = "application/x-www-form-urlencoded";
const ContentTypeMultipart = "multipart/form-data";
const ContentTypeTextXml = "text/xml";

function BasicUrl(controller, options) {
    this.add = getUrl(controller + "/insert");
    this.edit = getUrl(controller + "/update");
    this.delete = getUrl(controller + "/delete");
    this.query = getUrl(controller + "/list");
    this.category = getUrl(controller + "/category");
    if (options) {
        for (const key in options) {
            this[key] = options[key];
        }
    }
}
const urls = {
    admin: new BasicUrl("admin"),
    icon: new BasicUrl("icon"),
    user: new BasicUrl("user"),
    usermenu: new BasicUrl("usermenu"),
    upload: new BasicUrl("upload"),
    relation: new BasicUrl("relation"),
    work: new BasicUrl("work"),
    menu: new BasicUrl("menu", {
        parent_category: getUrl("menu/category")
    }),
    catagory: new BasicUrl("catagory", {
        role: getUrl("role")
    }),
};
/**
 * 拼接请求地址  "https://localhost:44328/admin/api/v1/" +url
 * @param {any} url 地址
 * @returns 
 */
function getUrl(url) {
    return baseUrl + url + "";
}



 function parseParam(obj, skipOptions, timeOptions) {
     return utility.post.parse("", obj, skipOptions, timeOptions);
}




function Gibson(options) {
    this.el = '';
    this.data = {
        request_content_type: 2, // 1 json 方式请求 2 from 请求方式 3 from data 请求 方式
        createDatePickerOptions: {
            shortcuts: [{
                text: '最近一周',
                onClick(picker) {
                    const end = new Date();
                    const start = new Date();
                    start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
                    picker.$emit('pick', [start, end]);
                }
            }, {
                text: '最近一个月',
                onClick(picker) {
                    const end = new Date();
                    const start = new Date();
                    start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
                    picker.$emit('pick', [start, end]);
                }
            }, {
                text: '最近三个月',
                onClick(picker) {
                    const end = new Date();
                    const start = new Date();
                    start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
                    picker.$emit('pick', [start, end]);
                }
            }]
        },
        test: true,//测试用的
        //操作地址
        operatorUrl: {
            query: '', //查询数据地址
            add: '', //提交数据url
            modify: '', //编辑数据url
            delete: '' //删除数据url
        },
        isOperatorRequest: false, //允许操作请求访问
        controller: '', //控制名称
        //是否有增删改查权限
        operator: {
            add: true,
            modify: true,
            delete: true,
            query: true,
        },
        tableData: [], //查询列表数据
        loading: true, //查询列表数据是否成功 false 成功
        disabled: false, //表单是否禁用 预览时禁用
        multipleSelection: [], //复选框多条删除记录或列表删除记录
        //dialog 弹出框信息
        dialog: {
            visible: false, //dialog 是否可用 true 可用
            title: '', //dialog 弹出框信息标题
            submitText: '', //dialog 弹出框 表单提交按钮文本
            resetText: '重置' //dialog 弹出框 表单重置按钮文本
        },
        //查询表单信息
        formQuery: {},
        //表单信息提交
        formSubmit: {},
        //表单信息规则验证
        formRoleValidator: {},
        //分页记录
        page: {
            page: 1,
            size: 10,
            current_page: 1,
            total: 10,
            sizes: [10, 20, 30, 40]
        },
        //操作标识 1代表提交 2代表编辑 3 代表预览
        flag: 1,
        checkboxAllSelect: true, //是否选中列表复选框 true 选中
        checkboxAllSelectText: '全选', //是否选中列表复选框 提示文本 : 取消全选 或 全选
        labelPosition: 'right', //表单对齐方式 默认右对齐
        options :[],//分类数据
		baseUrl:baseUrl
    };
    //this.created = {

    //};
    this.mounted = function () {
        this.loadMounted();
        this.loadTableDataOrQueryDataEvent();
    };
    this.watch =  {

    };
    this.methods = {
        handleCategoryQueryChangeEvent : function (val) {
            this.formQuery.id = val;
        },
        handleCategoryQueryVisableChangeEvent : function (val) {
            if (val) {
                this.queryCategoryEvent();
            }
        },
       queryCategoryEvent : function () {
           var $this = this;
           get(getUrl(this.controller + "/category"), response => { $this.options = response.data.data; });
        },
        //重新加载数据
        reload : function () {

        },
        //取消其他操作
        quitOtherOperatorEvent: function () {

        },
        //vue装载完成
        loadMounted: function () {

        },
        //提示
        tip: function (msg, title, confirmName, type, confirmTip, CancelTip) {
            this.$alert(msg, title, {
                confirmButtonText: confirmName,
                callback: action => {
                    //if (action == 'confirm') {
                    //    this.$message({
                    //        type: type,
                    //        message: confirmTip
                    //    });
                    //}
                    //else {
                    //    this.$message({
                    //        type:type,
                    //        message: CancelTip
                    //    });
                    //}
                }
            });
        },
        //查询列表数据 或查询按钮事件
        loadTableDataOrQueryDataEvent: function () {
            var $this = this;
            var paramOptions = utility.post.form($this.request_content_type, $this.formQuery, undefined, this.queryTimeField());
            post($this.operatorUrl.query + "/" + $this.page.current_page + "/" + $this.page.size, paramOptions.parem, paramOptions.contenType, function (response) {
                $this.tableData = !response.data.data.data ? [] : response.data.data.data;
                $this.page.size = response.data.data.result.size;
                $this.page.page = response.data.data.result.page;
                $this.page.total = response.data.data.result.total;
                $this.loading = false;
            });
        },
        //表单查询时 时间需要转换
        queryTimeField: function () {
            if (this.request_content_type != 1) {
                //时间转换
                return ["create_date", "update_date"];
            }
            return [];
        },
        //提交表单事件 添加或编辑按钮提交事件
        submitFormEvent: function (formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    var result = false;
                    if (this.flag == 3) return result;
                    this.quitOtherOperatorEvent();
                    var $this = this;
                    var url = this.flag == 1 ? this.operatorUrl.add : this.operatorUrl.modify;
                    var paramOptions = utility.post.form($this.request_content_type, $this.formSubmit);
                    post(url, paramOptions.parem, paramOptions.contenType, function (response) {
                        result = response.data.success;
                        if (result) {
                            $this.$nextTick(() => {
                                $this.loadTableDataOrQueryDataEvent(); //重新加载列表数据
                                $this.reload();
                                $this.dialog.visible = false;
                            });
                        }
                        //提示
                        $this.tip(response.data.message, '提示：', '确定',
                            'info', '确定!', '关闭!');
                    }, response => $this.tip(response.data.message, '提示：', '确定',
                        'info', '确定!', '关闭!'));
                    return result;
                } else {
                    console.log('error submit!!');
                    return false;
                }
            });
        },
        //重置表单事件
        resetFormEvent: function (formName) {
            if (this.$refs[formName]) {
                this.$refs[formName].resetFields();
            }
        },
        //禁用表单
        disabledFormEvent: function (formName) {
            if (this.$refs[formName]) {
                this.$refs[formName].disabled = this.disabled;
            }
        },
        //切换表单复选框是否选中 事件 即取消全选或 全选 按钮事件
        toggleSelectionTableCheckBoxButtonEvent: function (rows) {
            var flag = rows ? true : false;
            this.checkboxAllSelectTextChanngeEvent(flag);
            if (flag) {
                rows.forEach(row => {
                    this.$refs.multipleTable.toggleRowSelection(row);
                });
            } else {
                this.$refs.multipleTable.clearSelection();
            }
        },
        //复选框多选或列表中编辑、删除、查询事件 即当选择项发生变化时会触发该事件
        handleSelectionChange: function (val) {
            this.checkboxAllSelectTextChanngeEvent(val.length > 0);
            this.multipleSelection = val;
        },
        //取消全选或 全选 按钮 文本改变事件
        checkboxAllSelectTextChanngeEvent(val) {
            if (val) {
                this.checkboxAllSelect = false;
                this.checkboxAllSelectText = "取消全选";
            } else {
                this.checkboxAllSelect = true;
                this.checkboxAllSelectText = "全选";
            }
        },
        //关闭dialog
        handleDialogClose: function (done) {
            this.$confirm('确认关闭？')
                .then(_ => {
                    done();
                })
                .catch(_ => { });
        },
        //预览事件(列表)  清空表单 重新赋值表单
        handleSelectClickEvent: function (row) {
            this.submitTextSelectChanngeEevent();
            this.flag = 3;
            this.cleanFormToSetValue(row);
        },
        //清空表单 重新赋值表单
        cleanFormToSetValue: function (row) {
            this.dialog.visible = true;
            var $this = this;
            this.$nextTick(() => {
                $this.resetFormEvent('formSubmit');
                //$this.disabledFormEvent('formSubmit');
                $this.setValue(row);
            });
        },
        //预览表单 dialog 提示改变 表单 按钮文本改变
        submitTextSelectChanngeEevent: function () {

        },
        // 表单编辑按钮事件(列表) 清空表单 重新赋值表单
        handleModifyClickEvent: function (row) {
            this.submitTextModifyChanngeEevent();
            this.flag = 2;
            this.cleanFormToSetValue(row);
        },
        //编辑表单 dialog 提示改变 表单 按钮文本改变
        submitTextModifyChanngeEevent: function () {

        },
        //删除按钮事件(列表)
        handleDeleteClickEvent: function (row) {
            if (row.id) {
                var $this = this;
                this.handleDialogClose(function () {
                    $this.delete(row.id);
                });
            } else {
                this.tip('删除失败,id未找到!', '提示：', '确定',
                    'info', '确定!', '关闭!');
            }
        },
        //每页记录改变是触发事件
        handleSizeChangeEvent: function (val) {
            this.page.size = val;
            this.loadTableDataOrQueryDataEvent();
        },
        //当前页数改变事件
        handleCurrentPageChangeEvent: function (val) {
            this.page.current_page = val;
            this.loadTableDataOrQueryDataEvent();
        },
        //添加按钮事件 清空表单
        insertButtonClickEvent: function () {
            this.submitTextInsertChanngeEevent();
            this.flag = 1;
            this.dialog.visible = true;
            this.$nextTick(() => {
                this.resetFormEvent('formSubmit');
            });
        },
        //添加表单 dialog 提示改变 表单 按钮文本改变
        submitTextInsertChanngeEevent: function () {

        },
        //更新提交表单值
        setValue: function (row) {

        },
        //编辑按钮事件  dialog 提示改变 表单 按钮文本改变
        modifyButtonClickEvent: function () {
            if (this.multipleSelection.length != 1) {
                this.tip('请选中一行数据进行编辑!', '提示：', '确定',
                    'info', '确定!', '关闭!');
            } else {
                var row = this.multipleSelection[0];
                this.handleModifyClickEvent(row);
            }
        },
        //查看按钮事件  dialog 提示改变 表单 按钮文本改变
        queryButtonClickEvent: function () {
            if (this.multipleSelection.length != 1) {
                this.tip('请选中一行数据进行预览!', '提示：', '确定',
                    'info', '确定!', '关闭!');
            } else {
                var row = this.multipleSelection[0];
                this.handleSelectClickEvent(row);
            }
        },
        //删除按钮点击事件
        deleteButtonClickEvent: function () {
            if (this.multipleSelection.length == 0) {
                this.tip('请选中至少一行数据进行删除!', '提示：', '确定',
                    'info', '确定!', '关闭!');
            } else {
                if (this.multipleSelection.length == 1) {
                    var row = this.multipleSelection[0];
                    this.handleDeleteClickEvent(row);
                } else {
                    var $this = this;
                    this.handleDialogClose( ()=> {
                        var ids = [];
                        var rows = $this.multipleSelection;
                        rows.forEach(row => {
                            ids.push(row.id);
                        });
                        //var str = "<DeleteEntry><Ids>";
                        //ids.forEach(it => { str += "<int>" + it + "</int>" });
                        //str +="</Ids></DeleteEntry>"
                        var paramOptions = utility.post.form($this.request_content_type, { ids: ids});
                        post($this.operatorUrl.delete, paramOptions.parem, paramOptions.contenType,
                            (response) => {
                            $this.loadTableDataOrQueryDataEvent();
                            $this.reload();
                        }, (response) => { $this.tip(response.data.message, '提示：', '确定', 'info', '确定!', '关闭!'); });
                    });
                }
            }
        },
        //根据id单删除
        delete: function (id) {
            var $this = this;
            get(this.operatorUrl.delete + "/" + id, function(response)  {
                $this.loadTableDataOrQueryDataEvent();
                $this.reload();
            },
             function (response)  { $this.tip(response.data.message, '提示：', '确定', 'info', '确定!', '关闭!'); });
        }
    }
}
function get(url, result) {
    getTip(url, result);
}

function getTip(url, result, tip) {
    axios.get(url).then(response => {
        if (response.data.success) result(response);
        else console.log(response.data);
        if (tip) tip(response);
    }).catch(function (error) { // 请求失败处理
        console.log(error);
    });
}
function post(url, param, contenType, result,tip) {
    axios.post(url, param, { headers: { 'Content-Type': contenType } }).then(response => {
       console.log(response);
        if (response.data.success||response.data.status)
        {
            result(response);
        }
        if (tip) tip(response);
    }).catch(function (error) { // 请求失败处理
        console.log(error);
    });
}
//获取页面元素位置 
//注意：IE、Firefox3+、Opera9.5、Chrome、Safari支持，
//在IE中，默认坐标从(2,2)开始计算，导致最终距离比其他浏览器多出两个像素，我们需要做个兼容
 function clientRect() {
    var rect = function () {
        if (document.documentElement.getBoundingClientRect) {
            return document.documentElement.getBoundingClientRect();
        }
        else if (document.body.getBoundingClientRect) {
            return document.body.getBoundingClientRect();
        }
        else {
            return undefined;
        }
    }
    var clientTop = document.documentElement.clientTop;  // 非IE为0，IE为2
    var clientleft = document.documentElement.clientLeft; // 非IE为0，IE为2
    if (rect)
        return {
            top: rect.top - clientTop,
            left: rect.left - clientleft,
            right: rect.right - clientleft,
            bottom: rect.bottom - clientTop
        };
    return undefined;
};

