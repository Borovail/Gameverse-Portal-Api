using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Back_End.Models.BugModes
{
    public class BugModel
    {

        public string Id { get; private set; } = Guid.NewGuid().ToString();

        [StringLength(200)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }

        public string Status { get; set; }
        public DateTime CreatedAt { get; private set; }


        //uncomment  when  authentication is implemented
        //public string UserId { get;  set; }
        //public IdentityUser User { get;private set; }
    }
}
