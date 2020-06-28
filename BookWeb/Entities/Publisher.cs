using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookWeb.Entities
{
    public class Publisher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PublisherName { get; set; }
        public string Country { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
