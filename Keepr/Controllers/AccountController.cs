using System;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly VaultsService _vaultsService;

    public AccountController(AccountService accountService, VaultsService vaultsService)
    {
      _accountService = accountService;
      _vaultsService = vaultsService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Profile>> Get()
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<Profile>> Update(string id, [FromBody] Profile update)
    {
      try
      {
        Profile user = await HttpContext.GetUserInfoAsync<Profile>();
        // update.Id = id;
        Profile account = (Profile)_accountService.Edit(update, user.Email);
        return Ok(account);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // SECTION Get my vaults
    [HttpGet("vaults")]
    public async Task<ActionResult<List<Vault>>> GetMyVaults()
    {
      try
      {
        Profile user = await HttpContext.GetUserInfoAsync<Profile>();

        List<Vault> vault = _vaultsService.GetMyVaults(user.Id);
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }





  }
}