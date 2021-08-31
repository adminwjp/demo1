<template>
               <!-- 开始 添加 修改 -->
        <el-dialog  :title="dialog.title" :visible.sync="dialog.visible" width="30%" :before-close="handleDialogClose">
            <el-radio-group v-model="labelPosition" size="small">
                <el-radio-button label="left">左对齐</el-radio-button>
                <el-radio-button label="right">右对齐</el-radio-button>
                <el-radio-button label="top">顶部对齐</el-radio-button>
            </el-radio-group>
            <div style="margin: 20px;"></div>
            <el-form :model="formSubmit" status-icon :rules="formRoleValidator" ref="formSubmit" :disabled="disabled" class="demo-ruleForm" :label-position="labelPosition" label-width="80px">
                <el-input type="hidden" v-model="formSubmit.id"></el-input>
                <el-form-item label="工作公司" prop="company_name">
                    <el-input v-model="formSubmit.company_name"></el-input>
                </el-form-item>
                <el-form-item label="工作名称" prop="job">
                    <el-input v-model="formSubmit.job"></el-input>
                </el-form-item>
                <el-form-item label="分类名称" prop="category.id">
                    <el-select v-model="formSubmit.category.id" placeholder="请选择" @change="handleWorkFormChangeEvent" clearable @visible-change="handleWorkFormVisableChangeEvent">
                        <el-option v-for="it in job_options" :key="it.id" :label="it.category" :value="it.id">
                        </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="工作时间" prop="work_date">
                    <el-date-picker v-model="formSubmit.work_date" type="datetimerange" value-format="yyyy-MM-dd HH:mm:ss" :picker-options="createDatePickerOptions"
                                    range-separator="至" start-placeholder="开始日期" align="right">
                    </el-date-picker>
                </el-form-item>
                <el-form-item label="用户" prop="user.id">
                    <el-select v-model="formSubmit.user.id" filterable placeholder="请选择" @change="handleUserChangeEvent" @visible-change="handleUserVisableChangeEvent">
                        <el-option v-for="item in user_options" :key="item.id"
                                   :label="item.category" :value="item.id">
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
import {createDatePickerOptions} from "../../../js/base";
import { methods } from "../../../js/admin";
methods.handleWorkFormChangeEvent = function(val) {
  this.$parent.handleWorkFormChangeEvent(val);
};
methods.handleWorkFormVisableChangeEvent = function(val) {
  this.$parent.handleWorkFormVisableChangeEvent(val);
};
methods.handleUserChangeEvent = function(val) {
  this.$parent.handleUserChangeEvent(val);
};
methods.handleUserVisableChangeEvent = function(val) {
  this.$parent.handleUserVisableChangeEvent(val);
};
export default {
  name: "dialogForm",
  props: [
    "formSubmit",
    "dialog",
    "user_options",
    "job_options",
    "test",
    "disabled",
    "formRoleValidator"
  ],
  methods: methods,
  data: function() {
    return {
      createDatePickerOptions: createDatePickerOptions,
      labelPosition: "right"
    };
  },
  watch: {}
};
</script>
