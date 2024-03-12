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

            DataContainer.GetDataByKey(DataKey.TABLES);
            DataContainer.GetDataByKey(DataKey.COLUMNS);
        }
    }
}
