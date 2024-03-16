using System;
using System.Linq;
using ConsoleApp.Interface;

namespace ConsoleApp.classes
{
    internal class DataBase: IDataBaseSchema
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int NumberOfChildren { get; set; }

        public string CleanString(string input)
        {
            return input.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
        }

         public DataBase(string[] values)
        {
            try
            {
                var cleanedValues = values.Select(CleanString).ToArray();

                Type = cleanedValues[0].ToUpper();
                Name = cleanedValues[1];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}
