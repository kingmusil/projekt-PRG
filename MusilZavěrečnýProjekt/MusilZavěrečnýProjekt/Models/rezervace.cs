using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusilZavěrečnýProjekt.Models
{


    [Table("rezervace")]
    public class rezervace
    {
        [Key]
        [Column("rezervace_id")]
        public int ReservationId { get; set; }
        [Column("uzivatel_id")]
        public int UserId { get; set; }
        [Column("stul_id")]
        public int SeatId { get; set; }
        [Column("datum")]
        public DateTime ReservedOn { get; set; }
        [Column("cas_od")]
        public TimeOnly TimeFrom { get; set; }
        [Column("cas_do")]
        public TimeOnly TimeTo { get; set; }
    }
}
