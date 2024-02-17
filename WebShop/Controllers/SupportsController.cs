using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs.MailDtos;

namespace WebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SupportsController : ControllerBase
{
    private readonly ISupportrepository _supportrepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public SupportsController(ISupportrepository supportrepository, IUserRepository userRepository, IMapper mapper)
    {
        _supportrepository = supportrepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<SupportMailDto>>> GetSupportEmails()
    {
        var supportEmails = _mapper.Map<List<SupportMailDto>>(await _supportrepository.GetSupportMails());

        if (supportEmails != null)
        {
            return Ok(supportEmails);
        }

        return BadRequest();
    }

    [HttpGet("Users-Support-emails/{userId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<List<SupportMailDto>>> GetUserSupportMail(int userId)
    {
        var userSupportMails = _mapper.Map<List<SupportMailDto>>(await _supportrepository.GetUserSupportMails(userId));

        if (userSupportMails != null)
        {
            return Ok(userSupportMails);
        }

        return NotFound();
    }

    [HttpGet("{emailId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<SupportMailDto>> GetSupportEmail(int emailId)
    {
        var supportMail = _mapper.Map<SupportMailDto>(await _supportrepository.GetSupportMail(emailId));

        if (supportMail != null)
        {
            if (supportMail.Id == emailId)
            {
                return Ok(supportMail);
            }

            return NotFound();
        } 
        return BadRequest();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> CreateSupportMail([FromBody] SupportMailDto supportMailCreate)
    {

        var supportMailMap = _mapper.Map<SupportMail>(supportMailCreate);

        if (await _supportrepository.CreateSupportMail(supportMailMap))
        {
            return CreatedAtAction("GetSupportEmail", new { emailId = supportMailMap.Id }, supportMailMap);
        }

        return BadRequest();
    }

    [HttpDelete("{supportMailId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<bool>> DeleteSupportMail(int supportMailId)
    {
        var mailToDelete = await _supportrepository.GetSupportMail(supportMailId);

        if (await _supportrepository.DeleteSupportMail(mailToDelete))
        {
            return NoContent();
        }

        return BadRequest(mailToDelete);
    }
}
