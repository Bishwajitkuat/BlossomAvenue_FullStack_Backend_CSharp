using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.SharedDtos
{
    public class PaginatedResponse<T>
    {
        public int ItemPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPageCount { get; set; }

        public int TotalItemCount { get; set; }

        public ICollection<T> Items { get; set; }

        public PaginatedResponse(ICollection<T> items, int itemPerPage, int currentPage, int totalItemCount)
        {
            Items = items;
            TotalItemCount = totalItemCount;
            CurrentPage = currentPage;
            ItemPerPage = itemPerPage;
            TotalPageCount = (int)Math.Ceiling((double)totalItemCount / itemPerPage);
        }
    }
}