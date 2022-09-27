using System;
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
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vaultKeepsService;
    private readonly VaultsService _vaultsService;

    public VaultKeepsController(VaultKeepsService vaultKeepsService, VaultsService vaultsService)
    {
      _vaultKeepsService = vaultKeepsService;
      _vaultsService = vaultsService;
    }



    // SECTION Create a VaultKeep
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVaultKeep)
    {
      try
      {
        // FIXME this is wrong
        Profile user = await HttpContext.GetUserInfoAsync<Profile>();
        Account owner = await HttpContext.GetUserInfoAsync<Account>();
        newVaultKeep.CreatorId = user.Id;
        VaultKeep vaultKeep = _vaultKeepsService.Create(newVaultKeep);
        vaultKeep.Creator = user;
        if (user.Id != owner.Id)
        {
          return BadRequest();
        }
        return Ok(vaultKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // SECTION Get by id
    [HttpGet("{id}")]
    public async Task<ActionResult<VaultKeep>> GetOne(int id)
    {
      try
      {
        Profile user = await HttpContext.GetUserInfoAsync<Profile>();
        VaultKeep vaultKeep = _vaultKeepsService.GetOne(id);
        Vault vault = _vaultsService.GetOne(id);
        if (vault.isPrivate == true || vaultKeep.CreatorId != user.Id)
        {
          return BadRequest();
        }
        return Ok(vaultKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // SECTION Delete VaultKeep
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Profile user = await HttpContext.GetUserInfoAsync<Profile>();
        string message = _vaultKeepsService.Delete(id, user.Id);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}