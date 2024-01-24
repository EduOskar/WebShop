﻿namespace WebShop.Api.Entity;

public class Discount
{
    public int Id { get; set; }

    public string discountCode { get; set; } = default!;

    public decimal discountPercentage { get; set; } = default!;

    public int DiscountQuantity { get; set; } = default!;

    public bool DiscountIsStatus { get; set; } = default!;

}
