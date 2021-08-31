<template>
  <div class="login-container">
    <el-form
      :model="loginForm"
      status-icon
      :rules="loginRules"
      ref="loginForm"
      label-width="100px"
      class="demo-ruleForm"
    >
      <el-form-item label="账户" prop="account">
        <el-input v-model="loginForm.account" autocomplete="off"></el-input>
      </el-form-item>
      <el-form-item label="密码" prop="password">
        <el-input type="password" v-model="loginForm.password" autocomplete="off"></el-input>
      </el-form-item>
      <el-form-item label="记住密码">
        <el-switch v-model="loginForm.remember"></el-switch>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @@click="submitForm('loginForm')">登录</el-button>
        <el-button @@click="resetForm('loginForm')">重置</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>
<script>
export default {
  name: "login",
  data: function() {
    return {
      loginForm: {
        account: "",
        password: "",
        remember: false
      },
      enable: false,
      loginRules: {
        account: [
          { required: true, message: "请输入账户", trigger: "blur" },
          {
            min: 5,
            max: 20,
            message: "长度在 5 到 20 个字符账户",
            trigger: "blur"
          }
        ],
        password: [
          { required: true, message: "请输入密码", trigger: "blur" },
          {
            min: 6,
            max: 20,
            message: "长度在 6 到 20 个字符密码",
            trigger: "blur"
          }
        ]
      }
    };
  },
  method: {
    submitForm(formName) {
      this.$refs[formName].validate(valid => {
        if (valid) {
        } else {
          console.log("error submit!!");
          return false;
        }
      });
    },
    resetForm(formName) {
      this.$refs[formName].resetFields();
    }
  }
};
</script>