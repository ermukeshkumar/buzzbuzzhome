using System;
using System.Collections.Generic;

namespace BuzzBuzzHomeAPI.Models
{
    public partial class tblCustomer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string Address1 { get; set; } = null!;
        public string? Address2 { get; set; }
        public string Industry { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Title { get; set; }
        public string? PhoneNumber { get; set; }
        public bool Deleted { get; set; }
        public DateTime AddDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}
