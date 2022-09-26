using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    // SECTION Create a VaultKeep
    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      string sql = @"
                INSERT INTO vaultKeeps
        (vaultId, keepId, creatorId)
        VALUES
        ( @vaultId, @keepId, @creatorId);
        SELECT LAST_INSERT_ID();
        ";
      int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      newVaultKeep.Id = id;
      return newVaultKeep;
    }

    internal List<VaultKeep> GetVaultKeeps(int vaultId)
    {
      string sql = @"
      SELECT 
        vk.*,
        v.*
        FROM vaultKeeps vk
        JOIN vaults v ON v.id = vk.vaultId
      ";
      List<VaultKeep> vaultKeeps = _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vaultKeep, account) =>
      {
        vaultKeep.Creator = account;
        return vaultKeep;
      }).ToList();
      return vaultKeeps;
    }

    // SECTION Delete VaultKeep
    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vaultKeeps
      WHERE id = @id;
      ";
      _db.Execute(sql, new { id });
    }

    // SECTION get one
    internal VaultKeep GetOne(int id)
    {
      string sql = @"
      SELECT 
        vk.*,
        a.*
        FROM vaultKeeps vk
        JOIN accounts a ON a.id = vk.creatorId
        WHERE vk.id = @id
      ";
      return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vaultKeep, account) =>
      {
        vaultKeep.Creator = account;
        return vaultKeep;
      }, new { id }).FirstOrDefault();
    }
  }
}