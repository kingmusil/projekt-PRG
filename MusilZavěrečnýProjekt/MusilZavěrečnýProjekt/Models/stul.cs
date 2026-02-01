using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusilZavěrečnýProjekt.Models
{

    [Table("stul")]
    public class Seat
    {
        [Key]
        [Column("stul_id")]
        public int TabelId { get; set; }
        [Column("oznaceni")]
        public string Label { get; set; }
        [Column("kapacita")]
        public int Capacity { get; set; }
    }

}
