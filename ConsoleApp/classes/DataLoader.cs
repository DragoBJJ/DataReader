using System;
using System.Collections.Generic;
using System.IO;
using ConsoleApp.Interface;


namespace ConsoleApp.classes
{
    internal class DataLoader : IDataLoader
    {

        private List<string> ImportedDataLines;
        public DataLoader(string filePath)
        {

            ReadDataFromStream(filePath);
        }
          private void ReadDataFromStream(string filePath)
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
         public List<string> GetLoaderData()
        {
            return ImportedDataLines;
        }

   
    }
}
