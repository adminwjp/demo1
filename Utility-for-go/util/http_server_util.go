package util

import (
	"io"
	"log"
	"net/http"
	//"os"
	//"strconv"
	"time"
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

type  handlerInterceptor struct {
	Handlers []http.Handler
}
func (handlerInterceptor handlerInterceptor) ServeHTTP(w http.ResponseWriter, r *http.Request)  {
	w.Header().Set("Access-Control-Allow-Origin", "*")
	// TODO: 进行身份验证，比如校验cookie或token
	//w.Write([]byte("not login"))
	//return
	for i:=0;i<len(handlerInterceptor.Handlers);i++ {
		handlerInterceptor.Handlers[i].ServeHTTP(w,r)
	}
}
var Handler handlerInterceptor=handlerInterceptor{}
func HTTPInterceptor(h http.Handler) http.Handler {
	return Handler
}

var single =true
func  Start( router func(),ports []string,statics []string)  {
	
	http.HandleFunc("/test",HTTPHandlerFuncInterceptor(ServeHTTP))
	//http.Handle("/", http.FileServer(http.Dir("static")))
	Handler.Handlers=append(Handler.Handlers,http.FileServer(http.Dir("static")))
	if statics!=nil && len(statics)>0{
		for i := 0; i < len(statics); i++ {
			Handler.Handlers=append(Handler.Handlers,http.FileServer(http.Dir(statics[i])))
		}
	}
	http.Handle("/", Handler)
	if (single) {
		server:=&http.Server{
			Addr: ports[0],
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
	}else{
		//浏览器访问 http://localhost:8080/api
		for i := 0; i < len(ports); i++ {
			mux := http.NewServeMux()
			go http.ListenAndServe(ports[i], mux)
		}
		log.Println("http server start success")
		//阻塞程序
		select {}
	}
}