<template>
        <!-- 开始查询信息 -->
        <el-form :inline="true" :model="formQuery" ref="formQuery" class="demo-form-inline query">
            <el-form-item label="菜单分类" prop="id">
                <el-cascader v-model="formQuery.id" ref="refHandleQuery"
                             :options="options" filterable :props="{ checkStrictly: true,expandTrigger: 'hover' }"
                             @change="handleCategoryQueryChangeEvent" @visible-change="handleCategoryQueryVisableChangeEvent" clearable>
                    <template slot-scope="{ node, data }">
                        <span style="float: left" @click="handleCategotyQueryClickEvent(data.value)">{{ data.label }}</span>
                        <span style="float: right;color: #8492a6;font-size: 13px" @click="handleCategotyQueryClickEvent(data.value)"><i v-bind:class="data.style"></i></span>
                        <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
                    </template>
                </el-cascader>
            </el-form-item>
            <el-form-item label="菜单名称" prop="menu_name">
                <el-input v-model="formQuery.menu_name" placeholder="菜单名称"></el-input>
            </el-form-item>
            <el-form-item label="菜单分组" prop="menu_group">
                <el-input v-model="formQuery.menu_group" placeholder="菜单分组"></el-input>
            </el-form-item>
            <el-form-item label="父菜单分类" prop="parent_id">
                <el-select v-model="formQuery.parent_id" filterable placeholder="请选择" @change="handleParentCategoryQueryChangeEvent" @visible-change="handleParentCategoryQueryVisableChangeEvent">
                    <el-option v-for="item in parent_options"
                               :key="item.label"
                               :label="item.label"
                               :value="item.value">
                        <span style="float: left">{{ item.label }}</span>
                        <span style="float: right; color: #8492a6; font-size: 13px"><i v-bind:class="item.style"></i></span>
                    </el-option>
                </el-select>
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
// 菜单分类点击触发事件
methods.handleCategotyQueryClickEvent = function (val) {
  this.$parent.handleCategotyQueryClickEvent(val);
};
// 父菜单分类信息
methods.queryParentCategoryEvent = function () {
  this.$parent.queryParentCategoryEvent();
};// 父菜单分类改变值时触发事件
methods.handleParentCategoryQueryChangeEvent = function (val) {
  this.$parent.handleParentCategoryQueryChangeEvent(val);
};
// 父菜单分类打开时触发事件
methods.handleParentCategoryQueryVisableChangeEvent = function (val) {
  this.$parent.handleParentCategoryQueryVisableChangeEvent(val);
};
export default {
  name: "query",
  props: ["formQuery", "createDatePickerOptions", "options", "parent_options"],
  data: function() {
    return {
    };
  },
  mounted: function() {
  },
  methods: methods
};
</script>