namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ConsoleApp.Interface;

    internal class DataAggregator : IDataAggregator
    {

        private Dictionary<string, Dictionary<string, IChildrenSchema>> ParentData;
        private readonly List<IChildrenSchema> builderObjects;

        internal DataAggregator(List<IChildrenSchema> children)
        {
            try
            {
                builderObjects = children;
                AggregateData();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void AggregateDataByKey(IChildrenSchema obj)
        {

            var parentKey = $"{obj.ParentType}-{obj.ParentName}";
            var childKey = $"{obj.Type}-{obj.Name}";

            if (ParentData.ContainsKey(parentKey))
            {
                var oldChildren = ParentData[parentKey];
                oldChildren[childKey] = obj;

            } else

            {
                var children = new Dictionary<string, IChildrenSchema>
                {
                    [childKey] = obj
                };

                ParentData[parentKey] = children;
            }
        }


        private void ShowChildren(Dictionary<string, IChildrenSchema> children)
        {
            foreach (var child in children.Values)
            {
                var message = BuildLogsByKey(child.Type, child);
                Console.WriteLine($"{message}");
            }
        }

        private void ShowParent(KeyValuePair<string, Dictionary<string, IChildrenSchema>> parent)
        {
            var parentKey = parent.Key;
            var children = parent.Value;
            var parentKeyValues = parentKey.Split('-');
            Console.WriteLine($"-------------------------------------------------------------------");
            Console.WriteLine($"ParentType: {parentKeyValues[0]}, ParentName: {parentKeyValues[1]}, number of children: {children.Count()}");

                ShowChildren(children);
        }
        public void GetAllChildrenData()
        {
            foreach (var parent in ParentData)
            {
                ShowParent(parent);
            }
        }

        private void AggregateData()
        {
            ParentData = new Dictionary<string, Dictionary<string, IChildrenSchema>>();

            foreach (var obj in builderObjects)
            {
               
                    AggregateDataByKey(obj);
                      
            }
        }
        private string BuildLogsByKey(string key, IChildrenSchema value)
        {
            var tablesMessage = $"\t Type {value.Type}, Schema: {value.Schema}.{value.Name}";
            var columnsMessage = $"\t Type {value.Type}, Name: {value.Name}, with data type: {value.DataType}, {(value.IsNullable ? "accepts nulls" : "with no nulls")}";
            return key == "COLUMN" ? columnsMessage : tablesMessage;
        }
    }
}