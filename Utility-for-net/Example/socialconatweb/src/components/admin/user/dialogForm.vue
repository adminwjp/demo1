<template>
       <!-- 开始 添加 修改 -->
        <el-dialog :title="dialog.title" :visible.sync="dialog.visible" width="30%" :before-close="handleDialogClose">
            <el-radio-group v-model="labelPosition" size="small">
                <el-radio-button label="left">左对齐</el-radio-button>
                <el-radio-button label="right">右对齐</el-radio-button>
                <el-radio-button label="top">顶部对齐</el-radio-button>
            </el-radio-group>
            <div style="margin: 20px;"></div>
            <el-form :model="formSubmit" status-icon :rules="formRoleValidator" ref="formSubmit" :disabled="disabled" class="demo-ruleForm" :label-position="labelPosition" label-width="80px">
                <el-input type="hidden" v-model="formSubmit.id"></el-input>
                <el-form-item label="账户" prop="account">
                    <el-input v-model="formSubmit.account"></el-input>
                </el-form-item>
                <el-form-item label="昵称" prop="nick_name">
                    <el-input v-model="formSubmit.nick_name"></el-input>
                </el-form-item>
                <el-form-item label="姓名" prop="real_name">
                    <el-input v-model="formSubmit.real_name"></el-input>
                </el-form-item>
                <el-form-item label="头像" prop="head_pic.file_id">
                    <el-upload class="avatar-uploader" accept="image/jpeg,image/jpg,image/png,image/gif" ref="uploadHeadPic"
                               :action="baseUrl+'img/upload'" :data="data_head_pic"
                               :show-file-list="false"
                               :on-success="handleHeadPicSuccess"
                               :before-upload="beforeHeadPicUpload">
                        <img v-if="formSubmit.head_pic.file_id" :src="baseUrl+'img/'+formSubmit.head_pic.file_id" class="avatar">
                        <i v-else class="el-icon-plus avatar-uploader-icon avatar"></i>
                    </el-upload>
                </el-form-item>
                <el-form-item label="出生日期" prop="birthday">
                    <el-col :span="11">
                        <el-date-picker type="date" placeholder="选择日期" v-model="formSubmit.birthday"  value-format="yyyy-MM-dd HH:mm:ss" style="width: 100%;"></el-date-picker>
                    </el-col>
                </el-form-item>
                <el-form-item label="手机号码" prop="phone">
                    <el-input  v-model="formSubmit.phone"></el-input>
                </el-form-item>
                <el-form-item label="性别" prop="sex">
                    <el-radio-group v-model="formSubmit.sex">
                        <el-radio label="男"></el-radio>
                        <el-radio label="女"></el-radio>
                    </el-radio-group>
                </el-form-item>
                <el-form-item label="学历分类" prop="edution.id">
                    <el-select v-model="formSubmit.edution.id" placeholder="请选择" @change="handleEdutionCategoryChangeEvent" @visible-change="handleEdutionCategoryVisableChangeEvent">
                        <el-option v-for="it in edution_options" :key="it.id" :label="it.category" :value="it.id">
                        </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="婚姻状态" prop="marital.id">
                    <el-select v-model="formSubmit.marital.id" placeholder="请选择" @change="handleMaritalCategoryChangeEvent" @visible-change="handleMaritalCategoryVisableChangeEvent">
                        <el-option v-for="it in marital_options" :key="it.id" :label="it.category" :value="it.id">
                        </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="邮箱" prop="email">
                    <el-input type="email" v-model="formSubmit.email"></el-input>
                </el-form-item>
                <el-form-item label="身份证号" prop="card_id">
                    <el-input type="text" v-model="formSubmit.card_id"></el-input>
                </el-form-item>
                <el-form-item label="身份证正面" prop="card_photo1.file_id">
                    <el-upload class="avatar-uploader" accept="image/jpeg,image/jpg,image/png,image/gif" ref="uploadCardPhoto1"
                               :action="baseUrl+'img/upload'" :data="data_card_photo1"
                               :show-file-list="false"
                               :on-success="handleCardPhoto1Success"
                               :before-upload="beforeCardPhoto1Upload">
                        <img v-if="formSubmit.card_photo1.file_id" :src="baseUrl+'img/'+formSubmit.card_photo1.file_id" class="avatar">
                        <i v-else class="el-icon-plus avatar-uploader-icon avatar"></i>
                    </el-upload>
                </el-form-item>
                <el-form-item label="身份证反面面" prop="card_photo2.file_id">
                    <el-upload class="avatar-uploader" accept="image/jpeg,image/jpg,image/png,image/gif" ref="uploadCardPhoto2"
                               :action="baseUrl+'img/upload'" :data="data_card_photo2"
                               :show-file-list="false"
                               :on-success="handleCardPhoto2Success"
                               :before-upload="beforeCardPhoto2Upload">
                        <img v-if="formSubmit.card_photo2.file_id" :src="baseUrl+'img/'+formSubmit.card_photo2.file_id" class="avatar">

                        <i v-else class="el-icon-plus avatar-uploader-icon avatar"></i>
                    </el-upload>
                </el-form-item>
                <el-form-item label="手持身份证正面" prop="hand_card_photo1.file_id">
                    <el-upload class="avatar-uploader" accept="image/jpeg,image/jpg,image/png,image/gif" ref="uploadHandCardPhoto1"
                               :action="baseUrl+'img/upload'" :data="data_hand_card_photo1"
                               :show-file-list="false"
                               :on-success="handleHandCardPhoto1Success"
                               :before-upload="beforeHandCardPhoto1Upload">
                        <img v-if="formSubmit.hand_card_photo1.file_id" :src="baseUrl+'img/'+formSubmit.hand_card_photo1.file_id" class="avatar">
                        <i v-else class="el-icon-plus avatar-uploader-icon avatar"></i>
                    </el-upload>
                </el-form-item>
                <el-form-item label="手持身份证反面面" prop="hand_card_photo2.file_id">
                    <el-upload class="avatar-uploader" accept="image/jpeg,image/jpg,image/png,image/gif" ref="uploadHandCardPhoto2"
                               :action="baseUrl+'img/upload'" :data="data_hand_card_photo2"
                               :show-file-list="false"
                               :on-success="handleHandCardPhoto2Success"
                               :before-upload="beforeHandCardPhoto2Upload">
                        <img v-if="formSubmit.hand_card_photo2.file_id" :src="baseUrl+'img/'+formSubmit.hand_card_photo2.file_id" class="avatar">
                        <i v-else class="el-icon-plus avatar-uploader-icon avatar"></i>
                    </el-upload>
                </el-form-item>
                <el-form-item label="银行卡号" prop="bank_id">
                    <el-input type="text" v-model="formSubmit.bank_id"></el-input>
                </el-form-item>
                <el-form-item label="用户等级分类" prop="level.id">
                    <el-select v-model="formSubmit.level.id" placeholder="请选择" @change="handleLevelCategoryChangeEvent" @visible-change="handleLevelCategoryVisableChangeEvent">
                        <el-option v-for="it in level_options" :key="it.id" :label="it.category" :value="it.id">
                        </el-option>
                    </el-select>
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
import { methods } from "../../../js/admin";
methods.handleEdutionCategoryChangeEvent = function (val) {
  this.$parent.handleEdutionCategoryChangeEvent(val);
};
methods.handleEdutionCategoryVisableChangeEvent = function (val) {
  this.$parent.handleEdutionCategoryVisableChangeEvent(val);
};
methods.handleMaritalCategoryChangeEvent = function (val) {
  this.$parent.handleMaritalCategoryChangeEvent(val);
};
methods.handleMaritalCategoryVisableChangeEvent = function (val) {
  this.$parent.handleMaritalCategoryVisableChangeEvent(val);
};
methods.handleLevelCategoryChangeEvent = function (val) {
  this.$parent.handleLevelCategoryChangeEvent(val);
};
methods.handleLevelCategoryVisableChangeEvent = function (val) {
  this.$parent.handleLevelCategoryVisableChangeEvent(val);
};
// 文件上传操作
methods.handleHeadPicSuccess = function (res, file) {
  this.$parent.handleHeadPicSuccess(res, file);
};
methods.beforeHeadPicUpload = function (file) {
  return this.$parent.beforeHeadPicUpload(file);
};
methods.handleCardPhoto1Success = function (res, file) {
  this.$parent.handleCardPhoto1Success(res, file);
};
methods.beforeCardPhoto1Upload = function (file) {
  return this.$parent.beforeCardPhoto1Upload(file);
};
methods.handleCardPhoto2Success = function (res, file) {
  this.$parent.handleCardPhoto2Success(res, file);
};
methods.beforeCardPhoto2Upload = function (file) {
  return this.$parent.beforeCardPhoto2Upload(file);
};
methods.handleHandCardPhoto1Success = function (res, file) {
  this.$parent.handleHandCardPhoto1Success(res, file);
};
methods.beforeHandCardPhoto1Upload = function (file) {
  return this.$parent.beforeHandCardPhoto1Upload(file);
};
methods.handleHandCardPhoto2Success = function (res, file) {
  this.$parent.handleHandCardPhoto2Success(res, file);
};
methods.beforeHandCardPhoto2Upload = function (file) {
  return this.$parent.beforeHandCardPhoto2Upload(file);
};
export default {
  name: "dialogForm",
  props: [
    "formSubmit",
    "dialog",
    "test",
    "disabled",
    "formRoleValidator",
    "edution_options", "level_options", "marital_options", "data_head_pic", "data_card_photo1", "data_card_photo2", "data_hand_card_photo1", "data_hand_card_photo2" ,
	"baseUrl" 
  ],
  methods: methods,
  data: function() {
    return {
      labelPosition: "right"
    };
  },
  watch: {}
};
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
