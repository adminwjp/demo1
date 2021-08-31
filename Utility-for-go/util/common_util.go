package util

import (
	"strings"
	"time"
	"fmt"
	"runtime"
	//"os/exec"
)

func GetFileExtension(file string) string {
	if file == "" {
		return ""
	}
	var strs = strings.Split(file, ".")
	if len(strs) > 1 {
		return strs[len(strs)-1]
	}
	return ""
}

func IndexOfByString(str string, ch byte) int {
	if str == "" {
		return -1
	}
	var chars = []byte(str)
	for i := range chars {
		if str[i] == ch {
			return int(i)
		}
	}
	return -1
}
func ReplaceString(str string) string {
	str = strings.ReplaceAll(str, "\n", "")
	str = strings.ReplaceAll(str, "\r", "")
	str = strings.ReplaceAll(str, "\t", "")
	str = strings.ReplaceAll(str, " ", "")
	return str
}

func SubstringByString(str string, index int) string {
	if str == "" {
		return ""
	}
	var chars = []byte(str)
	var ll = len(str)
	var l = ll
	if ll > index {
		l = index
	} else {
		l = ll
	}
	var temps = make([]byte, l)
	for i := 0; i < l; i++ {
		temps[i] = chars[i]
	}
	return string(temps)
}

var DayUnixTime int64 = 24 * 60 * 60

//time.Utc -8h
var EightHours int64 = 8 * 60 * 60

//yyyy-MM-10 hh:mm:ss
//day 1 return  yyyy-MM-11 hh:mm:ss
func GetTimeByAddDay(day int) time.Time {
	t := time.Now()
	return time.Date(t.Year(), t.Month(), t.Day()+day, t.Hour(), t.Minute(), t.Second(), 0, time.Local)
}

//yyyy-MM-dd hh:mm:ss
func GetTimeToYMD(day int) time.Time {
	t := time.Now()
	return time.Date(t.Year(), t.Month(), t.Day()+day, t.Hour(), t.Minute(), t.Second(), 0, time.Local)
}

//yyyy-10-dd hh:mm:ss
//month 1 return  yyyy-11-dd hh:mm:ss
func GetTimeByAddMonth(month int) time.Time {
	if month > 12 {
		return time.Now()
	}
	t := time.Now()
	m := int(t.Month()) + month
	y := t.Year()
	if m > 12 {
		y += 1
		m -= 12
	}
	return time.Date(y, time.Month(m), t.Day(), t.Hour(), t.Minute(), t.Second(), 0, time.Local)
}

//yyyy-mm-dd 00:00:00:000
func GetTeimByStartYMD() time.Time {
	t := time.Now()
	return time.Date(t.Year(), t.Month(), t.Day(), 0, 0, 0, 0, time.Local)
}

//yyyy-mm-dd 23:59:59:999
func GetTeimByEndYMD() time.Time {
	t := time.Now()
	return time.Date(t.Year(), t.Month(), t.Day(), 23, 59, 59, 999, time.Local)
}

func GetPlatform(){
	fmt.Println("Go runs on")
	os:=runtime.GOOS
	fmt.Printf("%S.\n",os)
	switch os {
	case "darwin":
		fmt.Println("OS x.")
	case "linux":
		fmt.Println("Linux.")
	case "window":
		fmt.Println("Window.")
	default:
		//其他系统
		fmt.Println("Unkow System.")
		}
}
//https://blog.csdn.net/weixin_44282540/article/details/109066976
func GetCmd(commandName string,params []string ) {
	// 通过exec.Command函数执行命令或者shell
	// 第一个参数是命令路径，当然如果PATH路径可以搜索到命令，可以不用输入完整的路径
	// 第二到第N个参数是命令的参数
	// 下面语句等价于执行命令: ls -l /var/
	//cmd := exec.Command("/bin/ls", "-l", "/var/")
	// 执行命令，并返回结果
	//output,err := cmd.Output()
	//err := cmd.Run() //执行命令，返回命令是否执行成功
	//if err != nil {
	//	panic(err)
	//}
	// 因为结果是字节数组，需要转换成string
	//fmt.Println(string(output))

}