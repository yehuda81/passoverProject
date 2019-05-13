using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace passoverProject2
{
    interface ICustomer
    {
        string user { get; set; }
        int pass { get; set; }
        string firstName { get; set; }
        string lastName { get; set; }
        int creditCard { get; set; }
    }
       
}
