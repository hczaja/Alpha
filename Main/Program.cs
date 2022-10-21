using Main.Utils;
using System.Reflection;

var version = Assembly.GetExecutingAssembly().GetName().Version;
Console.WriteLine($"ver. {version}");

var engine = new GameEngine();
engine.Run();
