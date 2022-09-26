using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _profilesService;

    public ProfilesController(ProfilesService profilesService)
    {
      _profilesService = profilesService;
    }


    // SECTION Get by id
    [HttpGet("{id}")]
    public ActionResult<Profile> GetById(string id)
    {
      try
      {
        Profile profile = _profilesService.GetById(id);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // SECTION get profile keeps
    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetAllKeeps(string id)
    {
      try
      {
        List<Keep> keeps = _profilesService.GetAllKeeps(id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }



    // SECTION get profile vaults
    [HttpGet("{id}/vaults")]
    public ActionResult<List<Vault>> GetAllVaults(string id)
    {
      try
      {
        List<Vault> vaults = _profilesService.GetAllVaults(id);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


  }
}