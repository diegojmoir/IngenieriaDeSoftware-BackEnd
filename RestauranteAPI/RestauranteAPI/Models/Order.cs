using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RestauranteAPI.Models
{
    public class Order
    {
        public string Key
        {
            get { return this.Key; }
            set
            {
                this.Key = Guid.NewGuid().ToString();
            }
        }

        [Required]
        [DataType(DataType.Date)]
        public string Date { set; get; }

        [Required]
        public string Client { set; get; }

        [Required]
        public string Status { set; get; }

        [Required]
        public Collection<Product> ProductsOrdered { get; set; }
        public bool HasValidDate()
        {
            return DateTime.TryParse(this.Date, out _);

        }
    }
}
