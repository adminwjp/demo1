package models

type Cartinfor struct {
	Id      int64   `form:"id" json:"id" xml:"id" `
	Image   string  `form:"image" json:"image" xml:"image" `
	AttrVal string  `form:"attr_val" json:"attr_val" xml:"attr_val" `
	Stock   int32   `form:"stock" json:"stock" xml:"stock" `
	Title   string  `form:"title" json:"title" xml:"title" `
	Price   float64 `form:"price" json:"price" xml:"price" `
	Number  int32   `form:"number" json:"number" xml:"number" `
}

func (Cartinfor) TableName() string {
	return "t_cart"
}

type Cataloginfor struct {
	Id       int64           `form:"id" json:"id" xml:"id" `
	Pid      int64           `form:"pid" json:"pid" xml:"pid" `
	Parent   *Cataloginfor   `form:"parent" json:"parent" xml:"parent" `
	Children []*Cataloginfor `form:"children" json:"children" xml:"children" `
	Name     string          `form:"name" json:"name" xml:"name" `
	Picture  string          `form:"picture" json:"picture" xml:"picture" `
}

func (Cataloginfor) TableName() string {
	return "t_catalog"
}
