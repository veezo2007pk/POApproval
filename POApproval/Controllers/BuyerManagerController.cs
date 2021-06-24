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
   
    public class BuyerManagerController : Controller
    {
        dbSASAApprovalEntities db = new dbSASAApprovalEntities();
        BuyerManagerDB BuyerManagerDB = new BuyerManagerDB();
       
        [Authorize]
        public ActionResult BuyerManagerList()
        {
            var data = db.procSelectBuyerManager().ToList();
            return View(data);
        }
       
        [Authorize]
        public ActionResult AddBuyerManager()
        {
            PopulateDropdown();
            return View();
        }
       
        [HttpPost]
        public JsonResult GetUser()
        {
            List<SelectListItem> users = new List<SelectListItem>();
        
            for (int i = 0; i < db.procSelectUser().ToList().Count; i++)
            {
                //var data = db.tblDepartments.Where(x => x.intDepartmentCode == ID).FirstOrDefault();
                //bool selected = false;
                //if (ID != null) {
                //    if(ID== Convert.ToInt32(db.tblDepartments.ToList()[i].intDepartmentCode.ToString()))
                //    {
                //        selected = false;
                //    }
                //} 
                users.Add(new SelectListItem
                    {
                        Value = db.procSelectUser().ToList()[i].UserCode.ToString(),
                        Text = db.procSelectUser().ToList()[i].Username
                        //Selected=selected
                    });
                
                
            }

            return Json(users, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetBuyer()
        {
            List<SelectListItem> buyers = new List<SelectListItem>();

            for (int i = 0; i < db.procSelectBuyer().ToList().Count; i++)
            {
                //var data = db.tblDepartments.Where(x => x.intDepartmentCode == ID).FirstOrDefault();
                //bool selected = false;
                //if (ID != null) {
                //    if(ID== Convert.ToInt32(db.tblDepartments.ToList()[i].intDepartmentCode.ToString()))
                //    {
                //        selected = false;
                //    }
                //} 
                buyers.Add(new SelectListItem
                {
                    Value = db.procSelectBuyer().ToList()[i].BuyeCode.ToString(),
                    Text = db.procSelectBuyer().ToList()[i].BuyerName
                    //Selected=selected
                });


            }

            return Json(buyers, JsonRequestBehavior.AllowGet);
        }
       
        [Authorize]
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
          

                var user = db.procSelectUser().OrderBy(s => s.Username);
                ViewBag.userList = new SelectList(user, "UserCode", "Username");

            var buyer = db.procSelectBuyer().OrderBy(s => s.BuyerName);
            ViewBag.buyerList = new SelectList(buyer, "BuyeCode", "BuyerName");




        }

        /// <summary>  
        ///   
        /// Get All BuyerManager  
        /// </summary>  
        /// <returns></returns>  
       
        [Authorize]
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
        /// <summary>  
        /// Delete BuyerManager Information  
        /// </summary>  
        /// <param name="Emp"></param>  
        /// <returns></returns>  
        //public string Delete_BuyerManager(BuyerManager Emp)
        //{
        //    if (Emp != null)
        //    {
        //        using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
        //        {
        //            var Emp_ = Obj.Entry(Emp);
        //            if (Emp_.State == System.Data.Entity.EntityState.Detached)
        //            {
        //                Obj.BuyerManagers.Attach(Emp);
        //                Obj.BuyerManagers.Remove(Emp);
        //            }
        //            Obj.SaveChanges();
        //            return "BuyerManager Deleted Successfully";
        //        }
        //    }
        //    else
        //    {
        //        return "BuyerManager Not Deleted! Try Again";
        //    }
        //}
        /// <summary>  
        /// Update BuyerManager Information  
        /// </summary>  
        /// <param name="Emp"></param>  
        /// <returns></returns>  
        public int Update_BuyerManager(tblBuyerDetail BuyerManager)
        {
            //var Approval = db.tblBuyerDetails.Where(x => x.intBuyerManagerCode == BuyerManager.intBuyerManagerCode).FirstOrDefault();

            //if (Approval.intApprovalLevelCode != BuyerManager.intApprovalLevelCode)
            //{
            //    var checkUserBuyerManagerExist = db.tblBuyerManagers.Where(x => x.intUserCode == BuyerManager.intUserCode && x.intApprovalLevelCode == BuyerManager.intApprovalLevelCode && x.numFromApprovalAmount == BuyerManager.numFromApprovalAmount && x.numToApprovalAmount == BuyerManager.numToApprovalAmount).FirstOrDefault();
            //    if (checkUserBuyerManagerExist != null)
            //    {
            //        return 2;
            //    }
            //}
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