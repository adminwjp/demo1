package model

import (
	"encoding/json"
	"github.com/Unknwon/goconfig"
	"log"
	"os"
	"strings"
)

type Cfg struct {
	EsAddrs    string
	RedisAddrs string
	RedisPwd   string
	RedisDb    int
	Dialet     string
	Addrs      string
	Port       int
	Ip  string
	ConsulIp string
	ConsulPort int
	ServiceName string
	ConsulTag []string
}

//空方法 加载 改包 不然 用不了 自定义包
func Empty() {

}

var Config *Cfg

func InitCfg() {
	//var iniF string
	//flag.StringVar(&iniF, "confPath", "config/cfg.ini", "Set Configuration File")
	//flag.Parse()
	//iniF="config/cfg.ini"
	pwd, err := os.Getwd()
	if err != nil {
		println("path get fail")
		os.Exit(1)
	}
	println(pwd)
	pwd=strings.Replace(pwd,"/tests",
		"",-1)
	println(pwd)
	cf, err := goconfig.LoadConfigFile(pwd+"/config/cfg.ini")
	if err != nil {
		println("load cfg.ini fail")
		os.Exit(1)
	}
	log.Println("Load cfg.ini Success")
	Config = &Cfg{
		EsAddrs:    cf.MustValue("Es", "EsAddrs", ""),
		RedisAddrs: cf.MustValue("Redis", "RedisAddrs", ""),
		RedisPwd:   cf.MustValue("Redis", "RedisPwd", ""),
		RedisDb:    cf.MustInt("Redis", "RedisDb", 0),
		Dialet:     cf.MustValue("Db", "Dialet", ""),
		Addrs:      cf.MustValue("Db", "Addrs", ""),
		Port:       cf.MustInt("Register", "Port", 8080),
		Ip:       cf.MustValue("Register", "Ip", "127.0.0.1"),
		ConsulIp:       cf.MustValue("Register", "ConsulIp", "127.0.0.1"),
		ServiceName:       cf.MustValue("Register", "ServiceName", "user.api"),
		ConsulPort:       cf.MustInt("Register", "ConsulPort", 8500),
		ConsulTag:       cf.MustValueArray("Register", "ConsulTag", ","),
	}
	j1, err := json.Marshal(Config)
	log.Println(string(j1))

}

