using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.CustomExceptions
{
    public class RecordNotUpdatedException : Exception
    {
        public RecordNotUpdatedException(string entityName) : base($" The {entityName}, record not be updated because of having duplicate id(s) or missing id(s). Please try again!")
        {
        }
    }
}