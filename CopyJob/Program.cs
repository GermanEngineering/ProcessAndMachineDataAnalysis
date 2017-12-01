using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;
using System.Security.Principal;
using General;

namespace generalCopyJob
{
    class Program
    {
        // User Login
        [DllImport("advapi32.DLL", SetLastError = true)]
        public static extern int LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        static void Main(string[] args)
        {
            try
            {
                // load xml document
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "Configuration.xml");   // get settings from xml file
                XmlNodeList itemNodes = xmlDoc.SelectNodes("//configuration/folders/folder"); // list of copyJobs
                int iSyncedDays = Convert.ToInt32(xmlDoc.SelectSingleNode("//configuration/settings/syncedDays").Attributes["syncedDays"].Value);   // count of synchronised days   
                int bConsoleStatus = Convert.ToInt32(xmlDoc.SelectSingleNode("//configuration/settings/consoleStatus").Attributes["consoleStatus"].Value);   // programm status as console output - 1:on / 0:off  
                string sDecryptKey = Convert.ToString(xmlDoc.SelectSingleNode("//configuration/settings/decryptKey").Attributes["decryptKey"].Value);   // decryption Key

                if (bConsoleStatus == 1) { Console.WriteLine(DateTime.Now.ToString() + " CopyJob started "); }

                // loop through all folder nodes from xml file
                foreach (XmlNode itemNode in itemNodes)
                {
                    // get copyJob settings
                    string sNameOfCopyJob = itemNode.Attributes["copyJobName"].Value;
                    string sSourcePath = itemNode.Attributes["sourcePath"].Value;
                    string sDestinationPath = itemNode.Attributes["destinationPath"].Value;
                    string sLoginUser = itemNode.Attributes["loginUser"].Value;
                    string sLoginDomain = itemNode.Attributes["loginDomain"].Value;
                    string sLoginPassword = itemNode.Attributes["loginPassword"].Value;
                    
                    if (bConsoleStatus == 1) { Console.WriteLine(DateTime.Now.ToString() + " Copying " + sNameOfCopyJob + " files"); }
                    int iFileNumber = 0;

                    // login
                    if(sLoginUser != "" && sLoginDomain != "")
                    {
                        IntPtr intPtrLogin = new IntPtr();
                        if (LogonUser(sLoginUser, sLoginDomain, General.Decrypt.DecryptString(sLoginPassword, sDecryptKey), 9, 0, ref intPtrLogin) != 0)
                        {
                            WindowsImpersonationContext wic = new WindowsIdentity(intPtrLogin).Impersonate();
                        }
                        else { throw new Exception("login as user: " + sLoginUser + " at domain: " + sLoginDomain + " failed"); }
                    }

                    // copy files which are created within the syncronisation timeframe
                    foreach (string sSourceFile in Directory.GetFiles(sSourcePath, "*", SearchOption.AllDirectories))
                    {
                        if (bConsoleStatus == 1) { Console.Write("\r" + DateTime.Now.ToString() + " Processing File " + iFileNumber); iFileNumber += 1; }
                        if (File.GetLastWriteTime(sSourceFile) > DateTime.Now.AddDays(-iSyncedDays))
                        {
                            if (!Directory.Exists(Path.GetDirectoryName(sSourceFile.Replace(sSourcePath, sDestinationPath))))
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(sSourceFile.Replace(sSourcePath, sDestinationPath)));   // create destination folders if they dont exist
                            }
                            File.Copy(sSourceFile, sSourceFile.Replace(sSourcePath, sDestinationPath), true);   // copy the file
                        }
                    }

                    // delete empty folders and files which are older than the synchronisation timeframe in destination path
                    if (bConsoleStatus == 1) { Console.WriteLine("\n" + DateTime.Now.ToString() + " Deleting old " + sNameOfCopyJob + " files and empty folders"); }
                    foreach (string sDestinationFile in Directory.GetFiles(sDestinationPath, "*", SearchOption.AllDirectories))
                    {
                        if (File.GetLastWriteTime(sDestinationFile) < DateTime.Now.AddDays(-iSyncedDays)) { File.Delete(sDestinationFile); }
                    }
                    foreach (string sDestinationFolder in Directory.GetDirectories(sDestinationPath, "*", SearchOption.AllDirectories))
                    {
                        if (Directory.GetFiles(sDestinationFolder).Length == 0 && Directory.GetDirectories(sDestinationFolder).Length == 0) { Directory.Delete(sDestinationFolder, false); }
                    }
                }
                if (bConsoleStatus == 1) { Console.WriteLine(DateTime.Now.ToString() + " CopyJob finished"); Console.WriteLine("Press any key to exit"); Console.ReadKey(); }
            }
            catch (Exception ex) { LoggToFile.AppendStringToLogFile("EXCEPTION\nMessage:\n" + ex.Message + "\nStackTrace:\n" + ex.StackTrace); }
        }
    }
}
