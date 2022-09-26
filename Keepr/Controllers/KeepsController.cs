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
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _keepsService;

    public KeepsController(KeepsService keepsService)
    {
      _keepsService = keepsService;
    }

    // SECTION Create a Keep
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
    {
      try
      {
        Profile user = await HttpContext.GetUserInfoAsync<Profile>();
        newKeep.CreatorId = user.Id;
        Keep keep = _keepsService.Create(newKeep);
        keep.Creator = user;
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // SECTION Get All
    [HttpGet]
    public ActionResult<List<Keep>> GetAll()
    {
      try
      {
        List<Keep> keeps = _keepsService.GetAll();
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // SECTION Get by id
    [HttpGet("{id}")]
    public ActionResult<Keep> GetOne(int id)
    {
      try
      {
        Keep keep = _keepsService.GetOne(id);
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    // SECTION Update(edit) Keep
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Update(int id, [FromBody] Keep update)
    {
      try
      {
        Profile user = await HttpContext.GetUserInfoAsync<Profile>();
        update.Id = id;
        Keep keep = _keepsService.Update(update, user.Id);
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // SECTION DELETE Keep
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Profile user = await HttpContext.GetUserInfoAsync<Profile>();
        string message = _keepsService.Delete(id, user.Id);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}