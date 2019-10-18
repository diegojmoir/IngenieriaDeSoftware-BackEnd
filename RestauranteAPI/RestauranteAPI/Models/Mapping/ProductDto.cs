using System;
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
        [Required]
        public int Category { get; set; }
        public string Image { get; set; }

        [DataType(DataType.Date)]
        public string StartingDate { get; set; }
        [DataType(DataType.Date)]
        public string EndingDate { get; set; }



        public bool IsAvailableNow()
        {
            if (!IsAvailable)
                return false;

            System.DateTime startingDate = Convert.ToDateTime(StartingDate);
            System.DateTime endingDate = Convert.ToDateTime(EndingDate);
            System.DateTime now = System.DateTime.Now;

            if (now > endingDate || now < startingDate)
            {
                return false;
            }

            return true;

        }

    }
}
