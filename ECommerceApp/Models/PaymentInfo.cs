using ECommerceApp.Dto;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ECommerceApp.Models;

public class PaymentInfo
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public int TransactionId { get; set; }
    public string Status { get; set; } = string.Empty; 
    public Order order { get; set; } = new Order();
}

// User_Payment: Id, OrderId, PaymentMethod, TransactionId, Status
