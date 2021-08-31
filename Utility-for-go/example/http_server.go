package example

import (
	"io"
	"log"
	"net/http"
	"os"
	"strconv"
	"utility/example/company"

	"utility/util"
)

func router() {
	var nav company.NavCtl
	http.HandleFunc("/api/admin/nav/insert/", util.HTTPHandlerFuncInterceptor(nav.Insert))
	http.HandleFunc("/api/admin/nav/update/", util.HTTPHandlerFuncInterceptor(nav.Update))
	http.HandleFunc("/api/admin/nav/delete/", util.HTTPHandlerFuncInterceptor(nav.Delete))
	http.HandleFunc("/api/admin/nav/list/", util.HTTPHandlerFuncInterceptor(nav.Find))

	var admin company.Admintl
	http.HandleFunc("/api/admin/admin/insert/", util.HTTPHandlerFuncInterceptor(admin.Insert))
	http.HandleFunc("/api/admin/admin/update/", util.HTTPHandlerFuncInterceptor(admin.Update))
	http.HandleFunc("/api/admin/admin/delete/", util.HTTPHandlerFuncInterceptor(admin.Delete))
	http.HandleFunc("/api/admin/admin/list/", util.HTTPHandlerFuncInterceptor(admin.Find))
	http.HandleFunc("/test/1/2/3", util.HTTPHandlerFuncInterceptor(util.ServeHTTP))
	http.HandleFunc("/test", util.HTTPHandlerFuncInterceptor(util.ServeHTTP))
	http.HandleFunc("/down", func(writer http.ResponseWriter, request *http.Request) {
		name := request.URL.Query().Get("name")
		fileName := "config/test.txt"
		zipName := "config/test.zip"
		if name != "" {
			fileName = "config/" + name
			log.Println(fileName)
		} else {
			if util.CheckFileIsDir(zipName) {
				fileName = zipName
			} else {
				util.Zip(fileName, zipName)
				fileName = zipName
			}
		}

		file, err := os.Open(fileName)
		if err != nil {
			io.WriteString(writer, "")
			return
		}
		fileHeader := make([]byte, 512)
		file.Read(fileHeader)
		fileStat, err := file.Stat()
		if err != nil {
			io.WriteString(writer, "")
			return
		}
		writer.Header().Set("Content-Disposition", "attachment; filename="+fileName)
		writer.Header().Set("Content-Type", http.DetectContentType(fileHeader))
		writer.Header().Set("Content-Length", strconv.FormatInt(fileStat.Size(), 10))
		file.Seek(0, 0)
		io.Copy(writer, file)
	})

	//http.Handle("/", http.FileServer(http.Dir("static")))
	util.Handler.Handlers = append(util.Handler.Handlers, http.FileServer(http.Dir("static")))
	http.Handle("/", util.Handler)
}
func Start() {
	ports := make([]string, 3)
	ports[0] = ":8080"
	ports[1] = ":8081"
	ports[2] = ":8082"
	util.Start(router, ports, nil) //404
	//StartHttpServer(router,ports)
}
