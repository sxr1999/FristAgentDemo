using System.Text.Json;
using OpenAI.Chat;

namespace FristAgentDemo.Tools;

public static class GetWorldChampionTool
{
    public const string Name = "get_world_champion";

    public static ChatTool CreateFunctionTool()
    {
        return ChatTool.CreateFunctionTool(
            functionName: Name,
            functionDescription: "查询最新的英雄联盟全球总决赛冠军",
            functionParameters: 
            BinaryData.FromString("""
              {
                "type": "object",
                "properties": {
                  "year": {
                    "type": "integer",
                    "description": "年份，例如 2026"
                  }
                },
                "required": ["year"],
                "additionalProperties": false
              }
              """)
        );
    }

    public static string Execute(string arguments)
    {
        using JsonDocument json = JsonDocument.Parse(arguments);

        int year = json.RootElement
            .GetProperty("year")
            .GetInt32();

        return year switch
        {
            2026 => "Bilibili Gaming（BLG）",
            2025 => "T1",
            2024 => "T1",
            2023 => "T1",
            _ => "暂无数据"
        };
    }
}