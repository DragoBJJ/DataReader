
using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp.Interface;

namespace ConsoleApp.classes
{
    internal class DataBuilder: IDataBuilder
    {

        private List<BuilderObject> builderObjects;
        private readonly List<string> _importedDataLines;

        public DataBuilder(List<string> ImportedDataLines) {

            _importedDataLines = ImportedDataLines;
            buildDataObjects();
        }

        private string[] splitAndClearLine(string line)
        {
            return line.Split(';').Where(c => !string.IsNullOrEmpty(c)).ToArray();
        }
        private string[] readDataHeaders()
        { 
            var firstLine = _importedDataLines[0];
            var dataHeaders = splitAndClearLine(firstLine);
            return dataHeaders.ToArray();    
        }

        private void buildDataObjects()
        {
            builderObjects = new List<BuilderObject>();

            for (int i = 1; i < _importedDataLines.Count; i++)
            {
                var importedLine = _importedDataLines[i];

                var rowValues = splitAndClearLine(importedLine);

                var dataHeaders = readDataHeaders();

                var rowLength = rowValues.Count();

        
                if (rowLength == 6 || rowLength == dataHeaders.Length)
                {
                    var builderObject = new BuilderObject(rowValues);
                    builderObjects.Add(builderObject);
                }
            }
        }

        public List<BuilderObject> GetBuilderData()
        {
            return builderObjects;
        }
    }
}
