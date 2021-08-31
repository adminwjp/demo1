package splider

import (
	"encoding/json"
	"fmt"
	"gopkg.in/mgo.v2"
	"io/ioutil"
	"log"
	"os"
	"strconv"
	"strings"
	"time"
	"utility/example/news"
	"utility/util"
)
// go test  -count=1 -v
var MongInstance util.MongUtil

func init()  {
	//MongInstance.Conn()
}
var path string


func TestNews(catalogs *news.Catalogs)  {
	session:=MongInstance.Conn()
	//defer  session.Close()
	//ex begin

	//defer  handError()
	/*var info *mgo.CollectionInfo
	err=session.DB("news").C("catalog").Create(info)
	if err!=nil{
		t.Log("create mong col fail,error :"+err.Error())
		//return
	}else{
		t.Log("111")
	}*/
	//ex end

	//session=session.New()
	for  i:=0;i<len(catalogs.Catalogs);i++ {
		println(catalogs.Catalogs[i])
		session.DB("news").C("catalog").Insert(&catalogs.Catalogs[i])

	}
	//MongInstance.Session //error
	query :=session.DB("news").C("catalog").Find(map[string]interface{}{})
	var cas []*news.Catalog
	query.All(&cas)
	if cas!=nil{
		println(&cas)
		println("find suc "+strconv.Itoa(len(cas)))
	}else{
		println("find fail")
	}

	getData(catalogs,session)
	//MongInstance.Insert("news","catalog",catalogs.Catalogs)
	//session.DB("news").C("catalog").Insert(&catalogs.Catalogs)
}

func handError() {
	if err := recover(); err != nil {
		fmt.Println("recover msg: ", err)
	} else {
		fmt.Println("recover ok")
	}
}

func  getData(catalogs *news.Catalogs,session *mgo.Session)  {
	url:="https://unidemo.dcloud.net.cn/api/news?columnId=0&minId=0&pageSize=10&column=id%2Cpost_id%2Ctitle%2Cauthor_name%2Ccover%2Cpublished_at%2Ccomments_count&time="
	path,err:=os.Getwd()
	if err!=nil{
		log.Println("get path fail,error :"+err.Error())
		return
	}
	path=strings.Replace(path,"tests","",1)
	for i, catalog := range catalogs.Catalogs {
		if i>0{

		}
		url="https://unidemo.dcloud.net.cn/api/news?columnId="+strconv.Itoa(catalog.Newsid)+"&minId=0&pageSize=10&column=id%2Cpost_id%2Ctitle%2Cauthor_name%2Ccover%2Cpublished_at%2Ccomments_count&time="
		result:=util.GetString(url,"")
		var items []*news.NewsItem
		err:=json.Unmarshal([]byte(result),&items)
		if err==nil{
			for i2 := range items {
				se:=util.SeurityInstance
				img:=se.AesEncrypt(items[i2].Cover)
				println(img)
				buffer:=util.Http(util.HttpEntity{Url:items[i2].Cover,Method: util.HttpGet})
				err:=ioutil.WriteFile(path+"\\static\\imgs\\"+img+".jpg",buffer,0644)
				if err!=nil{
					println("img down fail,error:"+err.Error())
				}
				t,err:=time.Parse("2006-01-02 15:04:05",
					items[i2].PublishedAt)
				if err!=nil{
					continue
				}
				items[i2].Published=t.Unix()
				items[i2].Img=img
				items[i2].Newsid=catalog.Newsid
				session.DB("news").C("items").Insert(items[i2])
			}
		}
	}
}
