using System;
using System.Collections.Generic;

#nullable disable

namespace PRN221_Secondhand.Models
{
    public partial class TagPost
    {
        public byte[] Id { get; set; }
        public byte[] PostId { get; set; }
        public byte[] TagId { get; set; }

        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
