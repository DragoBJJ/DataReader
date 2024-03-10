using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Interface;

namespace ConsoleApp
{
         public class ImportedObject : IDataSchema
    {
        public string Name { get; set; }
        public string Schema { get; set; }
        public string ParentName { get; set; }
        public string ParentType { get; set; }
        public string DataType { get; set; }
        public bool IsNullable { get; set; }
        public int NumberOfChildren { get; set; }
        public string Type { get; set; }

        public ImportedObject(string[] values)
        {
            try
                {
                    var cleanedValues = values.Select(cleanString).ToArray();

                    Type = cleanedValues[0].ToUpper();
                    Name = cleanedValues[1];
                    Schema = cleanedValues[2];
                    ParentName = cleanedValues[3];
                    ParentType = cleanedValues[4].ToUpper();
                    DataType = cleanedValues[5];
                    IsNullable = cleanedValues[6] == "1";
             
                }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        private string cleanString(string input)
        {
            return input.Trim().Replace(" ", "").Replace(Environment.NewLine, "");   
        }
    }
}