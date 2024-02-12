using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WebShop.Models.DTOs;
public enum OrderStatusType
{
    [EnumMember(Value = "New")]
    New,

    [EnumMember(Value = "Sent")]
    Sent,

    [EnumMember(Value = "Delivered")]
    Delivered,

    [EnumMember(Value = "Cancelled")]
    Cancelled
}
public class OrderStatusDto
{
    public int Id { get; set; }
    public OrderStatusType CurrentStatus { get; set; }
    public DateTime StatusDate { get; set; }

    public OrderStatusDto()
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

