﻿using GuitarStore.Domain.Entities;

namespace GuitarStore.WebUi.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}