package example

//import cycle not allowed
//致使两个包相互依赖，从而出现 import cycle not allowed 这样的问题
//
import (
	//company "utility/demo"
)

//主程序 入口
func main ()  {
	//company.Init()
	Init() //undefined
	//company.Init()
}