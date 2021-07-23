﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POApproval.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class dbSASAApprovalEntities : DbContext
    {
        public dbSASAApprovalEntities()
            : base("name=dbSASAApprovalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblApprovalLevel> tblApprovalLevels { get; set; }
        public virtual DbSet<tblBuyer> tblBuyers { get; set; }
        public virtual DbSet<tblBuyerDetail> tblBuyerDetails { get; set; }
        public virtual DbSet<tblPO> tblPOes { get; set; }
        public virtual DbSet<tblPODetail> tblPODetails { get; set; }
        public virtual DbSet<tblPOHistory> tblPOHistories { get; set; }
        public virtual DbSet<tblStatu> tblStatus { get; set; }
        public virtual DbSet<tblManageApproval> tblManageApprovals { get; set; }
    
        public virtual ObjectResult<procCmbApprovalLevel_Result> procCmbApprovalLevel()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procCmbApprovalLevel_Result>("procCmbApprovalLevel");
        }
    
        public virtual ObjectResult<procCmbDepartment_Result> procCmbDepartment()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procCmbDepartment_Result>("procCmbDepartment");
        }
    
        public virtual ObjectResult<procGetAccessLevels_Result> procGetAccessLevels()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procGetAccessLevels_Result>("procGetAccessLevels");
        }
    
        public virtual ObjectResult<procGetAllBuyersByStaffType_Result> procGetAllBuyersByStaffType(string stafftype)
        {
            var stafftypeParameter = stafftype != null ?
                new ObjectParameter("stafftype", stafftype) :
                new ObjectParameter("stafftype", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procGetAllBuyersByStaffType_Result>("procGetAllBuyersByStaffType", stafftypeParameter);
        }
    
        public virtual ObjectResult<procGetAllBuyersByStaffTypeManageApproval_Result> procGetAllBuyersByStaffTypeManageApproval(string stafftype, Nullable<int> intUserCode)
        {
            var stafftypeParameter = stafftype != null ?
                new ObjectParameter("stafftype", stafftype) :
                new ObjectParameter("stafftype", typeof(string));
    
            var intUserCodeParameter = intUserCode.HasValue ?
                new ObjectParameter("intUserCode", intUserCode) :
                new ObjectParameter("intUserCode", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procGetAllBuyersByStaffTypeManageApproval_Result>("procGetAllBuyersByStaffTypeManageApproval", stafftypeParameter, intUserCodeParameter);
        }
    
        public virtual ObjectResult<procGetAllUsers_Result> procGetAllUsers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procGetAllUsers_Result>("procGetAllUsers");
        }
    
        public virtual ObjectResult<procGetUserApprovalLog_Result> procGetUserApprovalLog(string usercode)
        {
            var usercodeParameter = usercode != null ?
                new ObjectParameter("usercode", usercode) :
                new ObjectParameter("usercode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procGetUserApprovalLog_Result>("procGetUserApprovalLog", usercodeParameter);
        }
    
        public virtual int procRptPO(string intPOCode, string strUser)
        {
            var intPOCodeParameter = intPOCode != null ?
                new ObjectParameter("intPOCode", intPOCode) :
                new ObjectParameter("intPOCode", typeof(string));
    
            var strUserParameter = strUser != null ?
                new ObjectParameter("strUser", strUser) :
                new ObjectParameter("strUser", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("procRptPO", intPOCodeParameter, strUserParameter);
        }
    
        public virtual ObjectResult<procRptPOSubReport_Result> procRptPOSubReport(Nullable<int> intPoCode)
        {
            var intPoCodeParameter = intPoCode.HasValue ?
                new ObjectParameter("intPoCode", intPoCode) :
                new ObjectParameter("intPoCode", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procRptPOSubReport_Result>("procRptPOSubReport", intPoCodeParameter);
        }
    
        public virtual ObjectResult<string> procSearchPO(string intUserCode, string pO_Number, string strPOStatus)
        {
            var intUserCodeParameter = intUserCode != null ?
                new ObjectParameter("intUserCode", intUserCode) :
                new ObjectParameter("intUserCode", typeof(string));
    
            var pO_NumberParameter = pO_Number != null ?
                new ObjectParameter("PO_Number", pO_Number) :
                new ObjectParameter("PO_Number", typeof(string));
    
            var strPOStatusParameter = strPOStatus != null ?
                new ObjectParameter("strPOStatus", strPOStatus) :
                new ObjectParameter("strPOStatus", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("procSearchPO", intUserCodeParameter, pO_NumberParameter, strPOStatusParameter);
        }
    
        public virtual ObjectResult<procSelectBuyer_Result> procSelectBuyer()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procSelectBuyer_Result>("procSelectBuyer");
        }
    
        public virtual ObjectResult<procSelectBuyerManager_Result> procSelectBuyerManager()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procSelectBuyerManager_Result>("procSelectBuyerManager");
        }
    
        public virtual ObjectResult<procSelectUser_Result> procSelectUser()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procSelectUser_Result>("procSelectUser");
        }
    
        public virtual ObjectResult<procSelectUserData_Result> procSelectUserData(string usercode)
        {
            var usercodeParameter = usercode != null ?
                new ObjectParameter("usercode", usercode) :
                new ObjectParameter("usercode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procSelectUserData_Result>("procSelectUserData", usercodeParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> procSelectUserDataMenu(string usercode)
        {
            var usercodeParameter = usercode != null ?
                new ObjectParameter("usercode", usercode) :
                new ObjectParameter("usercode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("procSelectUserDataMenu", usercodeParameter);
        }
    
        public virtual ObjectResult<procSelectUserDetail_Result> procSelectUserDetail()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procSelectUserDetail_Result>("procSelectUserDetail");
        }
    
        public virtual ObjectResult<procSelectUserxpert_Result> procSelectUserxpert()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procSelectUserxpert_Result>("procSelectUserxpert");
        }
    
        public virtual ObjectResult<procValidateUserLogins_Result> procValidateUserLogins(string strLoginName, string strUserPassword)
        {
            var strLoginNameParameter = strLoginName != null ?
                new ObjectParameter("strLoginName", strLoginName) :
                new ObjectParameter("strLoginName", typeof(string));
    
            var strUserPasswordParameter = strUserPassword != null ?
                new ObjectParameter("strUserPassword", strUserPassword) :
                new ObjectParameter("strUserPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procValidateUserLogins_Result>("procValidateUserLogins", strLoginNameParameter, strUserPasswordParameter);
        }
    
        public virtual ObjectResult<procSelectBuyerManageApproval_Result> procSelectBuyerManageApproval(string intUserCode)
        {
            var intUserCodeParameter = intUserCode != null ?
                new ObjectParameter("intUserCode", intUserCode) :
                new ObjectParameter("intUserCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procSelectBuyerManageApproval_Result>("procSelectBuyerManageApproval", intUserCodeParameter);
        }
    
        public virtual ObjectResult<procSelectManageApproval_Result> procSelectManageApproval()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procSelectManageApproval_Result>("procSelectManageApproval");
        }
    
        public virtual ObjectResult<procUserMenu_Result> procUserMenu(string usercode)
        {
            var usercodeParameter = usercode != null ?
                new ObjectParameter("usercode", usercode) :
                new ObjectParameter("usercode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procUserMenu_Result>("procUserMenu", usercodeParameter);
        }
    }
}
