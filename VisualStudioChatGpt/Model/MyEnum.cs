namespace VisualStudioChatGpt.Model
{
    internal class TypeModel
    {
        internal static string Message = "Please select a code block before performing this operation!";

        internal static string Complete = $"Complete code\r\nYou are an experienced .NET senior developer, \r\nReturn code only\r\n\r\n";
        internal static string FindBug = $"Find code errors\r\nYou are an experienced .NET senior developer, \r\nOnly return error description and line number\r\nIf no errors, directly return \"No errors in code\"\r\n\r\n";
        internal static string RepairBug = $"Fix code errors\r\nYou are an experienced .NET senior developer, \r\nOnly return code\r\n\r\n";
        internal static string Optimize = $"Optimize and compress code\r\nYou are an experienced .NET senior developer, \r\nOnly return code\r\n\r\n";
        internal static string Explain = $"Explain\r\nYou are an experienced .NET senior developer, \r\nOnly return a brief explanation of the code\r\nRequirement: Add a newline at the end of each sentence\r\n\r\n";
        internal static string AddSummary = $"Task: You are a senior software developer\r\nRole: Senior software developer\r\nProgramming language: C#\r\nKeywords: code\r\nRequirement: Write Summary comments, no need to return code block, only return Summary structure and description\r\nExample format:\r\n/// <summary>\r\n///\r\n/// </summary>\r\n/// <param name=“obj”></param>\r\n/// <returns></returns>\r\n\r\nCode";
        internal static string AddTest = $"Generate unit tests\r\nYou are an experienced .NET senior developer, \r\nOnly return code\r\n\r\n";
        internal static string Translate = $"Chinese-English translation, only return translation results:\r\n";
        internal static string Refactoring = $"Refactor the following code, and give reasons for the refactoring, try to minimize the use of operations like character += which consume performance\r\nYou are an experienced .NET senior developer, \r\n";
    }

    /// <summary>
    /// Insert location
    /// </summary>
    internal enum InsertPointEnum
    {
        /// <summary>
        /// Insert before selected code
        /// </summary>
        Before = 0,

        /// <summary>
        /// Insert after selected code
        /// </summary>
        After = 1,

        /// <summary>
        /// Replace selected code
        /// </summary>
        Replace = 2
    }

    /// <summary>
    /// Service
    /// </summary>
    internal enum ServiceEnum
    {
        /// <summary>
        /// Native OpenAI service
        /// </summary>
        OpenAI = 0,

        /// <summary>
        /// Microsoft Azure cloud
        /// </summary>
        Azure = 1
    }
}
