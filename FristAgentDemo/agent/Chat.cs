using System.Text.Json;
using FristAgentDemo.Memory;
using FristAgentDemo.models;
using FristAgentDemo.Tools;
using OpenAI.Chat;

namespace FristAgentDemo;

public class Chat
{
    private const string SystemPrompt =
        "你是一个专业的英雄联盟教练，熟悉英雄联盟游戏中所有的角色和技能," +
        "只有明确不知道答案，或者问题涉及需要查询的最新数据时，才调用工具。";
    public static async Task<string> CallModel(string question)
    {
        var message = new List<ChatMessage>()
        {
            new SystemChatMessage(SystemPrompt),
        };
        message.AddRange(History.GetHistory());
        List<ChatTool> tools = new List<ChatTool>();
        var response =  await DeepSeek.CallModelAsync(message,tools);
        return  response.Content[0].Text;
    }

    public static async Task<string> CallModelWithTools(string question)
    {
        var message = new List<ChatMessage>()
        {
            new SystemChatMessage(SystemPrompt),
        };
       
        message.AddRange(History.GetHistory());
        List<ChatTool> tools =
        [
            GetWorldChampionTool.CreateFunctionTool()
        ];
        while (true)
        {
            var response =  await DeepSeek.CallModelAsync(message,tools);
            message.Add(new AssistantChatMessage(response));

            if (response.ToolCalls.Count == 0)
            {
                return response.Content[0].Text;
            }

            foreach (var tool in response.ToolCalls)
            {
                string toolReturn = tool.FunctionName switch
                {
                    GetWorldChampionTool.Name =>
                        GetWorldChampionTool.Execute(tool.FunctionArguments.ToString())
                };
                    
                message.Add(new ToolChatMessage(tool.Id,toolReturn));
            }
        }
    }
}