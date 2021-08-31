using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Utility.IO;

namespace Tool
{
    /// <summary>
    /// 
    /// </summary>
    public class StringHelper
    {
        public static void UpdateTargetFramework(string path,string targetFramework,string cureentTargetFramework= "<TargetFramework>(.*?)</TargetFramework>")
        {
            var dirs = Directory.GetDirectories(path);
            var temps = new List<string>();
            foreach (var item in dirs)
            {
                //tempOutput.WriteLine(item);
                //if (item == ".vs" || item.Equals(".vscode")
                //   ||item.Equals("Lib"))
                if (item.EndsWith(".vs") || item.EndsWith(".vscode")
               || item.EndsWith("Lib"))
                {
                    continue;
                }
                var str = item.Replace("\\", "/");
                temps.Add(str);
            }
            Console.WriteLine(temps.Count.ToString());
            foreach (var item in temps)
            {
                // string s = $"{path}/{item}/{item}.csproj";
                string s = $"{item}/{item.Split('/').LastOrDefault()}.csproj";
                string str = FileHelper.ReadFile(s);
                var tar = Regex.Replace(str, cureentTargetFramework, it => { return targetFramework; });
                // tempOutput.WriteLine(tar);
                FileHelper.WriteFile(s, tar); ;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="stringType"></param>
        /// <returns></returns>
        public static string GetString(string str,StringType stringType)
        {
            switch (stringType)
            {
                case StringType.Address:
                    return str.Replace("/", "\\").Replace("/", "\\"); ;
                case StringType.AddressSingle:
                    return str.Replace("//", "/").Replace("\\", "/");
                case StringType.StringSingle:
                    return Regex.Replace(str.Replace("\'", "\\\'").Replace("\"", "\\\'"), "[\\s|\t|\r|\n]+", " ");
                case StringType.String:
                default:
                    return Regex.Replace(str.Replace("\'", "\\\"").Replace("\"", "\\\""), "[\\s|\t|\r|\n]+", " ");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Clear(string str)
        {
            return Regex.Replace(str, "[\\s|\t|\r|\n]+", " ");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<StringEntity> Initial()
        {
            List<StringEntity> stringEntities = new List<StringEntity>();
            stringEntities.Add(new StringEntity() { StringType= StringType.Address,Name="地址正斜线"});
            stringEntities.Add(new StringEntity() { StringType = StringType.AddressSingle, Name = "地址反斜线" });
            stringEntities.Add(new StringEntity() { StringType = StringType.String, Name = "字符串双引号" });
            stringEntities.Add(new StringEntity() { StringType = StringType.StringSingle, Name = "字符串单引号" });
            return stringEntities;
        }

        /// <summary>
        /// 
        /// </summary>
        public enum StringType
        {
            /// <summary>
            /// 
            /// </summary>
            Address,
            /// <summary>
            /// 
            /// </summary>
            AddressSingle,
            /// <summary>
            /// 
            /// </summary>
            String,
            /// <summary>
            /// 
            /// </summary>
            StringSingle
        }

        /// <summary>
        /// 
        /// </summary>
        public class StringEntity
        {
            /// <summary>
            /// 
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public StringType StringType { get; set; }
        }
    }
}
