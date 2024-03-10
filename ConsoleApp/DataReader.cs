namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ConsoleApp.Enum;

    public class DataReader
    {

        private List<ImportedObject> ImportedObjects;

        private List<string> ImportedDataLines;

        private List<string> ImportDataColumns;

        public Dictionary<DataReaderKey, Dictionary<string, ImportedObject>> ImportedData;
        private void readDataFromStream(string filePath)
        {

            if (!File.Exists(filePath)) throw new FileNotFoundException($"File doesn't Exist: {filePath}");

            using (StreamReader streamReader = new StreamReader(filePath))
            {

                try
                {
                    ImportedDataLines = new List<string>();

                    while (!streamReader.EndOfStream)

                    {
                        var line = streamReader.ReadLine();
                        ImportedDataLines.Add(line);
                    }
                }
                catch (IOException ex)
                {
                    throw new Exception($"An error occurred while reading the file: {ex.Message}");
                }

                catch (Exception ex)
                {
                    throw new Exception($"An unexpected error occurred: {ex.Message}");

                }


            }

        }
        private string[] splitAndClearLine(string line)
        {
            return line.Split(';').Where(c => !string.IsNullOrEmpty(c)).ToArray();
        }
        private void readDataColumns()
        {
            ImportDataColumns = new List<string>(7);

            var firstLine = ImportedDataLines[0];
            var columns = splitAndClearLine(firstLine);

            foreach (var column in columns)
            {
                ImportDataColumns.Add(column);
            }

        }
        private void buildDataObjects()
        {
            ImportedObjects = new List<ImportedObject>();

            for (int i = 1; i < ImportedDataLines.Count; i++)
            {
                var importedLine = ImportedDataLines[i];

                var rowValues = splitAndClearLine(importedLine);

                if (rowValues.Count() == ImportDataColumns.Count())
                {
                    var ImportedObject = new ImportedObject(rowValues);
                    ImportedObjects.Add(ImportedObject);
                }
            }
        }
        private Dictionary<string, ImportedObject> agregateTables()
        {

            var AgregatedTables = new Dictionary<string, ImportedObject>();

            foreach (var obj in ImportedObjects)
            {

                var key = $"{obj.ParentType}-{obj.ParentName}";

                if (AgregatedTables.ContainsKey(key))
                {
                    var oldObj = AgregatedTables[key];
                    oldObj.NumberOfChildren++;

                }
                else
                {
                    obj.NumberOfChildren = 1;
                    AgregatedTables[key] = obj;

                }  
            }
            return AgregatedTables;
        }
        private Dictionary<string, ImportedObject> agregateColumns()
        {
            var AgregatedColumns = new Dictionary<string, ImportedObject>();

            foreach (var obj in ImportedObjects)
            {

                var key = $"{obj.Type}-{obj.Name}";

                if (!AgregatedColumns.ContainsKey(key))
                {
                    AgregatedColumns[key] = obj;
                }
            }

            return AgregatedColumns;

        }
        public Dictionary<string, ImportedObject> getDataByKey(DataReaderKey key)
        {
             var data = ImportedData[key];
    
            foreach (var obj in data)
            {
                var value = obj.Value;
                var dataLogs = buildLogsByKey(key, value);

                Console.WriteLine($"---------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"{dataLogs}");
            }
            return data;           
    }
        private void initializeState(string filePath)
        {
            readDataFromStream(filePath);
            readDataColumns();
        }
        private void agregateDataByKey()
        {
            ImportedData = new Dictionary<DataReaderKey, Dictionary<string, ImportedObject>>();

            var agregatedTables = agregateTables();
            var agregatedColumns = agregateColumns();

            ImportedData[DataReaderKey.TABLES] = agregatedTables;
            ImportedData[DataReaderKey.COLUMNS] = agregatedColumns;
        }
        private string buildLogsByKey(DataReaderKey key, ImportedObject value)
        {
                var tablesMessage = $"\tTable '{value.Schema}.{value.Name}' ({value.NumberOfChildren} columns)";
                var columsMessage = $"\t\tColumn '{value.Name}' with {value.DataType} data type {(value.IsNullable ? "accepts nulls" : "with no nulls")}";

                return key == DataReaderKey.TABLES ? tablesMessage : columsMessage;
        }
        public DataReader(string filePath)
        {
            try
                {
                    initializeState(filePath);
                    buildDataObjects();
                    agregateDataByKey();
                }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}