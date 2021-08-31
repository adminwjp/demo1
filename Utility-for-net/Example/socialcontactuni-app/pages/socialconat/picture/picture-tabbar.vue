<template>
	<view class="uni-goods-nav tabbar_bottom">
		<!-- 底部占位 -->
		<view class="uni-tab__seat" />
		<view class="uni-tab__cart-box flex">
			<view :class="{'uni-tab__right':fill}" class="flex uni-tab__cart-sub-right ">
				<view v-for="(item,index) in buttonGroup" :key="index" :style="{backgroundColor:item.backgroundColor,color:item.color}"
                class="flex uni-tab__cart-button-right" @click="buttonClick(index,item)">
                    <view class="uni-tab__icon">
                    	<image class="image" :src="item.icon" mode="widthFix"  @click="buttonClick(index,item)"/>
                    </view>
                    <text class="uni-tab__text" @click="buttonClick(index,item)">{{ item.text }}</text>
                    <view class="flex uni-tab__dot-box">
                    	<text v-if="item.info" :class="{ 'uni-tab__dots': item.info > 9 }" class="uni-tab__dot ">{{ item.info }}</text>
                    </view>
                 </view>
			</view>
		</view>
	</view>
</template>

<script>
	export default {
		name: 'PictureTabbar',
		props: {
			options: {
				type: Array,
				default () {
					return []
				}
			},
			buttonGroup: {
				type: Array,
				default () {
					return [{
						icon: './static/index-0.png',
                        url:'/pages/socialconat/picture/new/new',
						text: '最新'
					}, {
						icon: './static/hot-0.png',
                        url:'/pages/socialconat/picture/hot/hot',
						text: '推荐'
					}, {
						icon: './static/tag-0.png',
                        url:'/pages/socialconat/picture/tag/tag',
						text: '分类'
					}, {
						icon: './static/center-0.png',
                        url:'/pages/socialconat/picture/center/center',
						text: '我的'
					}
					]
				}
			},
			fill: {
				type: Boolean,
				default: false
			}
		},
		methods: {
			buttonClick(index, item) {
                console.log(index);
                uni.redirectTo({
                    url:this.buttonGroup[index].url
                })
                return;
				if (uni.report) {
					uni.report(item.text, item.text)
				}
				this.$emit('buttonClick', {
					index,
					content: item
				})
			}
		}
	}
</script>

<style scoped>
	.flex {
		/* #ifndef APP-NVUE */
		display: flex;
		/* #endif */
		flex-direction: row;
	}

	.uni-goods-nav {
		/* #ifndef APP-NVUE */
		display: flex;
		/* #endif */
		flex: 1;
		flex-direction: row;
	}
    .tabbar_bottom {
    	/* #ifndef APP-NVUE */
    	display: flex;
    	/* #endif */
    	flex-direction: column;
    	position: fixed;
    	left: 0;
    	right: 0;
    	bottom: 0;
    }
	.uni-tab__cart-box {
		flex: 1;
		height: 50px;
		background-color: #fff;
		z-index: 900;
	}

	.uni-tab__cart-sub-right {
		flex: 1;
	}

	.uni-tab__right {
		margin: 5px 0;
		margin-right: 10px;
		border-radius: 100px;
		overflow: hidden;
	}


	.uni-tab__icon {
		width: 18px;
		height: 18px;
	}

	.image {
		width: 18px;
		height: 18px;
	}

	.uni-tab__text {
		margin-top: 3px;
		font-size: 24rpx;
		color: #646566;
	}

	.uni-tab__cart-button-right {
		/* #ifndef APP-NVUE */
		display: flex;
		flex-direction: column;
		/* #endif */
		flex: 1;
		justify-content: center;
		align-items: center;
	}

	.uni-tab__cart-button-right-text {
		font-size: 28rpx;
		color: #fff;
	}

	.uni-tab__cart-button-right:active {
		opacity: 0.7;
	}

	.uni-tab__dot-box {
		/* #ifndef APP-NVUE */
		display: flex;
		flex-direction: column;
		/* #endif */
		position: absolute;
		right: -2px;
		top: 2px;
		justify-content: center;
		align-items: center;
		/* width: 0;
 */
		/* height: 0;
 */
	}

	.uni-tab__dot {
		/* width: 30rpx;
 */
		/* height: 30rpx;
 */
		padding: 0 4px;
		line-height: 15px;
		color: #ffffff;
		text-align: center;
		font-size: 12px;
		background-color: #ff0000;
		border-radius: 15px;
	}

	.uni-tab__dots {
		padding: 0 4px;
		/* width: auto;
 */
		border-radius: 15px;
	}

	.uni-tab__color-y {
		background-color: #ffa200;
	}

	.uni-tab__color-r {
		background-color: #ff0000;
	}
</style>
