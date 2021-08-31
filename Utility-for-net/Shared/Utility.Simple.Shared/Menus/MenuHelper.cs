#if !( NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System.Collections.Generic;
using System.Xml;

namespace Utility.Menus
{
    /// <summary>
    ///菜单帮助 类 用于 wpf winform
    /// </summary>
    public class MenuHelper
    {
        /// <summary>
        /// 加载菜单
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static List<MenuEntry> Load(string xml)
        {
            List<MenuEntry> menus = new List<MenuEntry>();
            XmlDocument document = new XmlDocument();
            document.Load(xml);//LoadXml 该方法从字符串中读取   XML
            if (document.DocumentElement.Name.Equals("Root"))
            {
           
                foreach (XmlNode xmlNode in document.DocumentElement.SelectNodes("Menu"))
                {
                    MenuEntry menu = new MenuEntry() { Header = xmlNode.Attributes["Header"].Value, TypeName = xmlNode.Attributes["TypeName"]?.Value,Children=new List<MenuEntry>() };
                    menus.Add(menu);
                    CursionChildrenMenu(menu, xmlNode);
                }
            }
            return menus;
        }

        /// <summary>
        /// 递归 更新 菜单信息
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="node"></param>
        public static void CursionChildrenMenu(MenuEntry menu,XmlNode node)
        {

            if (node.SelectSingleNode("Children") != null)
            {
                foreach (XmlNode childrenNode in node.SelectNodes("Children/Menu"))
                {
                    MenuEntry childrenMenu = new MenuEntry() { Header = childrenNode.Attributes["Header"].Value, TypeName = childrenNode.Attributes["TypeName"]?.Value,Children=new List<MenuEntry>() };
                    menu.Children.Add(childrenMenu);
                    CursionChildrenMenu(childrenMenu,childrenNode);
                }
            }
           
        }

        /// <summary>
        /// 加载 数据
        /// </summary>
        /// <param name="xml"></param>
        public static void LoadXml(string xml, List<MenuEntry> menus)
        {
            //var menuEntries=MenuHelper.Load(xml);
            //this.Menus.AddRange(menuEntries);
            //return;
            XmlDocument document = new XmlDocument();
            document.Load(xml);
            var root = document.SelectSingleNode("Root");
            foreach (XmlNode me in root.SelectNodes("Menu"))
            {
                var menu = new MenuEntry() { Header = me.Attributes["Header"].Value, Children = new List<MenuEntry>() };
                SetConstractorEntry(me, menu);
                menus.Add(menu);
                CursionsXml(me.SelectNodes("Children/Menu"), menu);
            }
        }
        private static void SetConstractorEntry(XmlNode node, MenuEntry menu)
        {
            if (node.Attributes["Flag"] != null)
            {
                menu.Flag = node.Attributes["Flag"].Value;
            }
            if (node.Attributes["TypeName"] != null)
            {
                menu.TypeName = node.Attributes["TypeName"].Value;
                if (node.SelectNodes("Constractor") != null && node.SelectNodes("Constractor").Count > 0)
                {
                    menu.Arags = new List<ConstractorEntry>();
                    foreach (XmlNode item in node.SelectNodes("Constractor"))
                    {
                        var constractorEntry = new ConstractorEntry() { Name = item.Attributes["Name"].Value, Value = item.Attributes["Value"].Value };
                        menu.Arags.Add(constractorEntry);

                    }
                }
            }
        }
        private static void CursionsXml(XmlNodeList nodeList, MenuEntry menu)
        {
            foreach (XmlNode ch in nodeList)
            {
                var chil = new MenuEntry() { Header = ch.Attributes["Header"].Value };
                SetConstractorEntry(ch, chil);
                menu.Children.Add(chil);
                if (ch.HasChildNodes && ch.SelectNodes("Children/Menu").Count > 0)
                {
                    chil.Children = new List<MenuEntry>();
                    CursionsXml(ch.SelectNodes("Children/Menu"), chil);
                }
            }
        }
    }
}
#endif