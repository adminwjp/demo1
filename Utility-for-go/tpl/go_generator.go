package tpl

import (
	"shop/util"
	"strings"
)

type DatabaseModel struct {
	Id int64 `json:"id"`
	Name string `json:"name"`
	ProgramName string `json:"program_name"`
	Remark string `json:"remark"`
	TableModels []TableModel `json:"table_models"`
}

type TableModel struct{
	Id int64 `json:"id"`
	ClassName string `json:"class_name"`
	TablemName string `json:"tablem_name"`
	Remark string `json:"remark"`
	Title string `json:"title"`
	DatabaseId int64 `json:"database_id"`
	Database DatabaseModel `json:"database"`
	ColumnModels []ColumnModel `json:"column_models"`
	}
type ColumnModel struct {
	Id int64 `json:"id"`
	CloumName string `json:"cloum_name"`
	PropertyName string `json:"property_name"`
	Remark string `json:"remark"`
	CsharepPropertyType string `json:"csharep_property_type"`
	Length int64 `json:"length"`
	Title string `json:"title"`
	TableId int64 `json:"table_id"`
	Table TableModel `json:"table"`
}
type TemplateApi struct {
	Status bool  `json:"status"`
	Code int32 `json:"code"`
	Data []DatabaseModel `json:"data"`
}
type GoGenerator struct {
	Data []DatabaseModel
}

func (generator GoGenerator) GoEntity()  {
	str:=""
	for i := 0; i < len(generator.Data); i++ {
		db:=generator.Data[i]
		if db.Name=="Shop"{
			str+="package entity\r\n"
			for j := 0; j < len(db.TableModels); j++ {
				tab:=db.TableModels[j]
				str+="type "+tab.ClassName+" struct {\r\n"
				for k := 0; k < len(tab.ColumnModels); k++ {
					col:=tab.ColumnModels[k]
					na:=col.CloumName
					id:="gorm:\"column:"+na+";"
					if col.Length>0{
						id+="size:"+string(col.Length)
					}
					if col.PropertyName=="Id"{
						//id+=";primary_key\""
						id="gorm:\"primary_key\""
					}else{
						id+="\""
					}
					str+=col.PropertyName+"         int64  `json:\""+na+"\" form:\""+na+"\"  xml:\""+na+"\" "+id+" `\r\n"
				}
				str+="}\r\n"
				str+="func ("+tab.ClassName+") TableName() string {\r\n"
				str+="\treturn \""+tab.TablemName+"\"\r\n"
				str+="}\r\n"
			}
		}
	}
	//log.Println(str)
	if str!=""{
		// write file
		util.Write("E:/work/shop/Shop-for-gin/entity/shop.go",[]byte(str))
	}
}

func (generator GoGenerator) GoDto()  {
	str:=""
	for i := 0; i < len(generator.Data); i++ {
		db:=generator.Data[i]
		if db.Name=="Shop"{
			for j := 0; j < len(db.TableModels); j++ {
				tab:=db.TableModels[j]
				cls :="type {.ClassName} struct {\r\n"
				for k := 0; k < len(tab.ColumnModels); k++ {
					col:=tab.ColumnModels[k]
					na:=col.CloumName
					cls+=col.PropertyName+"         int64  `json:\""+na+"\" form:\""+na+"\"  xml:\""+na+"\"  `\r\n"
				}
				cls+="}\r\n"
				str+=strings.Replace(cls,"{.ClassName}","Create"+tab.ClassName+"Input",-1)
				str+=strings.Replace(cls,"{.ClassName}","Update"+tab.ClassName+"Input",-1)
				str+=strings.Replace(cls,"{.ClassName}","Query"+tab.ClassName+"Input",-1)
				str+=strings.Replace(cls,"{.ClassName}","Query"+tab.ClassName+"Output",-1)
				// write file

				//str=""

			}
		}
	}
	if str!=""{
		// write file
		util.Write("E:/work/shop/Shop-for-gin/dto/shop_dto.go",[]byte(str))
	}
}