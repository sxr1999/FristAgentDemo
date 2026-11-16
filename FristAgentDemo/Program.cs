// See https://aka.ms/new-console-template for more information

using System.Net.Http.Headers;
using FristAgentDemo;
using FristAgentDemo.Memory;
using FristAgentDemo.PrintHelper;
using OpenAI.Chat;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File(
        path: "/Users/shenxinran/RiderProjects/FristAgentDemo/FristAgentDemo/Log/app-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        shared: true,
        outputTemplate:
        "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

Console.WriteLine("请输入内容");
while (true)
{
    var input = Console.ReadLine();
    if (input == "exit")
    {
        break;
    }
    Print.PrintHelper("........开始调用模型.......");
    History.SaveHistory(new UserChatMessage(input));
    var result = await Chat.CallModelWithTools(input);
    History.SaveHistory(new AssistantChatMessage(result));
    Print.PrintHelper($"模型回复：{result}");
}