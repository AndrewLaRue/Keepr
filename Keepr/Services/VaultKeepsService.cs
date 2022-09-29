using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vaultKeepsRepo;
    private readonly VaultsRepository _vaultsRepo;
    private readonly KeepsRepository _keepsRepo;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo, VaultsRepository vaultsRepo, KeepsRepository keepsRepo)
    {
      _vaultKeepsRepo = vaultKeepsRepo;
      _vaultsRepo = vaultsRepo;
      _keepsRepo = keepsRepo;
    }





    // SECTION Create VaultKeep
    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      Vault vault = _vaultsRepo.GetOne(newVaultKeep.VaultId);
      Keep keep = _keepsRepo.GetOne(newVaultKeep.KeepId);
      if (newVaultKeep.CreatorId != vault.CreatorId)
      {
        throw new Exception("Your not allowed to make that.");
      }
      keep.Kept++;
      _keepsRepo.Update(keep);
      return _vaultKeepsRepo.Create(newVaultKeep);
    }

    // SECTION Delete VaultKeep
    internal string Delete(int id, string userId)
    {
      VaultKeep vaultKeep = GetOne(id);
      Keep keep = _keepsRepo.GetOne(vaultKeep.KeepId);
      Vault vault = _vaultsRepo.GetOne(vaultKeep.VaultId);
      if (vault.CreatorId != userId)
      {
        throw new Exception("Your not allowed to delete that.");
      }

      keep.Kept--;
      _keepsRepo.Update(keep);
      _vaultKeepsRepo.Delete(id);
      return "VaultKeep has been deleted.";
    }
    // SECTION Get VaultKeeps
    internal List<CollectedKeepVM> GetVaultKeeps(int vaultId, string userId)
    {
      Vault vault = _vaultsRepo.GetOne(vaultId);
      if (vault.CreatorId != userId && vault.isPrivate)
      {
        throw new Exception("Your not allowed to see that.");
      }
      return _vaultKeepsRepo.GetVaultKeeps(vaultId);
    }


    internal VaultKeep GetOne(int id)
    {
      VaultKeep vaultKeep = _vaultKeepsRepo.GetOne(id);
      if (vaultKeep == null)
      {
        throw new Exception("No keep found with that id.");
      }
      // if (vaultKeep.CreatorId != userId)
      // {
      //   throw new Exception("Access Denied.");
      // }
      return vaultKeep;
    }
  }
}