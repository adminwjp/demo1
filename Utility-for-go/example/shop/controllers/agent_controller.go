package controllers

import (
	"news/shop/dto"
	"news/shop/models"
	"news/shop/services"

	"github.com/gin-gonic/gin"
)

func AddAgent(c *gin.Context) {
	var agent models.Agent

	c.BindJSON(&agent)
	res := services.AddAgent(agent)
	add(c, res)
}

func UpdateAgent(c *gin.Context) {
	var agent models.Agent

	c.BindJSON(&agent)
	res := services.UpdateAgent(agent)
	add(c, res)
}

func AgentList(c *gin.Context) {
	var agent models.Agent = models.Agent{}
	page, size := getPageAndSize(c)
	data, count := services.AgentList(agent, page, size)
	result := dto.ResultDto{Data: data}
	result.SetPage(page, size, int(count))
	c.JSON(200, gin.H{
		"data":    result,
		"success": true,
		"code":    200,
		"msg":     "success",
	})
}

func AddAgentRank(c *gin.Context) {
	var agentRank models.AgentRank
	c.BindJSON(&agentRank)
	res := services.AddAgentRank(agentRank)
	add(c, res)
}
func UpdateAgentRank(c *gin.Context) {
	var agentRank models.AgentRank
	c.BindJSON(&agentRank)
	res := services.UpdateAgentRank(agentRank)
	add(c, res)
}

func AgentRankList(c *gin.Context) {
	var agentRank models.AgentRank = models.AgentRank{}
	page, size := getPageAndSize(c)
	data, count := services.AgentRankList(agentRank, page, size)
	result := dto.ResultDto{Data: data}
	result.SetPage(page, size, int(count))
	c.JSON(200, gin.H{
		"data":    result,
		"success": true,
		"code":    200,
		"msg":     "success",
	})
}


func AddAgentCommission(c *gin.Context) {
	var agentCommission models.AgentCommission
	c.BindJSON(&agentCommission)
	res := services.AddAgentCommission(agentCommission)
	add(c, res)
}
func UpdateAgentCommission(c *gin.Context) {
	var agentCommission models.AgentCommission
	c.BindJSON(&agentCommission)
	res := services.UpdateAgentCommission(agentCommission)
	add(c, res)
}

func AgentCommissionList(c *gin.Context) {
	var agentCommission models.AgentCommission = models.AgentCommission{}
	page, size := getPageAndSize(c)
	data, count := services.AgentCommissionList(agentCommission, page, size)
	result := dto.ResultDto{Data: data}
	result.SetPage(page, size, int(count))
	c.JSON(200, gin.H{
		"data":    result,
		"success": true,
		"code":    200,
		"msg":     "success",
	})
}