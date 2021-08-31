using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialContact.Domain.Entities
{
    /// <summary>
    /// 分类 标识
    /// </summary>
    [Flags]
    public enum CatalogFlag 
    {
        None=0x0,
        [Description("学历分类")]
        Edution=0x1,

        [Description("文件分类")]
        File =0x3, 

        [Description("职位分类")]
        Job =0x4,

        [Description("爱好分类")]
        Like =0x5, 

        [Description("技能分类")]
        Skill =0x6, 

        [Description("标签分类")]
        Tag =0x7,

        [Description("角色分类")]
        Role =0x8,

        [Description("文章分类")]
        Article = 0x9,

        [Description("音乐分类")]
        Music = 0xA,

        [Description("视频分类")]
        Video = 0xB,

        [Description("好友分类")]
        Friend = 0xC
    }
}
