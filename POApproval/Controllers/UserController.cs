using POApproval.GlobalInfo;
using POApproval.Helper;
using POApproval.Models;
using POApproval.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POApproval.Controllers
{
    
    public class UserController : Controller
    {
        dbSASAApprovalEntities db = new dbSASAApprovalEntities();
        UserDB userDB = new UserDB();

        //[Authorize]
        public List<procUserMenu_Result> GetUserMenusss(String userCode)
        {

            List<procUserMenu_Result> GetUserMenus = db.procUserMenu(userCode).ToList();

            return GetUserMenus;
        }
        public ActionResult UserList()
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if(reqCookies == null && string.IsNullOrEmpty(Session["intUserCode"] as string))
            {
                return RedirectToAction("Login", "Account");
            }
            else if (!string.IsNullOrEmpty(Session["intUserCode"] as string))
            {
                List<procUserMenu_Result> menus = GetUserMenusss(Session["intUserCode"].ToString());

                foreach (var item in menus)
                {


                    var data = menus.Where(x => x.menucode == item.menucode).FirstOrDefault();
                    var link = data.menulink.Split('/');
                    if (link[1].ToString() == "UserList")
                    {
                        String userCode = Session["intUserCode"].ToString();
                        //using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                        //{
                        var data1 = db.procSelectUser().ToList();
                        return View(data1);



                    }



                }
            }
            else
            {
                List<procUserMenu_Result> menus = GetUserMenusss(reqCookies["intUserCode"].ToString());

                foreach (var item in menus)
                {


                    var data = menus.Where(x => x.menucode == item.menucode).FirstOrDefault();
                    var link = data.menulink.Split('/');
                    if (link[1].ToString() == "UserList")
                    {
                        String userCode = reqCookies["intUserCode"].ToString();
                        //using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                        //{
                        var data1 = db.procSelectUser().ToList();
                        return View(data1);



                    }



                }
            }
           
            return RedirectToAction("AccessDenied", "Errors");
          
            //return Json(userDB.ListAll(), JsonRequestBehavior.AllowGet);
            //}
            //if (Session["Superadmin"].ToString() == "y")
            //{
            //    var data = db.procSelectUser().ToList();
            //    return View(data);
            //}
            //else
            //{
            //    var data = db.procSelectUser().Where(x => x.UserCode == userCode).ToList();
            //    return View(data);
            //}
        }

        //[Authorize]
        public ActionResult AddUser()
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies == null && string.IsNullOrEmpty(Session["intUserCode"] as string))
            {
                return RedirectToAction("Login", "Account");
            }
            else if (!string.IsNullOrEmpty(Session["intUserCode"] as string))
            {
                List<procUserMenu_Result> menus = GetUserMenusss(Session["intUserCode"].ToString());

                foreach (var item in menus)
                {


                    var data = menus.Where(x => x.menucode == item.menucode).FirstOrDefault();
                    var link = data.menulink.Split('/');
                    if (link[1].ToString() == "UserList")
                    {
                        userDataViewModel UserVM = new userDataViewModel();

                        UserVM.getUserLists = db.procSelectUserxpert().ToList();
                        UserVM.GetAccessLevels = db.procGetAccessLevels().ToList();

                        return View(UserVM);



                    }



                }
            }
            else
            {
                List<procUserMenu_Result> menus = GetUserMenusss(reqCookies["intUserCode"].ToString());

                foreach (var item in menus)
                {


                    var data = menus.Where(x => x.menucode == item.menucode).FirstOrDefault();
                    var link = data.menulink.Split('/');
                    if (link[1].ToString() == "UserList")
                    {
                        userDataViewModel UserVM = new userDataViewModel();

                        UserVM.getUserLists = db.procSelectUserxpert().ToList();
                        UserVM.GetAccessLevels = db.procGetAccessLevels().ToList();

                        return View(UserVM);



                    }



                }
            }
          
            return RedirectToAction("AccessDenied", "Errors");
          
        }
        //[Authorize]
        //[HttpPost]
        //public JsonResult GetDepartment(int? ID)
        //{
        //    List<SelectListItem> departments = new List<SelectListItem>();
        
        //    for (int i = 0; i < db.tblDepartments.ToList().Count; i++)
        //    {
        //        //var data = db.tblDepartments.Where(x => x.intDepartmentCode == ID).FirstOrDefault();
        //        //bool selected = false;
        //        //if (ID != null) {
        //        //    if(ID== Convert.ToInt32(db.tblDepartments.ToList()[i].intDepartmentCode.ToString()))
        //        //    {
        //        //        selected = false;
        //        //    }
        //        //} 
        //            departments.Add(new SelectListItem
        //            {
        //                Value = db.tblDepartments.ToList()[i].intDepartmentCode.ToString(),
        //                Text = db.tblDepartments.ToList()[i].strDepartmentName
        //                //Selected=selected
        //            });
                
                
        //    }

        //    return Json(departments, JsonRequestBehavior.AllowGet);
        //}
        
        //[Authorize]
        public ActionResult UpdateUser(string ID)
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies == null && string.IsNullOrEmpty(Session["intUserCode"] as string))
            {
                return RedirectToAction("Login", "Account");
            }
            else if (!string.IsNullOrEmpty(Session["intUserCode"] as string))
            {
                List<procUserMenu_Result> menus = GetUserMenusss(Session["intUserCode"].ToString());

                foreach (var item in menus)
                {


                    var data = menus.Where(x => x.menucode == item.menucode).FirstOrDefault();
                    var link = data.menulink.Split('/');
                    if (link[1].ToString() == "UserList")
                    {
                        var userInfo = db.procSelectUserData(ID).FirstOrDefault();
                        var usermeuids = db.procSelectUserDataMenu(ID).ToList();

                        // var datasss=usermeuids.Select(s=>s)

                        //string[] menuids = usermeuids.Split(',');
                        if (userInfo != null)
                        {
                            bool admin;
                            bool UserApprover;
                            if (userInfo.SuperAdmin == "Y")
                            {
                                admin = true;
                            }
                            else
                            {
                                admin = false;
                            }
                            if (userInfo.UserApprover == "Y")
                            {
                                UserApprover = true;
                            }
                            else
                            {
                                UserApprover = false;
                            }
                            UserViewModel objUser = new UserViewModel()
                            {
                                usercode = userInfo.UserCode,
                                usergroup = userInfo.Department,
                                email = userInfo.Email,
                                pwd = userInfo.Password,
                                fullname = userInfo.Username,
                                status = userInfo.Status,
                                usermenuids = usermeuids,
                                SuperAdmin = admin,
                                UserApprover = UserApprover,
                                xpertLoginID = userInfo.xpertLoginID
                            };
                            return View(objUser);
                        }

                        return View();



                    }



                }
            }
            else
            {
                List<procUserMenu_Result> menus = GetUserMenusss(reqCookies["intUserCode"].ToString());

                foreach (var item in menus)
                {


                    var data = menus.Where(x => x.menucode == item.menucode).FirstOrDefault();
                    var link = data.menulink.Split('/');
                    if (link[1].ToString() == "UserList")
                    {
                        var userInfo = db.procSelectUserData(ID).FirstOrDefault();
                        var usermeuids = db.procSelectUserDataMenu(ID).ToList();

                        // var datasss=usermeuids.Select(s=>s)

                        //string[] menuids = usermeuids.Split(',');
                        if (userInfo != null)
                        {
                            bool admin;
                            bool UserApprover;
                            if (userInfo.SuperAdmin == "Y")
                            {
                                admin = true;
                            }
                            else
                            {
                                admin = false;
                            }
                            if (userInfo.UserApprover == "Y")
                            {
                                UserApprover = true;
                            }
                            else
                            {
                                UserApprover = false;
                            }
                            UserViewModel objUser = new UserViewModel()
                            {
                                usercode = userInfo.UserCode,
                                usergroup = userInfo.Department,
                                email = userInfo.Email,
                                pwd = userInfo.Password,
                                fullname = userInfo.Username,
                                status = userInfo.Status,
                                usermenuids = usermeuids,
                                SuperAdmin = admin,
                                UserApprover = UserApprover,
                                xpertLoginID = userInfo.xpertLoginID
                            };
                            return View(objUser);
                        }

                        return View();



                    }



                }
            }
         
            return RedirectToAction("AccessDenied", "Errors");
            //PopulateDropdown();          
          
        }
        public void PopulateDropdown()
        {
          

                var department = db.procCmbDepartment().OrderBy(s => s.Department);
                ViewBag.departmentList = new SelectList(department, "Code", "Department");

          


        }


        /// <summary>  
        ///   
        /// Get All User  
        /// </summary>  
        /// <returns></returns>  

        //[Authorize]
        public ActionResult Get_AllUser()
        {
            String userCode = null;
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                 userCode = reqCookies["intUserCode"].ToString();

            }
            else
            {
                 userCode = Session["intUserCode"].ToString();

            }
            //using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
            //{

            //return Json(userDB.ListAll(), JsonRequestBehavior.AllowGet);
            //}
            if (reqCookies != null)
            {
                if (reqCookies["SuperAdmin"].ToString() == "Y")
                {
                    var data = db.procSelectUser().ToList();
                    return View(data);
                }
                else
                {
                    var data = db.procSelectUser().Where(x => x.UserCode == userCode).ToList();
                    return View(data);
                }
            }
            else
            {
                if (Session["SuperAdmin"].ToString() == "Y")
                {
                    var data = db.procSelectUser().ToList();
                    return View(data);
                }
                else
                {
                    var data = db.procSelectUser().Where(x => x.UserCode == userCode).ToList();
                    return View(data);
                }
            }
            
        }
        [HttpPost]
        public JsonResult userDetail(string ID)
        {
            var data = db.procSelectUserDetail().Where(x => x.UserCode == ID).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult userDetail(string usercode)
        //{
        //    //using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
        //    //{

        //    //return Json(userDB.ListAll(), JsonRequestBehavior.AllowGet);
        //    //}
        //    var data = db.procSelectUserDetail().Where(x=>x.UserCode==usercode).ToList();
        //    return View(data);
        //}
        /// <summary>  
        /// Get User With Id  
        /// </summary>  
        /// <param name="Id"></param>  
        /// <returns></returns>  
        //public JsonResult Get_UserById(string Id)
        //{
        //    using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
        //    {
        //        int EmpId = int.Parse(Id);
        //        return Json(Obj.Users.Find(EmpId), JsonRequestBehavior.AllowGet);
        //    }
        //}
        /// <summary>  
        /// Insert New User  
        /// </summary>  
        /// <param name="user"></param>  
        /// <returns></returns>  
        public int Insert_User(userDataViewModel data, List<procGetAccessLevels_Result> lstMembersToNotify)
        {
            int length=lstMembersToNotify.Count;
            //var checkUserEmailExist = db.procGetAllUsers().Where(x => x.email == userdata.email).FirstOrDefault();
            //var checkUsernameExist = db.procGetAllUsers().Where(x =>  x.usercode == userdata.usercode.ToString()).FirstOrDefault();
            //if (checkUsernameExist != null)
            //{
            //    return 2;
            //}
            //if (checkUserEmailExist != null)
            //{
            //    return 3;
            //}
            if (data != null)
            {
                using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                {
                    return userDB.Add(data, lstMembersToNotify);
                }
            }
            else
            {
                return 0;
            }
        }
        public JsonResult GetUserMenus()
        {
            String userCode = null;
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                userCode = reqCookies["intUserCode"].ToString();

            }
            else
            {
                userCode = Session["intUserCode"].ToString();

            }
            List<procUserMenu_Result> GetUserMenus = db.procUserMenu(userCode).ToList();

            return Json(GetUserMenus, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAccessMenus()
        {
            List<procGetAccessLevels_Result> GetAccessMenus = db.procGetAccessLevels().ToList();



            return Json(GetAccessMenus, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetSelectedMenus(string ID)
        //{
            
        //    List<procSelectUserDataMenu_Result> GetAccessMenusdata = db.procSelectUserDataMenu(ID).ToList();



        //    return Json(GetAccessMenusdata, JsonRequestBehavior.AllowGet);
        //}
        /// <summary>  
        /// Delete User Information  
        /// </summary>  
        /// <param name="Emp"></param>  
        /// <returns></returns>  
        public string Delete_User(string usercode)
        {
            if (usercode != null)
            {
                using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                {
                    return userDB.Delete(usercode);
                }
            }
            else
            {
                return "Not Deleted";
            }

            
            }
            /// <summary>  
            /// Update User Information  
            /// </summary>  
            /// <param name="Emp"></param>  
            /// <returns></returns>  
            public int Update_User(userDataViewModel data, List<procGetAccessLevels_Result> lstMembersToNotify)
        {
            if(lstMembersToNotify== null || lstMembersToNotify[0].menuCode==0)
            {
                return 2;
            }
            int length = lstMembersToNotify.Count;
            //var checkUserEmailExist = db.procGetAllUsers().Where(x => x.email == userdata.email).FirstOrDefault();
            //var checkUsernameExist = db.procGetAllUsers().Where(x =>  x.usercode == userdata.usercode.ToString()).FirstOrDefault();
            //if (checkUsernameExist != null)
            //{
            //    return 2;
            //}
            //if (checkUserEmailExist != null)
            //{
            //    return 3;
            //}
            if (data != null)
            {
                using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                {
                    return userDB.Update(data, lstMembersToNotify);
                }
            }
            else
            {
                return 0;
            }
        }
    }
}