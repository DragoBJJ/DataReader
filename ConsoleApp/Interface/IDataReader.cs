﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Enum;

namespace ConsoleApp.Interface
{
    internal interface IDataReader
    {
         Dictionary<string, BuilderObject> GetDataByKey(DataReaderKey key);
    }
}