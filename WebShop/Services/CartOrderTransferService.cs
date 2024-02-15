using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebShop.Api.Data;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Services;

public class CartOrderTransferService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IEmailSenderRepository _emailSenderRepository;

    public CartOrderTransferService(ApplicationDbContext dbContext, IEmailSenderRepository emailSenderRepository)
    {
        _dbContext = dbContext;
        _emailSenderRepository = emailSenderRepository;
    }

    public async Task<bool> CartOrderTransfer(int userId, string? discountCode)
    {
        decimal totalCost = 0;

        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                return false;
            }

            var cart = await _dbContext.Carts.Include(c => c.CartItems!)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems!.Any())
            {
                return false;
            }

            if (!string.IsNullOrEmpty(discountCode))
            {
                var discount = await _dbContext.Discounts.FirstOrDefaultAsync(dc => dc.DiscountCode == discountCode);

                totalCost = cart.CartItems!.Sum(item =>
                (item.Product.DiscountedPrice ?? item.Product.Price) * item.Quantity);

                totalCost = totalCost * discount!.DiscountPercentage;

                user.Credit -= totalCost;
            }
            else
            {

                totalCost = cart.CartItems!.Sum(item =>
                (item.Product.DiscountedPrice ?? item.Product.Price) * item.Quantity);

                user.Credit -= totalCost;
            }


            if (user.Credit < totalCost)
            {
                return false;
            }


            var order = new Order
            {
                UserId = userId,
                PlacementTime = DateTime.Now,
                OrderStatus = new OrderStatus()
            };

            var getOrder = await _dbContext.Orders.AddAsync(order);

            var productListString = cart.CartItems!
          .Select(item =>
            $"{item.Quantity}x {item.Product.Name} (Price: {(item.Product.DiscountedPrice ?? item.Product.Price) * item.Quantity:C})")
            .Aggregate((current, next) => current + "\n" + next);

            var productList = cart.CartItems;

            foreach (var product in cart.CartItems!)
            {
                var orderItem = new OrderItem();

                orderItem.Order = getOrder.Entity;
                orderItem.ProductId = product.ProductId;
                orderItem.Product = product.Product;
                orderItem.Quantity = product.Quantity;
                orderItem.OrderId = getOrder.Entity.Id;
                orderItem.ProductDescription = product.Product.Description;

                await _dbContext.OrderItems.AddAsync(orderItem);

                product.Product.Quantity -= product.Quantity;
            }

            _dbContext.CartItems.RemoveRange(cart.CartItems);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            EmailDto email = new()
            {
                Subject = $"Order: {order.Id}",
                From = "Consid_webbShop@Consid.com",
                Body = $"Thank you for your purchase. Here are the products you've purchased:\n{productListString}\nTotal Cost: {totalCost:C}",
                To = user.Email!
            };

            await _emailSenderRepository.SendEmailAsync(email);

            return true;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return false;
        }
    }
}
