package template

import "strings"

type CsharpHelper struct {
}

//读取 AspNetCoreCshtml 模板 数据 AspNetCoreCshtml 语法
func (CsharpHelper) ReadAspNetCoreCshtml(html string) {

}

//读取csharp  数据  csharp 语法
func (CsharpHelper) ReadCsharp(csharp string) {

}
type  TableNameResolver struct {
}

func ToEntity()  {
	rns:="#define NHibernate\n#define Dapper\n#define EF\n#define Annotation\nusing D = Dapper;"
	println(rns)
}

func  GetNetCore()string  {
	return ""
}
type AnnotationFactory interface {


}
type AspNetCoreAnnotationFactory struct {
}
var AspNetCoreAnnotationFactoryPrefix string="Microsoft.AspNetCore.Mvc"

func (aspNetCore AspNetCoreAnnotationFactory) ProAttr(pro PropertyDto) string {
	if pro.PropertyName == pro.ColumnName {
		return ""
	}
	var p  = ""
	switch pro.RequestFlag {
	case 0:
		break
	case 1:
		p = "FromForm"
		break
	case 2:
		p = "FromBody"
		break
	case 3:
		p = "FromQuery"
		break
	case 4:
		p = "FromHeader"
		break
	case 5:
		p = "BindProperty"
		break
	}
	if p!=""{
		p="#if "+GetNetCore()+"        ["+AspNetCoreAnnotationFactoryPrefix+"."+p+"(Name= \""+pro.ColumnName+"\")]\r\n#endif\r\n"
	}
	return p
}

type DapperAnnotationFactory struct {
	
}
var DapperNamespace="Dapper"

func (dapper DapperAnnotationFactory) ClasAttr(cla ClassDto)string  {
	if cla.TableName==cla.ClassName{
		return ""
	}
	return "    ["+DapperNamespace+".Table(\""+cla.TableName+"\")]\r\n"
}
func ( dapper DapperAnnotationFactory) IdAttr(pro PropertyDto)string  {
	var attr="        ["+DapperNamespace+".Key]\r\n"
	attr+=dapper.ColumnAttr(pro)
	return attr
}

func (dapper DapperAnnotationFactory) ColumnAttr(pro PropertyDto)string  {
	var p  = ""
	switch pro.MappFlag {
	case 0:
		p = "        ["+DapperNamespace+".NotMapped]\r\n"
		break
	case 1:
		p = "        ["+DapperNamespace+".IgnoreInsert]\r\n"
		break
	case 2:
		p = "        ["+DapperNamespace+".IgnoreSelect]\r\n"
		break
	case 3:
		p = "        ["+DapperNamespace+".IgnoreUpdate]\r\n"
		break
	case 4:
		if pro.PropertyName != pro.ColumnName {
			p = "        ["+DapperNamespace+".Column( \""+pro.ColumnName+"\")]\r\n"
		}
		break
	}
	return p
}

type EfAnnotationFactory struct {
	
}
var EfAnnotationFactoryPrefix = "System.ComponentModel.DataAnnotations"

func (ef EfAnnotationFactory) ClasAttr(cla ClassDto)string  {
	if cla.TableName==cla.ClassName{
		return ""
	}
	return "    ["+EfAnnotationFactoryPrefix+".Table(\""+cla.TableName+"\")]\r\n"
}
func ( ef EfAnnotationFactory) IdAttr(pro PropertyDto)string  {
	var attr="        ["+EfAnnotationFactoryPrefix+".Key]\r\n"
	if pro.Identity{
		attr += "        ["+EfAnnotationFactoryPrefix+".Schema.DatabaseGenerated("+EfAnnotationFactoryPrefix+".Schema.DatabaseGeneratedOption.Identity)]\r\n"
	}
	attr+=ef.ColumnAttr(pro)
	return attr
}

func (ef EfAnnotationFactory) ColumnAttr(pro PropertyDto)string  {
	var p  = ""
	switch pro.MappFlag {
	case 0:
		p = "        ["+EfAnnotationFactoryPrefix+".Schema.NotMapped]\r\n"
		break
	case 1:
	case 2:
	case 3:
		break
	case 4:
		if pro.PropertyName != pro.ColumnName {
			p = "        ["+EfAnnotationFactoryPrefix+".Schema.Column( \""+pro.ColumnName+"\")]\r\n"
		}
		if pro.Length>0{
			p += "        ["+EfAnnotationFactoryPrefix+".StringLength( "+string(pro.Length)+")]\r\n"
		}
		break
	case 5:
		p = "        ["+EfAnnotationFactoryPrefix+".Schema.ForeignKey(\""+pro.FkColumnName+"\"]\r\n"
		break
	}
	return p
}

type NhibernateAnnotationFactory struct {

}
var NhibernateAnnotationFactoryPrefix = "[NHibernate.Mapping.Attributes"

func (nh NhibernateAnnotationFactory) ClasAttr(cla ClassDto)string  {
	var prefix=""
	if cla.TableName==cla.ClassName{
		prefix = "Table = \""+cla.TableName+"\","
	}
	var attr= "    ["+NhibernateAnnotationFactoryPrefix+".Class("+prefix+" Lazy=false)]\r\n"
	return attr
}
func ( nh NhibernateAnnotationFactory) IdAttr(pro PropertyDto)string  {
	var attr="        ["+NhibernateAnnotationFactoryPrefix+".Id("+NhibernateAnnotationFactoryPrefix+"UnsavedValue = \"0\")]\r\n"
	if pro.Identity{
		attr += "        ["+NhibernateAnnotationFactoryPrefix+".Generator(Class = \"increment\")]\r\n"
	}
	attr+=nh.ColumnAttr(pro)
	return attr
}

func (nh NhibernateAnnotationFactory) ColumnAttr(pro PropertyDto)string  {
	var p  = ""
	switch pro.MappFlag {
	case 0:
	case 1:
	case 2:
	case 3:
		break
	case 4:
		if pro.PropertyName != pro.ColumnName {
			p = "        [\"+NhibernateAnnotationFactoryPrefix+\".Property(Column = \""+pro.ColumnName+"\""
		}
		if pro.Length>0{
			if p!=""{
				p+=","
			}
			p += "Length=" + string(pro.Length)
		}
		if p!=""{
			p+=")]\r\n"
		}
		break
	case 5:
		p = "        ["+EfAnnotationFactoryPrefix+".Schema.ForeignKey(\""+pro.FkColumnName+"\"]\r\n"
		break
	}
	return p
}

type EfMappFactory struct {
	prefix string
	Efcore bool
}

func (ef EfMappFactory) init()  {
	if ef.Efcore {
		ef.prefix = "builder."
	}
}
func (ef EfMappFactory) IdMapp(pro PropertyDto) string{
	var prefix = ef.prefix

	var suffix = ""
	var p=""
	if ef.Efcore {
		suffix = "//.HasAnnotation(\"MySql: ValueGenerationStrategy\", MySqlValueGenerationStrategy.IdentityColumn);"
	} else {
		suffix = "//.HasTableAnnotation(\"MySql: ValueGenerationStrategy\", MySqlValueGenerationStrategy.IdentityColumn);"
	}
	p = "            " + prefix + "HasKey(it => it." + pro.PropertyName + ");" + suffix + "\r\n\r\n"
	if pro.Identity {
		if ef.Efcore {
			p += ".ValueGeneratedOnAdd()"
		} else {
			p += ".HasDatabaseGeneratedOption( "+EfAnnotationFactoryPrefix+".Schema.DatabaseGeneratedOption.Identity)"
		}
	}
	p += ";\r\n\r\n"
	return p
}
func (ef EfMappFactory) FkMapp(pro PropertyDto) string{
	var p=""
	if pro.Single {
		if ef.Efcore {
			p = "            builder.HasOne(it => it." + pro.PropertyName + ").WithMany("
			p += "it=>it." + pro.ReferencePropertyName
			p += ").HasForeignKey(it=>it." + pro.FkColumnName + ").HasConstraintName(\"" + pro.ConstiantName + "\");//" + pro.Comment + "\r\n\r\n"

		} else {
			p = "            HasOptional(it => it." + pro.PropertyName + ").WithMany(it=>it." + pro.ReferencePropertyName + ").Map(it=>{it.MapKey(\""
			p += pro.FkColumnName + "\"); "
			p += "it.ToTable(\"" + pro.ReferenceTable + "\");" + "}).WillCascadeOnDelete();//" + pro.Comment + "\r\n\r\n"
		}
	} else {
		if ef.Efcore {
			p = "            builder.HasMany(it => it." + pro.PropertyName + ").WithOne("
			p += "it=>it." + pro.ReferencePropertyName + ").HasForeignKey(it=>it." + pro.FkColumnName + ").HasConstraintName(\"" + pro.ConstiantName
			p += "\").IsRequired().OnDelete(DeleteBehavior.Cascade);//" + pro.Comment + "\r\n\r\n";
		} else {
			p = "            HasMany(it => it." + pro.PropertyName + ").WithOptional(it=>it." + pro.ReferencePropertyName
			p += ").Map(it=>{it.MapKey(\"" + pro.FkColumnName + "\"); "
			p += "it.ToTable(\"" + pro.ReferenceTable + "\");" + "}).WillCascadeOnDelete();//" + pro.Comment + "\r\n\r\n"
		}
	}
	p = "        [" + EfAnnotationFactoryPrefix + ".Schema.ForeignKey(\"" + pro.FkColumnName + "\"]\r\n"
	return  p
}

func (ef EfMappFactory) ProMapp(pro PropertyDto) string {
	if pro.MappFlag == 0 {
		return ""
	}
	var prefix = ef.prefix
	if pro.ValueType {
		return "            " + prefix + "Property(it => it." + pro.PropertyName + ").HasColumnName(\"" + pro.ColumnName + "\");//" + strings.Trim(pro.Comment, "[ |\t]") + "\r\n\r\n";
	}
	var p = ""
	switch pro.MappFlag {
	case 0:
	case 1:
	case 2:
	case 3:
		break
	case 4:
		p = "            " + prefix + "Property(it => it." + pro.PropertyName + ")"
		if pro.PropertyName != pro.ColumnName {
			p += ".HasColumnName(\"" + pro.ColumnName + "\")"
		}
		if pro.Length > 0 {
			if p != "" {
				p += ","
			}
			p += ".HasMaxLength(" + string(pro.Length) + ");"
		}
		p += "//" + pro.Comment + "\r\n\r\n"
		break
	case 5:
		p=ef.FkMapp(pro)
		break
	case 6:
		p=ef.IdMapp(pro)
		break
	}
	return p
}

type NhibernateMappFactory struct {
	
}
func (nh NhibernateMappFactory) IdMapp(pro PropertyDto) string{
	return ""
}
func (nh NhibernateMappFactory) FkMapp(pro PropertyDto) string{
	return ""
}
func (nh NhibernateMappFactory) ProMapp(pro PropertyDto) string {
	return ""
}