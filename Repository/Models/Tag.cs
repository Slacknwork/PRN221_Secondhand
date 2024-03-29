﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Tag
    {
        public Tag()
        {
            TagPosts = new HashSet<TagPost>();
        }

        public string Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<TagPost> TagPosts { get; set; }
    }
}
