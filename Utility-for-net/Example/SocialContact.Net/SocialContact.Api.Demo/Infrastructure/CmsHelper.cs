using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tunynet.CMS;

namespace SocialContact.Infrastructure
{
    public class CmsHelper
    {
        /// <summary>
        /// 热点图片
        /// </summary>
        /// <returns></returns>
        public static List<ContentItem> _HotarticleImg()
        {
            //一周热文
            var hotcmsImgList = new List<ContentItem>();
            var contentItems = ContentItemService.GetTopContentItemsofModelKey(6, ContentModelKeys.Instance().Image(), DateTime.Now.AddDays(-7), ContentItemSortBy.HitTimes);
            hotcmsImgList.AddRange(contentItems);
            if (hotcmsImgList.Count < 6)
            {
                var hotcmsImgListMonth = ContentItemService.GetTopContentItemsofModelKey(6, ContentModelKeys.Instance().Image(), DateTime.Now.AddMonths(-1), ContentItemSortBy.HitTimes);
                if (hotcmsImgListMonth.Count() > 0)
                    hotcmsImgList = hotcmsImgList.Union(hotcmsImgListMonth).ToList();
            }
            return hotcmsImgList;
        }
    }
}
