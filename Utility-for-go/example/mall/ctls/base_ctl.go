package ctls

import (
	"encoding/json"
	"news/models"
	"github.com/gin-gonic/gin"
	"io/ioutil"
	"strings"
)

type BaseCtl struct {
	SourceType int
	ReqParams map[string]string
	TOKEN_NAME string
	Sign string
	AllowAnonymouUrls []string
}

func NewBaseCtl() *BaseCtl {
	return &BaseCtl{TOKEN_NAME: "token",Sign:"sign"}
}

func (baseCtl BaseCtl) OnActionExecution(c *gin.Context) bool{
	userAgent:=c.Request.UserAgent()
	if strings.Contains(userAgent,"MicroMessenger"){
		baseCtl.SourceType=models.WeChatApp
	}else if strings.Contains(userAgent,"iPhone") || strings.Contains(userAgent,"iPod")||strings.Contains(userAgent,"iPad"){
		baseCtl.SourceType=models.IOS
	}else if strings.Contains(userAgent,"Android"){
		baseCtl.SourceType=models.Android
	}else{
		baseCtl.SourceType=models.Web
	}
	return false
	vals:=c.Request.URL.Query()
	for k,v:=range vals{
		baseCtl.ReqParams[k]=v[0]
	}
	if c.Request.Form!=nil{
		for k,v:=range c.Request.Form{
			baseCtl.ReqParams[k]=v[0]
		}
	}
	reader,err:=c.Request.GetBody()
	if err==nil{
		buffer,err:=ioutil.ReadAll(reader)
		if err==nil{
		   var post	map[string]string
			err=json.Unmarshal(buffer,&post)
			if err==nil{
				for k,v:=range post{
					baseCtl.ReqParams[k]=v
				}
			}
		}
	}

	if baseCtl.SourceType== models.Unknown{
		c.JSON(models.Unauthorized,"请设置User-Agent请求头: 如:iPhone 或者 Android 或则web")
		return  true
	}else{
		token:=""
		sign:=""
		str,ok:=baseCtl.ReqParams[baseCtl.TOKEN_NAME]
		if ok{
			token=str
		}
		str,ok=baseCtl.ReqParams[baseCtl.Sign]
		if ok{
			sign=str
		}
		if token==""{
			c.JSON(models.Unauthorized,"token is empty you are error！")
			return  true
		}else if sign==""{
			c.JSON(models.Unauthorized,"sign is empty you are error！")
			return  true
		}else{
			allow:=false
			for i:=range baseCtl.AllowAnonymouUrls{
				if strings.Contains(c.Request.URL.Path,baseCtl.AllowAnonymouUrls[i]){
					allow=true
				}
			}
			if allow{
				return  false
			}else{

			}
		}
		return  false
	}
}

func (baseCtl BaseCtl) OnActionExecuted(c *gin.Context) bool{
	return  false
}