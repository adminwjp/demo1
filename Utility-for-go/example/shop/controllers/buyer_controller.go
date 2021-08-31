package controllers

import (
	"strconv"
	"strings"
	"news/shop/dto"
	"news/shop/models"
	"news/shop/services"

	"github.com/gin-gonic/gin"
)

func BuyerList(c *gin.Context) {
	page, size := getPageAndSize(c)
	data, count := services.BuyerList(models.Buyer{}, page, size)
	result := Page{List: data, PageNumber: page, PageSize: size, TotalRow: int(count),
		TotalPage: getPage(int(count), size)}
	c.JSON(200, gin.H{
		"data":    result,
		"Success": true,
	})
}

func GetBuyer(c *gin.Context) {
	idStr, exists := c.GetQuery("id")
	var id int64 = 0
	if exists {
		//明明存在 还 报 红线
		v, err := strconv.ParseInt(idStr, 10, 64)
		if err != nil {
			c.JSON(303, gin.H{
				"Success": false,
			})
			return
		}
		id = v
	}
	data := services.GetBuyer(id)
	c.JSON(200, gin.H{
		"data":    data,
		"Success": true,
	})
}
func SetBuyerActive(c *gin.Context) {
	SetBuyerActiveOrSetBuyerIsReceiver(c, "active")
}

func SetBuyerIsReceiver(c *gin.Context) {
	SetBuyerActiveOrSetBuyerIsReceiver(c, "is_receiver")
}
func SetBuyerActiveOrSetBuyerIsReceiver(c *gin.Context, name string) {
	idStr, exists := c.GetQuery("ids")
	var ids []int64
	if exists {
		strs := strings.Split(idStr, "-")
		if strs == nil || len(strs) == 0 {
			c.JSON(200, gin.H{
				"success": true,
			})
			return
		}
		ids = make([]int64, len(strs))
		for i := 0; i < len(strs); i++ {
			//明明存在 还 报 红线
			v, err := strconv.ParseInt(idStr, 10, 64)
			if err != nil {
				c.JSON(303, gin.H{
					"Success": false,
				})
				return
			}
			ids[i] = v
		}

	}

	activeStr, exists := c.GetQuery(name)
	var active int32 = 0
	if exists {
		//明明存在 还 报 红线
		v, err := strconv.ParseInt(activeStr, 10, 32)
		if err != nil {
			c.JSON(303, gin.H{
				"Success": false,
			})
		}
		active = int32(v)
	}
	var data bool
	if name == "active" {
		data = services.SetBuyerActive(ids, active)
	} else {
		data = services.SetBuyerIsReceiver(ids, active)
	}

	c.JSON(200, gin.H{
		"data":    data,
		"Success": true,
	})
}

func AddBuyer(c *gin.Context) {
	var buyer models.Buyer
	c.BindJSON(&buyer)
	res := services.AddBuyer(buyer)
	add(c, res)
}

func AddBuyerAddr(c *gin.Context) {
	var buyerAddr models.BuyerAddr
	c.BindJSON(&buyerAddr)
	res := services.AddBuyerAddr(buyerAddr)
	add(c, res)
}
func BuyerAddrList(c *gin.Context) {
	page, size := getPageAndSize(c)
	data, count := services.BuyerAddrList(page, size)
	result := dto.ResultDto{Data: data}
	result.SetPage(page, size, int(count))
	c.JSON(200, gin.H{
		"data":    result,
		"success": true,
		"code":    200,
		"msg":     "success",
	})
}
