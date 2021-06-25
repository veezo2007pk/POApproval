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
       
        [HttpPost]
        public JsonResult GetUser()
        {
            List<SelectListItem> users = new List<SelectListItem>();
        
            for (int i = 0; i < db.tblUsers.ToList().Count; i++)
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
                        Value = db.tblUsers.ToList()[i].intUserCode.ToString(),
                        Text = db.tblUsers.ToList()[i].logon_user_id
                        //Selected=selected
                    });
                
                
            }

            return Json(users, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetApprovalLevel()
        {
            List<SelectListItem> approvalLevels = new List<SelectListItem>();

            for (int i = 0; i < db.tblApprovalLevels.ToList().Count; i++)
            {
                //var data = db.tblDepartments.Where(x => x.intDepartmentCode == ID).FirstOrDefault();
                //bool selected = false;
                //if (ID != null) {
                //    if(ID== Convert.ToInt32(db.tblDepartments.ToList()[i].intDepartmentCode.ToString()))
                //    {
                //        selected = false;
                //    }
                //} 
                approvalLevels.Add(new SelectListItem
                {
                    Value = db.tblApprovalLevels.ToList()[i].intApprovalLevelCode.ToString(),
                    Text = db.tblApprovalLevels.ToList()[i].strApprovalLevelName
                    //Selected=selected
                });


            }

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
          

                var user = db.tblUsers.OrderBy(s => s.logon_user_id);
                ViewBag.userList = new SelectList(user, "intUserCode", "logon_user_id");

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
        /// <summary>  
        /// Delete ManageApproval Information  
        /// </summary>  
        /// <param name="Emp"></param>  
        /// <returns></returns>  
        public string Delete_ManageApproval(tblManageApproval Emp)
        {
            if (Emp != null)
            {
                using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                {
                    var Emp_ = Obj.Entry(Emp);
                    if (Emp_.State == System.Data.Entity.EntityState.Detached)
                    {
                        Obj.tblManageApprovals.Attach(Emp);
                        Obj.tblManageApprovals.Remove(Emp);
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