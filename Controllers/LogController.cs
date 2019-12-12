using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetGroomingManager.Models;
using Newtonsoft.Json;

namespace PetGroomingManager.Controllers
{
    public class LogController : Controller
    {

        public ActionResult Index(int SN=0)
        {            
            List<Vw_Log> log = LogModel.getVwLogListByCustomerSN(SN);
            string customerName = CustomerModel.getCustomerBySN(SN).customerName;

            ViewBag.customerName = customerName;

            return View(log);
        }
        
        public ActionResult Checkout(int petSN)
        {
            Vw_Pet pet = PetModel.getVwPetBySN(petSN);
            List<Comm_Cosmetology> cosmetologyList = CosmetologyModel.getCosmetologyList();            

            ViewBag.cosmetologySN = new SelectList(cosmetologyList, "SN", "cosmetologyName");
            ViewBag.pet = pet;

            return View(pet);
        }

        [HttpPost]
        public string Checkout(string petSN, string result)
        {
            var jsonList = JsonConvert.DeserializeObject<List<checkoutJson>>(result);
            int BillNumber = LogModel.getBillNumber() + 1;           

            foreach (var item in jsonList)
            {
                Comm_Log log = new Comm_Log();
                log.petSN = Convert.ToInt32(petSN);
                log.BillNumber = BillNumber;
                log.cosmetologySN = item.cosmetologySN;
                log.price = item.price;
                log.CreateTime = DateTime.Now;
                log.Remark = string.Empty;

                LogModel.Inssert(log);
            }

            return "結帳成功";
        }
    }



}

