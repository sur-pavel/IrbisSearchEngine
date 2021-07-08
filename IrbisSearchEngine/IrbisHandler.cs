using ManagedIrbis;
using ManagedIrbis.Batch;
using NLog;
using System;
using System.Collections.Generic;

namespace IrbisSearchEngine
{
    internal class IrbisHandler
    {
        private const string CONNECT_STRING = "server=194.169.10.3;port=8888;user=СПА;password=4209;";
        private const string DATABASES_FILE = "DBNAM2_2.MNU";
        private IrbisConnection connection;
        private Logger logger;

        internal IrbisHandler(Logger logger)
        {
            this.logger = logger;
            connection = new IrbisConnection(CONNECT_STRING);
        }

        internal BatchRecordReader SimpleSearch(string searchTerm)
        {
            
                List<int> foundMfns = new();
                DatabaseInfo[] dataBaseInfos = connection.ListDatabases(DATABASES_FILE);
                foreach (DatabaseInfo databaseInfo in dataBaseInfos)
                {
                    connection.Database = databaseInfo.Name;
                    logger.Debug("DB: " + databaseInfo.Name);
                    try
                    {
                        int[] mfns = connection.Search(searchTerm);
                        foreach (int mfn in mfns)
                        {
                            foundMfns.Add(mfn);
                        }
                    } catch (Exception ex)
                    {
                        if (ex.Message == "Файл не существует")
                        {
                            continue;
                        }
                    }
                }
            int size = foundMfns.Count > 500 ? 500 : foundMfns.Count;
            return new BatchRecordReader(connection, "MPDA", size, foundMfns.ToArray());                       
        }

        internal string GetBriefDescription(MarcRecord marcRecord)
        {
                return connection.FormatRecord("@brief", marcRecord);
        }

        internal string GetOptimizedDescription(MarcRecord marcRecord)
        {           
                return connection.FormatRecord("@", marcRecord);
        }

        internal void Quit()
        {
            connection.Dispose();
        }
    }
}