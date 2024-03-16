using System.Collections.Generic;

namespace ConsoleApp.Interface
{
    internal interface IDataBuilder
    {
         abstract List<IChildrenSchema> GetChildrenData();
        abstract List<IDataBaseSchema> GetDataBases();
    }
}
