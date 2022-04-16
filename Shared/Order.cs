using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAplicationForServices.Shared
{
    public class Order
    {
        public int Id { get; set; }

        public string Name { get; set; }    

        public string? Email { get; set; }

        public string? Phone { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime ReserveDate { get; set; }

        public int Hour { get; set; }

        public int ProductId { get; set; }
    }
}
