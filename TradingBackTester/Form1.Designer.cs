namespace TradingBackTester
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.historicalChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.reqHistoricalData_btn = new System.Windows.Forms.Button();
            this.backTestData = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ticker_label = new System.Windows.Forms.Label();
            this.ticker_label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.historicalChart)).BeginInit();
            this.SuspendLayout();
            // 
            // historicalChart
            // 
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.historicalChart.ChartAreas.Add(chartArea1);
            legend1.ForeColor = System.Drawing.Color.DarkGray;
            legend1.HeaderSeparatorColor = System.Drawing.Color.DarkGray;
            legend1.ItemColumnSeparatorColor = System.Drawing.Color.DarkGray;
            legend1.Name = "Legend1";
            this.historicalChart.Legends.Add(legend1);
            this.historicalChart.Location = new System.Drawing.Point(12, 12);
            this.historicalChart.Name = "historicalChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.historicalChart.Series.Add(series1);
            this.historicalChart.Size = new System.Drawing.Size(1169, 483);
            this.historicalChart.TabIndex = 0;
            this.historicalChart.Text = "chart1";
            // 
            // reqHistoricalData_btn
            // 
            this.reqHistoricalData_btn.Location = new System.Drawing.Point(1023, 580);
            this.reqHistoricalData_btn.Name = "reqHistoricalData_btn";
            this.reqHistoricalData_btn.Size = new System.Drawing.Size(159, 28);
            this.reqHistoricalData_btn.TabIndex = 1;
            this.reqHistoricalData_btn.Text = "Request Data";
            this.reqHistoricalData_btn.UseVisualStyleBackColor = true;
            this.reqHistoricalData_btn.Click += new System.EventHandler(this.reqHistoricalData_btn_Click);
            // 
            // backTestData
            // 
            this.backTestData.Location = new System.Drawing.Point(1023, 538);
            this.backTestData.Name = "backTestData";
            this.backTestData.Size = new System.Drawing.Size(158, 28);
            this.backTestData.TabIndex = 2;
            this.backTestData.Text = "Back Test Data";
            this.backTestData.UseVisualStyleBackColor = true;
            this.backTestData.Click += new System.EventHandler(this.backTestData_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(886, 546);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(81, 21);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "ES";
            // 
            // ticker_label
            // 
            this.ticker_label.AutoSize = true;
            this.ticker_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ticker_label.Location = new System.Drawing.Point(837, 546);
            this.ticker_label.Name = "ticker_label";
            this.ticker_label.Size = new System.Drawing.Size(43, 15);
            this.ticker_label.TabIndex = 4;
            this.ticker_label.Text = "Ticker:";
            // 
            // ticker_label2
            // 
            this.ticker_label2.AutoSize = true;
            this.ticker_label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ticker_label2.Location = new System.Drawing.Point(771, 580);
            this.ticker_label2.Name = "ticker_label2";
            this.ticker_label2.Size = new System.Drawing.Size(109, 15);
            this.ticker_label2.TabIndex = 6;
            this.ticker_label2.Text = "Benchmark Ticker:";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(886, 580);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(81, 21);
            this.textBox2.TabIndex = 5;
            this.textBox2.Text = "ES";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1201, 619);
            this.Controls.Add(this.ticker_label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.ticker_label);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.backTestData);
            this.Controls.Add(this.reqHistoricalData_btn);
            this.Controls.Add(this.historicalChart);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.historicalChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart historicalChart;
        private System.Windows.Forms.Button reqHistoricalData_btn;
        private System.Windows.Forms.Button backTestData;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label ticker_label;
        private System.Windows.Forms.Label ticker_label2;
        private System.Windows.Forms.TextBox textBox2;
    }
}

