﻿#if NET35 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 ||(NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 ||NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0  || NETSTANDARD2_1 )
using System;
using System.Configuration;
using Utility.Helpers;

namespace Utility
{
    /// <summary>
    /// 配置文件 公共类 支持net framework
    /// </summary>
    public partial class ConfigHelper
    {
        
        /// <summary>
        /// appsetting 配置更新 支持net framework core cs(winform wpf)
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
       ///<exception cref="ArgumentNullException"></exception>
        public static void UpdateAppSettings(string key, string value)
        {
            ValidateHelper.ValidateArgumentNull("key",key);
            ValidateHelper.ValidateArgumentNull("value",value);
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            var isModified = false;
            foreach (System.Configuration.KeyValueConfigurationElement item in config.AppSettings.Settings)
            {
                if (item.Key.Equals(key))
                {
                    isModified = true;
                    break;
                }
            }
            if (isModified)
            {
                config.AppSettings.Settings.Remove(key);
            }
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Full);
            System.Configuration.ConfigurationManager.RefreshSection("AppSettings");
        }

        /// <summary>
        /// ConnectionStrings 配置更新 支持net framework core cs(winform wpf)
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="connectionString">连接地址</param>
        /// <param name="providerName">驱动名称</param>
        ///<exception cref="ArgumentNullException"></exception>
        public static void UpdateConnectionStrings(string name, string connectionString, string providerName)
        {
            ValidateHelper.ValidateArgumentNull("name", name);
            ValidateHelper.ValidateArgumentNull("connectionString",connectionString);
            ValidateHelper.ValidateArgumentNull("providerName",providerName);
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            var isModified = false;
            foreach (System.Configuration.ConnectionStringSettings item in config.ConnectionStrings.ConnectionStrings)
            {
                if (item.Name.Equals(name))
                {
                    isModified = true;
                    break;
                }
            }
            if (isModified)
            {
                config.ConnectionStrings.ConnectionStrings.Remove(name);
            }
            config.ConnectionStrings.ConnectionStrings.Add(new System.Configuration.ConnectionStringSettings(name,connectionString,providerName) );
            config.Save(System.Configuration.ConfigurationSaveMode.Full);
            System.Configuration.ConfigurationManager.RefreshSection("ConnectionStrings");
        }

        /// <summary>
        /// appsetting  支持net framework core cs(winform wpf)
        /// </summary>
        /// <param name="key">键</param>
        ///<exception cref="ArgumentNullException"></exception>
        public static string GetAppSettings(string key)
        {
            ValidateHelper.ValidateArgumentNull("key", key);
            return System.Configuration.ConfigurationManager.AppSettings[key].ToString();
        }

        /// <summary>
        /// ConnectionStrings  支持net framework core cs(winform wpf)
        /// </summary>
        /// <param name="name">名称</param>
        ///<exception cref="ArgumentNullException"></exception>
        public static string GetConnectionString(string name)
        {
            ValidateHelper.ValidateArgumentNull("name", name);
            return System.Configuration.ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        /// <summary>
        /// ConnectionStrings  支持net framework core cs(winform wpf)
        /// </summary>
        /// <param name="name">名称</param>
        ///<exception cref="ArgumentNullException"></exception>
        public static string GetProviderName(string name)
        {
            ValidateHelper.ValidateArgumentNull("name", name);
            return System.Configuration.ConfigurationManager.ConnectionStrings[name].ProviderName;
        }

        /// <summary>
        /// ConnectionStrings  支持net framework core cs(winform wpf)
        /// </summary>
        /// <param name="name">名称</param>
        ///<exception cref="ArgumentNullException"></exception>
        public static System.Configuration.ConnectionStringSettings GetConnectionStringSettings(string name)
        {
            ValidateHelper.ValidateArgumentNull("name",name);
            return System.Configuration.ConfigurationManager.ConnectionStrings[name];
        }
    }
}
#endif