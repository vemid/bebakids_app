using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BebaKids.Models
{
    [Table("pop_sta_mp_st")]
    public class Popis
    {
        [Key]
        public int Id { get; set; }

        [Column("ozn_pop_sta")]
        public string oznakaDokumenta { get; set; }

        [Column("dat_pop")]
        public DateTime datum { get; set; }

        [Column("sif_obj_mp")]
        public string objekat { get; set; }

        [Column("sif_rob")]
        public string sifra { get; set; }

        [Column("sif_ent_rob")]
        public string velicina { get; set; }

        [Column("kolic")]
        public int kolic { get; set; }

        [Column("preneto")]
        public int preneto { get; set; }

        
    }
}
