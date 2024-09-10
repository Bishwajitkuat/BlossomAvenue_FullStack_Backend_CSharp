using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.CustomExceptions
{
    public class ProductOutOfStockException : Exception
    {
        public ProductOutOfStockException(string variationName, int currentInventory) : base($"Sorry! The product is out of stock.The current inventory of {variationName} is {currentInventory} pcs.") { }
    }
}