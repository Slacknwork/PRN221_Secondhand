using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Category
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
