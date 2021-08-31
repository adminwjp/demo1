function  Utility(){

    function parseDate(date) {
        var dateObj = new Date(date);
        var time = dateObj.getFullYear() + "-" + (dateObj.getMonth() + 1) + "-" + dateObj.getDate() + " " + dateObj.getHours() + ":" +
            dateObj.getMinutes() + ":" + dateObj.getSeconds();
        return time;
    }
    function totalMill(date) {
        var dateObj = new Date(date);

        return dateObj.getTime();
    }
    function TokenUtil(){
        //token 信息 存储cookie中 获取 localStorage中
        const tokenKey = "token";
        /**
         * 获取token缓存信息
         * @returns
         */
        function getToken() {
            return !Cookies ? localStorage.getItem(tokenKey) : Cookies.get(tokenKey);
        }
        /**
         * 设置token缓存信息
         * @param token toekn信息
         * @returns
         */
        function setToken(token) {
            if (!Cookies) localStorage.setItem(tokenKey, token);
            else Cookies.set(tokenKey, token);
        }
        /**
         * 移除token缓存信息
         * @returns
         */
        function removeToken() {
            if (!Cookies) localStorage.removeItem(tokenKey);
            else Cookies.remove(tokenKey);
        }
        return {
            get:getToken,
            set:setToken,
            remove:removeToken()
        }
    }
    function ImgUtil(){
        /**
         * 网络图像文件转Base64
         */
        function getBase64Image(img) {
            var canvas = document.createElement("canvas");
            canvas.width = img.width;
            canvas.height = img.height;
            var ctx = canvas.getContext("2d");
            ctx.drawImage(img, 0, 0, img.width, img.height);
            var ext = img.src.substring(img.src.lastIndexOf(".") + 1).toLowerCase();
            var dataURL = canvas.toDataURL("image/" + "jpg");
            return dataURL;
        }
        /**
         *Base64字符串转二进制
         */
        function dataURLtoBlob(dataurl) {
            var arr = dataurl.split(','),
                mime = arr[0].match(/:(.*?);/)[1],
                bstr = atob(arr[1]),
                n = bstr.length,
                u8arr = new Uint8Array(n);
            while (n--) {
                u8arr[n] = bstr.charCodeAt(n);
            }
            return new Blob([u8arr], {
                type: mime
            });
        }

        function dataURLtoFile(dataurl, filename) {//将base64转换为文件
            var arr = dataurl.split(','), mime = arr[0].match(/:(.*?);/)[1],
                bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);
            while (n--) {
                u8arr[n] = bstr.charCodeAt(n);
            }
            return new File([u8arr], filename, { type: mime });
        }

        return {
            base64:getBase64Image,
            byte:dataURLtoBlob,
            file:dataURLtoFile
        }
    }
    function PostUtil(){
        function parseParam(obj, skipOptions, timeOptions) {
            return parse("", obj, skipOptions, timeOptions);
        }

        function parseRequestParam(requestContentType, param, skipOptions, timeOptions) {
            var paramOptions = {};
            if (requestContentType == 1) {
                paramOptions.parem = param;
                paramOptions.contenType = ContentTypeJson;
            } else if (requestContentType == 2) {
                paramOptions.parem = parseParam(param, skipOptions, timeOptions);
                paramOptions.contenType = ContentTypeForm;
            } else {
                var formData = new FormData();
                parseFormData(formData, "", param, skipOptions, timeOptions);
                paramOptions.parem = formData;
                paramOptions.contenType = ContentTypeMultipart;
            }
            return paramOptions;
        }



        function parseFormData(formData, prefix, obj, skipOptions, timeOptions) {
            var str = "";
            for (var i in obj) {
                if (skipOptions && skipOptions.indexOf(i) > -1) continue;
                if (obj[i] instanceof Date) {
                    formData.append(prefix + i.toString(), totalMill(obj[i.toString()]));
                    //formData.append(prefix + i.toString(), parseDate(obj[i.toString()]));
                }
                else if (obj[i] instanceof Array) {
                    if (obj[i] && obj[i].length > 0) {
                        obj[i].map(it => {
                            if (timeOptions && timeOptions.indexOf(i.toString()) > -1) {
                                formData.append(prefix + i.toString(), parseDate(it));
                            } else {
                                formData.append(prefix + i.toString(), it.toString());
                            }
                        });
                    }
                    // else {
                    //   formData.append(prefix + i.toString() , encodeURI("[]"));
                    //   formData.append(prefix + i.toString() , null);
                    //   formData.append(prefix + i.toString(), null);
                    // }
                } else if (obj[i] instanceof Object) {
                    str += parseFormData(formData, prefix + i.toString() + ".", obj[i]);
                } else {
                    if (obj[i.toString()] && timeOptions && timeOptions.indexOf(i.toString()) > -1) {
                        formData.append(prefix + i.toString(), parseDate(obj[i.toString()]));
                    } else if (obj[i.toString()]) {
                        formData.append(prefix + i.toString(), obj[i.toString()]);
                    }
                }
            }
            str = str.substring(0, str.length - 1);
            return str;
        }


        function parse(prefix, obj, skipOptions, timeOptions) {
            var str = "";
            for (var i in obj) {
                if (skipOptions && skipOptions.indexOf(i) > -1) continue;
                if(!obj[i]){
                    //a=&b= -> ''
                    continue;
                }
                if (obj[i] instanceof Date) {
                    str += encodeURI(prefix + i.toString()) + "=";
                    str += encodeURI(totalMill(obj[i.toString()]))+"&";
                    //str += encodeURI(parseDate(obj[i.toString()]))+"&";
                }
                else if (obj[i] instanceof Array) {
                    if (obj[i] && obj[i].length > 0) {
                        obj[i].map(it => {
                            str += encodeURI(prefix + i.toString()) + "=";
                            if (it) {
                                if (timeOptions && timeOptions.indexOf(i.toString()) > -1) {
                                    str += encodeURI(parseDate(it));
                                } else {
                                    str += encodeURI(it);
                                }
                            }
                            str += "&";
                        });
                    }
                    // else {
                    //   str += encodeURI(prefix + i.toString()) + "=" + encodeURI("[]") + "&";
                    //   str += encodeURI(prefix + i.toString()) + "=&";
                    //   str += encodeURI(prefix + i.toString()) + "=&";
                    // }
                } else if (obj[i] instanceof Object) {
                    str += parse(prefix + i.toString() + ".", obj[i]) + "&";
                } else {
                    str += encodeURI(prefix + i.toString()) + "=";
                    if (obj[i.toString()] && timeOptions && timeOptions.indexOf(i.toString()) > -1) {
                        str += encodeURI(parseDate(obj[i.toString()]));
                    } else if (obj[i.toString()]) {
                        str += encodeURI(obj[i.toString()]);
                    }
                    str += "&";
                }
            }
            str = str.substring(0, str.length - 1);
            return str;
        }
        return {
            form:parseRequestParam,
            fromData:parseFormData,
            parse:parse
        }
    }

    function  FileUtil(){
        function readFile(filepath, result) {
            //ie10 chrome
            if (window.FileReader) {
                var reader = new FileReader();
                reader.onload = function () {
                    result.content = this.result;
                    console.log(this.result);
                };
                reader.onerror = function () {
                    console.error(this.error);
                };
                reader.readAsText(filepath);
            }
            //>=ie7 <=ie10
            else if (typeof (window.ActiveXObject) != "undefined") {
                var xml = new window.ActiveXObject("Microsoft.XMLDOM");
                xml.async = false;
                xml.load(filepath);
                result.content = xml.xml;
                console.log(xml.xml);
            }
            //ff
            else if (document.implementation && document.implementation.createDocument) {
                var xml = document.implementation.createDocument("", "", null);
                xml.async = false;
                xml.load(filepath);
                result.content = xml.xml;
                console.log(xml.xml);
            } else {
                throw new Error("read file fail");
            }
        }
        return {
            read:readFile
        }
    }
    const  token=TokenUtil();
    const  img=ImgUtil()
    const  post=PostUtil()
    const  file=FileUtil()
    return {
        token:token,
        img:img,
        post: post,
        parseDate:parseDate,
        file:file,
        date:{
            toString:parseDate,
            totalMill:totalMill
        }
    }
}

const  utility=Utility();
