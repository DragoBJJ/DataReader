using System;
using System.Linq;
using ConsoleApp.Interface;

namespace ConsoleApp.classes
{
    internal class Table : IChildrenSchema
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Schema { get; set; }
        public string ParentName { get; set; }
        public string ParentType { get; set; }
        public int NumberOfChildren { get; set; }

        public string DataType { get; set; }
        public bool IsNullable { get; set; }

        public string CleanString(string input)
        {
            return input.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
        }
        public Table(string[] values) {
            try
            {
                var cleanedValues = values.Select(CleanString).ToArray();

                Type = cleanedValues[0].ToUpper();
                Name = cleanedValues[1];
                Schema = cleanedValues[2];
                ParentName = cleanedValues[3];
                ParentType = cleanedValues[4].ToUpper();

                DataType = null;
                IsNullable = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}
