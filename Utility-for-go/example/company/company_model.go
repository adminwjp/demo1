package company

import (
	//"github.com/jinzhu/gorm"
)

//分类标识
const(
	None =0x0 //[]
	Role=0x1 //角色分类
	Nav=0x2 //官网导航信息
	Service=0x3 //官网服务信息
	Work=0x4 //工作分类
	Brand=0x5 //公司品牌
	BasicCategory=0x6 //基础 分类
	Social=0x7
	Media=0x8 //媒体
	Skill=0x9 //技能
	Skin=0xA //皮肤
	TestimonialPerson=0xB //关于我们 团队
	Theme=0xC //官网主题信息 分类 1:公司 2:支持 3 :开发者 4 :我们的合作伙伴
)

//注意 json 放 前面 不然 不生效
type BaseEntity struct {
	ID int64 `json:"id";gorm:"primary_key;AUTO_INCREMENT";db:"id"` //id int64 not support 主键编号
	Lanage string `json:"lanage";gorm:"column:lanage";db:"lanage"`
	CreateDate int64 `json:"create_date";db:"create_date"` //创建时间
	ModifyDate int64  `json:"modify_date";db:"modify_date"`//修改时间
	Enable bool `json:"enable";db:"enable"` //是否启用
}



type AdminEntity struct {
	BaseEntity
	Account string `json:"account";gorm:"column:account"` //账号
	Phone string `json:"phone";gorm:"column:phone"` //手机号
	Password string `json:"password";gorm:"column:password"` //密码
	Email string `json:"email";gorm:"column:email"` //邮箱
	Sex string `json:"sex";gorm:"column:sex"` //性别
	Remark string `json:"remark";gorm:"column:remark"` //备注
}
func (AdminEntity) TableName() string {
	return "t_admin"
}

type AboutEntity struct {
	BaseEntity
	Title string `json:"title";gorm:"column:title"` //标题
	BackgroundImage  string `json:"background_image";gorm:"column:background_image"` //背景图片素材
	Logo string	 `json:"logo"` //图文logo素材
}
// 设置 AboutEntity 的表名为 t_about
func (AboutEntity) TableName() string {
	return "t_about"
}



type CategoryEntity struct {
	BaseEntity
	Name string `json:"name";db:"name"`
	Flag int64  `json:"flag";db:"flag"` //分类标识
	Body string `json:"body";db:"body"` //媒体内容
	Review int64 `json:"review"`//评分
	Color string `json:"color"`//颜色
	Process string `json:"process"` //进度
	Style string `json:"style"`//样式
	BackgroundImage string `json:"background_image"` //背景图片素材地址
	Href string `json:"href"`//品牌链接地址
	Feature string `json:"feature"`//品牌特征
	Filter string `json:"filter";db:"filter"` //工作分类 过滤条件
	// 二 选用 一 timeout wait too long time 90s
	//Parent *CategoryEntity `gorm:"ForeignKey:ParentId;AssociationForeignKey:ID"`
	Children []*CategoryEntity `json:"children";gorm:"ForeignKey:ParentId;ASSOCIATION_FOREIGNKEY:ID"`
	ParentId int64 `json:"parent_id";db:"parent_id"`
}
func (CategoryEntity) TableName() string {
	return "t_category"
}

type CompanyEntity struct {
	BaseEntity
	Tel string `json:"tel"` //联系电话
	Logo string `json:"logo"` //公司logo素材
	Logo1 string `json:"logo1"` //公司logo1素材

}
func (CompanyEntity) TableName() string {
	return "t_company"
}

type ImageEntity struct {
	BaseEntity
	Name string `json:"name"` //素材名称
	Href string `json:"href"`//素材地址别名
	Src string `json:"src"`//素材地址 即物理地址
	Type string  `json:"type"`//素材标识
}
func (ImageEntity) TableName() string {
	return "t_image"
}

type MainEntity struct {
	BaseEntity
	Name string `json:"name"`//名称
	ButtonHref1 string  `json:"button_href1"`//按钮1地址
	ButtonName1 string `json:"button_name1"` //按钮1名称
	ButtonHref2 string  `json:"button_href2"`//按钮2地址
	ButtonName2 string  `json:"button_name2"`//按钮2名称
	BackgroundImage string `json:"background_image"` //主页背景素材
}

func (MainEntity) TableName() string {
	return "t_main"
}

const (
	WorkByCatalogImg= "work_by_catalog_img"
	TeamSourceByCatalogTeam= "team_source_by_catalog_team"
)
type RelationEntity struct {
	BaseEntity
	Fk1 int `json:"fk1"`
	Fk2 int `json:"fk2"`
	Flag string `json:"flag"`
}
func (RelationEntity) TableName() string {
	return "t_relation"
}

type TeamEntity struct {
	Service int  `json:"service"`//服务
	Category int `json:"category"` //分类
	Img int  `json:"img"`//素材
	TeamSources []*CategoryEntity `json:"team_sources"` //团队来源

}
func (TeamEntity) TableName() string {
	return "t_team"
}