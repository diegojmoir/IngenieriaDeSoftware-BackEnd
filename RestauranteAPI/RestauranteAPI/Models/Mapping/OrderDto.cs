using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteAPI.Models.Mapping
{
    public class OrderDto : BaseModel<string>
    {
        [Required]
        public Guid? ID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string Date { set; get; }

        [Required]
        public string Client { set; get; }

        [Required]
        public int Status { set; get; }

        [Required]
        [NotMapped]
        public Guid[] Products { get; set; }

        public bool HasValidDate()
        {
            return DateTime.TryParse(this.Date, out _);

        }
    }
}
