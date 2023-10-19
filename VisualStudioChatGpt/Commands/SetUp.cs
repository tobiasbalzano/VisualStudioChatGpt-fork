using Microsoft.VisualStudio.Shell;
using System;

namespace VisualStudioChatGpt.Commands
{
    /// <summary>
    /// Settings
    /// </summary>
    internal class SetUp : MyBase
    {
        internal override void VirHandler(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread(); 

            FormSetUp.Instance.Show();
            FormSetUp.Instance.Activate();
        }
    }
}