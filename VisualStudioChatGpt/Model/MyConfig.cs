using Microsoft.VisualStudio.Shell;
using Newtonsoft.Json;
using System;
using System.IO;
using Vs_ChatGpt.Model;

namespace VisualStudioChatGpt.Model
{
    public class MyConfig
    {
        /// <summary>
        /// Configuration file name
        /// </summary>
        static string configFileName = "VisualStudioChatGpt.json";

        /// <summary>
        /// Read the configuration file
        /// </summary>
        /// <returns></returns>
        public static MyConfigModel Get()
        {
            var apiurl = "https://api.openai.com/v1/chat/completions"; // Default to OpenAI
            var entity = new MyConfigModel()
            {
                model = "gpt-3.5-turbo",
                maxtoken = "500",
                temperature = "0",
                timeout = "30",
                apiurl = apiurl,
            };

            if (File.Exists(getConfigFilePath()))
            {
                string json = File.ReadAllText(getConfigFilePath());
                entity = JsonConvert.DeserializeObject<MyConfigModel>(json);
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
        /// Save the configuration file
        /// </summary>
        /// <param name="entity"></param>
        public static void Set(MyConfigModel entity)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string json = JsonConvert.SerializeObject(entity);

            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path = System.IO.Path.Combine(appDataPath, "Microsoft", "VisualStudio") + "\\" + configFileName;
            File.WriteAllText(getConfigFilePath(), json);
        }

        /// <summary>
        /// Get the real disk path of the configuration file
        /// </summary>
        /// <returns></returns>
        private static string getConfigFilePath()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path = System.IO.Path.Combine(appDataPath, "Microsoft", "VisualStudio") + "\\" + configFileName;
            return path;
        }
    }
}
