<template>
  <!-- 开始 添加 修改 -->
  <el-dialog
    :title="dialog.title"
    :visible.sync="dialog.visible"
    width="30%"
    :before-close="handleDialogClose"
  >
    <el-radio-group v-model="labelPosition" size="small" >
      <el-radio-button label="left">左对齐</el-radio-button>
      <el-radio-button label="right">右对齐</el-radio-button>
      <el-radio-button label="top">顶部对齐</el-radio-button>
    </el-radio-group>
    <div style="margin: 20px;"></div>
    <el-form
      :model="formSubmit"
      status-icon
      :rules="formRoleValidator"
      ref="formSubmit"
      class="demo-ruleForm"
      :disabled="disabled"
      :label-position="labelPosition"
      label-width="80px"
    >
      <el-input type="hidden" v-model="formSubmit.id"></el-input>
      <el-form-item label="角色名称" prop="category">
        <el-input
          v-model="formSubmit.category"
          placeholder="角色名称"
        ></el-input>
      </el-form-item>
      <template v-if="test">
        <el-form-item label="管理员" prop="admin.id">
          <el-select
            v-model="formSubmit.admin.id"
            filterable
            placeholder="请选择"
            @change="handleAdminChangeEvent"
            @visible-change="handleAdminVisableChangeEvent"
          >
            <el-option
              v-for="item in admin_options"
              :key="item.id"
              :label="item.category"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </el-form-item>
      </template>
     <el-form-item label="角色分类" prop="role.id">
                    <el-cascader v-model="formSubmit.role.id" placeholder="角色分类" ref="refHandleForm"
                                    :options="role_options" change-on-select :show-all-levels="false" filterable
                                    :props="{  checkStrictly: true,expandTrigger: 'hover' }"
                                    @change="handleRoleFormChangeEvent" @visible-change="handleRoleVisableChangeEvent" :clearable="true">
                        <!--改方式无法验证下拉选框值改变验证 提交时验证可以通过 -->
                        <template slot-scope="{ node, data }">
                            <span style="float: left" @click="handleRoleFormClickEvent(data.value)">{{ data.label }}</span>
                            <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
                        </template>
                    </el-cascader>
                </el-form-item>
      <el-form-item label="描述" prop="description">
        <el-input
          type="textarea"
          v-model="formSubmit.description"
          value="这个人很懒,什么也没留下!"
        ></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="submitFormEvent('formSubmit')">{{
          dialog.submitText
        }}</el-button>
        <el-button @click="resetFormEvent('formSubmit')">{{
          dialog.resetText
        }}</el-button>
      </el-form-item>
    </el-form>
  </el-dialog>
  <!-- 结束 添加 修改 -->
</template>
<script>
import { methods } from "../../../js/admin";
// 分类打开时触发事件
methods.handleRoleFormChangeEvent = function(val) {
  this.$parent.handleRoleFormChangeEvent(val);
};
// 分类改变值时触发事件
methods.handleRoleVisableChangeEvent = function(val) {
  this.$parent.handleRoleVisableChangeEvent(val);
};
// 分类点击触发事件
methods.handleRoleFormClickEvent = function(val) {
  this.$parent.handleRoleFormClickEvent(val);
};
export default {
  name: "dialogForm",
  props: [
    "formSubmit",
    "dialog",
    "admin_options",
    "test",
    "disabled",
    "formRoleValidator",
    "role_options"
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
