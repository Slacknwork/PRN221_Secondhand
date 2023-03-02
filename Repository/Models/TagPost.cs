using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class TagPost
    {
        public string Id { get; set; }
        public string PostId { get; set; }
        public string TagId { get; set; }

        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
