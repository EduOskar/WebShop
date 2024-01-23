﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Api.Entity;

public enum OrderStatusType
{
    New,
    Sent,
    Delivered,
    Cancelled
}
public class OrderStatus
{
    public int Id { get; set; }
    public OrderStatusType CurrentStatus { get; set; }
    public DateTime StatusDate { get; set; }

    public OrderStatus()
    {
        CurrentStatus = OrderStatusType.New;
        StatusDate = DateTime.UtcNow;
    }

    public void UpdateStatus(OrderStatusType newStatus)
    {
        CurrentStatus = newStatus;
        StatusDate = DateTime.UtcNow;
    }
}