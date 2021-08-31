package models

//审核项目标识
type auditItemKeys struct {
	Comment string //评论
	Post string //贴子审核
	ContentItem string  //内容项审核
}
type accountTypeKeys struct {
	QQ string //QQ
	WeChat string //WeChat
	SinaWeibo string //SinaWeibo
	AliPay string //AliPay
}
type roleIds struct {
	SuperAdministrator int64 //超级管理员
	RegisteredUsers int64 //注册用户
	ModeratedUser int64 //管制用户
	Anonymous int64 //匿名用户
	TrustedUser int64 //受信任用户
}
func  newRoleIds() roleIds {
	return roleIds{
		SuperAdministrator:101,RegisteredUsers:121,ModeratedUser:123,Anonymous:122,
		TrustedUser:111,
	}
}

func  newAccountTypeKeys() accountTypeKeys {
	return accountTypeKeys{QQ: "QQ",WeChat:"WeChat",SinaWeibo: "SinaWeibo",AliPay: "AliPay"}
}

func  newAuditItemKeys() auditItemKeys {
	return auditItemKeys{Comment: "Comment",Post:"Post",ContentItem: "CMS"}
}

type  ITenantTypeIds interface {
	User() string
	Role() string
	Categorie() string
	Comment() string
	Tag() string
	GAttachment() string
	Recommend() string
	Link() string
	Advertising() string
	Review() string
	Point() string
	Permission() string
	Navigation() string
	Medal() string
	Section() string
	Thread() string
	Bar() string
	ContentItem() string
	CategoryManagers() string
	CMS_Article() string
	CMS_Image() string
	CMS_Video() string
}

//租户类型Id
type tenantTypeIds struct {
	user string //用户
	role string //角色
	categorie string //分类
	comment string //评论
	tag string //标签
	attachment string //附件
	recommend string //推荐
	link string //友情链接
	advertising string // 广告
	review string //评价
	point string //积分
	permission string //权限
	navigation string //导航
	searchWord string //搜索词
	medal string //勋章
	section string //板块
	thread string //贴子
	bar string //贴吧
	contentItem string //资讯
	categoryManagers string //资讯栏目
	cMS_Article string //文章
	cMS_Image string //组图
	cMS_Video string //视频
}

func newTenantTypeIds() tenantTypeIds {
	return tenantTypeIds{user: "000001",role: "000002",categorie:"000021",
		comment:"000031",tag:"000041",attachment:"000051",recommend:"000061",
		link:"友情链接",advertising:"广告位",review: "000101",point: "000111",
		permission: "000121",navigation: "000131",searchWord: "000141",
		medal: "000151",section: "100001",thread: "100002",contentItem: "100011",
		categoryManagers: "100012",cMS_Article: "100013",cMS_Image: "100014",
		cMS_Video: "100015",
	}
}

func (t tenantTypeIds)  User() string{
	return  t.user
}
func (t tenantTypeIds)  Role() string{
	return  t.role
}
func (t tenantTypeIds)  Categorie() string{
	return  t.categorie
}
func (t tenantTypeIds)  Comment() string{
	return  t.comment
}
func (t tenantTypeIds)  Tag() string{
	return  t.tag
}
func (t tenantTypeIds)  GAttachment() string{
	return  t.attachment
}
func (t tenantTypeIds)  Recommend() string{
	return  t.recommend
}
func (t tenantTypeIds)  Link() string{
	return  t.link
}
func (t tenantTypeIds)  Advertising() string{
	return  t.advertising
}
func (t tenantTypeIds)  Review() string{
	return  t.review
}
func (t tenantTypeIds)  Point() string{
	return  t.point
}
func (t tenantTypeIds)  Permission() string{
	return  t.searchWord
}
func (t tenantTypeIds)  Navigation() string{
	return  t.searchWord
}
func (t tenantTypeIds)  Medal() string{
	return  t.medal
}
func (t tenantTypeIds)  Section() string{
	return  t.section
}
func (t tenantTypeIds)  Thread() string{
	return  t.thread
}
func (t tenantTypeIds)  Bar() string{
	return  t.bar
}
func (t tenantTypeIds)  ContentItem() string{
	return  t.contentItem
}
func (t tenantTypeIds)  CategoryManagers() string{
	return  t.categoryManagers
}
func (t tenantTypeIds)  CMS_Article() string{
	return  t.cMS_Article
}
func (t tenantTypeIds)  CMS_Image() string{
	return  t.cMS_Image
}
func (t tenantTypeIds)  CMS_Video() string{
	return  t.cMS_Video
}
