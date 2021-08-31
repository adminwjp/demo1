package tests

import (
	"encoding/json"
	"io/ioutil"
	"os"
	"strings"
	"testing"
	"utility/example/news"
	"utility/splider"
)
// go test  -count=1 -v

var path string

func TestWYNews(t *testing.T){

}

func aTestNews(t *testing.T)  {
	path,err:=os.Getwd()
	if err!=nil{
		t.Log("get path fail,error :"+err.Error())
		return
	}
	path=strings.Replace(path,"tests","",1)
	buffer,err:=ioutil.ReadFile(path+"\\config\\catalog.json")
	//buffer,err:=ioutil.ReadFile("config/catalog.json")
	if err!=nil{
		t.Log("read json fail,error :"+err.Error())
		return
	}
	var catalogs *news.Catalogs
	err=json.Unmarshal(buffer,&catalogs)
	if err!=nil{
		t.Log("parse json to object fail,error :"+err.Error())
		return
	}
	splider.TestNews(catalogs)

}



