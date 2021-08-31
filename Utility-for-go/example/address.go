package example

// gorm 这个 包 有  好 几个(多个作者) 坑嗲  每个 作者 写 的 不同 ?
//其他 第三方 包 也 一样 的
// 模型定义
// https://blog.csdn.net/weixin_38386235/article/details/113386620
// index index:idx_name_code  unique_index
// 列名是字段名的蛇形小写 CreateTime create_time
// 默认表名是 addressinfors 表名是结构体名称的复数形式
type Addressinfor struct {
	Id          int64  `gorm:"id" json:"id" form:"id" json:"id" xml:"id" `
	Name        string `form:"name" json:"name" xml:"name"  binding:"required"`
	Mobile      string `form:"mobile" json:"mobile" xml:"mobile"  binding:"required"`
	AddressName string `form:"address_name" json:"address_name" address_name:"mobile"  binding:"required"`
	Address     string `form:"address" json:"address" xml:"address"  binding:"required"`
	Area        string `form:"area" json:"area" xml:"area"  binding:"required"`
	Default     bool   `form:"default" json:"default" xml:"default" `
	Lyricists   string `gorm:"-"`
}

func (Addressinfor) TableName() string {
	//实现TableName接口，以达到结构体和表对应，如果不实现该接口，并未设置全局表名禁用复数，gorm会自动扩展表名为articles（结构体+s）
	return "t_address"
}
