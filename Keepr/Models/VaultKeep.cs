namespace Keepr.Models
{
  public class VaultKeep
  {
    public int Id { get; set; }
    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
    public int VaultId { get; set; }
    public int KeepId { get; set; }
    public string CreatorId { get; set; }

    public Profile Creator { get; set; }
  }
}