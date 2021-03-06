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

        //[Authorize]
        public List<procUserMenu_Result> GetUserMenus(String userCode)
        {

            List<procUserMenu_Result> GetUserMenus = db.procUserMenu(userCode).ToList();

            return GetUserMenus;
        }

        public ActionResult BuyerManagerList()
        {
            if (TempData["errormessage"] != null)
            {
                ViewBag.geterror = TempData["errormessage"].ToString();
            }
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
                    if (link[1].ToString() == "BuyerManagerList")
                    {
                        var data1 = db.procSelectBuyerManager().ToList();
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
                    if (link[1].ToString() == "BuyerManagerList")
                    {
                        var data1 = db.procSelectBuyerManager().ToList();
                        return View(data1);



                    }



                }
            }
                return RedirectToAction("AccessDenied", "Errors");
            
        }

        //[Authorize]
        public ActionResult AddBuyerManager()
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
                    if (link[1].ToString() == "BuyerManagerList")
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
                    if (link[1].ToString() == "BuyerManagerList")
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
       
        public JsonResult GetBuyer()
        {
            List<procSelectBuyer_Result> buyers = db.procSelectBuyer().OrderBy(s => s.BuyerName).ToList();
          



            return Json(buyers, JsonRequestBehavior.AllowGet);
        }
       
        //[Authorize]
        public ActionResult UpdateBuyerManager(int ID)
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
                    if (link[1].ToString() == "BuyerManagerList")
                    {
                        PopulateDropdown();
                        var BuyerManagerInfo = db.tblBuyerDetails.FirstOrDefault(s => s.intBuyerDetailCode == ID);
                        if (BuyerManagerInfo != null)
                        {
                            
                            BuyerManagerViewModel objBuyerManager = new BuyerManagerViewModel()
                            {

                                dtCreatedAt = BuyerManagerInfo.dtCreatedAt,
                                dtModifyAt = BuyerManagerInfo.dtModifyAt,
                                intCreatedByCode = BuyerManagerInfo.intCreatedByCode,
                                intBuyerCode = BuyerManagerInfo.intBuyerCode,
                                intModifyBy = BuyerManagerInfo.intModifyBy,
                                intUserCode = BuyerManagerInfo.intUserCode,
                                bolIsActive = BuyerManagerInfo.bolIsActive,
                                intBuyerDetailCode = BuyerManagerInfo.intBuyerDetailCode


                            };

                            return View(objBuyerManager);
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
                    if (link[1].ToString() == "BuyerManagerList")
                    {
                        PopulateDropdown();
                        var BuyerManagerInfo = db.tblBuyerDetails.FirstOrDefault(s => s.intBuyerDetailCode == ID);
                        if (BuyerManagerInfo != null)
                        {
                            BuyerManagerViewModel objBuyerManager = new BuyerManagerViewModel()
                            {

                                dtCreatedAt = BuyerManagerInfo.dtCreatedAt,
                                dtModifyAt = BuyerManagerInfo.dtModifyAt,
                                intCreatedByCode = BuyerManagerInfo.intCreatedByCode,
                                intBuyerCode = BuyerManagerInfo.intBuyerCode,
                                intModifyBy = BuyerManagerInfo.intModifyBy,
                                intUserCode = BuyerManagerInfo.intUserCode,
                                bolIsActive = BuyerManagerInfo.bolIsActive,
                                intBuyerDetailCode = BuyerManagerInfo.intBuyerDetailCode


                            };

                            return View(objBuyerManager);
                        }

                        return View();



                    }



                }
            }
           
            return RedirectToAction("AccessDenied", "Errors");
            
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
            var checkUserBuyerManagerExist = db.tblBuyerDetails.Where(x => x.intUserCode == BuyerManager.intUserCode && x.intBuyerCode == BuyerManager.intBuyerCode && x.bolIsActive == BuyerManager.bolIsActive).FirstOrDefault();
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
        public ActionResult Delete_BuyerManager(int ID)
        {
            tblBuyerDetail on = db.tblBuyerDetails.Find(ID);
            List <tblManageApproval> manageApproval1 = db.tblManageApprovals.Where(x => x.intBuyerCode == on.intBuyerCode.ToString()).ToList();
            List<tblPO> pOs = db.tblPOes.Where(x => x.staff_code == on.intBuyerCode.ToString()&&(x.PO_Status!="Pending" || x.PO_Status!= "Rejected")).ToList();
            if (manageApproval1.Count > 0 || pOs.Count>0)
            {
                TempData["errormessage"]  = "Buyer already linked with approval level";
                return RedirectToAction("BuyerManagerList", "BuyerManager");
               
            }
            
            db.tblBuyerDetails.Remove(on);
            db.SaveChanges();
            return RedirectToAction("BuyerManagerList", "BuyerManager");
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
                
                if (BuyerManager.bolIsActive == checkUserBuyerManagerExist.bolIsActive && BuyerManager.intBuyerCode == checkUserBuyerManagerExist.intBuyerCode)
                {
                    return 2;
                }
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