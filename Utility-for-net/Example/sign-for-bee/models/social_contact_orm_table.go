package models

// 给结构体添加一个方法 方法会由框架自行调用
// 实现model 映射到对应的表上
// tn_Attachments
func (*Attachment) TableName() string {
	return "tn_Attachments"
}

func (*AuditItem) TableName() string {
	return "tn_AuditItems"
}

func (*AuditItemInUserRole) TableName() string {
	return "tn_AuditItemsInUserRoles"
}

func (*Category) TableName() string {
	return "tn_Categories"
}

func (*ItemInCategory) TableName() string {
	return "tn_ItemsInCategories"
}

func (*CategoryManager) TableName() string {
	return "tn_CategoryManagers"
}

func (*ContentCategory) TableName() string {
	return "tn_ContentCategories"
}

func (*ContentItem) TableName() string {
	return "tn_ContentItems"
}
func (*ContentModel) TableName() string {
	return "tn_ContentModels"
}
func (*ContentModelAdditionalFields) TableName() string {
	return "tn_ContentModelAdditionalFields"
}
func (*Comment) TableName() string {
	return "tn_Comments"
}
func (*CountEntity) TableName() string {
	return "tn_Counts"
}
func (*Section) TableName() string {
	return "tn_Sections"
}
func (*Thread) TableName() string {
	return "tn_Threads"
}
func (*TenantType) TableName() string {
	return "tn_TenantTypes"
}
func (*AccountType) TableName() string {
	return "tn_AccountTypes"
}
func (*AccountBinding) TableName() string {
	return "tn_AccountBindings"
}
func (*OnlineUser) TableName() string {
	return "tn_OnlineUsers"
}

func (*PointCategory) TableName() string {
	return "tn_PointCategories"
}

func (*PointItem) TableName() string {
	return "tn_PointItems"
}
func (*PointRecord) TableName() string {
	return "tn_PointRecords"
}
func (*Role) TableName() string {
	return "tn_Roles"
}
func (*UserInRole) TableName() string {
	return "tn_UserInRoles"
}
func (*UserRank) TableName() string {
	return "tn_UserRanks"
}
func (*User) TableName() string {
	return "tn_Users"
}
func (*Advertising) TableName() string {
	return "tn_Advertisings"
}
func (*Area) TableName() string {
	return "tn_Areas"
}
func (*Attitude) TableName() string {
	return "tn_Attitudes"
}
func (*AttitudeRecord) TableName() string {
	return "tn_AttitudeRecords"
}
func (*AtUserEntity) TableName() string {
	return "tn_AtUsers"
}
func (*FollowEntity) TableName() string {
	return "tn_Follows"
}
func (*ImpeachReport) TableName() string {
	return "tn_ImpeachReports"
}
func (*InvitationCode) TableName() string {
	return "tn_InvitationCodes"
}
func (*InviteFriendRecord) TableName() string {
	return "tn_InviteFriendRecords"
}
func (*LinkEntity) TableName() string {
	return "tn_Links"
}
func (*ListEntity) TableName() string {
	return "tn_Lists"
}
func (*ListItem) TableName() string {
	return "tn_ListItems"
}
func (*Medal) TableName() string {
	return "tn_Medals"
}
func (*MedalCondition) TableName() string {
	return "tn_MedalConditions"
}
func (*MedalInCondition) TableName() string {
	return "tn_MedalInConditions"
}
func (*MedalInGroup) TableName() string {
	return "tn_MedalInGroups"
}
func (*Message) TableName() string {
	return "tn_Messages"
}
func (*MessageSession) TableName() string {
	return "tn_MessageSessions"
}
func (*Notice) TableName() string {
	return "tn_Notices"
}
func (*NoticeType) TableName() string {
	return "tn_NoticeTypes"
}
func (*NoticeTypeSettings) TableName() string {
	return "tn_NoticeTypeSettings"
}
func (*OperationLog) TableName() string {
	return "tn_OperationLogs"
}
func (*ParsedMedia) TableName() string {
	return "tn_ParsedMedias"
}
func (*Permission) TableName() string {
	return "tn_Permissions"
}
func (*PermissionItem) TableName() string {
	return "tn_PermissionItems"
}
func (*PointRechargeOrder) TableName() string {
	return "tn_PointRechargeOrders"
}
func (*PointTask) TableName() string {
	return "tn_PointTasks"
}
func (*PointTaskRecord) TableName() string {
	return "tn_PointTaskRecords"
}
func (*PointTaskType) TableName() string {
	return "tn_PointTaskTypes"
}
func (*PointTaskUserInfo) TableName() string {
	return "tn_PointTaskUserInfos"
}
func (*Review) TableName() string {
	return "tn_Reviews"
}
func (*ReviewSummary) TableName() string {
	return "tn_ReviewSummaries"
}
func (*SearchWord) TableName() string {
	return "tn_SearchWords"
}
func (*ShortUrlEntity) TableName() string {
	return "tn_ShortUrls"
}
func (*SpecialContentItem) TableName() string {
	return "tn_SpecialContentItems"
}
func (*SpecialContentType) TableName() string {
	return "tn_SpecialContentTypes"
}

func (*FavoriteEntity) TableName() string {
	return "tn_Favorites"
}
func (*ItemInTag) TableName() string {
	return "tn_ItemInTags"
}
func (*RelatedTag) TableName() string {
	return "tn_RelatedTags"
}

func (*Tag) TableName() string {
	return "tn_Tags"
}
