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
        public void GetAllChildren()
        {
             Aggregator.GetAllChildrenData();    
        }
        public void GetChildrenByKey(string parentKey, string parentName) {
            {
                Aggregator.GetChildrenByKey(parentKey, parentName);
            } }

        public void GetAllDataBases()
        {
            Builder.GetDataBases();
        }
    }
}