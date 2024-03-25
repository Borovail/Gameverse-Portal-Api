using System.ComponentModel.DataAnnotations;

namespace Back_End.Models.BugModes
{
    public class CreateBugModel
    {
        [StringLength(200)]
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? ImageUrl { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
