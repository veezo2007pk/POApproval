using POApproval.GlobalInfo;
using POApproval.Helper;
using POApproval.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POApproval.Controllers
{

    public class BuyerController : Controller
    {
        dbSASAApprovalEntities db = new dbSASAApprovalEntities();
        BuyerDB BuyerDB = new BuyerDB();

        [Authorize]
        public ActionResult BuyerList()
        {
            var data = db.procSelectBuyer().ToList();
            return View(data);
        }

        [Authorize]
        public ActionResult AddBuyer()
        {
            PopulateDropdown();
            return View();
        }
        //[Authorize]
        //[HttpPost]
        //public JsonResult GetDepartment(int? ID)
        //{
        //    List<SelectListItem> departments = new List<SelectListItem>();

        //    for (int i = 0; i < db.tblDepartments.ToList().Count; i++)
        //    {
        //        //var data = db.tblDepartments.Where(x => x.intDepartmentCode == ID).FirstOrDefault();
        //        //bool selected = false;
        //        //if (ID != null) {
        //        //    if(ID== Convert.ToInt32(db.tblDepartments.ToList()[i].intDepartmentCode.ToString()))
        //        //    {
        //        //        selected = false;
        //        //    }
        //        //} 
        //            departments.Add(new SelectListItem
        //            {
        //                Value = db.tblDepartments.ToList()[i].intDepartmentCode.ToString(),
        //                Text = db.tblDepartments.ToList()[i].strDepartmentName
        //                //Selected=selected
        //            });


        //    }

        //    return Json(departments, JsonRequestBehavior.AllowGet);
        //}

        [Authorize]
        public ActionResult UpdateBuyer(int ID)
        {
            PopulateDropdown();
            var BuyerInfo = db.tblBuyers.FirstOrDefault(s => s.intBuyerCode == ID);
            if (BuyerInfo != null)
            {
                BuyerViewModel objBuyer = new BuyerViewModel()
                {
                    intBuyerCode = BuyerInfo.intBuyerCode,
                    strBuyerName = BuyerInfo.strBuyerName,
                  
                    bolIsActive = BuyerInfo.bolIsActive
                 


                };

                return View(objBuyer);
            }

            return View();
        }
        public void PopulateDropdown()
        {


            var department = db.procCmbDepartment().OrderBy(s => s.Department);
            ViewBag.departmentList = new SelectList(department, "Code", "Department");




        }

        /// <summary>  
        ///   
        /// Get All Buyer  
        /// </summary>  
        /// <returns></returns>  

        [Authorize]
        public ActionResult Get_AllBuyer()
        {
            //using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
            //{

            //return Json(BuyerDB.ListAll(), JsonRequestBehavior.AllowGet);
            //}
            var data = db.procSelectBuyer().ToList();
            return View(data);
        }
        /// <summary>  
        /// Get Buyer With Id  
        /// </summary>  
        /// <param name="Id"></param>  
        /// <returns></returns>  
        //public JsonResult Get_BuyerById(string Id)
        //{
        //    using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
        //    {
        //        int EmpId = int.Parse(Id);
        //        return Json(Obj.Buyers.Find(EmpId), JsonRequestBehavior.AllowGet);
        //    }
        //}
        /// <summary>  
        /// Insert New Buyer  
        /// </summary>  
        /// <param name="Buyer"></param>  
        /// <returns></returns>  
        public int Insert_Buyer(tblBuyer Buyer)
        {
            var checkBuyerNameExist = db.tblBuyers.Where(x => x.strBuyerName == Buyer.strBuyerName).FirstOrDefault();

            if (checkBuyerNameExist != null)
            {
                return 2;
            }
            if (Buyer != null)
            {
                using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                {

                    return BuyerDB.Add(Buyer);
                }
            }
            else
            {
                return 0;
            }
        }
        /// <summary>  
        /// Delete Buyer Information  
        /// </summary>  
        /// <param name="Emp"></param>  
        /// <returns></returns>  
        //public string Delete_Buyer(Buyer Emp)
        //{
        //    if (Emp != null)
        //    {
        //        using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
        //        {
        //            var Emp_ = Obj.Entry(Emp);
        //            if (Emp_.State == System.Data.Entity.EntityState.Detached)
        //            {
        //                Obj.Buyers.Attach(Emp);
        //                Obj.Buyers.Remove(Emp);
        //            }
        //            Obj.SaveChanges();
        //            return "Buyer Deleted Successfully";
        //        }
        //    }
        //    else
        //    {
        //        return "Buyer Not Deleted! Try Again";
        //    }
        //}
        /// <summary>  
        /// Update Buyer Information  
        /// </summary>  
        /// <param name="Emp"></param>  
        /// <returns></returns>  
        public int Update_Buyer(tblBuyer Buyer)
        {
            var Buyers = db.tblBuyers.Where(x => x.intBuyerCode == Buyer.intBuyerCode).FirstOrDefault();

            
                //var checkBuyernameExist = db.tblBuyers.Where(x => x.strBuyerName == Buyer.strBuyerName).FirstOrDefault();

                //if (checkBuyernameExist != null)
                //{
                //    return 2;
                //}
           

            if (Buyer != null)
            {
                using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                {
                    return BuyerDB.Update(Buyer);
                }
            }
            else
            {
                return 0;
            }
        }
    }
}