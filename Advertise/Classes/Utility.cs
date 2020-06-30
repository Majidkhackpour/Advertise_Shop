using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PacketParser.Services;

namespace Shop_Windows.Classes
{
    public class Utility
    {
        private const string login = "@uuid:autocontrol;password$";
        [DllImport("wtsapi32.dll", SetLastError = true)]
        static extern bool WTSDisconnectSession(IntPtr hServer, int sessionId, bool bWait);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr processHandle, uint desiredAccess, out IntPtr tokenHandle);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]

        private static extern bool CloseHandle(IntPtr hObject);

        const int WtsCurrentSession = -1;
        static readonly IntPtr WtsCurrentServerHandle = IntPtr.Zero;

        public static void Logoff()
        {
            if (!WTSDisconnectSession(WtsCurrentServerHandle, WtsCurrentSession, false))
                throw new Win32Exception();
        }

        public static async Task<string> GetNetworkIpAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return null;
            }
        }
        public static async Task<string> GetLocalIpAddress()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("http://ifconfig.me");

                request.UserAgent = "curl";

                string publicIPAddress;

                request.Method = "GET";
                using (var response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        publicIPAddress = reader.ReadToEnd();
                    }
                }

                return publicIPAddress.Replace("\n", "");
            }
            catch (WebException)
            {
                return null;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
                return null;
            }

        }



        public static async Task Wait(double second = 0.5)
        {
            await Task.Delay((int)(second * 1000));
        }

        public static List<string> GetFiles(string path, string filePattern = "*.*")
        {
            return Directory.Exists(path) ? Directory.GetFiles(path, filePattern).ToList() : null;
        }

        public static List<string> GetFolders(string path)
        {
            return Directory.Exists(path) ? Directory.GetDirectories(path).ToList() : null;
        }
        public static string DateSh(DateTime mDateTime)
        {
            var pc = new PersianCalendar();
            return pc.GetYear(mDateTime) + "/" + pc.GetMonth(mDateTime).ToString("00") + "/" + pc.GetDayOfMonth(mDateTime).ToString("00");
        }

        public static void CloseAllChromeWindows()
        {
            try
            {
                var userName = Environment.UserName;
                foreach (var item in Process.GetProcesses())
                    if (item.ProcessName == "chromedriver" || item.ProcessName == "chrome")
                    {
                        var userOfProcess = GetProcessUser(item);
                        if (userOfProcess == userName)
                            item.Kill();
                    }
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private static string GetProcessUser(Process process)
        {
            IntPtr processHandle = IntPtr.Zero;
            try
            {
                OpenProcessToken(process.Handle, 8, out processHandle);
                WindowsIdentity wi = new WindowsIdentity(processHandle);
                string user = wi.Name;
                return user.Contains(@"\") ? user.Substring(user.IndexOf(@"\", StringComparison.Ordinal) + 1) : user;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (processHandle != IntPtr.Zero)
                {
                    CloseHandle(processHandle);
                }
            }
        }

       

        public static IWebDriver RefreshDriver(IWebDriver driver)
        {
            try
            {
                if (driver?.Title == null)
                {
                    CloseAllChromeWindows();
                    driver = new ChromeDriver();
                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(2);
                    driver?.Manage().Window.Maximize();
                }
            }
            catch
            {
                CloseAllChromeWindows();
                driver = new ChromeDriver();
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(2);
                driver?.Manage().Window.Maximize();
            }
            return driver;
        }

        public static string GetHtmlCode(string url)
        {
            var data = "";
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;
                    if (receiveStream != null)
                    {
                        readStream = string.IsNullOrWhiteSpace(response.CharacterSet)
                            ? new StreamReader(receiveStream)
                            : new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    data = readStream?.ReadToEnd();

                    response.Close();
                    readStream?.Close();
                }
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                data = null;
            }

            return data;

        }


        public static async Task<string> FindGateWay()
        {
            try
            {
                var gateWay = NetworkInterface
                    .GetAllNetworkInterfaces()
                    .Where(n => n.OperationalStatus == OperationalStatus.Up)
                    .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
                    .Select(g => g?.Address)
                    .FirstOrDefault(a => a != null);
                return gateWay?.ToString() ?? null;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return null;
            }
        }

        public static async Task<bool> PingHost(string nameOrAddress)
        {
            var pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                var reply = pinger.Send(nameOrAddress);
                pingable = reply?.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                pingable = false;
            }
            finally
            {
                pinger?.Dispose();
            }

            return pingable;
        }

       

        static List<int> numbers = new List<int>();


        private static List<string> lstMessage = new List<string>();
    }
}
