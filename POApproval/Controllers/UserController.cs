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
        public ActionResult UserList()
        {
            var data = db.procSelectUser().ToList();
            return View(data);
        }
        
        //[Authorize]
        public ActionResult AddUser()
        {
            userDataViewModel UserVM = new userDataViewModel();
            
            UserVM.getUserList = db.procSelectUser().ToList();

            return View(UserVM);
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
            //PopulateDropdown();          
            var userInfo = db.procSelectUserData(ID).FirstOrDefault();
            if (userInfo != null)
            {
                bool admin;
                if (userInfo.SuperAdmin == "Y")
                {
                    admin = true;
                }
                else
                {
                    admin = false;
                }
                UserViewModel objUser = new UserViewModel()
                {
                    usercode=userInfo.UserCode,
                    usergroup = userInfo.Department,
                    email = userInfo.Email,
                    pwd = userInfo.Password,
                    fullname = userInfo.Username,
                    status = userInfo.Status,
                    bolIsApprovalLimit=userInfo.bolIsApprovalLimit,
                    bolIsNewUser=userInfo.bolIsNewUser,
                    bolIsManageBuyer = userInfo.bolIsManageBuyer,
                    bolIsNewBuyer = userInfo.bolIsNewBuyer,
                    SuperAdmin=admin,
                    xpertLoginID=userInfo.xpertLoginID
                };
                return View(objUser);
            }

            return View();
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
            //using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
            //{

               //return Json(userDB.ListAll(), JsonRequestBehavior.AllowGet);
            //}
            var data = db.procSelectUser().ToList();
            return View(data);
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
        public int Insert_User(userDataViewModel userdata)
        {
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
            if (userdata != null)
            {
                using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                {
                    return userDB.Add(userdata);
                }
            }
            else
            {
                return 0;
            }
        }
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
            public int Update_User(userDataViewModel userdatas)
        {
            //var user = db.tblUsers.Where(x => x.intUserCode == User.intUserCode).FirstOrDefault();

            //if (user.logon_user_id != User.logon_user_id)
            //{
            //    var checkUsernameExist = db.tblUsers.Where(x => x.logon_user_id == user.logon_user_id).FirstOrDefault();

            //    if (checkUsernameExist != null)
            //    {
            //        return 2;
            //    }
            //}
            //if (user.email != User.email)
            //{
            //    var checkUserEmailExist = db.tblUsers.Where(x => x.email == user.email).FirstOrDefault();

            //    if (checkUserEmailExist != null)
            //    {
            //        return 3;
            //    }
            //}
           
            if (userdatas != null)
            {
                using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                {
                    //return userDB.Update(userdatas);
                    return userDB.Update(userdatas);
                }
            }
            else
            {
                return 0;
            }
        }
    }
}