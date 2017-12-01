using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;
using System.Data;
using System.Xml;
using System.Security.Principal;
using System.Runtime.InteropServices;
using General;

namespace DB_Update
{
    class Program
    {
        [DllImport("advapi32.DLL", SetLastError = true)]    // Login - import dll for LogonUser
        public static extern int LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);  // Login - ?

        static void Main(string[] args)
        {
            int iUpdateDaysCount = 0;   // count of past days which should be updated   
            MySqlConnection objMySQLConnection = new MySqlConnection();  // object with MySQL connection info
            string sMySQLQuery = "";    // string for MySQL Query
            MySqlCommand objMySQLCommand = new MySqlCommand(); // object for MySQL command

            try
            {
                // load update settings from xml document
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "Configuration.xml");
                XmlNodeList itemNodes = xmlDoc.SelectNodes("//configuration/machines/machine");
                iUpdateDaysCount = Convert.ToInt32(xmlDoc.SelectSingleNode("//configuration/settings/updateTimeframe").Attributes["updateTimeframe"].Value);   // count of past days which should be updated  
                int bConsoleStatus = Convert.ToInt32(xmlDoc.SelectSingleNode("//configuration/settings/consoleStatus").Attributes["consoleStatus"].Value);   // programm status as console output - 1:on / 0:off  
                string sMySQLConnectionString = xmlDoc.SelectSingleNode("//configuration/settings/mySQLConnectionString").Attributes["mySQLConnectionString"].Value;
                string sMySQLConnPW = xmlDoc.SelectSingleNode("//configuration/settings/mySQLConnPW").Attributes["mySQLConnPW"].Value;
                string sDecryptKey = xmlDoc.SelectSingleNode("//configuration/settings/decryptKey").Attributes["decryptKey"].Value;
                objMySQLConnection = new MySqlConnection(sMySQLConnectionString + General.Decrypt.DecryptString(sMySQLConnPW, sDecryptKey));  // object with MySQL connection info

                if (bConsoleStatus == 1) { Console.WriteLine("DB_Update Progress:"); }  // update progress status in console
                if (bConsoleStatus == 1) { Console.WriteLine("Started reading log files - Step 1/2"); } // update progress status in console
                int iTotalLoops = iUpdateDaysCount * itemNodes.Count;
                int iLoopCounter = 0;
                int iFileNotFoundCount = 0;
                string sDomainAdress = "";

                objMySQLConnection.Open();  // open connection

                // read logfiles and update db
                for (DateTime dtDatum = DateTime.Today.AddDays(-iUpdateDaysCount); dtDatum <= DateTime.Today; dtDatum = dtDatum.AddDays(1))   // loop through last x days
                {
                    string sDay = dtDatum.ToString("yyyyMMdd");  // date of day as string

                    foreach (XmlNode itemNode in itemNodes)
                    {
                        string sToolID = itemNode.Attributes["ToolID"].Value;
                        string sFilePath = itemNode.Attributes["FilePath"].Value + sDay + itemNode.Attributes["FileType"].Value;

                        // Login on sourceFile computer if domain has changed
                        if (sDomainAdress != itemNode.Attributes["DomainAdress"].Value && sDomainAdress != "")
                        {
                            sDomainAdress = itemNode.Attributes["DomainAdress"].Value;
                            IntPtr admin_token = new IntPtr();
                            if (LogonUser("LoginUserName", sDomainAdress, "LoginPassword", 9, 0, ref admin_token) != 0)
                            {
                                WindowsImpersonationContext wic = new WindowsIdentity(admin_token).Impersonate();
                            }
                        }
                        iLoopCounter += 1;
                        if (File.Exists(sFilePath))  // if machine log file exists:
                        {
                            // concatenate import query string
                            sMySQLQuery = "LOAD DATA LOCAL INFILE '" + sFilePath.Replace(@"\", "/") + "'" +    // add filepath to query command
                                            " REPLACE" +    // replace existing db entrys with same primary key
                                            " INTO TABLE process_and_machine_data." + itemNode.Attributes["DBTable"].Value +   // add table name to query
                                            " FIELDS TERMINATED BY ';'" +  // set field seperator
                                            " (@field1, @field2, @field3)" +    // assign input fields to variables
                                            " SET throughput_id = CONCAT('" + dtDatum.ToString("yyyy-MM-dd") + "',MID(@field1,12,8),'" + sToolID + "')" +
                                            ", production_timestamp = STR_TO_DATE(@field1, '%d.%m.%Y %H:%i:%s')" +
                                            ", line = " + sToolID +
                                            ", pmpp = CONVERT(REPLACE(@field2, ',', '.'), DECIMAL(6,5))" +
                                            ", quality = TRIM(@field3)";

                            objMySQLCommand = new MySqlCommand(sMySQLQuery, objMySQLConnection); // object for MySQL command
                            try { int iTest = objMySQLCommand.ExecuteNonQuery(); }  // execute command
                            catch (Exception ex)
                            {
                                LoggToFile.AppendStringToLogFile("EXCEPTION || Message: " + ex.Message + " || FilePath: " + sFilePath + " || StackTrace: " + ex.StackTrace);
                                objMySQLConnection.Close();
                                objMySQLConnection.Open();
                            }

                            if (bConsoleStatus == 1) { Console.Write("\rCompleted: " + 100 * iLoopCounter / iTotalLoops + "%"); }   // update progress status in console
                        }
                        else { LoggToFile.AppendStringToLogFile("\nCouldnt find file: " + sFilePath); iFileNotFoundCount += 1; }
                    }
                }

                if (bConsoleStatus == 1)
                {
                    Console.Write("\n" + iFileNotFoundCount + "/" + iTotalLoops + " Files not found)"); // update progress status in console
                }
                iLoopCounter = 0;
            }
            catch (Exception ex) { LoggToFile.AppendStringToLogFile("EXCEPTION || Message: " + ex.Message + " || StackTrace: " + ex.StackTrace); }
            finally
            {
                if (objMySQLConnection.State != ConnectionState.Closed) { objMySQLConnection.Close(); } // close mysql connection if still open
            }
        }
    }
}