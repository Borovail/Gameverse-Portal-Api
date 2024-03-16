using System.ComponentModel.DataAnnotations;

namespace Back_End.Models.BugModes
{
    public class UpdateBugModel
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Priority { get; set; }
        public string? Status { get; set; }
    }
}
