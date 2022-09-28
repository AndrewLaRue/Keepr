using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vaultsRepo;

    public VaultsService(VaultsRepository vaultsRepo)
    {
      _vaultsRepo = vaultsRepo;
    }

    // SECTION Create Vault
    internal Vault Create(Vault newVault)
    {
      return _vaultsRepo.Create(newVault);
    }


    // SECTION Get Vault by id
    internal Vault GetOne(int id)
    {
      Vault vault = _vaultsRepo.GetOne(id);
      if (vault == null)
      {
        throw new Exception("No vault found with that id.");
      }
      return vault;
    }


    // SECTION Get Vault by id with user
    internal Vault GetOne(int id, string userId)
    {
      Vault vault = _vaultsRepo.GetOne(id);
      if (vault == null)
      {
        throw new Exception("No vault found with that id.");
      }
      if ((bool)vault.isPrivate && vault.CreatorId != userId)
      {
        throw new Exception("Your not allowed to see that.");
      }
      return vault;
    }

    // SECTION Update(edit) Vault
    internal Vault Update(Vault update)
    {
      Vault original = GetOne(update.Id, update.CreatorId);
      if (original.CreatorId != update.CreatorId)
      {
        throw new Exception("Your not allowed to modify that.");
      }
      original.Name = update.Name ?? original.Name;
      original.Description = update.Description ?? original.Description;
      original.isPrivate = update.isPrivate == false ? update.isPrivate : original.isPrivate;
      original.Img = update.Img ?? original.Img;
      return _vaultsRepo.Update(original);
    }


    // SECTION get my vaults
    internal List<Vault> GetMyVaults(string id)
    {
      return _vaultsRepo.GetMyVaults(id);
    }

    // SECTION Delete Vault
    internal string Delete(int id, string userId)
    {
      Vault vault = GetOne(id, userId);
      if (vault.CreatorId != userId)
      {
        throw new Exception("Your not allowed to delete that.");
      }

      _vaultsRepo.Delete(id);
      return $"{vault.Name} has been deleted.";
    }
  }
}