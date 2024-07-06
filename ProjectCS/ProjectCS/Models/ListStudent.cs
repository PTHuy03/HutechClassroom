using Microsoft.AspNetCore.Identity;

namespace ProjectCS.Models
{
    public class ListStudent
    {
        public string UserId { get; set; } = null!;

        public string ClassId { get; set; } = null!;

        public virtual Class Class { get; set; } = null!;

        public virtual ApplicationUser User { get; set; } = null!;
    }
}
