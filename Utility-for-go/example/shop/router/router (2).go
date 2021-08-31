package router

import (
	"shop-web/http_server"
)

func ServeHTTP(writer http.ResponseWriter, request *http.Request) {
	io.WriteString(writer, "test")
}

func init() {
	http.HandleFunc("/test", HTTPHandlerFuncInterceptor(ServeHTTP))
	//http.Handle("/", http.FileServer(http.Dir("static")))
	http_server.Handler.handlers = append(http_server.Handler.handlers, http.FileServer(http.Dir("static")))

	http.Handle("/", http_server.Handler)
}
