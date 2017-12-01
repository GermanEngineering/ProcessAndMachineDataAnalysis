namespace Visualization
{
    partial class FrmVisualization
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVisualization));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LblProductionKeyFigures = new System.Windows.Forms.Label();
            this.ChartCellCount = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ChartQuality = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ChartPowerDistribution = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.DgvCellCount = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartCellCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartPowerDistribution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCellCount)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Visualization.Properties.Resources.GEGreenOnWhiteLogo;
            this.pictureBox1.Location = new System.Drawing.Point(1121, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(147, 114);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // LblProductionKeyFigures
            // 
            this.LblProductionKeyFigures.AutoSize = true;
            this.LblProductionKeyFigures.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblProductionKeyFigures.Location = new System.Drawing.Point(12, 9);
            this.LblProductionKeyFigures.Name = "LblProductionKeyFigures";
            this.LblProductionKeyFigures.Size = new System.Drawing.Size(313, 25);
            this.LblProductionKeyFigures.TabIndex = 3;
            this.LblProductionKeyFigures.Text = "Production Key Figures of ...";
            // 
            // ChartCellCount
            // 
            this.ChartCellCount.BorderlineColor = System.Drawing.Color.Black;
            this.ChartCellCount.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            this.ChartCellCount.ChartAreas.Add(chartArea1);
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.IsDockedInsideChartArea = false;
            legend1.Name = "Legend1";
            this.ChartCellCount.Legends.Add(legend1);
            this.ChartCellCount.Location = new System.Drawing.Point(12, 285);
            this.ChartCellCount.Name = "ChartCellCount";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.ChartCellCount.Series.Add(series1);
            this.ChartCellCount.Size = new System.Drawing.Size(415, 300);
            this.ChartCellCount.TabIndex = 10;
            title1.Alignment = System.Drawing.ContentAlignment.TopLeft;
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title1";
            title1.Text = "Throughput [#]";
            this.ChartCellCount.Titles.Add(title1);
            // 
            // ChartQuality
            // 
            this.ChartQuality.BorderlineColor = System.Drawing.Color.Black;
            this.ChartQuality.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea2.Name = "ChartArea1";
            this.ChartQuality.ChartAreas.Add(chartArea2);
            legend2.Alignment = System.Drawing.StringAlignment.Center;
            legend2.DockedToChartArea = "ChartArea1";
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend2.IsDockedInsideChartArea = false;
            legend2.Name = "Legend1";
            this.ChartQuality.Legends.Add(legend2);
            this.ChartQuality.Location = new System.Drawing.Point(854, 285);
            this.ChartQuality.Name = "ChartQuality";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.ChartQuality.Series.Add(series2);
            this.ChartQuality.Size = new System.Drawing.Size(415, 300);
            this.ChartQuality.TabIndex = 11;
            title2.Alignment = System.Drawing.ContentAlignment.TopLeft;
            title2.BackColor = System.Drawing.Color.White;
            title2.DockedToChartArea = "ChartArea1";
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.IsDockedInsideChartArea = false;
            title2.Name = "Title1";
            title2.Text = "Quality Share B/C [%]";
            this.ChartQuality.Titles.Add(title2);
            // 
            // ChartPowerDistribution
            // 
            this.ChartPowerDistribution.BorderlineColor = System.Drawing.Color.Black;
            this.ChartPowerDistribution.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea3.Name = "ChartArea1";
            this.ChartPowerDistribution.ChartAreas.Add(chartArea3);
            legend3.Alignment = System.Drawing.StringAlignment.Center;
            legend3.DockedToChartArea = "ChartArea1";
            legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend3.IsDockedInsideChartArea = false;
            legend3.Name = "Legend1";
            this.ChartPowerDistribution.Legends.Add(legend3);
            this.ChartPowerDistribution.Location = new System.Drawing.Point(433, 285);
            this.ChartPowerDistribution.Name = "ChartPowerDistribution";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.ChartPowerDistribution.Series.Add(series3);
            this.ChartPowerDistribution.Size = new System.Drawing.Size(415, 300);
            this.ChartPowerDistribution.TabIndex = 12;
            title3.Alignment = System.Drawing.ContentAlignment.TopLeft;
            title3.BackColor = System.Drawing.Color.White;
            title3.DockedToChartArea = "ChartArea1";
            title3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title3.IsDockedInsideChartArea = false;
            title3.Name = "Title1";
            title3.Text = "Power Distribution A Quality [%]";
            this.ChartPowerDistribution.Titles.Add(title3);
            // 
            // DgvCellCount
            // 
            this.DgvCellCount.AllowUserToAddRows = false;
            this.DgvCellCount.AllowUserToDeleteRows = false;
            this.DgvCellCount.AllowUserToResizeColumns = false;
            this.DgvCellCount.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.DgvCellCount.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvCellCount.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DgvCellCount.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DgvCellCount.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DgvCellCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DgvCellCount.CausesValidation = false;
            this.DgvCellCount.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.DgvCellCount.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvCellCount.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DgvCellCount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(1);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvCellCount.DefaultCellStyle = dataGridViewCellStyle3;
            this.DgvCellCount.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DgvCellCount.Enabled = false;
            this.DgvCellCount.EnableHeadersVisualStyles = false;
            this.DgvCellCount.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DgvCellCount.Location = new System.Drawing.Point(12, 69);
            this.DgvCellCount.Margin = new System.Windows.Forms.Padding(2);
            this.DgvCellCount.MaximumSize = new System.Drawing.Size(1100, 200);
            this.DgvCellCount.MinimumSize = new System.Drawing.Size(1, 1);
            this.DgvCellCount.MultiSelect = false;
            this.DgvCellCount.Name = "DgvCellCount";
            this.DgvCellCount.ReadOnly = true;
            this.DgvCellCount.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(1);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvCellCount.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DgvCellCount.RowHeadersWidth = 169;
            this.DgvCellCount.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgvCellCount.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.DgvCellCount.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgvCellCount.RowTemplate.ReadOnly = true;
            this.DgvCellCount.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvCellCount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DgvCellCount.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DgvCellCount.ShowCellErrors = false;
            this.DgvCellCount.ShowCellToolTips = false;
            this.DgvCellCount.ShowEditingIcon = false;
            this.DgvCellCount.ShowRowErrors = false;
            this.DgvCellCount.Size = new System.Drawing.Size(1100, 200);
            this.DgvCellCount.TabIndex = 13;
            this.DgvCellCount.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 622);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 65);
            this.label1.TabIndex = 15;
            this.label1.Text = "xdØ: Average of the last x days\r\nm: Average\r\nstd: Standard Deviation\r\nmin: Minimu" +
    "m\r\nmax: Maximum";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 602);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Legend:";
            // 
            // FrmVisualization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DgvCellCount);
            this.Controls.Add(this.ChartPowerDistribution);
            this.Controls.Add(this.ChartQuality);
            this.Controls.Add(this.ChartCellCount);
            this.Controls.Add(this.LblProductionKeyFigures);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmVisualization";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Production Key Figures";
            this.Shown += new System.EventHandler(this.FrmVisualization_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartCellCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartPowerDistribution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCellCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label LblProductionKeyFigures;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartCellCount;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartQuality;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartPowerDistribution;
        private System.Windows.Forms.DataGridView DgvCellCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

