package com.template.data;

import com.template.model.Column;
import com.template.model.ColumnRelation;
import com.template.model.TableList;
import com.template.model.enums.DataFlag;
import com.utility.hibernate.HibernateFactory;
import com.utility.hibernate.HibernateTemplate;
import com.utility.mybatis.MybatisFactory;
import org.apache.ibatis.session.SqlSession;
import org.hibernate.Session;

import java.util.*;

public class DataFactory {
    public static  void initialData(){
        //创建表结构信息
        HibernateFactory.config("template/hibernate.cfg.xml");
        Session session =HibernateFactory.getSession();
        // Transaction transaction =session.beginTransaction();
        int res= session.createSQLQuery("INSERT into template.t_hui_skin(name,english_name,color) SELECT name,english_name,color from company.skin_info").executeUpdate();
        //transaction.commit();
        HibernateTemplate hibernateTemplate=new HibernateTemplate(session);
        MybatisFactory.config("template/mybatis-config.xml");
        SqlSession sqlSession= MybatisFactory.openSession();
        // hibernate 添加 则出现 外键没关联 直接使用mybaits
        //initialMenu(sqlSession);
        //initialTempalteData(sqlSession);
    }

    private static  void  initTempData(){
        Map<String, TableList> tableLists=new HashMap<String,TableList>();
        initTableList(tableLists);
        initColumn(tableLists);
        initColumnRelation(tableLists);
       /*
         private  long id;//id
    private Column column;//列
    private  TableList tableList;//表格
    private  int order;//排序

    private long columnId;
    private  long tableListId;*/
        TableList tableList=new TableList();
        tableLists.put("HuiIcon", tableList);
        tableList.setTitle("图标");
        tableList.setColumns(new HashSet<>());
        ColumnRelation columnRelation=initId(tableList);
        Column column=columnRelation.getColumn();
        column.setComment("图标编号");

        columnRelation=new ColumnRelation();
        column=new Column("icon","图标");
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);
        initDate(tableList);
}

        private  static  void  initHuiIcon(Map<String,TableList> tableLists){
            TableList tableList=new TableList();
            tableLists.put("HuiIcon", tableList);
            tableList.setTitle("图标");
            tableList.setColumns(new HashSet<>());
            ColumnRelation columnRelation=initId(tableList);
            Column column=columnRelation.getColumn();
            column.setComment("图标编号");

            columnRelation=new ColumnRelation();
            column=new Column("icon","图标");
            columnRelation.setColumn(column);
            tableList.getColumns().add(columnRelation);
            initDate(tableList);
        }
    private  static  void  initColumnRelation(Map<String,TableList> tableLists){
        TableList tableList=new TableList();
        tableLists.put("ColumnRelation", tableList);
        tableList.setTitle("表单列关联");
        tableList.setColumns(new HashSet<>());
        ColumnRelation columnRelation=initId(tableList);
        Column column=columnRelation.getColumn();
        column.setComment("表单列编号");

        columnRelation=new ColumnRelation();
        column=new Column();
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);
        column.setInt("order","排序");

        columnRelation=new ColumnRelation();
        column=new Column();
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);
        column.setInt("columnId","列编号");
        column.setFk(true);
        column.setReferenceColumn("id");
        column.setReferenceTable("Column");

        columnRelation=new ColumnRelation();
        column=new Column();
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);
        column.setInt("tableListId","表格编号");
        column.setFk(true);
        column.setReferenceColumn("id");
        column.setReferenceTable("tableList");
        initDate(tableList);
    }

    private  static  void  initColumn(Map<String,TableList> tableLists){
    TableList tableList=new TableList();
    tableLists.put("Column", tableList);
    tableList.setTitle("列");
    tableList.setColumns(new HashSet<>());
    ColumnRelation columnRelation=initId(tableList);
    Column column=columnRelation.getColumn();
    column.setComment("列编号");

    columnRelation=new ColumnRelation();
    column=new Column("database","列所在数据库");
    columnRelation.setColumn(column);
    tableList.getColumns().add(columnRelation);

    columnRelation=new ColumnRelation();
    column=new Column("table","列所在表");
    columnRelation.setColumn(column);
    tableList.getColumns().add(columnRelation);

    columnRelation=new ColumnRelation();
    column=new Column("referenceTable","列所在外键表");
    columnRelation.setColumn(column);
    tableList.getColumns().add(columnRelation);

    columnRelation=new ColumnRelation();
    column=new Column("referenceColumn","列所在外键列");
    columnRelation.setColumn(column);
    tableList.getColumns().add(columnRelation);

    columnRelation=new ColumnRelation();
    column=new Column();
    columnRelation.setColumn(column);
    tableList.getColumns().add(columnRelation);
    column.setBool("isPk","列是否主键");

    columnRelation=new ColumnRelation();
    column=new Column();
    columnRelation.setColumn(column);
    tableList.getColumns().add(columnRelation);
    column.setBool("isIdentity","列是否自增");

    columnRelation=new ColumnRelation();
    column=new Column();
    columnRelation.setColumn(column);
    tableList.getColumns().add(columnRelation);
    column.setBool("isFk","列是否外键");

    columnRelation=new ColumnRelation();
    column=new Column("column","列名");
    columnRelation.setColumn(column);
    tableList.getColumns().add(columnRelation);

    columnRelation=new ColumnRelation();
    column=new Column("dataType","列sql数据类型");
    columnRelation.setColumn(column);
    tableList.getColumns().add(columnRelation);

    columnRelation=new ColumnRelation();
    column=new Column();
    columnRelation.setColumn(column);
    tableList.getColumns().add(columnRelation);
    column.setBool("isNull","列长度");

    columnRelation=new ColumnRelation();
    column=new Column();
    columnRelation.setColumn(column);
    tableList.getColumns().add(columnRelation);
    column.setString("comment","列注释");

    columnRelation=new ColumnRelation();
    column=new Column();
    columnRelation.setColumn(column);
    tableList.getColumns().add(columnRelation);
    column.setString("title","标题");

    columnRelation=new ColumnRelation();
    column=new Column();
    columnRelation.setColumn(column);
    tableList.getColumns().add(columnRelation);
    column.setString("flag","数据类型");

    initDate(tableList);
}

    private  static  void  initTableList(Map<String,TableList> tableLists){
        TableList tableList=new TableList();
        tableLists.put("TableList", tableList);
        tableList.setTitle("表格");
        tableList.setColumns(new HashSet<>());
        ColumnRelation columnRelation=initId(tableList);
        Column column=columnRelation.getColumn();
        column.setComment("表格编号");

        columnRelation=new ColumnRelation();
        column=new Column("title","表格名称");
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);

        columnRelation=new ColumnRelation();
        column=new Column();
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);
        column.setBool("add","添加");

        columnRelation=new ColumnRelation();
        column=new Column();
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);
        column.setBool("modify","修改");

        columnRelation=new ColumnRelation();
        column=new Column();
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);
        column.setBool("delete","删除");

        columnRelation=new ColumnRelation();
        column=new Column();
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);
        column.setBool("tableModify","修改");

        columnRelation=new ColumnRelation();
        column=new Column();
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);
        column.setBool("tableDelete","删除");

        columnRelation=new ColumnRelation();
        column=new Column();
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);
        column.setBool("tablePreview","预览");

        initDate(tableList);
    }

    private static  void  initalTemplateShopData(SqlSession sqlSession){
        Map<String,TableList> tableLists=new HashMap<String,TableList>();
        TableList tableList=new TableList();
        tableLists.put("Product", tableList);
        tableList.setTitle("商品");
        tableList.setColumns(new HashSet<>());
        ColumnRelation columnRelation=initId(tableList);
        Column column=columnRelation.getColumn();
        column.setComment("商品分类编号");

        columnRelation=new ColumnRelation();
        column=new Column("Sn","序列号");
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);

        columnRelation=new ColumnRelation();
        column=new Column();
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);
        column.setColumn("price");
        column.setComment("价格");
        column.setFlag(DataFlag.Decimal);

        columnRelation=new ColumnRelation();
        column=new Column();
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);
        column.setColumn("marketPrice");
        column.setComment("市场价格");
        column.setFlag(DataFlag.Decimal);

        columnRelation=new ColumnRelation();
        column=new Column("image","图片");
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);

        columnRelation=new ColumnRelation();
        column=new Column();
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);
        column.setColumn("stock");
        column.setComment("库存");
        column.setFlag(DataFlag.Int);

        columnRelation=new ColumnRelation();
        column=new Column();
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);
        column.setColumn("deliveryWeight");
        column.setComment("交货重量");
        column.setFlag(DataFlag.Decimal);
 /*       initProductCategory(tableLists);
        private  boolean isMarketable;//是否 畅销
        private  boolean isUnifiedSpec;//是否统一规范
        private  boolean isList;//是否 集合
        private  String introduction;//介绍
        private  Integer sales;//出售 数量
        private ProductCategory category;//商品 分类 信息
        private  Integer deliveryType;//交货类型
        private  BigDecimal deliveryFees;//交货 费用

        private  Integer sellerId;//卖家 id
        private  Integer categoryId;//商品 分类 id
        private  Integer templateId;//交货 模板 id
        private Date startDate;//开始时间
        private  Date endDate;//结束时间*/
    }

    private  static  ColumnRelation initId(TableList tableList){
        ColumnRelation columnRelation=new ColumnRelation();
        Column column=new Column();
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);
        column.setColumn("Id");
        column.setFlag(DataFlag.Long);
        column.setFk(true);
        column.setIdentity(true);
        return  columnRelation;
    }

    private static void  initProductCategory( Map<String,TableList> tableLists){
        TableList tableList=new TableList();
        tableLists.put("ProductCategory", tableList);
        tableList.setTitle("商品分类");
        tableList.setColumns(new HashSet<>());
        ColumnRelation columnRelation=initId(tableList);
        Column column=columnRelation.getColumn();
        column.setComment("商品分类编号");


        columnRelation=new ColumnRelation();
        column=new Column("Name","商品名称");
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);

        columnRelation=new ColumnRelation();
        column=new Column("ImgPath","商品素材路劲");
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);

        columnRelation=new ColumnRelation();
        column=new Column("ParentId","商品分类父分类编号");
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);
        column.setReferenceTable("ProductCategory");
        column.setFlag(DataFlag.Long);
        column.setLength(0);

        initDate(tableList);
    }

    private  static  void  initDate(TableList tableList){
        ColumnRelation columnRelation=new ColumnRelation();
        Column column=new Column();
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);
        column.setColumn("CreateDate");
        column.setComment("创建时间");
        column.setFlag(DataFlag.Date);

        columnRelation=new ColumnRelation();
         column=new Column();
        columnRelation.setColumn(column);
        tableList.getColumns().add(columnRelation);
        column.setColumn("UpdateDate");
        column.setComment("更新时间");
        column.setFlag(DataFlag.Date);
    }

    private static  void  initalTemplateData(){
        //写死不然每个版本写得累

    }

//    private  static  void  initialTempalteData(SqlSession sqlSession){
//        id=0;
//        long tId=0;
//        String result= HttpUtils.doGet("https://localhost:5001/template/api/v1/template/get/template");
//        CSharpResponseApi<ArrayList<Column>> columns=CSharpResponseApi.toObject(result,Column.class);
//        HashMap<String,TableList> tableLists=new HashMap<String,TableList>();
//        for (Column column:columns.getData()) {
//            if(column.getTable().equals("t_desc")){
//                continue;
//            }
//          /*  TableList tableList=null;
//           if(!tableLists.containsKey(column.getTable())){
//               tableList=new TableList();
//               tableLists.put(column.getTable(),tableList);
//               tableList.setTitle(column.getTable());
//               tableList.setColumns(new HashSet<>());
//               int res=sqlSession.insert("tableList.add",tableList);
//               tId+=res;
//               tableList.setId(tId);
//           }else{
//               tableList=tableLists.get(column.getTable());
//
//           }*/
//            int res=sqlSession.insert("column.add",column);
//            id+=res;
//            column.setId(id);
//            /*ColumnRelation columnRelation=new ColumnRelation();
//            columnRelation.setColumnId(column.getId());
//            columnRelation.setTableListId(tableList.getId());
//            tableList.getColumns().add(columnRelation);*/
//        }
//        Iterator<String> iterator=tableLists.keySet().iterator();
//        while (iterator.hasNext()){
//            String key=iterator.next();
//            for (ColumnRelation columnRelation: tableLists.get(key).getColumns()) {
//                int res=sqlSession.insert("columnRelation.add",columnRelation);
//            }
//        }
//    }

//    private  static  void initialMenu(SqlSession sqlSession){
//        id=0;
//        Set<HuiMenu> menus=new HashSet<>();
//        HuiMenu template=new HuiMenu();
//        menus.add(template);
//        template.setName("模板");
//        template.setChildren(new HashSet<>());
//        HuiMenu icon=new HuiMenu();
//        template.getChildren().add(icon);
//        icon.setName("图标");
//        icon.setHref("icon.html");
//        HuiMenu menu=new HuiMenu();
//        template.getChildren().add(menu);
//        menu.setName("菜单");
//        menu.setHref("menu.html");
//        //主键获取不到(hibernate 可以) //mybatis第一次初始化可以不然异常
//        recursionMenu(menus,sqlSession,null);
//    }
//
//    private   static   long id=0;
//
//    private   static    void recursionMenu(Set<HuiMenu> data, SqlSession sqlSession, Long parentId){
//
//        for (HuiMenu it:data ) {
//            if(parentId!=null){
//                it.setParentId(parentId);
//            }
//            int res=sqlSession.insert("huiMenu.add",it);
//            id+=res;
//            it.setId(id);
//            if(it.getChildren()!=null&&it.getChildren().size()>0){
//                recursionMenu(it.getChildren(),sqlSession,it.getId());
//            }
//        }
//    }
}
