

using System.IO;

namespace ASHFramework.Mono.Utility
{
    public class MAUtility
    {
        public const string DELIM = "\r\n";

        static public string GetComputerName()
        {
            return System.Environment.MachineName;
        }

        static public string GetIPAddress()
        {
            System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return host.AddressList[i].ToString();
            }
            return string.Empty;
        }

        static public string GetDirectoryName(string strFilename)
        {
            string strDirectory = Path.GetDirectoryName(strFilename);
            if (strDirectory == string.Empty)
                return strDirectory;
            strDirectory.Replace(@"\", "/");

            if (strDirectory[strDirectory.Length - 1] != '/')
                strDirectory += "/";
            return strDirectory;
        }

        static public string GetAbsolutelyPath(string relativelyPath)
        {
            string absolutelyPath = Path.GetFullPath(relativelyPath);
            if (absolutelyPath == string.Empty)
                return absolutelyPath;
            return absolutelyPath.Replace(@"\", "/");
        }
    }
}
