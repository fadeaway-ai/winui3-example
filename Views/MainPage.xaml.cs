using Microsoft.UI.Xaml.Input;
using Microsoft.Web.WebView2.Core;
using System.IO;

namespace WinUIExample.Views
{
    public partial class MainPage : Page
    {
        private static string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "webview_log.txt");

        private static void Log(string message)
        {
            try
            {
                File.AppendAllText(logPath, $"{DateTime.Now:HH:mm:ss} - {message}\n");
            }
            catch { }
        }

        public MainPage()
        {
            Log("MainPage constructor started");
            this.InitializeComponent();
            Log("InitializeComponent done");

            // Wire up events in code
            BrowserView.NavigationCompleted += BrowserView_NavigationCompleted;
            BrowserView.NavigationStarting += (s, e) => Log($"NavigationStarting: {e.Uri}");

            try
            {
                Log($"BrowserView is null: {BrowserView == null}");
                Log("Setting Source to Bing...");
                BrowserView.Source = new Uri("https://www.bing.com");
                Log("Source set successfully");
            }
            catch (Exception ex)
            {
                Log($"ERROR setting source: {ex.GetType().Name}: {ex.Message}");
            }

            Log("MainPage constructor done");
        }

        private void NavigateToUrl()
        {
            var url = AddressBar.Text.Trim();
            Log($"NavigateToUrl called with: {url}");

            if (string.IsNullOrEmpty(url))
                return;

            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                url = "https://" + url;
            }

            try
            {
                Log($"Setting Source to: {url}");
                BrowserView.Source = new Uri(url);
                Log("Source set");
            }
            catch (Exception ex)
            {
                Log($"ERROR: {ex.Message}");
                AddressBar.Text = "ERROR: " + ex.Message;
            }
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            Log("GoButton clicked");
            NavigateToUrl();
        }

        private void AddressBar_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Log("Enter pressed");
                NavigateToUrl();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (BrowserView.CanGoBack)
            {
                BrowserView.GoBack();
            }
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (BrowserView.CanGoForward)
            {
                BrowserView.GoForward();
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            BrowserView.Reload();
        }

        private void BrowserView_NavigationCompleted(WebView2 sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            Log($"NavigationCompleted: IsSuccess={args.IsSuccess}, Status={args.WebErrorStatus}");
            if (args.IsSuccess)
            {
                AddressBar.Text = BrowserView.Source?.ToString() ?? "";
            }
            else
            {
                AddressBar.Text = "Failed: " + args.WebErrorStatus.ToString();
            }
        }

    }
}
