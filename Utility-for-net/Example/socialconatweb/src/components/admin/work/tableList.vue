<template>
          <!-- 开始 列表信息 -->
        <el-table v-loading="loading" class="table" element-loading-spinner="el-icon-loading" element-loading-background="rgba(0, 0, 0, 0.8)" ref="multipleTable" :data="tableData" tooltip-effect="dark"
                  highlight-current-row border @selection-change="handleSelectionChange" :default-sort="{prop: 'create_date', order: 'descending'}">
            <el-table-column type="selection" width="55"> </el-table-column>
            <el-table-column type="index" width="50" sortable label="序号"> </el-table-column>
            <el-table-column prop="id" label="编号" sortable width="70">  </el-table-column>
            <el-table-column prop="company_name" label="工作公司" width="200"> </el-table-column>
            <el-table-column prop="job" label="工作" width="50"> </el-table-column>
            <el-table-column prop="description" label="工作经历描述" width="300"> </el-table-column>
            <el-table-column prop="category.category" label="工作分类名称" width="200"> </el-table-column>
            <el-table-column prop="category.description" label="工作经历描述" width="300"> </el-table-column>
            <el-table-column prop="user.id" label="用户编号" sortable width="200">  </el-table-column>
            <el-table-column prop="start_date" label="工作起止日期" sortable width="200"> </el-table-column>
            <el-table-column prop="end_date" label="工作结束日期" sortable width="200"> </el-table-column>
            <el-table-column prop="create_date" label="创建日期" sortable width="200"> </el-table-column>
            <el-table-column prop="update_date" label="修改日期" sortable width="200"> </el-table-column>
            <el-table-column label="操作" width="200">
                <template slot-scope="scope">
                    <el-button @click="handleSelectClickEvent(scope.row)" type="text" icon="el-icon-view" size="small">查看</el-button>
                    <el-button type="text" @click="handleModifyClickEvent(scope.row)" v-if="operator.modify" icon="el-icon-edit" size="small">编辑</el-button>
                    <el-button type="text" @click="handleDeleteClickEvent(scope.row)" v-if="operator.delete" icon="el-icon-delete" size="small">删除</el-button>
                </template>
            </el-table-column>
        </el-table>
        <!-- 结束 列表信息 -->
</template>
<script>
import {methods} from "../../../js/table"
export default {
  name: "tableList",
  props: [ "tableData", "loading", "operator" ],
  methods: methods

}
</script>