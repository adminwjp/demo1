package models

import (
	"mime/multipart"
	"strings"
	"time"
	"sign/util"
)

func EmptyAttachment() Attachment{
	var  attachment=Attachment{DateCreated:time.Now().Unix()}
	return  attachment
}

func NewAttachment(file *multipart.FileHeader,contentType string) Attachment{
	var  attachment=Attachment{DateCreated:time.Now().Unix(),FileLength: file.Size}
	if contentType!=""{
		attachment.ContentType=contentType
	}else if file.Header.Get("content-type")!=""{
		attachment.ContentType=file.Header.Get("content-type")
	}
	if attachment.ContentType==""{
		attachment.ContentType="unknown/unknown"
		attachment.MediaType=Other_MediaType
	}else{
		attachment.ContentType=strings.Replace(attachment.ContentType,"pjpeg","jpeg",1)
		attachment.MediaType=attachment.GetMediaType(attachment.ContentType)
	}
	if util.GetFileExtension(file.Filename)=="" {
		switch attachment.ContentType {
		case "image/jpeg":attachment.FileName=file.Filename+".jpg"
		case "image/gif":attachment.FileName=file.Filename+".gif"
		case "image/png":attachment.FileName=file.Filename+".png"
		}
	}else{
		attachment.FileName=file.Filename
	}
	var index=util.IndexOfByString(attachment.FileName,byte('\\'))
	attachment.FriendlyFileName=util.SubstringByString(attachment.FileName,index+1)
	//自动生成用于存储的文件名
	attachment.FileName=GenerateFileName(util.GetFileExtension(attachment.FriendlyFileName))
	return  attachment
}


func NewAttachmentByFileByte(file []byte,contentType string,friendlyFileName string) Attachment{
	var  attachment=Attachment{DateCreated:time.Now().Unix(),
		FileLength: int64(len(file)),ContentType: contentType}
	attachment.MediaType=attachment.GetMediaType(contentType)
	attachment.FriendlyFileName=friendlyFileName
	attachment.FileName=GenerateFileName(util.GetFileExtension(attachment.FriendlyFileName))
	return  attachment
}