using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using Task = System.Threading.Tasks.Task;
using EnvDTE;
using System.Windows.Forms;
using System.Collections.Generic;
using VisualStudioChatGpt.Model;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;

namespace VisualStudioChatGpt.Commands
{
    internal class MyBase
    {
        // Define the delegate type for event handlers.
        internal delegate void MyShowEventHandler(string message);

        internal delegate void MyStartEventHandler();

        internal delegate void MyEndEventHandler();

        /// <summary>
        /// Insert node object.
        /// </summary>
        internal EditPoint insertPoint;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        internal static readonly Guid CommandSet = new Guid("c43b20df-6d16-49bc-b783-8bb7f5c6ff4e");

        internal MyBase()
        {

        }

        /// <summary>
        /// Add custom event.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="handler"></param>
        /// <param name="commandService"></param>
        internal void AddEvent(int id, EventHandler handler, OleMenuCommandService commandService)
        {
            var _obj = new CommandID(CommandSet, id);
            var menuItem = new MenuCommand(handler, _obj);
            commandService.AddCommand(menuItem);
        }

        #region Get Selected Content

        /// <summary>
        /// Get selected content.
        /// </summary>
        /// <returns></returns>
        internal async System.Threading.Tasks.Task<string> GetSelectedTextAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var ret = "";

            DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));
            TextSelection selection = dte.ActiveDocument.Selection as TextSelection;

            if (selection != null && !selection.IsEmpty)
            {
                ret = selection.Text;
            }

            if (string.IsNullOrEmpty(ret))
            {
                MessageBox.Show(TypeModel.Message, "Warning Prompt");
            }

            return ret;
        }

        #endregion

        #region Insert Content After Selected Code

        /// <summary>
        /// Insert content after selected code.
        /// </summary>
        /// <param name="preValue">Pre-insert values.</param>
        /// <param name="word">chatgpt Search text content.</param>
        /// <param name="problem">Ask for information.</param>
        /// <param name="position">Insert location.</param>
        /// <returns></returns>
        internal async Task InsertChatGptAsync(string word, InsertPointEnum position)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));
            TextSelection selection = dte.ActiveDocument.Selection as TextSelection;

            if (selection != null && !selection.IsEmpty)
            {
                if (position == InsertPointEnum.Before) //Before the code
                {
                    this.insertPoint = selection.TopPoint.CreateEditPoint();
                }
                else if (position == InsertPointEnum.After) //After the code
                {
                    this.insertPoint = selection.BottomPoint.CreateEditPoint();
                }
                else if (position == InsertPointEnum.Replace) //Comment the current code and insert afterwards
                {
                    var _insertPoint = selection.TopPoint.CreateEditPoint();
                    _insertPoint.Insert("/*\r\n");

                    _insertPoint = selection.BottomPoint.CreateEditPoint();
                    _insertPoint.Insert("\r\n*/\r\n");

                    this.insertPoint = selection.BottomPoint.CreateEditPoint();
                }

                // Add event handler
                _ = OpenAiAsync(word, VirShowMessage, VirStart, VirEnd);
            }
        }

        #endregion

        #region Insert Constant

        /// <summary>
        /// Insert Constant
        /// </summary>
        /// <param name="content"></param>
        internal async Task InsertConstAsync(string content, InsertPointEnum position)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));
            TextSelection selection = dte.ActiveDocument.Selection as TextSelection;

            if (selection != null && !selection.IsEmpty)
            {
                if (position == InsertPointEnum.Before) //Insert before code
                {
                    var _insertPoint = selection.TopPoint.CreateEditPoint();
                    _insertPoint.Insert(content);
                }
                else if (position == InsertPointEnum.After) //Insert after code
                {
                    var _insertPoint = selection.BottomPoint.CreateEditPoint();
                    _insertPoint.Insert(content);
                }
                else if (position == InsertPointEnum.Replace) //Comment current code and insert after
                {
                    var _insertPoint = selection.TopPoint.CreateEditPoint();
                    _insertPoint.Insert("/*\n");

                    _insertPoint = selection.BottomPoint.CreateEditPoint();
                    _insertPoint.Insert("\n*/\n");

                    _insertPoint = selection.BottomPoint.CreateEditPoint();
                    _insertPoint.Insert(content);
                }
            }
        }

        #endregion


        #region Format Code

        /// <summary>
        /// Format Code
        /// </summary>
        internal async Task SimulateCtrlKCtrlDAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));
            dte.ExecuteCommand("Edit.FormatDocument");
        }

        #endregion

        #region Request OpenAI for Data

        /// <summary>
        /// Request OpenAI for Data
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="ltcid"></param> 
        /// <param name="timeout">Default timeout 5 seconds</param> 
        /// <returns></returns>
        internal static async Task OpenAiAsync(string word, MyShowEventHandler showEvent,
            MyStartEventHandler startEvent, MyEndEventHandler endEvent)
        {
            await ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                var config = MyConfig.Get();
                if (string.IsNullOrEmpty(config.apikey))
                {
                    MessageBox.Show("Please set OpenAI key");

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

                HttpClientHandler handler = new HttpClientHandler(); // Create HttpClientHandler instance
                if (!string.IsNullOrEmpty(config.proxy))
                {
                    handler.Proxy = new WebProxy(config.proxy); // Set proxy server address and port
                }

                using (HttpClient httpClient = new HttpClient(handler))
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, config.apiurl);
                    request.Content = new StringContent(JsonConvert.SerializeObject(par), Encoding.UTF8);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    if (config.serviceName == ServiceEnum.Azure.ToString()) //Microsoft Azure Cloud
                    {
                        request.Content.Headers.Add("api-key", $"{config.apikey}");
                    }
                    else //OpenAI
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", $"{config.apikey}");
                    }

                    using (HttpResponseMessage response =
                           await httpClient.SendAsync(request,
                               HttpCompletionOption.ResponseHeadersRead)) //Send request and get response
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            startEvent.Invoke(); //Start

                            response.EnsureSuccessStatusCode();
                            using (Stream responseStream = await response.Content.ReadAsStreamAsync())
                            using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                            {
                                while (!reader.EndOfStream) //Read response stream line by line
                                {
                                    string line = await reader.ReadLineAsync();
                                    if (!string.IsNullOrEmpty(line) && line.Contains("content"))
                                    {
                                        line = line.Remove(0, 5);
                                        var obj = JsonConvert.DeserializeObject<dynamic>(line);
                                        var temp = obj["choices"][0]["delta"]["content"].ToString();
                                        showEvent.Invoke(temp); //Insert gpt result 
                                        await Task.Delay(1);
                                    }
                                }
                            }

                            endEvent.Invoke(); //End
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

        #region Virtual Methods

        /// <summary>
        /// Custom event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual async void VirHandler(object sender, EventArgs e)
        {
            MessageBox.Show("This functionality is not implemented!");
        }

        /// <summary>
        /// Show message
        /// </summary>
        /// <param name="message"></param>
        internal virtual void VirShowMessage(string message)
        {
            if (this.insertPoint != null)
            {
                this.insertPoint.Insert(message);
            }
        }

        /// <summary>
        /// Start execution
        /// </summary>
        /// <param name="message"></param>
        internal virtual void VirStart()
        {
        }

        /// <summary>
        /// Execution completed
        /// </summary>
        /// <param name="message"></param>
        internal virtual void VirEnd()
        {
        }

        #endregion
    }
}
