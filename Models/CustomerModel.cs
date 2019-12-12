using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGroomingManager.Models
{
    public class CustomerModel
    {        
        public static void Create(Comm_Customer customer)
        {
            using (Entities db = new Entities())
            {
                db.Comm_Customer.Add(customer);
                db.SaveChanges();
            }
        }

        public static void Update(Comm_Customer customer)
        {
            using (Entities db = new Entities())
            {
                db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static Comm_Customer getCustomerBySN(int SN)
        {
            using (Entities db = new Entities())
            {
                var data = (from o in db.Comm_Customer
                            where o.SN == SN
                            select o).FirstOrDefault();
                return data;
            }
        }

        public static List<Comm_Customer> getCustomerList()
        {
            using (Entities db = new Entities())
            {
                var data = (from o in db.Comm_Customer
                            select o).ToList();
                return data;
            }
        }

        public static Comm_Customer getCustomerCountByphone(string phone)
        {
            using (Entities db = new Entities())
            {
                var data = (from o in db.Comm_Customer
                             where o.phone == phone
                             select o).FirstOrDefault();
                return data;
            }
        }

        public static List<Comm_Customer> getCustomerListByKeyWord(string keyWord)
        {
            using (Entities db = new Entities())
            {
                var data = (from o in db.Comm_Customer
                            where o.customerName.Contains(keyWord) || o.phone.Contains(keyWord)
                            select o).ToList();

                return data;
            }
        }
    }
}