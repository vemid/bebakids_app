using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace BebaKids.Models
{
        public class MyContext : DbContext
    {
        public MyContext() : base("BebaKids.Properties.Settings.localSql")
        {
        }

        public DbSet<EanKod> EanKod { get; set; }
        public DbSet<Popis> Popis { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EanKod>().ToTable("ean_kod2");
            modelBuilder.Entity<EanKod>().Property(p => p.id).HasColumnName("id");
            modelBuilder.Entity<EanKod>().Property(p => p.sifraRobe).HasColumnName("sif_rob");
            modelBuilder.Entity<EanKod>().Property(p => p.VelicinaRobe).HasColumnName("sif_ent_rob");
            modelBuilder.Entity<EanKod>().Property(p => p.naziv).HasColumnName("naziv");
            modelBuilder.Entity<EanKod>().Property(p => p.barKod).HasColumnName("bar_kod");

            modelBuilder.Entity<Popis>().ToTable("pop_sta_mp_st");
            modelBuilder.Entity<Popis>().Property(a => a.Id).HasColumnName("id");
            modelBuilder.Entity<Popis>().Property(a => a.oznakaDokumenta).HasColumnName("ozn_pop_sta");
            modelBuilder.Entity<Popis>().Property(a => a.datum).HasColumnName("dat_pop");
            modelBuilder.Entity<Popis>().Property(a => a.objekat).HasColumnName("sif_obj_mp");
            modelBuilder.Entity<Popis>().Property(a => a.sifra).HasColumnName("sif_rob");
            modelBuilder.Entity<Popis>().Property(a => a.velicina).HasColumnName("sif_ent_rob");
            modelBuilder.Entity<Popis>().Property(a => a.kolic).HasColumnName("kolic");
            modelBuilder.Entity<Popis>().Property(a => a.preneto).HasColumnName("preneto");

        }
    }

}
