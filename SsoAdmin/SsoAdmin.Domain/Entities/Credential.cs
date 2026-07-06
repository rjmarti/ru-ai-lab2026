namespace SsoAdmin.Domain.Entities;

public class Credential
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Emisor { get; set; } = string.Empty;
}
