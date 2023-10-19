using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;

namespace VisualStudioChatGpt.Commands
{
    [Guid("f85cc75b-f0b3-4f5c-b948-07b54d22d5d5")]
    public class MyToolWindow : ToolWindowPane
    {
        public MyToolWindow() : base(null)
        {
            // Set the window title
            this.Caption = "My Tool Window";

            // Create the WinForms window instance
            var winFormsWindow = new FormRefactoring();

            // Embed the WinForms window into the tool window
            this.Content = winFormsWindow;
        }
    }
}