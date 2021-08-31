//package utility
//go run: cannot run non-main package

package main

import (
	"io"
	"log"
	"net/http"
	"time"
)

func main() {
	//log.Println(util.SeurityInstance)
	Start()
}

func ServeHTTP(writer http.ResponseWriter, request *http.Request) {
	io.WriteString(writer, "test")
}
func HTTPHandlerFuncInterceptor(h http.HandlerFunc) http.HandlerFunc {
	return http.HandlerFunc(
		func(w http.ResponseWriter, r *http.Request) {
			// TODO: 进行身份验证，比如校验cookie或token
			h(w, r)
		})
}

type HandlerInterceptor struct {
	handlers []http.Handler
}

func (handlerInterceptor HandlerInterceptor) ServeHTTP(w http.ResponseWriter, r *http.Request) {
	// TODO: 进行身份验证，比如校验cookie或token
	//w.Write([]byte("not login"))
	//return
	for i := 0; i < len(handlerInterceptor.handlers); i++ {
		handlerInterceptor.handlers[i].ServeHTTP(w, r)
	}
}

var hanler HandlerInterceptor

func HTTPInterceptor(h http.Handler) http.Handler {
	return hanler
}

func Start() {
	http.HandleFunc("/test", HTTPHandlerFuncInterceptor(ServeHTTP))
	http.Handle("/", http.FileServer(http.Dir("static")))
	//hanler.handlers=append(hanler.handlers,http.FileServer(http.Dir("static")))
	//http.Handle("/", hanler)
	server := &http.Server{
		Addr:           ":8080",
		ReadTimeout:    10 * time.Second,
		WriteTimeout:   10 * time.Second,
		MaxHeaderBytes: 1 << 20,
	}
	//server.Close()
	//err:=http.ListenAndServe("",nil)
	log.Println("http server starting")
	err := server.ListenAndServe()
	if err != nil {
		log.Fatal("http server start fail")
		panic(err)
	}
	log.Println("http server start success")
	return

	//浏览器访问 http://localhost:8080/api
	mux := http.NewServeMux()
	go http.ListenAndServe(":8080", mux)

	//浏览器访问 http://localhost:8081/api
	mux1 := http.NewServeMux()
	go http.ListenAndServe(":8081", mux1)

	mux2 := http.NewServeMux()
	//浏览器访问 http://localhost:8082/api
	go http.ListenAndServe(":8082", mux2)

	log.Println("http server start success")
	//阻塞程序
	select {}
}
