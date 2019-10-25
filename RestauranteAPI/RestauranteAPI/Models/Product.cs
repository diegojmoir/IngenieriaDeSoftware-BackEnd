using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace RestauranteAPI.Models
{
    public class Product
    {
        [JsonIgnore]
        public Guid? ID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        [NotMapped]
        public int?[] Categories { get; set; }
        public string Image { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartingDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndingDate { get; set; }
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

        public bool HasValidDate()
        {
            return DateTime.TryParse(this.StartingDate.ToLongDateString(), out _) && DateTime.TryParse(EndingDate.ToLongDateString(), out _);

        }
        [JsonIgnore]
        [NotMapped]
        public ICollection<ProductCategory>ProductCategories { get; set; }
    }
}
