<template>
          <!-- 开始 列表信息 -->
        <el-table v-loading="loading" class="table" element-loading-spinner="el-icon-loading"
                  element-loading-background="rgba(0, 0, 0, 0.8)" ref="multipleTable"
                  :data="tableData" tooltip-effect="dark"
                  highlight-current-row row-key="id" border
                  @selection-change="handleSelectionChange"
                  :default-sort="{prop: 'create_date', order: 'descending'}" >
            <el-table-column type="selection" width="55" align="center"> </el-table-column>
            <el-table-column type="index" width="50" sortable label="序号" align="center"> </el-table-column>
            <el-table-column prop="id" label="编号" sortable width="50" align="center">  </el-table-column>
            <el-table-column prop="account" label="账户" width="120" align="center">  </el-table-column>
            <el-table-column prop="password" label="密码" width="120" align="center"> </el-table-column>
            <el-table-column prop="nick_name" label="昵称" width="70" align="center"> </el-table-column>
            <el-table-column prop="login_date" label="登录时间" sortable width="100" align="center">
                <template slot-scope="scope">
                    <i class="el-icon-time"></i>
                    <span style="margin-left: 10px">{{ scope.row.login_date }}</span>
                </template>
            </el-table-column>
            <el-table-column prop="token" label="Token" width="100" align="center"> </el-table-column>
            <el-table-column prop="express_in" label="Token失效时间(单位：秒)" width="150" align="center"> </el-table-column>
            <el-table-column prop="real_name" label="真实姓名" width="70" align="center"> </el-table-column>
            <el-table-column prop="head_pic" label="头像" width="200" align="center">
                <template slot-scope="scope">
                    <img v-if="scope.row.head_pic" :src="baseUrl+'img/'+scope.row.head_pic" align="middle" class="avatar">
                    <img v-else src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" class="avatar" />
                </template>
            </el-table-column>
            <el-table-column prop="birthday" label="出生日期" sortable width="100" align="center">
                <template slot-scope="scope">
                    <i class="el-icon-time"></i>
                    <span style="margin-left: 10px">{{ scope.row.login_date }}</span>
                </template>
            </el-table-column>
            <el-table-column prop="edution" label="学历" width="70" align="center"> </el-table-column>
            <el-table-column prop="bank_id" label="银行卡号" width="100" align="center"> </el-table-column>
            <el-table-column prop="card_id" label="身份证号" width="100" align="center"> </el-table-column>
            <el-table-column prop="marital" label="婚姻状态" width="100" align="center"> </el-table-column>
            <el-table-column prop="status" label="用户状态" width="100" align="center"> </el-table-column>
            <el-table-column prop="level" label="用户等级" width="100" align="center"> </el-table-column>
            <el-table-column prop="phone" label="手机号" width="100" align="center"> </el-table-column>
            <el-table-column prop="login_ip" label="登录ip" width="100" align="center"> </el-table-column>
            <el-table-column prop="fail_count" label="登录失败次数" width="150" align="center"> </el-table-column>
            <el-table-column prop="sex" label="性别" width="70" align="center"> </el-table-column>
            <el-table-column prop="description" label="描述" width="100" align="center"> </el-table-column>
            <el-table-column prop="email" label="邮箱" width="100" align="center"> </el-table-column>
            <el-table-column prop="card_photo1" label="身份证正面" width="200" align="center">
                <template slot-scope="scope">
                    <img v-if="scope.row.card_photo1" :src="baseUrl+'img/'+scope.row.card_photo1" align="middle" class="avatar">
                    <img v-else src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" class="avatar" />
                </template>
            </el-table-column>
            <el-table-column prop="card_photo2" label="身份证反面" width="200" align="center">
                <template slot-scope="scope">
                    <img v-if="scope.row.card_photo2" :src="baseUrl+'img/'+scope.row.card_photo2" align="middle" class="avatar">
                    <img v-else src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" class="avatar" />
                </template>
            </el-table-column>
            <el-table-column prop="hand_card_photo1" label="手持身份证正面" width="200" align="center">
                <template slot-scope="scope">
                    <img v-if="scope.row.hand_card_photo1" :src="baseUrl+'img/'+scope.row.hand_card_photo1" align="middle" class="avatar">
                    <img v-else src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" class="avatar" />
                </template>
            </el-table-column>
            <el-table-column prop="hand_card_photo2" label="手持身份证反面" width="200" align="center">
                <template slot-scope="scope">
                    <img v-if="scope.row.hand_card_photo2" :src="baseUrl+'img/'+scope.row.hand_card_photo2" align="middle" class="avatar">
                    <img v-else src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" class="avatar" />
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
            <el-table-column label="操作" width="100" align="center">
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