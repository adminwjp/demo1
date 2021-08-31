<template>
 <div class="login-container" style="position:absolute;width:500px;height:300px;top:50%;left:50%;transform:translate(-50%,-50%);">
        <el-form :model="loginForm" status-icon :rules="loginRules" ref="loginForm" label-width="100px" class="demo-ruleForm">
            <el-form-item label="账户" prop="account">
                <el-input v-model="loginForm.account" autocomplete="off"></el-input>
            </el-form-item>
            <el-form-item label="密码" prop="pwd">
                <el-input type="password" v-model="loginForm.pwd" autocomplete="off"></el-input>
            </el-form-item>
            <el-form-item label="记住密码">
                <el-switch v-model="loginForm.remember"></el-switch>
            </el-form-item>
            <el-form-item>
                <el-button type="primary" @click="submitForm('loginForm')">登录</el-button>
                <el-button @click="resetForm('loginForm')">重置</el-button>
            </el-form-item>
        </el-form>
    </div>
</template>
  <script>
  import {
  createDatePickerOptions,
  ContentTypeJson,
  ContentTypeForm,
  ContentTypeMultipart,
  get,
  post,
  resetFormEvent,
  tip,
  Gibson,
  getBase64Image,
  dataURLtoFile,
  setRoleCategory,
  parseRequestParam
} from "../../js/base";
import {getToken,setToken} from "../../js/auth";
import { baseUrl,urls, getUrl } from "../../js/url";
function createGuid() {
	var a = void 0 === arguments[0] ? 22 : arguments[0],
		b = +new Date + "",
		c = [],
		d = "0123456789ABCDEF";
	for (a = a - b.length - 1; a--;) c.push(d.charAt(16 * Math.random()));
	var e = c.join("");
	return b + "_" + e
}

export default{
	name: "login",
	data: function () {
		return {
			loginForm: {
				account: 'test123',
				pwd: 'test123',
				remember: false
			},
			enable: false,
			loginRules: {
				account: [
					{ required: true, message: '请输入账户', trigger: 'blur' },
					{ min: 5, max: 20, message: '长度在 5 到 20 个字符账户', trigger: 'blur' }
				], pwd: [
					{ required: true, message: '请输入密码', trigger: 'blur' },
					{ min: 6, max: 20, message: '长度在 6 到 20 个字符密码', trigger: 'blur' }
				]
			}
		};
	},
	methods: {
		submitForm(formName) {
			this.$refs[formName].validate((valid) => {
				if (valid) {
					post(urls.admin.login + "?returnurl=/index", this.loginForm, ContentTypeJson, reponse => {
						console.log(reponse);
						setToken(reponse.data.data.token);
						location.href = "index";

					}, reponse =>  alert(reponse.data.message) );
				} else {
					console.log('error submit!!');
					return false;
				}
			});
		},
		resetForm (formName) {
			this.$refs[formName].resetFields();
		}
	}
};
</script>
