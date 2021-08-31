package util

import "sync"

var HttpInstance *httpUtil
var SeurityInstance *seurityUtl

var ConfigInstance *configUtil

//空方法 加载 改包 不然 用不了 自定义包
func Empty() {

}

//懒汉式  自带 初始化？ getInstane
var oSing sync.Once

func getInstane() *httpUtil {
	if HttpInstance == nil {
		//Do函数里面的函数只有在第一次才会被调用
		oSing.Do(func() {
			HttpInstance = &httpUtil{}
		})
	}
	return HttpInstance
}

// 饿汉式 自带 初始化？ init
func init() {
	SeurityInstance = &seurityUtl{BASE64Table: BASE64Table, DesKey: []byte(DesKey), AesKey: AesKey}
	ConfigInstance = &configUtil{}
}

//懒汉式  自带 初始化？ getInstane
/*
var oSing sync.Once
func  getInstane() *seurityUtl  {
	if SeurityInstance==nil{
		//Do函数里面的函数只有在第一次才会被调用
		oSing.Do(func() {
			SeurityInstance=&seurityUtl{BASE64Table:BASE64Table,DesKey:[]byte(DesKey),AesKey:AesKey}
		})
	}
	return  SeurityInstance
}
*/
