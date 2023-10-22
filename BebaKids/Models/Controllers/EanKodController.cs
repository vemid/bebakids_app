using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaKids.Models.Controllers
{
    class EanKodController
    {
        public List<EanKod> EanKodItems { get; set; }

        public void AddRelatedItem(EanKod newEanKod)
        {
            if (EanKodItems == null)
            {
                EanKodItems = new List<EanKod>();
            }

            EanKodItems.Add(newEanKod);
        }

        public List<Models.EanKod> GetDataFromDatabase()
        {
            List<Models.EanKod> products;

            using (var context = new Models.MyContext())
            {
                // Koristi Entity Framework za dohvat podataka iz tablice
                products = context.EanKod.Select(p => new Models.EanKod
                {
                    id = p.id,
                    sifraRobe = p.sifraRobe,
                    VelicinaRobe = p.VelicinaRobe,
                    barKod = p.barKod,
                    naziv = p.naziv
                }).ToList();
            }

            return products;
        }

    }
}
