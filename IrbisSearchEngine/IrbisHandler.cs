using System.Reflection;
using ManagedIrbis;
using ManagedIrbis.Batch;
using NLog;
using System;
using System.Collections.Generic;
using IrbisSearchEngine.Utils;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ManagedIrbis.Search;

namespace IrbisSearchEngine
{
    internal class IrbisHandler
    {
        private const string CONNECT_STRING = "server=194.169.10.3;port=8888;user=СПА;password=4209;arm=C";
        private const string DATABASES_FILE = "DBNAM2_TEST.MNU";
        private const string CALL_MESSAGE = "CALLING: ";
        private const int MAX_BATCH_SIZE = 500;
        private readonly IrbisConnection connection;
        private Logger logger;
        private StringParser stringParser;
        private ProgressBar progressBar;
        internal List<string> docTextList = new();

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
                logger.Debug("Database Name {}", dbName);
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
            docTextList = new List<string>();
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
                    InvNum = GetField(marcRecord, 910, 'b').Replace(",", ""),
                    Record = marcRecord
                });
                SaveDissertationRecord(marcRecord);
            }
            return list;
        }

        private void SaveDissertationRecord(MarcRecord marcRecord)
        {
            logger.Debug(GetBriefDescription(marcRecord) + " " + marcRecord.FM(910));
            string index = marcRecord.FM(686);
            string authorIndex = marcRecord.FM(908);
            string sifer = index != null && authorIndex != null ? index + "/" + authorIndex : "";
            string recordText = marcRecord.FM(700, 'a') + GetField(marcRecord, 700, 'b') + GetField(marcRecord, 700, '1') + " " + sifer;
            docTextList.Add(recordText);
        }

        private string GetField(MarcRecord record, int tag, char code)
        {
            string fieldText = "";
            string s = record.FM(tag, code);
            if (s != null && s.Length > 0)
            {
                fieldText = ", " + s;
            }
            return fieldText;
        }

        internal void ExtendedSearch(string text)
        {
            logger.Debug(CALL_MESSAGE + MethodBase.GetCurrentMethod());
            Console.WriteLine(CALL_MESSAGE + MethodBase.GetCurrentMethod());
            var marcRecords = DoSearch(text, "COLL");
            logger.Debug("records size:" + marcRecords.Count);
            HashSet<string> set = new HashSet<string>();
            var recordsToMove = new List<MarcRecord>();
            foreach (MarcRecord marcRecord in marcRecords)
            {
                string[] invNums = marcRecord.FMA(910, 'd');
                foreach (string str in invNums)
                {
                    if (Regex.IsMatch(str, @".+-[а-я]{2,}"))
                    {
                        set.Add(str);
                        recordsToMove.Add(marcRecord);
                    }
                }
            }
            foreach (string str in set)
            {
                logger.Debug(str);
            }
            logger.Debug("Records to Move :" + recordsToMove.Count);
        }

        private void MoveRecords(List<MarcRecord> recordsToMove)
        {
            logger.Debug(CALL_MESSAGE + MethodBase.GetCurrentMethod());
            Console.WriteLine(CALL_MESSAGE + MethodBase.GetCurrentMethod());
            connection.Database = "COLL";
            var movedRecods = new List<MarcRecord>();
            var manager = new CopyRecordManager(connection, "IFN");
            foreach (MarcRecord marcRecord in recordsToMove)
            {
                movedRecods.Add(manager.MoveRecord(marcRecord));
            }
            logger.Debug("Moved Recods: " + movedRecods.Count);
            int count = 0;
            foreach (var record in movedRecods)
            {
                logger.Debug(++count + ". " + connection.FormatRecord("@brief", record));
            }
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

        internal List<MarcRecord> DoSearch(string searchTerm, string dbName)
        {
            logger.Debug(CALL_MESSAGE + MethodBase.GetCurrentMethod());
            Console.WriteLine(CALL_MESSAGE + MethodBase.GetCurrentMethod());
            var marcRecords = new List<MarcRecord>();
            SearchParameters parameters = new SearchParameters
            {
                Database = dbName,
                FormatSpecification = "@",
                SequentialSpecification = searchTerm
            };

            try
            {
                int[] mfns = connection.SequentialSearch(parameters);
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
                logger.Error(ex.Message);
                Console.WriteLine(ex.Message);
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
            }
            return marcRecords;
        }

        //internal void CopyRecord(MarcRecord record, string dbName){
        //var copyRecordManager = CopyRecordManager(connection, dbName);
        //copyRecordManager.CopyRecord(record);

        //}
    }
}