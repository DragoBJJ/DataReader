namespace ConsoleApp
{
    using ConsoleApp.Container;
    internal class Program
    {
        static void Main(string[] args)
        {

            var DataContainer = new DataContainer("data.csv");

            DataContainer.GetAllChildren();
            DataContainer.GetAllDataBases();
            DataContainer.GetChildrenByKey("TABLE", "SalesPerson");

        }
    }
}
