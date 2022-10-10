using System.ComponentModel;
using Newtonsoft.Json;

namespace Beadando.enums;

public enum ERuleType
{
    [Description("Network")]
    NETWORK,
    [Description("Application")]
    APPLICATION
}