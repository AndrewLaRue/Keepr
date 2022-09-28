using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetById(string id)
    {
      string sql = "SELECT * FROM accounts WHERE id = @id";
      return _db.QueryFirstOrDefault<Profile>(sql, new { id });
    }

    internal List<Keep> GetAllKeeps(string id)
    {
      string sql = @"
      SELECT 
        k.*,
        a.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        WHERE a.id = @id
      ";
      List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }, new { id }).ToList();
      return keeps;
    }

    internal List<Vault> GetAllVaults(string id)
    {
      string sql = @"
      SELECT 
        v.*,
        a.*
        FROM vaults v
        JOIN accounts a ON a.id = v.creatorId
        WHERE v.creatorId = @id
      ";
      List<Vault> vaults = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
      {
        vault.Creator = account;
        return vault;
      }, new { id }).ToList();
      return vaults;
    }
  }
}