namespace ConsoleApp
{
    using System;
    using System.Runtime.InteropServices;
    using ConsoleApp.classes;
    using ConsoleApp.Container;
    using ConsoleApp.Enum;



    internal class Program
    {
        static void Main(string[] args)
        {

            var DataContainer = new DataContainer("data.csv");

            var tables = DataContainer.getDataByKey(DataKey.TABLES);
            var columns = DataContainer.getDataByKey(DataKey.COLUMNS);

            Console.WriteLine($"Amount of {DataKey.TABLES.ToString()} Data: {tables.Count}");
            Console.WriteLine($"Amount of {DataKey.COLUMNS.ToString()} Data: {columns.Count}");
        }
    }
}
