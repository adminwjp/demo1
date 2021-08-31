<template>
           <!-- 开始 添加 修改 -->
        <el-dialog :title="dialog.title" :visible.sync="dialog.visible" width="30%" :before-close="handleDialogClose">
            <el-radio-group v-model="labelPosition" size="small">
                <el-radio-button label="left">左对齐</el-radio-button>
                <el-radio-button label="right">右对齐</el-radio-button>
                <el-radio-button label="top">顶部对齐</el-radio-button>
            </el-radio-group>
            <div style="margin: 20px;"></div>
            <el-form :model="formSubmit" status-icon :rules="formRoleValidator" ref="formSubmit" enctype="multipart/form-data" :disabled="disabled" class="demo-ruleForm" :label-position="labelPosition" label-width="80px">
                <el-input type="hidden" v-model="formSubmit.id"></el-input>
                <el-form-item label="账户" prop="account">
                    <el-input v-model="formSubmit.account" :disabled="account_disabled"></el-input>
                </el-form-item>
                <el-form-item label="昵称" prop="nick_name">
                    <el-input v-model="formSubmit.nick_name"></el-input>
                </el-form-item>
                <el-form-item label="角色" prop="role.id">
                    <el-cascader v-model="formSubmit.role.id" placeholder="角色分类" ref="refHandleForm"
                                 :options="role_options"
                                 :props="{  checkStrictly: true,expandTrigger: 'hover' }"
                                 @change="handleRoleFormChangeEvent" @visible-change="handleRoleVisableChangeEvent" clearable>
                        <template slot-scope="{ node, data }">
                            <span style="float: left" @click="handleRoleFormClickEvent(data.value)">{{ data.label }}</span>
                            <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
                        </template>
                    </el-cascader>
                </el-form-item>
                <el-form-item label="姓名" prop="real_name">
                    <el-input v-model="formSubmit.real_name"></el-input>
                </el-form-item>
                <el-form-item label="头像" prop="head_pic">
                    <el-upload class="avatar-uploader" accept="image/jpeg,image/jpg,image/png,image/gif" ref="upload"
                               :action="submit_url" :data="formSubmit" name="head_pic"
                               :show-file-list="false" :auto-upload="false"
                               :http-request="upload" :on-change="onChange" >
                        <img v-if="head_pic" :src="head_pic" class="avatar">
                        <i v-else class="el-icon-plus avatar-uploader-icon avatar"></i>
                    </el-upload>
                </el-form-item>
                <el-form-item label="出生日期" prop="birthday">
                    <el-col :span="11">
                        <el-date-picker type="date" placeholder="选择日期" v-model="formSubmit.birthday" style="width: 100%;"></el-date-picker>
                    </el-col>
                </el-form-item>
                <el-form-item label="手机号码" prop="phone">
                    <el-input type="tel" v-model="formSubmit.phone"></el-input>
                </el-form-item>
                <el-form-item label="性别" prop="sex">
                    <el-radio-group v-model="formSubmit.sex">
                        <el-radio label="男"></el-radio>
                        <el-radio label="女"></el-radio>
                    </el-radio-group>
                </el-form-item>
                <el-form-item label="邮箱" prop="email">
                    <el-input type="email" v-model="formSubmit.email"></el-input>
                </el-form-item>
                <el-form-item label="描述" prop="description">
                    <el-input type="textarea" v-model="formSubmit.description" value="这个人很懒,什么也没留下!"></el-input>
                </el-form-item>
                <el-form-item label="密码" prop="password">
                    <el-input type="password" v-model="formSubmit.password"></el-input>
                </el-form-item>
                <el-form-item label="确认密码" prop="enter_password">
                    <el-input type="password" v-model="formSubmit.enter_password"></el-input>
                </el-form-item>
                <el-form-item>
                    <el-button type="primary" @click="submitFormEvent('formSubmit')">{{dialog.submitText}}</el-button>
                    <el-button @click="resetFormEvent('formSubmit')">{{dialog.resetText}}</el-button>
                </el-form-item>
            </el-form>
        </el-dialog>
        <!-- 结束 添加 修改 -->
</template>
<script>
import {methods} from "../../../js/admin";
// 角色分类改变 下拉框出现/隐藏时触发 事件
methods.handleRoleVisableChangeEvent = function (value) {
  this.$parent.handleRoleVisableChangeEvent(value);
};
// 表单 角色分类改变 当选中节点变化时触发事件
methods.handleRoleFormChangeEvent = function (value) {
  this.$parent.handleRoleFormChangeEvent(value);
};
// 表单 角色分类改变 当选中节点点击时触发事件
methods.handleRoleFormClickEvent = function (value) {
  this.$parent.handleRoleFormClickEvent(value);
};
methods.upload = function(file) {
  this.$parent.function(file);
};
methods.onChange = function (file, fileList) {
  this.$parent.onChange(file, fileList);
};
export default {
  name: "dialogForm",
  props: ["formSubmit", "dialog", "admin_options", "test", "disabled", "formRoleValidator", "role_options", "account_disabled", "submit_url", "head_pic"],
  methods: methods,
  watch: {

  },
  data: function() {
    return {labelPosition: "right"};
  }

}
</script>
<style scoped>
 .avatar-uploader .el-upload {
            border: 1px dashed #d9d9d9;
            border-radius: 6px;
            cursor: pointer;
            position: relative;
            overflow: hidden;
        }

            .avatar-uploader .el-upload:hover {
                border-color: #409EFF;
            }

        .avatar-uploader-icon {
            font-size: 28px;
            color: #8c939d;
            width: 178px;
            height: 178px;
            line-height: 178px;
            text-align: center;
        }
        .avatar {
            width: 178px;
            height: 178px;
            display: block;
        }
</style>