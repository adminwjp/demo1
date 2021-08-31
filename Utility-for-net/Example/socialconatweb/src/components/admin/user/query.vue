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
            <el-form-item label="真实姓名" prop="real_name">
                <el-input v-model="formQuery.real_name" placeholder="真实姓名"></el-input>
            </el-form-item>
            <el-form-item label="出生日期范围" prop="birthday_date">
                <el-date-picker v-model="formQuery.birthday_date" type="datetimerange" value-format="yyyy-MM-dd HH:mm:ss" :picker-options="createDatePickerOptions" range-separator="至"
                                start-placeholder="开始日期" end-placeholder="结束日期" align="right">
                </el-date-picker>
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
            <el-form-item label="学历" prop="edution_id">
                <el-select v-model="formQuery.edution_id" placeholder="请选择" @change="handleEdutionCategoryChangeEvent" @visible-change="handleEdutionCategoryVisableChangeEvent">
                    <el-option v-for="it in edution_options" :key="it.id" :label="it.category" :value="it.id">
                    </el-option>
                </el-select>
            </el-form-item>
            <el-form-item label="婚姻状态" prop="marital_id">
                <el-select v-model="formQuery.marital_id" placeholder="请选择" @change="handleMaritalCategoryChangeEvent" @visible-change="handleMaritalCategoryVisableChangeEvent">
                    <el-option v-for="it in marital_options" :key="it.id" :label="it.category" :value="it.id">
                    </el-option>
                </el-select>
            </el-form-item>
            <el-form-item label="邮箱" prop="email">
                <el-input v-model="formQuery.email" placeholder="邮箱"></el-input>
            </el-form-item>
            <el-form-item label="身份证号" prop="card_id">
                <el-input v-model="formQuery.card_id" placeholder="身份证号"></el-input>
            </el-form-item>
            <el-form-item label="银行卡号" prop="bank_id">
                <el-input v-model="formQuery.bank_id" placeholder="银行卡号"></el-input>
            </el-form-item>
            <el-form-item label="用户状态" prop="status_id">
                <el-select v-model="formQuery.status_id" placeholder="请选择" @change="handleStatusCategoryChangeEvent" @visible-change="handleStatusCategoryVisableChangeEvent">
                    <el-option v-for="it in status_options" :key="it.id" :label="it.category" :value="it.id">
                 </el-option>
                </el-select>
            </el-form-item>
            <el-form-item label="登录ip" prop="login_ip">
                <el-input v-model="formQuery.login_ip" placeholder="登录ip"></el-input>
            </el-form-item>
            <el-form-item label="用户等级" prop="level_id">
                <el-select v-model="formQuery.level_id" placeholder="请选择" @change="handleLevelCategoryChangeEvent" @visible-change="handleLevelCategoryVisableChangeEvent">
                    <el-option v-for="it in level_options" :key="it.id" :label="it.category" :value="it.id">
                    </el-option>
                </el-select>
            </el-form-item>
            <el-form-item label="登录时间范围" prop="login_date">
                <el-date-picker v-model="formQuery.login_date" type="datetimerange" value-format="yyyy-MM-dd HH:mm:ss" :picker-options="createDatePickerOptions" range-separator="至"
                                start-placeholder="开始日期" end-placeholder="结束日期" align="right">
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
methods.handleEdutionCategoryChangeEvent = function (val) {
  this.$parent.handleEdutionCategoryChangeEvent(val);
};
methods.handleEdutionCategoryVisableChangeEvent = function (val) {
  this.$parent.handleEdutionCategoryVisableChangeEvent(val);
};
methods.handleMaritalCategoryChangeEvent = function (val) {
  this.$parent.handleMaritalCategoryChangeEvent(val);
};
methods.handleMaritalCategoryVisableChangeEvent = function (val) {
  this.$parent.handleMaritalCategoryVisableChangeEvent(val);
};
methods.handleLevelCategoryChangeEvent = function (val) {
  this.$parent.handleLevelCategoryChangeEvent(val);
};
methods.handleLevelCategoryVisableChangeEvent = function (val) {
  this.$parent.handleLevelCategoryVisableChangeEvent(val);
};
methods.handleStatusCategoryChangeEvent = function (val) {
  this.$parent.handleStatusCategoryChangeEvent(val);
};
methods.handleStatusCategoryVisableChangeEvent = function (val) {
  this.$parent.handleStatusCategoryVisableChangeEvent(val);
};
export default {
  name: "query",
  props: ["formQuery", "createDatePickerOptions", "options", "edution_options", "level_options", "marital_options", "status_options"],
  data: function() {
    return {
    };
  },
  mounted: function() {
  },
  methods: methods
};
</script>