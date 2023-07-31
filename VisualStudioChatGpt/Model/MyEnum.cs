using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioChatGpt.Model
{
    internal class TypeModel
    {
        internal static string Message = "请选择代码块后在执行此操作!";

        internal static string Complete = $"补全代码\r\n你是一个经验丰富的.NET高级开发人员,简体中文\r\n只返回代码\r\n\r\n";
        internal static string FindBug = $"查找代码错误\r\n你是一个经验丰富的.NET高级开发人员,简体中文\r\n只返回代码错误说明和代码行号\r\n没有错误,直接返回\"代码没有错误\"\r\n\r\n";
        internal static string RepairBug = $"修复代码错误\r\n你是一个经验丰富的.NET高级开发人员,简体中文\r\n只返回代码\r\n\r\n";
        internal static string Optimize = $"优化压缩代码\r\n你是一个经验丰富的.NET高级开发人员,简体中文\r\n只返回代码\r\n\r\n";
        internal static string Explain = $"解释说明\r\n你是一个经验丰富的.NET高级开发人员,简体中文\r\n只返回代码的简短解释说明\r\n\r\n";
        internal static string AddSummary = $"任务:你是高级软件开发者\r\n角色:高级软件开发者\r\n编程语言:C#\r\n关键词:代码\r\n需求:写Summary说明注释,不需要返回代码块,只返回Summary结构和说明\r\n格式示例:\r\n/// <summary>\r\n///\r\n/// </summary>\r\n/// <param name=“obj”></param>\r\n/// <returns></returns>\r\n\r\n代码";
        internal static string AddTest = $"生成单元测试\r\n你是一个经验丰富的.NET高级开发人员,简体中文\r\n只返回代码\r\n\r\n";
        internal static string Translate = $"中英文互译,只返回译文结果:\r\n";
    }

    /// <summary>
    /// 插入位置
    /// </summary>
    internal enum InsertPointEnum
    {
        /// <summary>
        /// 选择代码前插入
        /// </summary>
        Before = 0,

        /// <summary>
        /// 选择代码后插入
        /// </summary>
        After = 1,

        /// <summary>
        /// 替换选择代码
        /// </summary>
        Replace = 2
    }

    /// <summary>
    /// 服务
    /// </summary>
    internal enum ServiceEnum
    {
        /// <summary>
        /// OpenAI原生服务
        /// </summary>
        OpenAI = 0,

        /// <summary>
        /// 微软Azure云
        /// </summary>
        Azure = 1
    }
}
