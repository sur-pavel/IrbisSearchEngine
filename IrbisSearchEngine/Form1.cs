using IrbisSearchEngine.Utils;
using ManagedIrbis;
using ManagedIrbis.Batch;
using NLog;
using System;
using System.Windows.Forms;

namespace IrbisSearchEngine
{
    public partial class Form1 : Form
    {
        private MarcRecord currentRecord;

        private WebBrowser webBrowser;
        private Logger logger;
        private IrbisHandler irbisHandler;
        private StringParser stringParser;

        public Form1()
        {
            InitializeComponent();
            simpleSearchTextbox.KeyUp += TextBoxKeyUp;
            simpleSearchTextbox.Select();
            LogRunner logRunner = new();
            this.logger = logRunner.Logger;
            stringParser = new StringParser();
            try
            {
                irbisHandler = new IrbisHandler(logger);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            simpleSearchTextbox.Text = "капитанская дочка";
            DoSimpleSearch();
        }

        private void SimpleSearchButton_Click(object sender, EventArgs e)
        {
            DoSimpleSearch();
        }

       

        private void TextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoSimpleSearch();
                e.Handled = true;
            }
        }

        private void DoSimpleSearch()
        {
            try
            {
                string searchTerm = stringParser.CreateSearchTerm(simpleSearchTextbox.Text);
                logger.Debug("SearchTerm: " + searchTerm);
                BatchRecordReader reader = irbisHandler.SimpleSearch(searchTerm);
                logger.Debug("Founded records: " + reader.BatchSize);
                tableLayoutPanel1.Controls.Clear();
                tableLayoutPanel1 = new TableLayoutPanel();
                this.tableLayoutPanel1.ColumnCount = 2;
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.89273F));
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.10727F));
                tableLayoutPanel1.Location = new System.Drawing.Point(29, 77);
                
                foreach (MarcRecord marcRecord in reader)
                {
                    currentRecord = marcRecord;
                    tableLayoutPanel1.RowCount++;
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
                    LinkLabel briefDescriptionLabel = new() { Text = irbisHandler.GetBriefDescription(marcRecord) };
                    LinkLabel fullDescriptionLabel = new() { Text = "Полное описание" };
                    briefDescriptionLabel.LinkVisited = true;
                    fullDescriptionLabel.LinkVisited = true;
                    fullDescriptionLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkLabel_LinkClicked);
                    tableLayoutPanel1.Controls.Add(briefDescriptionLabel, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(fullDescriptionLabel, 1, tableLayoutPanel1.RowCount - 1);
                }
                tableLayoutPanel1.Visible = true;
                tableLayoutPanel1.Show();
                simpleSearchPage.Controls.Add(this.tableLayoutPanel1);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }
        private void LinkLabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            if (webBrowser != null && webBrowser.Visible)
            {
                webBrowser.Hide();
                int row = tableLayoutPanel1.GetPositionFromControl(webBrowser).Row;
                tableLayoutPanel1.RowStyles[row].SizeType = SizeType.Absolute;
                tableLayoutPanel1.RowStyles[row].Height = 0;
                tableLayoutPanel1.Show();
            }
            else
            {
                webBrowser = new();
                string optimizedRecord = irbisHandler.GetOptimizedDescription(currentRecord);
                optimizedRecord = stringParser.FormatAsHttp(optimizedRecord);
                webBrowser.DocumentText = optimizedRecord;
                tableLayoutPanel1.RowCount++;
                tableLayoutPanel1.Controls.Add(webBrowser);
                tableLayoutPanel1.SetColumnSpan(webBrowser, 2);
                tableLayoutPanel1.Show();
                webBrowser.Show();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            irbisHandler.Quit();
        }
    }
}