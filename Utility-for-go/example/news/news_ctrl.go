package news

import (
	"encoding/json"
	"io"
	"io/ioutil"
	"log"
	"net/http"
	"strings"
	"utility/util"
)

func Insert(writer http.ResponseWriter, request *http.Request, name string,
	col string, model interface{}, flag string, many bool) {
	writer.Header().Set("Access-Control-Allow-Origin", "*") //允许访问所有域
	if strings.ToLower(request.Method) != "post" {
		io.WriteString(writer, "http method not support,only support post")
		return
	}

	reader, err := request.GetBody()
	if err != nil {
		log.Println(name + " read body stream fail,error :" + err.Error())
		io.WriteString(writer, "0")
		return
	}
	buffer, err := ioutil.ReadAll(reader)
	if err != nil {
		log.Println(name + " read body buffer fail,error :" + err.Error())
		io.WriteString(writer, "0")
		return
	}
	err = json.Unmarshal(buffer, &model)
	if err != nil {
		log.Println(name + " json parse body fail,error :" + err.Error())
		io.WriteString(writer, "0")
		return
	}
	res := newsService.Insert(col, flag, model, many)
	if res == 1 {
		io.WriteString(writer, "1")
		return
	}
	io.WriteString(writer, "0")
}

func InsertCatalog(writer http.ResponseWriter, request *http.Request) {
	var newsItem *NewsItem
	Insert(writer, request, "InsertCatalog", "catalog", &newsItem, "catalog", false)
}

func InsertManyCatalog(writer http.ResponseWriter, request *http.Request) {
	var catalogs *NewsCatalogs
	Insert(writer, request, "InsertManyCatalog", "catalog", &catalogs, "catalog", true)
}

func InsertNewsItem(writer http.ResponseWriter, request *http.Request) {
	var newsItem *NewsItem
	Insert(writer, request, "InsertCatalog", "new_item", &newsItem, "items", false)
}

func InsertManyNewsItem(writer http.ResponseWriter, request *http.Request) {
	var newsItems *NewsItems
	Insert(writer, request, "InsertCatalog", "new_item", &newsItems, "items", true)
}

func FindCatalog(writer http.ResponseWriter, request *http.Request) {
	writer.Header().Set("Access-Control-Allow-Origin", "*") //允许访问所有域
	io.WriteString(writer, newsService.FindCatalog())
}

func FindNewsItem(writer http.ResponseWriter, request *http.Request) {
	writer.Header().Set("Access-Control-Allow-Origin", "*") //允许访问所有域
	page, size := util.GetPageAndSize(request)
	if page < 1 && page > 1000 {
		page = 1
	}
	if size < 1 && size > 100 {
		size = 10
	}
	ids := request.URL.Query()["newsid"]
	var newsid = "0"
	if ids != nil && len(ids) > 0 {
		newsid = ids[0]
	}
	io.WriteString(writer, newsService.FindNewsItem(page, size, newsid))
}
