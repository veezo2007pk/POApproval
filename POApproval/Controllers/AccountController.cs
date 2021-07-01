using POApproval.GlobalInfo;
using POApproval.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace POApproval.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
       
        [SessionAuthorizeAttributeForLogin]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(procSelectUserData_Result objUser)
        {
            if (ModelState.IsValid)
            {
                using (dbSASAApprovalEntities db = new dbSASAApprovalEntities())
                {
                    string pasword = objUser.Password.Replace(" ", "%");
                    int code = Convert.ToInt32(objUser.UserCode);
                    var obj = db.procValidateUserLogins(code, pasword).FirstOrDefault();
                  
                    if (obj != null  && obj.status == "ACTIVE")
                    {
                        Session["intUserCode"] = obj.usercode.ToString();
                        Session["strUser"] = obj.fullname.ToString();
                        Session["SuperAdmin"] = obj.SuperAdmin.ToString();
                        Session["bolIsApprovalLimit"] = obj.bolIsApprovalLimit.ToString();
                        Session["bolIsNewUser"] = obj.bolIsNewUser.ToString();
                        Session["bolIsNewBuyer"]= obj.bolIsNewBuyer.ToString();
                        Session["bolIsManageBuyer"] = obj.bolIsManageBuyer.ToString();
                        //FormsAuthentication.SetAuthCookie(obj.usercode, objUser.RememberMe);
                        return RedirectToAction("SearchPO", "PO");
                    }
                    else if(obj != null && obj.status == "NOT ACTIVE")
                    {
                        ViewBag.loginFailed = "Username is inactive. Please contact admin";
                        return View();
                    }
                    else
                    {
                        ViewBag.loginFailed = "Invalid Username or Password";
                        return View();
                    }
                }
            }
            return View(objUser);
        }
       
        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            Session["intUserCode"] = null;
            Session["strUser"] = null;
            Session["SuperAdmin"] = null;
            Session["bolIsApprovalLimit"] = null;
            Session["bolIsNewUser"] = null;
            Session["bolIsNewBuyer"] = null;
            Session["bolIsManageBuyer"] = null;
            return RedirectToAction("Login", "Account");
        }
    }
}