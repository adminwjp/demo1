<template>
        <!-- 开始查询信息 -->
        <el-form :inline="true" :model="formQuery" ref="formQuery" class="demo-form-inline  query">
            <el-form-item label="编号" prop="id">
                <el-select v-model="formQuery.id" filterable placeholder="请选择" @change="handleCategoryQueryChangeEvent" @visible-change="handleCategoryQueryVisableChangeEvent">
                    <el-option v-for="item in options" :key="item.id"
                               :label="item.category" :value="item.id">
                    </el-option>
                </el-select>
            </el-form-item>
            <el-form-item label="角色名称" prop="category">
                <el-input v-model="formQuery.category" placeholder="角色名称"></el-input>
            </el-form-item>
             <el-form-item label="角色分类" prop="role_id">
                <el-cascader v-model="formQuery.role_id" placeholder="角色分类" ref="refHandleQuery" :options="role_options"
                             :props="{  checkStrictly: true,expandTrigger: 'hover' }" @change="handleRoleQueryChangeEvent" @visible-change="handleRoleVisableChangeEvent" clearable>
                    <template slot-scope="{ node, data }">
                        <span style="float: left" @click="handleRoleQueryClickEvent(data.value)">{{ data.label }}</span>
                        <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
                    </template>
                </el-cascader>
            </el-form-item>
            <el-form-item label="创建时间范围" prop="create_date">
                <el-date-picker v-model="formQuery.create_date" type="datetimerange" value-format="yyyy-MM-dd HH:mm:ss" :picker-options="createDatePickerOptions"
                                range-separator="至" start-placeholder="开始日期" align="right">
                </el-date-picker>
            </el-form-item>
            <el-form-item label="修改时间范围" prop="update_date">
                <el-date-picker v-model="formQuery.update_date" type="datetimerange" value-format="yyyy-MM-dd HH:mm:ss" :picker-options="createDatePickerOptions" range-separator="至" start-placeholder="开始日期"
                                end-placeholder="结束日期" align="right">
                </el-date-picker>
            </el-form-item>
            <el-form-item>
                <el-button type="primary" @click="loadTableDataOrQueryDataEvent">查询</el-button>
                <el-button @click="resetFormEvent('formQuery')">重置</el-button>
            </el-form-item>
        </el-form>
        <!-- 结束查询信息 -->
</template>
<script>
import {createDatePickerOptions, ContentTypeJson, ContentTypeForm, ContentTypeMultipart, get, post, resetFormEvent, tip} from "../../../js/base";
import "../../../js/auth";
import {urls} from "../../../js/url";
import {methods} from "../../../js/category";

// 查询表单 角色分类改变 当选中节点变化时触发事件
methods.handleRoleQueryChangeEvent = function (value) {
  this.$parent.handleRoleQueryChangeEvent(value);
};
// 角色分类改变 下拉框出现/隐藏时触发 事件
methods.handleRoleVisableChangeEvent = function (value) {
  this.$parent.handleRoleVisableChangeEvent(value);
};
// 查询表单 角色分类改变 当选中节点点击时触发事件
methods.handleRoleQueryClickEvent = function (value) {
  this.$parent.handleRoleQueryClickEvent(value);
};
export default {
  name: "query",
  props: ["formQuery", "createDatePickerOptions", "options", "role_options"],
  data: function() {
    return {

    };
  },
  mounted: function() {
  },
  methods: methods
};
</script>