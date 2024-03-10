namespace ConsoleApp
{
    using System;
    using ConsoleApp.classes;
    using ConsoleApp.Enum;

    internal class Program
    {
        static void Main(string[] args)
        {
            var loader = new DataLoader("data.csv");
            var data = loader.GetLoaderData();

            var builder = new DataBuilder(data);

            var preparedData = builder.GetBuilderData();

            var reader = new DataReader(preparedData);

            reader.GetDataByKey(DataReaderKey.COLUMNS);
            reader.GetDataByKey(DataReaderKey.TABLES);
        }
    }
}
