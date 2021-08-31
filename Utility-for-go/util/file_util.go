package util

import (
	"archive/zip"
	"bufio"
	"io"
	"io/fs"
	"io/ioutil"
	"log"
	"os"
	"path/filepath"
	"strings"
)

func  CheckFileIsExists(file string)bool  {
	if fi,err:=os.Stat(file);err==nil{
		fi.Size()
		return true
	}
	return  false
}

func  CheckFileIsDir(file string)bool  {
	fi,err:=os.Stat(file)
	if err!=nil{
		return false
	}
	return  fi.IsDir()
}

func  AppendFile(file string,buffer []byte)bool  {
	var f *os.File
	var err error
	if CheckFileIsExists(file){
		f, err = os.OpenFile(file, os.O_APPEND|os.O_CREATE|os.O_WRONLY, 0666) //打开文件

	}else{
		f, err = os.Create(file) //创建文件
	}
	if err!=nil{
		return  false
	}
	defer  f.Close()
	r, err:=f.Write(buffer)
	if err!=nil{
		return false
	}
	f.Sync()
	log.Println("AppendFile %s file  %d byte success ",file,r)
	return  true
}

//error batch write fail
func  HandlerAppendFile(f *os.File,file string,buffer []byte)bool  {
	var err error
	if CheckFileIsExists(file){
		f, err = os.OpenFile(file, os.O_APPEND|os.O_CREATE|os.O_WRONLY, 0666) //打开文件

	}else{
		f, err = os.Create(file) //创建文件
	}
	if err!=nil{
		return  false
	}
	r, err:=f.Write(buffer)
	if err!=nil{
		return false
	}
	log.Println("HandlerAppendFile %s file  %d byte success ",file,r)
	return  true
}



func  Append(file string,buffer []byte)bool  {
	err:=ioutil.WriteFile(file,buffer,0666)
	if err!=nil{
		return  false
	}
	log.Println("Append %s file  success ",file)
	return  true
}

func  WriteFile(file string,buffer []byte)bool  {
	f, err := os.Create(file) //创建文件
	if err!=nil{
		return  false
	}
	defer  f.Close()
	r, err:=f.Write(buffer)
	if err!=nil{
		return false
	}
	f.Sync()
	log.Println("WriteFile %s file  %d byte success ",file,r)
	return  true
}

func  Write(file string,buffer []byte)bool  {
	f, err := os.Create(file) //创建文件
	if err!=nil{
		return  false
	}
	defer  f.Close()
	w := bufio.NewWriter(f) //创建文件
	r, err:=w.Write(buffer)
	w.Flush()
	if err!=nil{
		return false
	}
	log.Println("wite %s file  %d byte success ",file,r)
	return  true
}

func Zip(srcFile string,descZip string) bool{
	zipFile,err:=os.Create(descZip)
	if err!=nil{
		return  false
	}
	defer zipFile.Close()
	archive:=zip.NewWriter(zipFile)
	defer archive.Close()
	err=filepath.Walk(srcFile, func(path string, info fs.FileInfo, err error) error {
		if err!=nil{
			return err
		}
		header,err:=zip.FileInfoHeader(info)
		if err!=nil{
			return err
		}
		header.Name=strings.TrimPrefix(path,filepath.Dir(srcFile)+"/")
		if info.IsDir(){
			header.Name+="/"
		}else{
			header.Method=zip.Deflate
		}
		writer,err:=archive.CreateHeader(header)
		if err!=nil{
			return err
		}
		if !info.IsDir(){
			file,err:=os.Open(path)
			if err!=nil{
				return err
			}
			defer file.Close()
			_,err =io.Copy(writer,file)
		}
		return  err
	})
	if err!=nil{
		return  false
	}
	return  true
}

func UnZip(zipFile string,destDir string)bool{
	zipReader,err:=zip.OpenReader(zipFile)
	if err!=nil{
		return  false
	}
	defer  zipReader.Close()
	for _,f:=range zipReader.File{
		fpath:=filepath.Join(destDir,f.Name)
		if f.FileInfo().IsDir(){
			os.MkdirAll(fpath,os.ModePerm)
		}else{
			if err=os.MkdirAll(filepath.Dir(fpath),os.ModePerm);err!=nil{
				return  false
			}
			inFile,err:=f.Open()
			if err!=nil{
				return  false
			}
			defer  inFile.Close()
			outFile,err:=os.OpenFile(fpath,os.O_WRONLY|os.O_CREATE|os.O_TRUNC,f.Mode())
			if err!=nil{
				return  false
			}
			defer  outFile.Close()
			_,err =io.Copy(outFile,inFile)
		}
	}
	return true
}