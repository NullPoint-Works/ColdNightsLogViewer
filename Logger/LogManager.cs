using System.IO;
using Path = System.IO.Path;

namespace ColdNightsLogViewer.Logger;

enum LogType
{
    Info,
    Warning,
    Error
}

public static class LogManager
{
    private static readonly string FilePath = AppDomain.CurrentDomain.BaseDirectory;

    static LogManager()
    {
        string path = Path.Combine(FilePath, "logViewer.log");
        File.WriteAllText(path, string.Empty);
    }

    private static void Log(string msg, LogType type, string prefix = "")
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string p = prefix.Length > 0 ? $"[{prefix}]" : "";
        string path = Path.Combine(FilePath, "logViewer.log");
        File.AppendAllText(path, $"[{type.ToString()}] [{timestamp}] {p} {msg}\n");
        PushToIdeConsole(msg, type);
    }
    
    private static void PushToIdeConsole(string msg, LogType type)
    {
        Console.WriteLine($"[{type.ToString()}] {msg}");
    }

    public static void PushInfo(string msg, object ctx)
    {
        string prefix = ctx.GetType().Name;
        Log(msg, LogType.Info, prefix);
    }

    public static void PushWarning(string msg, object ctx)
    {
        string prefix = ctx.GetType().Name;
        Log(msg, LogType.Warning, prefix);
    }

    public static void PushError(string msg, object ctx)
    {
        string prefix = ctx.GetType().Name;
        Log(msg, LogType.Error, prefix);
    }
    
    //-------------------------------------------//
    
    public static void PushInfo(string msg, string className)
    {
        string prefix = className;
        Log(msg, LogType.Info, prefix);
    }

    public static void PushWarning(string msg, string className)
    {
        string prefix = className;
        Log(msg, LogType.Warning, prefix);
    }

    public static void PushError(string msg, string className)
    {
        string prefix = className;
        Log(msg, LogType.Error, prefix);
    }
}