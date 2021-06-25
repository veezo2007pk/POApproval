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
    public class POController : Controller
    {
        dbSASAApprovalEntities db = new dbSASAApprovalEntities();
        PODB PODB = new PODB();


        // GET: PO
        public ActionResult PORpt(int intPOCode, string strUser)
        {
            ViewBag.intPOCode = intPOCode;
            ViewBag.strUser = strUser;
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
        public List<tblPOHistory> GetPOHistoriesModel(int ID)
        {
            List<tblPOHistory> POHistoryModel = db.tblPOHistories.Where(x => x.intPOCode == ID).ToList();
            return POHistoryModel;
        }
        public List<tblManageApproval> GetManageApprovalModel()
        {
            int ID = Convert.ToInt32(Session["intUserCode"]);
            List<tblManageApproval> ManageApprovalModel = db.tblManageApprovals.Where(x => x.intUserCode == ID).ToList();
            return ManageApprovalModel;
        }
        //[Authorize]
        public ActionResult ReviewPO(int ID)
        {
            POViewModel POVM = new POViewModel();
            POVM.tblPO = GetPOModel(ID);
            POVM.tblPODetails = GetPODetailsModel(ID);
            POVM.tblPOHistories = GetPOHistoriesModel(ID);
            POVM.tblManageApprovals = GetManageApprovalModel();
            ViewBag.strUser = Session["strUser"].ToString();
            return View(POVM);

        }

        //[Authorize]
        public ActionResult SearchPO()
        {
            List<tblStatu> statusList = db.tblStatus.ToList();
            ViewBag.Status = new MultiSelectList(statusList, "strStatusName", "strStatusName");

            int userCode = Convert.ToInt32(Session["intUserCode"]);
            var data = db.procSearchPO(userCode, null, "Pending").ToList();
            return View(data);

        }
        string approvalLevel;

        [HttpPost]
        public ActionResult SaveMultiplePO(List<procSearchPO_Result> searchPO_Results)
        {
            if (ModelState.IsValid)
            {

                if (searchPO_Results[0].strPOStatus == "reject")
                {
                    int userCode = Convert.ToInt32(Session["intUserCode"]);


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

                    int userCode = Convert.ToInt32(Session["intUserCode"]);


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
            return View("SearchPO");
        }

        [HttpPost]
        public ActionResult SearchPO(tblPO tblPO)
        {
            string myCodes = string.Empty;
            if (tblPO.strStatusName != null)
            {
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

            int userCode = Convert.ToInt32(Session["intUserCode"]);
            List<tblStatu> statusList = db.tblStatus.ToList();
            ViewBag.Status = new MultiSelectList(statusList, "strStatusName", "strStatusName");

            var data = PODB.SearchPO(userCode, myCodes, tblPO.PO_Number);
            return View(data);

        }

        [HttpPost]
        public int SavePOHistory(int ID, string status, string strRejectReason)
        {
            int userCode = Convert.ToInt32(Session["intUserCode"]);
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