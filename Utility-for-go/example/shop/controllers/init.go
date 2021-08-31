package controllers

import (
	"strconv"
	"news/shop/dto"
	"news/shop/services"

	"github.com/gin-gonic/gin"
)

type Page struct {
	List       interface{} `json:"list"`
	PageNumber int         `json:"pageNumber"`
	PageSize   int         `json:"pageSize"`
	TotalPage  int         `json:"totalPage"`
	TotalRow   int         `json:"totalRow"`
}

func init() {
	services.InitDb()
	//InitView()
}

//consul callback
func Check(c *gin.Context) {
	add(c, true)
}

func getPageAndSize(c *gin.Context) (int, int) {
	var page, size int = 1, 10
	p, exists := c.GetQuery("page")
	s, exists := c.GetQuery("size")
	if exists {
		//明明存在 还 报 红线
		pa, err := strconv.Atoi(p)
		page = pa
		if err != nil {
			page = 1
		}
	}
	if exists {
		si, err := strconv.Atoi(s)
		size = si
		if err != nil {
			size = 10
		}
	}
	return page, size
}

func getPage(rows int, size int) int {
	return dto.GetPage(rows, size)
}

func add(c *gin.Context, res bool) {
	if res {
		c.JSON(200, gin.H{
			"data":    nil,
			"success": true,
			"code":    200,
			"msg":     "success",
		})
	} else {
		c.JSON(200, gin.H{
			"data":    nil,
			"success": false,
			"code":    303,
			"msg":     "fail",
		})
	}

}
