using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TestVebtech.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0,120)]
        [Required]
        public int Age { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public List<Role>? Roles { get; set; } = new ();
    }
}
