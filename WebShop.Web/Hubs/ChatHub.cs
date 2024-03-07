using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;
using WebShop.Models.DTOs.MailDtos;
using WebShop.Web.Services;
using WebShop.Web.Services.Contracts;

public class ChatHub : Hub
{
    private static readonly ConcurrentDictionary<int, JoinSupportSession> supportSessions = new ConcurrentDictionary<int, JoinSupportSession>();
    private readonly CustomStateProvider _customStateProvider;
    private readonly ISupportService _supportService;

    public ChatHub(CustomStateProvider customStateProvider, ISupportService supportService)
    {
        _customStateProvider = customStateProvider;
        _supportService = supportService;
    }

    public override async Task OnConnectedAsync()
    {

        await Clients.Caller.SendAsync("Message", "Connected successfully.");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }

    public async Task JoinSupportSession(int supportMailId)
    {
        if (Context == null)
        {
            await Clients.Caller.SendAsync("Message", "You must be authenticated to join a support session.");
            return;
        }

        var session = supportSessions.GetOrAdd(supportMailId, _ => new JoinSupportSession());

        if (Context.User!.Claims.Any(c => c.ValueType == ClaimTypes.Role && c.Value == "Support"))
        {
            session.SupportWorkerConnectionId = Context.ConnectionId;
        }
        else
        {
            session.UserConnectionId = Context.ConnectionId;
        }

        supportSessions[supportMailId] = session;


        await Clients.Caller.SendAsync("JoinedSupportSession", supportMailId, "You have joined the support session successfully.");
    }

    public async Task SendMessageToSupport(int supportMailId, string user, string message)
    {
        //await Clients.Group(supportMailId.ToString()).SendAsync("ReceiveMessage", message);

        if (supportSessions.TryGetValue(supportMailId, out var session))
        {
            var targetConnectionId = session.UserConnectionId == Context.ConnectionId
                                     ? session.SupportWorkerConnectionId
                                     : session.UserConnectionId;

            if (!string.IsNullOrEmpty(targetConnectionId))
            {
                await Clients.Client(targetConnectionId).SendAsync("ReceiveMessage", user, message);
            }
        }
    }

    public async Task JoinGroup(int TicketId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, TicketId.ToString());

        await Clients.Caller.SendAsync("RecieveMessage", TicketId, "You have joined support session. wait for support to contact you");

        //throw new Exception("No Ticket session with that Id");
    }

    public async Task SendMessageToGroup(int TicketId, string user, string message)
    {
        await _supportService.AddSupportMessage(TicketId, new SupportMessagesDto { TicketId = TicketId, UserName = user, Message = message });

        await Clients.Group(TicketId.ToString()).SendAsync("ReceiveMessage", user, message);
    }
}

public class JoinSupportSession
{
    public string? UserConnectionId { get; set; }
    public string? SupportWorkerConnectionId { get; set; }
}
