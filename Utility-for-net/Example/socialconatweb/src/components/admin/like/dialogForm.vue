<template>
        <!-- 开始 添加 修改 -->
        <el-dialog  :title="dialog.title" :visible.sync="dialog.visible" width="30%" :before-close="handleDialogClose">
            <el-radio-group v-model="labelPosition" size="small">
                <el-radio-button label="left">左对齐</el-radio-button>
                <el-radio-button label="right">右对齐</el-radio-button>
                <el-radio-button label="top">顶部对齐</el-radio-button>
            </el-radio-group>
            <div style="margin: 20px;"></div>
            <el-form :model="formSubmit" status-icon :rules="formRoleValidator" :disabled="disabled" ref="formSubmit" class="demo-ruleForm" :label-position="labelPosition" label-width="80px">
                <el-input type="hidden" v-model="formSubmit.id"></el-input>
                <template v-if="test">
                    <el-form-item label="分类" prop="category.id">
                        <el-select v-model="formSubmit.category.id" filterable placeholder="请选择" @change="handleLikeCategoryChangeEvent" @visible-change="handleLikeCategoryVisableChangeEvent">
                            <el-option v-for="item in category_options" :key="item.id"
                                       :label="item.category" :value="item.id">
                            </el-option>
                        </el-select>
                    </el-form-item>
                </template>
                <el-form-item label="用户" prop="user.id">
                    <el-select v-model="formSubmit.user.id" filterable placeholder="请选择" @change="handleUserChangeEvent" @visible-change="handleUserVisableChangeEvent">
                        <el-option v-for="item in user_options" :key="item.id"
                                   :label="item.category" :value="item.id">
                        </el-option>
                    </el-select>
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
methods.userCategoryQueryEvent = function () {
  this.$parent.userCategoryQueryEvent();
};
methods.handleUserChangeEvent = function (val) {
  this.$parent.handleUserChangeEvent(val);
};
methods.handleUserVisableChangeEvent = function (val) {
  this.$parent.handleUserVisableChangeEvent(val);
};
methods.handleLikeCategoryChangeEvent = function (val) {
  this.$parent.handleLikeCategoryChangeEvent(val);
};
methods.handleLikeCategoryVisableChangeEvent = function (val) {
  this.$parent.handleLikeCategoryVisableChangeEvent(val);
};
export default {
  name: "dialogForm",
  props: ["formSubmit", "dialog", "user_options", "test", "disabled", "formRoleValidator", "category_options"],
  methods: methods,
  watch: {

  },
  data: function() {
    return {labelPosition: "right"};
  }

}
</script>