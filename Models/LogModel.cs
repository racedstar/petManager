using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGroomingManager.Models
{
    public class LogModel
    {        
        public static void Inssert(Comm_Log log)
        {
            using (Entities db = new Entities())
            {
                db.Comm_Log.Add(log);
                db.SaveChanges();
            }
        }

        public static void Update(Comm_Log log)
        {
            using (Entities db = new Entities())
            {
                db.Entry(log).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void DeleteBill(int BillNumber)
        {
            using (Entities db = new Entities())
            {
                List<Comm_Log> logList = getLogListByBillNumber(BillNumber);

                foreach (var item in logList)
                {
                    db.Comm_Log.Remove(item);
                }

                db.SaveChanges();
            }
        }

        //public static void Delete(Comm_Log log)
        //{
        //    db.Comm_Log.Remove(log);
        //}

        public static List<Vw_Log> getVwLogListByCustomerSN(int SN)
        {
            using (Entities db = new Entities())
            {
                var data = (from o in db.Vw_Log
                            where o.customerSN == SN
                            select o).ToList();

                return data;
            }
        }

        public static int getBillNumber()
        {
            using (Entities db = new Entities())
            {
                int BillNumber = 0;
                var data = (from o in db.Comm_Log
                            select o).FirstOrDefault();

                if (data != null)
                {
                    BillNumber = (from o in db.Comm_Log
                                  select o.BillNumber).Max();
                }

                return BillNumber;
            }
        }

        
        public static List<Comm_Log> getLogListByBillNumber(int BillNumber)
        {
            using (Entities db = new Entities())
            {
                var data = (from o in db.Comm_Log
                            where o.BillNumber == BillNumber
                            select o).ToList();

                return data;
            }
        }

        public static List<Vw_Log> getVwLogListByBillNumber(int BillNumber)
        {
            using (Entities db = new Entities())
            {
                var data = (from o in db.Vw_Log
                            where o.BillNumber == BillNumber
                            select o).ToList();

                return data;
            }
        }
    }

    public class checkoutJson
    {
        public int cosmetologySN { get; set; }
        public int price { get; set; }
    }
}