using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteAPI.Models.Mapping
{
    public class OrderDto : BaseModel<string>
    {
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
