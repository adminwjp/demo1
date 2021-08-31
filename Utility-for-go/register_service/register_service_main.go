package register_service

import (
	"google.golang.org/grpc"
	"google.golang.org/grpc/reflection"
	"io"
	"log"
	"net"
	pb "news/register_service/protos/impl/services"
	"net/http"
	"strconv"
	"sync"
	"time"
	context "context"
)
func ServeHTTP(writer http.ResponseWriter,request *http.Request){
	io.WriteString(writer,"test")
}
func HTTPHandlerFuncInterceptor(h http.HandlerFunc) http.HandlerFunc {
	return http.HandlerFunc(
		func(w http.ResponseWriter, r *http.Request) {
			// TODO: 进行身份验证，比如校验cookie或token
			h(w, r)
		})
}
type  HandlerInterceptor struct {
	handlers []http.Handler
}
func (handlerInterceptor HandlerInterceptor) ServeHTTP(w http.ResponseWriter, r *http.Request)  {
	// TODO: 进行身份验证，比如校验cookie或token
	//w.Write([]byte("not login"))
	//return
	for i:=0;i<len(handlerInterceptor.handlers);i++ {
		handlerInterceptor.handlers[i].ServeHTTP(w,r)
	}
}
var hanler HandlerInterceptor
func HTTPInterceptor(h http.Handler) http.Handler {
	return hanler
}
//注意 不能 相互 引用  不然 报错
func Main() {
	log.Println("statring grpc server ")
	grpcStart()
	//log.Println("statring check task thred")
	//go checkTask()
	//log.Println("executing check task thred")
	return
	http.HandleFunc("/test",HTTPHandlerFuncInterceptor(ServeHTTP))
	//http.Handle("/", http.FileServer(http.Dir("static")))
	//hanler.handlers=append(hanler.handlers,http.FileServer(http.Dir("static")))
	http.Handle("/", hanler)
	server:=&http.Server{
		Addr: ":8080",
		ReadTimeout: 10 * time.Second,
		WriteTimeout: 10 * time.Second,
		MaxHeaderBytes: 1 << 20,
	}
	//server.Close()
	//err:=http.ListenAndServe("",nil)
	log.Println("http server starting")
	err:=server.ListenAndServe()
	if err!=nil{
		log.Fatal("http server start fail")
		panic(err)
	}
	log.Println("http server start success")
}

// server is used to implement helloworld.GreeterServer.
type server struct{}

func grpcStart()  {
	lis, err := net.Listen("tcp", ":4500")
	if err != nil {
		log.Fatalf("failed to listen: %v", err)
	}
	s := grpc.NewServer()
	pb.RegisterRegisterServiceServer(s, &server{})
	// Register reflection service on gRPC server.
	reflection.Register(s)
	if err := s.Serve(lis); err != nil {
		log.Fatalf("failed to serve: %v", err)
	}

}


type ServiceInfo struct {
	Port int32
	Ip string
	CallIp string
	CallPort int32
	Client pb.RegisterServiceClient
	Create bool
	FailCount int
	CallFailCount int
	CurrentDate int64
	Name string

}
var rw sync.RWMutex=sync.RWMutex{}
func (server) Register(c context.Context,ser *pb.ServiceRequest) (*pb.ServiceReply, error){
	if ser.Ip==""{
		return &pb.ServiceReply{Status:-1}, nil
	}
	serviceInfo:=ServiceInfo{Name: ser.Name,CallIp: ser.Ip,CallPort: ser.Port, CurrentDate: time.Now().Unix()}
	//registerServices[ser.Ip+strconv.FormatInt(int64(serviceInfo.CallPort),10)]=serviceInfo
	registerServices[ser.Name]=serviceInfo
	return &pb.ServiceReply{Status:0,Msg:"success"}, nil
}
func (server) Callback(context.Context, *pb.EmptyRequest) (*pb.ServiceReply, error){
	return &pb.ServiceReply{Status:0,Msg:"success"}, nil
}

func (server) Get(c context.Context,name *pb.NameRequest) (*pb.ServiceApiReply, error){
	val,ok:=registerServices[name.Name]
	if ok {
		return &pb.ServiceApiReply{Status:0,Name: val.Ip,Ip: val.Ip,Port: val.Port}, nil
	}
	return &pb.ServiceApiReply{Status:-1}, nil

}

type queue struct {
	Services []map[string]int32
}

var registers queue=queue{Services:make([]map[string]int32,1000)}

var registerServices map[string] ServiceInfo=make(map[string] ServiceInfo,1000)

func checkTask()  {
	for  {
		check()
		t,err:=time.ParseDuration("60s")
		if err!=nil{
			continue
		}
		time.Sleep(t)
	}
}
func check()  {

	count:=len(registerServices)
	if count<1{
		return
	}
	i:=0
	for key,val := range registerServices{
		if i==count-1{
			break
		}
		if key==""{

		}
		if val.CallFailCount>100{
			continue
		}
		if val.CallFailCount>10{
			clientCallback(val)
		}
		t:=time.Now().Unix()
		if t-val.CurrentDate >1000*2{
			clientCallback(val)
		}
		i++
	}
}
func conn(call ServiceInfo)  {
	p:=strconv.FormatInt(int64(call.CallPort),10)
	conn, err := grpc.Dial(call.CallIp+":"+p, grpc.WithInsecure())
	if err != nil {
		log.Println("did not connect: %v,call back ip:"+call.Ip+",port:"+p, err)
		call.FailCount++
		return
	}
	//defer conn.Close()
	c :=pb.NewRegisterServiceClient(conn)
	call.Client=c
	call.Create=true
}
func  clientCallback(call ServiceInfo)  {
	call.CurrentDate=time.Now().Unix()
	// Set up a connection to the server.
	if call.FailCount>0{
		conn(call)
		if !call.Create{
			return
		}
		call.FailCount=0
	}
	if !call.Create{
		conn(call)
		return
	}
	// Contact the server and print out its response.

	ctx, cancel := context.WithTimeout(context.Background(), time.Second)
	defer cancel()
	stream, err := call.Client.Callback(ctx, &pb.EmptyRequest{})
	if err != nil {
		log.Printf("could not greet: %v", err)
		call.CallFailCount++
		return
	}
	call.CallFailCount=0
	log.Printf("callback: %s\n pass",stream.String())
}



