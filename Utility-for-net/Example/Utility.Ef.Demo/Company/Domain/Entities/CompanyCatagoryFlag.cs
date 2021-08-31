using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Entities
{
	public enum CompanyCatagoryFlag
	{
		None = 0, //[]
		Role = 1, //角色分类
		Nav = 2,//官网导航信息
		Service = 3, //官网服务信息
		Work = 4, //工作分类
		Brand = 5, //公司品牌
		BasicCategory = 6, //基础 分类
		Social = 7,
		Media = 8, //媒体
		Skill = 9, //技能
		Skin = 10, //皮肤
		TestimonialPerson =11, //关于我们 团队
		Theme = 12, //官网主题信息 分类 1:公司 2:支持 3 :开发者 4 :我们的合作伙伴
	    Company=13,
		Team=14,
		WorkCategory = 15,
		About = 16,
		Main=17
	}
}
