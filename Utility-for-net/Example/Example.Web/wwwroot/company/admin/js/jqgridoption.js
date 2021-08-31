var options = {
    //url:'',//获取数据的地址
    datatype: "local",//从服务器端返回的数据类型，默认xml。可选类型：xml，local，json，jsonnp，script，xmlstring，jsonstring，clientside
    //mtype:'',//ajax提交方式。POST或者GET，默认GET
    //colNames: ['ID Number', 'Last Sales', 'Name', 'Stock', 'Ship via', 'Notes'],//列显示名称，是一个数组对象 存在优先级最高
    //常用到的属性：name 列显示的名称；index 传到服务器端用来排序用的列名称；width 列宽度；align 对齐方式；sortable 是否可以排序
    colModel: [],
    pager: '#page',//定义翻页用的导航栏，必须是有效的html元素。翻页工具栏可以放置在html页面任意位置
    rowNum: 10,//在grid上显示记录条数，这个参数是要被传递到后台
    rowList: [10, 20, 30],//一个下拉选择框，用来改变显示记录数，当选择时会覆盖rowNum参数传递到后台
    sortname: 'id',//默认的排序列。可以是列名称或者是一个数字，这个参数会被提交到后台
    viewrecords: true,//定义是否要显示总记录数
    caption: "Date Picker Integration",//表格名称
    '[a1]': null,//对ajax参数进行全局设置，可以覆盖ajax事件
    '[a2]': null,//对ajax的select参数进行全局设置
    altclass: 'ui-priority-secondary',//用来指定行显示的css，可以编辑自己的css文件，只有当altRows设为 ture时起作用
    altRows: '',//设置表格 zebra-striped 值 boolean
    autoencode: false,//对url进行编码
    autowidth: true,//如果为ture时，则当表格在首次被创建时会根据父元素比例重新调整表格宽度。如果父元素宽度改变，为了使表格宽度能够自动调整则需要实现函数：setGridWidth
    cellLayout: 5,//定义了单元格padding + border 宽度。通常不必修改此值。初始值为
    cellEdit: true,//启用或者禁用单元格编辑功能
    cellsubmit: 'remote',//定义了单元格内容保存位置
    cellurl: '',//单元格提交的url
    datastr: '',//xmlstring或者jsonstring
    deselectAfterSort: true,//只有当datatype为local时起作用。当排序时不选择当前行
    direction: 'ltr',//表格中文字的显示方向，从左向右（ltr）或者从右向左（rtr）
    editurl: "",//定义对form编辑时的url
    emptyrecords: "没有数据",//当返回的数据行数为0时显示的信息。只有当属性 viewrecords 设置为ture时起作用
    ExpandColClick: true,//当为true时，点击展开行的文本时，treeGrid就能展开或者收缩，不仅仅是点击图片
    ExpandColumn: '',//指定那列来展开tree grid，默认为第一列，只有在treeGrid为true时起作用
    '[a3]': false,//当为true时，会在翻页栏之上增加一行
    forceFit: false,//当为ture时，调整列宽度不会改变表格的宽度。当shrinkToFit 为false时，此属性会被忽略
    gridstate: 'visible',//定义当前表格的状态：'visible' or 'hidden'
    gridview: false,//构造一行数据后添加到grid中，如果设为true则是将整个表格的数据都构造完成后再添加到grid中，但treeGrid, subGrid, or afterInsertRow 不能用
    height: 'auto',//表格高度，可以是数字，像素值或者百分比 150
    hiddengrid: false,//当为ture时，表格不会被显示，只显示表格的标题。只有当点击显示表格的那个按钮时才会去初始化表格数据。
    hidegrid: true,//启用或者禁用控制表格显示、隐藏的按钮，只有当caption 属性不为空时起效
    hoverrows: false,//当为false时mouse hovering会被禁用
    //描述json 数据格式的数组
    //需要定义jsonReader来跟服务器端返回的数据做对应，其默认值
    jsonReader: {
        root: "rows",//包含实际数据的数组
        page: "page",//当前页
        total: "total",//总页数
        records: "records",//查询出的记录数
        repeatitems: true,//指明每行的数据是可以重复的，如果设为false，则会从返回的数据中按名字来搜索元素，这个名字就是colModel中的名字
        cell: "cell",//当前行的所有单元格
        id: "id",//行id id属性值为“invid”。 一旦当此属性设为false时，我们就不必把所有在colModel定义的name值都赋值。因为是按name来进行搜索元素的，所以他的排序也不是按colModel中指定的排序结果。
        userdata: "userdata",//用户数据（user data） 在某些情况下，我们需要从服务器端返回一些参数但并不想直接把他们显示到表格中，而是想在别的地方显示，那么我们就需要用到userdata标签
        subgrid: {
            root: "rows",
            repeatitems: true,
            cell: "cell"
        }
    },
    lastpage: 0,//只读属性，定义了总页数
    lastsort: 0,//只读属性，定义了最后排序列的索引，从0开始
    loadonce: false,//如果为ture则数据只从服务器端抓取一次，之后所有操作都是在客户端执行，翻页功能会被禁用
    loadui: 'enable',//当执行ajax请求时要干什么。disable禁用ajax执行提示；enable默认，当执行ajax请求时的提示； block启用Loading提示，但是阻止其他操作
    multikey: '',//只有在multiselect设置为ture时起作用，定义使用那个key来做多选。shiftKey，altKey，ctrlKey
    multiboxonly: false,//只有当multiselect = true.起作用，当multiboxonly 为ture时只有选择checkbox才会起作用
    multiselect: false,//定义是否可以多选
    multiselectWidth: 20,//当multiselect为true时设置multiselect列宽度
    page: 1,//设置初始的页码
    pagerpos: 'center',//指定分页栏的位置
    pgbuttons: true,//是否显示翻页按钮
    pginput: true,//是否显示跳转页面的输入框
    pgtext: '第 {0}页 到 {1}页',//当前页信息
    prmNames: 'none',//Default valuesprmNames: {page:“page”,rows:“rows”, sort: “sidx”,order: “sord”, search:“_search”, nd:“nd”, npage:null} 当参数为null时不会被发到服务器端 array
    postData: [],//此数组内容直接赋值到url上，参数类型：{name1:value1…}
    reccount: 0,//只读属性，定义了grid中确切的行数。通常情况下与records属性相同，但有一种情况例外，假如rowNum=15，但是从服务器端返回的记录数是20，那么records值是20，但reccount值仍然为15，而且表格中也只显示15条记录。
    recordpos: 'right',//定义了记录信息的位置： left, center, right
    records: 'none',//只读属性，定义了返回的记录数 integer
    recordtext: '第{0} 条记录 到 {1} 条记录',//显示记录数信息。{0} 为记录数开始，{1}为记录数结束。viewrecords为ture时才能起效，且总记录数大于0时才会显示此信息
    resizeclass: '',//定义一个class到一个列上用来显示列宽度调整时的效果
    rownumbers: false,//如果为ture则会在表格左边新增一列，显示行顺序号，从1开始递增。此列名为'rn'.
    rownumWidth: 25,//如果rownumbers为true，则可以设置column的宽度
    savedRow: '',//只读属性，只用在编辑模式下保存数据 array
    scroll: false,//创建一个动态滚动的表格，当为true时，翻页栏被禁用，使用垂直滚动条加载数据，且在首次访问服务器端时将加载所有数据到客户端。当此参数为数字时，表格只控制可见的几行，所有数据都在这几行中加载
    scrollOffset: 18,//设置垂直滚动条宽度
    scrollrows: false,//当为true时让所选择的行可见
    selarrrow: [],//只读属性，用来存放当前选择的行
    selrow: null,//只读属性，最后选择行的id
    shrinkToFit: true,//此属性用来说明当初始化列宽度时候的计算类型，如果为ture，则按比例初始化列宽度。如果为false，则列宽度使用colModel指定的宽度
    sortable: false,//是否可排序
    sortorder: 'asc',//排序顺序，升序或者降序（asc or desc）
    subGrid: false,//是否使用suggrid
    subGridModel: [],//subgrid模型
    subGridType: null,//如果为空则使用表格的dataType mixed
    subGridUrl: '',//加载subgrid数据的url，jqGrid会把每行的id值加到url中
    subGridWidth: 20,//subgrid列的宽度
    //表格的工具栏。数组中有两个值，第一个为是否启用，第二个指定工具栏位置（相对于body layer），如：[true,”both”] 。工具栏位置可选值：“top”,”bottom”, “both”. 如果工具栏在上面，则工具栏id为“t_”+表格id；如果在下面则为 “tb_”+表格id；如果只有一个工具栏则为 “t_”+表格id
    toolbar: [true, 'both'],//[false,'']
    totaltime: 0,//只读属性，计算加载数据的时间。目前支持xml跟json数据
    treedatatype: null,//数据类型，通常情况下与datatype相同，不会变 mixed
    treeGrid: false,//启用或者禁用treegrid模式
    treeGridModel: 'Nested',//treeGrid所使用的方法
    treeIcons: [],//树的图标，默认值：{plus:'ui-icon-triangle-1-e',minus:'ui-icon-triangle-1-s',leaf:'ui-icon-radio-off'}
    treeReader: [],//扩展表格的colModel且加在colModel定义的后面
    tree_root_level: 0,//r oot元素的级别，
    userData: [],//从request中取得的一些用户信息
    userDataOnFooter: false,//当为true时把userData放到底部，用法：如果userData的值与colModel的值相同，那么此列就显示正确的值，如果不等那么此列就为空
    width: 'none',//如果设置则按此设置为主，如果没有设置则按colModel中定义的宽度计算 number
    xmlReader: [],//对xml数据结构的描述
    loadtext: '加载中.......',//
};