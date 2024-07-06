using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectCS.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = null!;

        public string? AvatarPath { get; set; }

        [NotMapped]
        public IFormFile? Avatar { get; set; }
        public Boolean IsPassword { get; set; } = true;

        public virtual ICollection<ListStudent> ListStudents { get; set; } = new List<ListStudent>();

        public virtual ICollection<ListAssign> ListAssigns { get; set; } = new List<ListAssign>();

        
        protected void onCreate()
        {
            if (this.IsPassword == null)
            {
                this.IsPassword = true;
            }
        }
    }
}
