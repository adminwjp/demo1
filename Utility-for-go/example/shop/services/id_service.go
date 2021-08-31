package services
import(
	"reflect"
	"sync/atomic"
)
type IIdService interface {
	GetId(name string)int64
	GetIdByStruct(model interface{})int64
}

type IdService struct {
}
var ids map[string]int64=make(map[string]int64,10)

func (IdService) GetId(name string)int64{
	/*if _, ok := map[name]; !ok {
    	ids[name]=0
    }*/
	id:=ids[name]
	atomic.AddInt64(&id,1)
	ids[name]=id
	return id 
}
func (idService IdService) GetIdByStruct(model interface{})int64{
	return idService.GetId(reflect.TypeOf(model).Name())
}


