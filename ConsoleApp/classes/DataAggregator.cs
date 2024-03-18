namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ConsoleApp.Interface;
    internal class DataAggregator : IDataAggregator
    {
        private readonly Dictionary<string, Dictionary<string, IChildrenSchema>> ParentData;
        private readonly List<IChildrenSchema> builderObjects;
        internal DataAggregator(List<IChildrenSchema> children)
        {
            try
            {
                ParentData = new Dictionary<string, Dictionary<string, IChildrenSchema>>();
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
        private void ShowParentMessage(string parentType, string parentName, int childrenCount)
        {
            Console.WriteLine($"-------------------------------------------------------------------");
            Console.WriteLine($"ParentType: {parentType}, ParentName: {parentName}, number of children: {childrenCount}");
        }

        private void DisplayAllData(KeyValuePair<string, Dictionary<string, IChildrenSchema>> parent)
        {
            var parentKey = parent.Key;
            var children = parent.Value;
            var key = parentKey.Split('-');

            ShowParentMessage(key[0], key[1], children.Count());
            ShowChildren(children);
        }
        public void GetAllChildrenData()
        {
            foreach (var parent in ParentData)
            {
                        DisplayAllData(parent);
            }
        }

        public void GetChildrenByKey(string parentKey, string parentName)
        {
                var key =  $"{parentKey}-{parentName}";
                var children = ParentData[key];
                ShowParentMessage(parentKey, parentName, children.Count());
                ShowChildren(children);
        }    
        private void AggregateData()
        {

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