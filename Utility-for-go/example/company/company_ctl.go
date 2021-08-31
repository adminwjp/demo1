package company

import (
	"encoding/json"
	"io"
	"log"
	"net/http"
	"regexp"
	"strconv"
	"strings"
	"utility/dto"
)

type Admintl struct {

}

func (Admintl) Insert(writer http.ResponseWriter,request *http.Request){

	io.WriteString(writer,"test")
}

func (Admintl) Update(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (Admintl) Delete(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (Admintl) Find(writer http.ResponseWriter,request *http.Request){
	Find(writer,request,func(size int,page int,res dto.ResponseDto){
		var datas [] AdminEntity
		GormUtil.Db.
			Order("create_date",true).Limit(size).Offset((page-1)*size).Find(&datas)
		res=dto.Response.CreateSuccess()
		res.Data=map[string]interface{}{"data":datas,
			"result":map[string]interface{}{"records":len(datas)}}
		write(writer,res)
	})
}

type AboutCtl struct {

}

func (AboutCtl) Insert(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (AboutCtl) Update(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (AboutCtl) Delete(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (AboutCtl) Find(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

type CategoryCtl struct {

}

func (CategoryCtl) Insert(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (CategoryCtl) Update(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (CategoryCtl) Delete(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (CategoryCtl) Find(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

type CompanyCtl struct {

}

func (CompanyCtl) Insert(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (CompanyCtl) Update(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (CompanyCtl) Delete(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (CompanyCtl) Find(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

type ImageCtl struct {

}

func (ImageCtl) Insert(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (ImageCtl) Update(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (ImageCtl) Delete(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (ImageCtl) Find(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

type MainCtl struct {

}

func (MainCtl) Insert(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (MainCtl) Update(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (MainCtl) Delete(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (MainCtl) Find(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

type RelationCtl struct {

}

func (RelationCtl) Insert(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (RelationCtl) Update(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (RelationCtl) Delete(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (RelationCtl) Find(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

type TeamCtl struct {

}

func (TeamCtl) Insert(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (TeamCtl) Update(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (TeamCtl) Delete(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (TeamCtl) Find(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

type NavCtl struct {

}

func (NavCtl) Insert(writer http.ResponseWriter,request *http.Request){

	io.WriteString(writer,"test")
}

func (NavCtl) Update(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}

func (NavCtl) Delete(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}
func (NavCtl) Find(writer http.ResponseWriter,request *http.Request){
	Find(writer,request,func(page int,size int,res dto.ResponseDto){
		log.Println("nav find ")
		var navs [] CategoryEntity
		GormUtil.Db.Where("flag = "+strconv.Itoa(Nav)).
		//model.GormUtil.Db.Where("flag = ?",model.CategoryEntity{Flag: model.Nav}).
			Order("create_date desc").Limit(size).Offset((page-1)*size).Find(&navs)
		res=dto.Response.CreateSuccess()
		//util.MappArray(&navs,&mavDtos)
		l:=len(navs)
		res.Data=map[string]interface{}{"data":navs,
			"result":map[string]interface{}{"records":l}}
		log.Println("nav find finish")
		write(writer,res)
	})
}
func Find(writer http.ResponseWriter,request *http.Request,bll func(p int,s int,r dto.ResponseDto) ){
	url :=request.URL.Path
	var res dto.ResponseDto
	reg,err:=regexp.Compile(`/\d+`)
	if err!=nil{
		error(writer)
		log.Println(" find reg pattern faill")
		return
	}else if !reg.MatchString(url){
		log.Printf( "%s find reg match faill",url)
		error(writer)
		return
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
			error(writer)
			return
		}
		size,err :=strconv.Atoi(strings.Replace(strs[1],"/","",-1))
		if err!=nil{
			log.Printf( "%s find reg match size faill",url)
			error(writer)
			return
		}
		bll(page,size,res)
	}

}

func  write(writer http.ResponseWriter,res dto.ResponseDto)  {
	j,err:=json.Marshal(&res)
	if err!=nil{
		log.Fatal("find parse json faill")
		error(writer)
		return
	}
	io.WriteString(writer,string(j))
}

func error(writer http.ResponseWriter)  {
	io.WriteString(writer,"{\"success\":false,\"msg\":\"error\",\"code\":500}")
}

