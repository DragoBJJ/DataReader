using ConsoleApp.classes;
using ConsoleApp.Interface;

namespace ConsoleApp.Container
{
    internal class DataContainer
    {
        private readonly IDataLoader Loader;
        private readonly IDataBuilder Builder;
        private readonly IDataAggregator Aggregator;
        public DataContainer(string filePath)
        {
             Loader = new DataLoader(filePath);

             Builder = new DataBuilder(Loader.GetLoaderData());
          
            Aggregator = new DataAggregator(Builder.GetChildrenData());
        }

        public void GetAllChildrenData()
        {
             Aggregator.GetAllChildrenData();    
        }

        public void GetDataBases()
        {
            Builder.GetDataBases();
        }
    }
}