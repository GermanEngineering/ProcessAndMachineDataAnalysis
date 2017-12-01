using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Visualization
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ProductionKeyFigures();

            /*try
            {                
                while (true)
                {
                    new Thread(Program.ProductionKeyFigures).Start();
                    DateTime now = DateTime.Now;    // get current datetime
                    DateTime next5am = now.AddDays(now.Hour >= 5 ? 1 : 0).AddHours(-now.Hour + 5); // get datetime of next 5 AM
                    int iTimerTime = Convert.ToInt32((next5am - now).TotalMilliseconds);  // calculate difference in milliseconds
                    Thread.Sleep(iTimerTime);
                }
            }
            catch (Exception ex)
            {
                string sFilePath = AppDomain.CurrentDomain.BaseDirectory + "ErrorLogFile.txt";
                System.IO.StreamWriter objFileWriter = new System.IO.StreamWriter(sFilePath, true); // create new file writer object
                objFileWriter.Write("\n" + DateTime.Now.ToString() + " - " + ex.Message);   // append string to file
                objFileWriter.Close();  // close file writer
            }*/
        }

        internal static void ProductionKeyFigures()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmVisualization());
        }
    }
}
