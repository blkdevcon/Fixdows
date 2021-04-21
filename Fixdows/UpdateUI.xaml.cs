using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;

namespace Fixdows
{
    /// <summary>
    /// Interaction logic for UpdateUI.xaml
    /// </summary>
    public partial class UpdateUI : Window
    {
        public UpdateUI()
        {
            InitializeComponent();
            var version = "1.1.2"; // Version we'll use to compare & to show on the update UI
            string update_url = "https://api.github.com/repos/odyssey346/fixdows/releases/latest"; // Our latest github release. Please don't change this.
            CurrentVersion_UpdUI.Content = "Current version: " + version;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(update_url);
            request.UserAgent = "Fixdows-Update-Tool"; // I'd prefer to not have to put in the useragent but it returns a 403 if I don't.
            request.Method = "GET"; // We're not POSTing.
            request.Accept = "application/vnd.github.v3+json"; // Just in case
            HttpWebResponse GhReleaseRes = (HttpWebResponse)request.GetResponse();
            UPDATE_STATUS_LABEL.Content = "Sent request to GitHub to get release data, got this in response: " + GhReleaseRes.StatusCode; // This shows the user if the request went through
            Stream dataStream = GhReleaseRes.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string strResponse = reader.ReadToEnd();
            dynamic data = JObject.Parse(strResponse);
            Console.WriteLine(strResponse);
            Console.WriteLine(data.tag_name);
            UPSTREAMVERSION_LABEL.Content = "Current GitHub version: " + data.tag_name;
            string updatetagname_str = Convert.ToString(data.tag_name);
            Console.WriteLine(updatetagname_str);
            // Convert to Version class
            Version currentversion = new Version(version);
            Version upstream_version = new Version(updatetagname_str);

            if (currentversion > upstream_version)
            {
                Console.WriteLine(currentversion);
                Console.WriteLine(upstream_version);
                Console.WriteLine("we're up to date");
                UPDATE_STATUS_LABEL.Content = "You're up to date.";
                return;
            }
            if (version.Equals(updatetagname_str)) 
            {
                Console.WriteLine("we're up to date");
                UPDATE_STATUS_LABEL.Content = "You're up to date.";
            }
            else
            {
                Console.WriteLine("we're outdated");
                var dir = Directory.GetCurrentDirectory();
                UPDATE_STATUS_LABEL.Content = "Downloading release version " + updatetagname_str;
                string updatereleasezip = "https://github.com/Odyssey346/Fixdows/releases/latest/download/Fixdows-" + data.tag_name + "-installer.exe";
                WebClient webClient = new WebClient();
                Console.WriteLine(updatereleasezip);
                webClient.DownloadFile(updatereleasezip, @dir + "\\relinstaller.exe");
                string zipPath = @dir + "\\relinstaller.exe";
                var installer = new ProcessStartInfo(zipPath);
                installer.Verb = "runas"; // Just to make sure that we launch as administrator
                installer.Arguments = "/DIR=" + dir + "  /LOG /CLOSEAPPLICATIONS"; // Arguments for the installer program
                UPDATE_STATUS_LABEL.Content = "Finished! Please run through the installer.";
                Process.Start(installer); // Now we run the installer. 
            }

        }
    }
}
