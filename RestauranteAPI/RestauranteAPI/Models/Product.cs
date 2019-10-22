using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace RestauranteAPI.Models
{
    public class Product
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        public int[] Categories { get; set; }
        public string Image { get; set; }
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
        [DataType(DataType.Date)]
        public string StartingDate { get; set; }
        [DataType(DataType.Date)]
        public string EndingDate { get; set; }
        public bool HasValidDate()
        {
            return DateTime.TryParse(this.StartingDate, out _) && DateTime.TryParse(EndingDate, out _);

        }

    }
}
