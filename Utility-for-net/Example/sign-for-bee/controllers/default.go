package controllers

import (
	 "sign/models"
	"encoding/json"
	"fmt"
	"strconv"

	"github.com/beego/beego/v2/client/orm"
	beego "github.com/beego/beego/v2/server/web"
)

type MainController struct {
	beego.Controller
}

func (c *MainController) Get() {
	c.Data["Website"] = "beego.me"
	c.Data["Email"] = "astaxie@gmail.com"
	c.TplName = "index.tpl"
}

type TestController struct {
	beego.Controller
}

func (this *TestController) Get() {
	this.Ctx.WriteString("test")
}

type MusicController struct {
	beego.Controller
}

//` 注意 不能 写错' "
type MusicInput struct {
	ImgSrc   string           `json:"img_src"`
	Swiper   *Swiper          `json:"swiper"`
	Catalogs []*models.Catalog `json:"catalogs"`
}

type Swiper struct {
	Autoplay bool     `json:"autoplay"`
	Img      []string `json:"img"`
}

func (this *MusicController) Post() {

	//jsoninfo := this.GetString("jsoninfo")
	// id := this.Input().Get("id")
	// intid, err := strconv.Atoi(id)
	//u := user{}
	//if err := this.ParseForm(&u); err != nil {
	//    //handle error
	//}
	//body json empty
	//"unexpected end of JSON input"
	//copyrequestbody = true
	//传过来 json 无论任何 类型都要用 字符串传递 不一定 csdn 说法错了(版本)
	// 数据 有的 有 有的 没 最好用 string 类型 不然数据都传 需要慢慢试下
	//"json: cannot unmarshal string into Go struct field Music.catalogs.musices.count of type int error "
	var ob MusicInput
	var err error
	if err = json.Unmarshal(this.Ctx.Input.RequestBody, &ob); err == nil {
		//objectid := models.AddOne(ob)
		o := orm.NewOrm()
		var i int
		//var cal models.Carousel
		for i = 0; i < len(ob.Swiper.Img); i++ {
			cal := models.Carousel{Src: ob.Swiper.Img[i]}
			//cannot use non-ptr model struct
			id, err := o.Insert(&cal)
			if err == nil {
				fmt.Println("Carousel id:", id)
			} else {
				this.Data["json"] = err.Error() + " error "
				this.ServeJSON()
				return
				//break
			}
		}
		//会 自动关联 外键? 不会 手动
		//"field `beedemo/models.Catalog.Parent` cannot be NULL error "
		/*successNums, err := o.InsertMulti(100, &ob.Catalogs)
		if err == nil {
			fmt.Println(successNums)
		} else {
			this.Data["json"] = err.Error() + " error "
			this.ServeJSON()
			return
		}*/
		var j int
		for i = 0; i < len(ob.Catalogs); i++ {
			ca := ob.Catalogs[i]
			// invalid memory address or nil pointer dereference
			id, err := o.Insert(ca)
			if err != nil {
				this.Data["json"] = err.Error() + " error "
				this.ServeJSON()
				return
			}
			ca.Id = id
			for j = 0; j < len(ca.Musices); j++ {
				music := ca.Musices[j]
				//外键 id 没关联
				//json: unsupported value: encountered a cycle via *models.Catalog
				//music.Catalog = ca
				user := models.Catalog{Id: ca.Id}
				//err := o.Read(&user)
				o.Read(&user)
				fmt.Println("Music Catalog id", ca.Id)

				music.Catalog = &user
				fmt.Println(music)
				id, err := o.Insert(music)
				if err == nil {
					fmt.Println("Music id:", id)
				} else {
					this.Data["json"] = err.Error() + " error "
					this.ServeJSON()
					return
					//break
				}
			}
		}

		this.Data["json"] = ob //"{\"ObjectId\":\"" + objectid + "\"}"
	} else {
		this.Data["json"] = err.Error() + " error "
	}
	this.ServeJSON()
	/*
		if jsoninfo == "" {
			this.Ctx.WriteString("jsoninfo is empty")
			return
		}
		this.Ctx.WriteString(jsoninfo)
	*/
}

type GetMusicController struct {
	beego.Controller
}

func (this *GetMusicController) Get() {
	// multiple-value this.Controller.Input()
	//page := this.Input().Get("page")
	//size :=this.Input().Get("size")
	page := this.GetString("page")
	size := this.GetString("size")
	intpage, err := strconv.Atoi(page)
	if err != nil {

	}
	intsize, err := strconv.Atoi(size)
	if err != nil {

	}
	if intpage < 0 || intpage > 999 {
		intpage = 1
	}
	if intsize < 0 || intsize > 100 {
		intsize = 10
	}
	var data map[string]int
	data = make(map[string]int)
	//' ' 注意 ""
	data["page"] = intpage
	data["size"] = intsize
	this.Data["json"] = data
	this.ServeJSON()
}

func (this *GetMusicController) Post() {
	page := this.Ctx.Input.Param(":page")
	size := this.Ctx.Input.Param(":size")
	intpage, err := strconv.Atoi(page)
	if err != nil {
		intpage = 1
	}
	intsize, err := strconv.Atoi(size)
	if err != nil {
		intsize = 10
	}
	if intpage < 0 || intpage > 999 {
		intpage = 1
	}
	if intsize < 0 || intsize > 100 {
		intsize = 10
	}
	//data := make(map[string]int)
	//' ' 注意 ""
	//data["page"] = intpage
	//data["size"] = intsize
	o := orm.NewOrm()
	var musices []*models.Music // = make([]*models.Music, 10, 100)
	//逻辑 分页 不好控制 只能原生 sql
	music := new(models.Music)
	o.QueryTable(music).RelatedSel().All(&musices)
	//整理 数据 bug 什么玩意 数据显示乱七八糟
	//invalid memory address or nil pointer
	catalogs1 := make([]*models.Catalog, 0, len(musices))
	//error 前面10条数据为null

	//catalogs1 := make([]*models.Catalog, 10, len(musices))
	var k, l, jj int = 0, 0, 0
	//var ca1 models.Catalog
	exis := false
	println("len :", len(musices))
	for k < len(musices) {
		ca1 := musices[k].Catalog
		exis = false
		jj = 0
		for jj < l {
			println("check starting")
			//invalid memory address or nil pointer
			if ca1 == nil {
				break
			}
			println("check value:", &catalogs1[jj])
			//error
			//println("check value:", &catalogs1[jj].Id)
			if catalogs1[jj] == ca1 {
				//error
				//if catalogs1[jj].Id == ca1.Id {
				println("check match")
				ca1 = catalogs1[jj]
				exis = true
				println("check finished")
				break
			}
			jj++
		}
		println("exis :", exis)
		if !exis {

			catalogs1 = append(catalogs1, ca1)
			println("catalogs1 add :")
			ca1.Musices = make([]*models.Music, 0, len(musices))
			println("Musices make :")
			l++
		}

		musices[k].Catalog = nil
		ca1.Musices = append(ca1.Musices, musices[k])
		println("Musices add :")
		k++
	}
	this.Data["json"] = &catalogs1
	this.ServeJSON()
	return

	//方案 2 只能使用原生 sql (外键id 怎么查?)
	// // 返回 QuerySeter
	//会关联外键 需要手动组合
	var catalogs []*models.Catalog
	//分类 关联 不到 音乐
	//音乐 可以关联 分类
	//怎么 根据 外键 id 查询 原生 sql?
	//o.QueryTable(&models.Catalog) error
	o.QueryTable("t_catalog").RelatedSel().All(&catalogs)
	//cata := new(models.Catalog)
	//error
	//o.QueryM2M(&cata, "Musices").All(&catalogs)
	/*
		i := 0

		for i < len(catalogs) {
			//fmt.Println(catalogs[i])

			// 类型不知道啥 类型 好多重复代码 没提示
			var musices []*models.Music // = make([]*models.Music, 10, 100)

			music := new(models.Music)
			//music.Catalog = catalogs[i]
			//beedemo:runtime error: index out of range [42] with length 4
			//catalog_id CatalogId Catalog.Id Catalog error  Catalog__Id pass
			//[]
			//o.QueryTable(music).Filter("Catalog__Id", catalogs[i].Id).Limit((intpage-1)*intsize, intpage*intsize).All(&musices)
			o.QueryTable(music).Limit((intpage-1)*intsize, intpage*intsize).All(&musices)
			fmt.Println("catalogs[i].Id ", catalogs[i].Id, &musices)
			catalogs[i].Musices = musices
			i++
		}
	*/
	//var musices []*models.Music
	//music :=new(models.Music)
	//o.QueryTable(music).Limit((intpage-1)*intsize, intpage*intsize).All(&musices)

	this.Data["json"] = &catalogs
	this.ServeJSON()
}
