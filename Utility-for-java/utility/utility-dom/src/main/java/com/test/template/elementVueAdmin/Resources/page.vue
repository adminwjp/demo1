<template>
    <!-- 开始 分页 -->
    <div class="block" style="text-align:center;">
        <el-pagination background @size-change="handleSizeChangeEvent" @current-change="handleCurrentPageChangeEvent" :current-page="page.current_page"
        :page-sizes="page.sizes" :page-size="page.size" :page-count="page.page" layout="total, sizes, prev, pager, next, jumper" :total="page.total">
    </el-pagination>
</div>
        <!-- 结束 分页 -->
        </template>
<script>
export default {
    name: "page",
    props: ["page"],
    methods: {
        // 每页记录改变是触发事件
        handleSizeChangeEvent: function (val) {
            this.page.size = val;
            this.$parent.loadTableDataOrQueryDataEvent();
        },
        // 当前页数改变事件
        handleCurrentPageChangeEvent: function (val) {
            this.page.current_page = val;
            this.$parent.loadTableDataOrQueryDataEvent();
        }
    }

}
</script>