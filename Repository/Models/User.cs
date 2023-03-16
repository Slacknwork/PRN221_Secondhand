using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class User
    {
        public User()
        {
            Wishes = new HashSet<Wish>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ExternalLoginId { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Wish> Wishes { get; set; }
    }
}
