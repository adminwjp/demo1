<template>
	<view class="index">
		<block v-for="(item, index) in list" :key="index">
			<view class="card">
				<view class="car-title-view" v-if="item.text">
					<rich-text :nodes="item.text"></rich-text>
				</view>
				<image
					class="card-img"
					v-if="item.img"
					:src="item.img"
					mode="widthFix"
					@click="previmg(item.img)"
				></image>
				<video v-if="item.video" :src="item.video" controls></video>
				<view class="iconbottom">
					<view
						@click="setlike('like', index, item.id)"
						:class="item.type && item.type == 'like' ? 'active' : ''"
						class="iconfont icon-appreciate_light"
					>
						<text>{{ item.like }}</text>
					</view>
					<view
						@click="setlike('hate', index, item.id)"
						:class="item.type && item.type == 'hate' ? 'active' : ''"
						class="icon-oppose_light iconfont"
					>
						<text>{{ item.hate }}</text>
					</view>
					<view @click="showreply(item.id, index)" class="icon-pinglun iconfont">
						<text>{{ item.replycount ? item.replycount : 0 }}</text>
					</view>
				</view>
			</view>
		</block>
		<text class="loadMore" v-if="loading">加载中...</text>
		<button
			open-type="getUserInfo"
			v-if="showlogin"
			id="showlogin"
			type="warn"
			@getuserinfo="login"
		>
			使用微信登录
		</button>
		<view id="mask" @click="showlogin = false" v-if="showlogin"></view>
        <novel-tabbar></novel-tabbar>
	</view>
</template>
<script>
/**
 * 首页
 */
import util, { parGetData, parSetData, checklogin } from './common/util';
 import novelTabbar from './novel-tabbar.vue';
export default {
     components:{novelTabbar},
	data() {
		return {
			/* 为true表示正在刷新 */
			refreshing: false,
			/* 服务商列表 */
			providerList: [],
			/* 数据数组 */
			list: [],
			openid: '',
			userinfo: '',
			token: '',
			/* 当前页数 */
			fetchPageNum: 1,
			/* 为true表示加载中 */
			loading: true,
			showlogin: false
		};
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
	mounted: function() {
		//#ifdef APP-PLUS
		plus.navigator.closeSplashscreen();
		//#endif
	},
	onShow() {
		this.checkstat();
	},
	onLoad() {
		console.log('11111111111111111');
		this.checkstat();
		this.getData();
	},
	onReachBottom() {
		/* 到底部加载 */
		this.getData();
	},
	onPullDownRefresh() {
		// 下拉刷新
		this.refreshing = true;
		this.getData();
	},
	onShareAppMessage() {
		return {
			title: '最新最好笑的趣事趣图，来看下吧',
			path: '/pages/new/new'
		};
	},
	methods: {
		checkstat() {
			var logininfo = util.checklogin();
			this.openid = logininfo.openid;
			this.userinfo = logininfo.userinfo;
			this.token = logininfo.token;
		},
		getData() {
			this.loading = true;
			var self = this;
			console.log(util.apiurl + 'syj/index/getlist');
			uni.request({
				url: util.apiurl + 'syj/index/getlist',
				method: 'POST',
				dataType: 'json',
				data: {
					page: this.refreshing ? 1 : this.fetchPageNum,
					pagesize: 20,
					status: 1,
					order: 'addtime',
					appid:'xybapp',
					token: this.token,
					shebei: util.shebei
				},
				success: function(ret) {
					if (ret.statusCode !== 200) {
						uni.showToast({
							title: '请求数据失败请重试' + ret.statusCode,
							duration: 2000,
							icon: 'none'
						});
					} else {
						if (ret.data.data.length < 1) {
							uni.showToast({
								title: '没有了',
								icon: 'none'
							});
							return false;
						}
						if (self.refreshing && ret.data.data[0].id === self.list[0].id) {
							uni.showToast({
								title: '已经最新',
								icon: 'none'
							});
							self.refreshing = false;
							uni.stopPullDownRefresh();
							return;
						}
						if (self.refreshing) {
							self.refreshing = false;
							uni.stopPullDownRefresh();
							self.list = ret.data.data;
							self.fetchPageNum = 2;
						} else {
							self.list = self.list.concat(ret.data.data);
							self.fetchPageNum += 1;
						}
					}
				},
				fail: res => {
					console.log(JSON.stringify(res));
					uni.showToast({
						title: '请求数据失败请重试1',
						duration: 2000,
						icon: 'none'
					});
				},
				complete: function() {
					this.loading = false;
				},
				header: {
					auth: util.httpauth
				}
			});
		},
		showreply(id, index) {
			uni.setStorage({
				key: 'detail-' + id,
				data: parSetData(this.list[index])
			});
			// 进入回复页面
			uni.navigateTo({
				url: '../detail/detail?id=' + id
			});
		},
		setlike(type, index, tid) {
			util.setlike(this, this.list[index], type, tid);
		},
		previmg(url) {
			uni.previewImage({
				urls: [url]
			});
		},
		/* 登录方法 */
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


