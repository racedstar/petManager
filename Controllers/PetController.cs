using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetGroomingManager.Models;
using X.PagedList;

namespace PetGroomingManager.Controllers
{
    public class PetController : Controller
    {
        // GET: Pet
        public ActionResult Index(string keyWord="")
        {
            List<Vw_Pet> petList = PetModel.getPetList();

            if (!keyWord.Equals(string.Empty))
            {
                petList = PetModel.getPetListByKeyWord(keyWord);
            }

            var result = petList.OrderBy(o => o.petSN).ToPagedList();

            return View(result);
        }

        public ActionResult BeautyRoom(string keyWord="")
        {
            List<Vw_Pet> petList = PetModel.getPetListByisState();

            if (!keyWord.Equals(string.Empty))
            {
                petList = PetModel.getPetListByKeyWord(keyWord).Where(o => o.isState == true).ToList();
            }

            var result = petList.OrderBy(o => o.petSN).ToPagedList();

            return View(result);
        }

        // GET: Pet/Create
        public ActionResult Create()
        {
            List<Comm_Customer> customerList = CustomerModel.getCustomerList();
            List<Comm_Kind> kindList = KindModel.getKindList();
            List<Comm_Variety> varietyList = new List<Comm_Variety>();

            ViewBag.customerSN = new SelectList(customerList, "SN", "customerName");
            ViewBag.kindSN = new SelectList(kindList, "SN", "kindName");
            ViewBag.varietySN = new SelectList(varietyList, "SN", "varietyName");

            return View();
        }

        // POST: Pet/Create
        [HttpPost]
        public ActionResult Create(int? customerSN, string petName, int? petGender, int? kindSN, int? varietySN, string petRemark)
        {            
            if(customerSN == null)
            {
                ModelState.AddModelError("customerSN", "請選擇飼主");
            }

            #region Validation
            if (petName == string.Empty)
            {
                ModelState.AddModelError("petName", "請輸入寵物姓名");
            }
            if(petGender == null)
            {
                ModelState.AddModelError("petGender", "請選擇性別");
            }
            if(kindSN == null)
            {
                ModelState.AddModelError("kindSN", "請選擇物種");
            }
            if(varietySN == null)
            {                
                ModelState.AddModelError("varietySN", "請選擇品種");
            }
            if(!ModelState.IsValid)
            {
                List<Comm_Customer> customerList = CustomerModel.getCustomerList();
                List<Comm_Kind> kindList = KindModel.getKindList();
                List<Comm_Variety> varietyList = new List<Comm_Variety>();

                ViewBag.customerSN = new SelectList(customerList, "SN", "customerName");
                ViewBag.kindSN = new SelectList(kindList, "SN", "kindName");
                ViewBag.varietySN = new SelectList(varietyList, "SN", "varietyName");
                return View();
            }

            #endregion

            try
            {
                Boolean gender = false;
                
                if(petGender == 1)
                {
                    gender = true;
                }

                Comm_Pet pet = new Comm_Pet();
                pet.customerSN = Convert.ToInt32(customerSN);
                pet.petName = petName;
                pet.petGender = gender;
                pet.kindSN = Convert.ToInt32(kindSN);
                pet.varietySN = Convert.ToInt32(varietySN);
                pet.petRemark = petRemark;

                PetModel.Create(pet);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pet/Edit/5
        public ActionResult Edit(int? SN)
        {            
            if(SN == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comm_Pet pet = PetModel.getPetBySN(Convert.ToInt32(SN));

            if(pet == null)
            {
                return HttpNotFound();
            }

            List<Comm_Customer> customerList = CustomerModel.getCustomerList();
            List<Comm_Kind> kindList = KindModel.getKindList();
            List<Comm_Variety> varietyList = VarietyModel.getVarietyListByKindSN(pet.kindSN);

            ViewBag.customerSN = new SelectList(customerList, "SN", "customerName", pet.customerSN);
            ViewBag.kindSN = new SelectList(kindList, "SN", "kindName", pet.kindSN);
            ViewBag.varietySN = new SelectList(varietyList, "SN", "varietyName", pet.varietySN);

            return View(pet);
        }

        // POST: Pet/Edit/5
        [HttpPost]
        public ActionResult Edit(int? SN, int? customerSN, string petName, int? petGender, int? kindSN, int? varietySN, string petRemark)
        {
            try
            {                
                if(SN == null)
                {                    
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Comm_Pet pet = PetModel.getPetBySN(Convert.ToInt32(SN));

                if(pet == null)
                {
                    return HttpNotFound();
                }

                #region Validation
                if (customerSN == null)
                {                    
                    ModelState.AddModelError("customerSN", "請選擇飼主");
                }

                if (petName == string.Empty)
                {                    
                    ModelState.AddModelError("petName", "請輸入寵物名稱");
                }

                if (petGender == null)
                {                    
                    ModelState.AddModelError("petGender", "請選擇寵物性別");
                }

                if (kindSN == null)
                {                    
                    ModelState.AddModelError("kindSN", "請選擇物種");
                }

                if(varietySN == null)
                {                    
                    ModelState.AddModelError("varietySN", "請選擇品種");
                }

                if(!ModelState.IsValid)
                {
                    List<Comm_Customer> customerList = CustomerModel.getCustomerList();
                    List<Comm_Kind> kindList = KindModel.getKindList();
                    List<Comm_Variety> varietyList = VarietyModel.getVarietyListByKindSN(pet.kindSN);

                    ViewBag.customerSN = new SelectList(customerList, "SN", "customerName", pet.customerSN);
                    ViewBag.kindSN = new SelectList(kindList, "SN", "kindName", pet.kindSN);
                    ViewBag.varietySN = new SelectList(varietyList, "SN", "varietyName", pet.varietySN);
                    return View(pet);
                }
                #endregion

                Boolean gender = false;
                if (petGender == 1)
                {
                    gender = true;
                }
                
                pet.customerSN = Convert.ToInt32(customerSN);
                pet.petName = petName;
                pet.petGender = gender;
                pet.kindSN = Convert.ToInt32(kindSN);
                pet.varietySN = Convert.ToInt32(varietySN);
                pet.petRemark = petRemark;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int? SN)
        {
            if(SN == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Vw_Pet pet = PetModel.getVwPetBySN(Convert.ToInt32(SN));

            return View(pet);
        }

        public string changeState(int SN=0)
        {            
            Comm_Pet pet = PetModel.getPetBySN(SN);

            if(pet == null)
            {
                return "輸入的寵物編號有誤";
            }

            string txt = "進美容室";
            pet.isState = !pet.isState;

            PetModel.Update(pet);

            if(pet.isState == true)
            {
                txt = "離開美容室";
            }

            return txt;
        }
    }
}
