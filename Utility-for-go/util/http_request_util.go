package util

import (
	"log"
	"net/http"
	"strconv"
	"strings"
	"regexp"
)

func GetPageAndSize(request *http.Request) (int,int) {
	url :=request.URL.Path
	reg,err:=regexp.Compile(`/\d+`)
	if err!=nil{
		log.Println(" find reg pattern faill")
		return 0,0
	}else if !reg.MatchString(url){
		log.Printf( "%s find reg match faill",url)
		return 0,0
	}else{
		strs:=reg.FindAllString(url,-1)
		if len(strs)>=2{
			println(strs[0]+"="+strs[1])
		}else{
			log.Printf( "%s find reg match page or  size faill ,len %d %s",url,len(strs),strs[0])
		}
		page,err :=strconv.Atoi(strings.Replace(strs[0],"/","",-1))

		if err!=nil{
			log.Printf( "%s find reg match page faill",url)
			return 0,0
		}
		size,err :=strconv.Atoi(strings.Replace(strs[1],"/","",-1))
		if err!=nil{
			log.Printf( "%s find reg match size faill",url)
			return page,0
		}
		return page,size
	}
	return 0,0
}
