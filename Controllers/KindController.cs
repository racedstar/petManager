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
    public class KindController : Controller
    {
        // GET: Kind
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            List<Comm_Kind> kindList = KindModel.getKindList();
            var result = kindList.OrderBy(o => o.SN).ToPagedList();

            return View(result);
        }

        // GET: Kind/Create
        public ActionResult Create()
        {            
            return View();
        }

        // POST: Kind/Create
        [HttpPost]
        public ActionResult Create(string kindName)
        {
            try
            {
                if (kindName.Equals(string.Empty))
                {
                    ModelState.AddModelError("kindName", "請輸入物種名稱");
                }
                Comm_Kind kind = new Comm_Kind();
                kind.kindName = kindName;

                KindModel.Create(kind);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Kind/Edit/5
        public ActionResult Edit(int? SN)
        {
            if(SN == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comm_Kind kind = KindModel.getKindBySN(Convert.ToInt32(SN));
            return View(kind);
        }

        // POST: Kind/Edit/5
        [HttpPost]
        public ActionResult Edit(int? SN, string kindName)
        {
            try
            {
                if(SN == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (kindName.Equals(string.Empty))
                {
                    ModelState.AddModelError("kindName", "請輸入物種");
                }

                Comm_Kind kind = KindModel.getKindBySN(Convert.ToInt32(SN));

                if (!ModelState.IsValid)
                {
                    return View(kind);
                }

                kind.kindName = kindName;

                KindModel.Update(kind);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
