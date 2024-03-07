using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using WebShop.Api.Entity;
using WebShop.Api.Repositories;
using WebShop.Api.Repositories.Contracts;
using WebShop.Api.Services;
using WebShop.Models.DTOs.MailDtos;

namespace WebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SupportsController : ControllerBase
{
    private readonly ISupportrepository _supportrepository;
    private readonly IUserRepository _userRepository;
    private readonly SupportEmailService _supportEmailService;
    private readonly SupportMessageService _supportMessageService;
    private readonly IMapper _mapper;

    public SupportsController(ISupportrepository supportrepository,
        IUserRepository userRepository, 
        SupportEmailService supportEmailService, 
        SupportMessageService supportMessageService,
        IMapper mapper)
    {
        _supportrepository = supportrepository;
        _userRepository = userRepository;
        _supportEmailService = supportEmailService;
        _supportMessageService = supportMessageService;
        _mapper = mapper;
    }

    [HttpGet("messages/{ticketId:int}")]
    public async Task<ActionResult<List<SupportMessagesDto>>> GetSupportMessages(int ticketId)
    {
        var messages = await _supportrepository.GetSupportMessagesForTicket(ticketId);
        if (messages != null)
        {
            var messageMap = _mapper.Map<List<SupportMessagesDto>>(messages);

            return Ok(messageMap);
        }

        return NotFound(new List<SupportMessagesDto>());
    }

    [HttpGet("messagesByTicket/{ticketId:int}")]
    public async Task<ActionResult<List<SupportMessagesDto>>> GetSupportMessagesByTicket(int ticketId)
    {
        var result = _mapper.Map<List<SupportMessagesDto>>(
            await _supportrepository.GetSupportMessagesForTicket(ticketId));

        if (result != null)
        {
            return result;
        }

        return NotFound();
    }

    [HttpPut("{ticketId:int}")]
    public async Task<ActionResult<bool>> UpdateSupportMessage(int ticketId)
    {
        var mail = await _supportrepository.GetSupportMail(ticketId);

        if (mail != null)
        {
            mail.IsAnswered = Entity.IsAnswered.Answered;

            if (await _supportrepository.UppdateSupportMail(mail))
            {

                return NoContent();

            }

            return BadRequest("couldnt update bool");
        }

        return NotFound();
    }

    [HttpPost("messages/{ticketId:int}")]
    public async Task<ActionResult<SupportMessagesDto>> CreateSupportMessage(int ticketId ,[FromBody] SupportMessagesDto messageDto)
    {
        var message = _mapper.Map<SupportMessages>(messageDto);
        message.TicketId = ticketId;

        if (await _supportrepository.AddSupportMessage(message))
        {
            return CreatedAtAction("GetSupportMessage", new { messageId = message.Id }, message);
        }

        return BadRequest();
    }


    [HttpGet("single-message/{messageId:int}")]
    public async Task<ActionResult<SupportMessagesDto>> GetSupportMessage(int messageId)
    {
        var result = _mapper.Map<SupportMessagesDto>(await _supportrepository.GetSupportMessage(messageId));

        if (result != null)
        {
            return result;
        }

        return NotFound();
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

    [HttpPost("AssignSupportToMail/{supportMailId:int}/{supportId:int}")]
    public async Task<ActionResult<bool>> AssignSupportToMail(int supportMailId, int supportId)
    {
        var result = await _supportEmailService.AssignSupportToTicket(supportMailId, supportId);

        if (result)
        {
            return Ok(result);
        }
        return NotFound();
    }

    [HttpPost("AssignSupportToTicket/{supportTicketId:int}/{supportId:int}")]
    public async Task<ActionResult<bool>> AssignSupportToTicket(int supportTicketId, int supportId)
    {
        var result = await _supportMessageService.AssignSupportToTicket(supportTicketId, supportId);
        if (result)
        {
            return Ok(result);
        }
        return NotFound();
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

    [HttpGet("Tickets/{Id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<MessageTicketDto>> GetSupportTicket(int Id)
    {
        var messageTicket = _mapper.Map<MessageTicketDto>(await _supportrepository.GetMessageTicket(Id));

        if (messageTicket != null)
        {
            return messageTicket;
        }

        return NotFound();
    }

    [HttpGet("UserTickets/{userId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<MessageTicketDto>>> GetUserSpportTickets(int userId)
    {
        var messageTickets = _mapper.Map<List<MessageTicketDto>>(
            await _supportrepository.GetMEssageTicketsByUser(userId));

        if (messageTickets != null)
        {
            return Ok(messageTickets);
        }

        return NotFound(messageTickets);
    }

    [HttpGet("Tickets")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<List<MessageTicketDto>>> GetSupportTickets()
    {
        var messageTickets = _mapper.Map<List<MessageTicketDto>>(
            await _supportrepository.GetMessageTickets());

        if (messageTickets != null)
        {
            return Ok(messageTickets);
        }

        return NotFound(messageTickets);
    }

    [HttpPost("Tickets")]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> CreateSupportTicket([FromBody] MessageTicketDto ticketCreate)
    {
        var ticketMap = _mapper.Map<MessageTicket>(ticketCreate);

        if (await _supportrepository.CreateSupportTicket(ticketMap))
        {
            return CreatedAtAction("GetSupportTicket", new { Id = ticketMap.Id }, ticketMap);
        }

        return BadRequest();
    }
}
