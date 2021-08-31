import requests

from enum import Enum
import json

class HttpResultFlag(Enum):
    string=0
    byte=1

class HttpEntity:
    # 用户代理
    user_agent="Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36"
    # 接受 类型
    accept="text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9"
    # 提交 类型
    content_type="application/x-www-form-urlencoded"
    cookie={} # cookie
    referer=""
    url=""
    retry_count=3
    method="get"
    send_encoding="utf-8"
    # https://segmentfault.com/q/1010000004114305/a-1020000004114551
    stream=False #大文件下载时 使用 true
    revice_encoding="utf-8"
    flag=HttpResultFlag.string
    param=''
    #proxies={"https":"101.236.54.97:8866","http":"101.236.54.97:8866"}
    proxies={}
    headers={}



class HttpResult:
    flag=HttpResultFlag.string
    string_result=''
    byte_result=[]
    cookie={} # cookie


import requests,time


class HttpHelper :
    ip=""
    port=0
    #请求 带 cookie 
    seesion = requests.session()

    def get(self, url, http_entity=None):
       return self.get(url,"","",http_entity)
    
    def get(self, url,referer, http_entity=None):
       return self.get(url,"",referer,http_entity)

    def http(self,http_entity):
        http_entity.method=http_entity.method.lower()
        headers={
             "User-Agent":http_entity.user_agent,
             "Accept": http_entity.accept,
        }
        if http_entity.method.lower()=='post':
            headers['Content-Type']=http_entity.content_type
        if http_entity.referer!='' and http_entity.referer==None:
            headers['Referer']=http_entity.referer
        fail=0
        http_result=HttpResult()
        while fail <= http_entity.retry_count :
            try:
                response=None
                if http_entity.method=='post':
                    if http_entity.content_type.indexOf('from')>-1:
                        # from
                        response = self.seesion.post(http_entity.url, http_entity.param,cookies=http_entity.cookie,timeout=5, proxies=http_entity.proxies, headers=headers,stream=http_entity.stream)
                    else:
                        # json
                        if isinstance(http_entity.param,dict):
                            #{"a":1}
                            response = self.seesion.post(http_entity.url, cookies=http_entity.cookie,timeout=5, json=http_entity.param, proxies=http_entity.proxies, headers=headers,stream=http_entity.stream)
                        else:
                            #'{"a":1}'
                            response = self.seesion.post(http_entity.url, cookies=http_entity.cookie,timeout=5,data= http_entity.param, proxies=http_entity.proxies, headers=headers,stream=http_entity.stream)
                else :
                    response = self.seesion.get(http_entity.url,cookies=http_entity.cookie,timeout=5,params=http_entity.param,proxies=http_entity.proxies, headers=headers,stream=http_entity.stream)
                #else :
                #    response = self.seesion.get(http_entity.url,cookies=http_entity.cookie,timeout=5,proxies=http_entity.proxies, headers=headers,stream=http_entity.stream)
                http_entity.cookie = requests.utils.dict_from_cookiejar(response.cookies)
                
                http_result.cookie=http_entity.cookie
                http_result.flag=http_entity.flag
                if http_entity.flag==HttpResultFlag.string:
                   if http_entity.revice_encoding != "utf-8" :
                        response.encoding = http_entity.revice_encoding
                   http_result.string_result= response.text
                if http_entity.flag==HttpResultFlag.byte: 
                    if http_entity.stream==False:
                        http_result.byte_result=response.content
                    else :
                       for chunk in response.iter_content(chunk_size=1024): # 每次加载1024字节
                         http_result.byte_result=http_result.byte_result.extend(chunk)
                return http_result
            except:
                fail=fail+1
                time.sleep(0.05) #s
        if http_entity.flag==HttpResultFlag.string:
            http_result.string_result='timeout'
        return http_result

    def get(self, url,params, referer, http_entity=None):
       if http_entity==None:
        http_entity=HttpEntity()
       http_entity.url=url
       http_entity.param=params
       http_entity.referer=referer
       http_result=self.http(http_entity)
       return http_result.string_result

    def post(self, url, params, referer, http_entity=None):
       if http_entity==None:
        http_entity=HttpEntity()
       http_entity.url=url
       http_entity.method='post'
       http_entity.param=params
       http_entity.referer=referer
       http_result=self.http(http_entity)
       return http_result.string_result

    def post_json(self, url, json, referer, http_entity=None):
       if http_entity==None:
        http_entity=HttpEntity()
       http_entity.url=url
       http_entity.content_type='application/json'
       http_entity.method='post'
       http_entity.param=params
       http_entity.referer=referer
       http_result=self.http(http_entity)
       return http_result.string_result

    def post_json_by_data(self, url, data, referer, http_entity=None):
       return self.post_json(url,data,referer,http_entity)
