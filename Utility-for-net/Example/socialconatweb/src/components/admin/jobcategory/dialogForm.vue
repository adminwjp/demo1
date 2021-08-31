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
                <el-form-item label="职位名称" prop="category">
                    <el-input v-model="formSubmit.category"></el-input>
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
import {methods} from "../../../js/admin"
export default {
  name: "dialogForm",
  props: ["formSubmit", "dialog", "admin_options", "test", "disabled", "formRoleValidator"],
  methods: methods,
  watch: {

  },
  data: function() {
    return {labelPosition: "right"};
  }

}
</script>