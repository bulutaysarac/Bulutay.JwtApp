﻿namespace Bulutay.JwtApp.Front.Models
{
    public class ProductUpdateModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public List<CategoryListModel>? CategoryList { get; set; }
    }
}
