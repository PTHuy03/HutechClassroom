namespace ProjectCS.Models
{
    public class Loai
    {
        public string LoaiId { get; set; } = null!;

        public string LoaiName { get; set; } = null!;

        public virtual ICollection<Assign> Assigns { get; set; } = new List<Assign>();
    }
}
