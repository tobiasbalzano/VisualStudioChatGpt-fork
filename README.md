# Visual Studio ChatGPT
- This is an extension that adds ChatGPT functionality directly into Visual Studio.
- You will be able to consult ChatGPT directly through the text editor or through a new specific tool window.
- Visual Studio 2022: [Plugin Address](https://marketplace.visualstudio.com/items?itemName=MogoVsixRightMenu1.VisualStudioChatGpt)
- Repository Address: [Gitee (Main)](https://gitee.com/wangshuyu/visual-studio-chat-gpt)
- Repository Address: [Github (Secondary)](https://github.com/wangshuyu/VisualStudioChatGpt)

## Description
- In versions before 1.0, the plugin may have bugs. Feel free to report any issues.
- I'll try to fix bugs the same day.

## Screenshots
- Choose a method and right-click the text editor, you will see these new ChatGPT commands:
- ![image](https://images001.wangshuyu.top/Images001/2023/0821/151003346.png)
- Animated Effect
- [Animated Effect](https://gitee.com/wangshuyu/visual-studio-chat-gpt/blob/master/VisualStudioChatGpt/Resources/vschatgpt2022.gif)

## Features in Text Editor
- **Code Completion:** Start writing a method, select it, and request completion.
- **Find Bugs:** Find errors in the selected method or code snippet.
- **Fix Bugs:** Modify errors in the selected method or code snippet.
- **Optimize Code:** Optimize the selected code method.
- **Add Comments:** Write explanations for the selected method.
- **Add Method Summary:** Add a C# method summary.
- **Add Unit Tests:** Create unit tests for the selected method.
- **Refactor Code:** Refactor code, experimental, can try using.
- **AI Chat:** Jump directly to the ChatGpt web version.
- **Settings:** Set OpenAI-related configuration information.

## Settings
![image](https://images001.wangshuyu.top/Images001/2023/0531/164701930.png)
- Backend Service: Supports OpenAI and Azure Cloud
- Api Url: Interface address, **this must correspond to the backend service, because OpenAI and Azure Cloud have a slight difference in verification methods**
- Api Key: Verification Key, how to apply [OpenAI](https://beta.openai.com/account/api-keys) [Azure Cloud](https://learn.microsoft.com/azure/cognitive-services/openai/overview)
- Proxy: Proxy address, format: http://ip:port, for example: http://127.0.0.1:10809
- MaxToken: Maximum number of returned tokens, not too large
- Temperature: Heat, model randomness, recommended not to exceed 0.3
- Model: The model, cheaper than Da Vinci, sufficient ability, $0.002/750 words
- TimeOut (seconds): Timeout duration

## Api Interface
### Apply for OpenAI Key
- To use this tool, you must register for the OpenAI API and apply for a developer key.
- You need to create and set up an OpenAI API key.
- OpenAI key application address [https://beta.openai.com/account/api-keys](https://beta.openai.com/account/api-keys)
- OpenAI officially provides a [status page](https://status.openai.com/), although minor faults are not shown much, major outages can be seen in the announcement.
- If you don't have a proxy, you can consider using Alibaba Cloud Function Compute to transparently pass the OpenAI interface address, [reference project](https://github.com/dyc87112/OpenAIProxy)

### Apply for Microsoft Azure Cloud Key
- Microsoft Azure Cloud key application address: [https://learn.microsoft.com/azure/cognitive-services/openai/overview](https://learn.microsoft.com/azure/cognitive-services/openai/overview)
- Pros: At least twice as fast as native OpenAI, no need for a ladder
- Cons: Application is cumbersome, requires a corporate account

### Build Your Own Api Interface
- Fully compatible with OpenAI interface mode
- Same price as OpenAI for small recharge use
- [Api Interface Documentation](https://chat.wangshuyu.top/#/ApiDoc)
- [Chat Tool](https://chat.wangshuyu.top/)

### Note
- You can apply for either OpenAI or Microsoft Azure Cloud.

## How to Avoid OpenAI Account Ban
- Do not use proxies from regions not served by OpenAI
- Using a virtual overseas phone number is more likely to get your account banned
- Linking a credit card can significantly increase account survival rate

## Known Issues
- Unfortunately, the API provided by OpenAI for interacting with ChatGPT has limitations on the size of the questions and the given answers.
- If the question sent is too long (e.g., a method with multiple lines) and/or the generated response is too long, the API may truncate the response or not respond at all.
- For these situations, I suggest you make requests through the tool window in a way that ChatGPT will not refuse to answer, or try modifying the model options to improve the response.

## Disclaimer
- Since this extension depends on the API provided by OpenAI, they may make some changes that could affect the operation of this extension without further notice.
- Since this extension depends on the API provided by OpenAI, the generated response may not be as expected.
- The speed and availability of the response depend directly on the API provided by OpenAI.
- If you find any errors or unexpected behavior, please leave a comment so that I can provide a fix.

## Version History
### v0.3.4 - v0.3.5
- Added code refactoring feature, experimental

### v0.3.2 - v0.3.3
- Adjusted query prompt wording

### v0.3.1
- Adjusted chat URL
- Adjusted query prompt wording

### v0.3.0
- Changed chat entry to self-built chat address, as some users reported not being able to bypass the Great Firewall
- Added settings for self-built API link, same price as OpenAI, $0.002/1000 tokens

### v0.2.7--v0.2.8
- Added invalid key exception prompt verification feature

### v0.2.6
- Continued to fix the issue where the configuration file was lost after system restart, this time it should be completely fixed

### v0.2.5
- Supports non-streaming return results

### v0.2.4
- Fixed an issue where storing the configuration file caused the program to crash
- Fixed the configuration file to support global storage

### v0.2.3
- Adjusted the storage location of the configuration file
- Note: I'm very sorry, the original storage configuration scheme was only effective in the current VS window, not globally. This version has fixed this issue.

### v0.2.2
- Added translation feature
- Optimization: Improved the accuracy of the code completion feature
- Adjusted the default value of the temperature parameter to 0

### v0.2.1
- Changed the unique GUID of the plugin as it was duplicated with other plugins
- Added Microsoft Azure Cloud
- Adjusted the default value of the temperature parameter to 0
- Refactored all method OpenAI preambles and attributive words to make the returned results more in line with expectations
- Removed the AddSummary (add comments) feature, as it was very similar to the Explain (add explanations) feature

### v0.1.3
- Initial version with basic features supported
- Complete code
- Find bugs
- Fix bugs
- Optimize code
- Write explanations
- Add comments
- Add method kiln
- Add unit tests
