using System.ClientModel;
using FristAgentDemo.PrintHelper;
using OpenAI;
using OpenAI.Chat;
using Serilog;

namespace FristAgentDemo.models;

public class DeepSeek
{
    const string Key = "xxxx";
    const string DeepSeekModel = "deepseek-flash";

    private static readonly ChatClient Client = CreateClient();
    
    private static ChatClient CreateClient()
    {
        var options = new OpenAIClientOptions
        {
            Endpoint = new Uri("https://api.deepseek.com")
        };

        return new ChatClient(
            model: DeepSeekModel,
            credential: new ApiKeyCredential(Key),
            options: options);
    }
    
    
    public static async Task<ChatCompletion> CallModelAsync(IEnumerable<ChatMessage> input, IList<ChatTool> tools)
    {
        try
        {
            Log.Information("模型输入：{@Input}", input);
            var options = new ChatCompletionOptions();
            foreach (var tool in tools)
            {
                options.Tools.Add(tool);
            }
            ChatCompletion completion = await Client.CompleteChatAsync(input,options);
            Log.Information("模型输出：{@Completion}", completion);
            Print.PrintHelper($"[输入 {completion.Usage.InputTokenCount} 个token，输出 {completion.Usage.OutputTokenCount} 个token]");
            return completion;
               
        }
        catch (ClientResultException ex)
        {
            Log.Error($"模型请求失败：{ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Log.Error($"模型调用失败：{ex.Message}");
            throw;
        }
    }
}