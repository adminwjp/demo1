package mall

import (
	"github.com/gin-gonic/gin"
	"net/http"
	"news/ctls"
	"strconv"
)
func main () {
	r := gin.Default()
	r.GET("/test", func(c *gin.Context){
		c.JSON(200,gin.H{
			"ceshi":"hello world",
		})
	})
	cataloginforCtl:=ctls.CataloginforCtl{}
	r.GET("/catalog", cataloginforCtl.GetList)
	//什么玩意封装 太 厉害 怎么访问 不了  每个对应写 映射
	//r.Static("/","static")
	r.Static("/admin","static/admin")
	r.StaticFile("/WxPic","static/WxPic")
	//r.StaticFile("/","static")
	r.StaticFS("/js", http.Dir("static/js"))
	r.StaticFS("/doc", http.Dir("static/doc"))
	r.StaticFS("/lib", http.Dir("static/lib"))
	r.StaticFS("/images", http.Dir("static/images"))
	r.Run(":"+strconv.Itoa(ctls.ServerPort))   // 强指定端口，默认8088
}