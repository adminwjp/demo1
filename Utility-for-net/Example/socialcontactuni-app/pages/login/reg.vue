<template>
	<view class="content">
		<view class="input-group">
			<view class="input-row border">
			<!-- #ifndef APP-PLUS || APP-PLUS-NVUE || MP -->
			<text class="title">账号：</text>
			<!-- #endif  -->
				<m-input type="text"  v-model="account" :value="account" :clearable="account" placeholder="请输入账号或邮箱"></m-input>
			</view>
			<view class="input-row border">
				<!-- #ifndef APP-PLUS || APP-PLUS-NVUE || MP -->
				<text class="title">密码：</text>
				<!-- #endif  -->
				<m-input type="password" displayable v-model="password" :value="password" :clearable="password" placeholder="请输入密码"></m-input>
			</view>
		</view>
		<view class="btn-row">
			<button type="primary" class="primary" @tap="register">注册</button>
		</view>
        <view class="action-row">
        	<navigator url="login">登录</navigator>
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
				account: '',
				password: ''
			}
		},
		methods: {
			register() {
				/**
				 * 客户端对账号信息进行一些必要的校验。
				 * 实际开发中，根据业务需要进行处理，这里仅做示例。
				 */
				if (this.account.length < 5) {
					uni.showToast({
						icon: 'none',
						title: '账号最短为 5 个字符'
					});
					return;
				}
				if (this.password.length < 6) {
					uni.showToast({
						icon: 'none',
						title: '密码最短为 6 个字符'
					});
					return;
				}

				const data = {
					account: this.account,
					password: this.password
				}
				//service.addUser(data);
				uni.showToast({
					title: '注册成功'
				});
				uni.navigateBack({
					delta: 1
				});
			}
		}
	}
</script>

<style>
@import url("login.css");
</style>
