using DesignPatternCodeGenerator.Attributes.NullObject;

namespace Samples.NullObject;

[NullObject]
public interface ILog
{
    public int Id { get; }
    public string Login(string username, string password);
    public void Info(string message);
    public void Warn(string message);
}


class ConsoleLog : ILog
{
    public int Id { get; set; }
    public void Info(string message)
    {
        Console.WriteLine(message);
    }

    public string Login(string username, string password)
    {
        Console.WriteLine($"Login... {username}");
        return password;
    }

    public void Warn(string message)
    {
        Console.WriteLine($"WARNING: {message}");
    }
}

class TestNullLog : ILog
{
    public int Id { get; set; }
    public void Info(string message) { }
    public string Login(string username, string password)
    {
        return default;
    }
    public void Warn(string message) { }
}
