namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
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
