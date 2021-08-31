using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Company.Domain.Entities
{
	public class CompanyCatagoryEntity:BaseEntity,System.ComponentModel.DataAnnotations.IValidatableObject
	{
		public const string Table = "t_c_catagory";
		public string ButtonHref1 { get; set; } //按钮1地址
		public string ButtonName1 { get; set; }  //按钮1名称
		public string ButtonHref2 { get; set; } //按钮2地址
		public string ButtonName2 { get; set; } //按钮2名称

		public string Title { get; set; }  //标题
		public string Description { get; set; }
		public string Name { get; set; }
		public CompanyCatagoryFlag Flag { get; set; } //分类标识
		public string Body { get; set; } //媒体内容
		public int Review { get; set; }//评分
		public string Color { get; set; }//颜色
		public string Process { get; set; } //进度
		public string Style { get; set; }//样式
		public string BackgroundImage { get; set; } //背景图片素材地址
		public string Href { get; set; }//品牌链接地址
		public string Feature { get; set; }//品牌特征
		public virtual string Icon { get; set; }
		public string Filter { get; set; } //工作分类 过滤条件
										   // 二 选用 一 timeout wait too long time 90s
		public CompanyCatagoryEntity Parent { get; set; }
		public List<CompanyCatagoryEntity> Children { get; set; }
		[Newtonsoft.Json.JsonProperty("parent_id")]
		public long? parent_id { get; set; }


		public string Tel { get; set; } //联系电话
		public string Logo { get; set; } //公司logo素材 图文logo素材
		public string Logo1 { get; set; } //公司logo1素材

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
		
			yield return null;
        }
    }
}
