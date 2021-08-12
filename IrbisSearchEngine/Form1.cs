using IrbisSearchEngine.Utils;
using NLog;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AM.Windows.Forms;
using System.Data;

namespace IrbisSearchEngine
{
    public partial class Form1 : Form
    {
        private Logger logger;
        private IrbisHandler irbisHandler;
        private StringParser stringParser;
        private List<FoundBook> foundBookList;
        private BrowserForm browserForm;
        public Form1()
        {
            InitializeComponent();
            simpleSearchTextbox.KeyUp += TextBoxKeyUp;
            simpleSearchTextbox.Select();
            LogRunner logRunner = new();
            browserForm = new BrowserForm();
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
            this.WindowState = FormWindowState.Maximized;
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
                foundBookList = irbisHandler.SimpleSearch(searchTerm);
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.CellClick += DataGridView1_CellClick;
                dataGridView1.DataSource = foundBookList;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                ShowBrowser(e);
                //ShowRow(e);
            }
        }

        private void ShowRow(DataGridViewCellEventArgs e)
        {
            string fullDescription = foundBookList[e.RowIndex].FullDescritption;
            foundBookList.Insert(e.RowIndex, new FoundBook() 
            {
                BriefDescription = fullDescription
                }
            );
        }

        private void ShowBrowser(DataGridViewCellEventArgs e)
        {
                if (browserForm.IsDisposed)
                {
                    browserForm = new BrowserForm();
                }
                if (browserForm.Visible == true)
                {
                    browserForm.Visible = false;
                }
                browserForm.DocumentText = foundBookList[e.RowIndex].FullDescritption;
                browserForm.Visible = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            irbisHandler.Quit();
        }
    }
}