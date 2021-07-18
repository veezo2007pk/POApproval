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
    [NoDirectAccess]
    public class BuyerManagerController : Controller
    {
        dbSASAApprovalEntities db = new dbSASAApprovalEntities();
        BuyerManagerDB BuyerManagerDB = new BuyerManagerDB();
       
        //[Authorize]
        public ActionResult BuyerManagerList()
        {
            var data = db.procSelectBuyerManager().ToList();
            return View(data);
        }
       
        //[Authorize]
        public ActionResult AddBuyerManager()
        {
            PopulateDropdown();
            return View();
        }
       
       
        public JsonResult GetUser()
        {
            List<procGetAllUsers_Result> users = db.procGetAllUsers().OrderBy(s => s.fullname).ToList();

           

            return Json(users, JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult GetBuyer()
        {
            List<procSelectBuyer_Result> buyers = db.procSelectBuyer().OrderBy(s => s.BuyerName).ToList();
          



            return Json(buyers, JsonRequestBehavior.AllowGet);
        }
       
        //[Authorize]
        public ActionResult UpdateBuyerManager(int ID)
        {
            PopulateDropdown();          
            var BuyerManagerInfo = db.tblBuyerDetails.FirstOrDefault(s => s.intBuyerDetailCode == ID);
            if (BuyerManagerInfo != null)
            {
                BuyerManagerViewModel objBuyerManager = new BuyerManagerViewModel()
                {
                 
                  dtCreatedAt= BuyerManagerInfo.dtCreatedAt,
                  dtModifyAt= BuyerManagerInfo.dtModifyAt,
                  intCreatedByCode= BuyerManagerInfo.intCreatedByCode,
                 intBuyerCode= BuyerManagerInfo.intBuyerCode,
                  intModifyBy = BuyerManagerInfo.intModifyBy,
                  intUserCode= BuyerManagerInfo.intUserCode,
                  intBuyerDetailCode=BuyerManagerInfo.intBuyerDetailCode
                

                };
               
                return View(objBuyerManager);
            }

            return View();
        }

        public void PopulateDropdown()
        {
          

                var user = db.procGetAllUsers().OrderBy(s => s.fullname);
                ViewBag.userList = new SelectList(user, "usercode", "fullname");

            var buyer = db.procGetAllBuyersByStaffType("BUYER").OrderBy(s => s.fullname);
            ViewBag.buyerList = new SelectList(buyer, "usercode", "fullname");




        }

        /// <summary>  
        ///   
        /// Get All BuyerManager  
        /// </summary>  
        /// <returns></returns>  
       
        //[Authorize]
        public ActionResult Get_AllBuyerManager()
        {
            //using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
            //{

            //    return Json(BuyerManagerDB.ListAll(), JsonRequestBehavior.AllowGet);
            //}
            var data = db.procSelectBuyerManager().ToList();
            return View(data);
        }
        /// <summary>  
        /// Get BuyerManager With Id  
        /// </summary>  
        /// <param name="Id"></param>  
        /// <returns></returns>  
        //public JsonResult Get_BuyerManagerById(string Id)
        //{
        //    using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
        //    {
        //        int EmpId = int.Parse(Id);
        //        return Json(Obj.BuyerManagers.Find(EmpId), JsonRequestBehavior.AllowGet);
        //    }
        //}
        /// <summary>  
        /// Insert New BuyerManager  
        /// </summary>  
        /// <param name="BuyerManager"></param>  
        /// <returns></returns>  
        public int Insert_BuyerManager(tblBuyerDetail BuyerManager)
        {
            var checkUserBuyerManagerExist = db.tblBuyerDetails.Where(x => x.intUserCode == BuyerManager.intUserCode && x.intBuyerCode == BuyerManager.intBuyerCode).FirstOrDefault();
            if (checkUserBuyerManagerExist != null)
            {
                return 2;
            }
            if (BuyerManager != null)
            {
                using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                {

                    return BuyerManagerDB.Add(BuyerManager);
                }
            }
            else
            {
                return 0;
            }
        }
        [HttpPost]
        /// <summary>  
        /// Delete BuyerManager Information  
        /// </summary>  
        /// <param name="Emp"></param>  
        /// <returns></returns>  
        public string Delete_BuyerManager(tblBuyerDetail Emp)
        {
            if (Emp != null)
            {
                using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                {
                    var Emp_ = Obj.Entry(Emp);
                    if (Emp_.State == System.Data.Entity.EntityState.Detached)
                    {
                        Obj.tblBuyerDetails.Attach(Emp);
                        Obj.tblBuyerDetails.Remove(Emp);
                    }
                    Obj.SaveChanges();
                    return "BuyerManager Deleted Successfully";
                }
            }
            else
            {
                return "BuyerManager Not Deleted! Try Again";
            }
        }
        /// <summary>  
        /// Update BuyerManager Information  
        /// </summary>  
        /// <param name="Emp"></param>  
        /// <returns></returns>  
        public int Update_BuyerManager(tblBuyerDetail BuyerManager)
        {
            var checkUserBuyerManagerExist = db.tblBuyerDetails.Where(x => x.intUserCode == BuyerManager.intUserCode && x.intBuyerCode == BuyerManager.intBuyerCode).FirstOrDefault();
            if (checkUserBuyerManagerExist != null)
            {
                return 2;
            }
            if (BuyerManager != null)
            {
                using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                {
                    return BuyerManagerDB.Update(BuyerManager);
                }
            }
            else
            {
                return 0;
            }
        }
    }
}