namespace ColdNightsLogViewer;

public class GlobalSettings
{
    public bool? AutoRefresh { get; set; } = true;
    public bool? DarkMode { get; set; } = false;
    public int AutoRefreshIntervalSeconds { get; set; } = 3;
    
    public static GlobalSettings I { get; } = new();
}