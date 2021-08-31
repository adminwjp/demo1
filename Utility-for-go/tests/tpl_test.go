package tests

import (
	"encoding/json"
	"testing"
)
import "utility/util"
import "utility/tpl"

func  TestEntity(t *testing.T ) {
	 api:="http://127.0.0.1:5000/api/v1/admin/database/find/1/10"
	 res:=util.PostJson(api,"","{}")
	 t.Logf(res)
	 var resApi *tpl.TemplateApi
	 err:=json.Unmarshal([]byte(res),&resApi)
	 if err!=nil{
	 	t.Logf(err.Error())
		 return
	 }
	 gener:=tpl.GoGenerator{}
	 gener.Data=resApi.Data
	 gener.GoEntity()
	 gener.GoDto()
}
