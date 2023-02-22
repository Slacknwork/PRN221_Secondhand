using System;
using System.Collections.Generic;

#nullable disable

namespace PRN221_Secondhand.Models
{
    public partial class Wish
    {
        public byte[] Id { get; set; }
        public byte[] UserId { get; set; }
        public byte[] PostId { get; set; }
        public DateTime? Created { get; set; }
        public int? Status { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
