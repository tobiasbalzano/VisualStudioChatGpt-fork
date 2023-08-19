using Microsoft.VisualStudio.Shell;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualStudioChatGpt.Model;
using static VisualStudioChatGpt.Commands.MyBase;
using MarkdownSharp;

namespace VisualStudioChatGpt.Commands
{
    public partial class FormRefactoring : Form
    {
        internal delegate void MyShowEventHandler(string message);

        private static FormRefactoring instance;

        public string SelectedText { get; set; }

        public static FormRefactoring Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new FormRefactoring();
                    instance.StartPosition = FormStartPosition.CenterScreen;
                }
                return instance;
            }
        }

        public FormRefactoring()
        {
            InitializeComponent();
        }

        private void FormRefactoring_Load(object sender, EventArgs e)
        {
            this.txt_q.Text = $"{TypeModel.Refactoring}{SelectedText}";
        }

        private async void button1_Click(object sender, EventArgs e)//提交问题
        {
            txt_a.Text += "问题:\r\n";
            txt_a.Text += this.txt_q.Text.Trim() + "\r\n\r\n";
            txt_a.Text += "答案:\r\n";
            await OpenAiAsync(this.txt_q.Text, ShowMessage);
            txt_a.Text += "\r\n\r\n=======================================================================================================\r\n\r\n";
            this.txt_q.Text = "";
        }

        /// <summary>
        /// 插入代码
        /// </summary>
        /// <param name="message"></param>
        internal void ShowMessage(string message)
        {
            txt_a.Text += message;
        }

        internal async Task OpenAiAsync(string word, MyShowEventHandler showEvent)
        {
            await ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                var config = MyConfig.Get();
                if (string.IsNullOrEmpty(config.apikey))
                {
                    MessageBox.Show("请设置OpenAI key");

                    FormSetUp.Instance.Show();
                    FormSetUp.Instance.Activate();
                    return;
                }

                var par = new
                {
                    model = config.model,
                    temperature = Convert.ToDouble(config.temperature),
                    stream = true,
                    max_tokens = Convert.ToInt32(config.maxtoken),
                    messages = new List<object> { new { role = "user", content = word } }
                };

                HttpClientHandler handler = new HttpClientHandler();// 创建HttpClientHandler实例
                if (!string.IsNullOrEmpty(config.proxy))
                {
                    handler.Proxy = new WebProxy(config.proxy);// 设置代理服务器地址和端口
                }
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, config.apiurl);
                    request.Content = new StringContent(JsonConvert.SerializeObject(par), Encoding.UTF8);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    if (config.serviceName == ServiceEnum.Azure.ToString())//微软Azure云
                    {
                        request.Content.Headers.Add("api-key", $"{config.apikey}");
                    }
                    else//OpenAI
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", $"{config.apikey}");
                    }

                    using (HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))//发送请求并获取响应
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            response.EnsureSuccessStatusCode();
                            using (Stream responseStream = await response.Content.ReadAsStreamAsync())
                            using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                            {
                                while (!reader.EndOfStream)//逐行读取响应流
                                {
                                    string line = await reader.ReadLineAsync();
                                    if (!string.IsNullOrEmpty(line) && line.Contains("content"))
                                    {
                                        line = line.Remove(0, 5);
                                        var obj = JsonConvert.DeserializeObject<dynamic>(line);
                                        var temp = obj["choices"][0]["delta"]["content"].ToString();
                                        showEvent.Invoke(temp);//插入gpt结果                                        
                                        await Task.Delay(1);
                                    }
                                }
                            }
                        }
                        else
                        {
                            string line = await response.Content.ReadAsStringAsync();
                            var obj = JsonConvert.DeserializeObject<dynamic>(line);
                            var message = obj["error"]["message"].ToString();
                            if (string.IsNullOrEmpty(message))
                            {
                                message = obj["error"]["code"].ToString();
                            }
                            MessageBox.Show(message);

                            FormSetUp.Instance.Show();
                            FormSetUp.Instance.Activate();
                        }
                    }
                }
            });
        }
    }
}
