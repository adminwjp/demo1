<template>
	<view class="content">
		<view class="input-group">
			<view class="input-row border">
				<!-- #ifndef APP-PLUS || APP-PLUS-NVUE || MP -->
				<text class="title">账号：</text>
				<!-- #endif  -->
				<m-input type="text" focus clearable v-model="account" placeholder="请输入账户或邮箱"  ></m-input>
			</view>
		</view>

		<view class="btn-row">
			<button type="primary" class="primary" @tap="findPassword">提交</button>
		</view>
        <view class="action-row">
        	<navigator url="reg">注册账号</navigator>
        	<text>|</text>
        	<navigator url="pwd">忘记密码</navigator>
        </view>
	</view>
</template>

<script>
	import mInput from '../../components/m-input.vue';

	export default {
		components: {
			mInput
		},
		data() {
			return {
				account: ''
			}
		},
		methods: {
			findPassword() {
				/**
				 * 仅做示例
				 */
				if (this.account.length < 3 || !~this.account.indexOf('@')) {
					uni.showToast({
						icon: 'none',
						title: '邮箱地址不合法',
					});
					return;
				}
				uni.showToast({
					icon: 'none',
					title: '已发送重置邮件至注册邮箱，请注意查收。',
					duration: 3000
				});
			}
		}
	}
</script>

<style>
@import url("login.css");
</style>
