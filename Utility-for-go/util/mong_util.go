package util

import (
	"fmt"
	"gopkg.in/mgo.v2"
	"time"
)
//https://studygolang.com/articles/14055
type MongUtil struct {
	// 指针 不支持 报错
	//runtime error: invalid memory address or nil pointer dereference
	Session *mgo.Session

}

func (mong MongUtil) Conn()  *mgo.Session {
	var dailInfo *mgo.DialInfo
	addr:="127.0.0.1"
	username:=""
	password:=""
	if CheckFileIsExists("config/cfg.ini"){
		cfg:=configUtil{}
		cf:=cfg.LoadFile("config/cfg.ini")
		addr=GetStringValue(cf,"Mong","MongIp","127.0.0.1")
		username=GetStringValue(cf,"Mong","MongUser","")
		username=GetStringValue(cf,"Mong","MongPwd","")
	}
	dailInfo=&mgo.DialInfo{
		Addrs:[]string{addr},
		Direct:false,
		Timeout:time.Second*1,
		Database:"test",
		Source:"",
		Username:username,
		Password:password,
		PoolLimit:1024,
	}
	session,err:=mgo.DialWithInfo(dailInfo)
	if err!=nil{
		fmt.Println("mong conn err ,errror:"+err.Error())
		return nil
	}
	mong.Session=session
	//defer  session.Clone()
	session.SetMode(mgo.Monotonic,true)
	fmt.Println("mong conn suc ")
	return  session
}

func (mong MongUtil)  Insert(db string,col string,doc ...interface{}) bool {
	c:=mong.Session.DB(db).C(col)
	return c.Insert(doc...)==nil
}

func (mong MongUtil)  Update(db string,col string,where interface{},val interface{}) bool {
	c:=mong.Session.DB(db).C(col)
	return c.Update(where,val)==nil
}


