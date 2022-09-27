using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _keepsRepo;

    public KeepsService(KeepsRepository keepsRepo)
    {
      _keepsRepo = keepsRepo;
    }

    // SECTION Create Keep
    internal Keep Create(Keep newKeep)
    {
      return _keepsRepo.Create(newKeep);
    }


    // SECTION Get All Keeps
    internal List<Keep> GetAll()
    {
      return _keepsRepo.GetAll();
    }

    // SECTION Get One by id
    internal Keep GetOne(int id)
    {
      Keep keep = _keepsRepo.GetOne(id);
      if (keep == null)
      {
        throw new Exception("No keep found with that id.");
      }
      keep.Views++;
      _keepsRepo.Update(keep);
      return keep;
    }



    // SECTION Update(edit) Keep
    internal Keep Update(Keep update, string userId)
    {
      Keep original = GetOne(update.Id);
      if (original.CreatorId != userId)
      {
        throw new Exception("Your not allowed to modify that.");
      }
      original.Name = update.Name ?? original.Name;
      original.Description = update.Description ?? original.Description;
      original.Img = update.Img ?? original.Img;
      return _keepsRepo.Update(original);
    }


    // SECTION Delete Keep
    internal string Delete(int id, string userId)
    {
      Keep keep = GetOne(id);
      if (keep.CreatorId != userId)
      {
        throw new Exception("Your not allowed to delete that.");
      }

      _keepsRepo.Delete(id);
      return $"{keep.Name} has been deleted.";
    }
  }
}