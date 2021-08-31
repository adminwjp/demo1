const  DefautlQueryData={
    createMin:"#createdatemin",
        createMax:"#createdatemax",
        modifyMin:"#modifydatemin",
        modifyMax:"#modifydatemax",
        name:"#name",
        enable:"#enable",
}

function emptyQuery(options) {
    options=options|DefautlQueryData;

    let queryData = Object.create({});
    let createMin = $(options.createMin).val();
    if (createMin) {
        queryData.create_start_date = createMin;
    }
    let createMax = $(options.createMax).val();
    if (createMax) {
        queryData.create_end_date = createMax;
    }
    let modifyMin = $(options.modifyMin).val();
    if (modifyMin) {
        queryData.modify_start_date = modifyMin;
    }
    let modifyMax = $(options.modifyMax).val();
    if (modifyMax) {
        queryData.modify_end_date = modifyMax;
    }
    let name = $(options.name).val();
    if (name) {
        queryData.name = name;
    }
    let checkbox = $(options.enable).val();
    if (checkbox != "") {
        queryData.enable = checkbox == "true" ? true : false;
    }
    //console.log(queryData);
    return queryData;
}

const adminUrl="http://127.0.0.1:8080/api/admin"
var adminUrls={
    "nav":new UrlEntity("nav")
}
function UrlEntity(name,options) {
    this.add = adminUrl + "/" + name + "/add";
    this.edit = adminUrl + "/" + name + "/edit";
    this.delete = adminUrl + "/" + name + "/delete";
    this.query = adminUrl + "/" + name + "/list";
    this.editstatus = adminUrl + "/" + name + "/editstatus";
    for (const i in options) {
        this[i] = adminUrl + "/" + name +"/"+ options[i];
    }
}

function AdminHelper(options) {
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
        return emptyQuery();
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
    this.getColumns=function (){
        return [
            {
                data: function (item) {
                    return "<div align='center'><input type='checkbox'  name='ckb-jobid' value='" + item.id + "'></div>";
                }
            },
            { data: 'id' },
            {
                data: function (item) {
                    return item.enable ? "<span class=\"label label-success radius\">已启用</span>" : "<span class=\"label radius\">已停用</span>";
                }
            },
            { data: 'language' }
        ];
    };
    this.getDateColumns=function (){
        return [
            {
                data: function (item) {
                    return  item.create_date>0?item.create_date:"none";
                }
            },
            {
                data: function (item) {
                    return  item.modify_date>0?item.modify_date:"none";
                }
            }
        ];
    };
    this.getOptionColumns=function (){
        return [
            {
                data: function (item) {
                    return "<a style=\"text-decoration:none\" onClick=\"" + (options && options.flag == 2 ? "setPublish" : "setEnable") + "(this,'" + self1.url.editstatus + "'," + item.id + ")\" href=\"javascript:;\" title=\""
                        + (options && options.flag == 2 ? (item.enable ? "下架" : "发布"):(!item.enable ? "启用" : "停用")) + "\"><i class=\"Hui-iconfont\">" +
                        (options && options.flag == 2 ? ((!item.enable ? "&#xe603;" : "&#xe6de;")) : (!item.enable ? "&#xe6e1;" : "&#xe631;"))
                        + "</i></a> <a title=\"编辑\" href=\"javascript:;\" onClick=\"operator('edit',this)\" class=\"ml-5\" style=\"text-decoration:none\"><i class=\"Hui-iconfont\">&#xe6df;</i></a>  <a title=\"删除\" href=\"javascript:;\" onclick=\"del('" + self1.url.delete + "'," +
                        item.id + ")\" class=\"ml-5\" style=\"text-decoration:none\"><i class=\"Hui-iconfont\">&#xe6e2;</i></a>";
                }
            }
        ];
    };
    this.getAllColumns=function (columns){
        var cols=[];
        var temps=this.getColumns();
        for (let i=0;i<temps.length;i++){
            cols.push(temps[i]);
        }
        for (let i=0;i<columns.length;i++){
            cols.push(columns[i]);
        }
        temps=this.getDateColumns();
        for (let i=0;i<temps.length;i++){
            cols.push(temps[i]);
        }
        temps=this.getOptionColumns();
        for (let i=0;i<temps.length;i++){
            cols.push(temps[i]);
        }
        return cols;
    };
    this.columns=[];
    this.tableInit = function (self) {
        console.log(self);
        console.log(options);
        console.log($(options.tableId));
        //https://datatables.net/manual/tech-notes/3
        if ( !$.fn.dataTable.isDataTable( options.tableId ) ){

            return;
        }
        $(options.tableId).dataTable({
            "lengthMenu": [5, 10, 25, 50, 75, 100],
            "ordering": false,
            ajax: {
                url: self.url.query+"/1/10",
                method: "post",
               // "serverSide": true,		//打开服务器模式
                data: self.defaultQueryData,
                dataSrc: "data.data",
                dataFilter: function (json) {
                    console.log(json)
                    json = JSON.parse(json);
                    $(options.id+" > span.r > strong").html(json.result.records);
                    var returnData = {
                        "page": 1,
                        "pages": json.data.result.records / 10 + json.data.result.records % 10==0?0:1,
                        "start": 10,
                        "end": 20,
                        "length": 10
                    };
                    returnData.draw = 1;
                    returnData.recordsTotal = json.data.result.records;  //返回数据全部记录
                    returnData.recordsFiltered = json.data.result.records;  //后台不实现过滤功能，每次查询均视作全部结果
                    returnData.data = json.data;  //返回的数据列表
                    return JSON.stringify(returnData);
                }
            },
            "createdRow": self.createdRow,
            columns: self.columns
        });
    };
    var search=options.searchId|"#search";
    $(search).click(function () {
        $(options.tableId).DataTable().ajax.reload();
    });
    var clear=options.clearId|"#clear";
    var queryOptions=options.queryOptions|DefautlQueryData;

    this.clearQueryData=function (){

        $(queryOptions.createMin).val("");
        $(queryOptions.createMax).val("");
        $(queryOptions.modifyMin).val("");
        $(queryOptions.modifyMax).val("");
        $(queryOptions.enable).val("");
        $(queryOptions.name).val("");
    }
    $(clear).click(function () {
        options.clearQueryData();
    });
}