package util

import (
	"strings"
	"time"
)

func  GetFileExtension(file string)  string {
	if file==""{
		return ""
	}
	var strs=strings.Split(file,".")
	if len(strs)>1{
		return strs[len(strs)-1]
	}
	return ""
}

func IndexOfByString(str string,ch byte)int  {
	if str==""{
		return  -1
	}
	var chars=[]byte(str)
	for i:=range chars{
		if str[i]==ch{
			return int(i)
		}
	}
	return  -1
}

func SubstringByString(str string,index int)string  {
	if str==""{
		return  ""
	}
	var chars=[]byte(str)
	var ll=len(str)
	var l=ll
	if ll>index{
		l=index
	}else {
		l=ll
	}
	var temps=make([]byte,l)
	for i:=0;i<l;i++{
		temps[i]=chars[i]
	}
	return  string(temps)
}

var DayUnixTime int64=24*60*60
//time.Utc -8h
var EightHours int64=8*60*60
//yyyy-MM-10 hh:mm:ss
//day 1 return  yyyy-MM-11 hh:mm:ss
func GetTimeByAddDay(day int)time.Time  {
	t:=time.Now()
	return time.Date(t.Year(),t.Month(),t.Day()+day,t.Hour(),t.Minute(),t.Second(),0,time.Local)
}

//yyyy-MM-dd hh:mm:ss
func GetTimeToYMD(day int)time.Time  {
	t:=time.Now()
	return time.Date(t.Year(),t.Month(),t.Day()+day,t.Hour(),t.Minute(),t.Second(),0,time.Local)
}

//yyyy-10-dd hh:mm:ss
//month 1 return  yyyy-11-dd hh:mm:ss
func GetTimeByAddMonth(month int)time.Time  {
	if month>12{
		return  time.Now()
	}
	t:=time.Now()
	m:=int(t.Month())+month
	y:=t.Year()
	if m>12{
		y+=1
		m-=12
	}
	return time.Date(y,time.Month(m),t.Day(),t.Hour(),t.Minute(),t.Second(),0,time.Local)
}

//yyyy-mm-dd 00:00:00:000
func GetTeimByStartYMD()  time.Time{
	t:=time.Now()
	return time.Date(t.Year(),t.Month(),t.Day(),0,0,0,0,time.Local)
}

//yyyy-mm-dd 23:59:59:999
func GetTeimByEndYMD()  time.Time{
	t:=time.Now()
	return time.Date(t.Year(),t.Month(),t.Day(),23,59,59,999,time.Local)
}
