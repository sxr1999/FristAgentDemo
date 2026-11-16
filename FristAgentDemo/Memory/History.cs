using System.Runtime.CompilerServices;
using OpenAI.Chat;

namespace FristAgentDemo.Memory;

public class History
{
    private static readonly List<ChatMessage> HistoryMessage = new  List<ChatMessage>();

    public static void SaveHistory(ChatMessage history)
    {
        HistoryMessage.Add(history);
    }

    public static List<ChatMessage> GetHistory()
    {
        return HistoryMessage;
    }
}