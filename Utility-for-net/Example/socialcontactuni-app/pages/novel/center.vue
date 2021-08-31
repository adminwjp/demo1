<template>
	<view class="center">
		<view class="logo">
			<!-- #ifdef MP-WEIXIN -->
			<image
				class="logo-img"
				:src="token ? userinfo.avatar : avatarUrl"
				@click="gobind"
			></image>
			<view class="logo-title">
				<text class="uer-name" v-if="token" @click="gobind">
					Hi，{{ userinfo.nickName }}
				</text>
				<text class="appusername" v-if="token && appusername">({{ appusername }})</text>
				<button
					open-type="getUserInfo"
					v-if="!token"
					id="loginbutton"
					type="primary"
					@getuserinfo="login"
				>
					使用微信登录！
				</button>
			</view>
			<text v-if="!token" class="go-login navigat-arrow">&#xe65e;</text>
			<!-- #endif -->

			<!-- #ifdef APP-PLUS -->
			<image
				class="logo-img"
				@click="toggleavatar"
				:src="token ? userinfo.avatar : avatarUrl"
			></image>
			<view class="logo-title">
				<text v-if="token" @click="toggleavatar">Hi，{{ userinfo.nickName }}</text>
				<button v-if="!token" id="loginbutton" type="primary" @click="gologinpage">
					用户名登录或注册！
				</button>
			</view>
			<text v-if="!token" class="go-login navigat-arrow">&#xe65e;</text>
			<!-- #endif -->
		</view>
		<view class="center-list">
			<view class="center-list-item border-bottom" @click="mylike">
				<text class="list-icon iconfont icon-zans"></text>
				<text class="list-text">我喜欢的</text>
				<text class="navigat-arrow">&#xe65e;</text>
			</view>
			<view class="center-list-item border-bottom" @click="my">
				<text class="list-icon iconfont icon-listview"></text>
				<text class="list-text">我发布的</text>
				<text class="navigat-arrow">&#xe65e;</text>
			</view>
			<view class="center-list-item border-bottom" @click="tougao">
				<text class="list-icon iconfont icon-edit"></text>
				<text class="list-text">立即投稿</text>
				<text class="navigat-arrow">&#xe65e;</text>
			</view>
		</view>
		<view class="center-list" @click="logout" v-if="token">
			<view class="center-list-item">
				<text class="list-icon iconfont icon-jinzhide"></text>
				<text class="list-text">退出登录</text>
				<text class="navigat-arrow">&#xe65e;</text>
			</view>
		</view>
        <novel-tabbar />
	</view>
</template>

<script>
/*
 * tabbar我的页面
 */
import util, { parGetData, parSetData, checklogin } from './common/util.js';
import novelTabbar from './novel-tabbar.vue';
export default {
	components:{novelTabbar},
	data() {
		return {
			avatarUrl: '../../static/logo.png',
			openid: '',
			token: '',
			userinfo: '',
			appusername: ''
		};
	},
	onShow() {
		this.checkstat();
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
	methods: {
		toggleavatar: async function() {
			if (!this.token) {
				uni.navigateTo({
					url: '/platforms/app-plus/login/login'
				});
				return false;
			}
			// 更换头像
			var self = this;
			uni.chooseImage({
				count: 1,
				sizeType: 'compressed ',
				success: res => {
					self.image = res.tempFilePaths[0];
					uni.showLoading({
						title: '上传中',
						icon: 'none'
					});
					uni.uploadFile({
						url: util.apiurl + 'syj/index/setavatar', //仅为示例，非真实的接口地址
						filePath: res.tempFilePaths[0],
						name: 'image',
						header: {
							auth: util.httpauth
						},
						success: res => {
							res.data = JSON.parse(res.data);
							if (res.statusCode == 200 && res.data.code < 1) {
								this.$set(self.userinfo, 'avatar', res.data.imageurl);
								// 存入用户信息
								uni.setStorageSync('userinfo', parSetData(self.userinfo));
							} else {
								uni.showToast({
									title: res.data.msg,
									icon: 'none'
								});
							}
						},
						complete: function() {
							uni.hideLoading();
						}
					});
				}
			});
		},
		gologinpage() {
			uni.navigateTo({
				url: '/platforms/app-plus/login/login'
			});
		},
		tougao() {
			uni.switchTab({
				url: './contribute'
			});
		},
		gobind() {
			if (this.token && !this.appusername) {
				// 去绑定
				uni.navigateTo({
					url: '/platforms/mp-weixin/login/login'
				});
				return false;
			}
		},
		my() {
			if (!this.token) {
				uni.showToast({
					title: '请先登录',
					icon: 'none'
				});
				return false;
			}
			uni.navigateTo({
				url: './my'
			});
		},
		checkstat() {
			var logininfo = util.checklogin();
			this.openid = logininfo.openid;
			this.userinfo = logininfo.userinfo;
			this.token = logininfo.token;
			this.appusername = logininfo.appusername;
		},
		logout() {
			this.openid = '';
			this.userinfo = '';
			this.token = '';
			this.appusername = '';
			uni.removeStorageSync('wxopenid');
			uni.removeStorageSync('userinfo');
			uni.removeStorageSync('token');
			uni.removeStorageSync('appusername');
		},
		mylike() {
			if (!this.token) {
				uni.showToast({
					title: '请先登录',
					icon: 'none'
				});
				return false;
			}
			uni.navigateTo({
				url: '../list/list'
			});
		},
		login(res) {
			util.logincomm(res, this);
		}
	}
};
</script>

<style>
@import './common/common.css';
@import './common/icon.css';
@import './common/uni.css';
page,
view {
	display: flex;
}
page {
	min-height: 100%;
	background-color: #f1f1f1;
}
.index .card {
	margin: 8upx auto;
	padding-bottom: 10upx;
	padding-top: 10upx;
}
.index .card image,
.index .card video {
	display: block;
	max-width: 96%;
	margin: 8upx auto;
}
.index .card video {
	width: 100%;
}
.index .card .car-title-view {
	font-size: 32upx;
	padding: 20upx 25upx;
	display: block;
}
#mask {
	position: fixed;
	width: 100%;
	height: 100%;
	top: 0;
	left: 0;
	z-index: 99;
	background: rgba(0, 0, 0, 0.5);
	overflow: hidden;
}
#showlogin {
	position: fixed;
	width: 50%;
	left: 50%;
	margin-left: -25%;
	top: 30%;
	z-index: 9999;
}
.iconbottom {
	align-items: center;
	text-align: center;
	justify-content: center;
	margin: 25upx auto 15upx;
}
.iconbottom .iconfont {
	display: inline-block;
	margin: auto 50upx;
	font-size: 30upx;
}
.iconbottom .iconfont.active {
	color: #f44336;
}
.iconbottom .iconfont text {
	padding: 0 10upx;
	color: #777777;
	font-size: 20upx;
}
</style>


<style>
.logo {
	position: relative;
	background: #f44336;
}
.center {
	overflow: hidden;
}
.logo-title {
	color: #ffffff;
}
.appusername {
	font-size: 20upx;
	color: #fff;
	margin-left: 10upx;
}
.center .list-icon {
	color: #f44336;
}
#loginbutton {
	display: flex;
	margin: 0;
	border: none;
	box-shadow: none;
	background: transparent;
	font-size: 30upx;
	z-index: 999;
	color: #ffffff;
	width: auto;
	position: absolute;
	top: 0;
	left: 0;
	text-align: center;
	width: 100%;
	height: 100%;
	align-items: center;
	justify-content: center;
}
#loginbutton:after,
#loginbutton:before {
	display: none;
}
</style>
