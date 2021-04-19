using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;


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

        private void EverythingLabel_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://voidtools.com");
        }

        private void WDSLabel_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://windirstat.net");
        }

        private void WUSource_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.tenforums.com/tutorials/24742-reset-windows-update-windows-10-a.html");
        }

        private void IntegrityFixButton_click(object sender, RoutedEventArgs e)
        {
            var dir = Directory.GetCurrentDirectory(); // Get current directory before actual code, and assign it to a variable ( I can probably optimize this )
            if (dir.Contains("Debug")) // Does the current Directory have "debug" in it? if so, do this.
            {
                Directory.SetCurrentDirectory("../");
                Directory.SetCurrentDirectory("../");
            }

            dir = Directory.GetCurrentDirectory(); // assign current directory to variable
            Console.WriteLine(dir); // For debugging
            var integrity = new ProcessStartInfo($"{dir}/assets/integrity.bat");
            integrity.Verb = "runas"; // Just to make sure that we launch as administrator
            Process.Start(integrity); // Now we run the integrity check script
        }

        private void WupdButton_Click(object sender, RoutedEventArgs e)
        {
            var dir = Directory.GetCurrentDirectory(); // Get current directory before actual code, and assign it to a variable ( I can probably optimize this )
            if (dir.Contains("Debug")) // Does the current Directory have "debug" in it? if so, do this.
            {
                Directory.SetCurrentDirectory("../");
                Directory.SetCurrentDirectory("../");
            }

            dir = Directory.GetCurrentDirectory(); // assign current directory to variable
            Console.WriteLine(dir); // For debugging
            var wu = new ProcessStartInfo($"{dir}/assets/wu.bat");
            wu.Verb = "runas"; // Just to make sure that we launch as administrator
            Process.Start(wu); // Now we run the integrity check script
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
    }
}
