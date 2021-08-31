export var methods = {
  // 复选框多选或列表中编辑、删除、查询事件 即当选择项发生变化时会触发该事件
  handleSelectionChange: function (val) {
    this.$parent.handleSelectionChange(val);
  },
  // 预览事件(列表)  清空表单 重新赋值表单
  handleSelectClickEvent: function (row) {
    this.$parent.handleSelectClickEvent(row);
  },
  // 表单编辑按钮事件(列表) 清空表单 重新赋值表单
  handleModifyClickEvent: function (row) {
    this.$parent.handleModifyClickEvent(row);
  },
  // 删除按钮事件(列表)
  handleDeleteClickEvent: function (row) {
    this.$parent.handleDeleteClickEvent(row);
  }
};
