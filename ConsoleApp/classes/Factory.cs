using System;
using ConsoleApp.classes;
using ConsoleApp.Interface;

namespace ConsoleApp
{
    internal struct Factory
    {
        internal readonly IChildrenSchema CreateDataStructure(string type, string[] values)
        {

            return type.ToUpper() switch
            {
                "TABLE" => new Table(values),
                "COLUMN" => new Column(values),
                _ => null,
            };
        }

    }
}