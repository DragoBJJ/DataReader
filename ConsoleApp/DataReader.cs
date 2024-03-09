namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DataReader
    {
        private List<ImportedObject> ImportedObjects;

        private List<string> ImportedDataLines;

        private List<string> ImportDataColumns;

        private Dictionary<string, int> ChildrenCountByParent;


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

                ImportedDataLines = ImportedDataLines.Skip(0).Take(200).ToList();
            }

        }

        private void readDataColumns()
        {
            ImportDataColumns = new List<string>(7);

            var firstLine = ImportedDataLines[0];
            var columns = firstLine.Split(';');

            foreach (var column in columns)
            {
                if(!string.IsNullOrEmpty(column))
                {
                    ImportDataColumns.Add(column);
                }
            }
               
        }
        private void buildDataObjects()
        {
            ImportedObjects = new List<ImportedObject>();
       
            for (int i = 1; i < ImportedDataLines.Count; i++)
            {
                var importedLine = ImportedDataLines[i];

                var rowValues = importedLine.Split(';').Where(line => !string.IsNullOrEmpty(line)).ToArray();


                if (rowValues.Count() == ImportDataColumns.Count())
                {
                    var ImportedObject = new ImportedObject(rowValues);
                    ImportedObjects.Add(ImportedObject);
                } 
            }
        }

        private void countTheChildren()
        {

            ChildrenCountByParent = new Dictionary<string, int>();

            foreach (var obj in ImportedObjects)
            {

                var key = $"{obj.ParentType}-{obj.ParentName}";

                if(ChildrenCountByParent.ContainsKey(key))
                {
                    ChildrenCountByParent[key] = ChildrenCountByParent[key] + 1;
                    obj.NumberOfChildren += ChildrenCountByParent[key];
                } else
                {
                    ChildrenCountByParent[key] = 1;
                    obj.NumberOfChildren = 1;
                }
            }
        }

        public DataReader(string filePath)
        {
            this.readDataFromStream(filePath);
            this.readDataColumns();
            this.buildDataObjects();
            this.countTheChildren();


            foreach (var obj in ChildrenCountByParent)
            {
                Console.WriteLine(obj.ToString());
            }


        }

       /* public void ImportAndPrintData(string fileToImport)

            foreach (var database in ImportedObjects)
            {
                if (database.Type == "DATABASE")
                {
                    Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");

                    // print all database's tables
                    foreach (var table in ImportedObjects)
                    {
                        if (table.ParentType.ToUpper() == database.Type)
                        {
                            if (table.ParentName == database.Name)
                            {
                                Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");

                                // print all table's columns
                                foreach (var column in ImportedObjects)
                                {
                                    if (column.ParentType.ToUpper() == table.Type)
                                    {
                                        if (column.ParentName == table.Name)
                                        {
                                            Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.ReadLine();
        }*/
    }

}