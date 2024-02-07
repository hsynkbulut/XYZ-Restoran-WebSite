using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurantProject.Models
{
    public class SiteAuthors
    {

        public int AuthorId { get; set; }

        [Required]
        public string AuthorName { get; set; }
    }
}

