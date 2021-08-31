package services

import (
	"news/shop/models"
)

func AgentCommissionList1() {
	sql := "elect bu.headimgurl, bu.nickname, a.id, a.agent_name, a.agent_phone,"
	sql += "a.created, a.audit_date, q.member_count, b.total_price "
	sql += "from t_agent a LEFT JOIN t_buyer bu on a.buyer_id = bu.id"
	sql += "LEFT JOIN (select count(parent_id) as member_count, parent_id as pid from"
	sql += " t_agent GROUP BY parent_id) q on a.id = q.pid "
	sql += " LEFT JOIN (select SUM(total_price) as total_price, buyer_id from t_order"
	sql += " where payment_status=2) b on a.buyer_id = b.buyer_id"
	sql += "where a.seller_id = ? and a.status = 1 and bu.active=1 "
}

func AddAgent(agent models.Agent) bool {
	agent.Id = Id.GetIdByStruct(agent)
	res,_:= add(agent)
	return  res
}

func UpdateAgent(agent models.Agent) bool {
	res,_:= update(agent)
	return  res
}

func AgentList(agent models.Agent, page int, size int) ([]*models.Agent, int64) {
	var agents []*models.Agent
	data, count,_ := selectList(agent, page, size, agents)
	return data.([]*models.Agent), count
}

func AddAgentRank(agentRank models.AgentRank) bool {
	agentRank.Id = Id.GetIdByStruct(agentRank)
	res,_ := add(agentRank)
	return  res
}

func UpdateAgentRank(agentRank models.AgentRank) bool {
	res,_ := update(agentRank)
	return  res
}

func AgentRankList(agentRank models.AgentRank, page int, size int) ([]*models.AgentRank, int64) {
	var agentRanks []*models.AgentRank
	data, count,_ := selectList(agentRank, page, size, agentRanks)
	return data.([]*models.AgentRank), count
}

func AddAgentCommission(agentCommission models.AgentCommission) bool {
	agentCommission.Id = Id.GetIdByStruct(agentCommission)
	res,_ := add(agentCommission)
	return  res
}

func UpdateAgentCommission(agentCommission models.AgentCommission) bool {
	res,_ := update(agentCommission)
	return  res
}

func AgentCommissionList(agentCommission models.AgentCommission, page int, size int) ([]*models.AgentCommission, int64) {
	var agentCommissions []*models.AgentCommission
	data, count,_ := selectList(agentCommission, page, size, agentCommissions)
	return data.([]*models.AgentCommission), count
}
