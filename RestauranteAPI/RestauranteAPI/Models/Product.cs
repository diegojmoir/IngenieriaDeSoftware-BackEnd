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
        public string StartingTime { get; set; }
        public string EndingTime {get; set; }
        public int Category { get; set; }
        public string Image
        {
            get;set;
        }
        public bool IsAvailableNow()
        {
            if (!IsAvailable)
                return false;

            System.DateTime sartingTime = System.DateTime.ParseExact(this.StartingTime, "HH:mm", CultureInfo.InvariantCulture);
            System.DateTime endingTime = System.DateTime.ParseExact(this.EndingTime, "HH:mm", CultureInfo.InvariantCulture);
            System.DateTime now = System.DateTime.Now;

            if(now > endingTime || now < sartingTime)
            {
                return false;
            }

            return true;
            
        }
    }
}
