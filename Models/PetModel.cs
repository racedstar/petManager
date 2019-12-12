using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGroomingManager.Models
{
    public class PetModel
    {        
        public static void Create(Comm_Pet pet)
        {
            using (Entities db = new Entities())
            {
                db.Comm_Pet.Add(pet);
                db.SaveChanges();
            }
        }

        public static void Update(Comm_Pet pet)
        {
            using (Entities db = new Entities())
            {
                db.Entry(pet).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static Comm_Pet getPetBySN(int SN)
        {
            using (Entities db = new Entities())
            {
                var data = (from o in db.Comm_Pet
                            where o.SN == SN
                            select o).FirstOrDefault();
                return data;
            }
        }

        public static Vw_Pet getVwPetBySN(int SN)
        {
            using (Entities db = new Entities())
            {
                var data = (from o in db.Vw_Pet
                            where o.petSN == SN
                            select o).FirstOrDefault();
                return data;
            }
        }

        public static List<Vw_Pet> getPetListByisState()
        {
            using (Entities db = new Entities())
            {
                var data = (from o in db.Vw_Pet
                            where o.isState == true
                            select o).ToList();

                return data;
            }
        }

        public static List<Vw_Pet> getPetList()
        {
            using (Entities db = new Entities())
            {
                var data = (from o in db.Vw_Pet
                            select o).ToList();

                return data;
            }
        }

        public static List<Vw_Pet> getPetListByKeyWord(string keyWord)
        {
            using (Entities db = new Entities())
            {
                var data = (from o in db.Vw_Pet
                            where o.customerName.Contains(keyWord) ||
                                  o.phone.Contains(keyWord) ||
                                  o.petName.Contains(keyWord) ||
                                  o.varietyName.Contains(keyWord) ||
                                  o.kindName.Contains(keyWord)
                            select o).ToList();

                return data;
            }
        }
    }
}