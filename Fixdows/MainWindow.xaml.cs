using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
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
            var main = this;
            File.Delete("relinstaller.exe");
        }

        private void AboutRedirButtonGithub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/Odyssey346/Fixdows");
        }

        private void AboutRedirButtonDiscord_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://discord.gg/HYrafUqq73");
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
            var version = "1.1"; // Version we'll use to compare & to show on the update UI
            string update_url = "https://api.github.com/repos/odyssey346/fixdows/releases/latest"; // Our latest github release. Please don't change this.
            updui.updateui_this.Status = "Current version: " + version;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(update_url);
            request.UserAgent = "Fixdows-Update-Tool"; // I'd prefer to not have to put in the useragent but it returns a 403 if I don't.
            request.Method = "GET"; // We're not POSTing.
            request.Accept = "application/vnd.github.v3+json"; // Just in case
            HttpWebResponse GhReleaseRes = (HttpWebResponse)request.GetResponse();
            updui.updateui_this.StatusLabelUpdate = "Sent request to GitHub to get release data, got this in response: " + GhReleaseRes.StatusCode; // This shows the user if the request went through
            Stream dataStream = GhReleaseRes.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string strResponse = reader.ReadToEnd();
            dynamic data = JObject.Parse(strResponse);
            Console.WriteLine(strResponse);
            Console.WriteLine(data.tag_name);
            updui.UPSTREAMVERSION_LABEL.Content = "Current GitHub version: " + data.tag_name;
            string updatetagname_str = Convert.ToString(data.tag_name);
            Console.WriteLine(updatetagname_str);
            if (version.Equals(updatetagname_str))
            {
                Console.WriteLine("we're up to date");
                updui.updateui_this.StatusLabelUpdate = "You're up to date.";

            }
            else
            {
                Console.WriteLine("we're outdated!");
                var dir = Directory.GetCurrentDirectory();
                updui.updateui_this.StatusLabelUpdate = "Downloading release version " + updatetagname_str;
                string updatereleasezip = "https://github.com/Odyssey346/Fixdows/releases/latest/download/Fixdows-" + data.tag_name +"-installer.exe";
                WebClient webClient = new WebClient();
                Console.WriteLine(updatereleasezip);
                webClient.DownloadFile(updatereleasezip, @dir + "\\relinstaller.exe");
                string zipPath = @dir + "\\relinstaller.exe";
                var installer = new ProcessStartInfo(zipPath);
                installer.Verb = "runas"; // Just to make sure that we launch as administrator
                installer.Arguments = "/DIR=" + dir + "  /LOG /CLOSEAPPLICATIONS"; // Arguments for the installer program
                Process.Start(installer); // Now we run the installer. 
            }   
        }
    }
}
