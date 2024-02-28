using System.ComponentModel.DataAnnotations;

namespace UrbanLoom_B.Dto.WhishListDto
{
    public class WhishListViewDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string CatagoryName { get; set; }
        public decimal Price { get; set; }
        public string ProductImage { get; set; }
    }
}
