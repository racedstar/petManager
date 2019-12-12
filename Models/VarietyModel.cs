using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGroomingManager.Models
{
    public class VarietyModel
    {        
        public static void Create(Comm_Variety variety)
        {
            using (Entities db = new Entities())
            {
                db.Comm_Variety.Add(variety);
                db.SaveChanges();
            }
        }

        public static void Update(Comm_Variety variety)
        {
            using (Entities db = new Entities())
            {
                db.Entry(variety).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static Comm_Variety getVarietyBySN(int SN)
        {
            using (Entities db = new Entities())
            {
                var data = (from o in db.Comm_Variety
                            where o.SN == SN
                            select o).FirstOrDefault();
                return data;
            }
        }

        public static List<Comm_Variety> getVarietyListByKindSN(int kindSN)
        {
            using (Entities db = new Entities())
            {
                var data = (from o in db.Comm_Variety
                            where o.kindSN == kindSN
                            select o).ToList();
                return data;
            }
        }
    }
}