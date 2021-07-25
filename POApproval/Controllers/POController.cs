using POApproval.GlobalInfo;
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
    [Authorize]
    public class POController : Controller
    {
        dbSASAApprovalEntities db = new dbSASAApprovalEntities();
        PODB PODB = new PODB();


        // GET: PO
        public ActionResult PORpt(int intPOCode, string xpertLoginID)
        {
            ViewBag.intPOCode = intPOCode;
            ViewBag.xpertLoginID = xpertLoginID;
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
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
            string ID = reqCookies["intUserCode"].ToString();
            List<tblManageApproval> ManageApprovalModel = db.tblManageApprovals.Where(x => x.intUserCode == ID).ToList();
            return ManageApprovalModel;
        }
        //[Authorize]
        public ActionResult ReviewPO(int ID)
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies == null)
            {
                return RedirectToAction("Login", "Account");
            }
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



            return RedirectToAction("AccessDenied", "Errors");
           

        }

        //[Authorize]
        public List<procUserMenu_Result> GetUserMenus(String userCode)
        {

            List<procUserMenu_Result> GetUserMenus = db.procUserMenu(userCode).ToList();

            return GetUserMenus;
        }
        public ActionResult SearchPO()
        {



            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies == null)
            {
                return RedirectToAction("Login", "Account");
            }
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
                        data1 = PODB.ListAllprocSearch(null, null, "Pending").ToList();
                    }
                    else
                    {
                        int userCode = Convert.ToInt32(reqCookies["intUserCode"].ToString());
                        data1 = PODB.ListAll(userCode, null, 0);

                    }
                    return View(data1);




                }
                 


            }



            return RedirectToAction("AccessDenied", "Errors");
        }
        string approvalLevel;

        [HttpPost]
        public ActionResult SaveMultiplePO(List<procSearchPO_Result> searchPO_Results)
        {
            if (ModelState.IsValid)
            {
                if (searchPO_Results[0].criteria == "report")
                {
                    List<procSearchPO_Result> selectList = new List<procSearchPO_Result>();
                    string myCodes = string.Empty;
                    foreach (var item in searchPO_Results)
                    {

                        if (item.IsSelected)
                        {
                            using (var transaction = db.Database.BeginTransaction())
                            {
                                try
                                {



                                           
                                    
                                       
                                            if (myCodes.Length > 0)
                                            {
                                                myCodes += ", "; // Add a comma if data already exists
                                            }

                                           
                                    myCodes += "'" + item.intPOCode.ToString() + "'";






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
                    ViewBag.xpertLoginID = reqCookies["xpertLoginID"].ToString();
                    ViewBag.intPOCode = myCodes;
                    return View("PORpt");


                }

                else if (searchPO_Results[0].criteria == "reject")
                {
                    HttpCookie reqCookies = Request.Cookies["userInfo"];
                    string userCode =reqCookies["intUserCode"].ToString();


                    // Add checked item to the list and render them in view
                    List<procSearchPO_Result> selectList = new List<procSearchPO_Result>();
                    foreach (var item in searchPO_Results)
                    {

                        if (item.IsSelected && (item.strPOStatus != null || item.strPOStatus != ""))
                        {
                            using (var transaction = db.Database.BeginTransaction())
                            {
                                try
                                {

                                
                                        PODB.Add(item.intPOCode, userCode, "Rejected", searchPO_Results[0].strRejectReason);
                                 

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

                                        var POData = this.db.tblPOes.Find(item.intPOCode);

                                        POData.strPOStatus = "Rejected";

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
                    return Redirect("SearchPO");
                }
                else
                {
                    HttpCookie reqCookies = Request.Cookies["userInfo"];
                    string userCode = reqCookies["intUserCode"].ToString();

                    if (reqCookies["SuperAdmin"].ToString() == "Y")
                    {
                        // Add checked item to the list and render them in view
                        List<procSearchPO_Result> selectList = new List<procSearchPO_Result>();
                        foreach (var item in searchPO_Results)
                        {

                            if (item.IsSelected && (item.strPOStatus != null || item.strPOStatus != ""))
                            {
                                using (var transaction = db.Database.BeginTransaction())
                                {
                                    try
                                    {

                                        if (approvalLevel == "Approved")
                                        {
                                            PODB.Add(item.intPOCode, userCode, item.NextPOStatus, null);
                                        }
                                        else
                                        {

                                            //tblSupervisor Supervisor = new tblSupervisor();
                                            tblPOHistory objPOHistory = new tblPOHistory()
                                            {

                                                dtCreatedAt = DateTime.Now,
                                                intPOCode = item.intPOCode,
                                                strPOStatus = item.NextPOStatus,
                                                intUserCode = userCode

                                            };

                                            db.tblPOHistories.Add(objPOHistory);
                                            db.SaveChanges();

                                            //string lastid = objOvertime.uqWorkerCode.ToString();

                                            var POData = this.db.tblPOes.Find(item.intPOCode);
                                            
                                            POData.strPOStatus = item.NextPOStatus;

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
                        return Redirect("SearchPO");

                    }
                    else
                    {
                        // Add checked item to the list and render them in view
                        List<procSearchPO_Result> selectList = new List<procSearchPO_Result>();
                        foreach (var item in searchPO_Results)
                        {

                            if (item.IsSelected && (item.strPOStatus != null || item.strPOStatus != ""))
                            {
                                using (var transaction = db.Database.BeginTransaction())
                                {
                                    try
                                    {

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
                                            PODB.Add(item.intPOCode, userCode, approvalLevel, null);
                                        }
                                        else
                                        {

                                            //tblSupervisor Supervisor = new tblSupervisor();
                                            tblPOHistory objPOHistory = new tblPOHistory()
                                            {

                                                dtCreatedAt = DateTime.Now,
                                                intPOCode = item.intPOCode,
                                                strPOStatus = approvalLevel,
                                                intUserCode = userCode

                                            };

                                            db.tblPOHistories.Add(objPOHistory);
                                            db.SaveChanges();

                                            //string lastid = objOvertime.uqWorkerCode.ToString();

                                            var POData = this.db.tblPOes.Find(item.intPOCode);

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
                        return Redirect("SearchPO");
                    }
                  
                }
            }
            return View("SearchPO");
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
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            int userCode = Convert.ToInt32(reqCookies["intUserCode"]);
            List<tblStatu> statusList = db.tblStatus.ToList();
            ViewBag.Status = new MultiSelectList(statusList, "strStatusName", "strStatusName");

            var data = PODB.SearchPO(userCode, myCodes, tblPO.PO_Number);
            return View(data);

        }

        [HttpPost]
        public int SavePOHistory(int ID, string status, string strRejectReason)
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            string userCode = reqCookies["intUserCode"].ToString();
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




    }
}