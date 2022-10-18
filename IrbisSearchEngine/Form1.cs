using IrbisSearchEngine.Utils;
using NLog;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AM.Windows.Forms;
using System.Data;
using System.Threading.Tasks;

namespace IrbisSearchEngine
{
    public partial class Form1 : Form
    {
        private Logger logger;
        private Logging selfLog;
        private IrbisHandler irbisHandler;
        private StringParser stringParser;

        private Patterns patterns;
        private List<FoundBook> foundBookList;
        private BrowserForm browserForm;

        public Form1()
        {
            InitializeComponent();
            selfLog = new Logging();
            selfLog.CreateLogFile();
            simpleSearchTextbox.KeyUp += TextBoxKeyUp;
            simpleSearchTextbox.Select();
            LogRunner logRunner = new();
            browserForm = new BrowserForm();
            patterns = new Patterns();

            this.logger = logRunner.Logger;
            try
            {
                stringParser = new StringParser(logger);
                irbisHandler = new IrbisHandler(logger, progressBar1);
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
            //simpleSearchTextbox.Text = "о воскресении мертвых";

            //DoSimpleSearch();
            // irbisHandler.ExtendedSearch("p(v910)");

            //SearchSaveByYear(2010);
            //SearchSaveByYear(2011);
            //SearchSaveByYear(2012);
            //SearchSaveByYear(2013);
            //SearchSaveByYear(2014);
            //SearchSaveByYear(2015);
            //SearchSaveByYear(2016);
            //SearchSaveByYear(2017);
            //SearchSaveByYear(2018);
            //SearchSaveByYear(2019);
            //SearchSaveByYear(2020);
        }

        private void SearchSaveByYear(int yearInt)
        {
            string KEYWORD_SEARCH_PREFIX = "G=";
            string year = yearInt.ToString();
            irbisHandler.SimpleSearch($"\"{KEYWORD_SEARCH_PREFIX}{year}\"/()");
            string text = string.Join("\n", irbisHandler.docTextList);
            selfLog.WriteLine(year);
            selfLog.WriteLine(text);
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
                if (searchTerm.Length > 3)
                {
                    var task = Task.Run(() => foundBookList = irbisHandler.SimpleSearch(searchTerm));
                    task.Wait();
                    logger.Info("Found {} records", foundBookList.Count);

                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.RowHeadersVisible = false;
                    dataGridView1.CellClick += DataGridView1_CellClick;
                    dataGridView1.DataSource = foundBookList;
                }
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

        private void extendedSearchButton_Click(object sender, EventArgs e)
        {
            irbisHandler.ExtendedSearch(extendedSearchTextBox1.Text);
        }

        private void simpleSearchButton_Click_1(object sender, EventArgs e)
        {
            DoSimpleSearch();
        }
    }
}