using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Xml;
using MySql.Data.MySqlClient;
using System.IO;
using General;

namespace Visualization
{
    public partial class FrmVisualization : Form
    {
        #region variables

        MySqlConnection objMySQLConnection = new MySqlConnection();  // object with MySQL connection info
        MySqlCommand objMySQLCommand = new MySqlCommand();
        string sMySQLQuery = "";
        List <int> liTimeframes = new List<int> { 7, 1 };  // list of calculated timeframes
        Dictionary<string, string> dSections = new Dictionary<string, string>() {   // dictionary of plant sections
            { "Line A", "'LineA'" }
            , { "Line B", "'LineB'"}
            , { "Line C", "'LineC'"} };
        Dictionary<string, string> dCellTypes = new Dictionary<string, string>() {     // dictionary of cell classes
            { "AQuality", "'A'" }
            , { "BQuality", "'B'" }
            , { "CQuality", "'C'"} };

        #endregion

        #region events

        public FrmVisualization()
        {
            InitializeComponent();
        }

        private void FrmVisualization_Shown(object sender, EventArgs e)
        {
            UpdateForm();
            //CaptureScreenshot();
            //SendMail();
            //this.Close();
            
        }

        /*==================================================================================================================================*/
        /*==================================================================================================================================*/

        #endregion

        #region functions

        /// <summary>
        /// updates tables and diagrams
        /// </summary>
        public void UpdateForm()
        {
            try
            {
                // update headline
                LblProductionKeyFigures.Text = "Production Key Figures of " + DateTime.Today.AddDays(-1).ToString("dd.MM.yyyy");

                // load settings from xml document
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "Configuration.xml");
                string sMySQLConnectionString = xmlDoc.SelectSingleNode("//configuration/settings/mySQLConnectionString").Attributes["mySQLConnectionString"].Value;
                string sMySQLConnPW = xmlDoc.SelectSingleNode("//configuration/settings/mySQLConnPW").Attributes["mySQLConnPW"].Value;
                string sDecryptKey = xmlDoc.SelectSingleNode("//configuration/settings/decryptKey").Attributes["decryptKey"].Value;

                // define and open MySQL connection
                objMySQLConnection = new MySqlConnection(sMySQLConnectionString + General.Decrypt.DecryptString(sMySQLConnPW, sDecryptKey));
                objMySQLConnection.Open();

                // update charts and table
                CellCountChart();
                PowerDistributionChart();
                RejectsChart();
                KeyfiguresTable();

                // refresh form
                this.Refresh();
            }
            catch (Exception ex) { General.LoggToFile.AppendStringToLogFile("EXCEPTION || Message: " + ex.Message + " || StackTrace: " + ex.StackTrace); }
            finally { if (objMySQLConnection.State != ConnectionState.Closed) { objMySQLConnection.Close(); } }
        }

        /*==================================================================================================================================*/
        /*==================================================================================================================================*/

        /// <summary>
        /// cell count chart - get data from mysql, copy to datatable and populate chart
        /// </summary>
        public void CellCountChart()
        {
            try
            {
                // create Datatable and add columns
                DataTable dtCellCount = new DataTable();
                DataRow drRow;
                foreach (KeyValuePair<string, string> dEntry in dSections)
                {
                    dtCellCount.Columns.Add(new DataColumn(dEntry.Key, System.Type.GetType("System.String")));
                }

                // get data and add rows to datatable
                drRow = dtCellCount.NewRow();
                foreach (KeyValuePair<string, string> dEntry in dSections)
                {
                    sMySQLQuery = "SELECT COUNT(throughput_id)" +
                                    " FROM process_and_machine_data.throughput" +
                                    " INNER JOIN process_and_machine_data.norm_lines ON (process_and_machine_data.throughput.line = process_and_machine_data.norm_lines.line_id)" +
                                    " WHERE production_timestamp >='" + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                    " AND production_timestamp <'" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                    " AND process_and_machine_data.norm_lines.line_name IN (" + dEntry.Value + ")";
                    objMySQLCommand = new MySqlCommand(sMySQLQuery, objMySQLConnection);
                    drRow[dEntry.Key] = Convert.ToInt32(objMySQLCommand.ExecuteScalar());
                }
                dtCellCount.Rows.Add(drRow);

                // add each datatable column as series to chart
                ChartCellCount.Series.Clear();
                ChartCellCount.Series.Add("Throughput");
                foreach (DataColumn dcColumn in dtCellCount.Columns)
                {
                    ChartCellCount.Series["Throughput"].Points.AddXY(dcColumn.ColumnName, dtCellCount.Rows[0][dcColumn].ToString());
                }
            }
            catch (Exception ex) { General.LoggToFile.AppendStringToLogFile("EXCEPTION || Message: " + ex.Message + " || StackTrace: " + ex.StackTrace); }
        }

        /*==================================================================================================================================*/
        /*==================================================================================================================================*/

        /// <summary>
        /// power distribution - get data from mysql, copy to datatable and populate chart
        /// </summary>
        public void PowerDistributionChart()
        {
            try
            {
                #region alte Version
                /*
                // create Datatable and add columns
                DataTable dtPowerDistribution = new DataTable();
                DataRow drRow;
                foreach (KeyValuePair<string, string> dEntry in dSections)
                {
                    dtPowerDistribution.Columns.Add(new DataColumn(dEntry.Key, System.Type.GetType("System.String")));
                }
                
                // get scale information
                int iScaleIntervallSteps = 25;
                sMySQLQuery = "SELECT IFNULL(MIN(pmpp),0)" +
                            " FROM productiondata.material_cell" +
                            " WHERE timestamp_so >='" + DateTime.Today.AddDays(-liTimeframes.Min()).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " AND timestamp_so <'" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " AND class REGEXP " + dCellTypes["Gutzellen"];
                objMySQLCommand = new MySqlCommand(sMySQLQuery, objMySQLConnection);                
                double dMin = Convert.ToDouble(objMySQLCommand.ExecuteScalar());
                sMySQLQuery = "SELECT IFNULL(MAX(pmpp),1)" +
                            " FROM productiondata.material_cell" +
                            " WHERE timestamp_so >='" + DateTime.Today.AddDays(-liTimeframes.Min()).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " AND timestamp_so <'" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " AND class REGEXP " + dCellTypes["Gutzellen"];
                objMySQLCommand = new MySqlCommand(sMySQLQuery, objMySQLConnection);
                double dMax = Convert.ToDouble(objMySQLCommand.ExecuteScalar());
                double dScaleStepSize = (dMax - dMin) / iScaleIntervallSteps;

                // get data and add rows to datatable
                for (double dStep = dMin; dStep <= dMax; dStep = dStep + dScaleStepSize)
                {
                    drRow = dtPowerDistribution.NewRow();
                    foreach (KeyValuePair<string, string> dEntry in dSections)
                    {
                        sMySQLQuery = "SELECT IFNULL(COUNT(cell_ident),0)" +
                                        " FROM productiondata.material_cell" +
                                        " INNER JOIN productiondata.tool_def ON (productiondata.material_cell.tool_ident = productiondata.tool_def.ID)" +
                                        " WHERE timestamp_so >='" + DateTime.Today.AddDays(-liTimeframes.Min()).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                        " AND timestamp_so <'" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                        " AND productiondata.tool_def.NAME IN (" + dEntry.Value + ")" +
                                        " AND class REGEXP " + dCellTypes["Gutzellen"];
                        objMySQLCommand = new MySqlCommand(sMySQLQuery, objMySQLConnection);
                        int iAllCellsOfType = Convert.ToInt32(objMySQLCommand.ExecuteScalar());
                        sMySQLQuery = "SELECT IFNULL(COUNT(cell_ident),0)" +
                                        " FROM productiondata.material_cell" +
                                        " INNER JOIN productiondata.tool_def ON (productiondata.material_cell.tool_ident = productiondata.tool_def.ID)" +
                                        " WHERE timestamp_so >='" + DateTime.Today.AddDays(-liTimeframes.Min()).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                        " AND timestamp_so <'" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                        " AND productiondata.tool_def.NAME IN (" + dEntry.Value + ")" +
                                        " AND class REGEXP " + dCellTypes["Gutzellen"] +
                                        " AND pmpp > REPLACE('" + dStep + "', ',', '.')" +
                                        " AND pmpp <= REPLACE('" + (dStep + dScaleStepSize) + "', ',', '.')";
                        objMySQLCommand = new MySqlCommand(sMySQLQuery, objMySQLConnection);
                        int iCellsInPRange = Convert.ToInt32(objMySQLCommand.ExecuteScalar());
                        drRow[dEntry.Key] = 100 * iCellsInPRange / (1 + iAllCellsOfType);
                    }
                    dtPowerDistribution.Rows.Add(drRow);
                }

                // add each datatable column as series to chart
                chartPowerDistribution.Series.Clear();                
                foreach (DataColumn dcColumn in dtPowerDistribution.Columns)
                {
                    chartPowerDistribution.Series.Add(dcColumn.ColumnName);
                    chartPowerDistribution.Series[dcColumn.ColumnName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    for (int i = 0; i < iScaleIntervallSteps; i++)
                    {
                        chartPowerDistribution.Series[dcColumn.ColumnName].Points.AddXY(Math.Round(dMin + i * dScaleStepSize,2) , dtPowerDistribution.Rows[i][dcColumn].ToString());
                    }
                }
                */
                #endregion

                ChartPowerDistribution.Series.Clear();

                foreach (KeyValuePair<string, string> dEntry in dSections)
                {
                    sMySQLQuery = "SELECT pmpp, IFNULL(COUNT(throughput_id) * 100 / (" +
                                        " SELECT IFNULL(COUNT(throughput_id),0)" +
                                        " FROM process_and_machine_data.throughput" +
                                        " INNER JOIN process_and_machine_data.norm_lines ON (process_and_machine_data.throughput.line = process_and_machine_data.norm_lines.line_id)" +
                                        " WHERE production_timestamp >='" + DateTime.Today.AddDays(-liTimeframes.Min()).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                        " AND production_timestamp <'" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                        " AND process_and_machine_data.norm_lines.line_name IN (" + dEntry.Value + ")" +
                                        " AND quality REGEXP " + dCellTypes["AQuality"] +
                                    "),0)" +
                                    " FROM process_and_machine_data.throughput" +
                                    " INNER JOIN process_and_machine_data.norm_lines ON (process_and_machine_data.throughput.line = process_and_machine_data.norm_lines.line_id)" +
                                    " WHERE production_timestamp >='" + DateTime.Today.AddDays(-liTimeframes.Min()).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                    " AND production_timestamp <'" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                    " AND process_and_machine_data.norm_lines.line_name IN (" + dEntry.Value + ")" +
                                    " AND quality REGEXP " + dCellTypes["AQuality"] +
                                    " GROUP BY ROUND(pmpp,2)";
                    objMySQLCommand = new MySqlCommand(sMySQLQuery, objMySQLConnection);
                    MySqlDataAdapter daMySQLDataAdapter = new MySqlDataAdapter(objMySQLCommand);
                    DataTable dtExportData = new DataTable();
                    daMySQLDataAdapter.Fill(dtExportData);

                    ChartPowerDistribution.Series.Add(dEntry.Key);
                    ChartPowerDistribution.Series[dEntry.Key].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    for (int i = 0; i < dtExportData.Rows.Count; i++)
                    {
                        ChartPowerDistribution.Series[dEntry.Key].Points.AddXY(dtExportData.Rows[i][0], dtExportData.Rows[i][1]);
                    }
                }
            }
            catch (Exception ex) { General.LoggToFile.AppendStringToLogFile("EXCEPTION || Message: " + ex.Message + " || StackTrace: " + ex.StackTrace); }
        }

        /*==================================================================================================================================*/
        /*==================================================================================================================================*/

        /// <summary>
        /// rejects chart - get data from mysql, copy to datatable and populate chart
        /// </summary>
        public void RejectsChart()
        {
            try
            {
                // create Datatable and add columns
                DataTable dtRejections = new DataTable();
                DataRow drRow;
                foreach (KeyValuePair<string, string> kvpCellType in dCellTypes.Where(kvp => kvp.Key != "AQuality"))
                {
                    dtRejections.Columns.Add(new DataColumn(kvpCellType.Key, System.Type.GetType("System.String")));                  
                }

                // get data and add rows to datatable                
                foreach (KeyValuePair<string, string> kvpSection in dSections) 
                {
                    drRow = dtRejections.NewRow();
                    sMySQLQuery = "SELECT COUNT(throughput_id)" +
                                    " FROM process_and_machine_data.throughput" +
                                    " INNER JOIN process_and_machine_data.norm_lines ON (process_and_machine_data.throughput.line = process_and_machine_data.norm_lines.line_id)" +
                                    " WHERE production_timestamp >='" + DateTime.Today.AddDays(-liTimeframes.Min()).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                    " AND production_timestamp <'" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                    " AND process_and_machine_data.norm_lines.line_name IN (" + kvpSection.Value + ")";
                    objMySQLCommand = new MySqlCommand(sMySQLQuery, objMySQLConnection);
                    double dSectionCellCount = Convert.ToDouble(objMySQLCommand.ExecuteScalar());
                    foreach (KeyValuePair<string, string> kvpCellType in dCellTypes.Where(kvp => kvp.Key != "AQuality"))
                    {
                        sMySQLQuery = "SELECT COUNT(throughput_id)" +
                                        " FROM process_and_machine_data.throughput" +
                                        " INNER JOIN process_and_machine_data.norm_lines ON (process_and_machine_data.throughput.line = process_and_machine_data.norm_lines.line_id)" +
                                        " WHERE production_timestamp >='" + DateTime.Today.AddDays(-liTimeframes.Min()).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                        " AND production_timestamp <'" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                        " AND process_and_machine_data.norm_lines.line_name IN (" + kvpSection.Value + ")" +
                                        " AND quality REGEXP " + kvpCellType.Value;
                        objMySQLCommand = new MySqlCommand(sMySQLQuery, objMySQLConnection);
                        if(dSectionCellCount != 0)
                        {
                            drRow[kvpCellType.Key] = Math.Round(Convert.ToDouble(objMySQLCommand.ExecuteScalar()) * 100.0 / dSectionCellCount, 2);
                        }
                        else { drRow[kvpCellType.Key] = 0.0; }                        
                    }
                    dtRejections.Rows.Add(drRow);
                }

                // add each datatable column as series to chart
                ChartQuality.Series.Clear();
                foreach (DataColumn dcColumn in dtRejections.Columns)
                {
                    ChartQuality.Series.Add(dcColumn.ColumnName);
                    int i = 0;
                    foreach(KeyValuePair<string,string> kvpSection in dSections)
                    {
                        ChartQuality.Series[dcColumn.ColumnName].Points.AddXY(kvpSection.Key, Convert.ToDouble(dtRejections.Rows[i][dcColumn]));
                        i++;
                    }
                }
            }
            catch (Exception ex) { General.LoggToFile.AppendStringToLogFile("EXCEPTION || Message: " + ex.Message + " || StackTrace: " + ex.StackTrace); }
        }

        /*==================================================================================================================================*/
        /*==================================================================================================================================*/

        /// <summary>
        /// keyfigures table - get data from mysql, copy to datatable and populate gridview
        /// </summary>
        public void KeyfiguresTable()
        {
            try
            {
                // create datatable
                DataTable dtKeyFigures = new DataTable("KeyFigures");
                List<string> liKeyfigureNames = new List<string> {};
                foreach (KeyValuePair<string, string> dEntry in dSections)
                {
                    foreach (int iTimeframe in liTimeframes)
                    {
                        dtKeyFigures.Columns.Add(new DataColumn(Convert.ToString(iTimeframe) + "d" + Convert.ToString((char)0xD8) + " " + dEntry.Key, System.Type.GetType("System.String")));
                    }
                }

                // add key figure rows
                AddNewRow("Throughput [#]", liKeyfigureNames, "COUNT(throughput_id)", "", dtKeyFigures, 0);
                AddNewRow("pmpp m [W]", liKeyfigureNames, "IFNULL(AVG(pmpp),0)", "", dtKeyFigures, 2);
                AddNewRow("pmpp std [W]", liKeyfigureNames, "IFNULL(STD(pmpp),0)", "", dtKeyFigures, 2);
                AddNewRow("pmpp min [W]", liKeyfigureNames, "IFNULL(MIN(pmpp),0)", "", dtKeyFigures, 2);
                AddNewRow("pmpp max [W]", liKeyfigureNames, "IFNULL(MAX(pmpp),0)", "", dtKeyFigures, 2);
                AddNewRow("A Quality [%]", liKeyfigureNames, "COUNT(throughput_id)", " AND quality REGEXP " + dCellTypes["AQuality"], dtKeyFigures, 0);
                AddNewRow("B Quality [%]", liKeyfigureNames, "COUNT(throughput_id)", " AND quality REGEXP " + dCellTypes["BQuality"], dtKeyFigures, 0);
                AddNewRow("C Quality [%]", liKeyfigureNames, "COUNT(throughput_id)", " AND quality REGEXP " + dCellTypes["CQuality"], dtKeyFigures, 0);

                // devide count by numer of days and calculate bad cell values as percent
                foreach (KeyValuePair<string, string> dEntry in dSections)
                {
                    foreach (int iTimeframe in liTimeframes)
                    {
                        dtKeyFigures.Rows[0].SetField(Convert.ToString(iTimeframe) + "d" + Convert.ToString((char)0xD8) + " " + dEntry.Key, Math.Round(Convert.ToDouble(dtKeyFigures.Rows[0].Field<string>(Convert.ToString(iTimeframe) + "d" + Convert.ToString((char)0xD8) + " " + dEntry.Key)) / iTimeframe , 0));
                        dtKeyFigures.Rows[5].SetField(Convert.ToString(iTimeframe) + "d" + Convert.ToString((char)0xD8) + " " + dEntry.Key, Math.Round(Convert.ToDouble(dtKeyFigures.Rows[5].Field<string>(Convert.ToString(iTimeframe) + "d" + Convert.ToString((char)0xD8) + " " + dEntry.Key)) / iTimeframe * 100 / Convert.ToDouble(dtKeyFigures.Rows[0].Field<string>(Convert.ToString(iTimeframe) + "d" + Convert.ToString((char)0xD8) + " " + dEntry.Key)), 1));
                        dtKeyFigures.Rows[6].SetField(Convert.ToString(iTimeframe) + "d" + Convert.ToString((char)0xD8) + " " + dEntry.Key, Math.Round(Convert.ToDouble(dtKeyFigures.Rows[6].Field<string>(Convert.ToString(iTimeframe) + "d" + Convert.ToString((char)0xD8) + " " + dEntry.Key)) / iTimeframe * 100 / Convert.ToDouble(dtKeyFigures.Rows[0].Field<string>(Convert.ToString(iTimeframe) + "d" + Convert.ToString((char)0xD8) + " " + dEntry.Key)), 1));
                        dtKeyFigures.Rows[7].SetField(Convert.ToString(iTimeframe) + "d" + Convert.ToString((char)0xD8) + " " + dEntry.Key, Math.Round(Convert.ToDouble(dtKeyFigures.Rows[7].Field<string>(Convert.ToString(iTimeframe) + "d" + Convert.ToString((char)0xD8) + " " + dEntry.Key)) / iTimeframe * 100 / Convert.ToDouble(dtKeyFigures.Rows[0].Field<string>(Convert.ToString(iTimeframe) + "d" + Convert.ToString((char)0xD8) + " " + dEntry.Key)), 1));
                    }
                }

                // add datatable to gridview
                DgvCellCount.DataSource = dtKeyFigures;

                // add row headers
                int iRowCount = 0;
                foreach(string sRowName in liKeyfigureNames)
                {
                    DgvCellCount.Rows[iRowCount].HeaderCell.Value = sRowName;
                    iRowCount++;
                }

                // activate gridview auto resize
                DgvCellCount.AutoSize = true;
            }
            catch (Exception ex) { General.LoggToFile.AppendStringToLogFile("EXCEPTION || Message: " + ex.Message + " || StackTrace: " + ex.StackTrace); }
        }

        /*==================================================================================================================================*/
        /*==================================================================================================================================*/

        /// <summary>
        /// add new row to keyfigures datatable
        /// </summary>
        /// <param name="sRowName">name of the row</param>
        /// <param name="sDataToSelect">mysql data to select</param>
        /// <param name="dt">datatable the row will be added to</param>
        /// <param name="iDecimals">number of displayed decimals</param>
        private void AddNewRow(string sRowName, List<string> liKeyfigureNames, string sDataToSelect, string sFilter, DataTable dt, int iDecimals)
        {
            try
            {
                DataRow drRow = dt.NewRow();

                liKeyfigureNames.Add(sRowName);

                foreach (KeyValuePair<string, string> dEntry in dSections)
                {
                    foreach (int iTimeframe in liTimeframes)
                    {
                        int iTimeframeShift = 0;    // shift the queryed comparison timeframe by the days of the viewed timeframe so the viewed timeframe isn't considered in the average
                        if (iTimeframe == liTimeframes.Max()) { iTimeframeShift = liTimeframes.Min(); }
                        sMySQLQuery = "SELECT " + sDataToSelect +
                                        " FROM process_and_machine_data.throughput" +
                                        " INNER JOIN process_and_machine_data.norm_lines ON (process_and_machine_data.throughput.line = process_and_machine_data.norm_lines.line_id)" +
                                        " WHERE production_timestamp >='" + DateTime.Today.AddDays(-iTimeframe - iTimeframeShift).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                        " AND production_timestamp <'" + DateTime.Today.AddDays(-iTimeframeShift).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                        " AND process_and_machine_data.norm_lines.line_name IN (" + dEntry.Value + ")" + sFilter;
                        objMySQLCommand = new MySqlCommand(sMySQLQuery, objMySQLConnection);
                        drRow[Convert.ToString(iTimeframe) + "d" + Convert.ToString((char)0xD8) + " " + dEntry.Key] = Math.Round(Convert.ToDouble(objMySQLCommand.ExecuteScalar()), iDecimals);                                                
                    }
                }
                dt.Rows.Add(drRow);
            }
            catch (Exception ex) { General.LoggToFile.AppendStringToLogFile("EXCEPTION || Message: " + ex.Message + " || StackTrace: " + ex.StackTrace);}
        }

        /*==================================================================================================================================*/
        /*==================================================================================================================================*/

        /// <summary>
        /// capture and save screenshot
        /// </summary>
        public void CaptureScreenshot()
        {
            try
            {
                //Bitmap bmpScreenshot = new Bitmap(ActiveForm.Size.Width, ActiveForm.Size.Height);
                Bitmap bmpScreenshot = new Bitmap(1280, 720);
                Graphics gScreenshot = Graphics.FromImage(bmpScreenshot);
                //gScreenshot.CopyFromScreen(ActiveForm.Location.X, ActiveForm.Location.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                this.TopMost = true;
                gScreenshot.CopyFromScreen(0, 0, 0, 0,new Size(1280, 720));
                if (!File.Exists(Environment.CurrentDirectory + @"\Produktionskennzahlen" + DateTime.Today.AddDays(-1).ToString("yyyyMMdd") + ".png"))
                {
                    bmpScreenshot.Save(Environment.CurrentDirectory + @"\Produktionskennzahlen" + DateTime.Today.AddDays(-1).ToString("yyyyMMdd") + ".png");
                }                
            }
            catch (Exception ex) { General.LoggToFile.AppendStringToLogFile("EXCEPTION || Message: " + ex.Message + " || StackTrace: " + ex.StackTrace); }
        }

        /*==================================================================================================================================*/
        /*==================================================================================================================================*/

        /// <summary>
        /// send Mail
        /// </summary>
        public void SendMail()
        {
            try
            {
                // load settings from xml document
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "Configuration.xml");
                string sSMTPHost = xmlDoc.SelectSingleNode("//configuration/settings/smtp").Attributes["host"].Value;
                int iSMTPPort = Convert.ToInt32(xmlDoc.SelectSingleNode("//configuration/settings/smtp").Attributes["port"].Value);
                string sSMTPUser = xmlDoc.SelectSingleNode("//configuration/settings/smtp").Attributes["credentialsUser"].Value;
                string sSMTPPassword = xmlDoc.SelectSingleNode("//configuration/settings/smtp").Attributes["credentialsPassword"].Value;
                string sSMTPKey = xmlDoc.SelectSingleNode("//configuration/settings/smtp").Attributes["decryptKey"].Value;
                string sSender = xmlDoc.SelectSingleNode("//configuration/settings/mail").Attributes["sender"].Value;
                string sReciever = xmlDoc.SelectSingleNode("//configuration/settings/mail").Attributes["reciever"].Value;
                string sSubject = xmlDoc.SelectSingleNode("//configuration/settings/mail").Attributes["subject"].Value + " " + DateTime.Today.AddDays(-1).ToString("dd.MM.yyyy");
                string sBody = xmlDoc.SelectSingleNode("//configuration/settings/mail").Attributes["body"].Value;

                // initialize SMTP Client object
                SmtpClient scSmtpClient = new SmtpClient();
                scSmtpClient.Host = sSMTPHost;
                scSmtpClient.Port = iSMTPPort;
                scSmtpClient.Credentials = new System.Net.NetworkCredential(sSMTPUser, General.Decrypt.DecryptString(sSMTPPassword, sSMTPKey));

                // initialize MailMessage object
                MailMessage mmMail = new MailMessage();
                mmMail.From = new MailAddress(sSender);
                mmMail.To.Add(sReciever);
                mmMail.Subject = sSubject;
                mmMail.IsBodyHtml = true;
                mmMail.Body = sBody;

                // attach screenshot to mail
                mmMail.Attachments.Add(new Attachment(Environment.CurrentDirectory + @"\ProductionKeyFigures" + DateTime.Today.AddDays(-1).ToString("yyyyMMdd") + ".png"));

                // send Mail
                scSmtpClient.Send(mmMail);                
            }
            catch (Exception ex) { General.LoggToFile.AppendStringToLogFile("EXCEPTION || Message: " + ex.Message + " || StackTrace: " + ex.StackTrace); }
        }

        /*==================================================================================================================================*/
        /*==================================================================================================================================*/

        #endregion
    }
}