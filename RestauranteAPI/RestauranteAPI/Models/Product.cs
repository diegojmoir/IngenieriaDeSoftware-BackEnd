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
        public double Price { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        [NotMapped]
        public int?[] Categories { get; set; }
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
        [JsonIgnore]
        [NotMapped]
        public ICollection<ProductCategory>ProductCategories { get; set; }
    }
}
