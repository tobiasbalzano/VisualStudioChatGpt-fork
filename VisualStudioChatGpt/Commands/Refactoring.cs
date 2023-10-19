using Microsoft.VisualStudio.Shell;
using System;

namespace VisualStudioChatGpt.Commands
{
    /// <summary>
    /// Refactoring Entry Point
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
        }
    }
}