using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }


    // SECTION Create Keep
    public Keep Create(Keep newKeep)
    {
      string sql = @"
                INSERT INTO keeps
        (name, description, img, creatorId)
        VALUES
        (@name, @description, @img, @creatorId);
        SELECT LAST_INSERT_ID();
        ";
      int id = _db.ExecuteScalar<int>(sql, newKeep);
      newKeep.Id = id;
      return newKeep;
    }

    // SECTION Get all Keeps
    internal List<Keep> GetAll()
    {
      string sql = @"
      SELECT 
        k.*,
        a.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
      ";
      List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, account) =>
      {
        keep.Creator = account;
        return keep;
      }).ToList();
      return keeps;
    }

    // SECTION Get one by id
    internal Keep GetOne(int id)
    {
      string sql = @"
      SELECT 
        k.*,
        a.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        WHERE k.id = @id
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, account) =>
      {
        keep.Creator = account;
        return keep;
      }, new { id }).FirstOrDefault();
    }

    // SECTION update(edit) Keep
    internal Keep Update(Keep original)
    {
      string sql = @"
      UPDATE keeps SET
      name = @name,
      img = @img,
      description = @description
      WHERE id = @id
      ";
      _db.Execute(sql, original);
      return original;
    }

    // SECTION Delete Keep
    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM keeps
      WHERE id = @id;
      ";
      _db.Execute(sql, new { id });
    }

  }
}