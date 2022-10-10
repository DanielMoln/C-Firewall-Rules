using Beadando.enums;
using Newtonsoft.Json;

namespace Beadando.models;

public class Rule
{
    [JsonProperty("RuleName")]
    public string ruleName { get; set; }
    
    [JsonProperty("RuleType")]
    public string ruleType { get; set; }
    
    [JsonProperty("Source")]
    public RuleSource source { get; set; }
    
    [JsonProperty("Destination")]
    public RuleDestination ruleDestination { get; set; }
    
    [JsonProperty("Protocol")]
    public string[] protocols { get; set; }
    
    [JsonProperty("Port")]
    public string[] ports { get; set; }

    [JsonProperty("Action")] 
    public string action { get; set; }

    private string printArray(string[] arr)
    {
        string r = "[";
        foreach (var e in arr)
        {
            r += $"{e},";
        }
        r += "]";
        return r;
    }
    
    public override string ToString()
    {
        return "{\n" +
               $"RuleName: {ruleName}\n" +
               $"RuleType: {ruleType}\n" +
               $"Source: {source}\n" +
               $"Destination: {ruleDestination}\n" +
               $"Protocols: {printArray(protocols)}\n" +
               $"Ports: {printArray(ports)}\n" +
               $"Action: {action}\n" +

               "},\n";
    }
}