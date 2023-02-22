using System;
using System.Collections.Generic;

#nullable disable

namespace PRN221_Secondhand.Models
{
    public partial class Admin
    {
        public byte[] Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }
    }
}
