using Microsoft.VisualStudio.Shell;
using System;
using VisualStudioChatGpt.Model;

namespace VisualStudioChatGpt.Commands
{
    /// <summary>
    /// Translation Entry
    /// </summary>
    internal class Translate : MyBase
    {
        internal override async void VirHandler(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                // Execute code logic after the main thread asynchronous operation is complete
                var selectedTex = await GetSelectedTextAsync();
                if(!string.IsNullOrEmpty(selectedTex))
                {
                    await InsertChatGptAsync($"{TypeModel.Translate}{selectedTex}", InsertPointEnum.After);
                }
            });
        }

        internal override void VirStart()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            this.insertPoint.Insert("\n/*");
        }

        internal override void VirEnd()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            this.insertPoint.Insert("*/\r\n");
            _ = SimulateCtrlKCtrlDAsync();
        }
    }
}