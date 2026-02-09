using System.Text.Json.Nodes;

namespace cs_pokedex.Extensions
{
    public static class JsonNodeExtensions
    {
        public static string GetStringOrEmpty(this JsonNode? node, string propertyName) =>
            node?[propertyName]?.GetValue<string>() ?? string.Empty;
        public static int GetIntOrZero(this JsonNode? node, string propertyName) =>
            node?[propertyName]?.GetValue<int>() ?? 0;
	}
}