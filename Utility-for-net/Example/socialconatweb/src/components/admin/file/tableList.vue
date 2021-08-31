<template>
        <!-- 开始 列表信息 -->
        <el-table v-loading="loading" element-loading-spinner="el-icon-loading" element-loading-background="rgba(0, 0, 0, 0.8)" ref="multipleTable" :data="tableData" tooltip-effect="dark" style="width: 100%"
                  highlight-current-row border @selection-change="handleSelectionChange" :default-sort="{prop: 'create_date', order: 'descending'}">
            <el-table-column type="selection" width="55"> </el-table-column>
            <el-table-column type="index" width="50" sortable label="序号" align="center"> </el-table-column>
            <el-table-column prop="id" label="编号" sortable width="70" align="center">  </el-table-column>
            <el-table-column prop="src" label="文件路劲" width="100" align="center"> </el-table-column>
            <el-table-column prop="src" label="文件展示" width="120" align="center">
                <template slot-scope="scope">
                    <img v-if="scope.row.src" style="border-radius:50%;width:80px;height:80px;vertical-align:middle;" :src="baseUrl+'file/get/'+scope.row.src">
                    <img v-else style="border-radius:50%;width:80px;height:80px;vertical-align:middle;" src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" />
                </template>
            </el-table-column>
            <el-table-column prop="description" label="文件描述" width="200" align="center"> </el-table-column>
            <el-table-column prop="category"  label="文件分类信息" width="440" resizable align="center">
                <el-table-column prop="category.id" label="编号" width="60" align="center"> </el-table-column>
                <el-table-column prop="category.category" label="名称" width="80" align="center"> </el-table-column>
                <el-table-column prop="category.accept" label="类型" width="80" align="center"> </el-table-column>
                <el-table-column prop="category.description" label="描述" width="200" align="center"> </el-table-column>
            </el-table-column>
            <el-table-column prop="create_date" label="创建日期" align="center" sortable width="200">
                <template slot-scope="scope">
                    <i class="el-icon-time"></i>
                    <span style="margin-left: 10px">{{ scope.row.create_date }}</span>
                </template>
            </el-table-column>
            <el-table-column prop="update_date" label="修改日期" align="center" sortable width="200">
                <template slot-scope="scope">
                    <i class="el-icon-time"></i>
                    <span style="margin-left: 10px">{{ scope.row.update_date }}</span>
                </template>
            </el-table-column>
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
  props: [ "tableData", "loading", "operator", "baseUrl" ],
  methods: methods

}
</script>
<style scoped>
 .avatar-uploader .el-upload {
            border: 1px dashed #d9d9d9;
            border-radius: 6px;
            cursor: pointer;
            position: relative;
            overflow: hidden;
        }

            .avatar-uploader .el-upload:hover {
                border-color: #409EFF;
            }

        .avatar-uploader-icon {
            font-size: 28px;
            color: #8c939d;
            width: 178px;
            height: 178px;
            line-height: 178px;
            text-align: center;
        }
        .avatar {
            width: 178px;
            height: 178px;
            display: block;
        }
</style>