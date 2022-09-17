using System;
using System.Collections.Generic;

namespace BuzzBuzzHomeAPI.Models
{
    public partial class tblProduct
    {
        public int ProductId { get; set; }
        public int? CustomerId { get; set; }
        public string ProductName { get; set; } = null!;
        public double? ProductPrice { get; set; }
        public bool Deleted { get; set; }
    }
}
