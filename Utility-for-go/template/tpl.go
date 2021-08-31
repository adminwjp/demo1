package template


//aspnet core model 注解 
AspNetCorePrefix := "Microsoft.AspNetCore.Mvc"

const(
	AspNetCoreNone=0
	AspNetCoreFromForm
	AspNetCoreFromBody
	AspNetCoreFromQuery
	AspNetCoreFromHeader
	AspNetCoreBindProperty
	AspNetCore
)

func getAspNetCoreProAttr(flag int) string{
	switch(flag){
		case AspNetCoreFromForm: return "FromForm"
		case AspNetCoreFromBody: return "FromBody"
		case AspNetCoreFromQuery: return "FromQuery"
		case AspNetCoreFromHeader: return "FromHeader"
		case AspNetCoreBindProperty: return "BindProperty"
		case AspNetCoreFromForm:
		default:
			return ""
	}
}

//dapper model 注解
DapperNamespace :="Dapper"

const(
	IgnoreInsert=0
	IgnoreSelect
	IgnoreUpdate
	NotMapped
	Key
)

func getDapperColumnAttr(flag int)string{
	switch(flag){
		case IgnoreInsert: return "        ["+DapperNamespace+".IgnoreInsert]\r\n"
		case IgnoreSelect: return "        ["+DapperNamespace+".IgnoreSelect]\r\n"
		case IgnoreUpdate: return "        ["+DapperNamespace+".IgnoreUpdate]\r\n"
		case NotMapped: return "        ["+DapperNamespace+".NotMapped]\r\n"
		case Key: return "        ["+DapperNamespace+".Key]\r\n"
		default:
			return ""
	}
}

func getDapperClasAttr(table string)string{
	return "    ["+DapperNamespace+".Table(\""+table+"\")]\r\n"
}

//ef model 注解 
EfAnnotationPrefix := "System.ComponentModel.DataAnnotations"

func getEfClasAttr(table string)string{
	str:= "        ["+EfAnnotationPrefix+".Schema.Table(\""+table+"\")]\r\n"
	return str
}

func getEfIdAttr(identity bool)string{
	str:= "        ["+EfAnnotationPrefix+".Key]\r\n"
	if identity{
		str+="        ["+EfAnnotationPrefix+".Schema.DatabaseGenerated("+EfAnnotationPrefix+".Schema.DatabaseGeneratedOption.Identity)]\r\n"
	}
	//str+=getEfColumnAttr("",0)
	return str
}


func getEfColumnAttr(column string,length int)string{
	str:= "        ["+EfAnnotationPrefix+".Schema.Column(\""+column+"\")]\r\n"
	if length>0 {
		str+="        ["+EfAnnotationPrefix+".StringLength("+length+")]\r\n"
	}
	return str
}

func getEfNotMappedColumnAttr()string{
	str:= "        ["+EfAnnotationPrefix+".Schema.NotMapped]\r\n"
	return str
}

func getEfForeignKeyAttr(column string)string{
	str:= "        ["+EfAnnotationPrefix+".Schema.ForeignKey(\""+column+"\")]\r\n"
	return str
}

//nhibernate model 注解 
NHibernatePrefix := "[NHibernate.Mapping.Attributes"

func getNHibernateClasAttr(table string)string{
	prefix:=""
	if table!=""{
		refix = $"Table = \""+table+"\","
	}
	str:= "    ["+NHibernatePrefix+".Class("+prefix+" Lazy=false)]\r\n"
	return str
}

func getEnterpriseLibrary(){

}

func getDapper(){

}

func getEf(){

}

func getNHibernate(){

}

func getWcf(){

}

func getRemote(){

}


types :=make(map[string]string,50){
	"string":"string","int":"int","int?":"int?","bool":"bool","bool?":"bool?",
	"System.DateTime?":"System.DateTime?","System.DateTime":"System.DateTime"
}
