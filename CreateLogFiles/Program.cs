using System;
using System.Xml;
using General;

namespace CreateLogFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // load configuration from xml document
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "Configuration.xml");
                // get settings from xml file
                XmlNodeList productionLines = xmlDoc.SelectNodes("//configuration/lines/line"); // list of production lines
                int days = Convert.ToInt32(xmlDoc.SelectSingleNode("//configuration/days").Attributes["days"].Value);   // number of days                

                Random randomNumber = new Random(); // initialize random numbers
                SolarCell solarCellObject = new SolarCell();    // create new solarCell object

                // create log file
                foreach (XmlNode productionLine in productionLines)   // loop through all production machines
                {
                    for (int i = 0; i < days; i++) // loop through all days
                    {
                        string FilePath = AppDomain.CurrentDomain.BaseDirectory + "\\logFiles\\" + productionLine.Attributes["line"].Value;
                        System.IO.Directory.CreateDirectory(FilePath); // create path if it doesnt exist
                        string FilePathAndName = FilePath + "\\" + DateTime.Today.AddDays(-i).ToString("yyyyMMdd") + ".txt";                        
                        if (!System.IO.File.Exists(FilePathAndName))
                        {
                            System.IO.StreamWriter objFileWriter = new System.IO.StreamWriter(FilePathAndName, false); // create new file writer object

                            DateTime timestamp = new DateTime(DateTime.Today.Year, DateTime.Now.Month, DateTime.Now.AddDays(-i).Day, 0, 0, 0);

                            while (timestamp < DateTime.Today.AddDays(1 - i))   // loop until next day is reached
                            {
                                solarCellObject.GetRandomSolarCellProperties(solarCellObject, randomNumber);    // create new solar cell with random metadata
                                objFileWriter.Write("\n" + timestamp.ToString() + ";" + solarCellObject.Pmpp + ";" + solarCellObject.Quality);   // append string to file
                                timestamp = timestamp.AddSeconds(solarCellObject.ProductionTime);  // add production time to timestamp
                            }
                            objFileWriter.Close();  // close file writer
                        }
                    }
                }
            }
            catch (Exception ex) { LoggToFile.AppendStringToLogFile("EXCEPTION || Message: " + ex.Message + " || StackTrace: " + ex.StackTrace); }
        }
    }
}
