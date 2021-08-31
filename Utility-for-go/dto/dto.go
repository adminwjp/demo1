package dto

type ResponseDto struct {
	Success bool        `json:"success"`
	Msg     string      `json:"msg"`
	Data    interface{} `json:"data"`
	Code    int         `json:"code"`
}

func (ResponseDto) CreateSuccess() ResponseDto {
	return ResponseDto{Success: true, Msg: "success", Code: 200}
}

func (ResponseDto) Fail() ResponseDto {
	return ResponseDto{Success: false, Msg: "fail", Code: 400}
}

func (ResponseDto) Error() ResponseDto {
	return ResponseDto{Success: false, Msg: "error", Code: 500}
}

type ApiResult struct {
	Status bool        `json:"status"`
	Msg    string      `json:"msg"`
	Data   interface{} `json:"data"`
	Code   int         `json:"code"`
}

func (ApiResult) Success() ApiResult {
	return ApiResult{Status: true, Msg: "success", Code: 200}
}

func (ApiResult) Fail() ApiResult {
	return ApiResult{Status: false, Msg: "fail", Code: 400}
}

func (ApiResult) Error() ApiResult {
	return ApiResult{Status: false, Msg: "error", Code: 500}
}
