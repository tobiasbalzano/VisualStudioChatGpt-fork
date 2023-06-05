﻿using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vs_ChatGpt.Model;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Settings;
using Microsoft.VisualStudio.Settings;

namespace VisualStudioChatGpt.Model
{
    public class MyConfig
    {
        static string key = "MyConfig";

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <returns></returns>
        public static MyConfigModel Get()
        {
            var apiurl = "https://api.openai.com/v1/chat/completions";//默认OpenAI
            var entity = new MyConfigModel()
            {
                model = "gpt-3.5-turbo",
                maxtoken = "500",
                temperature = "0",
                timeout = "60",
                apiurl = apiurl,
            };
            var settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
            var store = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

            if (store.CollectionExists(key))
            {
                entity = JsonConvert.DeserializeObject<MyConfigModel>(store.GetString(key, "Config"));
            }
            if (string.IsNullOrEmpty(entity.apiurl))
            {
                entity.apiurl = apiurl;
            }
            if (string.IsNullOrEmpty(entity.serviceName))
            {
                entity.serviceName = ServiceEnum.OpenAI.ToString();
            }
            return entity;
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="entity"></param>
        public static void Set(MyConfigModel entity)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string json = JsonConvert.SerializeObject(entity);

            var settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
            var store = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

            if (!store.CollectionExists(key))
            {
                store.CreateCollection(key);
            }
            store.SetString(key, "Config", json);
        }
    }
}
