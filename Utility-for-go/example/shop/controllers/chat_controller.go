package controllers

import (
	"github.com/gin-gonic/gin"
	"news/shop/dto"
)

func CheckPhoneAvailable(c *gin.Context) {
	var checkPhoneRequest =&dto.CheckPhoneRequest{}
	err:=c.ShouldBindJSON(&checkPhoneRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
			"result": false,
		})
		return
	}
	//true
	c.JSON(200, gin.H{
		"code":   200,
		"result": true,
	})
}

//{"code":200}
func SendCode(c *gin.Context) {
	var checkPhoneRequest =&dto.CheckPhoneRequest{}
	err:=c.ShouldBindJSON(&checkPhoneRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
			"result": false,
		})
		return
	}
	//true
	c.JSON(200, gin.H{
		"code": 200,
	})
}

func VerifyCode(c *gin.Context) {
	var verifyCodeRequest =&dto.VerifyCodeRequest{}
	err:=c.ShouldBindJSON(&verifyCodeRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code":   200,
		"result": map[string]string{"verification_token": ""},
	})
}

func Register(c *gin.Context) {
	var registerRequest =&dto.RegisterRequest{}
	err:=c.ShouldBindJSON(&registerRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code":   200,
		"result": map[string]string{"id": ""},
	})
}

func Login(c *gin.Context) {
	var loginRequest  =&dto.LoginRequest{}
	err:=c.ShouldBindJSON(&loginRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code":   200,
		"result": map[string]string{"id": "", "token": ""},
	})

}
func GetToken(c *gin.Context) {
	c.JSON(200, gin.H{
		"code":   200,
		"result": map[string]string{"id": "", "token": ""},
	})
}

//{"code":200}
func SetName(c *gin.Context) {
	var setNameRequest =&dto.SetNameRequest{}
	err:=c.ShouldBindJSON(&setNameRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code": 200,
	})
}

//{"code":200}
func SetPortrait(c *gin.Context) {
	 var setPortraitRequest =&dto.SetPortraitRequest{}
	err:=c.ShouldBindJSON(&setPortraitRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code": 200,
	})
}

//{"code":200}
func ChangePassword(c *gin.Context) {
	var changePasswordRequest =&dto.ChangePasswordRequest{}
	err:=c.ShouldBindJSON(&changePasswordRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code": 200,
	})
}

//{"code":200}
func RestPassword(c *gin.Context) {
	var restPasswordRequest  =&dto.RestPasswordRequest{}
	err:=c.ShouldBindJSON(&restPasswordRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code": 200,
	})
}

func GetUserInfoById(c *gin.Context) {
	userid := c.Param("userid")
	if userid==""{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code":   200,
		"result": map[string]string{"id": "", "nickname": "", "portraitUri": ""},
	})
}


func GetUserInfoFromPhone(c *gin.Context) {
	region := c.Param("region")
	phone := c.Param("phone")
	if region==""||phone==""{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code":   200,
		"result": map[string]string{"id": "", "nickname": "", "portraitUri": ""},
	})
}


func SendFriendInvitation(c *gin.Context) {
	 var friendInvitationRequest =&dto.FriendInvitationRequest{}
	err:=c.ShouldBindJSON(&friendInvitationRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code":    200,
		"result":  map[string]string{"action": "Sent"},
		"message": "",
	})
}


func GetAllUserRelationship(c *gin.Context) {
	c.JSON(200, gin.H{
		"code":    200,
		"result":  []map[string]interface{}{
			map[string]interface{}{"displayName":"",
			"message":"手机号:18622222222昵称:的用户请求添加你为好友",
			"status":11,
			"updatedAt":"2016-01-07T06:22:55.000Z",
			"user":map[string]string{
				"id":"i3gRfA1ml",
				"nickname":"nihaoa",
				"portraitUri":""},
			},
		},
	})
}


func GetFriendInfoByID(c *gin.Context) {
	userid :=c.Param("userid")
	if userid==""{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code":    200,
		"result":  []map[string]interface{}{
			map[string]interface{}{"displayName":"",
			"user":map[string]string{
				"id":"i3gRfA1ml",
				"nickname":"nihaoa",
				"region":"",
				"phone":"",
				"portraitUri":""},
			},
		},
	})
}

//{"code":200}
func AgreeFriends(c *gin.Context) {
	var agreeFriendsRequest=&dto.AgreeFriendsRequest{}
	err:=c.ShouldBindJSON(&agreeFriendsRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code": 200,
	})
}

//{"code":200}
func DeleteFriend(c *gin.Context) {
	var agreeFriendsRequest =&dto.AgreeFriendsRequest{}
	err:=c.ShouldBindJSON(&agreeFriendsRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code": 200,
	})
}


//{"code":200}
func SetFriendDisplayName(c *gin.Context) {
	var setFriendDisplayNameRequest =&dto.SetFriendDisplayNameRequest{}
	err:=c.ShouldBindJSON(&setFriendDisplayNameRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code": 200,
	})
}

func GetBlackList(c *gin.Context) {
		c.JSON(200, gin.H{
		"code":    200,
		"result":  []map[string]interface{}{
			map[string]interface{}{
			"user":map[string]string{
				"id":"i3gRfA1ml",
				"nickname":"nihaoa",
				"updatedAt":"",
				"portraitUri":""},
			},
		},
	})
}

//{"code":200}
func AddToBlackList(c *gin.Context) {
	var agreeFriendsRequest =&dto.AgreeFriendsRequest{}
	err:=c.ShouldBindJSON(&agreeFriendsRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code": 200,
	})
}

//{"code":200}
func RemoveFromBlackList(c *gin.Context) {
	var agreeFriendsRequest=&dto.AgreeFriendsRequest{}
	err:=c.ShouldBindJSON(&agreeFriendsRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code": 200,
	})
}


func CreateGroup(c *gin.Context) {
	 var createGroupRequest =&dto.CreateGroupRequest{}
	err:=c.ShouldBindJSON(&createGroupRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code":   200,
		"result": map[string]string{"id": ""},
	})
}

//{"code":200}
func SetGroupPortrait(c *gin.Context) {
	var setGroupPortraitRequest =&dto.SetGroupPortraitRequest{}
	err:=c.ShouldBindJSON(&setGroupPortraitRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code": 200,
	})
}

func GetGroups(c *gin.Context) {
		c.JSON(200, gin.H{
		"code":    200,
		"result":  []map[string]interface{}{
			map[string]interface{}{
				"user": map[string]interface{}{
					"id":          "i3gRfA1ml",
					"name":        "nihaoa",
					"creatorId":   "",
					"portraitUri": "",
					"memberCount": 7},
			},
		},
	})
}

func GetGroupInfo(c *gin.Context) {
	groupId :=c.Param("groupId")
	if groupId==""{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code":    200,
		"result":  []map[string]interface{}{
			map[string]interface{}{
				"id":"i3gRfA1ml",
				"name":"nihaoa",
				"creatorId":"",
				"portraitUri":"",
				"memberCount":7},
			},
	})
}


func GetGroupMember(c *gin.Context) {
	groupId :=c.Param("groupId")
	if groupId==""{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code":    200,
		"result":  []map[string]interface{}{
			map[string]interface{}{
			"user":map[string]interface{}{
				"displayName":"nihaoa",
				"createdAt":"",
				"role":7},
			},
		},
	})
}

//{"code":200}
func AddGroupMember(c *gin.Context) {
	var createGroupRequest =&dto.CreateGroupRequest{}
	err:=c.ShouldBindJSON(&createGroupRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code": 200,
	})
}

//{"code":200}
func DeleGroupMember(c *gin.Context) {
	var createGroupRequest =&dto.CreateGroupRequest{}
	err:=c.ShouldBindJSON(&createGroupRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code": 200,
	})
}

//{"code":200}
func SetGroupName(c *gin.Context) {
	 var setGroupNameRequest =&dto.SetGroupNameRequest{}
	err:=c.ShouldBindJSON(&setGroupNameRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code": 200,
	})
}

//{"code":200}
func QuitGroup(c *gin.Context) {
	var quitGroupRequest =&dto.QuitGroupRequest{}
	err:=c.ShouldBindJSON(&quitGroupRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code": 200,
	})
}

//{"code":200}
func DissmissGroup(c *gin.Context) {
	var quitGroupRequest =&dto.QuitGroupRequest{}
	err:=c.ShouldBindJSON(&quitGroupRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code": 200,
	})
}

//{"code":200}
func SetGroupDisplayName(c *gin.Context) {
	 var setGroupDisplayNameRequest =&dto.SetGroupDisplayNameRequest{}
	err:=c.ShouldBindJSON(&setGroupDisplayNameRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code": 200,
	})
}

//{"code":200}
func JoinGroup(c *gin.Context) {
	var quitGroupRequest  =&dto.QuitGroupRequest{}
	err:=c.ShouldBindJSON(&quitGroupRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	c.JSON(200, gin.H{
		"code": 200,
	})
}


func GetDefaultConversation(c *gin.Context) {
		c.JSON(200, gin.H{
		"code":    200,
		"result":  []map[string]interface{}{
			map[string]interface{}{
				"id":"i3gRfA1ml",
				"name":"nihaoa",
				"type":"group",
				"portraitUri":"",
				"memberCount":7,"maxMemberCount":7},
			},
	})
}


func GetUserInfos(c *gin.Context) {
	var paramRequest =&dto.ParamRequest{}
	err:=c.ShouldBind(&paramRequest)
	if err!=nil{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}
	params :=""
	if params!=""{

	}
	c.JSON(200, gin.H{
		"code":    200,
		"result":  []map[string]interface{}{
			map[string]interface{}{
				"id":"i3gRfA1ml",
				"nickname":"nihaoa",
				"portraitUri":""},
			},
	})
}


func GetQiNiuToken(c *gin.Context) {
		c.JSON(200, gin.H{
		"code":    200,
		"result":  []map[string]interface{}{
			map[string]interface{}{
				"target": "i3gRfA1ml",
				"domain": "nihaoa",
				"token":  "",
			},
		},
	})
}


func GetClientVersion(c *gin.Context) {
	c.JSON(200, gin.H{
		"ios":   map[string]interface{}{
			"version":"i3gRfA1ml",
			"build":"nihaoa",
			"url":"",
		},
		"android": map[string]interface{}{
			"version":"i3gRfA1ml",
			"url":"",
		},
	})
}


func SyncTotalData(c *gin.Context)  {
	version := c.Param("version")
	if version==""{
		c.JSON(200, gin.H{
			"code":   400,
		})
		return
	}

	c.JSON(200, gin.H{
		"version":1472796005679,
		"user":map[string]interface{}{
			"id":"i3gRfA1ml",
			"nickname":"nihaoa",
			"portraitUri":""},
		"blacklist":nil,
		"friends":[]map[string]interface{}{
			map[string]interface{}{"friendId":16,
				"displayName":"","status":20,
				"timestamp":1471237611667},
		},
		"groups":[]map[string]interface{}{
			map[string]interface{}{"displayName":"",
				"role":1,"isDeleted":false,
				"group":map[string]interface{}{"id":472,
					"name":"大融云",
					"portraitUri":"http://7xogjk.com1.z0.glb.clouddn.com/Tp6nLyUKX1466570117209114014",
					"timestamp":1472796005679}},
		},
		"group_members":[]map[string]interface{}{
			map[string]interface{}{"groupId":472,"memberId":4725,
				"displayName":"","role":1,"isDeleted":false,
				"timestamp":1471256883504,
				"user":map[string]interface{}{"nickname":"李峰",
					"portraitUri":"http://7xogjk.com1.z0.glb.clouddn.com/FhZKRkT7DInMbrqCSKX6NqIqHbEP"}},
		},
	})
}

func DownloadPic(c *gin.Context)  {
	url := ""
	if url!=""{

	}

}
