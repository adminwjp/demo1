<template>
         <!-- 开始 添加 修改 -->
        <el-dialog :title="dialog.title" :visible.sync="dialog.visible" width="30%" :before-close="handleDialogClose">
            <el-radio-group v-model="labelPosition" size="small">
                <el-radio-button label="left">左对齐</el-radio-button>
                <el-radio-button label="right">右对齐</el-radio-button>
                <el-radio-button label="top">顶部对齐</el-radio-button>
            </el-radio-group>
            <div style="margin: 20px;"></div>
            <el-form :model="formSubmit" status-icon :rules="formRoleValidator" ref="formSubmit"  class="demo-ruleForm" :disabled="disabled" :label-position="labelPosition" label-width="80px">
                <el-input type="hidden" v-model="formSubmit.id"></el-input>
                <el-form-item label="文件信息" prop="src">
                    <el-upload class="avatar-uploader" ref="upload" :auto-upload="false" :data="formSubmit"
                               :show-file-list="false" :action="submit_url" :http-request="upload"
                                :on-change="handleUploadSuccess">
                        <img v-if="file" :src="file" class="avatar">
                        <i v-else class="el-icon-plus avatar-uploader-icon avatar"></i>
                    </el-upload>
                </el-form-item>
                <template v-if="test">
                    <el-form-item label="管理员" prop="admin.id">
                        <el-select v-model="formSubmit.admin.id" filterable placeholder="请选择" @change="handleAdminChangeEvent" @visible-change="handleAdminVisableChangeEvent">
                            <el-option v-for="item in admin_options" :key="item.id"
                                       :label="item.category" :value="item.id">
                            </el-option>
                        </el-select>
                    </el-form-item>
                </template>
                <el-form-item label="文件分类" prop="category.id">
                    <el-select v-model="formSubmit.category.id" placeholder="请选择" @change="handleFileCategoryFormChangeEvent" @visible-change="handleFileCategoryVisableChangeEvent">
                        <el-option v-for="it in category_options" :key="it.id" :label="it.category" :value="it.id">
                        </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="描述" prop="description">
                    <el-input type="textarea" v-model="formSubmit.description" value="这个人很懒,什么也没留下!"></el-input>
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
methods.upload = function (file) {
  this.$parent.upload(file);
};
// 下拉框选项值改变事件
methods.handleFileCategoryFormChangeEvent = function (value) {
  this.$parent.handleFileCategoryFormChangeEvent(value);
};
// 下拉框显示隐藏改变事件
methods.handleFileCategoryVisableChangeEvent = function (visable) {
  this.$parent.handleFileCategoryVisableChangeEvent(visable);
};
methods.handleUploadSuccess = function (file) {
  this.$parent.handleUploadSuccess(file);
};
export default {
  name: "dialogForm",
  props: ["formSubmit", "dialog", "admin_options", "test", "disabled", "formRoleValidator", "category_options", "submit_url", "file"],
  methods: methods,
  watch: {

  },
  data: function() {
    return { labelPosition: "right" };
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