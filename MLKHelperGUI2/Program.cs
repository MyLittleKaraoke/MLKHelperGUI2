using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using ICSharpCode.SharpZipLib.Tar;




namespace MLKHelperGUI2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length == 0) { MessageBox.Show("No .MLK, no Muffins!"); }
            else 
            {
                for (int i = 0; i < args.Length; i++)
                {
                    RegistryKey regKey1 = Registry.LocalMachine;
                    regKey1 = regKey1.OpenSubKey(@"SOFTWARE\\DerpyMuffinsFactory");
                    string mlkpath = regKey1.GetValue("MLK Path").ToString();
                    String songs = ( mlkpath + @"songs\");
                    Stream inStream = File.OpenRead(@args[i]);
                    TarArchive mlkArchive = TarArchive.CreateInputTarArchive(inStream);
                    mlkArchive.ExtractContents(@songs);
                    mlkArchive.Close();
                    inStream.Close();
                    MessageBox.Show(args[i] + "Is Installed");
                }
            }
        }
    }
}
