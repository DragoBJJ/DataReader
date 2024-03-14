namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ConsoleApp.Interface;

    public class DataAggregator: IDataAggregator
    {

        private Dictionary<string, Dictionary<string, BuilderObject>> ParentData;
        private Dictionary<string, BuilderObject> Databases;
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

        private void AggregateDatabases(BuilderObject obj)
        {      
                if (!Databases.ContainsKey(obj.Name))
                {

                    Databases[obj.Name] = obj;
                }
        }
        private void AggregateDataByKey(BuilderObject obj)
        {

            var parentKey = $"{obj.ParentType}-{obj.ParentName}";
            var childKey = $"${obj.Type}-{obj.Name}";
 
            if (ParentData.ContainsKey(parentKey))
            {
                var oldChildren = ParentData[parentKey];
                oldChildren[childKey] = obj;
                
            } else

            {
                var children = new Dictionary<string, BuilderObject>
                {
                    [childKey] = obj
                };

                ParentData[parentKey] = children;
            }
        }


        private void ShowChildren(Dictionary<string,BuilderObject> children)
        {
            foreach (var child in children.Values)
            {
                var message = BuildLogsByKey(child.Type, child);
                Console.WriteLine($"{message}");
            }
        }

        private void ShowParent(KeyValuePair<string, Dictionary<string,BuilderObject>> parent)
        {
            var parentKey = parent.Key;
            var children = parent.Value;
            var parentKeyValues = parentKey.Split('-');
            Console.WriteLine($"-------------------------------------------------------------------");
            Console.WriteLine($"ParentType: {parentKeyValues[0]}, ParentName: {parentKeyValues[1]}, number of children: {children.Count()}");

            ShowChildren(children);
        }
        public void GetAllCollectedData()
        {
            foreach (var parent in ParentData)
            {
                ShowParent(parent); 
            }
        }
     
        private void AggregateData()
        {
            ParentData = new Dictionary<string, Dictionary<string, BuilderObject>>();
            Databases = new Dictionary<string, BuilderObject>();

            foreach (var obj in _builderObjects)
            {   
                    if (obj.ParentType is string & obj.ParentName is string)
                {
                        AggregateDataByKey(obj);
                }
                else

                {
                        AggregateDatabases(obj);                     
                }
            }       
        }
                   
        private string BuildLogsByKey(string key, BuilderObject value)
        {
                var tablesMessage = $"\t Type {value.Type}, Schema: {value.Schema}.{value.Name}";
                var columnsMessage = $"\t Type {value.Type}, Name: {value.Name}, with data type: {value.DataType}, {(value.IsNullable ? "accepts nulls" : "with no nulls")}";
               return key == "COLUMN" ? columnsMessage : tablesMessage;
        }

        public void GetAllDatabases()
        {
            foreach (var database in Databases)
            {
                Console.WriteLine($"-------------------------------------------------------------------");
                Console.WriteLine($"Database Name: {database.Key}");
            }
        }
    }

}