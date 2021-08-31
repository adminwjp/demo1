<template>
	<view class="page">
		<swiper indicator-dots="true">
			<swiper-item v-for="(img, key) in imgUrls" :key="key"><image :src="img" /></swiper-item>
		</swiper>
		<view class="uni-padding-wrap uni-common-mt">
			<view class="uni-title">

                <!-- 列表详情 -->
                <view class="banner" @click="goDetail(banner)">
                        <image class="banner-img" :src="banner.cover"></image>
                        <view class="banner-title">{{ banner.title }}</view>
                </view>
                <view class="uni-list">
                    <view class="uni-list-cell" hover-class="uni-list-cell-hover" v-for="(value, key) in listData" :key="key" >
                        <view v-if="value.type!=='ad'" class="uni-media-list" @click="goDetail(value)">
                            <image class="uni-media-list-logo" :src="value.cover"></image>
                            <view class="uni-media-list-body">
                                <view class="uni-media-list-text-top">{{ value.title }}</view>
                                <view class="uni-media-list-text-bottom">
                                    <text>{{ value.author_name }}</text>
                                    <text>{{ value.published_at }}</text>
                                </view>
                            </view>
                        </view>
                        <!--  信息流广告 -->
                        <view v-if="value.type =='ad'" style="padding-left: 22rpx;">
                           <!--  <ad unit-id="adunit-01b7a010bf53d74e"></ad>  -->
                        </view>
                    </view>
                </view>
                <uni-load-more :status="status"  :icon-size="16" :content-text="contentText" />

			</view>
		</view>
		<view style="height: 1000upx;"></view>
	</view>
</template>

<script>
    import uniLoadMore from '@/components/uni-load-more/uni-load-more.vue';
    var dateUtils = require('../../../../common/util.js').dateUtils;
export default {
    components:{uniLoadMore},//列表详情所需组件
	data() {
		return {
			showSwiper: false,
			imgUrls: [
				'https://img-cdn-qiniu.dcloud.net.cn/uniapp/images/muwu.jpg',
				'https://img-cdn-qiniu.dcloud.net.cn/uniapp/images/cbd.jpg'
			],
            //列表详情所需
            banner: {},
            listData: [],
            last_id: '',
            reload: false,
            status: 'more',
            contentText: {
            	contentdown: '上拉加载更多',
            	contentrefresh: '加载中',
            	contentnomore: '没有更多'
            },
            column: 'id,post_id,title,author_name,cover,published_at' //需要的字段名
		};
	},
	/**
	 * 当 searchInput 配置 disabled 为 true 时触发
	 */
	onNavigationBarSearchInputClicked(e) {
		console.log('事件执行了')
		uni.navigateTo({
			url: '/pages/template/nav-search-input/detail/detail'
		});
	},
	/**
	 *  点击导航栏 buttons 时触发
	 */
	onNavigationBarButtonTap() {
		uni.showModal({
			title: '提示',
			content: '用户点击了功能按钮，这里仅做展示。',
			success: res => {
				if (res.confirm) {
					console.log('用户点击了确定');
				}
			}
		});
	},
    //列表详情
   onLoad() {
   	this.getBanner();
   	this.getList();
   },
   onPullDownRefresh() {
   	this.reload = true;
   	this.last_id = '';
   	this.getBanner();
   	this.getList();
   },
   onReachBottom() {
   	this.status = 'more';
   	this.getList();
   },
   methods: {
   	getBanner() {
        let data = {
        	column: this.column //需要的字段名
        };
   		uni.request({
   			url: 'https://unidemo.dcloud.net.cn/api/banner/36kr',
   			data: data,
   			success: data => {
   				uni.stopPullDownRefresh();
   				if (data.statusCode == 200) {
   					this.banner = data.data;
   				}
   			},
   			fail: (data, code) => {
   				console.log('fail' + JSON.stringify(data));
   			}
   		});
   	},
   	getList() {
   		var data = {
   			column: this.column //需要的字段名
   		};
   		if (this.last_id) {
   			//说明已有数据，目前处于上拉加载
   			this.status = 'loading';
   			data.minId = this.last_id;
   			data.time = new Date().getTime() + '';
   			data.pageSize = 10;
   		}
   		uni.request({
   			url: 'https://unidemo.dcloud.net.cn/api/news',
   			data: data,
   			success: data => {
   				if (data.statusCode == 200) {
   					let list = this.setTime(data.data);
   					this.listData = this.reload ? list : this.listData.concat(list);
   					this.last_id = list[list.length - 1].id;
   					this.reload = false;
   				}
   			},
   			fail: (data, code) => {
   				console.log('fail' + JSON.stringify(data));
   			}
   		});
   	},
   	goDetail: function(e) {
   		// 				if (!/前|刚刚/.test(e.published_at)) {
   		// 					e.published_at = dateUtils.format(e.published_at);
   		// 				}
   		let detail = {
   			author_name: e.author_name,
   			cover: e.cover,
   			id: e.id,
   			post_id: e.post_id,
   			published_at: e.published_at,
   			title: e.title
   		};
   		uni.navigateTo({
   			url: '../list2detail-detail/list2detail-detail?detailDate=' + encodeURIComponent(JSON.stringify(detail))
   		});
   	},
   	setTime: function(items) {
   		var newItems = [];
   		items.forEach(e => {
               //信息流中插入广告，微信限制一页只允许出现一个广告
               if( this.listData.length==0 && newItems.length==5){
                   newItems.push({type:'ad'});
               }
   			newItems.push({
   				author_name: e.author_name,
   				cover: e.cover,
   				id: e.id,
   				post_id: e.post_id,
   				published_at: dateUtils.format(e.published_at),
   				title: e.title
   			});
   		});
   		return newItems;
   	}
   }
};
</script>

<style>
image,
swiper,
.img-view {
	width: 750upx;
	height: 500upx;
}
.page-section-title {
	margin-top: 50upx;
}
</style>

<style>
.banner {
	height: 360upx;
	overflow: hidden;
	position: relative;
	background-color: #ccc;
}

.banner-img {
	width: 100%;
}

.banner-title {
	max-height: 84upx;
	overflow: hidden;
	position: absolute;
	left: 30upx;
	bottom: 30upx;
	width: 90%;
	font-size: 32upx;
	font-weight: 400;
	line-height: 42upx;
	color: white;
	z-index: 11;
}

.uni-media-list-logo {
	width: 180upx;
	height: 140upx;
}

.uni-media-list-body {
	height: auto;
	justify-content: space-around;
}

.uni-media-list-text-top {
	height: 74upx;
	font-size: 28upx;
	overflow: hidden;
}

.uni-media-list-text-bottom {
	display: flex;
	flex-direction: row;
	justify-content: space-between;
}
</style>
