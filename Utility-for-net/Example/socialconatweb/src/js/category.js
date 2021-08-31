export var methods = {
  loadTableDataOrQueryDataEvent: function () {
    // this.$parent.loadTableDataOrQueryDataEvent(this.formQuery);
    this.$emit("loadTableDataOrQueryDataEvent", this.formQuery);
  },
  // 重置表单事件
  resetFormEvent: function (formName) {
    this.$parent.resetFormEvent(formName);
  },
  // 分类信息
  queryCategoryEvent: function () {
    this.$parent.queryCategoryEvent();
  },
  // 分类改变值时触发事件
  handleCategoryQueryChangeEvent: function (val) {
    this.$parent.handleCategoryQueryChangeEvent(val);
  },
  // 分类打开时触发事件
  handleCategoryQueryVisableChangeEvent: function (val) {
    this.$parent.handleCategoryQueryVisableChangeEvent(val);
  }
};
