using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace ConsoleUI
{
	public class Data
	{

        public string GetStream(string address)
		{
            var serveralias = address.Split('/')[0];
            var filename = address.Substring(Math.Min(serveralias.Length + 1, address.Length));
            var server = "";
            var nameServers = new CSVDocument(File.ReadAllText("nameserver.csv"));
            foreach (var nameServer in nameServers)
            {
                server = new WebClient().DownloadString(string.Format(nameServer["Pattern"], serveralias)).Trim(' ', '\n', '\r');
                if (server != "") { break; }
            }

            if (!server.StartsWith("http://")) { server = "http://" + server; }
            var resolvedAddress = Path.Combine(server, filename).Replace('\\', '/');

            var siteContent = "";
            foreach (var appendage in new[] { "", ".tk", "/index.tk" })
            {
                try
                {
                    var siteRequest = ((HttpWebResponse)WebRequest.Create((resolvedAddress + appendage).Replace('\\', '/')).GetResponse());
                    siteContent = new StreamReader(siteRequest.GetResponseStream()).ReadToEnd();
                    if (!Regex.IsMatch(siteContent, "Apache.*at.*Port", RegexOptions.Singleline))
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }

            return siteContent;
        }
	}
}
