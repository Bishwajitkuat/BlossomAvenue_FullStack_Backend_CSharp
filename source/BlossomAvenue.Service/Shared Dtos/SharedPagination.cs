using System.ComponentModel.DataAnnotations;

namespace BlossomAvenue.Service.Shared_Dtos
{
    public abstract class SharedPagination
    {
        public string? Search { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Least possible value for the page number should be 1")]
        public int PageNo { get; set; } = 1;
        [Range(1, int.MaxValue, ErrorMessage = "Least possible value for the page number should be 1")]
        public int PageSize { get; set; } = 10;
        public string OrderWith { get; set; } = String.Empty; // Hide the property in the derived class
        public OrderBy OrderBy { get; set; } = OrderBy.ASC;
    }
}
