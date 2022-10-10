using System.Threading.Channels;
using Beadando.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

internal class Program
{
	public static List<Rule> Rules = new List<Rule>(); 
	
    public static async Task Main(string[] args)
    {
	    try
	    {
		    /* D:\\Iskola\\VargaGábor\\JsonFileHandler\\Beadando\\Beadando\\datas\\firewall.json */
		    string path = "";
		    
		    /* reading parameters */
		    if (args.Length == 0)
		    {
			    Console.WriteLine("* Nem adtál meg útvonalat!");
			    Console.WriteLine("** Kérlek adj meg egy fájl elérési útvonalat!");
			    Console.WriteLine("*** Használat: -JsonInput <elérési útvonal>");
			    return;
		    }

		    for (int i = 0; i < args.Length; i++)
		    {
			    if (args[i] == "-JsonInput")
			    {
				    path = args[++i];
			    }
		    }
		    
		    /* load datas from the file */
		    await loadJson(path);

		    /* print statistic datas */
		    printStatistics();
		    
		    /* upload new data */
		    do
		    {
			    Console.Write("* Szeretne felvinni új tűzfal szabályt? (I/N)");
			    string response = Console.ReadLine().ToLower();

			    if (response.Equals("n")) return;

			    if (!response.Equals("i"))
			    {
				    Console.WriteLine("Hibás adatot adtál meg!");
			    }
			    else
			    {
				    Console.Write("* Kérlek adj meg egy szabály nevet: ");
				    string ruleName = Console.ReadLine();
				    Console.WriteLine();
				    
				    Console.Write("* Kérlek add meg a szabály típusát:");
				    string ruleType = Console.ReadLine();
				    Console.WriteLine();

					Console.WriteLine("** Forrás: ");
				    Console.Write("** Kérlek add meg a forrás típúsát:");
				    string sourceType = Console.ReadLine();
				    Console.WriteLine();
				    
				    Console.Write("** Kérlek add meg a az állomások IP-címeit:");
				    Console.WriteLine("** Kilépéshez használd a q betűt!");
				    List<string> destinations = new List<string>();
				    do
				    {
					    string input = Console.ReadLine();
					    if (input.Equals("q"))
					    {
						    break;
					    }
					    else
					    {
						    if (input != null)
						    {
							    int dots = 0;
							    foreach (var letter in input.ToCharArray())
							    {
								    if (letter == '.')
								    {
									    dots++;
								    }
							    }

							    if (dots != 3)
							    {
								    Console.WriteLine("Hibás az ip-címed! (xxx.xxx.xxx.xxx/xx)");
							    }
							    else
							    {
								    if (input.Contains("/"))
								    {
									    destinations.Add(input);
								    }
								    else
								    {
									    Console.WriteLine("Hibás az ip-címed! (xxx.xxx.xxx.xxx/xx)");
								    }
							    }
						    }
					    }
				    } while (true);
				    
					Console.WriteLine("** Célállomás: ");
				    Console.Write("** Kérlek add meg a cél típúsát:");
				    string destinationType = Console.ReadLine();
				    Console.WriteLine();
				    
				    Console.Write("** Kérlek add meg az állomások IP-címeit:");
				    Console.WriteLine("** Kilépéshez használd a q betűt!");
				    List<string> ddestinations = new List<string>();
				    do
				    {
					    string input = Console.ReadLine();
					    if (input.Equals("q"))
					    {
						    break;
					    }
					    else
					    {
						    if (input != null)
						    {
							    int dots = 0;
							    foreach (var letter in input.ToCharArray())
							    {
								    if (letter == '.')
								    {
									    dots++;
								    }
							    }

							    if (dots != 3)
							    {
								    Console.WriteLine("Hibás az ip-címed! (xxx.xxx.xxx.xxx/xx)");
							    }
							    else
							    {
								    if (input.Contains("/"))
								    {
									    ddestinations.Add(input);
								    }
								    else
								    {
									    Console.WriteLine("Hibás az ip-címed! (xxx.xxx.xxx.xxx/xx)");
								    }
							    }
						    }
					    }
				    } while (true);
				    
					Console.WriteLine("* Kérlek add meg a protokollokat: ");
				    Console.WriteLine("** Kilépéshez használd a q betűt!");
				    List<string> protocols = new List<string>();
				    do
				    {
					    string input = Console.ReadLine();
					    if (input.Equals("q"))
					    {
						    break;
					    }
					    else
					    {
						    if (input != null)
						    {
							    int number = 0;
							    if (int.TryParse(input, out number))
							    {
								    Console.WriteLine("A protokoll nem tartalmazhat számokat!");
							    }
							    else
							    {
								    protocols.Add(input.ToUpper());
							    }
						    }
					    }
				    } while (true);
				    
					Console.WriteLine("* Kérlek add meg a portokat: ");
				    Console.WriteLine("** Kilépéshez használd a q betűt!");
				    List<string> ports = new List<string>();
				    do
				    {
					    string input = Console.ReadLine();
					    if (input.Equals("q"))
					    {
						    break;
					    }
					    else
					    {
						    if(input != null) ports.Add(input);
					    }
				    } while (true);
					    
				    Console.Write("* Engedélyezed vagy letiltod az adott szabályt?: (I/N)");
				    response = Console.ReadLine().ToLower();
				    string action = "";
				    if (!response.Equals("i"))
				    {
					    action = "Deny";
				    }

				    action = "Allow";
				     
				    // Add the new rule
				    Rule newRule = new Rule()
				    {
					    ruleName = ruleName,
					    ruleType = ruleType,
					    source = new RuleSource()
					    {
						    sourceType = sourceType,
						    destination = destinations.ToArray()
					    },
					    ruleDestination = new RuleDestination()
					    {
						    destinationType = destinationType,
						    destination = ddestinations.ToArray()
					    },
					    protocols = protocols.ToArray(),
					    ports = ports.ToArray(),
					    action = action
				    };
				    
				    Rules.Add(newRule);
				    Console.WriteLine($"! Új szabály lett hozzáadva ({newRule.ruleName})");
				    Console.WriteLine(newRule);
			    }
		    } while (true);
	    }
	    catch (Exception e)
	    {
		    Console.WriteLine(e.Message);
	    }
    }

    private static async Task loadJson(string url)
    {
	    using (StreamReader r = new StreamReader(url))
	    {
		    string json = r.ReadToEnd();
		    Rules = JsonConvert.DeserializeObject<List<Rule>>(json);
	    }
    }

    private static void printStatistics()
    {
	    int applicationRules = 0;
	    int denies = 0;
	    int allows = 0;

	    foreach (var rule in Rules)
	    {
		    if (rule.ruleType.ToLower() == "application") applicationRules++;
		    if (rule.action.ToLower() == "deny") denies++;
		    if (rule.action.ToLower() == "allow") allows++;
	    }

	    Console.WriteLine("Statisztika:");
	    Console.WriteLine("\t* Application szabályok száma: " + applicationRules);
	    Console.WriteLine("\t* Engedélyezett szabályok száma: " + allows);
	    Console.WriteLine("\t* Tiltott szabályok száma: " + denies + "\n");
    }
}

/*
 *  Rule {
 *		ruleName - string,
 *		ruletype - enum (network, application),
 *		source {
 *			source-type - enum (subnet, ipaddress)
 *			destination - string[]
 *		},
 *		destination {
 *			dest-type - enum (ipaddress, subnet, FQDN),
 *			destination - string[]
 *		},
 *		protocol - enum[] (tcp, udp, icmp),
 *		port - int[]
 *		action - string (rule is enabled or not)
 *  }
 *
 * {
		"RuleName":"Outbound DNS",
		"RuleType":"Network",
		"Source":{
			"Type":"Subnet",
			"Destination":	[
				"192.168.10.0/24"
			]
		},
		"Destination":{
			"Type":"IPAddress",
			"Destination":
			[
				"81.222.142.44",
				"81.222.142.45"
			]
		},
		"Protocol":["UDP"],
		"Port":["53"],
		"Action":"Allow"
	}
 * 
 *
 * 
 */