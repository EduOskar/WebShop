using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;
using WebShop.Web.Services;
using WebShop.Web.Services.Contracts;

public class ChatHub : Hub
{
    private static readonly ConcurrentDictionary<int, JoinSupportSession> supportSessions = new ConcurrentDictionary<int, JoinSupportSession>();
    private readonly CustomStateProvider customStateProvider;
    private readonly ISupportService supportService;

    public ChatHub(CustomStateProvider customStateProvider, ISupportService supportService)
    {
        this.customStateProvider = customStateProvider;
        this.supportService = supportService;
    }

    public override async Task OnConnectedAsync()
    {
        // Log connection or perform any general setup here
        await Clients.Caller.SendAsync("Message", "Connected successfully.");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // Log disconnection or clean up resources here
        await base.OnDisconnectedAsync(exception);
    }

    public async Task JoinSupportSession(int supportMailId)
    {
        if (Context == null)
        {
            // Optionally, notify the caller that joining the session failed due to authentication
            await Clients.Caller.SendAsync("Message", "You must be authenticated to join a support session.");
            return;
        }

        var session = supportSessions.GetOrAdd(supportMailId, _ => new JoinSupportSession());

        if (Context.User.Claims.Any(c => c.ValueType == ClaimTypes.Role && c.Value == "Support"))
        {
            session.SupportWorkerConnectionId = Context.ConnectionId;
        }
        else
        {
            session.UserConnectionId = Context.ConnectionId;
        }

        supportSessions[supportMailId] = session; // Update the session in the dictionary

        // Notify the user of successful joining
        await Clients.Caller.SendAsync("JoinedSupportSession", supportMailId, "You have joined the support session successfully.");
    }

    public async Task SendMessageToSupport(int supportMailId, string message)
    {
        //await Clients.Group(supportMailId.ToString()).SendAsync("ReceiveMessage", message);

        if (supportSessions.TryGetValue(supportMailId, out var session))
        {
            var targetConnectionId = session.UserConnectionId == Context.ConnectionId
                                     ? session.SupportWorkerConnectionId
                                     : session.UserConnectionId;

            if (!string.IsNullOrEmpty(targetConnectionId))
            {
                await Clients.Client(targetConnectionId).SendAsync("ReceiveMessage", message);
            }
        }
    }

    public async Task JoinGroup(int supportMailId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, supportMailId.ToString());
    }

    public async Task SendMessageToGroup(int supportMailId, string message)
    {
        await Clients.Group(supportMailId.ToString()).SendAsync("ReceiveMessage", message);
    }
}

public class JoinSupportSession
{
    public string? UserConnectionId { get; set; }
    public string? SupportWorkerConnectionId { get; set; }
}
