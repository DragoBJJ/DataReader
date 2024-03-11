using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.classes;
using ConsoleApp.Enum;
using ConsoleApp.Interface;

namespace ConsoleApp.Container
{
    internal class DataContainer
    {
        private readonly IDataLoader Loader;
        private readonly IDataBuilder Builder;
        private IDataReader Reader;
        public DataContainer(string filePath)
        {
             Loader = new DataLoader(filePath);
             Builder = new DataBuilder(Loader.GetLoaderData());
             Reader = new DataReader(Builder.GetBuilderData());
        }

        public Dictionary<string,BuilderObject> getDataByKey(DataKey key)
        {
            return Reader.GetDataByKey(key);    
        }
    }
}