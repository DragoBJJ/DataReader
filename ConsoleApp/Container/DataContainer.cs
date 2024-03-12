﻿using System;
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
        private readonly IDataAggregator Aggregator;
        public DataContainer(string filePath)
        {
             Loader = new DataLoader(filePath);

            Builder = new DataBuilder(Loader.GetLoaderData());
           
            Aggregator = new DataAggregator(Builder.GetBuilderData());
        }

        public Dictionary<string,BuilderObject> GetDataByKey(DataKey key)
        {
            return Aggregator.GetDataByKey(key);    
        }
    }
}