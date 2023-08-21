using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
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
    /// 重构代码入口
    /// </summary>
    internal class Refactoring : MyBase
    {
        internal override void VirHandler(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var selectedTex = GetSelectedTextAsync().Result;
            if (!string.IsNullOrEmpty(selectedTex))
            {
                var obj = FormRefactoring.Instance;
                obj.SelectedText = selectedTex;
                obj.Show();
                obj.Activate();
            }


            //await ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            //{
            //    // 在主线程执行异步操作完成后的代码逻辑
            //    var selectedTex = await GetSelectedTextAsync();
            //    if (!string.IsNullOrEmpty(selectedTex))
            //    {
            //        await InsertChatGptAsync($"{TypeModel.Translate}{selectedTex}", InsertPointEnum.After);
            //    }
            //});
        }

        //internal override void VirStart()
        //{
        //    ThreadHelper.ThrowIfNotOnUIThread();
        //    this.insertPoint.Insert("\n/*");
        //}

        //internal override void VirEnd()
        //{
        //    ThreadHelper.ThrowIfNotOnUIThread();
        //    this.insertPoint.Insert("*/\r\n");
        //    _ = SimulateCtrlKCtrlDAsync();
        //}
    }
}
