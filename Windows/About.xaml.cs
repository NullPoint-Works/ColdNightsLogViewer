using System.IO;
using System.Windows;
using ColdNightsLogViewer.Logger;

namespace ColdNightsLogViewer.Windows;

public partial class About : Window
{
    private readonly string _path = AppDomain.CurrentDomain.BaseDirectory;
    
    public About()
    {
        InitializeComponent();
        LogManager.PushInfo("Initializing 'About' window", this);

        string path = Path.Combine(_path, "Assets/about.txt");
        LogManager.PushInfo($"About path={path}", this);
        
        Tb.Text = File.ReadAllText(path);
    }
    
}