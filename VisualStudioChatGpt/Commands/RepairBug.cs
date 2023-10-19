﻿using Microsoft.VisualStudio.Shell;
using System;
using VisualStudioChatGpt.Model;

namespace VisualStudioChatGpt.Commands
{
    internal class RepairBug : MyBase
    {
        internal override async void VirHandler(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                // Execute code logic after the asynchronous operation is completed on the main thread
                var selectedTex = await GetSelectedTextAsync();
                if (!string.IsNullOrEmpty(selectedTex))
                {
                    await InsertChatGptAsync($"{TypeModel.RepairBug}{selectedTex}", InsertPointEnum.Replace);
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