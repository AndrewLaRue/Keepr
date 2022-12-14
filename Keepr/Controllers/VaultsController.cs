using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vaultsService;
    private readonly VaultKeepsService _vaultKeepsService;

    public VaultsController(VaultsService vaultsService, VaultKeepsService vaultKeepsService)
    {
      _vaultsService = vaultsService;
      _vaultKeepsService = vaultKeepsService;
    }



    // SECTION Create a Vault
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
    {
      try
      {
        Profile user = await HttpContext.GetUserInfoAsync<Profile>();
        newVault.CreatorId = user.Id;
        Vault vault = _vaultsService.Create(newVault);
        vault.Creator = user;
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }




    // SECTION Update(edit) Vault
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Update(int id, [FromBody] Vault update)
    {
      try
      {
        Profile user = await HttpContext.GetUserInfoAsync<Profile>();
        update.Id = id;
        update.CreatorId = user.Id;
        Vault vault = _vaultsService.Update(update);
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }



    // SECTION Delete Vault
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Profile user = await HttpContext.GetUserInfoAsync<Profile>();
        string message = _vaultsService.Delete(id, user.Id);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // SECTION Get VaultKeeps
    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<CollectedKeepVM>>> GetVaultKeeps(int id)
    {
      try
      {
        Profile user = await HttpContext.GetUserInfoAsync<Profile>();
        // Vault vault = _vaultsService.GetOne(id, user?.Id);
        List<CollectedKeepVM> vaultKeeps = _vaultKeepsService.GetVaultKeeps(id, user?.Id);
        // if (vault.isPrivate == true && vault.CreatorId != user.Id)
        // {
        //   return BadRequest();
        // }
        return Ok(vaultKeeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }



    // SECTION Get Vault by id
    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> GetOne(int id)
    {
      try
      {
        Profile user = await HttpContext.GetUserInfoAsync<Profile>();
        // Account owner = await HttpContext.GetUserInfoAsync<Account>();
        Vault vault = _vaultsService.GetOne(id, user?.Id);
        // if (vault.CreatorId != user.Id && vault.isPrivate == true)
        // {
        //   return BadRequest();

        // }
        return Ok(vault);

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


  }
}