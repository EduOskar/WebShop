namespace WebShop.Web.Services.Contracts;

public interface ICartOrderTransferService
{
    Task<bool> CartOrderTransfer(int userId);
}
