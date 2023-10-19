﻿using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using Task = System.Threading.Tasks.Task;
using VisualStudioChatGpt.Commands;

namespace VisualStudioChatGpt
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class MyCommand : MyBase
    {
        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private MyCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            AddEvent(0x0001, new Complete().VirHandler, commandService);//完成代码
            AddEvent(0x0002, new FindBug().VirHandler, commandService);//查找bug
            AddEvent(0x0003, new RepairBug().VirHandler, commandService);//修复bug
            AddEvent(0x0004, new Optimize().VirHandler, commandService);//优化代码
            AddEvent(0x0005, new Explain().VirHandler, commandService);//写注释说明 
            AddEvent(0x0007, new AddSummary().VirHandler, commandService);//为方法写Summary注释
            AddEvent(0x0008, new AddTest().VirHandler, commandService);//单元测试 
            AddEvent(0x0099, new AskAnything().VirHandler, commandService);//提问 
            AddEvent(0x0012, new Refactoring().VirHandler, commandService);//重构点
            AddEvent(0x0090, new Translate().VirHandler, commandService);//翻译
            AddEvent(0x0100, new SetUp().VirHandler, commandService);//设置
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static MyCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in Command1's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new MyCommand(package, commandService);
        }
    }
}
