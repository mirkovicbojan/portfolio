using System.ComponentModel.DataAnnotations;

namespace TimeSheet.Models
{
    public class Category
    {
        [Key]
        public int categoryID { get; set; }
        public string? categoryName { get; set; }
        public string? categoryDescription { get; set; }
    }
}