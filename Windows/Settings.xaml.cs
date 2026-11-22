using System.Windows;

namespace ColdNightsLogViewer.Windows;

public partial class Settings : Window
{
    public Settings()
    {
        InitializeComponent();
    }

    private void ChkDarkMode_OnClick(object sender, RoutedEventArgs e)
    {
        GlobalSettings.I.DarkMode = ChkAutoRefresh.IsChecked;
    }

    private void ChkAutoRefresh_OnClick(object sender, RoutedEventArgs e)
    {
        GlobalSettings.I.AutoRefresh = ChkAutoRefresh.IsChecked;
    }
}