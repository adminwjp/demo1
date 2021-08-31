<template>
        <!-- 开始 列表信息 -->
        <el-table v-loading="loading" class="table" element-loading-spinner="el-icon-loading" element-loading-background="rgba(0, 0, 0, 0.8)" ref="multipleTable" :data="tableData" tooltip-effect="dark"
                  highlight-current-row border @selection-change="handleSelectionChange" :default-sort="{prop: 'create_date', order: 'descending'}">
            <el-table-column type="selection" width="55" align="center"> </el-table-column>

            <el-table-column type="index" width="50" sortable label="序号" align="center"> </el-table-column>
            <el-table-column prop="id" label="编号" sortable width="70" align="center">  </el-table-column>
            <el-table-column prop="category.category" label="名称" width="100" align="center"> </el-table-column>
            <el-table-column prop="category.description" label="描述" width="200" align="center"> </el-table-column>
            <el-table-column prop="create_date" label="创建日期" sortable width="200" align="center"> </el-table-column>
            <el-table-column prop="update_date" label="修改日期" sortable width="200" align="center"> </el-table-column>
            <el-table-column label="操作" width="200" align="center">
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