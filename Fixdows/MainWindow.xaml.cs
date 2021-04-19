using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Navigation;

namespace Fixdows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            File.Delete("relinstaller.exe");
        }

        private static void OpenRepairTool(string file)
        {
            var dir = Directory.GetCurrentDirectory(); // Get current directory before actual code, and assign it to a variable ( I can probably optimize this )
            if (dir.Contains("Debug")) // Does the current Directory have "debug" in it? if so, do this.
            {
                Directory.SetCurrentDirectory("../");
                Directory.SetCurrentDirectory("../");
            }
            Console.WriteLine(file);
            Console.WriteLine(dir); // For debugging
            var FileToOpen = new ProcessStartInfo($"{dir}/assets/" + file);
            FileToOpen.Verb = "runas"; // Just to make sure that we launch as administrator
            Process.Start(FileToOpen); // Now we run the integrity check script
        }
        private void AboutRedirButtonGithub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/Odyssey346/Fixdows");
        }

        private void AboutRedirButtonMyEmail_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("mailto:odyssey346@disroot.org");
        }

        private void CleanDiskButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("cleanmgr");
        }

        private void IntegrityFixButton_click(object sender, RoutedEventArgs e)
        {
            OpenRepairTool("integrity.bat");
        }

        private void WupdButton_Click(object sender, RoutedEventArgs e)
        {
            OpenRepairTool("wu.bat");
        }

        private void WSResetButton_Click(object sender, RoutedEventArgs e)
        {
            var ws = new ProcessStartInfo("WSReset.exe");
            ws.Verb = "runas"; // Just to make sure that we launch as administrator
            Process.Start(ws); // Now we run the integrity check script
        }

        private void UpdateCheckButtonClicked(object sender, RoutedEventArgs e)
        {
            UpdateUI updui = new UpdateUI();
            updui.Show(); 
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // for .NET Core you need to add UseShellExecute = true
            // see https://docs.microsoft.com/dotnet/api/system.diagnostics.processstartinfo.useshellexecute#property-value
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

    }
}
