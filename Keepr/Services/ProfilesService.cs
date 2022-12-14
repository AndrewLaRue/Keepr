using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class ProfilesService
  {

    private readonly ProfilesRepository _profilesRepo;
    private readonly VaultsRepository _vaultsRepo;

    public ProfilesService(ProfilesRepository profilesRepo, VaultsRepository vaultsRepo)
    {
      _profilesRepo = profilesRepo;
      _vaultsRepo = vaultsRepo;
    }

    internal Profile GetById(string id)
    {
      Profile profile = _profilesRepo.GetById(id);
      if (profile == null)
      {
        throw new Exception("No profile found with that id.");
      }
      return profile;
    }

    // SECTION get all keeps
    internal List<Keep> GetAllKeeps(string id)
    {
      if (id == null)
      {
        throw new Exception("bad profile id");
      }
      return _profilesRepo.GetAllKeeps(id);
    }

    // SECTION get all vaults
    internal List<Vault> GetAllVaults(string id)
    {
      if (id == null)
      {
        throw new Exception("bad profile id");
      }
      List<Vault> vaults = _vaultsRepo.GetMyVaults(id);
      List<Vault> publicVaults = vaults.FindAll(v => v.isPrivate == false);
      return publicVaults;
    }
  }
}