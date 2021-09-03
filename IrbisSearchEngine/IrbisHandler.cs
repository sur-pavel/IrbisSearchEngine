using System.Reflection;
using ManagedIrbis;
using ManagedIrbis.Batch;
using NLog;
using System;
using System.Collections.Generic;
using IrbisSearchEngine.Utils;
using System.Windows.Forms;

namespace IrbisSearchEngine
{
    internal class IrbisHandler
    {
        private const string CONNECT_STRING = "server=194.169.10.3;port=8888;user=1;password=1;arm=R";
        private const string DATABASES_FILE = "DBNAM2_TEST.MNU";
        private const string CALL_MESSAGE = "CALLING: ";
        private const int MAX_BATCH_SIZE = 500;
        private readonly IrbisConnection connection;
        private Logger logger;
        private StringParser stringParser;
        private ProgressBar progressBar;

        internal IrbisHandler(Logger logger, ProgressBar progressBar)
        {
            this.logger = logger;
            this.progressBar = progressBar;
            connection = new IrbisConnection(CONNECT_STRING);
            stringParser = new StringParser(logger);
            logger.Debug("Connection established");
            Console.WriteLine("Connection established");
        }

        internal List<FoundBook> SimpleSearch(string searchTerm)
        {
            logger.Debug(CALL_MESSAGE + MethodBase.GetCurrentMethod());
            Console.WriteLine(CALL_MESSAGE + MethodBase.GetCurrentMethod());
            var marcRecords = new List<MarcRecord>();
            DatabaseInfo[] dataBaseInfos = connection.ListDatabases(DATABASES_FILE);

            foreach (DatabaseInfo databaseInfo in dataBaseInfos)
            {
                string dbName = databaseInfo.Name;
                Console.WriteLine("Database Name: " + dbName);
                connection.Database = dbName;
                try
                {
                    int[] mfns = connection.Search(searchTerm);
                    if (mfns.Length > 0)
                    {
                        int size = mfns.Length > MAX_BATCH_SIZE ? MAX_BATCH_SIZE : mfns.Length;
                        Console.WriteLine($"Batch size: {size}");
                        BatchRecordReader reader = new(connection, dbName, size, mfns);
                        marcRecords.AddRange(reader);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Файл не существует")
                    {
                        continue;
                    }
                    else
                    {
                        logger.Error(ex.Message);
                        Console.WriteLine(ex.Message);
                        logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
            return GetFoundBooks(marcRecords);
        }

        private List<FoundBook> GetFoundBooks(List<MarcRecord> marcRecords)
        {
            logger.Debug(CALL_MESSAGE + MethodBase.GetCurrentMethod());
            Console.WriteLine(CALL_MESSAGE + MethodBase.GetCurrentMethod());
            int size = marcRecords.Count;
            var list = new List<FoundBook>(size);
            int inc = 0;
            foreach (MarcRecord marcRecord in marcRecords)
            {
                inc++;
                // progressBar.Invoke(new Action(() => progressBar.Value = inc * 100 / size));
                list.Add(new FoundBook
                {
                    BriefDescription = GetBriefDescription(marcRecord),
                    FullDescritption = GetFullDescription(marcRecord),
                    DatabaseName = marcRecord.Database,
                    Record = marcRecord
                });
            }
            return list;
        }

        internal string GetBriefDescription(MarcRecord marcRecord)
        {
            logger.Debug(CALL_MESSAGE + MethodBase.GetCurrentMethod());
            Console.WriteLine(CALL_MESSAGE + MethodBase.GetCurrentMethod());
            return connection.FormatRecord("@brief", marcRecord);
        }

        internal string GetFullDescription(MarcRecord marcRecord)
        {
            logger.Debug(CALL_MESSAGE + MethodBase.GetCurrentMethod());
            Console.WriteLine(CALL_MESSAGE + MethodBase.GetCurrentMethod());
            return connection.FormatRecord("@", marcRecord);
        }

        internal void Quit()
        {
            connection.Dispose();
        }
    }
}