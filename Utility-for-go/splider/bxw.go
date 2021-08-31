package splider

import (
	"utility/util"
)
//百姓网
type BaiXingWang struct {

}

func  Http(param map[string]interface{})  {
	 url:="https://wuhan.baixing.com/"
	 httpEntity:=util.HttpEntity{Url: url}
	 util.Http(httpEntity)
}