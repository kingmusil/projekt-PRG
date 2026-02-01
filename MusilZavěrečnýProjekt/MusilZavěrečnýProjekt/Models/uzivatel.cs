using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusilZavěrečnýProjekt.Models
{

    [Table("uzivatel")]
    public class User
    {
        [Key]
        [Column("uzivatel_id")]
        public int UserId { get; set; }
        [Column("jmeno")]
        public string Username { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("heslo_hash")]
        public string PasswordHash { get; set; }
    }

}

