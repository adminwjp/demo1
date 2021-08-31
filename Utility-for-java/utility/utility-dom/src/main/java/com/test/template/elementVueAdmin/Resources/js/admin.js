export const methods = {
  handleAdminChangeEvent: function (val) {
    this.$parent.handleAdminChangeEvent(val);
  },
  handleAdminVisableChangeEvent: function (val) {
    this.$parent.handleAdminVisableChangeEvent();
  },
  submitFormEvent: function (formName) {
    this.$parent.submitFormEvent(formName);
  },
  resetFormEvent: function (formName) {
    this.$parent.resetFormEvent(formName);
  },
  handleDialogClose: function () {
    this.$parent.handleDialogClose();
  }
};