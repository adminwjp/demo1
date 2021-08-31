using System.Text.RegularExpressions;

namespace Utility.Helpers
{
    /// <summary> 正则 公共类 </summary>
    public static class RegexHelper
    {
        //https://blog.csdn.net/bmjhappy/article/details/80512917
        /// <summary> 匹配中文字符的正则表达式 </summary>
        public const string Chinese = "[\\u4e00-\\u9fa5]";

        /// <summary>匹配双字节字符(包括汉字在内) </summary>
        public const string ChineseDoubleByte= "[^\\x00-\\xff]";

        /// <summary> 英文字母 </summary>
        public const string Letter = "[a-z|A-Z]";

        /// <summary> 数字 </summary>
        public const string Number = "[0-9]";
        /// <summary>
        /// 匹配中文，英文字母和数字及_ -
        /// <para>code from http://caibaojian.com/zhongwen-regexp.html</para>
        /// </summary>
        public const string ChineseAndLetterAndNumberAndUnderline = "[\\u4e00-\\u9fa5_a-zA-Z0-9-\\-]";

        /// <summary>
        /// 手机号
        /// </summary>
        public const string Phone = "^1[3-9]\\d{9,9}$";


        /// <summary>
        /// 邮箱
        /// </summary>
        public const string Email = "^([a-zA-Z0-9_.-]+)@([0-9A-Za-z.-]+).([a-zA-Z.]{2,6})$";

        /// <summary>
        /// 验证 手机号
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="pattern"> 手机号 正则</param>
        public static bool IsPhone(string phone,string pattern= Phone)
        {
            return Regex.Match(phone, pattern).Success;
        }

        /// <summary>
        /// 验证 邮箱
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="pattern"> 邮箱 正则</param>
        public static bool IsEmail(string email, string pattern = Email)
        {
            return Regex.Match(email, pattern).Success;
        }

        /// <summary> 正则获取值 </summary>
        /// <param name="input">匹配的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="groupnum">获取的位置，索引从0开始</param>
        /// <param name="options">正则匹配模式</param>
        /// <returns></returns>
        public static string GetValue(string input, string pattern, int groupnum = 1, RegexOptions options = RegexOptions.Singleline)
            => System.Text.RegularExpressions.Regex.Match(input, pattern, options).Groups[groupnum].Value;

        /// <summary> 正则匹配是否成功 </summary>
        /// <param name="input">匹配的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="options">正则匹配模式</param>
        /// <returns></returns>
        public static bool IsMatch(string input, string pattern, RegexOptions options = RegexOptions.None) => System.Text.RegularExpressions.Regex.IsMatch(input: input, pattern: pattern, options: options);

        /// <summary> 正则获取正则Match匹配对象 </summary>
        /// <param name="input">匹配的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="options">正则匹配模式</param>
        /// <returns></returns>
        public static Match Match(string input, string pattern, RegexOptions options = RegexOptions.None) => System.Text.RegularExpressions.Regex.Match(input: input, pattern: pattern, options: options);

        /// <summary> 正则获取正则GroupCollection对象 ,即正则分组的信息 </summary>
        /// <param name="input">匹配的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="options">正则匹配模式</param>
        /// <returns></returns>
        public static GroupCollection Grgoups(string input, string pattern, RegexOptions options = RegexOptions.None) => System.Text.RegularExpressions.Regex.Match(input: input, pattern: pattern, options: options).Groups;

        /// <summary> 正则获取正则MatchCollection对象 ,即正则Match集合 </summary>
        /// <param name="input">匹配的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="options">正则匹配模式</param>
        /// <returns></returns>
        public static MatchCollection Matches(string input, string pattern, RegexOptions options = RegexOptions.None) => System.Text.RegularExpressions.Regex.Matches(input: input, pattern: pattern, options: options);
 
    }
}
