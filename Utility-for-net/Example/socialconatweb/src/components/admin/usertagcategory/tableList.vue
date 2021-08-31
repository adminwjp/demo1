<template>
     <!-- 开始 列表信息 -->
        <el-table v-loading="loading" class="table" element-loading-spinner="el-icon-loading" element-loading-background="rgba(0, 0, 0, 0.8)" ref="multipleTable" :data="tableData" tooltip-effect="dark"
                  highlight-current-row border @selection-change="handleSelectionChange" :default-sort="{prop: 'create_date', order: 'descending'}">
            <el-table-column type="selection" width="55" align="center"> </el-table-column>
            <el-table-column type="expand">
                <template slot-scope="props">
                    <el-table border :data="props.row.users" style="width: 100%">
                        <el-table-column label="用户编号" prop="id"></el-table-column>
                        <el-table-column label="账户" prop="name"></el-table-column>
                        <el-table-column label="昵称" prop="desc"></el-table-column>
                        <el-table-column label="姓名" prop="desc"></el-table-column>
                        <el-table-column label="出生日期" prop="birthday">
                            <template slot-scope="scope">
                                <i class="el-icon-time"></i>
                                <span style="margin-left: 10px">{{ scope.row.birthday }}</span>
                            </template>
                        </el-table-column>
                        <el-table-column label="头像" prop="head_pic">
                            <template slot-scope="scope">
                                <img v-if="scope.row.head_pic" style="margin-left:10px;margin-top:-5px;border-radius:50%;width:36px;height:36px;vertical-align:middle;" :src="baseUrl+'img/'+scope.row.head_pic" class="avatar">
                                <img v-else style="margin-left:10px;margin-top:-5px;border-radius:50%;width:36px;height:36px;vertical-align:middle;" src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" />
                            </template>
                        </el-table-column>
                        <el-table-column label="手机" prop="phone"></el-table-column>
                        <el-table-column label="性别" prop="sex"></el-table-column>
                        <el-table-column label="描述" prop="description"></el-table-column>
                        <el-table-column label="邮箱" prop="email"></el-table-column>
                        <el-table-column label="角色" prop="role"></el-table-column>
                    </el-table>
                </template>
            </el-table-column>
            <el-table-column type="index" width="50" sortable label="序号"> </el-table-column>
            <el-table-column prop="id" label="编号" sortable width="70">  </el-table-column>
            <el-table-column prop="category" label="分类名称" width="200"> </el-table-column>
            <el-table-column prop="description" label="描述" width="300"> </el-table-column>
            <el-table-column prop="admin" label="管理员编号" width="200" align="center">
                <template slot-scope="scope" v-if="scope.row.admin">
                    <el-popover trigger="hover" placement="top">
                        <p>管理员编号: {{ scope.row.admin.id }}</p>
                        <p>账户: {{ scope.row.admin.account }}</p>
                        <p>昵称: {{ scope.row.admin.nick_name }}</p>
                        <p>姓名: {{ scope.row.admin.real_name }}</p>
                        <p>
                            出生日期:  <i class="el-icon-time"></i>
                            <span style="margin-left: 10px">{{ scope.row.admin.birthday }}</span>
                        </p>
                        <p>
                        <p>
                            头像:<img v-if="scope.row.admin.head_pic" style="margin-left:10px;margin-top:-5px;border-radius:50%;width:36px;height:36px;vertical-align:middle;" :src="baseUrl+'img/'+scope.row.admin.head_pic" class="avatar">
                            <img v-else style="margin-left:10px;margin-top:-5px;border-radius:50%;width:36px;height:36px;vertical-align:middle;" src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" />
                        </p>
                        <p>手机: {{ scope.row.admin.phone }}</p>
                        <p>性别: {{ scope.row.admin.sex }}</p>
                        <p>描述: {{ scope.row.admin.description }}</p>
                        <p>邮箱: {{ scope.row.admin.email }}</p>
                        <p>角色: {{ scope.row.admin.role }}</p>
                        <div slot="reference" class="name-wrapper">
                            <el-tag size="medium">{{ scope.row.admin.id }}</el-tag>
                        </div>
                    </el-popover>
                </template>
            </el-table-column>
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
  props: [ "tableData", "loading", "operator", "baseUrl" ],
  methods: methods

}
</script>