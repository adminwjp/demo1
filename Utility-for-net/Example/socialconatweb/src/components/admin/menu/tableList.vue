<template>
          <!-- 开始 列表信息 -->
        <el-table v-loading="loading" class="table" element-loading-spinner="el-icon-loading" element-loading-background="rgba(0, 0, 0, 0.8)" ref="multipleTable" :data="tableData" tooltip-effect="dark"
                  highlight-current-row row-key="id" border @selection-change="handleSelectionChange" :default-sort="{prop: 'create_date', order: 'descending'}" :tree-props="{children: 'children', hasChildren: 'hasChildren'}">
            <el-table-column type="selection" align="center" width="55"> </el-table-column>
            <el-table-column type="index" width="50" align="center" sortable label="序号"> </el-table-column>
            <el-table-column prop="id" label="编号"  sortable width="70">  </el-table-column>
            <el-table-column prop="menu_name" label="菜单名称" align="center" width="200"> </el-table-column>
            <el-table-column prop="menu_group" label="菜单分组" align="center" width="200">
                <template slot-scope="scope">
                    <span v-html="!scope.row.menu_group?'无':scope.row.menu_group"></span>
                </template>
            </el-table-column>
            <el-table-column prop="description" label="菜单描述" align="center" width="300"> </el-table-column>
            <el-table-column prop="href" label="菜单链接" align="center" width="200">
                <template slot-scope="scope">
                    <span v-html="!scope.row.href?'无':scope.row.href"></span>
                </template>
            </el-table-column>
            <el-table-column prop="icon" label="图标编号" align="center" width="200">
                <template slot-scope="scope" v-if="scope.row.icon">
                    <el-popover trigger="hover" placement="top">
                        <p>图标编号: {{ scope.row.icon.id }}</p>
                        <p>图标名称: {{ scope.row.icon.label }}</p>
                        <p>图标样式: {{ scope.row.icon.value }}</p>
                        <p>图标样式展示: <i :class="scope.row.icon.value"></i></p>
                        <div slot="reference" class="name-wrapper">
                            <el-tag size="medium">{{ scope.row.icon.id }}</el-tag>
                        </div>
                    </el-popover>
                </template>
            </el-table-column>
            <el-table-column label="父菜单编号" prop="parent" align="center" width="200">
                <template slot-scope="scope" v-if="scope.row.parent">
                    <el-popover trigger="hover" placement="top">
                        <p>父菜单编号: {{ scope.row.parent.id }}</p>
                        <p>父菜单名称: {{ scope.row.parent.menu_name }}</p>
                        <p>父菜单分组:<span v-html="!scope.row.parent.menu_group?'无':scope.row.parent.menu_group"></span></p>
                        <p>父菜单描述: {{scope.row.parent.description}}</p>
                        <div slot="reference" class="name-wrapper">
                            <el-tag size="medium">{{ scope.row.parent.id }}</el-tag>
                        </div>
                    </el-popover>
                </template>
            </el-table-column>
            <el-table-column prop="admin.id" label="管理员编号" align="center" sortable width="200">
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
            <el-table-column prop="create_date" label="创建日期" align="center" sortable width="200"> </el-table-column>
            <el-table-column prop="update_date" label="修改日期" align="center" sortable width="200"> </el-table-column>
            <el-table-column label="操作" width="200">
                <template slot-scope="scope">
                    <el-button @click="handleSelectClickEvent(scope.row)" type="text" icon="el-icon-view"  size="small">查看</el-button>
                    <el-button type="text" @click="handleModifyClickEvent(scope.row)" icon="el-icon-edit" v-if="operator.modify" size="small">编辑</el-button>
                    <el-button type="text" @click="handleDeleteClickEvent(scope.row)" icon="el-icon-delete" v-if="operator.delete" size="small">删除</el-button>
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