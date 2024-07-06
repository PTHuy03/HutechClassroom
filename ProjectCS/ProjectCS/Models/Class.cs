namespace ProjectCS.Models
{
    public class Class
    {
        public string ClassId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Titlle { get; set; }

        public string? Topic { get; set; }

        public string? Room { get; set; }

        public string? Image { get; set; }

        public virtual ICollection<Assign> Assigns { get; set; } = new List<Assign>();

        public virtual ICollection<ListStudent> ListStudents { get; set; } = new List<ListStudent>();
    }
}
