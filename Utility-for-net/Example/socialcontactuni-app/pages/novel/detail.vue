<template>
	<view class="index">
		<view class="card">
			<view class="car-title-view" v-if="data.text">
				<rich-text :nodes="data.text"></rich-text>
			</view>
			<image
				class="card-img"
				v-if="data.img"
				:src="data.img"
				mode="widthFix"
				@click="previmg(data.img)"
			></image>
			<video v-if="data.video" :src="data.video" controls></video>
			<view class="iconbottom">
				<view
					@click="setlike('like', 0, data.id)"
					:class="data.type && data.type == 'like' ? 'active' : ''"
					class="iconfont icon-appreciate_light"
				>
					<text>{{ data.like }}</text>
				</view>
				<view
					@click="setlike('hate', 0, data.id)"
					:class="data.type && data.type == 'hate' ? 'active' : ''"
					class="icon-oppose_light iconfont"
				>
					<text>{{ data.hate }}</text>
				</view>
			</view>
		</view>
		<text class="loadMore" v-if="loading">加载中...</text>
		<view class="section">
			<form @submit="send">
				<textarea @focus="checklogin" v-model="text" placeholder="期待你的神评论" />
				<button form-type="submit" size="mini">发表评论</button>
			</form>
		</view>

		<view class="uni-list" v-if="replylist.length">
			<view class="uni-list-cell" v-for="(value, key) in replylist" :key="key">
				<view class="uni-media-list">
					<view class="uni-media-list-body">
						<view class="uni-media-list-text-bottom">
							<image
								class="uni-media-list-logo"
								:src="value.avatar ? value.avatar : '../../static/logo.png'"
							></image>
							<text>{{ value.nickName }}</text>
							<text>评论于{{ value.pubtime }}</text>
						</view>
						<view class="uni-media-list-text-top">{{ value.text }}</view>
					</view>
				</view>
			</view>
		</view>

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
    import novelTabbar from './novel-tabbar.vue';
    export default{
        components:{novelTabbar},
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
@import './common/uni.css';
.section {
	display: block;
}
.section form {
	display: block;
	padding: 20upx;
}
.section textarea {
	background: #ffffff;
	display: block;
	margin: auto;
	font-size: 25upx;
	width: 98%;
	height: 100upx;
	padding: 10upx;
}
.section button {
	display: block;
	width: 200upx;
	margin: 10upx 0 10upx auto;
}

.uni-media-list-logo {
	width: 60upx;
	height: 60upx;
}

.uni-media-list-body {
	height: auto;
	justify-content: space-around;
}

.uni-media-list-text-top {
	font-size: 28upx;
	overflow: hidden;
	margin-top: 15upx;
	word-break: break-all;
}

.uni-media-list-text-bottom {
	display: flex;
	flex-direction: row;
	justify-content: flex-start;
	align-items: center;
}
.uni-media-list-text-bottom text {
	margin: auto 15upx;
}
</style>

<script>
/* *
 *评论页面
 * */
import { mapState, mapMutations, mapActions } from 'vuex';
import util, { parGetData, parSetData, checklogin } from './common/util';
export default {
	data() {
		return {
			data: {},
			detailDec: '',
			text: '',
			id: '',
			showlogin: false,
			replylist: [],
			fetchPageNum: 1,
			openid: '',
			token: '',
			userinfo: '',
			loading: true
		};
	},
	onShow() {
		this.checkstat();
		this.fetchPageNum = 1;
		this.getData(this.id);
	},
	onLoad(e) {
		// e.data为获取的字符串
		// data 为传递来的笑话数据结构
		this.id = e.id;
		var self = this;
		uni.getStorage({
			key: 'detail-' + this.id,
			success: function(res) {
				self.data = parGetData(res.data);
				var title = self.data.text;
				if (title) {
					title = title.replace(/<*?>/gi, '').substr(0, 12);
					self.detailDec = title;
					uni.setNavigationBarTitle({
						title: title
					});
				}
			},
			complete: function() {
				this.loading = false;
			}
		});
	},
	onShareAppMessage() {
		return {
			title: this.detailDec ? this.detailDec : '最新最好笑的趣事趣图，来看下吧',
			path: '/pages/detail/detail?id=' + this.id
		};
	},
	methods: {
		checkstat() {
			var logininfo = util.checklogin();
			this.openid = logininfo.openid;
			this.userinfo = logininfo.userinfo;
			this.token = logininfo.token;
		},
		checklogin() {
			if (!this.token) {
				//#ifdef APP-PLUS
				uni.showModal({
					title: '请登录',
					content: '请登录后继续操作',
					confirmText: '去登录',
					success: function(res) {
						if (res.confirm) {
							uni.navigateTo({
								url: '/platforms/app-plus/login/login'
							});
							return false;
						}
					}
				});
				return false;
				//#endif
				this.showlogin = true;
				return false;
			}
			return true;
		},
		send(e) {
			if (!this.checklogin()) {
				return false;
			}
			if (!this.text) {
				uni.showToast({
					title: '请输入评论内容',
					icon: 'none'
				});
				return false;
			}
			var self = this;
			self.replylist.unshift({
				text: this.text,
				avatar: this.userinfo.avatarUrl,
				nickName: this.userinfo.nickName,
				pubtime: '刚刚'
			});
			uni.request({
				url: util.apiurl + 'syj/index/setpinglun',
				method: 'POST',
				data: {
					text: self.text,
					id: self.id,
					token: self.token,
					shebei: util.shebei
				},
				success: ret => {
					if (ret.statusCode == 200 && ret.data.code < 1) {
						self.text = '';
						uni.showToast({
							title: '审核后显示'
						});
						setTimeout(function() {
							uni.hideLoading();
						}, 1000);
					} else {
						uni.showToast({
							title: ret.data.msg,
							icon: 'none'
						});
						setTimeout(function() {
							uni.hideLoading();
						}, 1000);
					}
				},
				fail: function() {
					uni.hideLoading();
				},
				complete: function() {
					this.loading = false;
				},
				header: {
					auth: util.httpauth
				}
			});
		},
		setlike(type, index, tid) {
			util.setlike(this, this.data, type, tid);
		},
		previmg(url) {
			uni.previewImage({
				urls: [url]
			});
		},
		onReachBottom() {
			/* 到底部加载 */
			this.getData(this.id);
		},
		getData(id) {
			var self = this;
			self.loading = true;
			// 获取评论
			uni.request({
				url: util.apiurl + 'syj/index/getpinglun',
				method: 'POST',
				dataType: 'json',
				data: {
					page: self.fetchPageNum,
					pagesize: 20,
					order: 'addtime',
					id: self.id,
					token: self.token,
					shebei: util.shebei
				},
				success: ret => {
					self.loading = false;
					if (ret.statusCode !== 200) {
						uni.showToast({
							title: '请求数据失败请重试' + ret.statusCode,
							duration: 2000,
							icon: 'none'
						});
					} else {
						if (self.fetchPageNum < 2) {
							// 这是刚进入时第一页
						} else if (ret.data.replylist.length < 1) {
							// 大于第一页且无评论
							uni.showToast({
								title: '没有更多评论了',
								icon: 'none'
							});
							return false;
						}
						// 第一页或者大于第一页且有评论时到这里
						self.replylist = self.replylist.concat(ret.data.replylist);
						self.fetchPageNum += 1;
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
					self.loading = false;
				},
				header: {
					auth: util.httpauth
				}
			});
		},
		/* 登录方法 */
		login(res) {
			util.logincomm(res, this);
		} //login end
	}
};
</script>
