using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class ProfilesService
  {

    private readonly ProfilesRepository _profilesRepo;

    public ProfilesService(ProfilesRepository profilesRepo)
    {
      _profilesRepo = profilesRepo;
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
      return _profilesRepo.GetAllVaults(id);
    }
  }
}