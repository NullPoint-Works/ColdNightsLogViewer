using System.Windows;

namespace ColdNightsLogViewer.Windows;

public partial class Settings : Window
{
    public event EventHandler? SettingChanged;
    
    public Settings()
    {
        InitializeComponent();
        
        ChkAutoRefresh.IsChecked = GlobalSettings.I.AutoRefresh;
        ChkDarkMode.IsChecked = GlobalSettings.I.DarkMode;
    }

    private void ChkDarkMode_OnClick(object sender, RoutedEventArgs e)
    {
        GlobalSettings.I.DarkMode = ChkAutoRefresh.IsChecked;
        SettingChanged?.Invoke(this, null!);
    }

    private void ChkAutoRefresh_OnClick(object sender, RoutedEventArgs e)
    {
        GlobalSettings.I.AutoRefresh = true;
        SettingChanged?.Invoke(this, null!);
    }

    private void ChkAutoRefresh_OnUnchecked(object sender, RoutedEventArgs e)
    {
        GlobalSettings.I.AutoRefresh = false;
        SettingChanged?.Invoke(this, null!);
    }
}