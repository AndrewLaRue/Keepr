using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vaultKeepsRepo;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo)
    {
      _vaultKeepsRepo = vaultKeepsRepo;
    }

    // SECTION Create VaultKeep
    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      return _vaultKeepsRepo.Create(newVaultKeep);
    }


    // SECTION Get VaultKeeps
    internal List<CollectedKeepVM> GetVaultKeeps(int vaultId)
    {
      return _vaultKeepsRepo.GetVaultKeeps(vaultId);
    }


    // SECTION Delete VaultKeep
    internal string Delete(int id, string userId)
    {
      VaultKeep vaultKeep = GetOne(id);
      if (vaultKeep.CreatorId != userId)
      {
        throw new Exception("Your not allowed to delete that.");
      }

      _vaultKeepsRepo.Delete(id);
      return "VaultKeep has been deleted.";
    }

    internal VaultKeep GetOne(int id)
    {
      VaultKeep vaultKeep = _vaultKeepsRepo.GetOne(id);
      if (vaultKeep == null)
      {
        throw new Exception("No keep found with that id.");
      }
      return vaultKeep;
    }
  }
}