package news

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
	"net/http"
	"strconv"
	"utility/model"
	"utility/util"

	"github.com/jinzhu/gorm"
	//_ "github.com/jinzhu/gorm/dialects/mysql"
	_ "github.com/jinzhu/gorm/dialects/sqlite"
)

//数据 不统一 麻烦

var Db *gorm.DB

func initDb() *gorm.DB {

	model.InitCfg()
	db, err := gorm.Open(model.Config.Dialet, model.Config.Addrs)
	if err != nil {
		fmt.Print("connection database fail")
		panic(err)
	}
	fmt.Println("connection database suc")
	// 全局禁用表名复数
	//db.SingularTable(true) // 如果设置为true,Company 的默认表名为 company,使用TableName设置的表名不受影响

	//defer db.Close()

	// 自动迁移
	db.AutoMigrate(&NewsCatalog{}, &NewsItem{})
	MongInstance = util.MongUtil{}
	Db = db
	return db //必须返回 不然指针 为 null
}

// 网易数据
type WyModel struct {
}

func NewDataByUniApp() {
	Db = initDb()

	session := MongInstance.Conn()
	if session == nil {
		return
	}
	defer session.Close()
	//defer  db.Close()
	var catalogs []*NewsCatalog
	session.DB("news").C("catalog").Find(map[string]interface{}{}).All(&catalogs)
	println(len(catalogs))
	var items []*NewsItem
	query := session.DB("news").C("items").Find(map[string]interface{}{})
	query.All(&items)
	println(len(items))
	if Db == nil {
		println("db is nil")
	}
	return
	//tx := Db.Begin() //ex
	for _, v := range catalogs {
		//b, _ := json.Marshal(v)
		//println(b)
		//Db.Create(&v)
		Db.Model(NewsCatalog{}).Create(NewsCatalog{
			Name: v.Name,
			ID:   v.ID,
			//Href:v.Href,
			Newsid:  v.Newsid,
			Soucrce: "uni-app",
		})
	}
	for _, v := range items {
		//b, _ := json.Marshal(v)
		//println(b)
		//Db.Create(&v)
		Db.Model(NewsItem{}).Create(NewsItem{
			ID:            v.ID,
			PostID:        v.PostID,
			Title:         v.Title,
			AuthorName:    v.AuthorName,
			Cover:         v.Cover,
			Img:           v.Img,
			Newsid:        v.Newsid,
			PublishedAt:   v.PublishedAt,
			Published:     v.Published,
			CommentsCount: v.CommentsCount,
			Content:       v.Content,
			Soucrce:       "uni-app",
			Flag:          "uni-app",
		})
	}
	//tx.Commit()
	Db.Close()
}

func NewDataByWy() {
	dir := "E:/work/shop/Shop-for-django/spliders/wy"
	//utf-8-sig
	b, _ := ioutil.ReadFile(dir + "/wy.json")
	println(len(b))
	//var catalogs []NewsCatalog
	//charset ex
	var catalogs []map[string]string
	err := json.Unmarshal(b, &catalogs)
	if err != nil {
		println(err.Error())
	}
	println(len(catalogs))
	b, _ = ioutil.ReadFile(dir + "/data.json")
	println(len(b))
	//var items []]NewsItem
	//var items map[string]NewsItem
	var items map[string][]map[string]interface{}
	err = json.Unmarshal(b, &items)
	if err != nil {
		println(err.Error())
	}
	println(len(items))
	Db = initDb()
	if Db == nil {
		println("db is nil")
	}
	return
	tx := Db.Begin()
	for _, v := range catalogs {
		cata := NewsCatalog{
			Name:    v["name"],
			Href:    v["href"],
			Soucrce: "wy",
		}
		cata.ID = util.SeurityInstance.Sha1(cata.Name)
		continue
		tx.Create(cata)
	}
	//var time="2021-07-25"
	for k, v := range items {
		//val := v.(map[string]interface{})
		for _, val := range v {
			//1 title author_name published href img
			//2 title  published href
			//3 title author_name published href
			//like_count comments_count pic
			//video | img [] | incentive | replay_count

			news := NewsItem{
				Title: val["title"].(string),
				//Published:    val["published"].(string),
				//CommentsCount:int(val["comments_count"].(string)),
				Href: val["href"].(string),

				PublishedAt: val["published_at"].(string),
				Flag:        "1",
				CatalogId:   k,
			}
			news.Title = util.ReplaceString(news.Title)

			img, ok := val["img"]
			if ok {
				imgs, ok := img.(string)
				if ok {
					news.Cover = imgs
					news.Img = imgs
				} else {
					img1s, ok := img.([]string)
					if ok {
						bu, err := json.Marshal(img1s)
						if err != nil {
							news.Cover = string(bu)

						}
					}
				}
			}
			author_name, ok := val["author_name"]
			if ok {
				news.AuthorName = author_name.(string)
				news.AuthorName = util.ReplaceString(news.AuthorName)
			}
			soucrce, ok := val["soucrce"]
			if ok {
				news.Soucrce = soucrce.(string)
				news.Soucrce = util.ReplaceString(news.Soucrce)
			}
			like_count, ok := val["like_count"]
			if ok {
				va, _ := strconv.Atoi(like_count.(string))
				news.LikeCount = va
			}
			pic, ok := val["pic"]
			if ok {
				news.Pic = pic.(string)
			}
			info, ok := val["info"]
			if ok {
				news.Info = info.(string)
				news.Info = util.ReplaceString(news.Info)
			}
			video, ok := val["video"]
			if ok {
				news.Video = video.(string)
			}
			incentiveok, ok := val["incentive"]
			if ok {
				news.Incentive = incentiveok.(string)
			}
			replay_count, ok := val["replay_count"]
			if ok {
				va, _ := strconv.Atoi(replay_count.(string))
				news.ReplayCount = va
			}
			continue
			tx.Create(news)
		}
	}
	tx.Commit()
	Db.Close()
}

var newsService = NewsMongService{}
var MongInstance util.MongUtil

type NewsDbService struct {
}

//mong tx not support
type NewsMongService struct {
}

func (NewsDbService) Insert(col string, flag string, model interface{}, many bool) int {
	session := MongInstance.Conn()
	if session == nil {
		return -2
	}
	defer session.Close()
	if many {
		if "catalog" == flag {
			var catalogs = model.(*NewsCatalogs)
			if catalogs != nil && len(catalogs.Catalogs) < 1 {
				return -1
			}
			//MongInstance.Session.DB("news").C("").Insert()
			res := 0
			for i := range catalogs.Catalogs {
				if session.DB("news").C(col).Insert(catalogs.Catalogs[i]) == nil {
					res += 1
				} else {
					//session.DB("news").C(col).Remove()
					//return -3
				}
			}
			return res
		} else {
			var items = model.(*NewsItems)
			if items != nil && len(items.Items) < 1 {
				return -1
			}
			res := 0
			for i := range items.Items {
				if session.DB("news").C(col).Insert(items.Items[i]) == nil {
					res += 1
				} else {
					//session.DB("news").C(col).Remove()
					//return -3
				}
			}
			return res
		}
	}
	if session.DB("news").C(col).Insert(model) == nil {
		return 1
	}
	return 0

}

func (NewsMongService) Insert(col string, flag string, model interface{}, many bool) int {
	session := MongInstance.Conn()
	if session == nil {
		return -2
	}
	defer session.Close()
	if many {
		if "catalog" == flag {
			var catalogs = model.(*NewsCatalogs)
			if catalogs != nil && len(catalogs.Catalogs) < 1 {
				return -1
			}
			//MongInstance.Session.DB("news").C("").Insert()
			res := 0
			for i := range catalogs.Catalogs {
				if session.DB("news").C(col).Insert(catalogs.Catalogs[i]) == nil {
					res += 1
				} else {
					//session.DB("news").C(col).Remove()
					//return -3
				}
			}
			return res
		} else {
			var items = model.(*NewsItems)
			if items != nil && len(items.Items) < 1 {
				return -1
			}
			res := 0
			for i := range items.Items {
				if session.DB("news").C(col).Insert(items.Items[i]) == nil {
					res += 1
				} else {
					//session.DB("news").C(col).Remove()
					//return -3
				}
			}
			return res
		}
	}
	if session.DB("news").C(col).Insert(model) == nil {
		return 1
	}
	return 0

}

func (NewsMongService) InsertCatalog(writer http.ResponseWriter, request *http.Request) {
	var newsItem *NewsItem
	Insert(writer, request, "InsertCatalog", "catalog", &newsItem, "catalog", false)
}

func (NewsMongService) FindCatalog() string {
	session := MongInstance.Conn()
	if session == nil {
		return "0"
	}
	defer session.Close()
	var catalogs []*NewsCatalog
	session.DB("news").C("catalog").Find(map[string]interface{}{}).All(&catalogs)
	if catalogs == nil {
		return "{\"catalogs\":[]}"
	}
	buffer, err := json.Marshal(NewsCatalogs{Catalogs: catalogs})
	if err != nil {
		log.Println("FindCatalog read data object to json fail,error:" + err.Error())
		return "{\"catalogs\":[]}"
	}
	return string(buffer)
}

func (NewsMongService) FindNewsItem(page int, size int, newsid string) string {
	session := MongInstance.Conn()
	if session == nil {
		return "{\"items\":[]}"
	}
	id, err := strconv.Atoi(newsid)
	if err != nil {
		return "{\"items\":[]}"
	}
	defer session.Close()
	var items []*NewsItem
	query := session.DB("news").C("items").Find(map[string]interface{}{
		"newsid": id})
	query.Limit(size).Skip((page - 1) * size).All(&items)
	if items == nil {
		return "{\"items\":[]}"
	}
	buffer, err := json.Marshal(NewsItems{Items: items})
	if err != nil {
		log.Println("FindNewsItem read data object to json fail,error:" + err.Error())
		return "{\"items\":[]}"
	}
	return string(buffer)
}
