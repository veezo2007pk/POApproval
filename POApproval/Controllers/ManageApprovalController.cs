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
   
    public class ManageApprovalController : Controller
    {
        dbSASAApprovalEntities db = new dbSASAApprovalEntities();
        ManageApprovalDB ManageApprovalDB = new ManageApprovalDB();
       
        //[Authorize]
        public ActionResult ManageApprovalList()
        {
            var data = db.procSelectManageApproval().ToList();
            return View(data);
        }
       
        //[Authorize]
        public ActionResult AddManageApproval()
        {
            PopulateDropdown();
            return View();
        }
       
        
        public JsonResult GetUser()
        {
            List<procGetAllUsers_Result> users = db.procGetAllUsers().OrderBy(s => s.fullname).ToList();



            return Json(users, JsonRequestBehavior.AllowGet);
          
        }
       
        public JsonResult GetApprovalLevel()
        {
            List<procCmbApprovalLevel_Result> approvalLevels = db.procCmbApprovalLevel().OrderBy(s => s.ApprovalLevel).ToList();



            return Json(approvalLevels, JsonRequestBehavior.AllowGet);
        }
       
        //[Authorize]
        public ActionResult UpdateManageApproval(int ID)
        {
            PopulateDropdown();          
            var ManageApprovalInfo = db.tblManageApprovals.FirstOrDefault(s => s.intManageApprovalCode == ID);
            if (ManageApprovalInfo != null)
            {
                ManageApprovalViewModel objManageApproval = new ManageApprovalViewModel()
                {
                  intApprovalLevelCode= ManageApprovalInfo.intApprovalLevelCode,
                  bolIsActive= ManageApprovalInfo.bolIsActive,
                  dtCreatedAt= ManageApprovalInfo.dtCreatedAt,
                  dtModifyAt= ManageApprovalInfo.dtModifyAt,
                  intCreatedByCode= ManageApprovalInfo.intCreatedByCode,
                  intManageApprovalCode= ManageApprovalInfo.intManageApprovalCode,
                  intModifyBy= ManageApprovalInfo.intModifyBy,
                  intUserCode= ManageApprovalInfo.intUserCode,
                  numFromApprovalAmount= ManageApprovalInfo.numFromApprovalAmount,
                  numToApprovalAmount= ManageApprovalInfo.numToApprovalAmount
                };
                return View(objManageApproval);
            }
            return View();
        }

        public void PopulateDropdown()
        {
          

                //var user = db.tblUsers.OrderBy(s => s.logon_user_id);
                //ViewBag.userList = new SelectList(user, "intUserCode", "logon_user_id");

            var user = db.procGetAllUsers().OrderBy(s => s.fullname);
            ViewBag.userList = new SelectList(user, "usercode", "fullname");

            var approverLevel = db.tblApprovalLevels.OrderBy(s => s.strApprovalLevelName);
            ViewBag.approverLevelList = new SelectList(approverLevel, "intApprovalLevelCode", "strApprovalLevelName");




        }

        /// <summary>  
        ///   
        /// Get All ManageApproval  
        /// </summary>  
        /// <returns></returns>  
       
        //[Authorize]
        public ActionResult Get_AllManageApproval()
        {
            //using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
            //{

            //    return Json(ManageApprovalDB.ListAll(), JsonRequestBehavior.AllowGet);
            //}
            var data = db.procSelectManageApproval().ToList();
            return View(data);
        }
        /// <summary>  
        /// Get ManageApproval With Id  
        /// </summary>  
        /// <param name="Id"></param>  
        /// <returns></returns>  
        //public JsonResult Get_ManageApprovalById(string Id)
        //{
        //    using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
        //    {
        //        int EmpId = int.Parse(Id);
        //        return Json(Obj.ManageApprovals.Find(EmpId), JsonRequestBehavior.AllowGet);
        //    }
        //}
        /// <summary>  
        /// Insert New ManageApproval  
        /// </summary>  
        /// <param name="ManageApproval"></param>  
        /// <returns></returns>  
        public int Insert_ManageApproval(tblManageApproval ManageApproval)
        {
            var checkUserManageApprovalExist = db.tblManageApprovals.Where(x => x.intUserCode == ManageApproval.intUserCode && x.intApprovalLevelCode == ManageApproval.intApprovalLevelCode && x.numFromApprovalAmount == ManageApproval.numFromApprovalAmount && x.numToApprovalAmount == ManageApproval.numToApprovalAmount).FirstOrDefault();
            if (checkUserManageApprovalExist != null)
            {
                return 2;
            }
            if (ManageApproval != null)
            {
                using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                {

                    return ManageApprovalDB.Add(ManageApproval);
                }
            }
            else
            {
                return 0;
            }
        }
        [HttpPost]
        /// <summary>  
        /// Delete ManageApproval Information  
        /// </summary>  
        /// <param name="Emp"></param>  
        /// <returns></returns>  
        public string Delete_ManageApproval(tblManageApproval ManageApproval)
        {
            if (ManageApproval != null)
            {
                using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                {
                    var Emp_ = Obj.Entry(ManageApproval);
                    if (Emp_.State == System.Data.Entity.EntityState.Detached)
                    {
                        Obj.tblManageApprovals.Attach(ManageApproval);
                        Obj.tblManageApprovals.Remove(ManageApproval);
                    }
                    Obj.SaveChanges();
                    return "ManageApproval Deleted Successfully";
                }
            }
            else
            {
                return "ManageApproval Not Deleted! Try Again";
            }
        }
        /// <summary>  
        /// Update ManageApproval Information  
        /// </summary>  
        /// <param name="Emp"></param>  
        /// <returns></returns>  
        public int Update_ManageApproval(tblManageApproval ManageApproval)
        {
            var Approval = db.tblManageApprovals.Where(x => x.intManageApprovalCode == ManageApproval.intManageApprovalCode).FirstOrDefault();

            if (Approval.intApprovalLevelCode != ManageApproval.intApprovalLevelCode)
            {
                var checkUserManageApprovalExist = db.tblManageApprovals.Where(x => x.intUserCode == ManageApproval.intUserCode && x.intApprovalLevelCode == ManageApproval.intApprovalLevelCode && x.numFromApprovalAmount == ManageApproval.numFromApprovalAmount && x.numToApprovalAmount == ManageApproval.numToApprovalAmount).FirstOrDefault();
                if (checkUserManageApprovalExist != null)
                {
                    return 2;
                }
            }
            if (ManageApproval != null)
            {
                using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                {
                    return ManageApprovalDB.Update(ManageApproval);
                }
            }
            else
            {
                return 0;
            }
        }
    }
}