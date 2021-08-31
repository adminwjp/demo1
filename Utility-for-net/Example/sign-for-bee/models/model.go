package models

import (
	"github.com/beego/beego/v2/client/orm"
)

const (
	Personality      = iota //个性电台
	Recommend               //为你推荐
	News                    //最新专辑
	ExclusiveContent        //独家内容
)

//模型层 必须 按 规则 来 不然 不创建
// 外键 其实 创建 了 只不过没关联
//default is to use 'id' if not set
// 手字母小写 不会创建
type Carousel struct {
	//Id int64
	//id  int64 `orm:"pk"`
	Id  int64  `orm:"column(id)" json:"id"`
	Src string `orm:"column(src);size(100)" json:"src"`
}

// 给结构体添加一个方法 方法会由框架自行调用
// 实现model 映射到对应的表上
// CarouselTableName
func (l *Carousel) TableName() string {
	return "t_carousel"
}

type Music struct {
	Id     int64  `orm:"column(id)" json:"id"`
	Name   string `orm:"column(name);size(100)" json:"name"`
	Disc   string `orm:"null;column(disc);size(100)" json:"disc"`
	Src    string `orm:"column(src);size(100)" json:"src"`
	Author string `orm:"null;column(author);size(100)" json:"author"`
	// catalog_id
	Catalog *Catalog `orm:"null;rel(fk)" json:"catalog"` // 设置一对一反向关系(可选)
	//CatalogId  int64    `orm:"column(catalog_id)" json:"catalog_id"`
	Count      string `orm:"null;column(count)" json:"count"`
	UpdateTime string `orm:"null;column(update_time)" json:"update_time"`
}

//MusicTableName
func (l *Music) TableName() string {
	return "t_music"
}

type Catalog struct {
	Id   int64  `orm:"column(id)" json:"id"`
	Name string `orm:"column(name);size(100)" json:"name"`
	Src  string `orm:"null;column(src);size(100)" json:"src"`
	Flag int    `orm:"null;column(flag)" json:"flag"`

	Musices []*Music `orm:"null;reverse(many)" json:"musices"` // 设置一对一反向关系(可选)

	// parent_id
	Parent   *Catalog   `orm:"null;rel(fk)" json:"parent"`         // 设置一对一反向关系(可选)
	Children []*Catalog `orm:"null;reverse(many)" json:"children"` //设置多对多反向关系
}

// CatalogTableName
func (l *Catalog) TableName() string {
	return "t_catalog"
}

func Init() {
	// 需要在init中注册定义的model
	orm.RegisterModel(new(Carousel), new(Music), new(Catalog))


}
