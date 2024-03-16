using System.ComponentModel.DataAnnotations;

namespace Back_End.Models.BugModes
{
    public class CreateBugModel
    {
        [StringLength(200)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }

        public string Status { get; set; }
    }
}
