using Beadando.enums;
using Newtonsoft.Json;

namespace Beadando.models;

public class RuleDestination
{
    [JsonProperty("Type")]
    public string destinationType { get; set; }
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
               $"Type: {destinationType}\n" +
               $"Destination: {printArray()}\n" +
               "}";
    }
}