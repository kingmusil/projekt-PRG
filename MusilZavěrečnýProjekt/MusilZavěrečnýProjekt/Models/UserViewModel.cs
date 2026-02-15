using System.ComponentModel.DataAnnotations;

namespace MusilZavěrečnýProjekt.Models
{
 public class UserViewModel
 {
 [Required]
 public string Username { get; set; }

 [Required]
 [EmailAddress]
 public string Email { get; set; }

 [Required]
 public string Password { get; set; }
 }
}
