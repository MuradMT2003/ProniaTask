﻿using ProniaTask.Models;

namespace ProniaTask.ViewModels.ProductVMs;

public record GetShopVM
{
    public IEnumerable<Product> Products { get; set; }
    public IEnumerable<Category> Categories { get; set; }
    public int CategoryCount { get; set; }
    public int ProductCount { get; set; }
}