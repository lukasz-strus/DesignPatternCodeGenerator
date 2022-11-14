using Samples.Singleton;

var cfg = Configuration.GetInstance();
var cfg2 = Configuration.GetInstance();

cfg.SomeProperty = "property";

if (ReferenceEquals(cfg, cfg2))
{
    Console.WriteLine("Configuration is a singleton");
}

Console.WriteLine($"SomeProperty in cfg = {cfg.SomeProperty}");
Console.WriteLine($"SomeProperty in cfg2 = {cfg2.SomeProperty}");