package models

import "github.com/beego/beego/v2/client/orm"
import _ "github.com/go-sql-driver/mysql"
//import _ "github.com/mattn/go-sqlite3"
var TenantTypeIds ITenantTypeIds
var  AuditItemKeys auditItemKeys
var AccountTypeKeys accountTypeKeys
var RoleIds roleIds
func init()  {
	TenantTypeIds=newTenantTypeIds()
	AuditItemKeys=newAuditItemKeys()
	AccountTypeKeys=newAccountTypeKeys()
	RoleIds= newRoleIds()
	initModel()
}

func  initModel()  {
	orm.RegisterModel(new(Attachment), new(AuditItem), new(AuditItemInUserRole),
		new(Category), new(ItemInCategory), new(CategoryManager),
		new(ContentCategory), new(ContentItem), new(ContentModel),
		new(ContentModelAdditionalFields), new(Comment), new(Section),
		new(Thread), new(TenantType), new(AccountType),
		new(AccountBinding), new(OnlineUser),

		new(PointCategory), new(PointItem), new(PointRecord),
		new(Role), new(UserInRole), new(UserRank),
		new(User), new(Advertising), new(Area),
		new(Attitude), new(AttitudeRecord), new(AtUserEntity),
		new(FollowEntity), new(ImpeachReport), new(InvitationCode),
		new(InviteFriendRecord), new(LinkEntity), new(ListEntity),
		new(ListItem), new(Medal), new(MedalCondition),
		new(MedalInCondition), new(MedalInGroup), new(Message),
		new(MessageSession), new(Notice), new(NoticeType),
		new(NoticeTypeSettings), new(OperationLog), new(ParsedMedia),

		new(Permission), new(PermissionItem), new(PointRechargeOrder),
		new(PointTask), new(PointTaskRecord), new(PointTaskType),
		new(PointTaskUserInfo), new(Review), new(ReviewSummary),
		new(SearchWord), new(SpecialContentItem), new(SpecialContentType),
		new(FavoriteEntity),

		new(ItemInTag),new(RelatedTag), new(Tag), new(UserSignIn),
		new(UserSignInDetail),
	 )

	orm.RegisterDriver("mysql", orm.DRMySQL)
	orm.RegisterDataBase("default", "mysql", "root:wjp930514.W@(192.168.1.4:3306)/bee?charset=utf8")

	//orm.DR_Sqlite undefined
	//orm.RegisterDriver("sqlite3", orm.DRSqlite)
	//orm.RegisterDataBase("default", "sqlite3", "E:/work/db/sqlite/bee.sqlite3")
	//orm.RegisterDataBase("default", "sqlite3", "/home/program/db/sqlite/bee.sqlite3")


	orm.RunSyncdb("default", false, true)
	//orm.DefaultTimeLoc = time.UTC

}