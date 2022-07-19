using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EAP_C2007_NGUYENQUANGHUY.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {
            this.Products = new HashSet<Product>();
        }
    }
}