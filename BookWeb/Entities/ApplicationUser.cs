using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookWeb.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {

                return this.FirstName + " " + this.LastName;
            }
        }

    }
}
