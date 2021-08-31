using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Wpf.Attributes;

namespace Wpf.OA
{
    [MenuGroupAttribute(Config = "config/oa.json")]
    public enum OAFlag
    {
        AccountItem = 0,
        /// <summary>
        /// 权限信息
        /// </summary>
        AuthorityOperator = 1,
        /// <summary>
        /// 培训内容信息
        /// </summary>
        BringUpContent = 2,
        /// <summary>
        /// 培训人员信息
        /// </summary>
        BringUpPerson = 3,
        /// <summary>
        /// 职位信息
        /// </summary>
        Duty = 4,
        FamousRace = 5,
        Module = 6,
        /// <summary>
        /// 个人信息
        /// </summary>
        Person = 7,
        /// <summary>
        /// 奖惩信息
        /// </summary>
        ReawrdsAndPunishment = 8,
        /// <summary>
        /// 账套 人员设置
        /// </summary>
        ReckoningList = 9,
        /// <summary>
        /// 账套 名称 信息
        /// </summary>
        ReckoningName = 10,
        /// <summary>
        /// 账套信息
        /// </summary>
        Reckoning = 11,
        Role = 12,
        /// <summary>
        /// 考勤信息
        /// </summary>
        TimeCard = 13,
        User = 14
    }
    public struct OAConstant
    {
        public const string AccountItem = " AccountItem";
        /// <summary>
        /// 权限信息
        /// </summary>
        public const string AuthorityOperator = "AuthorityOperator";
        /// <summary>
        /// 培训内容信息
        /// </summary>
        public const string BringUpContent = "BringUpContent";
        /// <summary>
        /// 培训人员信息
        /// </summary>
        public const string BringUpPerson = "BringUpPerson";
        /// <summary>
        /// 职位信息
        /// </summary>
        public const string Duty = "Duty";
        public const string FamousRace = "FamousRace";
        public const string Module = "Module";
        /// <summary>
        /// 个人信息
        /// </summary>
        public const string Person = " Person";
        /// <summary>
        /// 奖惩信息
        /// </summary>
        public const string ReawrdsAndPunishment = " ReawrdsAndPunishment";
        /// <summary>
        /// 账套 人员设置
        /// </summary>
        public const string ReckoningList = " ReckoningList";
        /// <summary>
        /// 账套 名称 信息
        /// </summary>
        public const string ReckoningName = " ReckoningName";
        /// <summary>
        /// 账套信息
        /// </summary>
        public const string Reckoning = " Reckoning";
        public const string Role = " Role";
        /// <summary>
        /// 考勤信息
        /// </summary>
        public const string TimeCard = " TimeCard";
        public const string User = " User";
    }
}
