<template>
    <view class="uni-container">
        111
    <novel-tabbar />
    </view>
</template>

<script>
import novelTabbar from './novel-tabbar.vue';
    export default{
        components:{novelTabbar},
        onLoad() {

        },
        destroyed() {

        },
        onBackPress(e) {
            //this.$emit("unmount")
            // 这里可以自定义返回逻辑，比如下面跳转其他页面
        	uni.switchTab({
        		url: '/pages/main/component/component'
        	});
        	// return true 表示禁止默认返回
        	// return true
        },
    }
</script>

<style>
    
</style>
