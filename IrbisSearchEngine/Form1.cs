using IrbisSearchEngine.Utils;
using ManagedIrbis;
using ManagedIrbis.Batch;
using NLog;
using System;
using System.Collections.Generic;
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
                List<FoundBook> list = irbisHandler.SimpleSearch(searchTerm);
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = list;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            irbisHandler.Quit();
        }
    }
}