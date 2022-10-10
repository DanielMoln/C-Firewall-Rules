using Beadando.enums;
using Newtonsoft.Json;

namespace Beadando.models;

public class RuleSource
{
    [JsonProperty("Type")]
    public string sourceType { get; set; }
    [JsonProperty("Destination")]
    public string[] destination { get; set; }

    private string printArray()
    {
        string r = "[";
        foreach (var e in destination)
        {
            r += $"{e},";
        }
        r += "]";
        return r;
    }
    
    public override string ToString()
    {
        return "{\n" +
               $"Type: {sourceType}\n" +
               $"Destination: {printArray()}\n" +
               "}";
    }
}