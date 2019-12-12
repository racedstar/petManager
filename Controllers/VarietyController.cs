using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetGroomingManager.Models;
using System.Web.Script.Serialization;
using X.PagedList;
using System.Net;

namespace PetGroomingManager.Controllers
{
    public class VarietyController : Controller
    {
        // GET: Variety
        public ActionResult Index(int kindSN, int page = 1, int pageSize = 10)
        {
            List<Comm_Variety> varietyList = VarietyModel.getVarietyListByKindSN(kindSN);
            var result = varietyList.OrderBy(o => o.SN).ToPagedList();

            ViewBag.kindSN = kindSN;

            return View(result);
        }


        // GET: Variety/Create
        public ActionResult Create(string kindSN)
        {
            if (kindSN.Equals(string.Empty))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.kindSN = kindSN;
            return View();
        }

        // POST: Variety/Create
        [HttpPost]
        public ActionResult Create(string kindSN, string varietyName)
        {
            try
            {
                if(kindSN.Equals(string.Empty))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (varietyName.Equals(string.Empty))
                {
                    ModelState.AddModelError("varietyName", "請輸入品種");
                }

                if (!ModelState.IsValid)
                {
                    return View();
                }

                int kSN = 0;
                int.TryParse(kindSN.ToString(), out kSN);

                Comm_Variety variety = new Comm_Variety();
                variety.varietyName = varietyName;
                variety.kindSN = kSN;

                VarietyModel.Create(variety);

                return RedirectToAction("Index", new { kindSN = kindSN} );
            }
            catch
            {
                return View();
            }
        }

        // GET: Variety/Edit/5
        public ActionResult Edit(int? SN)
        {
            if(SN == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comm_Variety variety = VarietyModel.getVarietyBySN(Convert.ToInt32(SN));
            ViewBag.kindSN = variety.kindSN;

            return View(variety);
        }

        // POST: Variety/Edit/5
        [HttpPost]
        public ActionResult Edit(int? SN, string varietyName)
        {
            try
            {
                if(SN == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (varietyName.Equals(string.Empty))
                {
                    ModelState.AddModelError("varietyName", "請輸入品種");
                }

                Comm_Variety variety = VarietyModel.getVarietyBySN(Convert.ToInt32(SN));

                if (!ModelState.IsValid)
                {
                    return View(variety);
                }
                
                variety.varietyName = varietyName;

                VarietyModel.Update(variety);

                return RedirectToAction("Index", new { kindSN = variety.kindSN});
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public JsonResult getVarietyList(int kindSN)
        {
            List<Comm_Variety> varietyList = new List<Comm_Variety>();
            if (kindSN != 0)
            {
                varietyList = VarietyModel.getVarietyListByKindSN(kindSN);
                varietyList.Insert(0, new Comm_Variety() { SN = 0, varietyName = "品種" });
            }
            else
            {
                varietyList.Add(new Comm_Variety() { SN=0, varietyName="品種"});
            }

            return Json(varietyList, JsonRequestBehavior.AllowGet);
        }
    }
}
