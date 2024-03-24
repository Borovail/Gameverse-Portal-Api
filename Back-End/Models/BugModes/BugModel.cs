using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Back_End.Models.BugModes
{
    public class BugModel
    {

        public string Id { get; private set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? ImageUrl { get; set; }

        public string Status { get; set; }
        public DateTime CreatedAt { get; private set; }

        public string UserId { get; set; }
        public IdentityUser User { get; private set; }
    }
}
