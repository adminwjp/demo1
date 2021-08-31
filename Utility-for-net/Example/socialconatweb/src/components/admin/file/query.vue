<template>
     <!-- 开始查询信息 -->
        <el-form :inline="true" :model="formQuery" ref="formQuery" class="demo-form-inline ">
            <el-form-item label="编号" prop="id">
                <el-select v-model="formQuery.id" placeholder="请选择" @change="handleCategoryQueryChangeEvent" @visible-change="handleCategoryQueryVisableChangeEvent">
                    <el-option v-for="it in options" :key="it.id" :label="it.category" :value="it.id">
                    </el-option>
                </el-select>
            </el-form-item>
            <el-form-item label="文件路劲" prop="src">
                <el-input v-model="formQuery.src" placeholder="文件路劲"></el-input>
            </el-form-item>
            <el-form-item label="文件分类" prop="category_id">
                <el-select v-model="formQuery.category_id" placeholder="请选择" @change="handleFileCategoryQueryChangeEvent" @visible-change="handleFileCategoryVisableChangeEvent">
                    <el-option v-for="it in category_options" :key="it.id" :label="it.category" :value="it.id">
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
// 下拉框选项值改变事件
methods.handleFileCategoryQueryChangeEvent = function (value) {
  this.$parent.handleFileCategoryQueryChangeEvent(value);
};
// 下拉框显示隐藏改变事件
methods.handleFileCategoryVisableChangeEvent = function (visable) {
  this.$parent.handleFileCategoryVisableChangeEvent(visable);
};
export default {
  name: "query",
  props: ["formQuery", "createDatePickerOptions", "options", "category_options"],
  data: function() {
    return {
    };
  },
  mounted: function() {
  },
  methods: methods
};
</script>
<style scope>
.demo-form-inline {
  margin: 0px 70px;
  text-align: left;
}
</style>