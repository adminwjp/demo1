package news

import (
	"io"
	"log"
	"net/http"
	"os"
	"strconv"
	"strings"
	"time"
	"utility/util"
)

func Start() {

	http.HandleFunc("/test", util.HTTPHandlerFuncInterceptor(util.ServeHTTP))
	http.HandleFunc("/down", func(writer http.ResponseWriter, request *http.Request) {
		writer.Header().Set("Access-Control-Allow-Origin", "*") //允许访问所有域
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

	http.HandleFunc("/img/", func(writer http.ResponseWriter, request *http.Request) {
		writer.Header().Set("Access-Control-Allow-Origin", "*") //允许访问所有域
		println(request.URL.Path)
		names := strings.Split(request.URL.Path, "/")
		name := names[len(names)-1]
		if strings.Index(name, ".jpg") < 0 {
			name = name + ".jpg"
		}
		buffer, err := os.ReadFile("static/imgs/" + name)
		if err != nil {
			io.WriteString(writer, "")
			return
		}
		writer.Write(buffer)
	})

	//http.Handle("/", http.FileServer(http.Dir("static")))
	util.Handler.Handlers = append(util.Handler.Handlers, http.FileServer(http.Dir("static")))
	http.Handle("/", util.Handler)

	http.HandleFunc("/catalog/insert", InsertCatalog)
	http.HandleFunc("/catalog/insert_many", InsertManyCatalog)
	http.HandleFunc("/catalog/find", FindCatalog)

	http.HandleFunc("/news/insert", InsertNewsItem)
	http.HandleFunc("/news/insert_many", InsertManyNewsItem)
	http.HandleFunc("/news/find/", FindNewsItem)
	cfg := util.ConfigInstance
	cf := cfg.LoadFile("config/cfg.ini")
	if cfg.Cfg == nil {
		log.Println("load cfg suc,but point is null ")
	}

	port := util.GetIntValue(cf, "Server", "ServerPort", 8080)
	server := &http.Server{
		Addr:           ":" + strconv.Itoa(port),
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
