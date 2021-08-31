package tests

import (
	"encoding/json"
	//"log"
	"strconv"
	"testing"

	//"time"
	"utility/shop/models"
	"utility/shop/services"
	//"shop/util"
	//"github.com/jinzhu/gorm"
)

var rows int = 100

func init() {
	services.InitDb()
}
func aTestBuyer(t *testing.T) {
	var buyers []models.Buyer = make([]models.Buyer, rows)
	tx := services.Db.Begin()
	for i := 0; i < rows; i++ {
		buyers[i] = models.Buyer{}
		buyers[i].Id = services.Id.GetIdByStruct(models.Buyer{})
		buyers[i].Nickname = "test" + strconv.Itoa(i)
		tx.Create(buyers[i])
	}
	tx.Commit()
	tx = services.Db.Begin()
	for i := 0; i < rows; i++ {
		buyerAddr := models.BuyerAddr{}
		buyerAddr.Id = services.Id.GetIdByStruct(models.BuyerAddr{})
		buyerAddr.Addr = "Addr_test" + strconv.Itoa(i)
		tx.Create(buyerAddr)
	}
	tx.Commit()
	//error
	//services.Db.CreateInBatches(buyers,len(buyers))
	t.Log("insert batcht buyers ")
	data, count := services.BuyerList(models.Buyer{}, 1, 10)

	result := map[string]interface{}{"data": data, "count": count}
	res, err := json.Marshal(result)
	if err != nil {
		t.Log("query fail,error" + err.Error())
	} else {
		t.Log(string(res))
	}
	buyer := services.GetBuyer(1)
	res, err = json.Marshal(buyer)
	if err != nil {
		t.Log("query fail,error" + err.Error())
	} else {
		t.Log(string(res))
	}
}

func aTestSeller(t *testing.T) {
	tx := services.Db.Begin()
	for i := 0; i < rows; i++ {
		buyer := models.Seller{}
		buyer.Id = services.Id.GetIdByStruct(models.Seller{})
		buyer.Nickname = "test" + strconv.Itoa(i)
		tx.Create(buyer)
	}
	tx.Commit()
	tx = services.Db.Begin()
	for i := 0; i < rows; i++ {
		sellerAddr := models.SellerAddr{}
		sellerAddr.Id = services.Id.GetIdByStruct(models.SellerAddr{})
		sellerAddr.Addr = "Addr_test" + strconv.Itoa(i)
		tx.Create(sellerAddr)
	}
	tx.Commit()

}

func aTestAgent(t *testing.T) {
	tx := services.Db.Begin()
	for i := 0; i < rows; i++ {
		agent := models.Agent{Id: services.Id.GetIdByStruct(models.Agent{}),
			AgentName: "test" + strconv.Itoa(i)}
		tx.Create(agent)
	}
	tx.Commit()
	tx = services.Db.Begin()
	for i := 0; i < rows; i++ {
		agentRank := models.AgentRank{Id: services.Id.GetIdByStruct(models.AgentRank{}),
			RankName: "RankName_test" + strconv.Itoa(i)}
		tx.Create(agentRank)
	}
	tx.Commit()

}
