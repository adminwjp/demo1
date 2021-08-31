	var areas = ["全球", "中国大陆", "中国香港", "中国台湾", "中国澳门", "韩国", "马来西亚", "澳大利亚", "新加坡", "新西兰", "加拿大", "美国", "日本"];
		var str = "";
		for (var i = 0; i < areas.length; i++) {
			str += "<li>" + areas[i] + "</li>";
		}
		Utility.html(document.querySelector("div.addressu-list > ul "), str);
		var navs = [
			{
				name: "淘宝网首页",
				src: "index.html",
				children: []
			},
			{
				name: "我的淘宝",
				src: "",
				children: [{
					name: "已买到的宝贝",
					src: ""
				}, {
					name: "我的足迹",
					src: ""
				}]
			},
			{
				icon: "fa fa-shopping-cart",
				name: "购物车",
				src: "cart.html",
				children: []
			},
			{
				icon: "fa fa-star",
				name: "收藏夹",
				src: "",
				children: [{
					name: "收藏的宝贝",
					src: ""
				}, {
					name: "收藏的店铺",
					src: ""
				}]
			},
			{
				name: "商品分类",
				src: "",
				children: []
			}, {
				name: "千牛卖家中心",
				src: "",
				children: [{
					name: "免费开店",
					src: ""
				}, {
					name: "已卖出的宝贝",
					src: ""
				}, {
					name: "出售中的宝贝",
					src: ""
				}, {
					name: "卖家服务市场",
					src: ""
				}, {
					name: "卖家培训中心",
					src: ""
				}, {
					name: "体检中心",
					src: ""
				}, {
					name: "问商友",
					src: ""
				}, ]
			},
			{
				name: "联系客服",
				src: "",
				children: [{
						name: "消费者客服",
						src: ""
					},
					{
						name: "卖家客服",
						src: ""
					},
				]
			}, {
				icon: "fa fa-bars",
				name: "网站导航",
				src: "",
				flag: 1,
				children: [{
						name: "主题市场",
						cols: 4,
						children: ["女装", "男装", "内衣", "鞋靴", "箱包", "婴童", "家电", "数码", "手机", "美妆", "珠宝", "眼镜", "手表", "运动",
							"户外", "乐器", "游戏", "动漫", "影视", "美食", "鲜花", "宠物", "农资", "房产", "装修", "建材", "家居", "百货",
							"汽车", "二手车", "办公", "定制", "教育", "卡券", "本地"
						]
					},
					{
						name: "特色市场",
						cols: 3,
						children: ["全球购", "极有家", "阿里拍卖", "淘宝众筹", "飞猪", "农资", "天天特卖", "Outlets", "淘抢购", "试用", "量贩团",
							"阿里翻译"
						]
					},
					{
						name: "阿里APP",
						cols: 3,
						children: ["淘宝", "天猫", "支付宝", "聚划算", "飞猪", "蚂蚁聚宝", "闲鱼", "淘小铺", "阿里钱盾", "钉钉", "高德地图", "虾米音乐",
							"淘票票", "菜鸟裹裹", "阿里云", "网商银行", "阿里邮箱", "阿里众包"
						]
					},
					{
						name: "精彩推荐集",
						cols: 2,
						children: ["余额宝", "大牌捡宝", "淘公仔", "浏览器", "淘宝香港", "淘宝台湾", "淘宝全球", "淘宝东南亚", "闺蜜淘货", "大众评审",
							"淘工作", "阿里巴巴认证"
						]
					}
				]
			}
		];
		str = "";
		var site = "";
	var obj={
		"id": null,
		"flag": 2,
		"logo": "",
		"icon": "",
		"name": "",
		"href": "",
		"count":null,
		"parent": null,
		"children": [],
		"parentId": null
	};
	for (var i = 0; i < navs.length; i++){
		if (navs[i].flag==1) {
			obj.icon = navs[i].icon;
			obj.name = navs[i].name;
			for (var j = 0; j < navs[i].children.length; j++)
			{
				var temp = {
					"id": null,
					"flag": 2,
					"logo": "",
					"icon": "",
					"name": navs[i].children[j].name,
					"href": "",
					"count": navs[i].children[j].cols,
					"parent": null,
					"children": [],
					"parentId": null
				};
				obj.children.push(temp);
				for (var k = 0; k < navs[i].children[j].children.length; k++) {
					var temp1 = {
						"id": null,
						"flag": 2,
						"logo": "",
						"icon": "",
						"name": navs[i].children[j].children[k],
						"href": "",
						"count": null,
						"parent": null,
						"children": [],
						"parentId": null
					};
					temp.children.push(temp1);
				}
			}
		}
		continue;
	}
	console.log(JSON.stringify(obj));
		for (var i = 0; i < navs.length; i++) {
			if (navs[i].children && navs[i].children.length > 0) {
				str += "<li class='" + (navs[i].flag ? "nav-site-show" : "nav-show") + "'" + (navs[i].flag ?
						"style='z-index: 10000;position: relative;'" : "") + "><a href='" + navs[i].src + "'>" +
					(navs[i].icon ? "<i class=\"" + navs[i].icon +
						"\" style='padding-right:5px;color:orange !important;' aria-hidden=\"true\"></i>" :
						"") + navs[i].name +
					"</a><span><i class=\"fa fa-angle-down \" aria-hidden=\"true\"></i></span>";
				if (navs[i].flag) {
					site += "<ul id='nav-site' class='hide'>";
					var wi = (100 % navs[i].children.length == 0 ? (100 / navs[i].children.length - 2) : 100 / navs[
						i].children.length-1) + "%";
					for (var j = 0; j < navs[i].children.length; j++) {
						site += "<li style='width:" + wi + ";'><h4>" + navs[i].children[j].name + "</h4><ul>";
						navs[i].children[j].cols += 1;
						var width = (100 % navs[i].children[j].cols == 0 ? (100 / navs[i].children[j].cols - 1) : 100 / navs[
								i].children[j].cols) +
							"%";
						for (var k = 0; k < navs[i].children[j].children.length; k++) {
							var it = navs[i].children[j].children[k];
							site += "<li style='width:" + width + ";'><a href='' >" + it + "</a></li>";
						}
						site += "</ul></li>";
					}
					site += "</ul>";
					str += site;
					str += "</li>";
					continue;
				} else {
					str += "<ul class='hide nav-right'> ";
				}
				for (var j = 0; j < navs[i].children.length; j++) {
					str += "<li> <a href='" + navs[i].children[j].src + "'>" +
						(navs[i].icon ? "<i class=\"" + navs[i].children[j].icon +
							" style='padding-right:5px;color:orange !important;' \"aria-hidden=\"true\"></i>" : "") + navs[
							i].children[j].name +
						"</a> </li>";
				}
				str += "</ul></li>";
			} else {
				str += "<li><a href='" + navs[i].src + "'>" +
					(navs[i].icon ? "<i class=\"" + navs[i].icon +
						"\" style='padding-right:5px;color:#f40 !important;' aria-hidden=\"true\"></i>" :
						"") + navs[i].name + "</a></li>";
			}
		}
		Utility.html(document.querySelector("ul.right "), str);
		if (site !== "") {
			console.warn(site);
			$("div.nav-site-inner").html(site);
		}
		var mouseAreaToggle = function (e) {
			Utility.toggleClass(document.querySelector("div.addressu-list"), "hide");
		}
		var area = document.querySelector(".nav-show-area");
		Utility.onmouseout(area, mouseAreaToggle);
		Utility.onmouseover(area, mouseAreaToggle);
		var mouseNavToggle = function (e, ele) {
			console.log(e.target.children);
			Utility.toggleClass(ele, "hide");
		};
		var navs = $(".nav-show");
		for (var i = 0; i < navs.length; i++) {
			$(".nav-show").eq(i).mouseout(function (e) {
				$(this).find(" ul").toggleClass("hide");
			});
			$(".nav-show").eq(i).mouseover(function (e) {
				$(this).find("  ul").toggleClass("hide");
			});
		}
		$(".nav-site-show").mouseout(function (e) {
			$("#nav-site").toggleClass("hide");
		});
		$(".nav-site-show").mouseover(function (e) {
			$("#nav-site").toggleClass("hide");
		});

		// var navshow = document.querySelectorAll(".nav-show");
		// var navshowUl = document.querySelectorAll(".nav-show > ul");
		// for (var i = 0; i < navshow.length; i++) {
		// 	var mouseToggle = function (e) {
		// 		mouseNavToggle(e, navshowUl[i]);
		// 	};
		// 	Utility.onmouseout(navshow[i], mouseToggle);
		// 	Utility.onmouseover(navshow[i], mouseToggle);
		// }