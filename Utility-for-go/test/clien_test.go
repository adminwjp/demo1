package test

import (
	"bufio"
	"fmt"
	"io"
	"log"
	"net"
	"os"
	"strings"
	"testing"
	"time"
)

func ioCopy(dst io.Writer, src io.Reader) {
	if _, err := io.Copy(dst, src); err != nil {
		log.Fatal(err)
	}
}
func sendMsg(conn net.Conn, ch <-chan string)  {
	for msg:=range  ch{
		fmt.Fprintln(conn,msg)
	}
}
//10 min 限制
func TestClient(t *testing.T){
	//异常 怎么重连
	defer func() {
		if err := recover(); err != nil{
			_testClient(t)
		}
	}()
	defer _testClient(t)
}
//10 min ex
func _testClient(t *testing.T){
	//t.Parallel()
	serverAddr:="127.0.0.1:8000"
	client,err:=net.Dial("tcp",serverAddr)
	if err!=nil{
		t.Errorf("client connect server fail,error:%s",err)
	}
	defer client.Close() // 关闭连接
	t.Logf("client connect server success")
	enable:=false
	//不能 用 tcp 阻塞了

	if enable {
		go ioCopy(os.Stdout, client)
		ioCopy(client, os.Stdin)
	}

	// 客户端发送单行数据，然后就退出
	reader := bufio.NewReader(client)// 标准输入（终端）
	// 再将line发送给服务器
	_, err = client.Write([]byte("login\n"))
	if err!=nil{
		fmt.Println("conn write err =", err)
	}
	for{
		// 从终端读取一行用户输入，并发送给服务器
		fmt.Print(">")
		line, err := reader.ReadString('\n')
		if err!=nil{
			fmt.Println("reading string err =", err)
			time.Sleep(1)
			continue
		}
		fmt.Println(line)
		// 如果用户输入的是exit，客户端就退出
		line = strings.Trim(line, "\r\n")
		if line=="exit"{
			return
		}
		msg:=""
		switch line {
			case "1": msg="login success"
			case "test": msg="write success"
		}
		// 再将line发送给服务器
		_, err = client.Write([]byte(msg+"\n"))
		if err!=nil{
			fmt.Println("conn write err =", err)
			time.Sleep(1)
			continue
		}
		 //fmt.Printf("客户端发送了%s字节", msg)
	}
	return
}
