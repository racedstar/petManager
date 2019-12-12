using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGroomingManager.Models
{
    public class CosmetologyModel
    {        
        public static void Create(Comm_Cosmetology cosmetology)
        {
            using (Entities db = new Entities())
            {
                db.Comm_Cosmetology.Add(cosmetology);
                db.SaveChanges();
            }
        }

        public static void Update(Comm_Cosmetology cosmetology)
        {
            using (Entities db = new Entities())
            {
                db.Entry(cosmetology).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }

        public static Comm_Cosmetology getCosmetologyBySN(int SN)
        {
            using (Entities db = new Entities())
            {
                var data = (from o in db.Comm_Cosmetology
                            where o.SN == SN
                            select o).FirstOrDefault();
                return data;
            }
        }

        public static List<Comm_Cosmetology> getCosmetologyList()
        {
            using (Entities db = new Entities())
            {
                var data = (from o in db.Comm_Cosmetology
                            select o).ToList();
                return data;
            }
        }
    }
}