using System.ComponentModel.DataAnnotations;

namespace GuitarStore.WebUi.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Please enter a name")]
        [RegularExpression("^[a-zA-Z0-9\\s]+", ErrorMessage = "Enter valid Name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter enter an address line")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        [RegularExpression("[a-zA-Z ]+$", ErrorMessage = "Enter valid City.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a zip code")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Enter valid Zip Address.")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter a country name")]
        [RegularExpression("[a-zA-Z ]+$", ErrorMessage = "Enter valid Country.")]
        public string Country { get; set; }
    }
}
