function getName(data, lang) {
    return get("name", data, lang);
}

const SourceUrl="http://192.168.1.3:5000/"
const baseAdminUrl = "http://localhost:6002/company/admin/api/v1";
const baseUrl = "http://localhost:6002/company/api/v1";
//const baseAdminUrl = "http://localhost:80002/api/v1/admin";
//const baseUrl = "http://localhost:8002/api/v1";
const urls = {
    category: new UrlEntry("category", {
        flag:'flag',
        service_category: "service_category", skill_category: "skill_category", testimonial_category: "testimonial_category",
        theme_category: "theme_category", menu_category: "menu_category", menus: "menus",
        brand_category: "brand_category", work_category: "work_category", team_category:"team_category"}),
};
function emptyQueryData() {
    let queryData = Object.create({});
    let createMin = $("#createdatemin").val();
    if (createMin) {
        queryData.create_start_date = createMin;
    }
    let createMax = $("#createdatemax").val();
    if (createMax) {
        queryData.create_end_date = createMax;
    }
    let modifyMin = $("#modifydatemin").val();
    if (modifyMin) {
        queryData.modify_start_date = modifyMin;
    }
    let modifyMax = $("#modifydatemax").val();
    if (modifyMax) {
        queryData.modify_end_date = modifyMax;
    }
    let name = $("#name").val();
    if (name) {
        queryData.name = name;
    }
    let checkbox = $("#enable").val();
    if (checkbox != "") {
        queryData.enable = checkbox == "true" ? true : false;
    }
    //console.log(queryData);
    return queryData;
}
function Admin(options) {
    if ($(".input-text,.textarea")) {
        $(".input-text,.textarea").Huifocusblur();

    }
    if ($(".textarea")) {
        $(".textarea").Huitextarealength({
            minlength: 0,
            maxlength: 500.
        });
    }
    this.defaultQueryData = function () {
        return emptyQueryData();
    }
    this.url = {};
    let self1 = this;
    this.createdRow = function (row, data, index) {
        //console.log(index);
        $(row).addClass("text-c");
        //$('td', row).map(it => {
        //    $('td', row).eq(it).addClass("text-l");//.attr("align", "center");
        //});
        $('td', row).eq(3).addClass('highlight');
        $('td', row).eq(2).addClass('td-status');
        $('td', row).eq(self1.columns.length - 1).addClass('td-manage');
    };
    this.checkbox={
            data: function (item) {
                return "<div align='center'><input type='checkbox'  name='ckb-jobid' value='" + item.id + "'></div>";
            }
        };
    this.id= { data: 'id' };
    this.name= { data: "name" };
    this.enable=  {
            data: function (item) {
                return item.enable ? "<span class=\"label label-success radius\">已启用</span>" : "<span class=\"label radius\">已停用</span>";
            }
        };
    this.description=  { data: "description" };
    this.category_id= { data: 'category.id' };
    this.category_name= { data: "category.name" };
    this.create_date=    { data: "create_date" };
    this.modify_date=    { data: "modify_date" };
    this.operator=    {
            data: function (item) {
                return "<a style=\"text-decoration:none\" onClick=\"" + (options && options.flag == 2 ? "setPublish" : "setEnable") + "(this,'" + self1.url.editstatus + "'," + item.id + ")\" href=\"javascript:;\" title=\""
                    + (options && options.flag == 2 ? (item.enable ? "下架" : "发布"):(!item.enable ? "启用" : "停用")) + "\"><i class=\"Hui-iconfont\">" +
                    (options && options.flag == 2 ? ((!item.enable ? "&#xe603;" : "&#xe6de;")) : (!item.enable ? "&#xe6e1;" : "&#xe631;"))
                    + "</i></a> <a title=\"编辑\" href=\"javascript:;\" onClick=\"operator('edit',this)\" class=\"ml-5\" style=\"text-decoration:none\"><i class=\"Hui-iconfont\">&#xe6df;</i></a>  <a title=\"删除\" href=\"javascript:;\" onclick=\"del('" + self1.url.delete + "'," +
                    item.id + ")\" class=\"ml-5\" style=\"text-decoration:none\"><i class=\"Hui-iconfont\">&#xe6e2;</i></a>";
            }
        }
    this.columns = [];
    this.tableInit = function (self) {
        self = self || this;

        $('.table-sort').dataTable({
            paging: true, //true 显示分页
            serverSide: false,//true 服务器端分页
            searching: false,
            "lengthMenu": [5, 10, 25, 50, 75, 100],
            "ordering": false,
            ajax: {
                url: self.url.query+"/1/10",
                method: "post",
                data: self.defaultQueryData,
                dataSrc: "data",//注意 用的是dataFilter 数据 returnData.data 调试麻烦
                dataFilter: function (json) {
                    json = JSON.parse(json);
                    $("span.r > strong").html(json.data.result.records);
                    var returnData = {};
                    returnData.draw = 1;
                    returnData.recordsTotal = json.data.result.records;  //返回数据全部记录  
                    returnData.recordsFiltered = json.data.result.records;  //后台不实现过滤功能，每次查询均视作全部结果
                    returnData.data = json.data.data;  //返回的数据列表
                    return JSON.stringify(returnData);
                    //return returnData;
                }
            },
            "createdRow": self.createdRow,
            columns: self.columns
        });
    };
   
    $("#search").click(function () {
        $('.table-sort').DataTable().ajax.reload();
    });
    $("#clear").click(function () {

        $("#createdatemin").val("");
        $("#createdatemax").val("");
        $("#modifydatemin").val("");
        $("#modifydatemax").val("");
        $("#name").val("");
        $("#enable").val("");
    });
}

 /*启用-停用*/
function setEnable(self, url, id, enable, flag) {
    let enable1 = flag ? enable : $(self).parent().parent()/*.find(".td-status")*/.find("td:eq(2)").text() == "已启用";
    var tip = flag ? enable1 : !enable1;
    layer.confirm(!tip ? '确认要停用吗？' : '确认要启用吗？', function (index) {
        let post = id instanceof Array && id.length > 0;
        let en = flag ? enable1 : !enable1
        $.ajax({
            type: post ? 'post' : 'get',
            url: post ? url : (url + "?id=" + id + "&enable=" + en),
            dataType: 'json',
            data: post ? { ids: id, "enable": enable1 } : {},
            success: function (data) {
                if (data.success) {
                    //多条件
                    if (post) {
                        let chks = $("input[type='checkbox']:checked");
                        let trs = [];
                        chks.map(it => {
                            //存在
                            if (id.indexOf($(chks).eq(it).val()) > -1) {
                                trs.push($(chks).eq(it));
                            }
                        });
                        for (let i = 0; i < trs.length; i++) {
                            let it = trs[i];
                            $(it).parents("tr").find(".td-manage").prepend('<a style="text-decoration:none" onClick="setEnable(this,\'' + url + '\',' + id + ')" href="javascript:;" title="'
                                + (en ? "停用" : "启用") + '"><i class="Hui-iconfont">' + (en ? "&#xe631;" : "&#xe6e1;") + '</i></a>');
                            $(it).parents("tr").find(".td-status").html('<span class="' + (!en ? "label label-defaunt radius" : "label label-success radius") + '">' + (!en ? "已停用" : "已启用") + '</span>');
                            $(it).parents("tr").find(".td-manage > a:eq(1)").remove();
                        }
                    }
                    //单条件
                    else {
                        $(self).parents("tr").find(".td-manage").prepend('<a style="text-decoration:none" onClick="setEnable(this,\'' + url + '\',' + id + ')" href="javascript:;" title="'
                            + (en ? "停用" : "启用") + '"><i class="Hui-iconfont">' + (en ? "&#xe631;" : "&#xe6e1;") + '</i></a>');
                        $(self).parents("tr").find(".td-status").html('<span class="' + (!en ? "label label-defaunt radius" : "label label-success radius") + '">' + (!en ? "已停用" : "已启用") + '</span>');
                        if ($(self).parent().hasClass("td-manage")) {
                            $(self).remove();
                        }
                        else {
                            $(self).parents("tr").find(".td-manage > a:eq(1)").remove();
                        }
                    }
                    layer.msg(!en? "已停用" : "已启用", { icon: 5, time: 1000 });
                }
                else {
                    layer.msg(!en? "已停用失败" : "已启用失败", { icon: 5, time: 1000 });
                }
             
            },
            error: function (data) {
                console.log(data.msg);
                layer.msg(!en? "已停用失败" : "已启用失败", { icon: 5, time: 1000 });
            },
        });
    });
}
/*发布-下架*/
function setPublish(self, url, id, enable, flag) {
    let enable1 = flag ? enable : $(self).parent().parent()/*.find(".td-status")*/.find("td:eq(2)").text() == "已发布";
    var tip = flag ? enable1 : !enable1;
    //console.log(flag + "  " + enable);
    layer.confirm(tip ? '确认要发布吗？' : '确认要下架吗？', function (index) {
        let post = id instanceof Array && id.length > 1;
        
        let en = flag ? enable1 : !enable1
        $.ajax({
            type: post ? 'post' : 'get',
            url: post ? url : (url + "?id=" + id + "&enable=" + en),
            dataType: 'json',
            data: post ? { ids: post ? id : [id], "enable": enable1 } : {},
            success: function (data) {
                if (data.success) {
                    //多条件
                    if (post) {
                        let chks = $("input[type='checkbox']:checked");
                        let trs = [];
                        chks.map(it => {
                            //存在
                            if (id.indexOf($(chks).eq(it).val()) > -1) {
                                trs.push($(chks).eq(it));
                            }
                        });
                        for (let i = 0; i < trs.length; i++) {
                            let it = trs[i];
                            $(it).parents("tr").find(".td-manage").prepend('<a style="text-decoration:none" onClick="setPublish(this,\'' + url + '\',' + id + ')" href="javascript:;" title="'
                                + (en ? "下架" : "发布") + '"><i class="Hui-iconfont">' + (!en ? "&#xe603;" : "&#xe6de;") + '</i></a>');
                            $(it).parents("tr").find(".td-status").html('<span class="' + (!en ? "label label-defaunt radius" : "label label-success radius") + '">' + (!en ? "已下架" : "已发布") + '</span>');
                            $(it).parents("tr").find(".td-manage > a:eq(1)").remove();
                        }
                    }
                    //单条件
                    else {
                        $(self).parents("tr").find(".td-manage").prepend('<a style="text-decoration:none" onClick="setPublish(this,\'' + url + '\',' + id + ')" href="javascript:;" title="'
                            + (en ? "下架" : "发布") + '"><i class="Hui-iconfont">' + (!en ? "&#xe603;" : "&#xe6de;") + '</i></a>');
                        $(self).parents("tr").find(".td-status").html('<span class="' + (!en ? "label label-defaunt radius" : "label label-success radius") + '">' + (!en ? "已下架" : "已发布") + '</span>');
                        if ($(self).parent().hasClass("td-manage")) {
                            $(self).remove();
                        }
                        else {
                            $(self).parent().parent().parent().find(".td-manage > a:eq(1)").remove();
                        }
                    }
                    layer.msg(!en ? "已下架" : "已发布", { icon: 5, time: 1000 });
                }
                else {
                    layer.msg(!en ? "已下架失败" : "已发布失败", { icon: 5, time: 1000 });
                }
            },
            error: function (data) {
                console.log(data.msg);
                layer.msg(!en? "已下架失败" : "已发布失败", { icon: 5, time: 1000 });
            },
        });
    });
}
/*审核*/
function shenhe(obj, id) {
    layer.confirm('审核？', {
        btn: ['通过', '不通过'],
        shade: false
    },
        function () {
            $(obj).parents("tr").find(".td-manage").prepend('<a class="c-primary" onClick="product_start(this,id)" href="javascript:;" title="申请上线">申请上线</a>');
            $(obj).parents("tr").find(".td-status").html('<span class="label label-success radius">已发布</span>');
            $(obj).remove();
            layer.msg('已发布', { icon: 6, time: 1000 });
        },
        function () {
            $(obj).parents("tr").find(".td-manage").prepend('<a class="c-primary" onClick="product_shenqing(this,id)" href="javascript:;" title="申请上线">申请上线</a>');
            $(obj).parents("tr").find(".td-status").html('<span class="label label-danger radius">未通过</span>');
            $(obj).remove();
            layer.msg('未通过', { icon: 5, time: 1000 });
        });
}

/*-申请上线*/
function shenqing(obj, id) {
    $(obj).parents("tr").find(".td-status").html('<span class="label label-default radius">待审核</span>');
    $(obj).parents("tr").find(".td-manage").html("");
    layer.msg('已提交申请，耐心等待审核!', { icon: 1, time: 2000 });
}
/*删除*/
function del(url,id,flag,func) {
    layer.confirm('确认要删除吗？', function (index) {
        $.ajax({
            type: 'Get',
            url: url + "?id=" + id,
            dataType: 'json',
            success: function (data) {
                layer.msg(data.message, { icon: 1, time: 1000 }, function () {
                    if (data.success) {
                        if (flag) {
                            //do nothing
                            func();
                        }
                        else {
                            $('.table-sort').DataTable().ajax.reload();
                        }
                    }
                });
            },
            error: function (data) {
                console.log(data.msg);
            },
        });
    });
}
function hasChecked() {
    let ids = getIds();
    if (ids.length > 0) {
        if (ids.length == 1) {
            return true;
        } else {
            layer.msg("只能选中一行数据进行编辑", { icon: 1, time: 1000 });
            return false;
        }
    }
    else {
        layer.msg("未选中数据进行编辑", { icon: 1, time: 1000 });
        return false;
    }
}
function getCheckedItem() {
    let checks = $("input[type='checkbox']:checked");
    return checks.length == 2 ? checks[1]: checks[0];
}
function getTrItem(self) {
    let tr = self ? $(self).parent().parent() : $(getCheckedItem()).parent().parent().parent();
    return tr;
}
/**
 * 修改 是否启用
 * @param {any} url 路劲
 * @param {any} enable 状态
 */
function modifyEnable(url, flag) {
    //layer.msg("暂无实现,敬请公告.........", { icon: 1, time: 1000 });
    //console.log(flag);
    if (getIds().length>0) {
        let checks = $("input[type='checkbox']:checked");
        let ids = [];
        checks.map(it => {
            if ($(checks).eq(it).val() == "") {

            }
            else {
                ids.push({
                    id: $(checks).eq(it).val(),
                    enable: $(checks).eq(it).parent().parent().parent().find("td[class='td-status']").text(),
                    self: $(checks).eq(it).parent().parent().parent().find("td[class='td-status']")
                });
            }
        });
        //单操作
        if (ids.length == 1) {
            if (ids[0].enable == "已启用" || ids[0].enable == "已发布") {
                if (flag) {
                    layer.msg("已选中,启用状态相同,无法操作!", { icon: 1, time: 1000 });
                    return;
                }
            }
            else {
                if (!flag) {
                    layer.msg("已选中,暂停状态相同,无法操作!", { icon: 1, time: 1000 });
                    return;
                }
            }
            if (ids[0].enable == "已启用" || ids[0].enable == "已停用") {
                setEnable(ids[0].self,url, ids[0].id, flag,true);
            }
            else {
                console.log(flag + "  ==11 " + true);
                setPublish(ids[0].self, url, ids[0].id, flag, true);
            }
        }
        //多操作
        else {
            let temps = [];
            let fabu = true;
            for (var i in ids) {
                fabu = ids[i].enable == "已发布" || ids[i].enable == "已下架";
                if (ids[i].enable == "已启用" || ids[i].enable == "已发布") {
                    if (flag) {
                        continue;
                    }
                    temps.push(ids[i].id);
                }
                else {
                    if (!flag) {
                        continue;
                    }
                    temps.push(ids[i].id);
                }
            }
            if (temps.length == 0) {
                layer.msg("已选中,相同状态过滤后无不同状态数据,无法更新!", { icon: 1, time: 1000 });
                return;
            }
            else {
                if (fabu) {
                    console.log(flag+"  ==" +true);
                    setPublish(ids[0].self, url, temps, flag, true);
                }
                else {
                    setEnable(ids[0].self, url, temps, flag, true);
                }
            }
        }
    }
    else {
        layer.msg("未选中,无法使用!", { icon: 1, time: 1000 });
    }
}
function getIds() {
    let ids = [];
    let checks = $("input[type='checkbox']:checked");
    checks.map(it => {
        let va = $(checks[it]).val();
        if (va != "") {
            ids.push($(checks[it]).val());
        }
    });
    return ids;
}
function datadel(url,id,func) {
    let ids =id?id: getIds();
    console.log(ids);
    if (ids.length > 0) {
        layer.confirm('确认要删除吗？', function (index) {
            $.ajax({
                type: 'post',
                url: url,
                dataType: 'json',
                data: { ids },
                success: function (data) {
                    layer.msg(data.message, { icon: 1, time: 1000 }, function () {
                        if (data.success) {
                            if (id) {
                                //do  nothing 
                                func();
                            }
                            else {
                                $('.table-sort').DataTable().ajax.reload();
                            }
                        }
                    });
                },
                error: function (data) {
                    console.log(data.msg);
                },
            });
        });
    }
    else {
        layer.msg("未选中数据进行删除", { icon: 1, time: 1000 });
    }
}

function AdminJqGrid(options1) {
    if ($(".input-text,.textarea")) {
        $(".input-text,.textarea").Huifocusblur();

    }
    if ($(".textarea")) {
        $(".textarea").Huitextarealength({
            minlength: 0,
            maxlength: 500.
        });
    }
    var obj = null;
    this.url = {};
    var self1 = this;
    /**
     * 在客户端我们可以有下面两种方法得到这些额外信息：
     * @param key
     */
    this.getUserData = function (key) {
        return key ? obj.getUserDataItem(key) : obj.getUserData() || obj.getUserData('userData');
    }
    var options = {
        pager: '#page',//定义翻页用的导航栏，必须是有效的html元素。翻页工具栏可以放置在html页面任意位置
        rowNum: 10,//在grid上显示记录条数，这个参数是要被传递到后台
        rowList: [10, 20, 30],//一个下拉选择框，用来改变显示记录数，当选择时会覆盖rowNum参数传递到后台
        sortname: 'id',//默认的排序列。可以是列名称或者是一个数字，这个参数会被提交到后台
        viewrecords: true,//定义是否要显示总记录数
        height: 200,// 'auto',//表格高度，可以是数字，像素值或者百分比 150
        autowidth: true,//如果为ture时，则当表格在首次被创建时会根据父元素比例重新调整表格宽度。如果父元素宽度改变，为了使表格宽度能够自动调整则需要实现函数：setGridWidth
        //描述json 数据格式的数组
        //需要定义jsonReader来跟服务器端返回的数据做对应，其默认值
        jsonReader: {
            root: "data.data",//"rows",//包含实际数据的数组
            page: "data.result.page",//当前页
            total: "data.result.total",//总页数
            records: "data.result.records",//查询出的记录数
            repeatitems: true,//指明每行的数据是可以重复的，如果设为false，则会从返回的数据中按名字来搜索元素，这个名字就是colModel中的名字
            cell: "cell",//当前行的所有单元格
            id: "id",//行id id属性值为“invid”。 一旦当此属性设为false时，我们就不必把所有在colModel定义的name值都赋值。因为是按name来进行搜索元素的，所以他的排序也不是按colModel中指定的排序结果。
            userdata: "userdata",//用户数据（user data） 在某些情况下，我们需要从服务器端返回一些参数但并不想直接把他们显示到表格中，而是想在别的地方显示，那么我们就需要用到userdata标签
            subgrid: {
                root: "children",
                repeatitems: true,
                cell: "cell"
            }
        },
        url: '',//获取数据的地址
        datatype: "json",//从服务器端返回的数据类型，默认xml。可选类型：xml，local，json，jsonnp，script，xmlstring，jsonstring，clientside
        mtype: 'post',//ajax提交方式。POST或者GET，默认GET
        loadtext: '数据加载中.......',//	string	当请求或者排序时所显示的文字内容
        multiselect: true,
        rownumbers: true,
        page: 1,//指定初始化页码
        gridComplete : function () {
            $("#table").closest("div.ui-jqgrid-view")
                .children("div.ui-jqgrid-titlebar")
                .css("text-align", "center")
                .children("span.ui-jqgrid-title")
                .css("float", "none");
            //var rowNum =  $("#table").jqGrid('getGridParam', 'rowNum'); 
            var total = $("#table").jqGrid('getGridParam', 'records'); //获取查询得到的总记录数量
            $("span.r > strong").html(total);
        },
        //当选择行时触发此事件。rowid：当前行id；status：选择状态，当multiselect 为true时此参数才可用
        onSelectRow :function (rowid, status) {
            //其他样式受影响
            $("tbody > tr[id='" + rowid + "']").removeClass("ui-state-highlight");
        },
        //multiselect为ture，且点击头部的checkbox时才会触发此事件。aRowids：所有选中行的id集合，为一个数组。status：boolean变量说明checkbox的选择状态，true选中false不选中。无论checkbox是否选择，aRowids始终有 值
        onSelectAll : function (aRowids, status) {
            //其他样式受影响
            $("tbody > tr[role='row']").removeClass("ui-state-highlight");
            //console.log(aRowids);
        },
        //当从服务器返回响应时执行，xhr：XMLHttpRequest 对象
        loadComplete : function (data) {
            //console.log(data);
        },
        //当用户点击当前行在未选择此行时触发。rowid：此行id；e：事件对象。返回值为ture或者false。如果返回true则选择完成，如果返回false则不会选择此行也不会触发其他事件
       beforeSelectRow : function (rowid, e) {
            if (e.type == 'click') {
                let i = $.jgrid.getCellIndex($(e.target).closest('td')[0]),
                    cm = jQuery("#table").jqGrid('getGridParam', 'colModel');
                return (cm[i].name != 'act' && cm[i].name != 'enable'); //当点击的单元格的名字不是为act时，才触发选择行事件
            }
            return false;
        },
       //当点击单元格时触发。rowid：当前行id；iCol：当前单元格索引；cellContent：当前单元格内容；e：event对象
        onCellSelect : function (rowid, iCol, cellcontent, e) {
            //beforeSelectRow事件没有返回 true 该事件无法触发
            if (iCol == 2) {
                console.log("enable index 2 onCellSelect event trigger ");
            }
            // console.log(" index " + iCol + " onCellSelect event trigger ");
        },
        //双击行时触发。rowid：当前行id；iRow：当前行索引位置；iCol：当前单元格位置索引；e:event对象
        ondblClickRow : function (rowid, iRow, iCol, e) {
            if (iCol === 3) {
                //let cellvalue = $(e.target).text();
                //$(e.target).html("<input type='checkbox' name='enable' value='true' " + (cellvalue ? "checked" : "") + "/>启用&nbsp;&nbsp;<input type='checkbox' name='enable' value='false' " + (!cellvalue ? "checked" : "") + " />暂停");
                //$(e.target).mouseleave(function () {
                //    $(e.target).html(cellvalue);
                //});
                //$(e.target).find("input").click(function () {
                //    $(this).prop("checked", "checked");
                //    if ($(this).prev().attr("checked")) {
                //         $(this).prev().prop("checked", "");
                //    }
                //    else {
                //        $(this).prev().next("checked", "");
                //    }
                //});
            }
            //$('#table').jqGrid('editRow', rowid, {
            //    "keys": true,
            //    "aftersavefunc": function () {
            //    },
            //    "afterrestorefunc": function () {
            //    },
            //    "oneditfunc": function () {                 //正在编辑行的
            //    }
            //});
        },
       
    };
    function initColModel() {
        options.colModel = [
            { label: "编号", name: 'id', index: 'id', width: 90, align: 'center' },
            {
                label: "启用", name: 'enable', sortable: false, width: 90, editable: true, edittype: "checkbox", editoptions: { value: "true:false" },
                align: 'center',
                //formatter: function (cellvalue, options, rowObject) {
                //    //return "<input type='checkbox' name='enable' value='true' " + (cellvalue?"checked":"") + "/>启用&nbsp;&nbsp;<input type='checkbox' name='enable' value='false' "+(!cellvalue?"checked":"")+" />暂停";
                //    return cellvalue;//一动其他的也受影响
                //},
                editOptions: {
                    url: urls.nav.editstatus, reloadAfterSubmit: true, closeOnEscape: true, afterSubmit: function (form) { },
                    serializeDelData: function (postdata) {
                        var rowdata = $('#table').getRowData(postdata.id);
                        // append postdata with any information
                        //return {id: postdata.id, oper: postdata.oper, year: rowdata.year, spc_cd: rowdata.spc_cd};
                        return postdata;
                    }
                }
            },
            { label: "名称", name: 'name', index: 'name', width: 150, sortable: false, editable: true, editoptions: { size: "20", maxlength: "30" }, align: 'center' },
            { label: "英文名称", name: 'english_name', index: 'english_name', sortable: false, width: 100, editable: true, align: 'center' },
            { label: "地址", name: 'href', index: 'href', width: 60, sortable: false, editable: true, align: 'center' },
            {
                label: "分类", name: 'parent_id', index: 'parent_id', sortable: false, width: 90, editable: true, edittype: "select", editoptions: { value: categorys.join(';') }, align: 'center',
                formatter: function (cellvalue, options, rowObject) {
                    if (rowObject.parent_id != null) {
                        for (var i = 0; i < categorys.length; i++) {
                            if (categorys[i].split(':')[0] == rowObject.parent_id.toString()) {
                                return categorys[i].split(':')[1];
                            }
                        }
                    }
                    return rowObject.parent_id == "请选择" || rowObject.work_id == null ? "无" : rowObject.parent_id;
                }
            },
            { label: "加入时间", name: 'create_date', index: 'create_date', width: 150, align: 'center', frozen: true },
            { label: "修改时间", name: 'modify_date', index: 'modify_date', width: 150, align: 'center', frozen: true },
            {
                label: "操作", name: 'act', index: 'act', width: 75, align: 'center',
                formatter: function (cellvalue, options, rowObject) {

                    return "<a style=\"text-decoration:none\" onClick=\"setEditEnable('" + self1.url.editstatus + "'," +
                        rowObject.id + ",false,false," + (options1 && options1.flag == 2 ? "true" : "false" )+")\" href=\"javascript:;\" title=\""
                        + (options1 && options1.flag == 2 ? (rowObject.enable ? "下架" : "发布") : (!rowObject.enable ? "启用" : "停用")) + "\"><i class=\"Hui-iconfont\">" +
                        (options1 && options1.flag == 2 ? ((!rowObject.enable ? "&#xe603;" : "&#xe6de;")) : (!rowObject.enable ? "&#xe6e1;" : "&#xe631;"))
                        + "</i></a> <a title=\"编辑\" href=\"javascript:;\" onClick=\"jQuery('#table').editRow('" + rowObject.id + "')\" class=\"ml-5\" style=\"text-decoration:none\"><i class=\"Hui-iconfont\">&#xe6df;</i></a>"
                        + " <a title=\"保存\" href=\"javascript:;\" onClick=\"jQuery('#table').setGridParam({editurl:'" + self1.url.edit + "'});jQuery('#table').saveRow('" + rowObject.id + "')\" class=\"ml-5\" style=\"text-decoration:none\">保存</a>" +

                        "<a title=\"取消\" href=\"javascript:;\" onClick=\"jQuery('#table').restoreRow('" + rowObject.id + "')\" class=\"ml-5\" style=\"text-decoration:none\">取消</a>"
                        + "  <a title=\"删除\" href=\"javascript:;\" onclick=\"deleteJqGrid(" +
                        rowObject.id + ",'" + self1.url.delete + "',false)\" class=\"ml-5\" style=\"text-decoration:none\"><i class=\"Hui-iconfont\">&#xe6e2;</i></a>";
                }
            },
        ]
    }
    this.options = options;
    this.initColModel = initColModel;
    var search = function () {

        $("#search").click(function () {
            console.log(111);
            let da = emptyQueryData();
            var pd = $("#table").jqGrid('getGridParam', 'postData');
            pd = $.extend(pd, da);
            $("#table").jqGrid('setGridParam', 'postData', pd);
            $("#table").trigger("reloadGrid");
            $("#clear").trigger("click");
        });
    };
    this.search = search;
    
}
function beforeShowForm(form, add) {
    var dlgDiv = $("#editmodtable");
    var parentDiv = dlgDiv.parent();
    var dlgWidth = dlgDiv.width();
    var parentWidth = parentDiv.width();
    var dlgHeight = dlgDiv.height();
    var parentHeight = parentDiv.height();
    dlgDiv.css({ top: Math.round((parentHeight - dlgHeight) / 2), left: Math.round((parentWidth - dlgWidth) / 2) });
}
function operatorJqGrid(title, flag,url) {
    if (flag == "add") {
        //打开窗口
        jQuery("#table").setGridParam({ editurl: url });
        jQuery("#table").jqGrid('editGridRow', "new", {
            addCaption: title,
            reloadAfterSubmit: true,
            width: 400,
            beforeShowForm: beforeShowForm
        });
        //$("#table").trigger("reloadGrid");
        //$("#table").addRowData(0, {},0);//新增一个空行
    }
    else if (flag == "edit") {
        var gr = jQuery("#table").jqGrid('getGridParam', 'selrow');
        if (gr != null) {
            //打开窗口赋值
            jQuery("#table").setGridParam({ editurl: url });
            jQuery("#table").jqGrid('editGridRow', gr, {
                editCaption: title,
                reloadAfterSubmit: false,
                beforeShowForm: beforeShowForm
            });
            //$("#table").trigger("reloadGrid");
        }
        else
            layer.msg("请选择行编辑", { icon: 1, time: 1000 });
    }
    else if (flag == 'query') {
        jQuery("#table").jqGrid('searchGrid', {
            sopt: ['cn', 'bw', 'eq', 'ne', 'lt', 'gt', 'ew']
        });
    }
    else {
        deleteJqGrid('',url,true);
    }
}
function saveJqGrid(rowid) {
    jQuery("#table").jqGrid('saveRow', rowid, {
        successfunc: null,
        url: null,
        extraparam: {},
        aftersavefunc: null,
        errorfunc: null,
        afterrestorefunc: null,
        restoreAfterError: true,
        mtype: "POST",
        saveui: "enable",
        validationCell: null,
        beforeSaveRow: null,
        beforeCancelRow: null,
        //savetext: $.jgrid.getRegional($t, 'defaults.savetext')
    });
}
function deleteJqGrid(id, url,flag) {
    //layer.msg("暂无实现,敬请公告.........", { icon: 1, time: 1000 });
    if (flag) {
        var ids = $('#table').jqGrid('getGridParam', 'selarrrow');
        datadel(url, ids, function () {
            for (var i = 0; i < ids.length; i++) {
                $("#table").jqGrid("delRowData", ids[i], { reloadAfterSubmit: false});
               //自己实现 移除 
            }
        });
    }
    else {
        if (id) {
            //var id=$('#table').jqGrid('getGridParam','selrow');//获取选择一行的id
            del(url, id, true, function () {
                // var gr = jQuery("#table").jqGrid('getGridParam', 'selrow');
                // jQuery("#table").setGridParam({ editurl: url });
                //该方式重新提交需要设置url
               // jQuery("#table").jqGrid('delGridRow', id, { reloadAfterSubmit: false });
                $("#table").jqGrid("delRowData", id, { reloadAfterSubmit: false });
                //自己实现 移除
            });
        } else {
            layer.msg("请选择行删除", { icon: 1, time: 1000 });
        }
    }
}
function setEditEnable(url, id, enable, flag, publish) {
    //  layer.msg("暂无实现,敬请公告.........", { icon: 1, time: 1000 });
    var enable1;
    if (flag) {
        enable1 = enable && enable.toString() == "true";
        var ids = $('#table').jqGrid('getGridParam', 'selarrrow');//多个或一个
        var data = [];
        for (var i = 0; i < ids.length; i++) {
            var rowData = $("#table").jqGrid('getRowData', ids[i]);
            let en = rowData.enable && rowData.enable.toString() == "true";
            if (en != enable) {
                data.push(ids[i]);
            }
        }
        if (data.length == 0) {
            layer.msg("没有修改的操作,状态都相同", { icon: 1, time: 1000 });
            return;
        }
        id = data;
    }
    else {
        var rowData = $("#table").jqGrid('getRowData', id);
        enable1 = rowData.enable && rowData.enable.toString() == "true";//获取状态
    }
    var tip = flag ? enable1 : !enable1;
    layer.confirm(publish ? (tip ? '确认要发布吗？' : '确认要下架吗？')
        : (!tip ? '确认要停用吗？' : '确认要启用吗？'), function (index) {
            let post = id instanceof Array && id.length > 0;
            let en = flag ? enable1 : !enable1;
            $.ajax({
                type: post ? 'post' : 'get',
                url: post ? url : (url + "?id=" + id + "&enable=" + en),
                dataType: 'json',
                data: post ? { ids: id, "enable": enable1 } : {},
                success: function (data) {
                    if (data.success) {
                        function button(pk) {
                            let use = flag ? !en : en;
                            let str = "<a style=\"text-decoration:none\" onClick=\"" +
                                (publish ? "setEditPublish" : "setEditEnable") + "('" +
                                url + "'," + pk + ")\" href=\"javascript:;\" title=\""
                                + (publish ? (use ? "下架" : "发布") : (use ? "启用" : "停用")) +
                                "\"><i class=\"Hui-iconfont\">" +
                                (publish ? ((!use ? "&#xe603;" : "&#xe6de;")) : (use ? "&#xe6e1;" : "&#xe631;"))
                                + "</i></a> ";
                            let tr = $("tr[id='" + pk + "']");
                            console.log(pk + " " + en);
                            $("#table").jqGrid("setCell", pk, "enable", en);
                            let td = tr.find("td").last();
                            td.prepend(str);
                            td.find("a:eq(1)").remove();
                        }
                        //多条件
                        if (post) {
                            for (let i = 0; i < id.length; i++) {

                                button(id[i]);
                            }
                        }
                        //单条件
                        else {
                            button(id);
                        }
                        layer.msg(publish ? (!en ? "已下架" : "已发布")
                            : !en ? "已停用" : "已启用", { icon: 5, time: 1000 });
                    }
                    else {
                        layer.msg(publish ? (!en ? "已下架失败" : "已发布失败")
                            : !en ? "已停用失败" : "已启用失败", { icon: 5, time: 1000 });
                    }

                },
                error: function (data) {
                    console.log(data.msg);
                    layer.msg(publish ? (!en ? "已下架失败" : "已发布失败")
                        : !en ? "已停用失败" : "已启用失败", { icon: 5, time: 1000 });
                },
            });
        });
}
function imgUrl(file) {
    if (window.createObjectURL != undefined) {
        return window.createObjectURL(file);
    }
    else if (window.URL != undefined) {
        return  window.URL.createObjectURL(file);
    }
    else if (window.webkitURL != undefined) {
        return window.webkitURL.createObjectURL(file);
    }
    return undefined;
}
function AdminOperator() {
    if ($(".input-text,.textarea")) {
        $(".input-text,.textarea").Huifocusblur();
        $(".input-text,.textarea").trigger("blur");
    }
    if ($(".textarea")) {
        $(".textarea").Huitextarealength({
            minlength: 0,
            maxlength: 500.
        });
    }
    this.rules = {};
    this.url = {};
    this.edit = false;
    this.close = true;
    this.success = function () {

    };
    this.setName = function (obj) {
        this.setEnable(obj.enable);
        $("#id").val(obj.id);
        $("#name").val(obj.name);
        $("#english_name").val(obj.english_name);
    };
    this.setDesc = function (obj) {
        this.setName(obj);
        $("#description").val(obj.description);
        $("#english_description").val(obj.english_description);
    };
    this.setEnable = function (enable) {
        if (enable) {
            $("input[name='enable']").eq(0).parent().addClass("checked");
            $("input[name='enable']").eq(0).next().trigger("click");
            $("input[name='enable']").eq(1).parent().removeClass("checked");
        } else {
            $("input[name='enable']").eq(0).parent().removeClass("checked");
            $("input[name='enable']").eq(1).parent().addClass("checked");
            $("input[name='enable']").eq(1).next().trigger("click");
      
        }
    };
    this.form = function () {
        let self = this;
        $("#form").validate({
            rules: self.rules,
            onkeyup: false,
            focusCleanup: true,
            success: "valid",
            submitHandler: function (form) {
                $(form).ajaxSubmit({
                    type: 'post',
                    url: !self.edit ? self.url.add : self.url.edit,
                    success: function (data) {
                        console.log(data);
                        layer.msg(data.message, { icon: 1, time: 1000 }, function () {
                            if (self.close) {
                                var index = parent.layer.getFrameIndex(window.name);
                                parent.$('.btn-refresh').click();
                                parent.layer.close(index);
                            }
                            self.success();
                        });
                    },
                    error: function (XmlHttpRequest, textStatus, errorThrown) {
                        layer.msg('error!', { icon: 1, time: 1000 }, function () {
                            if (self.close) {
                                var index = parent.layer.getFrameIndex(window.name);
                                parent.$('.btn-refresh').click();
                                parent.layer.close(index);
                            }
                            self.success();
                        });
                    }
                });
                return false;
            }
        });
    };
}
const companyUrls = {
    // portfolio
    porwork: baseUrl + "/catagory/porworks",
    //about
    about: baseUrl + "/catagory/about",
    team: baseUrl + "/catagory/teams",
    //index
    nav: baseUrl + "/catagory/nav/1",
    company: baseUrl + "/catagory/company",
    main: baseUrl + "/catagory/main",
    feature: baseUrl + "/catagory/features",
    work: baseUrl + "/catagory/works",
    service: baseUrl + "/catagory/services",
    skill: baseUrl + "/catagory/skills",
    media: baseUrl + "/catagory/media",
    testimonial: baseUrl + "/catagory/testimonials",
    partner: baseUrl + "/catagory/partners",
    theme: baseUrl + "/catagory/themes",
    img: "http://127.0.0.1:5000/",
};
function UrlEntry(name,options) {
    this.add = baseAdminUrl + "/" + name + "/add";
    this.edit = baseAdminUrl + "/" + name + "/update";
    this.delete = baseAdminUrl + "/" + name + "/delete";
    this.query = baseAdminUrl + "/" + name + "/list";
    this.editstatus = baseAdminUrl + "/" + name + "/status";
    for (const i in options) {
        this[i] = baseAdminUrl + "/" + name +"/"+ options[i];
    }
}
function getLanguage() {
    return "chinese";
}
function get(suffix, data, lang) {
    //console.log(data);
    if (!lang) {
        lang = language;
    }
    switch (lang) {
        case "english": return data["english_" + suffix];
        default: return data[suffix];
    }
}
function companyUI() {
    function init() {
        function refreshSlider() {
            $('#testimonial-sliders').owlCarousel({
                margin: 30,
                responsive: {
                    0: {
                        items: 1
                    },
                    768: {
                        items: 2
                    },
                    992: {
                        items: 3
                    }
                },
                autoplay: true,
                autoplayTimeout: 2000,
                loop: true
            });
        }
        /*** portfolio */
        function getPorWork() {
            $.getJSON(companyUrls.porwork, data => {
                if (data.success) {
                    portfolio.work = data.data;
                    updatePorWork();
                }
            });
        }
        /*** about */
        function getAbout() {
            $.getJSON(companyUrls.about, data => {
                if (data.success) {
                    about.about = data.data;
                    updateAbout();
                }
            });
        }
        function getTeam() {
            $.getJSON(companyUrls.team, data => {
                if (data.success) {
                    about.team = data.data;
                    updateTeam();
                }
            });
        }
        /*** about */
        function getNav() {
            $.getJSON(companyUrls.nav, data => {
                if (data.success) {
                    obj.navs = data.data;
                    updateNav();
                }
            });
        }
        function getCompany() {
            $.getJSON(companyUrls.company, data => {
                if (data.success) {
                    obj.company = data.data;
                    updateCompany();
                }
            });
        }
       
        function getMain() {
            $.getJSON(companyUrls.main, data => {
                if (data.success) {
                    obj.mains = data.data;
                    updateMain();
                }
            });
        }
        function getFeature() {
            $.getJSON(companyUrls.feature, data => {
                if (data.success) {
                    obj.feature = data.data;
                    updateFeature();
                }
            });
        }
        function getWork() {
            $.getJSON(companyUrls.work, data => {
                if (data.success) {
                    obj.work = data.data;
                    updateWork();
                }
            });
        }
        function getService() {
            $.getJSON(companyUrls.service, data => {
                if (data.success) {
                    obj.service = data.data;
                    updateService();
                }
            });
        }
        function getSkill() {
            $.getJSON(companyUrls.skill, data => {
                if (data.success) {
                    obj.skill = data.data;
                    updateSkill();
                }
            });
        }
        function getMedia() {
            $.getJSON(companyUrls.media, data => {
                if (data.success) {
                    obj.medias = data.data;
                    updateMedia();
                }
            });
        }
        function getTestimonial() {
            $.getJSON(companyUrls.testimonial, data => {
                if (data.success) {
                    obj.testimonials = data.data;
                    updateTestimonials();
                }
            });
        }
        function getPartner() {
            $.getJSON(companyUrls.partner, data => {
                if (data.success) {
                    obj.partner = data.data;
                    updatePartner();
                }
            });
        }
        function getTheme() {
            $.getJSON(companyUrls.theme, data => {
                if (data.success) {
                    obj.themes = data.data;
                    updateTheme();
                }
            }, data => {
                console.log(data);
            });
        }
        //http://demo.lanrenzhijia.com/2013/pic1201/demos/customJson.html 请求不了
        //$("div#testimonial-slider").owlCarousel({
        //    dataType :'application/json',//'jsonp',
        //    //jsonp: 'jsonpCallback',
        //    jsonPath : 'https://localhost:44392/api/testimonial/get',
        //    jsonSuccess : updateTestimonials
        // });
        var obj = {
            company: {},
            navs:[],
            mains:[],
            feature: {},
            work: {},
            index: true,
            work_page: 0,
            service: {},
            skill: {},
            medias: [],
            testimonials: {},
            partner: {},
            themes: [],
        };
        var portfolio = {
            work: {}
        };
        var about = {
            about: {},
            team: {},
            portfolio: portfolio
        };
        var companyObj = {
            index: obj,
            about: about
        };
        //portfolio
        function updatePorWork() {
            let str = "";
            $("section#portfolio > div.container > div.center").html(" <h2>" + getName(portfolio.work.category) + "</h2> <p class=\"lead\">" + get(desc, portfolio.work.category) + "</p>");
            for (let i = 0; i < portfolio.work.work_categories.length; i++) {
                let it = portfolio.work.work_categories[i];
                str += " <li><a class=\"btn btn-default " + (i == 0 ? "active" : "") + "\" href=\"#\" data-filter=\"" + it.filter + "\">" + getName(it) + "</a></li>";
            }
            $("section#portfolio > div.container > ul").html(str);
            str = "";
            for (let i = 0; i < portfolio.work.works.length; i++) {
              
                let it = portfolio.work.works[i];
                str += " <div class=\"portfolio-item " + it.filter + " col-xs-12 col-sm-4 col-md-3 single-work\"> <div class=\"recent-work-wrap\"> <img class=\"img-responsive\" src=\""
                    + (companyUrls.img + it.logo) + "\" alt=\"\"> <div class=\"overlay\"> <div class=\"recent-work-inner\"> <a class=\"preview\" href=\"" + (companyUrls.img + it.logo)
                    +"\" rel=\"prettyPhoto\"><i class=\"fa fa-plus\"></i></a> </div> </div> </div> </div>";
            }
            $("section#portfolio > div.container > div.row > div.portfolio-items").html(str);
            // portfolio filter
            $(window).load(function () {
                'use strict';
                var $portfolio_selectors = $('.portfolio-filter >li>a');
                var $portfolio = $('.portfolio-items');
                $portfolio.isotope({
                    itemSelector: '.portfolio-item',
                    layoutMode: 'fitRows'

                });

                $portfolio_selectors.on('click', function () {
                    $portfolio_selectors.removeClass('active');
                    $(this).addClass('active');
                    var selector = $(this).attr('data-filter');
                    $portfolio.isotope({
                        filter: selector
                    });
                    return false;
                });
            });
            //Pretty Photo
            $("a[rel^='prettyPhoto']").prettyPhoto({
                social_tools: false
            });
        }
        //about
        function updateAbout() {
            if (about.about) {
                $("div.page-title").css({ "background-image": "url('" + companyUrls.img   + about.about.background_image + "')" });
                $("div.page-title > h1 ").html(getName(about.about));
                let str = " <div class=\"col-md-7\"> <div class=\"about-img\"> <img src=\"" + companyUrls.img  + about.about.logo
                    + "\" alt=\"\"> </div> </div> <div class=\"col-md-5\"> <div class=\"about-content\"> <h2>" + get("title", about.about) + "</h2> <p>" + get(desc, about.about) + "</p> </div> </div>";
                $("section#about-us > div.container > div.row").html(str);
            }
        }
        function updateTeam() {
            if (about.team) {
                let str = " <div class=\"center fadeInDown\"> <h2>" + getName(about.team) + "</h2> <p class=\"lead\">" + get(desc, about.team) + "</p> </div>";
                str += " <div class=\"row\">";
                for (let i = 0; i < about.team.teams.length; i++) {
                    let it = about.team.teams[i];
                    let iconStr = "";
                    if (it.source) {
                        let strs = it.source.split(",");
                        let str1s = it.href.split(",");
                        for (let j = 0; j < strs.length; j++) {
                            iconStr += "<a class=\"fa " + strs[j] + "\" href=\"" + str1s[i]+"\"></a>";
                        }
                    }
                    str += " <div class=\"col-md-4 col-sm-6 single-team\"> <div class=\"inner\"> <div class=\"team-img\"> <img src=\"" +
                        companyUrls.img + it.logo
                        + "\" alt=\"\"> </div> <div class=\"team-content\"> <h4>" + getName(it) + "</h4> <span class=\"desg\">" + getName(it.service) + "</span> <div class=\"team-social\"> " + iconStr + " </div> </div> </div> </div>";
                }
                str += "</div>";
                $("section#team-area > div.container").html(str);
            }
        }
        //index
        function updateNav() {
            if (obj.navs) {
                let str = "";
                for (let i = 0; i < obj.navs.length; i++) {
                    let it = obj.navs[i];
                    let active = location.href.indexOf(it.href)>-1 ? "active" : "";
                    if (it.children && it.children.length > 0) {
                        active = "";
                        for (let j = 0; j < it.children.length; j++) {
                            if (location.href.indexOf(it.children[j].href)>-1) {
                                active = "active";
                                break;
                            }
                        }
                        str += "<li class=\"" + (active) + "\"><a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">" + getName(it) + "<i class=\"fa fa-angle-down\"></i></a>";
                        str += " <ul class=\"dropdown-menu\">";
                        for (let j = 0; j < it.children.length; j++) {
                            str += "<li><a href=\"" + it.children[j].href + "\">" + getName(it.children[j]) + "</a></li>";
                        }
                        str += "</ul> </li>";
                    }
                    else {
                        str += "<li class=\"" + (active) + "\"><a href=\"" + it.href + "\">" + getName(it) + "</a></li>";
                    }
                }
                $("ul.nav.navbar-nav").html(str);
            }
        }
        function updateCompany() {

        }
        function updateMain() {
            if (obj.mains) {
                let count = obj.mains.length;
                let str = " <ol class=\"carousel-indicators\">";
                for (let i = 0; i < count; i++) {
                    str += "<li data-target=\"#main-slider\" data-slide-to=\"" + i + "\" " + (i == 0 ?"class=\"active\"":"")+"></li>";
                }
                str += " </ol>";
                str += " <div class=\"carousel-inner\">";
                for (let i = 0; i < count; i++) {
                    let it = obj.mains[i];
                    let src = companyUrls.img + it.background_image;
                    str += " <div class=\"item " + (i == 0 ? "active" : "") + "\" style=\"background-image: url('" + src
                        + "')\"> <div class=\"container\"> <div class=\"row\"> <div class=\"col-md-7\"> <div class=\"carousel-content\"> <h1 class=\"animation animated-item-1\">" +
                        getName(it, language)
                    +"</h1> <div class=\"animation animated-item-2\">" + get(desc, it, language) + " </div> <a class=\"btn-slide animation animated-item-3\" href=\"" + it.button_href1 + "\">" + get("button_name1", it)
                        + "</a> <a class=\"btn-slide white animation animated-item-3\" href=\"" + it.button_href2 + "\">" + get("button_name2", it)+"</a> </div> </div> </div> </div> </div>";
                }
                str += "</div>";
                $("section#main-slider > div").prepend(str);
                $('#main-carousel').carousel({
                    interval: 5000
                });
            }
        }
        function updateFeature() {
            if (obj.feature) {
                let str = " <div class=\"center fadeInDown\"> <h2>" + getName(obj.feature, language) + "</h2> <p class=\"lead\">" +
                    get("description", obj.feature, language) + "</p> </div> <div class=\"row\"><div class=\"features\">";
                for (let i = 0; i < obj.feature.features.length; i++) {
                    let it = obj.feature.features[i];
                    str += "<div class=\"col-md-3 col-sm-4 fadeInDown\" data-wow-duration=\"1000ms\" data-wow-delay=\"600ms\"> <div class=\"feature-wrap\"> <div class=\"icon\"> <i class=\"fa " +
                        it.feature + "\"></i> </div> <h2>" + getName(it, language) + "</h2> <p>" + get("description", it, language) + "</p> </div> </div>";
                }
                str += "</div></div>";
                $("section#feature > div.container").html(str);
            }
        }
        function show_more() {
            let str = "";
            for (let i = 6 * obj.work_page; i < obj.work.recent_works.length && i < 6 * (obj.work_page + 1); i++) {
                let it = obj.work.recent_works[i];
                let src = companyUrls.img + it.logo;
                str += " <div class=\"col-xs-12 col-sm-6 col-md-4 single-work\"> <div class=\"recent-work-wrap\"> <img class=\"img-responsive\" src=\"" + src
                    + "\" alt=\"\"> <div class=\"overlay\"> <div class=\"recent-work-inner\"> <a class=\"preview\" href=\"" + src + "\" rel=\"prettyPhoto\"><i class=\"fa fa-plus\"></i></a> </div> </div> </div> </div>";
            }
            obj.work_page++;
            $("section#recent-works > div.container > div.row").append(str);
            //Pretty Photo
            $("a[rel^='prettyPhoto']").prettyPhoto({
                social_tools: false
            });
        }
        function updateWork() {
            if (obj.work) {
                let str = " <div class=\"center fadeInDown\"> <h2>" +
                    getName(obj.work, getLanguage()) + "</h2> <p class=\"lead\">" + get("description", obj.work, getLanguage()) + "</p> </div> <div class=\"row\">";
                for (let i = 0; i < obj.work.works.length && i < 6; i++) {
                    let it = obj.work.works[i];
                    let src = companyUrls.img + it.logo;
                    str += " <div class=\"col-xs-12 col-sm-6 col-md-4 single-work\"> <div class=\"recent-work-wrap\"> <img class=\"img-responsive\" src=\"" + src
                        + "\" alt=\"\"> <div class=\"overlay\"> <div class=\"recent-work-inner\"> <a class=\"preview\" href=\"" + src + "\" rel=\"prettyPhoto\"><i class=\"fa fa-plus\"></i></a> </div> </div> </div> </div>";
                }
                obj.work_page = 1;
                str += "</div>";
                $("section#recent-works > div.container ").prepend(str);
                //Pretty Photo
                $("a[rel^='prettyPhoto']").prettyPhoto({
                    social_tools: false
                });
            }
        }
        function updateService() {
            let str = " <div class=\"container\"> <div class=\"center fadeInDown\">";
            str += " <h2>" + getName(obj.service, getLanguage()) + "</h2>";
            str += " <p class=\"lead\">" + get("description", obj.service, getLanguage()) + "</p>";
            str += "  </div>";
            //let three = false;
            str += "<div class=\"row\">";
            for (let i = 0; i < obj.service.services.length; i++) {
                //if (i % 3 == 0) {
                //    if (three) {
                //        str += "</div>";
                //        three = false;
                //    }
                //    else {
                //        str += "<div class=\"row\">";
                //        three = true;
                //    }
                //}
                let item = obj.service.services[i];
                str += " <div class=\"col-sm-6 col-md-4\"> <div class=\"media services-wrap fadeInDown\"> <div class=\"pull-left\"> <img class=\"img-responsive\" src=\"" +
                    (companyUrls.img + item.logo)
                    + "\"> </div> <div class=\"media-body\"> <h3 class=\"media-heading\">" + getName(item, getLanguage())
                    + "</h3> <p>" + get("description", item, getLanguage()) + "</p> </div> </div> </div>";
            }
            //if (!three) {
            //     str += "</div>";
            //}
            str += "</div>";
            $("#services").html(str);
        }
        function updateSkill() {
            if (obj.skill) {
                $("section.skill-area").css({
                    "background-image": "url(" + companyUrls.img + obj.skill.background_image + ")",
                    "background-size": "cover",
                    "background-position": "center"
                });
                let str = " <div class=\"col-sm-12 fadeInDown\"> <div class=\"skill\"> <h2>" +
                    getName(obj.skill, getLanguage()) + "</h2> <p>" + get("description", obj.skill, getLanguage()) + "</p> </div> </div>";
                let ou = false;
                for (let i = 0; i < obj.skill.skills.length; i++) {
                    if (i % 2 == 0) {
                        if (ou) {
                            ou = false;
                            str += "</div>";
                        }
                        else {
                            ou = true;
                        }
                        str += " <div class=\"col-sm-6\">";
                    }
                    let item = obj.skill.skills[i];
                    str += " <div class=\"progress-wrap\"> <h3>" + getName(item, getLanguage()) + "</h3> <div class=\"progress\"> <div class=\"progress-bar \" role=\"progressbar\" aria-valuenow=\"40\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + item.process + "%;" + item.style + "\"> <span class=\"bar-width\">" + item.process + "%</span> </div> </div> </div>";
                }
                if (!ou) {
                    str += "</div>";
                }
                $("div.skill_html").html(str);
            }
        }
        function updateMedia() {
            if (obj.medias && obj.medias.length > 0) {
                let prefixStr = " <div class=\"parrent pull-left\"> <ul class=\"nav nav-tabs nav-stacked\">";
                let suffixStr = " <div class=\"parrent media-body\"> <div class=\"tab-content\">";
                for (let i = 0; i < obj.medias.length; i++) {
                    const item = obj.medias[i];
                    let j = i + 1;
                    prefixStr += " <li class=\"" + (i == 0 ? "active  " : "") + "\"><a href=\"#tab" + j + "\" data-toggle=\"tab\" >" + getName(item, getLanguage()) + "</a></li>";
                    suffixStr += "<div class=\"tab-pane fade " + (i == 0 ? " active in" : "") + "\" id=\"tab" + j + "\">";
                    suffixStr += get("body", item, getLanguage()).replace("/company/images", companyUrls.img + "upload/imgs") + "</div>";
                }
                prefixStr += "</ul></div>";
                suffixStr += "</div></div>";
                let str = prefixStr + suffixStr;
                //console.log(str);<img class="img-responsive" src="/company/images/tab2.png">
                $("div.meida_html").html(str);
                $("a[rel^='prettyPhoto']").prettyPhoto({
                    social_tools: false
                });
            }
        }
        function updateTestimonials(data) {
            if (data && data.success) {
                obj.testimonials = data;
            }
            var name = getName(obj.testimonials, language);
            $("#testimonial > div > div > h2").html(getName(obj.testimonials, language));
            $("#testimonial > div > div > p.lead").html(get("description", obj.testimonials, language));
            var str = "";
            for (let i in obj.testimonials.testimonials) {
                str += "<div class=\"single-slide\"><div class=\"slide-img\">";
                str += "<img src=\"" + companyUrls.img + obj.testimonials.testimonials[i].logo/*person_pic.name*/ + "\" width='100' height='100' alt=\"\">";
                str += "</div><div class=\"content\">";
                if (obj.testimonials.testimonials[i].review == 0) {
                    str += "<img src=\"images/review.png\" alt=\"\">";
                }
                else {
                    str += "<img src=\"images/review.png\" alt=\"\">";
                }
                str += "<p>" + get("description", obj.testimonials.testimonials[i], language) + "</p>";
                str += "<h4>" + getName(obj.testimonials.testimonials[i], language) + "</h4>";
                str += "</div></div>";
            }
            $("#testimonial > div  > div.testimonial-slider").html(str);
            $("div.testimonial-slider").addClass("owl-carousel");
            //http://www.jq22.com/jquery-info6010
            //https://bbs.csdn.net/topics/390516949 起冲突 2 个以上 切换界面循环失效  id不一致就可以了
            refreshSlider();
        }
        function updatePartner() {
            $("#partner").css(
                {
                    "background-image": "url(" + companyUrls.img + obj.partner.background_image + ")",
                    "background-size": "cover",
                    "background-position": "center"
                });
            $("#partner > div > div > h2").html(getName(obj.partner, language));
            $("#partner > div > div > p.lead").html(get("description", obj.partner, language));
            var str = "";
            for (let i = 0; i < obj.partner.our_partners.length; i++) {
                let it = obj.partner.our_partners[i];
                str += "<li><a href=\"" + it.href + "\"><img class=\"img-responsive fadeInDown\" data-wow-duration=\"1000ms\" data-wow-delay=\"" + (300 * (i + 1)) + "ms\" src=\"" + (companyUrls.img + it.logo) + "\"></a></li>";
            }
            $("#partner > div.container > div.partners > ul").html(str);
        }
        function updateTheme() {
            if (obj.themes) {
                var str = "";
                for (let category in obj.themes) {
                    str += " <div class=\"col-md-3 col-sm-6\"> <div class=\"widget\"><h3>" + getName(obj.themes[category], language) + "</h3><ul>";
                    for (let theme in obj.themes[category].themes) {
                        // console.log(obj.theme_categorys[category].themes[theme]);
                        str += "<li><a href=\"" + obj.themes[category].themes[theme].href + "\">" + getName(obj.themes[category].themes[theme], language) + "</a></li>";
                    }
                    str += "</ul></div></div>";
                }
                $(".themes").html(str);
            }
        }
        return {
            show_more: show_more,
            index: function () {
                getNav();
                getMain();
                getFeature();
                getWork();
                getService();
                getSkill();
                getMedia();
                getTestimonial();
                getPartner();
                getTheme();
            },
            about: function () {
                getNav();
                getAbout();
                getTeam();
                getSkill();
                getTheme();
            },
            service: function () {
                getNav();
                getService();
                getTestimonial();
                getPartner();
                getTheme();
            },
            portfolio: function () {
                getNav();
                getPorWork();
                getTheme();
            },
            blog_item: function () {
                getNav();
                getTheme();
            },
            pricing: function () {
                getNav();
                getTheme();
            },
            blog: function () {
                getNav();
                getTheme();
            },
            contact: function () {
                getNav();
                getTheme();
            }
        };
    }
    //this.reload = false;
    //let oj = init();
    //this.init = function () {
    //    return oj = init();
    //};
    return { init: init };
}
const desc = "description";
var copyrights = {
    chinese: {
        name: ".版权所有。",
        home: "主页",
        about: "关于我们",
        faq: "常见问题",
        contact: "联系我们"
    },
    english: {
        name: ". All Rights Reserved.",
        home: "Home",
        about: "About Us",
        faq: "Faq",
        contact: "Contact Us"
    }
};
var copyright = copyrights.chinese;
switch (language) {
    case "english": copyright = copyrights.english; break;
    default: break;
}
const company = companyUI();
var language = getLanguage();
function baidu() {
    //此乃百度统计代码，请自行删除
    var _hmt = _hmt || [];
    (function () {
        var hm = document.createElement("script");
        hm.src = "https://hm.baidu.com/hm.js?080836300300be57b7f34f4b3e97d911";
        var s = document.getElementsByTagName("script")[0];
        s.parentNode.insertBefore(hm, s);
    })();
}