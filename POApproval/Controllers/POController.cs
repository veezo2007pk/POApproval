using POApproval.Models;
using POApproval.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POApproval.Controllers
{

    public class POController : Controller
    {
        dbSASAApprovalEntities db = new dbSASAApprovalEntities();
        PODB PODB = new PODB();


        // GET: PO
        public ActionResult PORpt(string intPOCode)
        {
            ViewBag.intPOCode = intPOCode;
            return View();
        }
        //public ActionResult Index()
        //{
        //    return View(this.SearchPO(1));
        //}
      
        public tblPO GetPOModel(int ID)
        {
            tblPO POModel = db.tblPOes.Where(x => x.intPOCode == ID).FirstOrDefault();
            return POModel;
        }
        public List<tblPODetail> GetPODetailsModel(int ID)
        {
            List<tblPODetail> PODetailModel = db.tblPODetails.Where(x => x.intPOCode == ID).ToList();
            return PODetailModel;
        }
        public List<procGetUserApprovalLog_Result> GetPOHistoriesModel(int ID)
        {
            string userid = ID.ToString();
            List<procGetUserApprovalLog_Result> POHistoryModel = db.procGetUserApprovalLog(userid).ToList();
            return POHistoryModel;
        }
        public List<tblManageApproval> GetManageApprovalModel()
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            string ID = null;
            if (reqCookies != null)
            {
                ID = reqCookies["intUserCode"].ToString();

            }
            else
            {
                ID = Session["intUserCode"].ToString();

            }
            List<tblManageApproval> ManageApprovalModel = db.tblManageApprovals.Where(x => x.intUserCode == ID).ToList();
            return ManageApprovalModel;
        }
        //[Authorize]
        public ActionResult ReviewPO(int ID)
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
                    if (link[1].ToString() == "SearchPO")
                    {
                        List<tblStatu> statusList = db.tblStatus.ToList();
                        ViewBag.Status = new MultiSelectList(statusList, "strStatusName", "strStatusName");

                        var data1 = new List<procSearchPO_Result>();

                        if (Session["SuperAdmin"].ToString() == "Y")
                        {
                            int userCode = Convert.ToInt32(Session["intUserCode"].ToString());
                            ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequenceForSuperAdmin().ToList();

                            POViewModel POVM = new POViewModel();
                            POVM.tblPO = GetPOModel(ID);
                            POVM.tblPODetails = GetPODetailsModel(ID);
                            POVM.tblPOHistories = GetPOHistoriesModel(ID);
                            POVM.tblManageApprovals = GetManageApprovalModel();
                            ViewBag.xpertLoginID = Session["xpertLoginID"].ToString();
                            return View(POVM);
                        }
                        else
                        {
                            int userCode = Convert.ToInt32(Session["intUserCode"].ToString());
                            ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequence(Session["intUserCode"].ToString()).ToList();
                            POViewModel POVM = new POViewModel();
                            POVM.tblPO = GetPOModel(ID);
                            POVM.tblPODetails = GetPODetailsModel(ID);
                            POVM.tblPOHistories = GetPOHistoriesModel(ID);
                            POVM.tblManageApprovals = GetManageApprovalModel();
                            ViewBag.xpertLoginID = Session["xpertLoginID"].ToString();
                            return View(POVM);

                        }
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
                    if (link[1].ToString() == "SearchPO")
                    {
                        List<tblStatu> statusList = db.tblStatus.ToList();
                        ViewBag.Status = new MultiSelectList(statusList, "strStatusName", "strStatusName");

                        var data1 = new List<procSearchPO_Result>();

                        if (reqCookies["SuperAdmin"].ToString() == "Y")
                        {
                            int userCode = Convert.ToInt32(reqCookies["intUserCode"].ToString());
                            ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequenceForSuperAdmin().ToList();

                            POViewModel POVM = new POViewModel();
                            POVM.tblPO = GetPOModel(ID);
                            POVM.tblPODetails = GetPODetailsModel(ID);
                            POVM.tblPOHistories = GetPOHistoriesModel(ID);
                            POVM.tblManageApprovals = GetManageApprovalModel();
                            ViewBag.xpertLoginID = reqCookies["xpertLoginID"].ToString();
                            return View(POVM);
                        }
                        else
                        {
                            int userCode = Convert.ToInt32(reqCookies["intUserCode"].ToString());
                            ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequence(reqCookies["intUserCode"].ToString()).ToList();
                            POViewModel POVM = new POViewModel();
                            POVM.tblPO = GetPOModel(ID);
                            POVM.tblPODetails = GetPODetailsModel(ID);
                            POVM.tblPOHistories = GetPOHistoriesModel(ID);
                            POVM.tblManageApprovals = GetManageApprovalModel();
                            ViewBag.xpertLoginID = reqCookies["xpertLoginID"].ToString();
                            return View(POVM);

                        }
                        return View(data1);




                    }



                }
            }




            return RedirectToAction("AccessDenied", "Errors");


        }

        //[Authorize]
        public List<procUserMenu_Result> GetUserMenus(String userCode)
        {

            List<procUserMenu_Result> GetUserMenus = db.procUserMenu(userCode).ToList();

            return GetUserMenus;
        }
        public ActionResult SearchPOWithFilter()
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
                    if (link[1].ToString() == "SearchPO")
                    {
                        List<tblStatu> statusList = db.tblStatus.ToList();
                        ViewBag.Status = new MultiSelectList(statusList, "strStatusName", "strStatusName");

                        var data1 = new List<procSearchPO_Result>();

                        if (Session["SuperAdmin"].ToString() == "Y")
                        {
                            int userCode = Convert.ToInt32(Session["intUserCode"].ToString());
                            ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequenceForSuperAdmin().ToList();

                            data1 = PODB.ListAllprocSearch(null, null, "Pending").ToList();
                        }
                        else
                        {
                            int userCode = Convert.ToInt32(Session["intUserCode"].ToString());
                            ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequence(Session["intUserCode"].ToString()).ToList();
                            data1 = PODB.ListAll(userCode, null, 0).ToList();

                        }
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
                    if (link[1].ToString() == "SearchPO")
                    {
                        List<tblStatu> statusList = db.tblStatus.ToList();
                        ViewBag.Status = new MultiSelectList(statusList, "strStatusName", "strStatusName");

                        var data1 = new List<procSearchPO_Result>();

                        if (reqCookies["SuperAdmin"].ToString() == "Y")
                        {
                            int userCode = Convert.ToInt32(reqCookies["intUserCode"].ToString());
                            ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequenceForSuperAdmin().ToList();

                            data1 = PODB.ListAllprocSearch(null, null, "Pending").ToList();
                        }
                        else
                        {
                            int userCode = Convert.ToInt32(reqCookies["intUserCode"].ToString());
                            ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequence(reqCookies["intUserCode"].ToString()).ToList();
                            data1 = PODB.ListAll(userCode, null, 0).ToList();

                        }
                        return View(data1);




                    }



                }
            }




            return RedirectToAction("AccessDenied", "Errors");
        }
        public ActionResult SearchPOs(int currentPage)
        {
          
            int maxRows = 10;

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
                    if (link[1].ToString() == "SearchPO")
                    {
                        List<tblStatu> statusList = db.tblStatus.ToList();
                        ViewBag.Status = new MultiSelectList(statusList, "strStatusName", "strStatusName");

                        var data1 = new List<procSearchPO_Result>();

                        if (Session["SuperAdmin"].ToString() == "Y")
                        {
                            int userCode = Convert.ToInt32(Session["intUserCode"].ToString());
                            ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequenceForSuperAdmin().ToList();

                            data1 = PODB.ListAllprocSearch(null, null, "Pending").Skip((currentPage - 1) * maxRows).Take(maxRows).ToList();
                            var datacount = PODB.ListAllprocSearch(null, null, "Pending").ToList();

                            ViewBag.sumqty = data1.Sum(x => x.Qty);
                            ViewBag.sumtotal = data1.Sum(x => x.Amount);
                            procSearchPO_Result pO_Result = new procSearchPO_Result();
                            double pageCount = (double)((decimal)datacount.Count() / Convert.ToDecimal(maxRows));
                            pO_Result.PageCount = (int)Math.Ceiling(pageCount);

                            pO_Result.CurrentPageIndex = currentPage;

                            ViewBag.pO_Result = pO_Result;
                        }
                        else
                        {
                            int userCode = Convert.ToInt32(Session["intUserCode"].ToString());
                            ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequence(Session["intUserCode"].ToString()).ToList();
                            data1 = PODB.ListAll(userCode, null, 0).ToList();



                        }

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
                    if (link[1].ToString() == "SearchPO")
                    {
                        List<tblStatu> statusList = db.tblStatus.ToList();
                        ViewBag.Status = new MultiSelectList(statusList, "strStatusName", "strStatusName");

                        var data1 = new List<procSearchPO_Result>();

                        if (reqCookies["SuperAdmin"].ToString() == "Y")
                        {
                            int userCode = Convert.ToInt32(reqCookies["intUserCode"].ToString());
                            ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequenceForSuperAdmin().ToList();

                            data1 = PODB.ListAllprocSearch(null, null, "Pending").ToList();
                        }
                        else
                        {
                            int userCode = Convert.ToInt32(reqCookies["intUserCode"].ToString());
                            ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequence(reqCookies["intUserCode"].ToString()).ToList();
                            data1 = PODB.ListAll(userCode, null, 0).ToList();

                        }
                        return View(data1);




                    }



                }
            }




            return RedirectToAction("AccessDenied", "Errors");
        }


        public ActionResult SearchPO()
        {
            int currentPage = 1;
            int maxRows = 10;

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
                    if (link[1].ToString() == "SearchPO")
                    {
                        List<tblStatu> statusList = db.tblStatus.ToList();
                        ViewBag.Status = new MultiSelectList(statusList, "strStatusName", "strStatusName");

                        var data1 = new List<procSearchPO_Result>();

                        if (Session["SuperAdmin"].ToString() == "Y")
                        {
                            int userCode = Convert.ToInt32(Session["intUserCode"].ToString());
                            ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequenceForSuperAdmin().ToList();

                            data1 = PODB.ListAllprocSearch(null, null, "Pending").Skip((currentPage - 1) * maxRows).Take(maxRows).ToList();
                            var datacount = PODB.ListAllprocSearch(null, null, "Pending").ToList();

                            ViewBag.sumqty = data1.Sum(x => x.Qty);
                            ViewBag.sumtotal = data1.Sum(x => x.Amount);
                            procSearchPO_Result pO_Result = new procSearchPO_Result();
                            double pageCount = (double)((decimal)datacount.Count() / Convert.ToDecimal(maxRows));
                            pO_Result.PageCount = (int)Math.Ceiling(pageCount);

                            pO_Result.CurrentPageIndex = currentPage;

                            ViewBag.pO_Result = pO_Result;
                        }
                        else
                        {
                            int userCode = Convert.ToInt32(Session["intUserCode"].ToString());
                            ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequence(Session["intUserCode"].ToString()).ToList();
                            data1 = PODB.ListAll(userCode, null, 0).ToList();



                        }

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
                    if (link[1].ToString() == "SearchPO")
                    {
                        List<tblStatu> statusList = db.tblStatus.ToList();
                        ViewBag.Status = new MultiSelectList(statusList, "strStatusName", "strStatusName");

                        var data1 = new List<procSearchPO_Result>();

                        if (reqCookies["SuperAdmin"].ToString() == "Y")
                        {
                            int userCode = Convert.ToInt32(reqCookies["intUserCode"].ToString());
                            ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequenceForSuperAdmin().ToList();

                            data1 = PODB.ListAllprocSearch(null, null, "Pending").ToList();
                        }
                        else
                        {
                            int userCode = Convert.ToInt32(reqCookies["intUserCode"].ToString());
                            ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequence(reqCookies["intUserCode"].ToString()).ToList();
                            data1 = PODB.ListAll(userCode, null, 0).ToList();

                        }
                        return View(data1);




                    }



                }
            }




            return RedirectToAction("AccessDenied", "Errors");
        }


      
        string approvalLevel;

        [HttpPost]
        public ActionResult SaveMultiplePO(List<procSearchPO_Result> searchPO_Results, string criteria, string RejectReason)
        {

            if (ModelState.IsValid)
            {
                if (criteria == "report")
                {
                    List<procSearchPO_Result> selectList = new List<procSearchPO_Result>();
                    string myCodes = string.Empty;
                    foreach (var item in searchPO_Results)
                    {
                        if (item.PO_Number != null)
                        {

                            using (var transaction = db.Database.BeginTransaction())
                            {
                                try
                                {

                                    var data = db.tblPOes.Where(x => x.PO_Number == item.PO_Number).FirstOrDefault();




                                    if (myCodes.Length > 0)
                                    {
                                        myCodes += ", "; // Add a comma if data already exists
                                    }


                                    myCodes += "'" + data.intPOCode.ToString() + "'";






                                }
                                catch (Exception ex)
                                {
                                    // roll back all database operations, if any thing goes wrong

                                    ViewBag.ResultMessage = ex.Message + "Error occured, records rolledback.";
                                }
                            }
                        }

                    }
                    HttpCookie reqCookies = Request.Cookies["userInfo"];
                    if (reqCookies != null)
                    {
                        ViewBag.xpertLoginID = reqCookies["xpertLoginID"].ToString();
                    }
                    else
                    {
                        ViewBag.xpertLoginID = Session["xpertLoginID"].ToString();

                    }

                    ViewBag.intPOCode = myCodes;
                    return Json(myCodes);


                }
                else if (criteria == "reject")
                {
                    HttpCookie reqCookies = Request.Cookies["userInfo"];
                    if (reqCookies != null)
                    {
                        var obj = db.procValidateUserLogins(reqCookies["intUserCode"].ToString(), reqCookies["Password"].ToString()).FirstOrDefault();
                        if (obj != null)
                        {
                            if (obj.xpertLoginID != null)
                            {
                                string userCode = null;
                                if (reqCookies != null)
                                {
                                    userCode = reqCookies["intUserCode"].ToString();

                                }
                                else
                                {
                                    userCode = Session["intUserCode"].ToString();

                                }

                                // Add checked item to the list and render them in view
                                List<procSearchPO_Result> selectList = new List<procSearchPO_Result>();
                                foreach (var item in searchPO_Results)
                                {

                                    if (item.PO_Number != null)
                                    {
                                        using (var transaction = db.Database.BeginTransaction())
                                        {
                                            try
                                            {
                                                var data = db.tblPOes.Where(x => x.PO_Number == item.PO_Number).FirstOrDefault();


                                                PODB.Add(data.intPOCode, userCode, "Rejected", RejectReason);


                                                ////tblSupervisor Supervisor = new tblSupervisor();
                                                //tblPOHistory objPOHistory = new tblPOHistory()
                                                //{

                                                //    dtCreatedAt = DateTime.Now,
                                                //    intPOCode = item.intPOCode,
                                                //    strPOStatus = "Rejected",
                                                //    intUserCode = userCode





                                                //};

                                                //db.tblPOHistories.Add(objPOHistory);
                                                //db.SaveChanges();

                                                //string lastid = objOvertime.uqWorkerCode.ToString();

                                                var POData = this.db.tblPOes.Where(x => x.PO_Number == item.PO_Number).FirstOrDefault();

                                                POData.strPOStatus = "Rejected";
                                                if (RejectReason != null)
                                                {
                                                    POData.strRejectReason = RejectReason;
                                                }
                                                db.Entry(POData).State = EntityState.Modified;
                                                db.SaveChanges();





                                                transaction.Commit();

                                            }
                                            catch (Exception ex)
                                            {
                                                // roll back all database operations, if any thing goes wrong
                                                transaction.Rollback();
                                                ViewBag.ResultMessage = ex.Message + "Error occured, records rolledback.";
                                            }
                                        }
                                    }

                                }
                                //string[] intPOCode = formCollection["intPOCode"].Split(char.Parse(","));
                                //string[] ApprovalLevel = formCollection["ApprovalLevel"].Split(char.Parse(","));
                                ////for (var i = 0; i < names.Length; i++)
                                ////{
                                ////    student stds = new student();
                                ////    stds.name = names[i];
                                ////    stds.dno = dnos[i];
                                ////    stds.address = adds[i];
                                ////    stds.active = true;
                                ////    db.students.AddObject(stds);
                                ////    db.SaveChanges();
                                //}
                                return Json(true);
                            }
                            else
                            {
                                return Json("Cannot perform this action. User xpertloginid is null");
                            }
                        }
                        else
                        {
                            return Json("Cannot perform this action. User xpertloginid is null");
                        }
                    }
                    else
                    {
                        var obj = db.procValidateUserLogins(Session["intUserCode"].ToString(), Session["Password"].ToString()).FirstOrDefault();
                        if (obj != null)
                        {
                            if (obj.xpertLoginID != null)
                            {
                                string userCode = null;
                                if (reqCookies != null)
                                {
                                    userCode = reqCookies["intUserCode"].ToString();

                                }
                                else
                                {
                                    userCode = Session["intUserCode"].ToString();

                                }

                                // Add checked item to the list and render them in view
                                List<procSearchPO_Result> selectList = new List<procSearchPO_Result>();
                                foreach (var item in searchPO_Results)
                                {

                                    if (item.PO_Number != null)
                                    {
                                        using (var transaction = db.Database.BeginTransaction())
                                        {
                                            try
                                            {
                                                var data = db.tblPOes.Where(x => x.PO_Number == item.PO_Number).FirstOrDefault();


                                                PODB.Add(data.intPOCode, userCode, "Rejected", RejectReason);


                                                ////tblSupervisor Supervisor = new tblSupervisor();
                                                //tblPOHistory objPOHistory = new tblPOHistory()
                                                //{

                                                //    dtCreatedAt = DateTime.Now,
                                                //    intPOCode = item.intPOCode,
                                                //    strPOStatus = "Rejected",
                                                //    intUserCode = userCode





                                                //};

                                                //db.tblPOHistories.Add(objPOHistory);
                                                //db.SaveChanges();

                                                //string lastid = objOvertime.uqWorkerCode.ToString();

                                                var POData = this.db.tblPOes.Where(x => x.PO_Number == item.PO_Number).FirstOrDefault();

                                                POData.strPOStatus = "Rejected";
                                                if (RejectReason != null)
                                                {
                                                    POData.strRejectReason = RejectReason;
                                                }
                                                db.Entry(POData).State = EntityState.Modified;
                                                db.SaveChanges();





                                                transaction.Commit();

                                            }
                                            catch (Exception ex)
                                            {
                                                // roll back all database operations, if any thing goes wrong
                                                transaction.Rollback();
                                                ViewBag.ResultMessage = ex.Message + "Error occured, records rolledback.";
                                            }
                                        }
                                    }

                                }
                                //string[] intPOCode = formCollection["intPOCode"].Split(char.Parse(","));
                                //string[] ApprovalLevel = formCollection["ApprovalLevel"].Split(char.Parse(","));
                                ////for (var i = 0; i < names.Length; i++)
                                ////{
                                ////    student stds = new student();
                                ////    stds.name = names[i];
                                ////    stds.dno = dnos[i];
                                ////    stds.address = adds[i];
                                ////    stds.active = true;
                                ////    db.students.AddObject(stds);
                                ////    db.SaveChanges();
                                //}
                                return Json(true);
                            }
                            else
                            {
                                return Json("Cannot perform this action. User xpertloginid is null");
                            }
                        }
                        else
                        {
                            return Json("Cannot perform this action. User xpertloginid is null");
                        }
                    }






                }

                else
                {
                    HttpCookie reqCookies = Request.Cookies["userInfo"];
                    string userCode = null;
                    if (reqCookies != null)
                    {
                        userCode = reqCookies["intUserCode"].ToString();

                    }
                    else
                    {
                        userCode = Session["intUserCode"].ToString();
                    }
                    if (reqCookies != null)
                    {
                        var obj = db.procValidateUserLogins(reqCookies["intUserCode"].ToString(), reqCookies["Password"].ToString()).FirstOrDefault();
                        if (obj != null)
                        {

                            if (reqCookies != null)
                            {
                                if (reqCookies["SuperAdmin"].ToString() == "Y")
                                {
                                    // Add checked item to the list and render them in view
                                    List<procSearchPO_Result> selectList = new List<procSearchPO_Result>();
                                    foreach (var item in searchPO_Results)
                                    {
                                        if (item.PO_Number != null)
                                        {

                                            using (var transaction = db.Database.BeginTransaction())
                                            {
                                                try
                                                {
                                                    var data = db.tblPOes.Where(x => x.PO_Number == item.PO_Number).FirstOrDefault();

                                                    var getlistForAdmin = db.procCheckApprovalLevelSequenceForSuperAdmin().ToList();

                                                    if (item.strPOStatus == "Pending")
                                                    {

                                                        foreach (var itemm in getlistForAdmin.Where(x => x.po_number == item.PO_Number))
                                                        {

                                                            if (itemm.ApprovalLevel == "Reviewer 1")
                                                            {
                                                                approvalLevel = "Reviewed 1";

                                                                break;
                                                            }


                                                            if (itemm.ApprovalLevel == "Reviewer 2")
                                                            {
                                                                approvalLevel = "Reviewed 2";

                                                                break;
                                                            }
                                                            if (itemm.ApprovalLevel == "Reviewer 3")
                                                            {
                                                                approvalLevel = "Reviewed 3";

                                                                break;
                                                            }
                                                            if (itemm.ApprovalLevel == "Approver")
                                                            {
                                                                approvalLevel = "Approved";

                                                                break;
                                                            }



                                                        }

                                                    }
                                                    if (item.strPOStatus == "Reviewed 1")
                                                    {

                                                        foreach (var itemm in getlistForAdmin.Where(x => x.po_number == item.PO_Number))
                                                        {



                                                            if (itemm.ApprovalLevel == "Reviewer 2")
                                                            {
                                                                approvalLevel = "Reviewed 2";

                                                                break;
                                                            }
                                                            if (itemm.ApprovalLevel == "Reviewer 3")
                                                            {
                                                                approvalLevel = "Reviewed 3";

                                                                break;
                                                            }
                                                            if (itemm.ApprovalLevel == "Approver")
                                                            {
                                                                approvalLevel = "Approved";

                                                                break;
                                                            }



                                                        }

                                                    }
                                                    if (item.strPOStatus == "Reviewed 2")
                                                    {

                                                        foreach (var itemm in getlistForAdmin.Where(x => x.po_number == item.PO_Number))
                                                        {




                                                            if (itemm.ApprovalLevel == "Reviewer 3")
                                                            {
                                                                approvalLevel = "Reviewed 3";

                                                                break;
                                                            }
                                                            if (itemm.ApprovalLevel == "Approver")
                                                            {
                                                                approvalLevel = "Approved";

                                                                break;
                                                            }



                                                        }

                                                    }

                                                    if (item.strPOStatus == "Reviewed 3")
                                                    {

                                                        foreach (var itemm in getlistForAdmin.Where(x => x.po_number == item.PO_Number))
                                                        {





                                                            if (itemm.ApprovalLevel == "Approver")
                                                            {
                                                                approvalLevel = "Approved";

                                                                break;
                                                            }



                                                        }

                                                    }






                                                    if (approvalLevel == "Approved")
                                                    {
                                                        PODB.Add(data.intPOCode, userCode, approvalLevel, null);
                                                    }
                                                    else
                                                    {

                                                        //tblSupervisor Supervisor = new tblSupervisor();
                                                        tblPOHistory objPOHistory = new tblPOHistory()
                                                        {

                                                            dtCreatedAt = DateTime.Now,
                                                            intPOCode = data.intPOCode,
                                                            strPOStatus = approvalLevel,
                                                            intUserCode = userCode

                                                        };

                                                        db.tblPOHistories.Add(objPOHistory);
                                                        db.SaveChanges();

                                                        //string lastid = objOvertime.uqWorkerCode.ToString();

                                                        var POData = this.db.tblPOes.Where(x => x.PO_Number == data.PO_Number).FirstOrDefault();

                                                        POData.strPOStatus = approvalLevel;

                                                        db.Entry(POData).State = EntityState.Modified;
                                                        db.SaveChanges();
                                                    }




                                                    transaction.Commit();

                                                }
                                                catch (Exception ex)
                                                {
                                                    // roll back all database operations, if any thing goes wrong
                                                    transaction.Rollback();
                                                    ViewBag.ResultMessage = ex.Message + "Error occured, records rolledback.";
                                                }
                                            }

                                        }
                                    }
                                    //string[] intPOCode = formCollection["intPOCode"].Split(char.Parse(","));
                                    //string[] ApprovalLevel = formCollection["ApprovalLevel"].Split(char.Parse(","));
                                    ////for (var i = 0; i < names.Length; i++)
                                    ////{
                                    ////    student stds = new student();
                                    ////    stds.name = names[i];
                                    ////    stds.dno = dnos[i];
                                    ////    stds.address = adds[i];
                                    ////    stds.active = true;
                                    ////    db.students.AddObject(stds);
                                    ////    db.SaveChanges();
                                    //}
                                    return Json(true);

                                }
                                else
                                {
                                    // Add checked item to the list and render them in view
                                    List<procSearchPO_Result> selectList = new List<procSearchPO_Result>();
                                    foreach (var item in searchPO_Results)
                                    {
                                        if (item.PO_Number != null)
                                        {

                                            using (var transaction = db.Database.BeginTransaction())
                                            {
                                                try
                                                {
                                                    var data = db.tblPOes.Where(x => x.PO_Number == item.PO_Number).FirstOrDefault();


                                                    if (item.ApprovalLevel == "Reviewer 1")
                                                    {
                                                        approvalLevel = "Reviewed 1";
                                                    }
                                                    else if (item.ApprovalLevel == "Reviewer 2")
                                                    {
                                                        approvalLevel = "Reviewed 2";
                                                    }
                                                    else if (item.ApprovalLevel == "Reviewer 3")
                                                    {
                                                        approvalLevel = "Reviewed 3";
                                                    }
                                                    else if (item.ApprovalLevel == "Approver")
                                                    {
                                                        approvalLevel = "Approved";
                                                    }
                                                    if (approvalLevel == "Approved")
                                                    {
                                                        PODB.Add(data.intPOCode, userCode, approvalLevel, null);
                                                    }
                                                    else
                                                    {

                                                        //tblSupervisor Supervisor = new tblSupervisor();
                                                        tblPOHistory objPOHistory = new tblPOHistory()
                                                        {

                                                            dtCreatedAt = DateTime.Now,
                                                            intPOCode = data.intPOCode,
                                                            strPOStatus = approvalLevel,
                                                            intUserCode = userCode

                                                        };

                                                        db.tblPOHistories.Add(objPOHistory);
                                                        db.SaveChanges();

                                                        //string lastid = objOvertime.uqWorkerCode.ToString();

                                                        var POData = this.db.tblPOes.Where(x => x.PO_Number == data.PO_Number).FirstOrDefault();

                                                        POData.strPOStatus = approvalLevel;

                                                        db.Entry(POData).State = EntityState.Modified;
                                                        db.SaveChanges();
                                                    }




                                                    transaction.Commit();

                                                }
                                                catch (Exception ex)
                                                {
                                                    // roll back all database operations, if any thing goes wrong
                                                    transaction.Rollback();
                                                    ViewBag.ResultMessage = ex.Message + "Error occured, records rolledback.";
                                                }
                                            }
                                        }

                                    }
                                }
                                //string[] intPOCode = formCollection["intPOCode"].Split(char.Parse(","));
                                //string[] ApprovalLevel = formCollection["ApprovalLevel"].Split(char.Parse(","));
                                ////for (var i = 0; i < names.Length; i++)
                                ////{
                                ////    student stds = new student();
                                ////    stds.name = names[i];
                                ////    stds.dno = dnos[i];
                                ////    stds.address = adds[i];
                                ////    stds.active = true;
                                ////    db.students.AddObject(stds);
                                ////    db.SaveChanges();
                                //}
                                return Json(true);


                            }
                        }
                        else
                        {
                            return Json("Cannot perform this action. User xpertloginid is null");
                        }
                    }

                    else if (reqCookies == null)
                    {
                        var obj = db.procValidateUserLogins(Session["intUserCode"].ToString(), Session["Password"].ToString()).FirstOrDefault();
                        if (obj != null)
                        {
                            if (Session["SuperAdmin"].ToString() == "Y")
                            {
                                // Add checked item to the list and render them in view
                                List<procSearchPO_Result> selectList = new List<procSearchPO_Result>();
                                foreach (var item in searchPO_Results)
                                {
                                    if (item.PO_Number != null)
                                    {

                                        using (var transaction = db.Database.BeginTransaction())
                                        {
                                            try
                                            {
                                                var data = db.tblPOes.Where(x => x.PO_Number == item.PO_Number).FirstOrDefault();

                                                var getlistForAdmin = db.procCheckApprovalLevelSequenceForSuperAdmin().ToList();

                                                if (item.strPOStatus == "Pending")
                                                {

                                                    foreach (var itemm in getlistForAdmin.Where(x => x.po_number == item.PO_Number))
                                                    {

                                                        if (itemm.ApprovalLevel == "Reviewer 1")
                                                        {
                                                            approvalLevel = "Reviewed 1";

                                                            break;
                                                        }


                                                        if (itemm.ApprovalLevel == "Reviewer 2")
                                                        {
                                                            approvalLevel = "Reviewed 2";

                                                            break;
                                                        }
                                                        if (itemm.ApprovalLevel == "Reviewer 3")
                                                        {
                                                            approvalLevel = "Reviewed 3";

                                                            break;
                                                        }
                                                        if (itemm.ApprovalLevel == "Approver")
                                                        {
                                                            approvalLevel = "Approved";

                                                            break;
                                                        }



                                                    }

                                                }
                                                if (item.strPOStatus == "Reviewed 1")
                                                {

                                                    foreach (var itemm in getlistForAdmin.Where(x => x.po_number == item.PO_Number))
                                                    {



                                                        if (itemm.ApprovalLevel == "Reviewer 2")
                                                        {
                                                            approvalLevel = "Reviewed 2";

                                                            break;
                                                        }
                                                        if (itemm.ApprovalLevel == "Reviewer 3")
                                                        {
                                                            approvalLevel = "Reviewed 3";

                                                            break;
                                                        }
                                                        if (itemm.ApprovalLevel == "Approver")
                                                        {
                                                            approvalLevel = "Approved";

                                                            break;
                                                        }



                                                    }

                                                }
                                                if (item.strPOStatus == "Reviewed 2")
                                                {

                                                    foreach (var itemm in getlistForAdmin.Where(x => x.po_number == item.PO_Number))
                                                    {




                                                        if (itemm.ApprovalLevel == "Reviewer 3")
                                                        {
                                                            approvalLevel = "Reviewed 3";

                                                            break;
                                                        }
                                                        if (itemm.ApprovalLevel == "Approver")
                                                        {
                                                            approvalLevel = "Approved";

                                                            break;
                                                        }



                                                    }

                                                }

                                                if (item.strPOStatus == "Reviewed 3")
                                                {

                                                    foreach (var itemm in getlistForAdmin.Where(x => x.po_number == item.PO_Number))
                                                    {





                                                        if (itemm.ApprovalLevel == "Approver")
                                                        {
                                                            approvalLevel = "Approved";

                                                            break;
                                                        }



                                                    }

                                                }
                                                if (approvalLevel == "Approved")
                                                {
                                                    PODB.Add(data.intPOCode, userCode, approvalLevel, null);
                                                }
                                                else
                                                {

                                                    //tblSupervisor Supervisor = new tblSupervisor();
                                                    tblPOHistory objPOHistory = new tblPOHistory()
                                                    {

                                                        dtCreatedAt = DateTime.Now,
                                                        intPOCode = data.intPOCode,
                                                        strPOStatus = approvalLevel,
                                                        intUserCode = userCode

                                                    };

                                                    db.tblPOHistories.Add(objPOHistory);
                                                    db.SaveChanges();

                                                    //string lastid = objOvertime.uqWorkerCode.ToString();

                                                    var POData = this.db.tblPOes.Where(x => x.PO_Number == item.PO_Number).FirstOrDefault();

                                                    POData.strPOStatus = approvalLevel;

                                                    db.Entry(POData).State = EntityState.Modified;
                                                    db.SaveChanges();
                                                }




                                                transaction.Commit();

                                            }
                                            catch (Exception ex)
                                            {
                                                // roll back all database operations, if any thing goes wrong
                                                transaction.Rollback();
                                                ViewBag.ResultMessage = ex.Message + "Error occured, records rolledback.";
                                            }
                                        }
                                    }

                                }
                                //string[] intPOCode = formCollection["intPOCode"].Split(char.Parse(","));
                                //string[] ApprovalLevel = formCollection["ApprovalLevel"].Split(char.Parse(","));
                                ////for (var i = 0; i < names.Length; i++)
                                ////{
                                ////    student stds = new student();
                                ////    stds.name = names[i];
                                ////    stds.dno = dnos[i];
                                ////    stds.address = adds[i];
                                ////    stds.active = true;
                                ////    db.students.AddObject(stds);
                                ////    db.SaveChanges();
                                //}
                                return Json(true);

                            }
                            else
                            {
                                // Add checked item to the list and render them in view
                                List<procSearchPO_Result> selectList = new List<procSearchPO_Result>();
                                foreach (var item in searchPO_Results)
                                {

                                    if (item.PO_Number != null)
                                    {
                                        using (var transaction = db.Database.BeginTransaction())
                                        {
                                            try
                                            {
                                                var data = db.tblPOes.Where(x => x.PO_Number == item.PO_Number).FirstOrDefault();


                                                if (item.ApprovalLevel == "Reviewer 1")
                                                {
                                                    approvalLevel = "Reviewed 1";
                                                }
                                                else if (item.ApprovalLevel == "Reviewer 2")
                                                {
                                                    approvalLevel = "Reviewed 2";
                                                }
                                                else if (item.ApprovalLevel == "Reviewer 3")
                                                {
                                                    approvalLevel = "Reviewed 3";
                                                }
                                                else if (item.ApprovalLevel == "Approver")
                                                {
                                                    approvalLevel = "Approved";
                                                }
                                                if (approvalLevel == "Approved")
                                                {
                                                    PODB.Add(data.intPOCode, userCode, approvalLevel, null);
                                                }
                                                else
                                                {

                                                    //tblSupervisor Supervisor = new tblSupervisor();
                                                    tblPOHistory objPOHistory = new tblPOHistory()
                                                    {

                                                        dtCreatedAt = DateTime.Now,
                                                        intPOCode = data.intPOCode,
                                                        strPOStatus = approvalLevel,
                                                        intUserCode = userCode

                                                    };

                                                    db.tblPOHistories.Add(objPOHistory);
                                                    db.SaveChanges();

                                                    //string lastid = objOvertime.uqWorkerCode.ToString();

                                                    var POData = this.db.tblPOes.Where(x => x.PO_Number == item.PO_Number).FirstOrDefault();

                                                    POData.strPOStatus = approvalLevel;

                                                    db.Entry(POData).State = EntityState.Modified;
                                                    db.SaveChanges();
                                                }




                                                transaction.Commit();

                                            }
                                            catch (Exception ex)
                                            {
                                                // roll back all database operations, if any thing goes wrong
                                                transaction.Rollback();
                                                ViewBag.ResultMessage = ex.Message + "Error occured, records rolledback.";
                                            }
                                        }
                                    }

                                }
                                //string[] intPOCode = formCollection["intPOCode"].Split(char.Parse(","));
                                //string[] ApprovalLevel = formCollection["ApprovalLevel"].Split(char.Parse(","));
                                ////for (var i = 0; i < names.Length; i++)
                                ////{
                                ////    student stds = new student();
                                ////    stds.name = names[i];
                                ////    stds.dno = dnos[i];
                                ////    stds.address = adds[i];
                                ////    stds.active = true;
                                ////    db.students.AddObject(stds);
                                ////    db.SaveChanges();
                                //}
                                return Json(true);
                            }
                        }
                        else
                        {
                            return Json("Cannot perform this action. User xpertloginid is null");
                        }



                    }

                }
            }
            return Json(true);
        }
        [HttpPost]
        public ActionResult SearchPO(tblPO tblPO)
        {
            string myCodes = string.Empty;
            if (tblPO.strStatusName != null)
            {
                ViewBag.strStatusName = tblPO.strStatusName;
                foreach (var item in tblPO.strStatusName)
                {
                    if (myCodes.Length > 0)
                    {
                        myCodes += ", "; // Add a comma if data already exists
                    }

                    myCodes += "'" + item + "'";
                }

            }
            if (myCodes == "")
            {
                myCodes = null;
            }
            if (tblPO.PO_Number.ToString() == "" || tblPO.PO_Number == null)
            {
                tblPO.PO_Number = null;
            }
            int userCode = 0;
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                userCode = Convert.ToInt32(reqCookies["intUserCode"]);
                if (reqCookies["SuperAdmin"].ToString() == "Y")
                {
                    ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequenceForSuperAdmin().ToList();

                }
                else
                {
                    ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequence(userCode.ToString()).ToList();

                }


            }
            else
            {
                userCode = Convert.ToInt32(Session["intUserCode"]);
                if (Session["SuperAdmin"].ToString() == "Y")
                {
                    ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequenceForSuperAdmin().ToList();

                }
                else
                {
                    ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequence(userCode.ToString()).ToList();

                }

            }

            List<tblStatu> statusList = db.tblStatus.ToList();
            ViewBag.Status = new MultiSelectList(statusList, "strStatusName", "strStatusName");

            var data = PODB.SearchPO(userCode, myCodes, tblPO.PO_Number);
            return View(data);

        }
        [HttpPost]
        public ActionResult SearchPOWithFilter(tblPO tblPO)

        {

            string myCodes = string.Empty;
            if (tblPO.strStatusName != null)
            {
                ViewBag.strStatusName = tblPO.strStatusName;
                foreach (var item in tblPO.strStatusName)
                {
                    if (myCodes.Length > 0)
                    {
                        myCodes += ", "; // Add a comma if data already exists
                    }

                    myCodes += "'" + item + "'";
                }

            }
            if (myCodes == "")
            {
                myCodes = null;
            }
            if (tblPO.PO_Number.ToString() == "" || tblPO.PO_Number == null)
            {
                tblPO.PO_Number = null;
            }
            else
            {
                ViewBag.PO_Number = tblPO.PO_Number;
            }
            int userCode = 0;
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                userCode = Convert.ToInt32(reqCookies["intUserCode"]);
                if (reqCookies["SuperAdmin"].ToString() == "Y")
                {
                    ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequenceForSuperAdmin().ToList();

                }
                else
                {
                    ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequence(userCode.ToString()).ToList();

                }


            }
            else
            {
                userCode = Convert.ToInt32(Session["intUserCode"]);
                if (Session["SuperAdmin"].ToString() == "Y")
                {
                    ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequenceForSuperAdmin().ToList();

                }
                else
                {
                    ViewBag.approvalLevelSequence = db.procCheckApprovalLevelSequence(userCode.ToString()).ToList();

                }

            }

            List<tblStatu> statusList = db.tblStatus.ToList();
            ViewBag.Status = new MultiSelectList(statusList, "strStatusName", "strStatusName");

            var data = PODB.SearchPO(userCode, myCodes, tblPO.PO_Number);
            return View(data);

        }

        [HttpPost]
        public int SavePOHistory(int ID, string status, string strRejectReason)
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                var obj = db.procValidateUserLogins(reqCookies["intUserCode"].ToString(), reqCookies["Password"].ToString()).FirstOrDefault();
                if (obj != null)
                {
                    string userCode = null;
                    if (reqCookies != null)
                    {
                        userCode = reqCookies["intUserCode"].ToString();
                    }
                    else
                    {
                        userCode = Session["intUserCode"].ToString();
                    }

                    if (ID != null)
                    {
                        using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                        {

                            return PODB.Add(ID, userCode, status, strRejectReason);
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 5;
                }

            }
            else
            {
                var obj = db.procValidateUserLogins(Session["intUserCode"].ToString(), Session["Password"].ToString()).FirstOrDefault();
                if (obj != null)
                {
                    string userCode = null;
                    if (reqCookies != null)
                    {
                        userCode = reqCookies["intUserCode"].ToString();
                    }
                    else
                    {
                        userCode = Session["intUserCode"].ToString();
                    }

                    if (ID != null)
                    {
                        using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                        {

                            return PODB.Add(ID, userCode, status, strRejectReason);
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 5;
                }

            }



        }

    }
}