using System;
using System.Diagnostics;

namespace VisualStudioChatGpt.Commands
{
    /// <summary>
    /// Ask Anything
    /// </summary>
    internal class AskAnything : MyBase
    {
        internal override void VirHandler(object sender, EventArgs e)
        {
            Process.Start("https://chat.wangshuyu.top/");
        }
    }
}