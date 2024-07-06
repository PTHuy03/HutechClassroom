using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjectCS.Models
{
    public class ListAssign
    {
        public string UserId { get; set; } = null!;

        public string AssignId { get; set; } = null!;

        public string LoaiId { get; set; } = null!;

        public decimal? Point {  get; set; }

        public virtual Assign Assign { get; set; } = null!;

        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<ListFile> ListFiles { get; set; } = new List<ListFile>();
    }
}
