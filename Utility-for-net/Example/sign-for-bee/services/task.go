package services

type ITask interface {
	Execute()
}
var tasks []ITask=make([]ITask,10)
var taskIndex=0;
func InitTask()  {
	tasks[taskIndex]=UserSignInTask{}
	taskIndex++
}
//定时更新每日连签，每月累计
type UserSignInTask struct {

}

func (UserSignInTask) Execute()  {
	UpdateUserSignInTask()
}