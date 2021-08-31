<template>
      <!-- 开始 操作按钮 -->
        <div class="operator" style="margin-top: 20px;text-align:left;margin-bottom:10px">
            <el-button @click="toggleSelectionTableCheckBoxButtonEvent(checkboxAllSelect?tableData:null)">{{checkboxAllSelectText}}</el-button>
            <el-button @click="insertButtonClickEvent" v-if="operator.add" icon="el-icon-plus">添加</el-button>
            <el-button @click="modifyButtonClickEvent" v-if="operator.modify" icon="el-icon-edit">编辑</el-button>
            <el-button @click="deleteButtonClickEvent" v-if="operator.delete" icon="el-icon-delete">删除</el-button>
            <el-button @click="queryButtonClickEvent" icon="el-icon-view">查看</el-button>
            <el-button @click="loadTableDataOrQueryDataEvent" icon="el-icon-refresh">刷新</el-button>
        </div>
        <!-- 结束 操作按钮 -->
</template>
<script>
export default {
  name: "operator",
  props: ["checkboxAllSelect", "tableData", "checkboxAllSelectText", "operator"],
  methods: {
    insertButtonClickEvent: function() {
      this.$parent.insertButtonClickEvent();
    },
    modifyButtonClickEvent: function() {
      this.$parent.modifyButtonClickEvent();
    },
    deleteButtonClickEvent: function() {
      this.$parent.deleteButtonClickEvent();
    },
    queryButtonClickEvent: function() {
      this.$parent.queryButtonClickEvent();
    },
    toggleSelectionTableCheckBoxButtonEvent: function (rows) {
      this.$parent.toggleSelectionTableCheckBoxButtonEvent(rows);
    },
    loadTableDataOrQueryDataEvent: function() {
      this.$parent.loadTableDataOrQueryDataEvent();
    }
  }
}
</script>