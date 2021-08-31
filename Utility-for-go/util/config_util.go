package util

import (
	"log"
	"github.com/Unknwon/goconfig"
)

type configUtil struct {
	// 作用域 容易 消失 指针 为 nil
	Cfg *goconfig.ConfigFile
}

func (c configUtil) LoadFile(file string, files ...string) *goconfig.ConfigFile {
	cfg, err := goconfig.LoadConfigFile(file, files...)
	if err != nil {
		log.Println("Load %s Fail", err.Error())
	}
	c.Cfg = cfg
	return cfg
}

func (c configUtil) GetStringValue(section string, key string, defValue string) (r string) {
	return GetStringValue(c.Cfg, section, key, defValue)
}

func (c configUtil) GetIntValue(section string, key string, defValue int) (r int) {
	return GetIntValue(c.Cfg, section, key, defValue)
}

func (c configUtil) GetBoolValue(section string, key string, defValue bool) (r bool) {
	return GetBoolValue(c.Cfg, section, key, defValue)
}

func (c configUtil) GetFloat64Value(section string, key string, defValue float64) (r float64) {
	return GetFloat64Value(c.Cfg, section, key, defValue)
}

func (c configUtil) GetInt64Value(section string, key string, defValue int64) (r int64) {
	return GetInt64Value(c.Cfg, section, key, defValue)
}

func (c configUtil) GetArrayValue(section string, key string, delim string) (r []string) {
	return GetArrayValue(c.Cfg, section, key, delim)
}

//===== *goconfig.ConfigFile is nil   =====

func  GetStringValue(cfg *goconfig.ConfigFile, section string, key string, defValue string) (r string) {
	val, err := cfg.GetValue(section, key)
	if err != nil {
		return defValue
	}
	return val
}

func  GetIntValue(cfg *goconfig.ConfigFile, section string, key string, defValue int) (r int) {
	val, err := cfg.Int(section, key)
	if err != nil {
		return defValue
	}
	return val
}

func  GetBoolValue(cfg *goconfig.ConfigFile, section string, key string, defValue bool) (r bool) {
	val, err := cfg.Bool(section, key)
	if err != nil {
		return defValue
	}
	return val
}

func GetFloat64Value(cfg *goconfig.ConfigFile, section string, key string, defValue float64) (r float64) {
	val, err := cfg.Float64(section, key)
	if err != nil {
		return defValue
	}
	return val
}

func  GetInt64Value(cfg *goconfig.ConfigFile, section string, key string, defValue int64) (r int64) {
	val, err := cfg.Int64(section, key)
	if err != nil {
		return defValue
	}
	return val
}

func GetArrayValue(cfg *goconfig.ConfigFile, section string, key string, delim string) (r []string) {
	return cfg.MustValueArray(section, key, delim)
}
