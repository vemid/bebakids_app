using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BebaKids.Models
{
    [Table("ean_kod2")]
    public class EanKod
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("naziv")]
        public string naziv { get; set; }

        [Column("sif_rob")]
        public string sifraRobe { get; set; }

        [Column("sif_ent_rob")]
        public string VelicinaRobe { get; set; }

        [Column("bar_kod")]
        public string barKod { get; set; }


    }

}
