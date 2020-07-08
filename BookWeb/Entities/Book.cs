using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookWeb.Entities
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id  { get; set; }
        public string  Title { get; set; }
        public int AuthorId { get; set; }
        public int GerneID { get; set; }
        public string ISBN { get; set; }
        public int YearPublish { get; set; }
        public int Rating { get; set; }
        public string  Summary { get; set; }
        public string CreatedBy { get; set; }
        
        private DateTime? dateCreated = null;
        public DateTime DateCreated
        {
            get
            {
                return dateCreated.HasValue
                   ? dateCreated.Value
                   : DateTime.Now;
            }

            set { dateCreated = value; }

        }
    }
}
