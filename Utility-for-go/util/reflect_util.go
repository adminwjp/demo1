package util

import "reflect"

func Mapp(obj interface{},dest interface{})  {
	objType:=reflect.TypeOf(obj)
	desctType:=reflect.TypeOf(dest)
	// 获取值
	vObj := reflect.ValueOf(objType)
	vDest := reflect.ValueOf(objType)
	//vObj = vObj.Elem()
	//vDest = vDest.Elem()
	num:=desctType.NumField()
	for i:=0;i<num;i++ {
		structField:=desctType.Field(i)
		name:=structField.Name
		objFie,exits:=objType.FieldByName(name)
		if exits{
			println(structField.Type.Name())
			println(structField.Type.Kind().String())
			println(vObj.FieldByName(name).IsValid())
			if vDest.FieldByName(name).IsValid(){
				continue
			}
			println(vObj.FieldByName(name).Kind().String()) //invalid
			println(vDest.FieldByName(name).Kind().String())//invalid
			//switch vObj.FieldByName(name).Kind() {
			switch structField.Type.Kind() {
			case reflect.Uint:
			case reflect.Uint8:
			case reflect.Uint16:
			case reflect.Uint32:
			case reflect.Uint64:
				vDest.FieldByName(name).SetUint(vObj.FieldByName(objFie.Name).Uint())
			case reflect.Int8:
			case reflect.Int16:
			case reflect.Int32:
			case reflect.Int:
				vDest.FieldByName(name).SetLen(vObj.FieldByName(objFie.Name).Cap())
			case reflect.Int64:
				vDest.FieldByName(name).SetInt(vObj.FieldByName(objFie.Name).Int())
			case reflect.String:
				println("String "+name+" "+vObj.FieldByName(objFie.Name).String())
				//objFie,exits=desctType.FieldByName(name)
				vDest.FieldByName(name).SetString(vObj.FieldByName(objFie.Name).String())
			case reflect.Bool:
				vDest.FieldByName(name).SetBool(vObj.FieldByName(objFie.Name).Bool())
				break
			case reflect.Array:
				vDest.FieldByName(name).SetBytes(vObj.FieldByName(objFie.Name).Bytes())
				break
			case reflect.Map:
				//vDest.FieldByName(name).SetMapIndex(vObj.FieldByName(objFie.Name).Bytes())
			case reflect.Float32:
			case reflect.Float64:
				vDest.FieldByName(name).SetFloat(vObj.FieldByName(objFie.Name).Float())
			default:
				println("default "+name)
				vDest.FieldByName(name).Set(vObj.FieldByName(objFie.Name))
			}

		}
	}
}


func MappArray(objs []*interface{},dests []*interface{})  {
	l:=len(dests)
	if l>len(objs){
		l=len(objs)
	}
	for i:=0;i<l;i++ {
		Mapp(objs[i],dests[i])
	}
}