namespace ProjectCS.Models
{
    public class Assign
    {
        public string AssignId { get; set; } = null!;

        public string AssignName { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime Posttime { get; set; }

        public string? AssignFile1 { get; set; }

        public string? AssignFile2 { get; set; }

        public string ClassId { get; set; } = null!;

        public string LoaiId { get; set; } = null!;

        public virtual Loai Loai { get; set; } = null!;

        public virtual Class Class { get; set; } = null!;

        public virtual ICollection<ListAssign> ListAssigns { get; set; } = new List<ListAssign>();
    }
}
