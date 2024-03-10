﻿namespace ConsoleApp
{
    using System;
    using ConsoleApp.Enum;

    internal class Program
    {
        static void Main(string[] args)
        {
            var reader = new DataReader("data.csv");

            reader.getDataByKey(DataReaderKey.COLUMNS);
            reader.getDataByKey(DataReaderKey.TABLES);

        }
    }
}
