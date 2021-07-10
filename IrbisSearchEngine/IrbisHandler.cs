using ManagedIrbis;
using ManagedIrbis.Batch;
using NLog;
using System;
using System.Collections.Generic;

namespace IrbisSearchEngine
{
    internal class IrbisHandler
    {
        private const string CONNECT_STRING = "server=194.169.10.3;port=8888;user=1;password=1;arm=R";
        private const string DATABASES_FILE = "DBNAM2_TEST.MNU";
        private readonly IrbisConnection connection;
        private Logger Logger;

        internal IrbisHandler(Logger logger)
        {
            this.Logger = logger;
            connection = new IrbisConnection(CONNECT_STRING);
        }

        internal List<FoundBook> SimpleSearch(string searchTerm)
        {
            var marcRecords = new List<MarcRecord>();
            DatabaseInfo[] dataBaseInfos = connection.ListDatabases(DATABASES_FILE);
            foreach (DatabaseInfo databaseInfo in dataBaseInfos)
            {
                connection.Database = databaseInfo.Name;
                try
                {
                    int[] mfns = connection.Search(searchTerm);
                    int size = mfns.Length > 500 ? 500 : mfns.Length;
                    BatchRecordReader reader = new BatchRecordReader(connection, databaseInfo.Name, size, mfns);
                    marcRecords.AddRange(reader);
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Файл не существует")
                    {
                        continue;
                    }
                    else
                    {
                        Logger.Error(ex.Message);
                        Logger.Error(ex.StackTrace);
                    }
                }
            }

            return GetFoundBooks(marcRecords);
        }

        private List<FoundBook> GetFoundBooks(List<MarcRecord> marcRecords)
        {
            var list = new List<FoundBook>(marcRecords.Count);
            foreach (MarcRecord marcRecord in marcRecords)
            {
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
            return connection.FormatRecord("@brief", marcRecord);
        }

        internal string GetFullDescription(MarcRecord marcRecord)
        {
            return connection.FormatRecord("@", marcRecord);
        }

        internal void Quit()
        {
            connection.Dispose();
        }
    }
}