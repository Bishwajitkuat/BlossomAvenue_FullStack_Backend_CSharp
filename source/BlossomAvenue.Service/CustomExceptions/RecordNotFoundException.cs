using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.CustomExceptions
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException(string entityName) : base($"{entityName}, record not found")
        {
        }
    }
}
