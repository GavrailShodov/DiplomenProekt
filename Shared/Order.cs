using System;
using System.Collections.Generic;
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
        public DateTime ReserveDate { get; set; }

        public int ProductId { get; set; }
    }
}
