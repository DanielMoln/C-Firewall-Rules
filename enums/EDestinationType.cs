using System.ComponentModel;

namespace Beadando.enums;

public enum EDestinationType
{
    [Description("IPAddress")]
    IPADDRESS,
    [Description("subnet")]
    SUBNET,
    [Description("FQDN")]
    FQDN
}