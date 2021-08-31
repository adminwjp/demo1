package http_server

import (
	"net/http"
)

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

var Handler HandlerInterceptor

func HTTPInterceptor(h http.Handler) http.Handler {
	return hanler
}
