package util

import (
	"fmt"

	"github.com/jinzhu/gorm"
)

type GormUtil struct {
	Dialet string
	Addrs  string
	// 作用域 容易 消失 指针 为 nil
	Db *gorm.DB
}

func (g GormUtil) Conn() *gorm.DB {
	db, err := gorm.Open(g.Dialet, g.Addrs)
	if err != nil {
		fmt.Print("connection database fail")
		panic(err)
	}
	g.Db = db
	return db
}

func (g GormUtil) Insert(value interface{}) bool {
	return g.Db.Create(value).Error != nil
}

func (g GormUtil) Update(value interface{}) bool {
	return g.Db.Updates(value).Error != nil
}

func (g GormUtil) Delete(value interface{}, where ...interface{}) bool {
	return g.Db.Delete(value, where...).Error != nil
}

func (g GormUtil) Find(value interface{}, where ...interface{}) bool {
	return g.Db.Find(&value, where...).Error != nil
}

func (g GormUtil) Count(value interface{}, count *int64) bool {
	return g.Db.Count(count).Error != nil
}

func Insert(db *gorm.DB, value interface{}) bool {
	return db.Create(value).Error != nil
}

func  Update(db *gorm.DB, value interface{}) bool {
	return db.Updates(value).Error != nil
}

func  Delete(db *gorm.DB, value interface{}, where ...interface{}) bool {
	return db.Delete(value, where...).Error != nil
}

func Find(db *gorm.DB, value interface{}, where ...interface{}) bool {
	return db.Find(&value, where...).Error != nil
}

func  Count(db *gorm.DB, value interface{}, count *int64) bool {
	return db.Count(count).Error != nil
}
