﻿using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace GuitarStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Positive price is required.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }

    }
}
