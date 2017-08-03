using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
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

                var cHelper = new HelperClass();
                if (!cHelper.InstallLocationInRegistryKeyExists())
                {
                    if (!cHelper.IsAdministrator()) //Needs to run as admin to edit the registry
                    {
                        cHelper.StartProcessAsAdmin(cHelper.SelfPath, args.Length > 0 ? args[0] : "");
                        Environment.Exit(0);
                    }

                    if (MessageBox.Show(".MLK files aren't associated with this program. Set association now?", "MLK song installer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cHelper.SetInstallLocationInRegistryKey();
                        MessageBox.Show("Association sucesfully set!", "MLK song installer");
                    }
                }

                if (args.Length == 0)
                {
                        MessageBox.Show("No .MLK, no Muffins!", "MLK song installer");
                }
                else 
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        string mlkpath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                        String songs = (Path.Combine(mlkpath, "songs"));
                        Stream inStream = File.OpenRead(@args[i]);
                        TarArchive mlkArchive = TarArchive.CreateInputTarArchive(inStream);
                        mlkArchive.ExtractContents(@songs);
                        mlkArchive.Close();
                        inStream.Close();
                        MessageBox.Show(args[i] + " succesfully installed.", "MLK song installer");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Installation of package failed!" + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace);
            }
        }
    }
}
