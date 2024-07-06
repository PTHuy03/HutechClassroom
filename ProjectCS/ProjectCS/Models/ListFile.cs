namespace ProjectCS.Models
{
    public class ListFile
    {
        public string UserId { get; set; } = null!;

        public string AssignId { get; set; } = null!;

        public string LoaiId { get; set; } = null!;

        public string FileId { get; set; } = null!;

        public string FileName { get; set; } = null!;

        public string? FilePath { get; set; }

        public DateTime? SubmissTime { get; set; } = DateTime.Now;

        public virtual ListAssign ListAssigns { get; set; } = null!;
    }
}
