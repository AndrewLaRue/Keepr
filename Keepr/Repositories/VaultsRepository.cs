using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }


    // SECTION Create Vault 
    internal Vault Create(Vault newVault)
    {
      string sql = @"
              INSERT INTO vaults
                (name, description, img, isPrivate, creatorId)
              VALUES
                (@name, @description, @img, @isPrivate, @creatorId);
              SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, newVault);
      newVault.Id = id;
      return newVault;
    }

    internal List<Vault> GetMyVaults(string id)
    {
      string sql = @"
      SELECT 
        v.*
        FROM vaults v
        WHERE v.creatorId = @id
      ";
      List<Vault> vaults = _db.Query<Vault>(sql, new { id }).ToList();
      return vaults;
    }

    // SECTION Get One by id
    internal Vault GetOne(int id)
    {
      string sql = @"
      SELECT 
        v.*,
        a.*
        FROM vaults v
        JOIN accounts a ON a.id = v.creatorId
        WHERE v.id = @id
      ";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, account) =>
      {
        vault.Creator = account;
        return vault;
      }, new { id }).FirstOrDefault();
    }

    // SECTION update(edit) vault

    internal Vault Update(Vault original)
    {
      string sql = @"
      UPDATE vaults SET
      name = @name,
      img = @img,
      description = @description,
      isPrivate = @isPrivate
      WHERE id = @id
      ";
      _db.Execute(sql, original);
      return original;
    }



    // SECTION Delete Vault
    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vaults
      WHERE id = @id;
      ";
      _db.Execute(sql, new { id });
    }
  }
}