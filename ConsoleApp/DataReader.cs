namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.IO;
    using System.Linq;
    using System.Reflection.Emit;

    public class DataReader
    {

        private List<ImportedObject> ImportedObjects;

        private List<string> ImportedDataLines;
        private List<string> ImportDataColumns;

        private Dictionary<string, ImportedObject> AgregatedTables;
        private Dictionary<string, ImportedObject> AgregatedColumns;


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
            return  line.Split(';').Where(c => !string.IsNullOrEmpty(c)).ToArray();
        }

        private void readImportDataColumns()
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

        private void agregateTables()
        {

            AgregatedTables = new Dictionary<string, ImportedObject>();

            foreach (var obj in ImportedObjects)
            {

                var key = $"{obj.ParentType}-{obj.ParentName}";
           

                if(AgregatedTables.ContainsKey(key))
                {
                    var oldObj = AgregatedTables[key];
                    oldObj.NumberOfChildren++;

                } else
                {
                    obj.NumberOfChildren = 1;
                    AgregatedTables[key] = obj;
                 
                }
            }
        }
        private void agregateColumns()
        {
            AgregatedColumns = new Dictionary<string, ImportedObject>();

            foreach (var obj in ImportedObjects)
            {

                var key = $"{obj.Type}-{obj.Name}";

                if(!AgregatedColumns.ContainsKey(key))
                {
                    AgregatedColumns[key] = obj;    
                }
            }

        }

        public DataReader(string filePath)
        {
            this.readDataFromStream(filePath);
            this.readImportDataColumns();

            this.buildDataObjects();

            this.agregateTables();
            this.agregateColumns();


            foreach (var obj in AgregatedTables)
            {
                var table = obj.Value;
                Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");
            }
            Console.WriteLine($"---------------------------------------------------------------------------------------------------------------");
            foreach (var obj in AgregatedColumns)
            {
                var column = obj.Value;
                            Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable ? "accepts nulls" : "with no nulls")}");
                        }
        }
    }

}