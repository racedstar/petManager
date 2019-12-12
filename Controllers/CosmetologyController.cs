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
    public class CosmetologyController : Controller
    {
        // GET: Cosmetology
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            List<Comm_Cosmetology> cosmetology = CosmetologyModel.getCosmetologyList();
            var result = cosmetology.OrderBy(o => o.SN).ToPagedList();
            return View(result);
        }

        // GET: Cosmetology/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cosmetology/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cosmetology/Create
        [HttpPost]
        public ActionResult Create(string cosmetologyName, int price)
        {
            try
            {
                if (cosmetologyName.Equals(string.Empty))
                {
                    ModelState.AddModelError("cosmetologyName", "請輸入項目");
                }

                if (price.Equals(string.Empty))
                {
                    ModelState.AddModelError("price", "請輸入金額");
                }

                if (!ModelState.IsValid)
                {
                    return View();
                }

                Comm_Cosmetology cosmetology = new Comm_Cosmetology();
                cosmetology.cosmetologyName = cosmetologyName;
                cosmetology.price = price;

                CosmetologyModel.Create(cosmetology);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cosmetology/Edit/5
        public ActionResult Edit(int? SN)
        {
            if(SN == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comm_Cosmetology cosmetology = CosmetologyModel.getCosmetologyBySN(Convert.ToInt32(SN));
            
            return View(cosmetology);
        }

        // POST: Cosmetology/Edit/5
        [HttpPost]
        public ActionResult Edit(int SN, string cosmetologyName, int price)
        {
            try
            {

                if (cosmetologyName.Equals(string.Empty))
                {
                    ModelState.AddModelError("cosmetologyName", "請輸入項目");
                }

                if (price.Equals(string.Empty))
                {
                    ModelState.AddModelError("price", "請輸入金額");
                }

                Comm_Cosmetology cosmetology = CosmetologyModel.getCosmetologyBySN(SN);

                if (!ModelState.IsValid)
                {
                    return View(cosmetology);
                }
                
                cosmetology.cosmetologyName = cosmetologyName;
                cosmetology.price = price;

                CosmetologyModel.Update(cosmetology);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public string getCosmetologyPrice(int SN)
        {
            string price = CosmetologyModel.getCosmetologyBySN(SN).price.ToString();

            return price;
        }
    }
}
