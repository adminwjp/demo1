package example

import (
	"container/list"
	"context"
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
	"net/http"
	"os"
	"strconv"
	"strings"
	"sync"
	"time"
	"utility/model"
	"utility/util"

	"github.com/go-redis/redis"
	"github.com/jinzhu/gorm"
	"github.com/olivere/elastic/v7"

	//_ "github.com/jinzhu/gorm/dialects/mysql"
	"net/url"

	_ "github.com/jinzhu/gorm/dialects/sqlite"
)

func EsToCsv() {

}

// gorm 这个 包 有  好 几个(多个作者) 坑嗲  每个 作者 写 的 不同 ?
//其他 第三方 包 也 一样 的
// 模型定义
// https://blog.csdn.net/weixin_38386235/article/details/113386620
// index index:idx_name_code  unique_index
// 列名是字段名的蛇形小写 CreateTime create_time
// 默认表名是 companys 表名是结构体名称的复数形式
type Company struct {
	// gorm.Model //gorm.Model 结构体
	ID          int    `gorm:"primary_key;AUTO_INCREMENT"`                      //id int64 not support
	CompanyName string `gorm:"type:varchar(100);column:company_name;not null;"` // 公司名称
	// type:varchar(30) =   size:30
	Corporation       string `gorm:"size:30;not null;"`            // 公司法人 corporation
	RegisteredCapital string `gorm:"type:varchar(100);not null;"`  //注册资本 registered_capital
	RegistrationTime  string `gorm:"type:varchar(20);not null;"`   //注册时间 registration_time
	ContactNumber     string `gorm:"type:varchar(100);not null;"`  //联系电话 contact_number
	Address           string `gorm:"type:varchar(200);not null;"`  //地址 address
	BusinessAddress   string `gorm:"type:varchar(200);not null;"`  // 企业地址 business_address
	Mailbox           string `gorm:"type:varchar(1000);not null;"` // 邮箱 mailbox
	BusinessScope     string `gorm:"type:varchar(2000);not null;"` // 经营范围 business_scope
	GetTime           string `gorm:"type:varchar(20);not null;"`   // 获取时间 get_time
	Catalog           string `gorm:"type:varchar(100);not null;"`  // 分类 catalog
	ChildrenCatalog   string `gorm:"type:varchar(100);not null;"`  //子分类 children_catalog
	Status            string `gorm:"type:varchar(10);not null;"`   // 状态 status
	Times             string `gorm:"type:varchar(100);not null;"`  //注册时间 范围 times
	CreateTime        string `gorm:"type:varchar(20);not null;"`   //创建时间 create_time
	UpdateTime        string `gorm:"type:varchar(20);"`            //更新时间 update_time
}

var key = []byte("abcdefgh")

type Person struct {
	Name    string `json:"name"`
	Age     int    `json:"age"`
	Married bool   `json:"married"`
}

func testEs() {
	client, err := elastic.NewClient(elastic.SetURL("http://127.0.0.1:9200"))
	if err != nil {
		// Handle error
		panic(err)
	}

	fmt.Println("connect to es success")
	p1 := Person{Name: "lmh", Age: 18, Married: false}
	put1, err := client.Index().
		Index("user").
		BodyJson(p1).
		Do(context.Background())
	if err != nil {
		// Handle error
		panic(err)
	}
	fmt.Printf("Indexed user %s to index %s, type %s\n", put1.Id, put1.Index, put1.Type)
}

func Init() {
	//return
	cfg := model.Config
	var isDb = false
	var db *gorm.DB
	//return
	//db, err := gorm.Open("mysql", "root:root@(127.0.0.1:3306)/db1?charset=utf8mb4&parseTime=True&loc=Local")
	//db, err := gorm.Open("sqlite3", "E:/work/db/sqlite/company2.db")
	if isDb {
		db, err := gorm.Open(cfg.Dialet, cfg.Addrs)
		if err != nil {
			fmt.Print("connection database fail")
			panic(err)
		}
		// 全局禁用表名复数
		//db.SingularTable(true) // 如果设置为true,Company 的默认表名为 company,使用TableName设置的表名不受影响

		defer db.Close()

		// 自动迁移
		//db.AutoMigrate(&Company{})
	}
	//test es
	//es 语句 乱七八糟 的 文档 参考 别人 的 原生 文档 打开速度 好慢
	//https://www.elastic.co/guide/en/elasticsearch/reference/7.x/query-dsl.html
	//分组 查询 用 sql 查询  java 生态多点  版本不对 坑 要么 升级 迁移 数据 要么 http 手动 操作 go 生态少点 github go es 支持 sql
	//https://www.elastic.co/guide/en/elasticsearch/reference/7.x/search-aggregations.html
	/**
	//testEs()

	elasticUtil :=util.NewElasticUtil()
	conn :=elasticUtil.Connection()
	if conn{
		println("es connec suc")
	}else {
		println("es connec fail")
	}
	var data map[string]interface{} =map[string]interface{}{"a":1}//make(map[string]interface{},10)
	data["b"]=2
	//panic: runtime error: invalid memory address or nil pointer dereference
	//每次 操作 需要 重新 连接  es 全局 变量 就 不需要 重连了
	elasticUtil.Insert("test",data)
	p:= Person{Name: "test"}
	p.Age=1
	j,err :=json.Marshal(data)
	if err!=nil{
		println(err)
	}
	elasticUtil.Insert("test",p)
	elasticUtil.InsertJson("test",string(j))
	elasticUtil.SaveOrUpdate("test","test",data)
	**/

	/**
	{
	  "query": {
	    "match_all": {}
	  },
	  "aggregations": {
	    "group_by_company": {
	      "terms": {
	        "field": "company",
	        "size": 5
	      }
	    }
	  },
	  "_source": {
	    "includes": [
	      "company"
	    ],
	    "excludes": [
	      ""
	    ]
	  }
	}



	*/

	elasticUtil := util.NewElasticUtil()
	//res:=elasticUtil.MultiSearch(request)
	var request *elastic.SearchRequest = elastic.NewSearchRequest()
	request.Index("crawl")
	request.FetchSourceIncludeExclude([]string{"company"}, []string{})
	request.Sort("publish_date", true)
	agg := elastic.NewBucketSortAggregation() //group by
	//聚合 不 支持 分页
	// elk 写 sql 生成 dsl 格式 版本都要统一 不然 启动不了
	agg.Meta(map[string]interface{}{
		"test": map[string]interface{}{
			"terms": map[string]interface{}{
				//"field":"company",
				"script": "doc['company.keyword'].value",
				//"script": "doc['company.keyword'].value + '#' + doc['age'].value + '#' +doc['city'].value",
			},
		},
	})
	//request.Aggregation("group_by_company", agg) //error
	//职位 上 公司  好多 重复的 217407  1400w 70

	if false {
		var redisUtil = util.NewRedisUtl()
		//client := redisUtil.GetClient()
		fileName := "config/test.txt"
		writeTxt(fileName, db, redisUtil)
		return
	}

	esClient := elasticUtil.GetClient()
	//es:=EsHanler{client: client,redisUtil: redisUtil}
	es := EsHanlerCsv{}
	//26w /m 1500/25 =60 分钟 es read redis write 7m 105w
	//分片 查询 后面 越来 越慢 10m 136w redis 慢 ？es 慢？ 12m 172w
	// 9000 2秒处理完
	//1m 35w 2m 58w 基于 map 去重 快点 4m 109w

	//1 m redis 35w write
	esScroll(esClient, es, 14098379, 9000)
	if true {
		println("拆开 信息 数量 :=========>>>")
		println(len(companys))
		for _, v := range companys {
			co := v.(map[string]interface{})
			link := co["link"].(util.Collection)
			co["link"] = link.ToArray()
		}
		println(len(citys))
		println(len(areas))
		println(len(jobs))
		b, _ := json.Marshal(companys)
		util.WriteFile("config/companys.json", b)

		b, _ = json.Marshal(citys)
		util.WriteFile("config/citys.json", b)

		b, _ = json.Marshal(areas)
		util.WriteFile("config/areas.json", b)

		b, _ = json.Marshal(jobs)
		util.WriteFile("config/jobs.json", b)
		return
	}
	//taskEnd=true
	//handlerCompanyNameList(redisUtil,1,5)

	//应该 最多 只能 跑 1w ? 只能用分片

	//elasticUtil.MultiSearchByHandler(es,100,request)
	//client.Close()
	//return

	//esSql()

	//var redisUtil = util.NewRedisUtl()
	//346423
	// 555936 - 346423 =209513 -217407  =7894 重复数量
	//insertCache(db, redisUtil)

	//test 不稳定 爬太多 容易封 最好用 api 获取数据
	//it 桔子 查询公司信息 不稳定 api爬
	//TianYanCha("北京博途物流设备有限公司")

}

func writeTxt(file string, db *gorm.DB, redisUtil *util.RedisUtl) {
	sql := " SELECT company_name FROM t_company where company_name != '' group by  company_name"
	//var names []string //乱码 不知道 啥玩意
	var names []CompanyName
	db.Raw(sql).Scan(&names)
	client := redisUtil.GetClient()
	strs := redisUtil.Keys(client, "*")
	fileName := file
	var f *os.File
	var err error
	suc := false
	//suc:=util.HandlerAppendFile(f,fileName,[]byte("")) //error
	if util.CheckFileIsExists(fileName) {
		f, err = os.OpenFile(fileName, os.O_APPEND|os.O_CREATE|os.O_WRONLY, 0666) //打开文件

	} else {
		f, err = os.Create(fileName) //创建文件
	}
	if err == nil {
		suc = true
	} else {
		log.Fatal("write faile fial")
	}
	if suc {
		for i := range strs {
			println(strs[i])
			//r, err:=f.WriteString(strs[i]+"\r\n")
			r, err := f.Write([]byte(strs[i] + "\r\n"))
			if err != nil {
				log.Fatal("write   fial,byte ", r)
			}
		}
	}
	//f.Sync()
	f.Close()
}

func esToRedis() {

}

type CompanyNameList struct {
	CpmpanyNames []string
	Used         bool
	count        int
	//CpmpanyNames list.List
}

var companyNameLists []CompanyNameList
var taskEnd = false
var set map[string]bool

func init() {
	companyNameLists = make([]CompanyNameList, 10)
	for i := range companyNameLists {
		companyNameLists[i] = CompanyNameList{CpmpanyNames: make([]string, 5000), Used: false}
	}
	set = make(map[string]bool, 15000000)
}

var mutex sync.RWMutex

//集合  里 去重 也 可以
func handlerCompanyNameList(redisUtil *util.RedisUtl, s int, e int) {
	client := redisUtil.GetClient()
	for {
		//mutex.Lock()
		for key := range set {
			p, err := time.ParseDuration("1h")
			if err != nil {
				panic(err) //handler ex
			}
			//0-0-0 百度 天眼查 it 桔子 未 查询
			if redisUtil.Set(client, key, "0-0-0", p*24) {
				println("插入成功")
			} else {
				println("插入失败")
			}
		}
		//mutex.Unlock()
		if taskEnd {
			break
		}
	}

	return
	for {
		for i := s; i < e; i++ {
			mutex.Lock()
			if companyNameLists[i].Used {
				continue
			} else if companyNameLists[i].count > 0 {
				//handler task
				for j := range companyNameLists[i].CpmpanyNames {
					key := util.SeurityInstance.AesEncrypt(companyNameLists[i].CpmpanyNames[j])
					p, err := time.ParseDuration("1h")
					if err != nil {
						panic(err) //handler ex
					}
					//0-0-0 百度 天眼查 it 桔子 未 查询
					if redisUtil.Set(client, key, "0-0-0", p*24) {
						println("插入成功")
					} else {
						println("插入失败")
					}
				}

			}
			mutex.Unlock()
		}
		if taskEnd {
			break
		}
	}

}

func esScroll(esClient *elastic.Client, es Handler, total int, rows int) {
	scollService := esClient.Scroll("crawl")
	fetchSourceContext := elastic.NewFetchSourceContext(true)
	fetchSourceContext.Include("company") //pass 70-80w ex
	//什么情况之前 好好的 难道 不能整合放到一起 拆开? 包问题?
	//es 最近 怎么老是异常
	fetchSourceContext.Exclude("")
	scollService = scollService.Sort("publish_date", true).FetchSourceContext(fetchSourceContext)
	size := rows
	readTotal := rows
	scollService.Size(size)
	println("当前 执行开始 时间 " + time.Now().Format("2006-01-02 15:04:05"))
	result, err := scollService.Do(context.Background())
	if err != nil {
		return
	}

	for {

		if result.Hits == nil || result.Hits.Hits == nil {
			println("scoll read fail")
			break
		}
		if len(result.Hits.Hits) == 0 {
			println("scoll read end")
			break
		}
		time.Sleep(10)
		for s := range result.Hits.Hits {
			var data map[string]interface{}
			buffer := result.Hits.Hits[s].Source
			if buffer == nil {
				continue
			}
			defer func() bool { //必须要先声明defer，否则不能捕获到panic异常
				if err2 := recover(); err2 != nil {
					fmt.Print("internal error")
					return true
				}
				return false
			}()
			//ex
			err := json.Unmarshal(buffer, &data)
			if err != nil {
				continue
			}
			es.Handler(data)
		}
		println("当前 处理 时间 " + time.Now().Format("2006-01-02 15:04:05") + " 处理 " + strconv.Itoa(readTotal) + "条数据")
		if total > readTotal {
			readTotal += rows
			/*if size>=total{
				size=total
			}*/
		} else {
			println("当前 end  时间 " + time.Now().Format("2006-01-02 15:04:05") + "scoll read end")
			break
		}
		defer func() bool { //必须要先声明defer，否则不能捕获到panic异常
			if err2 := recover(); err2 != nil {
				fmt.Print("internal error")
				return true
			}
			return false
		}()
		//ex
		result, err = scollService.ScrollId(result.ScrollId).Do(context.Background())
		if err != nil {
			println("当前 fail 时间 " + time.Now().Format("2006-01-02 15:04:05"))
			break
		}
		println("当前 执行结束 时间 " + time.Now().Format("2006-01-02 15:04:05"))
	}
}

func esSql() {
	//https://www.elastic.co/guide/en/elasticsearch/reference/master/sql-rest-format.html
	url := "http://127.0.0.1:9200/_sql?format=json"
	//1400*10000 max  10000 这怎么查 去重
	//	param :=map[string]interface{}{"query":"select company from crawl group by company  order by crawl.publish_date ","fetch_size":1} //error
	//	param :=map[string]interface{}{"query":"select company from crawl   order by crawl.publish_date ","fetch_size":1} //pass
	param := map[string]interface{}{
		"query":      "select company from crawl group by company  ",
		"fetch_size": 1,
		/*"filter": {
			"range": {
				"page_count": {
					"gte" : 100,
					"lte" : 200
				}
			}
		},*/
		//"cursor": "sDXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAAEWYUpOYklQMHhRUEtld3RsNnFtYU1hQQ==:BAFmBGRhdGUBZgVsaWtlcwFzB21lc3NhZ2UBZgR1c2Vy9f///w8=",
	}
	/**
		{"columns":[{"name":"company","type":"text"}],"rows":[["(新王牌教育)拥华教育投资
	管理(上海)有限公司"]],"cursor":"w7ysAwFjAQVjcmF3bJ4BAQEJY29tcG9zaXRlB2dyb3VwYnkA
	AP8BAAI5MgEPY29tcGFueS5rZXl3b3JkAAABAAABAQoBAjkyABco5paw546L54mM5pWZ6IKyKeaLpeWN
	juaVmeiCsuaKlei1hOeuoeeQhijkuIrmtbcp5pyJ6ZmQ5YWs5Y+4AAIBAAAAAAEA/////w8AAAAAAAAA
	AAAAAAABWgMAAgIAAAAAAAD/////DwEBawI5MgABWgABAQ=="}
	*/

	j, err := json.Marshal(param)
	if err != nil {
		println("json fail", err)
	}
	result, err := http.Post(url, "application/json", strings.NewReader(string(j)))
	if err != nil {
		println("post fail", err)
	}
	buffer, err := ioutil.ReadAll(result.Body)
	if err != nil {
		println("post read fail", err)
	}
	println(string(buffer))
}

type EsHanlerCsv struct {
	//EsHanler
	client    *redis.Client
	redisUtil *util.RedisUtl
}
type EsHanler struct {
	client    *redis.Client
	redisUtil *util.RedisUtl
}

//int 查询 麻烦(效率低) 不知道具体数据
//id string 不用查询
var companys map[string]interface{} = make(map[string]interface{}, 100*10000)
var citys map[string]string = make(map[string]string, 100*1000)
var areas map[string][]string = make(map[string][]string, 100*1000)

var jobs map[string]map[string]string = make(map[string]map[string]string, 1500*10000)

var ids map[string]int64 = make(map[string]int64, 100*10000)

type Handler interface {
	Handler(data map[string]interface{}) bool
}

// runtime: out of memory: cannot allocate
// 4194304-byte block (1727954944 in use)
// fatal error: out of memory
//17w -40w ex *2 内存分配异常
//最好 不用mak map [] out of memory 最好用实体 默认分配

func (es EsHanlerCsv) Handler(data map[string]interface{}) bool {
	return false
	//b, _ := json.Marshal(data)
	//print(string(b))
	companyName := data["company"].(string)
	v, ok := data["company_link"]
	company_link := ""
	if ok {
		company_link = v.(string)
	}
	city := data["city"].(string)
	job_id := data["job_id"].(string)
	job := data["job"].(string)
	area := data["area"].(string)
	address := data["address"].(string)
	salary := data["salary"].(string)
	max_year_salary := data["max_year_salary"].(string)
	min_year_salary := data["min_year_salary"].(string)
	publish_date := data["publish_date"].(string)

	//println(companyName)
	//ex out memory
	c := util.SeurityInstance.AesEncrypt(companyName)
	val, ok := companys[c]

	//ex out memory 17w
	jobs[job_id] = make(map[string]string, 10)

	if city != "" {
		ci := util.SeurityInstance.AesEncrypt(city)
		citys[ci] = city
		jobs[job_id]["city"] = ci
	} else {
		jobs[job_id]["city"] = city
	}
	a := util.SeurityInstance.AesEncrypt(area + address)
	as := make([]string, 2)
	as[0] = area
	as[1] = address
	areas[a] = as
	//*2 内存  不够 不能 使用 append
	if ok {
		var m = val.(map[string]interface{})
		//var links = m["link"].([]string)
		//links = append(links, company_link)
		//m["link"] = links
		var links = m["link"].(util.Collection)
		links.Add(company_link)
		//l := m["length"].(int32)
		//m["length"] = l + 1
		//links[l] = company_link
	} else {
		var co = make(map[string]interface{}, 3)
		var links = util.NewList()
		links.Add(company_link)
		co["link"] = links
		co["name"] = companyName
		//var l int32 = 0
		//co["length"] = l

		companys[c] = co
		//links[0] = company_link

		val = co
	}

	jobs[job_id]["job"] = job

	jobs[job_id]["company"] = c
	jobs[job_id]["area"] = a
	jobs[job_id]["salary"] = salary
	jobs[job_id]["max_year_salary"] = max_year_salary
	jobs[job_id]["min_year_salary"] = min_year_salary
	jobs[job_id]["publish_date"] = publish_date
	return true
}
func (es EsHanler) Handler(data map[string]interface{}) bool {
	companyName := data["company"].(string)
	p, err := time.ParseDuration("1h")
	if err != nil {
		print(err)
	}
	println(companyName)
	key := util.SeurityInstance.AesEncrypt(companyName)
	set[key] = true
	return true
	//0-0-0 百度 天眼查 it 桔子 未 查询
	if es.redisUtil.Set(es.client, key, "0-0-0", p*24) {
		println("插入成功")
		return true
	} else {
		//println("插入失败")
		return false
	}
}

type CompanyName struct {
	CompanyName string
}

func insertCache(db *gorm.DB, redisUtil *util.RedisUtl) {
	sql := " SELECT company_name FROM t_company where company_name != '' group by  company_name"
	//var names []string //乱码 不知道 啥玩意
	var names []CompanyName
	db.Raw(sql).Scan(&names)
	client := redisUtil.GetClient()
	//https://studygolang.com/articles/23190?fr=sidebar
	//%y-%M-%d %H:%m:%s yyyy-MM-dd HH:mm:ss fail 2006-01-02 15:04:05 时间 不能 写成 其他 的
	println("当前 执行开始 时间 " + time.Now().Format("2006-01-02 15:04:05"))
	sTime := time.Now().Unix()
	for i := range names {
		//fmt.Print("xxx %d "+v.CompanyName+"\n",i)
		//fmt.Print("xxx %d \n",i)
		p, err := time.ParseDuration("1h")
		if err != nil {
			print(err)
		}
		key := util.SeurityInstance.AesEncrypt(names[i].CompanyName)
		//0-0-0 百度 天眼查 it 桔子 未 查询
		if redisUtil.Set(client, key, "0-0-0", p*24) {
			println("插入成功")
		} else {
			//println("插入失败")
		}
	}
	eTime := time.Now().Unix()
	println("当前 结束开始 时间 " + time.Now().Format("2006-01-02 15:04:05"))
	println("当前 任务结束 花费 时间 ", eTime-sTime)
}

//有参 构造函数 方法名 不能一致
func NewCompany(id int, companyName string, corporation string, registeredCapital string,
	registrationTime string, address string, businessAddress string, mailbox string,
	businessScope string, getTime string, catalog string, childrenCatalog string,
	status string, times string, createTime string, updateTime string) *Company {
	return &Company{ID: id, CompanyName: companyName, Corporation: corporation,
		RegisteredCapital: registeredCapital, RegistrationTime: registrationTime,
		Address: address, BusinessAddress: businessAddress, Mailbox: mailbox,
		BusinessScope: businessScope, GetTime: getTime, Catalog: catalog,
		ChildrenCatalog: childrenCatalog, Status: status, Times: times, CreateTime: createTime,
		UpdateTime: updateTime}
}

//无参 构造函数
func NoNewCompany() *Company {
	return &Company{}
}

// 设置 Company 的表名为 t_company
func (Company) TableName() string {
	return "t_company"
}

func TianYanCha(name string) string {

	//%E5%8C%97%E4%BA%AC%E5%8D%9A%E9%80%94%E7%89%A9%E6%B5%81%E8%AE%BE%E5%A4%87%E6%9C%89%E9%99%90%E5%85%AC%E5%8F%B8
	//北京博途物流设备有限公司
	escapeName := url.QueryEscape(name)
	//url.QueryUnescape(name)
	//查询相关联公司
	//var urlMatch string ="https://sp0.tianyancha.com/search/suggestV2.json?key="+escapeName+"&_="+strconv.FormatFloat(rand.Float64(),'g',30,32)
	var urlMatch string = "https://sp0.tianyancha.com/search/suggestV2.json?key=" + escapeName + "&_=" + strconv.FormatInt(time.Now().Unix(), 15)
	//json
	res, err := http.Get(urlMatch)
	if err != nil {
		fmt.Print("match ex : %s", err.Error())
	}
	defer res.Body.Close()
	body, err := ioutil.ReadAll(res.Body)
	if err != nil {
		fmt.Print("match suc, read buffer ex : %s", err.Error())
	}
	var result map[string]interface{}
	err = json.Unmarshal(body, &result)
	if err != nil {
		fmt.Print("match suc, parse map  ex : %s", err.Error())
	}
	if result["state"] == "ok" {
		ls, ok := result["data"].(list.List)
		if ok {
			for i := ls.Front(); i != nil; i = i.Next() {
				companys := i.Value.(map[string]interface{})
				//匹配 最 相适应的公司
				if s, ok := companys["comName"].(string); ok && s == name {
					name = s
				}
			}

			//
			//for i,v := range ls {
			//	var n string=ls[i].(string)
			//	var n string=v.(string)
			//}
		}
	}

	//查询公司信息
	escapeName = url.QueryEscape(name)
	urlCompany := "https://www.tianyancha.com/search?key=" + escapeName
	//html
	res, err = http.Get(urlCompany)
	if err != nil {
		fmt.Print("read ex : %s", err.Error())
	}
	defer res.Body.Close()
	body, err = ioutil.ReadAll(res.Body)
	if err != nil {
		fmt.Print("read suc, read buffer ex : %s", err.Error())
	}
	str := string(body)
	//麻烦 还需要第三方库
	print(str)
	return str
}
