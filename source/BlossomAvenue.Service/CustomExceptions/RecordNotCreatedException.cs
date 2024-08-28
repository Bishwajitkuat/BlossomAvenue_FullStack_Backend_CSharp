using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.CustomExceptions
{
    public class RecordNotCreatedException : Exception
    {
        public RecordNotCreatedException(string entityName) : base($" The {entityName}, record not be created because of having duplicate id(s). Please try again!")
        {
        }
    }
}