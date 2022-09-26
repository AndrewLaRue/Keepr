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

    public VaultKeepsController(VaultKeepsService vaultKeepsService)
    {
      _vaultKeepsService = vaultKeepsService;
    }

    // SECTION Create a VaultKeep
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVaultKeep)
    {
      try
      {
        Profile user = await HttpContext.GetUserInfoAsync<Profile>();
        newVaultKeep.CreatorId = user.Id;
        VaultKeep vaultKeep = _vaultKeepsService.Create(newVaultKeep);
        vaultKeep.Creator = user;
        return Ok(vaultKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // SECTION Get by id
    [HttpGet("{id}")]
    public ActionResult<VaultKeep> GetOne(int id)
    {
      try
      {
        VaultKeep vaultKeep = _vaultKeepsService.GetOne(id);
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