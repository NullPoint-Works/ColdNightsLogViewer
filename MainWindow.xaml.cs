using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using ColdNightsLogViewer.Logger;
using ColdNightsLogViewer.Windows;
using Microsoft.Win32;

namespace ColdNightsLogViewer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private string _currentPath;
    private About? _about;
    private Settings? _settings;

    private DispatcherTimer? _timer;
    
    
    public MainWindow()
    {
        InitializeComponent();
        
        LogManager.PushInfo("Initializing Cold Nights Log Viewer.", this);
        
        _timer = new DispatcherTimer() {Interval = TimeSpan.FromSeconds(GlobalSettings.I.AutoRefreshIntervalSeconds)};
        _timer.Tick += OnTimerTick;

        if (GlobalSettings.I.AutoRefresh == true)
        {
            _timer.Start();
        }
    }

    private void BtnOpen_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = "Log files (*.log)|*.log";

        if (ofd.ShowDialog() == true)
        {
            TxtPath.Text = ofd.FileName;
            _currentPath = ofd.FileName;

            ListLogs.Items.Clear();

            foreach (var line in File.ReadLines(ofd.FileName))
            {
                if (PassesFilter(line) && PassesFilter(line))
                    ListLogs.Items.Add(line);
            }
        }
    }

    private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
    {
        if(e.Key == Key.Enter)
            ReloadLog();
    }

    private void TextSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(TxtSearch.Text.Length == 0)
            ReloadLog();
    }

    private void FilterChanged(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_currentPath))
        {
            LogManager.PushError($"No file selected!", this);
            return;
        }
        ReloadLog();
    }

    private void ReloadLog()
    {
        ListLogs.Items.Clear();
        
        foreach (var line in File.ReadLines(_currentPath))
        {
            if (PassesFilter(line) && PassesSearch(line))
                ListLogs.Items.Add(line);
        }
    }

    private bool PassesFilter(string line)
    {
        if (line.Contains("[Error]") && ChkErr.IsChecked == true)
            return true;
        if (line.Contains("[Warning]") && ChkWarn.IsChecked == true)
            return true;
        if (!line.Contains("[Error]") && !line.Contains("[Warning]") && ChkInfo.IsChecked == true)
            return true;
        if (line.Contains("[Debug]") && ChkDbg.IsChecked == true)
            return true;

        return false;
    }

    private bool PassesSearch(string line)
    {
        if (string.IsNullOrWhiteSpace(TxtSearch.Text))
            return true;

        return line.Contains(TxtSearch.Text, StringComparison.OrdinalIgnoreCase);
    }

    private void BtnRefresh_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(_currentPath))
        {
            LogManager.PushError($"No file selected!", this);
            return;
        }
        ReloadLog();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (_about != null)
            return;
        
        _about = new About
        {
            Owner = this
        };
        _about.Show();
        _about.Closed += ClearAboutWnd;
    }

    private void ClearAboutWnd(object? sender, EventArgs e)
    {
       _about?.Close();
       _about = null;
       LogManager.PushInfo($"about={_about}", this);
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_currentPath)) return;
        ReloadLog();
    }

    private void BtnSettingsClick(object sender, RoutedEventArgs e)
    {
        _settings = new Settings() {Owner = this};
        _settings.Closed += (_, _) =>
        {
            _settings.Close();
            _settings = null; 
        };
        _settings.Show();

        _settings.SettingChanged += (_, _) =>
        {
            if (GlobalSettings.I.AutoRefresh == true)
            {
                _timer?.Start();
            }
            else
            {
                _timer?.Stop();
            }
        };
    }
}