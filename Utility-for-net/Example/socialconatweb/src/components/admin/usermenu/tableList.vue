<template>
         <!-- 开始 列表信息 -->
        <el-table v-loading="loading" element-loading-spinner="el-icon-loading" element-loading-background="rgba(0, 0, 0, 0.8)" ref="multipleTable" :data="tableData" tooltip-effect="dark" style="width: 100%"
                  highlight-current-row row-key="id" border @selection-change="handleSelectionChange" :default-sort="{prop: 'create_date', order: 'descending'}">
            <el-table-column type="index" width="50" sortable label="序号" align="center"> </el-table-column>
            <el-table-column prop="id" label="编号" sortable width="100" resizable align="center">  </el-table-column>
            <el-table-column prop="role" label="角色信息" width="300" align="center">
                <el-table-column prop="role.value" label="角色编号" width="100" align="center"> </el-table-column>
                <el-table-column prop="role.label" label="角色名称" width="100" align="center"> </el-table-column>
                <el-table-column prop="role.description" label="角色描述" width="200" align="center"> </el-table-column>
            </el-table-column>

            <el-table-column prop="admin.id" label="管理员编号" sortable width="200" align="center">
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
            <el-table-column prop="enable" label="启用" sortable width="100" resizable align="center">
                <template slot-scope="scope">
                    <el-checkbox v-model="scope.row.enable" @change="checkEd(scope.row.id,scope.row.enable,'enable')">启用</el-checkbox>
                </template>
            </el-table-column>
            <el-table-column prop="level" label="等级信息" width="300" align="center">
                <el-table-column prop="level.id" label="等级编号" width="120"></el-table-column>
                <el-table-column prop="level.name" label="等级名称" width="120"></el-table-column>
            </el-table-column>
            <el-table-column prop="menu" label="菜单信息" width="300" align="center">
                <el-table-column prop="menu.id" label="菜单编号" width="120"></el-table-column>
                <el-table-column prop="menu.menu_name" label="菜单名称" width="120"></el-table-column>
                <el-table-column prop="menu.href" label="菜单地址" width="120"></el-table-column>
            </el-table-column>
            <el-table-column prop="add" label="增" width="120">
                <template slot-scope="scope">
                    <el-checkbox v-model="scope.row.add" @change="checkEd(scope.row.id,scope.row.add,'add')">启用</el-checkbox>
                </template>
            </el-table-column>
            <el-table-column prop="delete" label="删" width="120">
                <template slot-scope="scope">
                    <el-checkbox v-model="scope.row.delete" @change="checkEd(scope.row.id,scope.row.delete,'delete')">启用</el-checkbox>
                </template>
            </el-table-column>
            <el-table-column prop="modify" label="改" width="120">
                <template slot-scope="scope">
                    <el-checkbox v-model="scope.row.modify"  @change="checkEd(scope.row.id,scope.row.modify,'modify')">启用</el-checkbox>
                </template>
            </el-table-column>
            <el-table-column prop="query" label="查" width="120">
                <template slot-scope="scope">
                    <el-checkbox v-model="scope.row.query"  @change="checkEd(scope.row.id,scope.row.query,'query')">启用</el-checkbox>
                </template>
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
        </el-table>
        <!-- 结束 列表信息 -->
</template>
<script>
import {methods} from "../../../js/table";
methods.checkEd=function (id, val, type){
	this.$parent.checkEd(id,val,type);
};
export default {
  name: "tableList",
  props: [ "tableData", "loading", "operator", "baseUrl" ],
  methods: methods

}
</script>