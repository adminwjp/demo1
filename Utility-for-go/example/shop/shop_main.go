package shop

import (
"log"
"os"
"news/shop/router"
"github.com/Unknwon/goconfig"
//"shop/protos"
"net/http"
"github.com/gin-gonic/gin"
)

//注意 不能 相互 引用  不然 报错
func main() {
	//protos.Test()
	r := gin.Default()
	r.Use(Cors())
	//err := initZipkinTracer(r)
	//if err != nil {
	//panic(err)
	//}
	//defer zkReporter.Close()
	router.InitChatRouter(r)
	router.InitShopRouter(r)
	r.GET("/test", func(c *gin.Context) {
		c.Writer.WriteString("test")
	})
	r.GET("/aa", func(c *gin.Context) {

		log.Println("RemoteAddr :" + c.Request.RemoteAddr + " , RequestURI:" + c.Request.RequestURI)
		c.JSON(200, gin.H{
			"ceshi": "hello world",
		})
	})


	//r.GET("/test1", controllers.TestTpl)

	//r.GET("/seller/list", controllers.BuyerList)
	r.StaticFS("/front", http.Dir("static/front/styles/default"))
	r.StaticFS("/js", http.Dir("static/js"))
	r.StaticFS("/doc", http.Dir("static/doc"))
	r.StaticFS("/lib", http.Dir("static/lib"))
	r.StaticFS("/images", http.Dir("static/images"))
	cf, err := goconfig.LoadConfigFile("config/cfg.ini")
	if err != nil {
		println("load cfg.ini fail")
		os.Exit(1)
	}
	log.Println("Load cfg.ini Success")
	r.Run(":" + cf.MustValue("Register", "Port", "8080"))

	//r.Run(":" + strconv.Itoa(ctls.ServerPort)) // 强指定端口，默认8088
}

func Cors() gin.HandlerFunc {
	return func(c *gin.Context) {
		c.Header("Access-Control-Allow-Origin", "*") // 可将将 * 替换为指定的域名
		c.Header("Access-Control-Allow-Methods", "POST, GET, OPTIONS, PUT, DELETE, UPDATE")
		c.Header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, Authorization")
		c.Header("Access-Control-Expose-Headers", "Content-Length, Access-Control-Allow-Origin, Access-Control-Allow-Headers, Cache-Control, Content-Language, Content-Type")
		c.Header("Access-Control-Allow-Credentials", "true")
		c.Next()
	}
}

// 这个 包 在 哪个位置 文档 好少 下载源码 看
//https://github.com/openzipkin/zipkin-go-opentracing
/*
var (
	zkReporter reporter.Reporter
	zkTracer   opentracing.Tracer
)

const (
	serviceName     = "zipkin_gin_server"
	serviceEndpoint = "localhost:8080"
	zipkinAddr      = "http://127.0.0.1:9411/api/v2/spans"
)

func initZipkinTracer(engine *gin.Engine) error {
	zkReporter = zkHttp.NewReporter(zipkinAddr)
	endpoint, err := zipkin.NewEndpoint(serviceName, serviceEndpoint)
	if err != nil {
		log.Fatalf("unable to create local endpoint: %+v\n", err)
		return err
	}
	nativeTracer, err := zipkin.NewTracer(zkReporter, zipkin.WithTraceID128Bit(true), zipkin.WithLocalEndpoint(endpoint))
	if err != nil {
		log.Fatalf("unable to create tracer: %+v\n", err)
		return err
	}
	zkTracer = zkOt.Wrap(nativeTracer)
	opentracing.SetGlobalTracer(zkTracer)
	// 将tracer注入到gin的中间件中
	engine.Use(func(c *gin.Context) {
		span := zkTracer.StartSpan(c.FullPath())
		defer span.Finish()
		c.Next()
	})
	return nil
}
*/
