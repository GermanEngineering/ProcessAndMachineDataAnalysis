using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General
{    
    public class LoggToFile
    {
        /// <summary>
        /// loggs passed string to file
        /// </summary>
        /// <param name="sStringToLogg">string that will be logged to file</param>
        /// <param name="sFileName">name of the file (default: "LogFile.txt")</param>
        public static void AppendStringToLogFile(string sStringToLogg, string sFileName = "LogFile.txt")
        {
            string sFilePath = AppDomain.CurrentDomain.BaseDirectory + sFileName;
            System.IO.StreamWriter objFileWriter = new System.IO.StreamWriter(sFilePath, true); // create new file writer object
            objFileWriter.Write("\n" + DateTime.Now.ToString() + " - " + sStringToLogg);   // append string to file
            objFileWriter.Close();  // close file writer
        }
    }
}
