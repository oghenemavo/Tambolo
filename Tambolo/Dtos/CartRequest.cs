﻿namespace Tambolo.Dtos
{
    public class CartRequest
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
