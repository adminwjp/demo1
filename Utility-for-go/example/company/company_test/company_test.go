package company_test

import (
	"github.com/jinzhu/gorm"
	_ "github.com/jinzhu/gorm/dialects/sqlite"
	"testing"
	"time"
	"utility/util"
	 "utility/example/company"
)
var gorm_util *util.GormUtil

func initDb (t *testing.T)  {
	//gorm_util =&util.GormUtil{Addrs: "root:wjp930514.@(127.0.0.1:3306)/company?charset=utf8mb4&parseTime=True&loc=Local",Dialet: "mysql"}
	gorm_util =&util.GormUtil{Addrs: "E:/work/db/sqlite/company_test.db",Dialet: "sqlite3"}
	//gorm_util_test.Conn()
	//t.Fatal 提前 终止 不会执行下去 了
	/*if gorm_util.Db==nil{
		t.Log("conne mysql fail")
		//return
	}*/
	db, err := gorm.Open(gorm_util.Dialet, gorm_util.Addrs)
	if err!= nil{
		t.Log("connection database fail")
		panic(err)
	}
	gorm_util.Db=db
	if gorm_util.Db==nil{
		t.Log("re conne sqlite fail")
		return
	}	// 自动迁移
	db.AutoMigrate(&company.AboutEntity{},&company.CategoryEntity{},&company.CompanyEntity{},
	&company.ImageEntity{},&company.MainEntity{},&company.RelationEntity{},&company.TeamEntity{})
	t.Log("re conne sqlite suc")
}

//var config *model.Cfg
// append make 不能 混合使用 二选一
//单元 测试 什么 玩意 最好 打 日志 不然不知道 啥情况
//gorm 也是 什么 玩意  最好 打 日志 不然不知道 啥情况
//多个不同 数据库 不能 写在 一起 连接 异常 分包
//写得 类 查询绑定 不到 数据 迁移 写 模型类 应该支持  要么 其他 orm or db util
func aTestNav(t *testing.T) {
	initDb(t)
	//invalid memory address or nil pointer dereference
	navs :=make([]*company.CategoryEntity,10*2)
	//  panic: runtime error: index out of range [0] with length 0
	//var navs [] *model.CategoryEntity
	i:=0
	//0
	initNav(t,i,[]string{"首页","Index"},navs)
	navs[i*2].Href="index.html"
	navs[i*2+1].Href=navs[i*2].Href
	i++

	//1
	initNav(t,i,[]string{"关于我们","About Us"},navs)
	navs[i*2].Href="about-us.html"
	navs[i*2+1].Href="about-us.html"
	i++

	//2
	initNav(t,i,[]string{"简介","Portfolio"},navs)
	navs[i*2].Href="services.html"
	navs[i*2+1].Href="services.html"
	i++

	//3
	initNav(t,i,[]string{"服务","Services"},navs)
	navs[i*2].Href="portfolio.html"
	navs[i*2+1].Href="portfolio.html"
	i++


	//4
	initNav(t,i,[]string{"主页","Page"},navs)
	//navs[i*2].Children=make([]*model.CategoryEntity,4)
	//navs[i*2+1].Children=make([]*model.CategoryEntity,4)
	i++

	//5
	initNav(t,i,[]string{"单博客","Single Blog"},navs)
	navs[i*2].Href="blog-item.html"
	navs[i*2+1].Href="blog-item.html"
	navs[4*2].Children=append(navs[4*2].Children,navs[i*2])
	navs[4*2+1].Children=append(navs[4*2+1].Children,navs[i*2+1])
	i++

	//6
	initNav(t,i,[]string{"价格","Pricing"},navs)
	navs[i*2].Href="pricing.html"
	navs[i*2+1].Href="pricing.html"
	navs[4*2].Children=append(navs[4*2].Children,navs[i*2])
	navs[4*2+1].Children=append(navs[4*2+1].Children,navs[i*2+1])
	i++

	//7
	initNav(t,i,[]string{"博客","Blog"},navs)
	navs[i*2].Href="blog.html"
	navs[i*2+1].Href="blog.html"
	navs[4*2].Children=append(navs[4*2].Children,navs[i*2])
	navs[4*2+1].Children=append(navs[4*2+1].Children,navs[i*2+1])
	i++

	//8
	initNav(t,i,[]string{"联系我们","Contact Us"},navs)
	navs[i*2].Href="contact-us.html"
	navs[i*2+1].Href="contact-us.html"
	i++

	//9
	initNav(t,i,[]string{"404","404"},navs)
	navs[i*2].Href="404.html"
	navs[i*2+1].Href="404.html"
	navs[4*2].Children=append(navs[4*2].Children,navs[i*2])
	navs[4*2+1].Children=append(navs[4*2+1].Children,navs[i*2+1])
	//i++

	//remove
	//navs = navs[:17] //remove 18 19
	//什么 玩意 没有 数据？ 有数据  其它问题造成的需要写日志 否则不知道 啥情况
	temps:=make([]*company.CategoryEntity,12)
	//0-9
	for k:=0;k<10;k++  {
		temps[k]=navs[k]
	}
	//16- 18
	for k:=16;k<18;k++  {
		temps[k-6]=navs[k]
	}
/*	temp := navs[:10] //remove 10- 19
	t.Log("l "+strconv.Itoa(len(temp)))
	temp1 :=navs[16:18]
	t.Log("l "+strconv.Itoa(len(temp1)))
	temp2 :=append(temp,temp1[0])
	temp3 :=append(temp2,temp1[1])
	t.Log("l "+strconv.Itoa(len(temp3)))*/
	//tx:=gorm_util.Db.Begin()
	id:=1
	for k:=0;k<len(temps);k++ {
		//temps[k].Parent=nil //error ex wait many time  100s not set fk childrran auto add
		if k%2!=0{
			//gorm_util.Db.First(&temps[k-1],int64(k-1))
			//temps[k].Parent=temps[k-1]
		}

		//j1,err:=json.Marshal(&temps[k])//{}
		//if err!=nil{
			//t.Log("add nav json fail"+temps[k].Name)
			//return
		//}
		//t.Log(string(j1))
		//gorm bug 各种问题 各种细节 造成的 最好 单表
		temps[k].ParentId=int64(id) //9 channge  13 error
		gorm_util.Db.Create(&temps[k])
		//update 外键 id 即自关联
	/*	var current *model.CategoryEntity
		gorm_util.Db.First(&current,int64(k+1))
		current.Parent=current
		gorm_util.Db.Save(current)*/
		id++
		if temps[k].Children!=nil{
			id=id+len(temps[k].Children)
		}
		/*if temps[k].Children!=nil{
			//fk match error
			//var parent *model.CategoryEntity
			//parent=temps[k] //must set but error point used
			t.Log(""+strconv.Itoa(k))//8
			gorm_util.Db.First(&temps[k],int64(k+1))
			for p:=0;p<len(temps[k].Children);p++ {
				//temps[k].Children[p].Parent=parent //fk error
				temps[k].Children[p].Parent=temps[k] //fk error
				//temps[k].Children[p].ParentId=int64(k+1) //timeout wait time too long 93s
				gorm_util.Db.Create(&temps[k].Children[p])
			}
		}*/
	}
	//tx.Commit()

}



func  initNav(t *testing.T,i int,names []string,navs []*company.CategoryEntity)  {
	for j:=0;j<len(names);j++ {
		nav:=&company.CategoryEntity{}
		nav.Enable=true
		nav.Flag=company.Nav
		nav.CreateDate=time.Now().Unix()
		nav.Name=names[j]
		navs[i*2+j]=nav
		//navs[i*2+j].Parent=navs[i*2+j]
		if j==1 {
			nav.Lanage="zh-en"
		}else{
			nav.Lanage="zh-cn"
		}
	}
}

