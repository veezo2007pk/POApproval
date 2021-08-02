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
        public List<procUserMenu_Result> GetUserMenus(String userCode)
        {

            List<procUserMenu_Result> GetUserMenus = db.procUserMenu(userCode).ToList();

            return GetUserMenus;

        }
        public ActionResult ManageApprovalList()
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies == null && string.IsNullOrEmpty(Session["intUserCode"] as string))
            {
                return RedirectToAction("Login", "Account");
            }
            else if (!string.IsNullOrEmpty(Session["intUserCode"] as string))
            {
                List<procUserMenu_Result> menus = GetUserMenus(Session["intUserCode"].ToString());

                foreach (var item in menus)
                {


                    var data = menus.Where(x => x.menucode == item.menucode).FirstOrDefault();
                    var link = data.menulink.Split('/');
                    if (link[1].ToString() == "ManageApprovalList")
                    {
                        var data1 = db.procSelectManageApproval().ToList();
                        return View(data1);




                    }



                }
            }
            else
            {
                List<procUserMenu_Result> menus = GetUserMenus(reqCookies["intUserCode"].ToString());

                foreach (var item in menus)
                {


                    var data = menus.Where(x => x.menucode == item.menucode).FirstOrDefault();
                    var link = data.menulink.Split('/');
                    if (link[1].ToString() == "ManageApprovalList")
                    {
                        var data1 = db.procSelectManageApproval().ToList();
                        return View(data1);




                    }



                }
            }
            
            return RedirectToAction("AccessDenied", "Errors");
          
        }
        public JsonResult GetBuyer(string ID)
        {
            
              
                List<procSelectBuyerManageApproval_Result> buyers = db.procSelectBuyerManageApproval(ID).OrderBy(s => s.BuyerName).ToList();




                return Json(buyers, JsonRequestBehavior.AllowGet);

          
        }


        //[Authorize]
        public ActionResult AddManageApproval()
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies == null && string.IsNullOrEmpty(Session["intUserCode"] as string))
            {
                return RedirectToAction("Login", "Account");
            }
            else if (!string.IsNullOrEmpty(Session["intUserCode"] as string))
            {
                List<procUserMenu_Result> menus = GetUserMenus(Session["intUserCode"].ToString());

                foreach (var item in menus)
                {


                    var data = menus.Where(x => x.menucode == item.menucode).FirstOrDefault();
                    var link = data.menulink.Split('/');
                    if (link[1].ToString() == "ManageApprovalList")
                    {
                        PopulateDropdown();
                        return View();




                    }



                }
            }
            else
            {
                List<procUserMenu_Result> menus = GetUserMenus(reqCookies["intUserCode"].ToString());

                foreach (var item in menus)
                {


                    var data = menus.Where(x => x.menucode == item.menucode).FirstOrDefault();
                    var link = data.menulink.Split('/');
                    if (link[1].ToString() == "ManageApprovalList")
                    {
                        PopulateDropdown();
                        return View();




                    }



                }
            }
           
            return RedirectToAction("AccessDenied", "Errors");
            
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
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies == null && string.IsNullOrEmpty(Session["intUserCode"] as string))
            {
                return RedirectToAction("Login", "Account");
            }
            else if (!string.IsNullOrEmpty(Session["intUserCode"] as string))
            {
                List<procUserMenu_Result> menus = GetUserMenus(Session["intUserCode"].ToString());

                foreach (var item in menus)
                {


                    var data = menus.Where(x => x.menucode == item.menucode).FirstOrDefault();
                    var link = data.menulink.Split('/');
                    if (link[1].ToString() == "ManageApprovalList")
                    {
                        PopulateDropdown();
                        var ManageApprovalInfo = db.tblManageApprovals.FirstOrDefault(s => s.intManageApprovalCode == ID);
                        if (ManageApprovalInfo != null)
                        {
                            ManageApprovalViewModel objManageApproval = new ManageApprovalViewModel()
                            {
                                intApprovalLevelCode = ManageApprovalInfo.intApprovalLevelCode,
                                bolIsActive = ManageApprovalInfo.bolIsActive,
                                dtCreatedAt = ManageApprovalInfo.dtCreatedAt,
                                intBuyerCode = ManageApprovalInfo.intBuyerCode,
                                dtModifyAt = ManageApprovalInfo.dtModifyAt,
                                intCreatedByCode = ManageApprovalInfo.intCreatedByCode,
                                intManageApprovalCode = ManageApprovalInfo.intManageApprovalCode,
                                intModifyBy = ManageApprovalInfo.intModifyBy,
                                intUserCode = ManageApprovalInfo.intUserCode,
                                numFromApprovalAmount = ManageApprovalInfo.numFromApprovalAmount,
                                numToApprovalAmount = ManageApprovalInfo.numToApprovalAmount
                            };
                            return View(objManageApproval);
                        }
                        return View();




                    }



                }
            }
            else
            {
                List<procUserMenu_Result> menus = GetUserMenus(reqCookies["intUserCode"].ToString());

                foreach (var item in menus)
                {


                    var data = menus.Where(x => x.menucode == item.menucode).FirstOrDefault();
                    var link = data.menulink.Split('/');
                    if (link[1].ToString() == "ManageApprovalList")
                    {
                        PopulateDropdown();
                        var ManageApprovalInfo = db.tblManageApprovals.FirstOrDefault(s => s.intManageApprovalCode == ID);
                        if (ManageApprovalInfo != null)
                        {
                            ManageApprovalViewModel objManageApproval = new ManageApprovalViewModel()
                            {
                                intApprovalLevelCode = ManageApprovalInfo.intApprovalLevelCode,
                                bolIsActive = ManageApprovalInfo.bolIsActive,
                                dtCreatedAt = ManageApprovalInfo.dtCreatedAt,
                                intBuyerCode = ManageApprovalInfo.intBuyerCode,
                                dtModifyAt = ManageApprovalInfo.dtModifyAt,
                                intCreatedByCode = ManageApprovalInfo.intCreatedByCode,
                                intManageApprovalCode = ManageApprovalInfo.intManageApprovalCode,
                                intModifyBy = ManageApprovalInfo.intModifyBy,
                                intUserCode = ManageApprovalInfo.intUserCode,
                                numFromApprovalAmount = ManageApprovalInfo.numFromApprovalAmount,
                                numToApprovalAmount = ManageApprovalInfo.numToApprovalAmount
                            };
                            return View(objManageApproval);
                        }
                        return View();




                    }



                }
            }
           
            return RedirectToAction("AccessDenied", "Errors");
            
        }

        public void PopulateDropdown()
        {
          

                //var user = db.tblUsers.OrderBy(s => s.logon_user_id);
                //ViewBag.userList = new SelectList(user, "intUserCode", "logon_user_id");

            var user = db.procGetAllUsers().OrderBy(s => s.fullname);
            ViewBag.userList = new SelectList(user, "usercode", "fullname");

            var approverLevel = db.tblApprovalLevels.OrderBy(s => s.strApprovalLevelName);
            ViewBag.approverLevelList = new SelectList(approverLevel, "intApprovalLevelCode", "strApprovalLevelName");
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            int ID = 0;
            if (reqCookies != null)
            {
                ID = Convert.ToInt32(reqCookies["intUserCode"].ToString());

            }
            else
            {
                ID = Convert.ToInt32(Session["intUserCode"].ToString());

            }




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
            var checkUserManageApprovalExist = db.tblManageApprovals.Where(x => x.intUserCode == ManageApproval.intUserCode && x.intBuyerCode == ManageApproval.intBuyerCode && x.intApprovalLevelCode == ManageApproval.intApprovalLevelCode && x.numFromApprovalAmount == ManageApproval.numFromApprovalAmount && x.numToApprovalAmount == ManageApproval.numToApprovalAmount && x.bolIsActive == ManageApproval.bolIsActive).FirstOrDefault();
            if (checkUserManageApprovalExist != null)
            {
                return 2;
            }
            // checkUserManageApprovalExist = db.tblManageApprovals.Where(x => x.intUserCode == ManageApproval.intUserCode && x.intBuyerCode == ManageApproval.intBuyerCode && x.intApprovalLevelCode == ManageApproval.intApprovalLevelCode).FirstOrDefault();
            //if (checkUserManageApprovalExist != null)
            //{
            //    return 2;
            //}
            //var checkUserManageApprovalRange = db.tblManageApprovals.Where(x => x.intUserCode == ManageApproval.intUserCode && x.intBuyerCode == ManageApproval.intBuyerCode && x.intApprovalLevelCode == ManageApproval.intApprovalLevelCode).FirstOrDefault();
            //if (checkUserManageApprovalRange != null)
            //{
            //   if(ManageApproval.numFromApprovalAmount<checkUserManageApprovalRange.numToApprovalAmount)
            //    {
            //        return 3;
            //    }
            //   if (ManageApproval.numToApprovalAmount < checkUserManageApprovalRange.numToApprovalAmount)
            //    {
            //        return 3;
            //    }
            //}
            var checkUserManageApprovalRangeBuyer = db.tblManageApprovals.Where(x => x.intUserCode == ManageApproval.intUserCode && x.intBuyerCode == ManageApproval.intBuyerCode && x.intApprovalLevelCode == ManageApproval.intApprovalLevelCode).ToList();
            if (checkUserManageApprovalRangeBuyer != null)
            {
                foreach (var item in checkUserManageApprovalRangeBuyer)
                {
                    if (ManageApproval.numFromApprovalAmount < item.numToApprovalAmount)
                    {
                        return 3;
                    }
                    if (ManageApproval.numToApprovalAmount < item.numToApprovalAmount)
                    {
                        return 3;
                    }
                }

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
        public ActionResult Delete_ManageApproval(int ID)
        {
            tblManageApproval on = db.tblManageApprovals.Find(ID);
            db.tblManageApprovals.Remove(on);
            db.SaveChanges();
            return RedirectToAction("ManageApprovalList", "ManageApproval");
               
            
        }
        /// <summary>  
        /// Update ManageApproval Information  
        /// </summary>  
        /// <param name="Emp"></param>  
        /// <returns></returns>  
        public int Update_ManageApproval(tblManageApproval ManageApproval)
        {
            var checkUserManageApprovalExist = db.tblManageApprovals.Where(x => x.intUserCode == ManageApproval.intUserCode && x.intBuyerCode == ManageApproval.intBuyerCode && x.intApprovalLevelCode == ManageApproval.intApprovalLevelCode && x.numFromApprovalAmount == ManageApproval.numFromApprovalAmount && x.numToApprovalAmount == ManageApproval.numToApprovalAmount && x.bolIsActive == ManageApproval.bolIsActive).FirstOrDefault();
            if (checkUserManageApprovalExist != null)
            {
                return 2;
            }
            // checkUserManageApprovalExist = db.tblManageApprovals.Where(x => x.intUserCode == ManageApproval.intUserCode && x.intBuyerCode == ManageApproval.intBuyerCode && x.intApprovalLevelCode == ManageApproval.intApprovalLevelCode).FirstOrDefault();
            //if (checkUserManageApprovalExist != null)
            //{
            //    return 2;
            //}
            //var checkUserManageApprovalRange = db.tblManageApprovals.Where(x => x.intUserCode == ManageApproval.intUserCode && x.intBuyerCode == ManageApproval.intBuyerCode && x.intApprovalLevelCode == ManageApproval.intApprovalLevelCode).FirstOrDefault();
            //if (checkUserManageApprovalRange != null)
            //{
            //   if(ManageApproval.numFromApprovalAmount<checkUserManageApprovalRange.numToApprovalAmount)
            //    {
            //        return 3;
            //    }
            //   if (ManageApproval.numToApprovalAmount < checkUserManageApprovalRange.numToApprovalAmount)
            //    {
            //        return 3;
            //    }
            //}
            var checkUserManageApprovalRangeBuyer = db.tblManageApprovals.Where(x => x.intUserCode == ManageApproval.intUserCode && x.intBuyerCode == ManageApproval.intBuyerCode && x.intApprovalLevelCode == ManageApproval.intApprovalLevelCode).ToList();
            if (checkUserManageApprovalRangeBuyer.Count > 1)
            {
                foreach (var item in checkUserManageApprovalRangeBuyer)
                {
                    if (ManageApproval.numFromApprovalAmount < item.numToApprovalAmount)
                    {
                        return 3;
                    }
                    if (ManageApproval.numToApprovalAmount < item.numToApprovalAmount)
                    {
                        return 3;
                    }
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