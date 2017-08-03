using System;
using System.Security.Principal;
using Microsoft.Win32;
using System.Diagnostics;

namespace MLKHelperGUI2
{
    class HelperClass
    {
        public string SelfPath = System.Reflection.Assembly.GetEntryAssembly().Location;
        public bool InstallLocationInRegistryKeyExists()
        {
            try
            {
                return Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Classes\derpymuffinsfactory.mlk.v1\shell\open\command", null, null) != null && Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Classes\.mlk", null, "derpymuffinsfactory.mlk.v1") != null;
            }
            catch (Exception ex)
            {
                return true; //Returns true because if this fails, we don't want to attempt to set any registry keys
            }
        }

        public bool SetInstallLocationInRegistryKey()
        {
            try
            {
                string regkey = SelfPath + " \"%1\"";
                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\derpymuffinsfactory.mlk.v1");
                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\derpymuffinsfactory.mlk.v1\shell");
                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\derpymuffinsfactory.mlk.v1\shell\open");
                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\derpymuffinsfactory.mlk.v1\shell\open\command");
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Classes\derpymuffinsfactory.mlk.v1\shell\open\command", null, regkey);
                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\.mlk");
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Classes\.mlk", null, "derpymuffinsfactory.mlk.v1");
                return true;
            }
            catch (Exception ex)
            {
                //ShowErrorMessageDialog(ex.Message, ex.StackTrace, "HelperClass.SetInstallLocationInRegistryKey(string InstallPath)");
                return false;
            }
        }

        public bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public void StartProcessAsAdmin(string Path, string Args = "")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(Path);
            startInfo.Arguments = Args;
            startInfo.Verb = "runas";
            Process.Start(startInfo);
        }
    }
}
