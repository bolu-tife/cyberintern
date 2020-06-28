using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookWeb.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Location { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
