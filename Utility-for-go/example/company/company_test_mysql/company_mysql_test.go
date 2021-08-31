package company_test_mysql

import (
	"context"
	"encoding/json"
	"github.com/jinzhu/gorm"
	//_ "github.com/jinzhu/gorm/dialects/mysql"
	"testing"
	"utility/util"
	"github.com/jmoiron/sqlx"
	_ "github.com/go-sql-driver/mysql"
)
var gorm_util_test *util.GormUtil

func init ()  {

}
var db1 *gorm.DB
func initDb (t *testing.T)  {
	gorm_util_test =&util.GormUtil{Addrs: "root:wjp930514.@(127.0.0.1:3306)/company?charset=utf8mb4&parseTime=True&loc=Local",Dialet: "mysql"}

	//gorm_util_test.Conn() //连接 不上 应该抛出异常 但 没反应
	//t.Fatal 提前 终止 不会执行下去 了
	/*if gorm_util_test.Db==nil{
		t.Log("conne mysql fail")
		//return
	}*/
	db, err := gorm.Open(gorm_util_test.Dialet, gorm_util_test.Addrs)
	if err!= nil{
		t.Log("connection database fail")
		panic(err)
	}
	db1=db
	gorm_util_test.Db=db
	if gorm_util_test.Db==nil{
		t.Log("re conne mysql fail")
		return
	}
	t.Log("re conne mysql suc")
}
func TestMysqlInit(t *testing.T) {
	t.Log("TestMysqlInit")
	//initDb(t)
}
type TestBaseEntity struct {
	Id int `gorm:"primary_key;AUTO_INCREMENT";json:"id"` //id int64 not support 主键编号
	//Lanage string `json:"lanage"`
	//CreateDate time.Time `json:"create_date";yaml:"create_date"` //创建时间
	//ModifyDate time.Time  `json:"modify_date";yaml:"modify_date"`//修改时间
	Enable bool `json:"enable"` //是否启用
}

type TestNavEntity struct {
	TestBaseEntity
	Href string `yaml:"href";gorm:"column:href"`//品牌链接地址
	Name string  `yaml:"name";gorm:"column:name"`
	EnglishName string  `yaml:"english_name";gorm:"column:english_name"`
	//ParentId int
}

func  TestInit(t *testing.T)  {
	database, err := sqlx.Open("mysql", "root:wjp930514.@tcp(127.0.0.1:3306)/company")
	//database, err := sqlx.Open("数据库类型", "用户名:密码@tcp(地址:端口)/数据库名")
	if err!=nil{
		t.Log("conn mysql fail")
		return
	}
	var work_categories [] map[string]interface{}
	workCategory(work_categories,database,t)

	rows,err:=database.QueryContext(context.Background(),"select id ,enable,category_id,img_id from work_info")
	if err!=nil{
		t.Log("query  fail")
		return
	}
	var works [] map[string]interface{}
	for rows.Next() {
		var (
			id int
			enable bool
			category_id int
			img_id int
		)
		rows.Scan(&id ,&enable,&category_id,&img_id)
		works=append(works,map[string]interface{}{ "id":id,"enable":enable,
			"category_id":category_id,"img_id":img_id })
		t.Log(category_id)
	}

}

func  workCategory(work_categories [] map[string]interface{},database *sqlx.DB,t *testing.T)  {
	rows,err:=database.QueryContext(context.Background(),"select id ,enable,name,english_name,filter,work_id,parent_id from work_category_info")
	if err!=nil{
		t.Log("query  fail")
		return
	}
	for rows.Next() {
		var (
			id int
			enable bool
			name string
			english_name string
			filter  string
			work_id int
			parent_id int
		)
		rows.Scan(&id ,&enable,&name,&english_name,&filter,&work_id,&parent_id)
		work_categories=append(work_categories,map[string]interface{}{ "id":id,"enable":enable,"name":name,
			"english_name" : english_name,"filter":filter,"work_id":work_id,"parent_id":parent_id })
		t.Log(name)
	}
}
//查询 值 怎么绑定 了 获取不到值
func bTestNav(t *testing.T) {
	initDb(t)
	//id Id, create_date, modify_date, enable Enable, name Name, english_name EnglishName, href Href, parent_id
	//rows,err:= gorm_util_test.Db.Raw("select * from nav_info").Rows()
	//select id relfe ex
	//select id Id  pass
	var nav1 TestNavEntity
	db1.Raw("select id Id,enable Enable, name Name, english_name EnglishName, href Href from nav_info limit 1,1").Scan(&nav1)//nav1 ex
	j1,err:=json.Marshal(nav1)//{}
	if err!=nil{
		t.Log("select nav json fail")
		return
	}
	t.Log(string(j1))
	return
	rows,err:= db1.Raw("select id Id,enable Enable, name Name, english_name EnglishName, href Href from nav_info").Rows()
	if err!=nil{
		t.Log("select nav fail")
		return
	}
	//defer  rows.Close()

	//i:=0
	for rows.Next() {
		var nav TestNavEntity
		//rows.Scan(&nav)//null
		err=db1.ScanRows(rows,&nav)//null
		if err!=nil{
			t.Log("select nav bind mode  fail")
			continue
		}
		j,err:=json.Marshal(nav)//{}
		if err!=nil{
			t.Log("select nav json fail")
			continue
		}
		t.Log(string(j))
	}
	rows.Close()
}
//看不 出来  t.Log 没信息输出 -v
func aTestNav(t *testing.T) {

	//id create_date modify_date enable name english_name href parent_id
	rows,err:= gorm_util_test.Db.Raw("select * from nav_info").Rows()
	//rows,err:= gorm_util_test.Db.Raw("select id, create_date, modify_date, enable, name, english_name, href, parent_id from nav_info").Rows()
	if err!=nil{
		t.Log("select nav fail")
		return
	}
	defer  rows.Close()
	//var navs * map[string]interface{}
	//var naves  []*map[string]interface{}=make([]*map[string]interface{},20)
	var id int
	var create_date string
	var modify_date string
	var enable bool
	var name string
	var english_name string
	var href string
	var parent_id int
	//i:=0
	for rows.Next() {
		//rows.Scan(&navs)//null
		//rows.Scan(&id,&create_date,&create_date,&modify_date,&enable,&name,&english_name,&href,&parent_id)
		//navs={"id":id,"create_date":create_date}
		navs:=make(map[string]interface{},10)
		//naves = append(naves, navs)
		navs["id"]=&id
		navs["create_date"]=&create_date
		navs["update_date"]=&modify_date
		navs["enable"]=&enable
		navs["name"]=&name
		navs["english_name"]=&english_name
		navs["href"]=&href
		navs["parent_id"]=&parent_id
		//gorm_util_test.Db.ScanRows(rows,&navs)//null
		j,err:=json.Marshal(&navs)//{}
		if err!=nil{
			t.Log("select nav json fail")
			continue
		}
		t.Log(string(j))
	}

}
