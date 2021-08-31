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
					<view v-if="item.status != 1" class="active iconfont">
						<text>{{ item.status < 1 ? '审核中' : '未通过' }}</text>
					</view>
					<view class="iconfont icon-appreciate_light">
						<text>{{ item.like }}</text>
					</view>
					<view class="icon-oppose_light iconfont">
						<text>{{ item.hate }}</text>
					</view>
					<view @click="showreply(item.id, index)" class="icon-pinglun iconfont">
						<text>{{ item.replycount ? item.replycount : 0 }}</text>
					</view>
				</view>
			</view>
		</block>
		<text class="loadMore" v-if="loading">加载中...</text>
        <navigator></navigator>
	</view>
</template>

<script>
/**
 * 我的投稿页面
 */
import { mapState, mapMutations } from 'vuex';
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
			loading: true
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
	onLoad() {
		var self = this;
		this.checkstat();
		if (!this.token) {
			uni.showToast({
				title: '请先登录',
				icon: 'none'
			});
			uni.switchTab({
				url: '../center/center'
			});
			return false;
		}
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
	methods: {
		checkstat() {
			var logininfo = util.checklogin();
			this.openid = logininfo.openid;
			this.userinfo = logininfo.userinfo;
			this.token = logininfo.token;
		},
		previmg(url) {
			uni.previewImage({
				urls: [url]
			});
		},
		showreply(id, index) {
			// 进入回复页面
			uni.setStorage({
				key: 'detail-' + id,
				data: parSetData(this.list[index])
			});
			uni.navigateTo({
				url: '../detail/detail?id=' + id
			});
		},
		getData() {
			this.loading = true;
			uni.request({
				url: util.apiurl + 'syj/index/mytougao',
				method: 'POST',
				dataType: 'json',
				data: {
					page: this.refreshing ? 1 : this.fetchPageNum,
					pagesize: 20,
					order: 'addtime',
					token: this.token,
					shebei: util.shebei
				},
				success: ret => {
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
						this.list = this.list.concat(ret.data.data);
						this.fetchPageNum += 1;
					}
				},
				fail: res => {
					uni.showToast({
						title: '请求数据失败请重试',
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
		}
	}
};
</script>

<style>
.iconbottom {
	align-items: center;
	text-align: center;
	justify-content: center;
	margin: 20upx auto 10upx;
}
.iconbottom .iconfont {
	display: inline-block;
	margin: auto 50upx;
	font-size: 30upx;
}
.iconbottom .active {
	color: #f44336;
}
.iconbottom .iconfont text {
	padding: 0 10upx;
	color: #777777;
	font-size: 20upx;
}
</style>
