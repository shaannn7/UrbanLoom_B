using System.ComponentModel.DataAnnotations;

namespace UrbanLoom_B.Entity
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string CatagoryName { get; set; }
        public virtual List<Product> products {  get; set; }
    }
}
