using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Communication
{
    public static class IPAddresses
    {
        private static string ExecuteCommandSync(object command)
        {
            string result = string.Empty;
            try
            {
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command);
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;

                Process proc = new Process();
                proc.StartInfo = procStartInfo;
                proc.Start();

                result = proc.StandardOutput.ReadToEnd();
            }
            catch (Exception)
            {
                MessageBox.Show("Getting text with ips from cmd failed!");
            }

            return result;
        }


        public static List<string> GetAddressesOnLAN()
        {
            string text = ExecuteCommandSync("arp -a");
            string expression = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";

            MatchCollection mc = Regex.Matches(text, expression);

            List<string> addresses = new List<string>();
            foreach (Match m in mc)
            {
                addresses.Add(m.Value);
            }

            return addresses;
        }
    }
}
