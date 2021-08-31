package ctls

import (
	"github.com/gin-gonic/gin"
)

type UserCtl struct {
	BaseCtl
}

//AllowAnonymous
func (userCtl UserCtl) WxLogin(c *gin.Context)  {
	if userCtl.OnActionExecution(c){
		return
	}

}

//AllowAnonymous
func (userCtl UserCtl) Login(c *gin.Context)  {
	if userCtl.OnActionExecution(c){
		return
	}

}

func (userCtl UserCtl) GetAdminUsers(c *gin.Context)  {
	if userCtl.OnActionExecution(c){
		return
	}

}