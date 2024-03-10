namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ConsoleApp.Enum;
    using ConsoleApp.Interface;

    public class DataReader: IDataReader
    {

        private Dictionary<DataReaderKey, Dictionary<string, BuilderObject>> readData;

        private readonly List<BuilderObject> _builderObjects;

        public DataReader(List<BuilderObject> builderObjects)
        {
            try
                {
                    _builderObjects = builderObjects;
                    agregateDataByKey();
                }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private Dictionary<string, BuilderObject> agregateTables()
        {

            var AgregatedTables = new Dictionary<string, BuilderObject>();

            foreach (var obj in _builderObjects)
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
        private Dictionary<string, BuilderObject> agregateColumns()
        {
            var AgregatedColumns = new Dictionary<string, BuilderObject>();

            foreach (var obj in _builderObjects)
            {

                var key = $"{obj.Type}-{obj.Name}";

                if (!AgregatedColumns.ContainsKey(key))
                {
                    AgregatedColumns[key] = obj;
                }
            }

            return AgregatedColumns;

        }
        public Dictionary<string, BuilderObject> GetDataByKey(DataReaderKey key)
        {
             var data = readData[key];
    
            foreach (var obj in data)
            {
                var value = obj.Value;
                var dataLogs = buildLogsByKey(key, value);

                Console.WriteLine($"---------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"{dataLogs}");
            }
            return data;           
    }

        private void agregateDataByKey()
        {
            readData = new Dictionary<DataReaderKey, Dictionary<string, BuilderObject>>();

            var agregatedTables = agregateTables();
            var agregatedColumns = agregateColumns();

            readData[DataReaderKey.TABLES] = agregatedTables;
            readData[DataReaderKey.COLUMNS] = agregatedColumns;

        }
        private string buildLogsByKey(DataReaderKey key, BuilderObject value)
        {
                var tablesMessage = $"\tTable '{value.Schema}.{value.Name}' ({value.NumberOfChildren} columns)";
                var columsMessage = $"\t\tColumn '{value.Name}' with {value.DataType} data type {(value.IsNullable ? "accepts nulls" : "with no nulls")}";

                return key == DataReaderKey.TABLES ? tablesMessage : columsMessage;
        }

    }
}