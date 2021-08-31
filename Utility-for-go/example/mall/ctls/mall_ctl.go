package ctls

import (
	"encoding/json"
	"news/models"
	"github.com/gin-gonic/gin"
	"github.com/jinzhu/gorm"
	"io"
	"log"
	"strconv"
	"strings"
)
type AddressinforCtl struct {
	BaseCtl
}
//https://www.cnblogs.com/zisefeizhu/p/12788060.html
//https://www.jianshu.com/p/98965b3ff638/
func (addressinforCtl AddressinforCtl) Insert(c *gin.Context)  {
	addressinforCtl.InsertOrUpdate(c,models.Addressinfor{},
	true,true)
}
func success() map[string]interface{} {
	var res map[string]interface{}= map[string]interface{}{
		"code":200,"status":true,"msg":"success",
	}
	return res
}
func fail() map[string]interface{} {
	var res map[string]interface{}= map[string]interface{}{
		"code":400,"status":false,"msg":"fail",
	}
	return res
}
func error() map[string]interface{} {
	var res map[string]interface{}= map[string]interface{}{
		"code":500,"status":false,"msg":"error",
	}
	return res
}
func writeErr(c *gin.Context,res map[string]interface{})  {
	j,err:=json.Marshal(res)
	if err!=nil{
		c.Writer.WriteString("error")
		return
	}
	c.Writer.WriteString(string(j))
}
func (baseCtl BaseCtl) InsertOrUpdate(c *gin.Context,
	model interface{},insert bool,address bool)  {
	if strings.ToLower(c.Request.Method)!="post"{
		writeErr(c,error())
		return
	}
	if baseCtl.OnActionExecution(c){
		return
	}
	var suc bool=false
	if c.ShouldBindJSON(&model)!=nil{
		db:= Db
		tx:=db.Begin()
		if address &&model.(models.Addressinfor).Default{
			db:=db.Model(models.Addressinfor{})
			db=db.Where("default",1)
			db=db.Update("default",0)
			if db!=nil{
				tx.Rollback()
				writeErr(c,error())
				return
			}
		}
		if insert{
			suc=db.Create(model)!=nil
		}else{
			suc=db.Updates(model)!=nil
		}
		if !suc{
			tx.Rollback()
		}else{
			tx.Commit()
		}
	}
	if suc{
		writeErr(c,success())
	}else{
		writeErr(c,fail())
	}
}


func (addressinforCtl AddressinforCtl) InsertMany(c *gin.Context)  {
	if addressinforCtl.OnActionExecution(c){
		return
	}
	if strings.ToLower(c.Request.Method)!="post"{
		writeErr(c,error())
		return
	}
	if addressinforCtl.OnActionExecution(c){
		return
	}
	reader,err:=c.Request.GetBody()
	if err!=nil{
		writeErr(c,error())
		return
	}
	buffer,err:=io.ReadAll(reader)
	if err!=nil{
		writeErr(c,error())
		return
	}
	var addressinforList AddressinforList
	err=json.Unmarshal(buffer,&addressinforList)
	if err!=nil{
		writeErr(c,error())
		return
	}
	var suc bool=false
	//suc=GormUtil.Db.CreateInBatches(addressinforList.Addresses,len(addressinforList.Addresses))
	db:= Db
    tx:=db.Begin()
	for i:=0;i<len(addressinforList.Addresses);i++{
		if addressinforList.Addresses[i].Default{
			db:=db.Model(models.Addressinfor{})
			db=db.Where("default",1)
			db=db.Update("default",0)
			if db!=nil{
				tx.Rollback()
				writeErr(c,fail())
				return
			}
			break
		}
	}
    for i:=0;i<len(addressinforList.Addresses);i++{
		db.Create(addressinforList.Addresses[i])
    	if db!=nil{
			suc=false
			break
		}
	}
	if suc{
		tx.Commit()
		if tx!=nil{
			writeErr(c,fail())
			return
		}
		writeErr(c,success())
	}else{
		tx.Rollback()
		writeErr(c,fail())
	}
}

type AddressinforList struct {
	Addresses []*models.Addressinfor `json:"addresses"`
}

func (addressinforCtl AddressinforCtl) Update(c *gin.Context)  {
	addressinforCtl.InsertOrUpdate(c,models.Addressinfor{},
		false,true)

}

func (addressinforCtl UserCtl) Delete(c *gin.Context)  {
	if addressinforCtl.OnActionExecution(c){
		return
	}

}

func (addressinforCtl UserCtl) GetList(c *gin.Context)  {
	var addresses []*models.Addressinfor
	addressinforCtl.getList(c,models.Addressinfor{}, func(db *gorm.DB)interface {}{
		db=db.Scan(&addresses)
		return addresses
	},false)
}
//unsupported destination model new()
// no such table
func (baseCtl BaseCtl) getList(c *gin.Context,model interface{},
datas func(db *gorm.DB) interface{} ,all bool)  {
	/*if baseCtl.OnActionExecution(c){
		return
	}*/
	//c.GetInt("size") 0
	var page,size int =1,10
	p,exists:=c.GetQuery("page")
	s,exists:=c.GetQuery("size")
	if exists{
		//明明存在 还 报 红线
		pa,err:=strconv.Atoi(p)
		page=pa
		if err!=nil{
			page=1
		}
	}
	if exists{
		si,err:=strconv.Atoi(s)
		size=si
		if err!=nil{
			size=10
		}
	}
	db:= Db
	db=db.Model(model)
	if !all{
		//no such table errror  db.Model(model).Offset((page-1)*size)
		db=db.Offset((page-1)*size)
		db=db.Limit(size)
		log.Println("page or size get suc")
	}
	data :=datas(db)
	if db.Error!=nil{
		log.Println("getlist fail"+db.Error.Error())
	}
	res:=success()
	res["data"]=data
	writeErr(c,res)
}

type CartinforCtl struct {
	BaseCtl
}

func (cartinforCtl CartinforCtl) Insert(c *gin.Context)  {
	cartinforCtl.InsertOrUpdate(c,models.Cartinfor{},
		true,false)
}

func (cartinforCtl CartinforCtl) InsertMany(c *gin.Context)  {
	if cartinforCtl.OnActionExecution(c){
		return
	}

}

func (cartinforCtl CartinforCtl) Update(c *gin.Context)  {
	cartinforCtl.InsertOrUpdate(c,models.Cartinfor{},
		false,false)
}

func (cartinforCtl CartinforCtl) Delete(c *gin.Context)  {
	if cartinforCtl.OnActionExecution(c){
		return
	}

}

func (cartinforCtl CartinforCtl) GetList(c *gin.Context)  {
	var cartinfors []*models.Cartinfor
	cartinforCtl.getList(c,new(models.Cartinfor), func(db *gorm.DB) interface{}{
		db=db.Scan(&cartinfors)
		return cartinfors
	},false)
}

func (cartinforCtl CartinforCtl) Clear(c *gin.Context)  {
	if cartinforCtl.OnActionExecution(c){
		return
	}
	db:= Db
	db.Model(new(models.Cartinfor)).Delete(models.Cartinfor{Id: 1})
   if db!=nil {
	   writeErr(c,error())
	   return
   }
	writeErr(c,success())
}

type CataloginforCtl struct {
	BaseCtl
}

func (cataloginforCtl CataloginforCtl) Insert(c *gin.Context)  {
	cataloginforCtl.InsertOrUpdate(c,models.Cataloginfor{},
		true,false)
}

func (cataloginforCtl CataloginforCtl) InsertMany(c *gin.Context)  {
	if cataloginforCtl.OnActionExecution(c){
		return
	}
	var cataloginforList CataloginforList
	if c.ShouldBindJSON(&cataloginforList)!=nil{
		writeErr(c,error())
		return
	}
	db:= Db
	tx:=db.Begin()
	if cursion_add_catalog(db,cataloginforList.Catalogs,nil){
		tx.Commit()
		if tx!=nil{
			writeErr(c,success())
			return
		}
	}
	tx.Rollback()
	writeErr(c,fail())
}

type CataloginforList struct {
	Catalogs []*models.Cataloginfor `json:"catalogs"`
}
func cursion_add_catalog(db *gorm.DB,datas []*models.Cataloginfor,
	p *models.Cataloginfor) bool{
	if len(datas) > 0{
		for i:= range  datas{
			x:=datas[i]
			c:=&models.Cataloginfor{Id:x.Id,
				Name: x.Name,Picture: x.Picture}
			c.Id=0
			//c.Pid=0
			c.Parent=p
			db.Create(c)
			if db!=nil{
				return false
			}
			if !cursion_add_catalog(db,x.Children,c){
				return false
			}
		}
	}
	return  true
}


func (cataloginforCtl CataloginforCtl) Update(c *gin.Context)  {
	cataloginforCtl.InsertOrUpdate(c,models.Cataloginfor{},
		false,false)
}

func (cataloginforCtl CataloginforCtl) Delete(c *gin.Context)  {
	if cataloginforCtl.OnActionExecution(c){
		return
	}

}

func (cataloginforCtl CataloginforCtl) GetList(c *gin.Context)  {
	var cataloginfors []models.Cataloginfor
	cataloginforCtl.getList(c,&models.Cataloginfor{}, func(db *gorm.DB)interface {}{
		db=db.Scan(&cataloginfors)
		return cataloginfors
	},false)
}

func (cataloginforCtl CataloginforCtl) GetAll(c *gin.Context)  {
	var cataloginfors []*models.Cataloginfor
	cataloginforCtl.getList(c,&models.Cataloginfor{}, func(db *gorm.DB)interface {}{
		db=db.Scan(&cataloginfors)
		return cataloginfors
	},true)
}