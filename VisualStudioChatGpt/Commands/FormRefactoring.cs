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
using System.Runtime.Remoting.Messaging;

namespace VisualStudioChatGpt.Commands
{
    public partial class FormRefactoring : Form
    {
        internal delegate void MyShowEventHandler(string message);

        private static FormRefactoring instance;

        public string SelectedText { get; set; }

        private int index = 0;

        private List<Messages> messages { get; set; }
        private string tempContent = "";

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
            messages = new List<Messages>();
            txt_a.Padding = new Padding(10, 10, 10, 10);
        }

        private async void FormRefactoring_Load(object sender, EventArgs e)
        {
            this.txt_a.Text = "思考中.....";

            this.txt_q.Text = $"{TypeModel.Refactoring}{SelectedText}";

            tempContent = "";
            await OpenAiAsync(this.txt_q.Text, ShowMessage);
            AppendTextColorful("\r\n\r\n", Color.Black);
            messages.Add(new Messages() { role = "user", content = tempContent });

            this.txt_q.Text = "";
            SetScroll();
            btn_tijiao.Enabled = true;
        }

        private async void btn_tijiao_Click(object sender, EventArgs e)//提交问题
        {
            btn_tijiao.Enabled = false;

            index = 0;
            var q = this.txt_q.Text.Trim();
            AppendTextColorful("问题", Color.Red, true);
            AppendTextColorful($"{q}", Color.Black);
            messages.Add(new Messages() { role = "user", content = q });
            SetScroll();

            tempContent = "";
            AppendTextColorful("答案:", Color.Red, true);
            await OpenAiAsync(this.txt_q.Text, ShowMessage);
            AppendTextColorful("\r\n\r\n", Color.Black);
            messages.Add(new Messages() { role = "user", content = tempContent });

            this.txt_q.Text = "";
            SetScroll();
            btn_tijiao.Enabled = true;
        }

        /// <summary>
        /// 显示消息 换行
        /// </summary>
        /// <param name="message">要显示的消息</param>
        internal void ShowMessage(string message)
        {
            if (index == 0)
            {
                this.txt_a.Text = "";
            }
            if (index % 20 == 0)
            {
                SetScroll();
            }
            AppendTextColorful(message, Color.Black, false, false);
            tempContent += message;
            index++;
        }

        /// <summary>
        /// 设置滚动条位置
        /// </summary>
        private void SetScroll()
        {
            txt_a.SelectionStart = txt_a.Text.Length + 2000;
            txt_a.ScrollToCaret();
        }

        /// <summary>
        /// RichTextBox 追加颜色字体
        /// </summary> 
        /// <param name="text"></param>
        /// <param name="color"></param>
        public void AppendTextColorful(string text, Color color, bool bold = false, bool wrap = true)
        {
            if (wrap)
            {
                text += "\r\n";
            }
            var obj = this.txt_a;
            obj.SelectionStart = obj.TextLength;
            obj.SelectionLength = 0;
            obj.SelectionColor = color;
            if (bold)
            {
                obj.SelectionFont = new Font("宋体", 12f, FontStyle.Bold);
            }
            obj.AppendText(text);
            obj.SelectionColor = obj.ForeColor;
        }


        #region Openai请求

        /// <summary>
        /// 异步打开OpenAI
        /// </summary>
        /// <param name="word">要发送给OpenAI的单词</param>
        /// <param name="showEvent">显示事件处理程序</param>
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
        #endregion
    }

    public class Messages
    {
        public string role { get; set; }
        public string content { get; set; }
    }
}
