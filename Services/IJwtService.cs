namespace DocumentManagementSystem.Services
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string username);
    }
}
