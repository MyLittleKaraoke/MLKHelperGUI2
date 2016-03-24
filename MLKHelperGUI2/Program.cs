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
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (args.Length == 0) { MessageBox.Show("No .MLK, no Muffins!"); }
                else 
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        string mlkpath = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\DerpyMuffinsFactory", 
                            "MLK Path", Path.Combine(Environment.GetEnvironmentVariable("ProgramFiles(x86)"), "MyLittleKaraoke"));
                        String songs = (Path.Combine(mlkpath, "songs"));
                        Stream inStream = File.OpenRead(@args[i]);
                        TarArchive mlkArchive = TarArchive.CreateInputTarArchive(inStream);
                        mlkArchive.ExtractContents(@songs);
                        mlkArchive.Close();
                        inStream.Close();
                        MessageBox.Show(args[i] + " successfully installed.");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Installation of package failed!" + Environment.NewLine + ex.Message
                    + Environment.NewLine + Environment.NewLine + ex.StackTrace);
            }
            
        }
    }
}
