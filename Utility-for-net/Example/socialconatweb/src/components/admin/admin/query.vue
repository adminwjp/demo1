<template>

        <!-- 开始查询信息 -->
        <el-form :inline="true" :model="formQuery" ref="formQuery" class="demo-form-inline query">
            <el-form-item label="编号" prop="id">
                <el-select v-model="formQuery.id" placeholder="请选择" @change="handleCategoryQueryChangeEvent" @visible-change="handleCategoryQueryVisableChangeEvent">
                    <el-option v-for="it in options" :key="it.id" :label="it.category" :value="it.id">
                    </el-option>
                </el-select>
            </el-form-item>
            <el-form-item label="Token" prop="token">
                <el-input v-model="formQuery.token" placeholder="Token"></el-input>
            </el-form-item>
            <el-form-item label="账户" prop="account">
                <el-input v-model="formQuery.account" placeholder="账户"></el-input>
            </el-form-item>
            <el-form-item label="昵称" prop="nick_name">
                <el-input v-model="formQuery.nick_name" placeholder="昵称"></el-input>
            </el-form-item>
            <el-form-item label="出生日期范围" prop="birthday_date">
                <el-date-picker v-model="formQuery.birthday_date" type="datetimerange" value-format="yyyy-MM-dd" :picker-options="createDatePickerOptions"
                                range-separator="至" start-placeholder="开始日期" align="right">
                </el-date-picker>
            </el-form-item>
            <el-form-item label="角色" prop="role_id">
                <el-cascader v-model="formQuery.role_id" placeholder="角色分类" ref="refHandleQuery"
                             :options="role_options"
                             :props="{  checkStrictly: true,expandTrigger: 'hover' }"
                             @change="handleRoleQueryChangeEvent" @visible-change="handleRoleVisableChangeEvent" clearable>
                    <template slot-scope="{ node, data }">
                        <span style="float: left" @click="handleRoleQueryClickEvent(data.value)">{{ data.label }}</span>
                        <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
                    </template>
                </el-cascader>
            </el-form-item>

            <el-form-item label="真实姓名" prop="real_name">
                <el-input v-model="formQuery.real_name" placeholder="真实姓名"></el-input>
            </el-form-item>

            <el-form-item label="手机号" prop="phone">
                <el-input v-model="formQuery.phone" placeholder="手机号"></el-input>
            </el-form-item>
            <el-form-item label="性别" prop="sex">
                <el-select v-model="formQuery.sex" placeholder="性别">
                    <el-option label="男" value="男"></el-option>
                    <el-option label="女" value="女"></el-option>
                </el-select>
            </el-form-item>
            <el-form-item label="邮箱" prop="email">
                <el-input v-model="formQuery.email" placeholder="邮箱"></el-input>
            </el-form-item>
            <el-form-item label="登录时间范围" prop="login_date">
                <el-date-picker v-model="formQuery.login_date" type="datetimerange" value-format="yyyy-MM-dd HH:mm:ss" :picker-options="createDatePickerOptions"
                                range-separator="至" start-placeholder="开始日期" align="right">
                </el-date-picker>
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
import {createDatePickerOptions} from "../../../js/base";
import {methods} from "../../../js/category";
// 角色分类改变 下拉框出现/隐藏时触发 事件
methods.handleRoleVisableChangeEvent = function (value) {
  this.$parent.handleRoleVisableChangeEvent(value);
};
// 查询表单 角色分类改变 当选中节点点击时触发事件
methods.handleRoleQueryClickEvent = function (value) {
  this.$parent.handleRoleQueryClickEvent(value);
};
// 查询表单 角色分类改变 当选中节点变化时触发事件
methods.handleRoleQueryChangeEvent = function (value) {
  this.$parent.handleRoleQueryChangeEvent(value);
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