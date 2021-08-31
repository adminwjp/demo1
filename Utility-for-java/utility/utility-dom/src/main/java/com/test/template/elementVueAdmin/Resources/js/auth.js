import {
  Cookies
} from "js-cookie";
import axios from "axios";
import {baseUrl} from "../js/url";
// token 信息 存储cookie中 获取 localStorage中
const tokenKey = "token";
/**
 * 获取token缓存信息
 * @returns
 */
export function getToken() {
  return !Cookies ? localStorage.getItem(tokenKey) : Cookies.get(tokenKey);
}
/**
 * 设置token缓存信息
 * @param token toekn信息
 * @returns
 */
export function setToken(token) {
  if (!Cookies) localStorage.setItem(tokenKey, token);
  else Cookies.set(tokenKey, token);
}
export function init(){
	//request interceptor
	axios.interceptors.request.use(
		config => {
			// do something before request is sent
			var to = getToken();
			if (to) config.headers['token'] = to;
			return config;
		},
		error => {
			// do something with request error
			console.log(error); // for debug
			return Promise.reject(error);
		}
	);
}
