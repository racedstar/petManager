using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PetGroomingManager.Models
{
    public class KindModel
    {
        public static void Create(Comm_Kind kind)
        {
            using (Entities db = new Entities())
            {
                db.Comm_Kind.Add(kind);
                db.SaveChanges();
            }
        }

        public static void Update(Comm_Kind kind)
        {
            using (Entities db = new Entities())
            {
                db.Entry(kind).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static Comm_Kind getKindBySN(int SN)
        {
            using (Entities db = new Entities())
            {
                var data = (from o in db.Comm_Kind
                            where o.SN == SN
                            select o).FirstOrDefault();

                return data;
            }
        }

        public static List<Comm_Kind> getKindList()
        {
            using (Entities db = new Entities())
            {
                var data = (from o in db.Comm_Kind
                            select o).ToList();
                return data;
            }
        }
    }
}