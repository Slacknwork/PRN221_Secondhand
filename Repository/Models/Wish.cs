﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Wish
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
        public DateTime? Created { get; set; }
        public int? Status { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
