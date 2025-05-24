namespace DocumentManagementSystem.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public int Revision { get; set; }
        public byte[] Content { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
