using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp.Interface;

namespace ConsoleApp.classes
{
    internal class DataBuilder: IDataBuilder
    {

        private List<IChildrenSchema> children;
        private List<IDataBaseSchema> dataBases;

        private readonly List<string> _importedDataLines;

        private readonly Factory factory;
        public DataBuilder(List<string> ImportedDataLines) {

            _importedDataLines = ImportedDataLines;
            BuildDataObjects();
        }

        private string[] splitAndClearLine(string line)
        {
            return line.Split(';').Where(c => !string.IsNullOrEmpty(c)).ToArray();
        }

        private (string rowType, string[] rowValues) GetRawValues(int i)
        {
            var importedLine = _importedDataLines[i];

            var rowValues = splitAndClearLine(importedLine);
            var rowType = rowValues.Length >= 1 ? rowValues[0].ToUpper() : "";

            return (rowType, rowValues);
        }
        private void BuildDataObjects()
        {
            children = new List<IChildrenSchema>();
            dataBases = new List<IDataBaseSchema>();

            for (int i = 1; i < _importedDataLines.Count; i++)
            {
                
                var (rowType, rowValues) = GetRawValues(i);

                if (rowType == "DATABASE")
                    {
                        dataBases.Add(new DataBase(rowValues));
                    }
                else
                    {                  
                            var child = factory.CreateDataStructure(rowType, rowValues);
                            if (child is IChildrenSchema) children.Add(child);                    
                    }               
            }
        }

        public List<IChildrenSchema> GetChildrenData()
        {
            return children;
        }
        public List<IDataBaseSchema> GetDataBases()
        {
                return dataBases;
        }
    }
}
