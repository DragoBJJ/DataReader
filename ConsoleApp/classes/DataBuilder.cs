
using System.Collections.Generic;
using System.Linq;
using ConsoleApp.Interface;

namespace ConsoleApp.classes
{
    internal class DataBuilder: IBuilderData
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
            var dataHeaders = new List<string>(7);

            var firstLine = _importedDataLines[0];
            var columns = splitAndClearLine(firstLine);

            foreach (var column in columns)
            {
                dataHeaders.Add(column);
            }

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

                if (rowValues.Count() == dataHeaders.Count())
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
