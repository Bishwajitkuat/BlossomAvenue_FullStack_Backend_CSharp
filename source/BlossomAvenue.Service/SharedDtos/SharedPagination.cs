using System.ComponentModel.DataAnnotations;

namespace BlossomAvenue.Service.SharedDtos
{
    public abstract class SharedPagination
    {
        public string? Search { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Least possible value for the page number should be 1")]
        public int PageNo { get; set; } = 1;
        [Range(1, int.MaxValue, ErrorMessage = "Least possible value for the page number should be 1")]
        public int PageSize { get; set; } = 10;
        public OrderBy OrderBy { get; set; } = OrderBy.ASC;
    }
}
