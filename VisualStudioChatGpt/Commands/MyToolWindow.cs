using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioChatGpt.Commands
{
    [Guid("f85cc75b-f0b3-4f5c-b948-07b54d22d5d5")]

    public class MyToolWindow : ToolWindowPane
    {
        public MyToolWindow() : base(null)
        {
            // 设置窗口标题
            this.Caption = "My Tool Window";

            // 创建WinForms窗口实例
            var winFormsWindow = new FormRefactoring();

            // 将WinForms窗口嵌入到工具窗口中
            this.Content = winFormsWindow;
        }
    }
}
