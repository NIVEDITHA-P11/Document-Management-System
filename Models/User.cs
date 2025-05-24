namespace DocumentManagementSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; }

        public List<Document> Documents { get; set; } = new();
    }
}
