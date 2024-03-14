namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ConsoleApp.Enum;
    using ConsoleApp.Interface;

    public class DataAggregator: IDataAggregator
    {

        private Dictionary<DataKey, BuilderObject> collectionData;
        private Dictionary<string, Dictionary<string, BuilderObject>> ParentData;

        private  Dictionary<string, int> AggregatedTables;
        private  Dictionary<string, BuilderObject> AggregatedColumns;

        private readonly List<BuilderObject> _builderObjects;

        public DataAggregator(List<BuilderObject> builderObjects)
        {
            try
            {
                _builderObjects = builderObjects;
                AggregateData();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


  
        private void SaveTableByKey(BuilderObject obj)
        {
            var parentKey = $"{obj.ParentType}-{obj.ParentName}";
            var childKey = $"${obj.Type}-{obj.Name}";



            if (AggregatedTables.ContainsKey(parentKey))
            {
                AggregatedTables[parentKey] = AggregatedTables[parentKey] + 1;

            }
            else
            {
                AggregatedTables[parentKey] = 1;
            }


            if (ParentData.ContainsKey(parentKey))
            {
                var oldChildren = ParentData[parentKey];
                oldChildren[childKey] = obj;
                
            } else
            {
                var children = new Dictionary<string, BuilderObject>();
                children[childKey] = obj;
                ParentData[parentKey] = children;
            }
        }

        private void showAllDataBaseTable()
        {
            /*        var databaseKey = "DATABASE-BaseballData";
                    var children = parentData[databaseKey];

        */
    
            foreach (var parent in ParentData)
            {
                    var parentKey = parent.Key;
                    var children = parent.Value;

                Console.WriteLine($"-------------------------------------------------------------------");
                Console.WriteLine($"Parent: {parentKey}: Number of Child: {children.Count()}");
                foreach (var child in children.Values)
                {
                    Console.WriteLine($"Child: {child.Type}-{child.Name}");
                }
            }
        }
        private void AggregateData()
        {
             AggregatedTables = new Dictionary<string, int>();
            AggregatedColumns = new Dictionary<string, BuilderObject>();
            ParentData = new Dictionary<string, Dictionary<string, BuilderObject>>();



            foreach (var obj in _builderObjects)
            {
                 SaveTableByKey(obj);                            
            }       
        }
                   
        private string BuildLogsByKey(DataKey key, BuilderObject value)
        {
                var tablesMessage = $"\t Type {value.Type}, Schema: {value.Schema}.{value.Name}' ({value.NumberOfChildren} Children)";
                var columnsMessage = $"\t Type {value.Type}, Name: {value.Name}, with data type: {value.DataType}, {(value.IsNullable ? "accepts nulls" : "with no nulls")}";

                return key == DataKey.TABLES ? tablesMessage : columnsMessage;
        }

        public void GetDataByKey()
        {
           /* foreach (var parent in AggregatedTables)
            {
                var childNumber = parent.Value;
                Console.WriteLine($"key:{parent.Key} {childNumber}");

            }*/

            showAllDataBaseTable();

            }
        }
}