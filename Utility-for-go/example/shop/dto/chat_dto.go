package dto

type LoginRequest struct {
	CheckPhoneRequest
	 Password string `json:"password" bind:"required"`
}

type CheckPhoneRequest struct {
	Region string `json:"region" bind:"required"`
	Phone string `json:"phone" bind:"required"`
}
type VerifyCodeRequest struct {
	CheckPhoneRequest
	Code string `json:"code" bind:"required"`
}

type RegisterRequest struct {
	Nickname string `json:"nickname" bind:"required"`
	RestPasswordRequest
}

type SetNameRequest struct {
	Nickname string `json:"nickname" bind:"required"`
}

type SetPortraitRequest struct {
	PortraitUri string `json:"portraitUri" bind:"required"`
}

type ChangePasswordRequest struct {
	  OldPassword string `json:"oldPassword" bind:"required"`
	  NewPassword string `json:"newPassword" bind:"required"`
}

type RestPasswordRequest struct {
	Password string `json:"password" bind:"required"`
	VerificationToken string `json:"verification_token"  bind:"required"`
}
type FriendInvitationRequest struct {
	AgreeFriendsRequest
	Message string `json:"message"  bind:"required"`
}

type AgreeFriendsRequest struct {
	FriendId string `json:"friendId" bind:"required"`
}

type SetFriendDisplayNameRequest struct {
	AgreeFriendsRequest
	DisplayName string `json:"displayName" bind:"required"`
}
type CreateGroupRequest struct {
	Name string `json:"name" bind:"required"`
	MemberIds []string `json:"memberIds" bind:"required"`
}

type SetGroupPortraitRequest struct {
	GroupId string `json:"groupId" bind:"required"`
	PortraitUri string `json:"portraitUri" bind:"required"`
}

type SetGroupNameRequest struct {
	GroupId string `json:"groupId" bind:"required"`
	Name string `json:"name" bind:"required"`
}

type QuitGroupRequest struct {
	GroupId string `json:"groupId" bind:"required"`
}
type SetGroupDisplayNameRequest struct {
	GroupId string `json:"groupId" bind:"required"`
	DisplayName string `json:"displayName" bind:"required"`
}
type ParamRequest struct {
	Ids []int64 `json:"id" form:"id"`
}

type CheckUserResponse struct {
	Code   int  `json:"code"`
	Result bool `json:"result"`
}

type VerifyCodeResponse struct {
	Code int `json:"code"`
	//{"verification_token":"1"}
	Result ResultEntity `json:"result"`
}

type ResultEntity struct {
	VerificationToken string `json:"verification_token"`
}

type RegisterResponse struct {
	Code int `json:"code"`
	//{"id":"1"}
	Result interface{} `json:"result"`
}
type LoginResponse struct {
	Code int `json:"code"`
	//{"id":"1","token":"1"}
	Result interface{} `json:"result"`
}

type GetTokenResponse struct {
	Code int `json:"code"`
	//{"id":"1","token":"1"}
	Result interface{} `json:"result"`
}

func CheckUserResponseSuccess() CheckUserResponse {
	return CheckUserResponse{Code: 200, Result: true}
}

func CheckUserResponseFail() CheckUserResponse {
	return CheckUserResponse{Code: 400, Result: false}
}
