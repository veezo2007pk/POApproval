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
        dbSASAApprovalEntities db = new dbSASAApprovalEntities();
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            //if (HttpContext.Request.Cookies["userInfo"] != null)
            //{0
                HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                List<procUserMenu_Result> menus = GetUserMenus(reqCookies["intUserCode"].ToString());
                int? minimum = int.MaxValue;
                foreach (var item in menus)
                {
                    if (item.menucode == 1004)
                    {
                        return RedirectToAction("SearchPO", "PO");

                    }
                    else
                    {
                        int? num = item.sys_menu_sort;
                        if (num < minimum)
                            minimum = num;
                        var data = menus.Where(x => x.sys_menu_sort == minimum).FirstOrDefault();
                        var link = data.menulink.Split('/');
                        return RedirectToAction(link[1].ToString(), link[0].ToString());

                    }

                }
            }
           
                return View();
            
                
            //}
            //else
            //{
            //    return View();
            //}


        }
        public List<procUserMenu_Result> GetUserMenus(String userCode)
        {
           
            List<procUserMenu_Result> GetUserMenus = db.procUserMenu(userCode).ToList();

            return GetUserMenus;
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(procSelectUserData_Result objUser)
        {
            if (ModelState.IsValid)
            {
                using (dbSASAApprovalEntities db = new dbSASAApprovalEntities())
                {
                    string pasword = objUser.Password.Replace(" ", "%");
                    int code = Convert.ToInt32(objUser.UserCode);
                    var objUsername = db.procValidateUserLogins(objUser.UserCode, null).FirstOrDefault();
                    if(objUsername != null)
                    {

                        var obj = db.procValidateUserLogins(objUser.UserCode, pasword).FirstOrDefault();

                        if (obj != null && obj.status == "ACTIVE")
                        {
                            if (objUser.RememberMe == true)
                            {
                                // They do, so let's create an authentication cookie
                                var cookie = FormsAuthentication.GetAuthCookie(obj.usercode, objUser.RememberMe);
                                // Since they want to be remembered, set the expiration for 30 days
                                cookie.Expires = DateTime.Now.AddDays(30);
                                // Store the cookie in the Response
                                Response.Cookies.Add(cookie);

                                HttpCookie userInfo = new HttpCookie("userInfo");
                                userInfo["xpertLoginID"] = obj.xpertLoginID.ToString();
                                userInfo["Password"] = obj.Password.ToString();
                                userInfo["intUserCode"] = obj.usercode.ToString();
                                userInfo["strUser"] = obj.fullname.ToString();
                                userInfo["SuperAdmin"] = obj.SuperAdmin.ToString();
                                userInfo.Expires = DateTime.Now.AddDays(30);
                                Response.Cookies.Add(userInfo);
                            }
                            else
                            {
                                Session["xpertLoginID"] = obj.xpertLoginID.ToString();
                                Session["intUserCode"] = obj.usercode.ToString();
                                Session["Password"] = obj.Password.ToString();
                                Session["strUser"] = obj.fullname.ToString();
                                Session["SuperAdmin"] = obj.SuperAdmin.ToString();
                            }
                            //Session["xpertLoginID"] = obj.xpertLoginID.ToString();
                            //Session["intUserCode"] = obj.usercode.ToString();
                            //Session["strUser"] = obj.fullname.ToString();
                            //Session["SuperAdmin"] = obj.SuperAdmin.ToString();
                            List<procUserMenu_Result> menus = GetUserMenus(objUser.UserCode.ToString());
                            if (menus.Count == 0)
                            {
                                ViewBag.loginFailed = "User don't have permission to view any page";
                            }
                            int? minimum = int.MaxValue;
                            foreach (var item in menus)
                            {
                                if (item.menucode == 1004)
                                {
                                    return RedirectToAction("SearchPO", "PO");

                                }
                                else
                                {
                                    int? num = item.sys_menu_sort;
                                    if (num < minimum)
                                        minimum = num;
                                    var data = menus.Where(x => x.sys_menu_sort == minimum).FirstOrDefault();
                                    var link = data.menulink.Split('/');
                                    return RedirectToAction(link[1].ToString(), link[0].ToString());

                                }

                            }


                            //FormsAuthentication.SetAuthCookie(obj.usercode, objUser.RememberMe);

                        }
                        else if (obj != null && obj.status == "INACTIVE")
                        {
                            ViewBag.loginFailed = "Username is inactive. Please contact admin";
                            return View();
                        }
                        else
                        {
                            ViewBag.loginFailed = "Invalid Password";
                            return View();
                        }


                    }
                    else
                    {
                        ViewBag.loginFailed = "Invalid Username";
                        return View();
                    }
                }
            }
            return View(objUser);
        }
       
        public ActionResult Logout()
        {
            Session.Abandon();
            Response.Cookies.Clear();
            
                  HttpCookie notreme = new HttpCookie("notreme");
            HttpCookie myCookie = new HttpCookie("userInfo");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            notreme.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
            Response.Cookies.Add(notreme);
            return RedirectToAction("Login", "Account");
        }
        public ActionResult ClearCookie()
        {
            Session.Abandon();
            Response.Cookies.Clear();

            HttpCookie notreme = new HttpCookie("notreme");
            HttpCookie myCookie = new HttpCookie("userInfo");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            notreme.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
            Response.Cookies.Add(notreme);
            return RedirectToAction("Login", "Account");
        }
    }
}