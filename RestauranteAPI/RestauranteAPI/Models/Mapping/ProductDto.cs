using System.ComponentModel.DataAnnotations;

namespace RestauranteAPI.Models.Dto
{
    public class ProductDto : BaseModel<string>
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        public string StartingTime { get; set; }
        public string EndingTime { get; set; }
        public int Category { get; set; }

    }
}
