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
   
    public class UserController : Controller
    {
        dbSASAApprovalEntities db = new dbSASAApprovalEntities();
        UserDB userDB = new UserDB();
        
        [Authorize]
        public ActionResult UserList()
        {
            var data = db.procSelectUser().ToList();
            return View(data);
        }
        
        [Authorize]
        public ActionResult AddUser()
        {
            PopulateDropdown();
            return View();
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
        
        [Authorize]
        public ActionResult UpdateUser(int ID)
        {
            PopulateDropdown();          
            var userInfo = db.tblUsers.FirstOrDefault(s => s.intUserCode == ID);
            if (userInfo != null)
            {
                UserViewModel objUser = new UserViewModel()
                {
                    intUserCode=userInfo.intUserCode,
                    strDepartmentName = userInfo.strDepartmentName,
                    email = userInfo.email,
                    UserPassword=userInfo.UserPassword,
                    logon_user_id = userInfo.logon_user_id,
                    bolIsActive=userInfo.bolIsActive,
                    bolIsApprovalLimit=userInfo.bolIsApprovalLimit,
                    bolIsNewUser=userInfo.bolIsNewUser
                 
                    
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
       
        [Authorize]
        public ActionResult Get_AllUser()
        {
            //using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
            //{

               //return Json(userDB.ListAll(), JsonRequestBehavior.AllowGet);
            //}
            var data = db.procSelectUser().ToList();
            return View(data);
        }
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
        public int Insert_User(tblUser user)
        {
            var checkUserEmailExist = db.tblUsers.Where(x => x.email == user.email).FirstOrDefault();
            var checkUsernameExist = db.tblUsers.Where(x => x.logon_user_id == user.logon_user_id).FirstOrDefault();
            if (checkUsernameExist != null)
            {
                return 2;
            }
            if (checkUserEmailExist != null)
            {
                return 3;
            }
            if (user != null)
            {
                using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                {

                    return userDB.Add(user);
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
        //public string Delete_User(User Emp)
        //{
        //    if (Emp != null)
        //    {
        //        using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
        //        {
        //            var Emp_ = Obj.Entry(Emp);
        //            if (Emp_.State == System.Data.Entity.EntityState.Detached)
        //            {
        //                Obj.Users.Attach(Emp);
        //                Obj.Users.Remove(Emp);
        //            }
        //            Obj.SaveChanges();
        //            return "User Deleted Successfully";
        //        }
        //    }
        //    else
        //    {
        //        return "User Not Deleted! Try Again";
        //    }
        //}
        /// <summary>  
        /// Update User Information  
        /// </summary>  
        /// <param name="Emp"></param>  
        /// <returns></returns>  
        public int Update_User(tblUser User)
        {
            var user = db.tblUsers.Where(x => x.intUserCode == User.intUserCode).FirstOrDefault();

            if (user.logon_user_id != User.logon_user_id)
            {
                var checkUsernameExist = db.tblUsers.Where(x => x.logon_user_id == user.logon_user_id).FirstOrDefault();

                if (checkUsernameExist != null)
                {
                    return 2;
                }
            }
            if (user.email != User.email)
            {
                var checkUserEmailExist = db.tblUsers.Where(x => x.email == user.email).FirstOrDefault();

                if (checkUserEmailExist != null)
                {
                    return 3;
                }
            }
           
            if (User != null)
            {
                using (dbSASAApprovalEntities Obj = new dbSASAApprovalEntities())
                {
                    return userDB.Update(User);
                }
            }
            else
            {
                return 0;
            }
        }
    }
}