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
                <el-form-item label="菜单名称" prop="menu_name">
                    <el-input v-model="formSubmit.menu_name"></el-input>
                </el-form-item>
                <el-form-item label="菜单分组" prop="menu_group">
                    <el-input v-model="formSubmit.menu_group"></el-input>
                </el-form-item>
                <el-form-item label="菜单链接" prop="href">
                    <el-input v-model="formSubmit.href"></el-input>
                </el-form-item>
                <el-form-item label="菜单图标" prop="icon.id">
                    <el-select v-model="formSubmit.icon.id" filterable placeholder="请选择" @change="handleIconChangeEvent" @visible-change="handleIconVisableChangeEvent">
                        <el-option v-for="item in icon_options"
                                   :key="item.label"
                                   :label="item.label"
                                   :value="item.id">
                            <span style="float: left">{{ item.label }}</span>
                            <span style="float: right; color: #8492a6; font-size: 13px"><i v-bind:class="item.value"></i></span>
                        </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="菜单分类" prop="parent.id">
                    <el-cascader v-model="formSubmit.parent.id" ref="refHandleForm"
                                 :options="options" filterable :props="{ checkStrictly: true,expandTrigger: 'hover' }"
                                 @change="handleCategoryFormChangeEvent" @visible-change="handleCategoryQueryVisableChangeEvent" clearable>
                        <template slot-scope="{ node, data }">
                            <span style="float: left" @click="handleCategotyFormClickEvent(data.value)">{{ data.label }}</span>
                            <span style="float: right;color: #8492a6;font-size: 13px" @click="handleCategotyFormClickEvent(data.value)"><i v-bind:class="data.style"></i></span>
                            <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
                        </template>
                    </el-cascader>
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
                <el-form-item label="菜单描述" prop="description">
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
import {methods} from "../../../js/admin";
// 分类打开时触发事件
methods.handleCategoryQueryVisableChangeEvent = function (val) {
  this.$parent.handleCategoryQueryVisableChangeEvent(val);
}
// 菜单分类改变值时触发事件
methods.handleCategoryQueryChangeEvent = function (val) {
   this.$parent.handleCategoryQueryChangeEvent(val);
};
// 菜单分类点击触发事件
methods.handleCategotyQueryClickEvent = function (val) {
    this.$parent.handleCategotyQueryClickEvent(val);
};
// 图标分类改变值时触发事件
methods.handleIconChangeEvent = function (val) {
    this.$parent.handleIconChangeEvent(val);
};
// 图标分类打开时触发事件
methods.handleIconVisableChangeEvent = function (val) {
    this.$parent.handleIconVisableChangeEvent(val);
};
// 菜单分类改变值时触发事件
methods.handleCategoryFormChangeEvent = function (val) {
	this.$parent.handleCategoryFormChangeEvent(val);
};
// 菜单分类点击触发事件
methods.handleCategotyFormClickEvent = function (val) {
 	this.$parent.handleCategotyFormClickEvent(val);
};
export default {
  name: "dialogForm",
  props: ["formSubmit", "dialog", "admin_options", "test", "disabled", "formRoleValidator", "icon_options", "options" ],
  methods: methods,
  data: function() {
    return {
      labelPosition: "right"
    };
  },
  watch: {

  }

}
</script>