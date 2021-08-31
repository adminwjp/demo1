 const tempNovelToobar=[{
                index: 0,
				"pagePath": "pages/novel/index",
				"iconPath": "static/index-0.png",
				"selectedIconPath": "static/index-1.png",
				"text": "首页"
			},
            {
                index: 1,
				"pagePath": "pages/novel/new",
				"iconPath": "static/index-0.png",
				"selectedIconPath": "static/index-1.png",
				"text": "最新"
			},
			{
                index: 2,
				//"pagePath": "pages/novel/hot",
				"iconPath": "static/hot-0.png",
				"selectedIconPath": "static/hot-1.png",
				"text": "最热"
			},
			{
                index: 3,
				//"pagePath": "pages/novel/contribute",
				"iconPath": "static/tag-0.png",
				"selectedIconPath": "static/tag-1.png",
				"text": "投稿"
			},
			{
                index: 4,
				//"pagePath": "pages/novel/center",
				"iconPath": "static/center-0.png",
				"selectedIconPath": "static/center-1.png",
				"text": "我的"
			}
		];
const tempToobar=[{
            index: 0,
            "pagePath": "pages/tabBar/component/component",
            "iconPath": "static/component.png",
            "selectedIconPath": "static/componentHL.png",
            "text": "内置组件"
        },
        {
            index: 1,
            "pagePath": "pages/main/component/component",
            "iconPath": "static/api.png",
            "selectedIconPath": "static/apiHL.png",
            "text": "首页"
        },
        {
            index: 2,
            "pagePath": "pages/tabBar/API/API",
            "iconPath": "static/api.png",
            "selectedIconPath": "static/apiHL.png",
            "text": "接口"
        },
        {
            index: 3,
            "pagePath": "pages/tabBar/extUI/extUI",
            "iconPath": "static/extui.png",
            "selectedIconPath": "static/extuiHL.png",
            "text": "扩展组件"
        },
        {
            index: 4,
            "pagePath": "pages/tabBar/template/template",
            "iconPath": "static/template.png",
            "selectedIconPath": "static/templateHL.png",
            "text": "模板"
        }
    ];

function resetTabBar(reset){
    if(reset){
        for (var i = 0; i < tempToobar.length; i++) {
            customItem(tempToobar[i]);
        }
    }else{
       for (var i = 0; i < tempNovelToobar.length; i++) {
           customItem(tempNovelToobar[i]);
        }
    }
    setTabBar(false);
}
function destroyed(index) {
    uni.removeTabBarBadge({index: index })
    uni.hideTabBarRedDot({ index: index })
    uni.showTabBar()
    uni.setTabBarStyle({
        color: '#7A7E83',
        selectedColor: '#007AFF',
        backgroundColor: '#F8F8F8',
        borderStyle: 'black'
    })
    uni.setTabBarItem(tempToobar[index])
}

function setTabBarBadge(index,num) {
    if(num>0){
        //移除红点
        uni.hideTabBarRedDot({
            index: index
        });
         //设置文本
        uni.setTabBarBadge({
            index: index,
            text: num.toString()
        });
    }else{
        this.remove(index);
    }
}
function   remove(index){
    //移除红点
    uni.hideTabBarRedDot({
        index: index
    });
      //移除文本
    uni.removeTabBarBadge({
        index: index
    });
}
function showTabBarRedDot(index,show) {
    if(show){
        //移除文本
        uni.removeTabBarBadge({
            index: index
        });
        //设置红点
        uni.showTabBarRedDot({
            index: index
        })
    }
    else{
        this.remove(index);
    }
}
function	setTabBar(hiddern) {
    if (hiddern) {
        uni.hideTabBar()
    } else {
        uni.showTabBar()
    }
}
function	customStyle(customedStyle) {
    if (customedStyle) {
        uni.setTabBarStyle({
            color: '#7A7E83',
            selectedColor: '#007AFF',
            backgroundColor: '#F8F8F8',
            borderStyle: 'black'
        })
    } else {
        uni.setTabBarStyle({
            color: '#FFF',
            selectedColor: '#007AFF',
            backgroundColor: '#000000',
            borderStyle: 'black'
        })
    }
}
//更改名称
function	customItem(tabBarOptions) {
    uni.setTabBarItem(tabBarOptions)
}
export  {
tempNovelToobar,
tempToobar,
destroyed,
customItem,
customStyle,
setTabBar,
showTabBarRedDot,
setTabBarBadge,
resetTabBar
}
