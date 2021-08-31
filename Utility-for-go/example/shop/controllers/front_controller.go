package controllers

import (
	"html/template"
	"io/ioutil"
	"log"
	"os"
	"strings"

	"github.com/gin-gonic/gin"
	jsoniter "github.com/json-iterator/go"
)

var tpl *template.Template
var tplTest *template.Template

func InitView() {
	return
	tpl = template.Must(template.ParseFiles(
		//"static/front/user.html",
		"static/front/buyer/settings.html",
		"static/front/buyer/buyer_password.html",
		"static/front/buyer/profile.index.html",
		"static/front/buyer/buyer_address.html",
		"static/front/top.html",
		"static/front/user.menu.html",
		"static/front/user.submenu.html",
		"static/front/user.curlocal.html",
		"static/front/user.header.html",
		"static/front/user.footer.html",
	))
	//tpl=template.Must(template.ParseGlob("static/front/*.html"))
	tplTest = template.Must(template.ParseGlob(
		"static/*.html"))

	InitLanguage("zh-cn")

}

func InitLanguage(lange string) {
	dir, err := os.Getwd()
	if err != nil {
		log.Fatalln("get dir fail,error:" + err.Error())
	}
	currentDir := dir + "/config/" + lange
	files, err := ioutil.ReadDir(currentDir)
	if err != nil {
		log.Fatalln("get files by dir [" + dir + "] fail,error:" + err.Error())
	}
	if len(files) > 0 {
		for f := range files {
			filepath := currentDir + files[f].Name()
			buffer, err := ioutil.ReadFile(filepath)
			if err != nil {
				log.Println("get files by dir [" + dir + "] - and path [" + filepath + "] fail,error:" + err.Error())
				continue
			}
			var data map[string]interface{}
			err = jsoniter.Unmarshal(buffer, &data)
			if err != nil {
				log.Println("get files by dir [" + dir + "] - and path [" + filepath + "] success but json parse map  fail,error:" + err.Error())
				continue
			}
			names := strings.Split(files[f].Name(), ".")
			configs[names[0]] = data
		}
	} else {
		log.Println("get files by dir [" + dir + "] success but not has data")
	}
}

var configs map[string]interface{} = make(map[string]interface{}, 10)

func Index(c *gin.Context) {
	tpl.ExecuteTemplate(c.Writer, "front/index.html",
		map[string]interface{}{
			"tel":         "18176895410",
			"email":       "andcpp@qq.com",
			"title":       "商家入住",
			"description": "商家入住协议",
		})
}

func Login(c *gin.Context) {
	tpl.ExecuteTemplate(c.Writer, "login",
		map[string]interface{}{})
}

//https://www.cnblogs.com/f-ck-need-u/p/10053124.html
func TestTpl(c *gin.Context) {
	//return
	data := map[string]interface{}{
		"title": "test",
		"items": []string{"test1", "test2"},
		"test2": TestModel{Id: 1, Name: "aa"},
		"test":  map[string]interface{}{"a": "b", "c": "d"},
		"test1": []map[string]interface{}{map[string]interface{}{"a": "b", "c": "d"}},
	}
	/*template.ParseFiles(
	//"static/front/*.html"))
	"static/*.html")*/
	log.Println(tplTest.Name())
	err := tplTest.ExecuteTemplate(c.Writer, "test.html", data)
	if err != nil {
		log.Println("test tpl fail,error:" + err.Error())
	}
	//ex
	//c.Header("Content-Type", "application/html")
	//c.HTML(http.StatusOK, "static/test.html", data)
}

type TestModel struct {
	Id   int
	Name string
}

//平台审核
func AppalyVerify(c *gin.Context) {
	seller(c, "class=\"succeed\"", "class=\"succeed\"", "class=\"succeed\"",
		"class=\"current\"", "")
}

//填写店铺信息
func AppalyFill(c *gin.Context) {
	seller(c, "class=\"succeed\"", "class=\"succeed\"",
		"class=\"current\"", "", "")
}

//店家入住
func AppalyAgreement(c *gin.Context) {
	seller(c, "class=\"succeed\"",
		"class=\"current\"", "", "", "")
}

//入驻指南
func Appaly(c *gin.Context) {
	seller(c, "class=\"current\"", "", "", "", "")
}

func seller(c *gin.Context, apply string, agreement string, fill string, verify string,
	openShop string) map[string]interface{} {
	data := map[string]interface{}{
		"tel":         "18176895410",
		"email":       "andcpp@qq.com",
		"title":       "商家入住",
		"description": "商家入住协议",

		"apply":     "",
		"agreement": agreement,
		"fill":      fill,
		"verify":    verify,
		"openShop":  openShop,
	}
	return data
}

//个人资料
func Profile(c *gin.Context) {

}
func ProfileBasic(c *gin.Context) {
	data := getTop()
	data["profile.basic"] = true
}
func ProfileUpdatePwd(c *gin.Context) {
	data := getTop()
	data["profile.password"] = true
}
func ProfileUpdatePhone(c *gin.Context) {
	data := getTop()
	data["profile.phone"] = true
}
func ProfileUpdateEmail(c *gin.Context) {
	data := getTop()
	data["profile.email"] = true
}

func Buyer(c *gin.Context) {
	data := getTop()
	if len(data) > 0 {

	}
}
func getTop() map[string]interface{} {
	var data = make(map[string]interface{}, 20)
	data["language"] = "en"
	data["charset"] = "utf-8"
	data["csrfParam"] = ""
	data["csrfToken"] = ""

	data["page.title"] = ""
	data["page.keywords"] = ""
	data["page.description"] = ""

	data["homeUrl"] = "en"
	data["siteUrl"] = "utf-8"
	data["priceFormat"] = ""
	data["enablePretty"] = ""

	data["redirect"] = "index"
	data["lang.login"] = "登录"
	data["lang.register"] = "注册"
	data["lang.gcategory"] = "商品分类"
	data["visitor.userid"] = ""
	data["g_history"] = nil
	data["navs.header"] = nil
	data["isbuyer"] = true
	data["isseller"] = false
	data["lang.im_buyer"] = "买家"
	data["lang.im_seller"] = "卖家"

	return data
}

var language = "en"
var charset = "utf-8"
var csrfParam = ""
var csrfToken = ""

type FrontIndex struct {
	Name     string
	Effect   string
	Autoplay string
}
