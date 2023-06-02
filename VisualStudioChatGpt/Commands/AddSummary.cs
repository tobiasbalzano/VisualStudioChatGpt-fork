﻿using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualStudioChatGpt.Model;

namespace VisualStudioChatGpt.Commands
{
    /// <summary>
    /// 添加方法摘要Summary
    /// </summary>
    internal class AddSummary : MyBase
    {
        internal override async void VirHandler(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                // 在主线程执行异步操作完成后的代码逻辑
                var selectedTex = await GetSelectedTextAsync();
                if (!string.IsNullOrEmpty(selectedTex))
                {
                    await InsertChatGptAsync($"{TypeModel.AddSummary}{selectedTex}", InsertPointEnum.Before);
                }
            });
        }

        internal override void VirStart()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            this.insertPoint.Insert("\r\n");
        }

        internal override void VirEnd()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            this.insertPoint.Insert("\r\n");
            _ = SimulateCtrlKCtrlDAsync();
        }
    }
}
