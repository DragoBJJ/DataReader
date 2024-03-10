### Code Review

### Performance issues and design patterns
- ImportAndPrintData - Function breaks the principle of single responsibility. i would separate the logic into separate functions first.
- new ImportedObject() - I would enclose the logic of creating a new object and assigning values in a separate class.
- clear and correct imported data - I would write the cleaning logic once in a separate function and call it if needed.
- assign number of children - loop within a loop is not the most efficient solution. I would use a dictionary. which accepts union keys and, in the case of repetition, increments the number of children.
- print all database's tables || print all table's columns - Nested loop problem again. I would use the previously created dictionary to read the values.
- class ImportedObject : ImportedObjectBaseClass - I think the ImportedObject class can implement its own interface instead of the base class

### Problems related to the correct operation of the application and syntax
- public string IsNullable { get; set; } - The value should be of type bool
-  for (int i = 0; i <= importedLines.Count; i++) - The first line is "data type names," not values. We should spell from: 1. It is best to create a separate function which will contain: "data type names".
-  var values = importedLine.Split(';') - We separate the rows but do not clear them or check whether the length of the row coincides with the length of the required data.
-  public double NumberOfChildren - double :) ?
-  reader.ImportAndPrintData("dataa.csv") - Typo - data.csv.
- Console.ReadLine() - Seems redundant to me.
