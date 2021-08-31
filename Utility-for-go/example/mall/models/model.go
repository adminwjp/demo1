package models

// https://gorm.io/docs/connecting_to_the_database.html
// https://github.com/go-gorm/gorm
type Picture struct {
	id    int
	name  string
	title string
	img   string
}

type AdminAction struct {
	Id int64
	ActionName string
	Code string
	Info string
	CreateTime int64
	Enable bool
}

type AdminRoleAction struct {
	Id int64
	RoleId int64
	ActionId int64
	CreateTime int64
}

type AdminRole struct {
	Id int64
	RoleName string
	Info string
	IsDeleted bool
	CreateTime int64
}

type AdminUser struct {
	Id int64
	Username string
	Password string
	Nickname string
	RoleId int64
	CreateTime int64
}

type Announce struct {
	Id int64
	Title string
	Content string
	IsDel bool
	RoleId int64
	CreateTime int64
	UpdateTime int64
	Types int

}

type BackstageUser struct {
	Id int64
	LoginName string
	Password string
	LastLoginIp string
	Email string
	CreateTime int64
	UpdateTime int64
	LastLoginTime int
	RoleId int
	FullName string
	Mobile string
	Gender int
	IdCard string
	AccountType string
	AccountStatus int
	SourceType int
	Role int
}

type Banner struct {
	Id int64
	Pic string
	JumpUrl string
	LastLoginIp bool
	Types int64
	IsDel int
	CreateTime int64
	UpdateTime int64
}

type MessageEntity struct {
	Id int64
	UserId int64
	Title string
	Content string
	LookCount bool
	Pics string
	Orders int
	Types int64
	IsDel int
	CreateTime int64
	UpdateTime int64
}

type MessageType struct {
	Id int64
	Title string
	Pic string
	Orders int
	Types int64
	IsDel int
	CreateTime int64
	UpdateTime int64
}

type Scenic struct {
	Id int64
	UserId int64
	Title string
	LTitle string
	LookCount int64
	Content string
	Pic string
	Orders int
	Mark1 string
	Mark2 string
	IsDel int
	CreateTime int64
	UpdateTime int64
}

type ShopEntity struct {
	Id int64
	UserId int64
	Status int64
	Orders int
	Title string
	Content string
	LogoPic string
	LookCount int64
	Types int
	ShopType int
	Latitude float64
	Longitude float64
	Mark1 string
	Mark2 string
	IsDel int
	OpenTime int64
	CloseTime int64
	PhoneNum string
	CreateTime int64
	UpdateTime int64
	BeginTime int64
	EndTime int64
}

type ShopsDetail struct {
	Id int64
	ShopId int64
	Pic string
	Content string
	IsDel int
	CreateTime int64
	UpdateTime int64
}

type SystemAction struct {
	ActionId int64
	ActionDescription string
	ActionName string
	CreateTime int64
	Url string
	Orders int
	ParentAction string
	Icon string
}

type SystemRolePermission struct {
	RoleId int64
	ActionId string
	CreateTime int64
	Url string
	Orders int
	ParentAction string
	Icon string
}

type SystemRole struct {
	Id int64
	Name string
}

type TaskSchedule struct {
	Id int64
	JobGroup string
	JobName string
	CronExpress string
	StarRunTime int64
	EndRunTime int64
	NextRunTime int64
	RunStatus int
	Remark string
	CreateTime int64
	UpdateTime int64
	CreateAuthr string
}

type User struct {
	Id int64
	NickName string
	PhoneNum string
	PassWord string
	Pic string
	Token string
	Uid string
	Status int
	RefId int64
	Amount float64
	OpenId string
	SessionKey string
	UPic string
	CreateTime int64
	UpdateTime int64
}

