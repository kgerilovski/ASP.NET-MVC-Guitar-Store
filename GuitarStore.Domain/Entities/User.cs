using System.ComponentModel.DataAnnotations;

namespace GuitarStore.Domain.Entities
{
    public class User
    {
        [Key]
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
