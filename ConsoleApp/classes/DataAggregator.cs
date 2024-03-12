﻿namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ConsoleApp.Enum;
    using ConsoleApp.Interface;

    public class DataAggregator: IDataAggregator
    {

        private Dictionary<DataKey, Dictionary<string, BuilderObject>> collectionData;

        private  Dictionary<string, BuilderObject> AggregatedTables;
        private  Dictionary<string, BuilderObject> AggregatedColumns;

        private readonly List<BuilderObject> _builderObjects;

        public DataAggregator(List<BuilderObject> builderObjects)
        {
            try
            {
                _builderObjects = builderObjects;
                CollectionData();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void AggregateData()
        {
             AggregatedTables = new Dictionary<string, BuilderObject>();
             AggregatedColumns = new Dictionary<string, BuilderObject>();

            foreach (var obj in _builderObjects)
            {

                var tableKey = $"{obj.ParentType}-{obj.ParentName}";
                var columnKey = $"{obj.Type}-{obj.Name}";

                if (AggregatedTables.ContainsKey(tableKey))
                {
                    var oldObj = AggregatedTables[tableKey];
                    oldObj.NumberOfChildren++;

                }
                else
                {
                    obj.NumberOfChildren = 1;
                    AggregatedTables[tableKey] = obj;

                }

                if (!AggregatedColumns.ContainsKey(columnKey))
                {
                    AggregatedColumns[columnKey] = obj;
                }
            }
        }
        public Dictionary<string, BuilderObject> GetDataByKey(DataKey key)
        {
             var data = collectionData[key];
    
            foreach (var obj in data)
            {
                var value = obj.Value;
                var dataLogs = BuildLogsByKey(key, value);

                Console.WriteLine($"---------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"{dataLogs}");
            }
            return data;           
    }

        private void CollectionData()
        {
            collectionData = new Dictionary<DataKey, Dictionary<string, BuilderObject>>();

            AggregateData();

            collectionData[DataKey.TABLES] = AggregatedTables;
            collectionData[DataKey.COLUMNS] = AggregatedColumns;

        }
        private string BuildLogsByKey(DataKey key, BuilderObject value)
        {
                var tablesMessage = $"\tTable '{value.Schema}.{value.Name}' ({value.NumberOfChildren} columns)";
                var columsMessage = $"\t\tColumn '{value.Name}' with {value.DataType} data type {(value.IsNullable ? "accepts nulls" : "with no nulls")}";

                return key == DataKey.TABLES ? tablesMessage : columsMessage;
        }

    }
}