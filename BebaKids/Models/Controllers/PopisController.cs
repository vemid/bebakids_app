using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaKids.Models.Controllers
{
    class PopisController
    {
        public List<Popis> PopisItems { get; set; }

        Classes.ErrorLogger errorLogger = new Classes.ErrorLogger();

        public void AddRelatedItem(Popis newPopis)
        {
            if (PopisItems == null)
            {
                PopisItems = new List<Popis>();
            }

            PopisItems.Add(newPopis);
        }

        public void saveItemsToDatabase() 
        {
            using (var context = new Models.MyContext())
            {
                try
                {
                    context.Popis.AddRange(PopisItems);
                    context.SaveChanges();
                    PopisItems.Clear();


                }
                catch (Exception ex)
                {
                    errorLogger.LogException(ex);
                }
            }
        }

        public void saveToDatabase(Popis popisItem)
        {
            using (var context = new Models.MyContext())
            {
                try
                {
                    context.Popis.Add(popisItem);
                    context.SaveChanges();

                }
                catch (Exception ex)
                {
                    errorLogger.LogException(ex);
                }
            }

        }
    }
}
