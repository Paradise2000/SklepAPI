﻿namespace SklepAPI.Models
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public int ImagePath { get; set; }
    }
}
