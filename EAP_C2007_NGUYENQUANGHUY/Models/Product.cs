
using System.ComponentModel.DataAnnotations;

namespace EAP_C2007_NGUYENQUANGHUY.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        [StringLength(32,MinimumLength =3,ErrorMessage ="Product name length must be between 3 and 32")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public System.DateTime ReleaseDate { get; set; }
        [Required]
        [Range(1,100,ErrorMessage ="Quantity must be within a range from 1 to 10")]
        public int Quantity { get; set; }
        [Required]
        [RegularExpression("([1-9][0-9]*)")]
        public double Price { get; set; }
        public int CategoryId { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual Category Category { get; set; }
    }
}