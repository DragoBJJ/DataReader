using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Interface
{
    internal interface IDataSchema
    {
         string Name { get; set; }

         string Type { get; set; }

         string Schema { get; set; }
         string ParentName { get; set; }
         string ParentType { get; set; }
         string DataType { get; set; }
         bool IsNullable { get; set; }
         int NumberOfChildren {  get; set; }
    }
}
