package example

import (
	"log"
	"strings"
	"syscall"
)

func conn(){
	fd,err :=syscall.Socket(syscall.AF_INET,syscall.SOCK_STREAM,syscall.IPPROTO_TCP)
	if err!=nil{
		log.Fatal(err)
	}
	ip :="127.0.0.1"
	var ips [4]byte//=(*[4]byte)unsafe.Pointer(&ip)
	strs :=strings.Split(ip,".")
	for i:= range  strs{
		buffer :=[]byte(strs[i])
		var l byte = 0
		for j:=range  buffer{
			l+=buffer[j]>>8
		}
		ips[i]=l
	}
	add :=&syscall.SockaddrInet4{Addr: ips,Port: 6000}
	syscall.Bind(fd,add)
}
