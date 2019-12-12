using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetGroomingManager.Models;
using X.PagedList;
using System.Net;

namespace PetGroomingManager.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index(string keyWord="", int page = 1, int pageSize = 10)
        {
            List<Comm_Customer> customer = CustomerModel.getCustomerList();
            
            if (!keyWord.Equals(string.Empty))
            {
                customer = CustomerModel.getCustomerListByKeyWord(keyWord);
            }
            var result = customer.OrderBy(o => o.SN).ToPagedList(page, pageSize);
            return View(result);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? SN)
        {
            if(SN == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comm_Customer customer = CustomerModel.getCustomerBySN(Convert.ToInt32(SN));
            List<Vw_Pet> petList = PetModel.getPetList().Where(o => o.customerSN == customer.SN).ToList();
            ViewBag.petList = petList;

            return View(customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Create(string customerName, string phone, string address, string customerRemark)
        {
            if(customerName.Equals(string.Empty))
            {
                ModelState.AddModelError("customerName", "輸入客戶姓名");
            }

            if (phone.Equals(string.Empty))
            {
                ModelState.AddModelError("phone", "請輸入電話號碼");
            }            

            Comm_Customer data = CustomerModel.getCustomerCountByphone(phone);
            if(data != null)
            {
                ModelState.AddModelError("Phone", "已有相同手機。");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            Comm_Customer customer = new Comm_Customer();
            customer.customerName = customerName;
            customer.phone = phone;
            customer.address = address;
            customer.customerRemark = customerRemark;

            CustomerModel.Create(customer);
            return RedirectToAction("Index");
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? SN)
        {
            if(SN == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comm_Customer customer = CustomerModel.getCustomerBySN(Convert.ToInt32(SN));
            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int? SN, string customerName, string phone, string address, string customerRemark)
        {
            try
            {
                if(SN == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }


                Comm_Customer customer = CustomerModel.getCustomerBySN(Convert.ToInt32(SN));
                Comm_Customer data = CustomerModel.getCustomerCountByphone(phone);
                if (data != null && customer.SN != data.SN)
                {
                    ModelState.AddModelError("Phone", "已有相同手機。");
                }                

                if (customerName.Equals(string.Empty))
                {
                    ModelState.AddModelError("customerName", "請輸入客戶姓名");
                }

                if (phone.Equals(string.Empty))
                {
                    ModelState.AddModelError("phone", "請輸入電話號碼");
                }

                if (!ModelState.IsValid)
                {                    
                    return View(customer);
                }
                
                customer.customerName = customerName;
                customer.phone = phone;
                customer.address = address;
                customer.customerRemark = customerRemark;

                CustomerModel.Update(customer);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
