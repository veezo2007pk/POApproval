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
        public virtual DbSet<tblPOHistory> tblPOHistories { get; set; }
        public virtual DbSet<tblManageApproval> tblManageApprovals { get; set; }
        public virtual DbSet<tblStatu> tblStatus { get; set; }
        public virtual DbSet<tblBuyer> tblBuyers { get; set; }
        public virtual DbSet<tblBuyerDetail> tblBuyerDetails { get; set; }
        public virtual DbSet<tblPODetail> tblPODetails { get; set; }
        public virtual DbSet<tblPO> tblPOes { get; set; }
    
        public virtual ObjectResult<procCmbApprovalLevel_Result> procCmbApprovalLevel()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procCmbApprovalLevel_Result>("procCmbApprovalLevel");
        }
    
        public virtual ObjectResult<procCmbDepartment_Result> procCmbDepartment()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procCmbDepartment_Result>("procCmbDepartment");
        }
    
        public virtual int procDeleteManageApproval(Nullable<int> intManageApprovalCode)
        {
            var intManageApprovalCodeParameter = intManageApprovalCode.HasValue ?
                new ObjectParameter("intManageApprovalCode", intManageApprovalCode) :
                new ObjectParameter("intManageApprovalCode", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("procDeleteManageApproval", intManageApprovalCodeParameter);
        }
    
        public virtual int procInsertUpdateManageApproval(Nullable<int> intManageApprovalCode, Nullable<int> intUserCode, Nullable<int> intApprovalLevelCode, Nullable<decimal> numFromApprovalAmount, Nullable<decimal> numToApprovalAmount, Nullable<bool> bolIsActive, Nullable<System.DateTime> dtCreatedAt, Nullable<int> intCreatedByCode, Nullable<System.DateTime> dtModifyAt, Nullable<int> intModifyByCode, string action)
        {
            var intManageApprovalCodeParameter = intManageApprovalCode.HasValue ?
                new ObjectParameter("intManageApprovalCode", intManageApprovalCode) :
                new ObjectParameter("intManageApprovalCode", typeof(int));
    
            var intUserCodeParameter = intUserCode.HasValue ?
                new ObjectParameter("intUserCode", intUserCode) :
                new ObjectParameter("intUserCode", typeof(int));
    
            var intApprovalLevelCodeParameter = intApprovalLevelCode.HasValue ?
                new ObjectParameter("intApprovalLevelCode", intApprovalLevelCode) :
                new ObjectParameter("intApprovalLevelCode", typeof(int));
    
            var numFromApprovalAmountParameter = numFromApprovalAmount.HasValue ?
                new ObjectParameter("numFromApprovalAmount", numFromApprovalAmount) :
                new ObjectParameter("numFromApprovalAmount", typeof(decimal));
    
            var numToApprovalAmountParameter = numToApprovalAmount.HasValue ?
                new ObjectParameter("numToApprovalAmount", numToApprovalAmount) :
                new ObjectParameter("numToApprovalAmount", typeof(decimal));
    
            var bolIsActiveParameter = bolIsActive.HasValue ?
                new ObjectParameter("bolIsActive", bolIsActive) :
                new ObjectParameter("bolIsActive", typeof(bool));
    
            var dtCreatedAtParameter = dtCreatedAt.HasValue ?
                new ObjectParameter("dtCreatedAt", dtCreatedAt) :
                new ObjectParameter("dtCreatedAt", typeof(System.DateTime));
    
            var intCreatedByCodeParameter = intCreatedByCode.HasValue ?
                new ObjectParameter("intCreatedByCode", intCreatedByCode) :
                new ObjectParameter("intCreatedByCode", typeof(int));
    
            var dtModifyAtParameter = dtModifyAt.HasValue ?
                new ObjectParameter("dtModifyAt", dtModifyAt) :
                new ObjectParameter("dtModifyAt", typeof(System.DateTime));
    
            var intModifyByCodeParameter = intModifyByCode.HasValue ?
                new ObjectParameter("intModifyByCode", intModifyByCode) :
                new ObjectParameter("intModifyByCode", typeof(int));
    
            var actionParameter = action != null ?
                new ObjectParameter("Action", action) :
                new ObjectParameter("Action", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("procInsertUpdateManageApproval", intManageApprovalCodeParameter, intUserCodeParameter, intApprovalLevelCodeParameter, numFromApprovalAmountParameter, numToApprovalAmountParameter, bolIsActiveParameter, dtCreatedAtParameter, intCreatedByCodeParameter, dtModifyAtParameter, intModifyByCodeParameter, actionParameter);
        }
    
        public virtual ObjectResult<procSelectManageApproval_Result> procSelectManageApproval()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procSelectManageApproval_Result>("procSelectManageApproval");
        }
    
        public virtual int procInsertUpdatePOHistory(Nullable<int> intPOCode, string strPOStatus, Nullable<int> intUserCode, string strRejectReason, string action)
        {
            var intPOCodeParameter = intPOCode.HasValue ?
                new ObjectParameter("intPOCode", intPOCode) :
                new ObjectParameter("intPOCode", typeof(int));
    
            var strPOStatusParameter = strPOStatus != null ?
                new ObjectParameter("strPOStatus", strPOStatus) :
                new ObjectParameter("strPOStatus", typeof(string));
    
            var intUserCodeParameter = intUserCode.HasValue ?
                new ObjectParameter("intUserCode", intUserCode) :
                new ObjectParameter("intUserCode", typeof(int));
    
            var strRejectReasonParameter = strRejectReason != null ?
                new ObjectParameter("strRejectReason", strRejectReason) :
                new ObjectParameter("strRejectReason", typeof(string));
    
            var actionParameter = action != null ?
                new ObjectParameter("Action", action) :
                new ObjectParameter("Action", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("procInsertUpdatePOHistory", intPOCodeParameter, strPOStatusParameter, intUserCodeParameter, strRejectReasonParameter, actionParameter);
        }
    
        public virtual ObjectResult<procRptPOSubReport_Result> procRptPOSubReport(Nullable<int> intPoCode)
        {
            var intPoCodeParameter = intPoCode.HasValue ?
                new ObjectParameter("intPoCode", intPoCode) :
                new ObjectParameter("intPoCode", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procRptPOSubReport_Result>("procRptPOSubReport", intPoCodeParameter);
        }
    
        public virtual ObjectResult<procPendingPO_Result> procPendingPO(Nullable<int> intUserCode)
        {
            var intUserCodeParameter = intUserCode.HasValue ?
                new ObjectParameter("intUserCode", intUserCode) :
                new ObjectParameter("intUserCode", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procPendingPO_Result>("procPendingPO", intUserCodeParameter);
        }
    
        public virtual ObjectResult<procSearchPO_Result> procSearchPO(Nullable<int> intUserCode, string pO_Number, string strPOStatus)
        {
            var intUserCodeParameter = intUserCode.HasValue ?
                new ObjectParameter("intUserCode", intUserCode) :
                new ObjectParameter("intUserCode", typeof(int));
    
            var pO_NumberParameter = pO_Number != null ?
                new ObjectParameter("PO_Number", pO_Number) :
                new ObjectParameter("PO_Number", typeof(string));
    
            var strPOStatusParameter = strPOStatus != null ?
                new ObjectParameter("strPOStatus", strPOStatus) :
                new ObjectParameter("strPOStatus", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procSearchPO_Result>("procSearchPO", intUserCodeParameter, pO_NumberParameter, strPOStatusParameter);
        }
    
        public virtual ObjectResult<procSelectBuyer_Result> procSelectBuyer()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procSelectBuyer_Result>("procSelectBuyer");
        }
    
        public virtual int procInsertUpdateBuyer(Nullable<int> intBuyerCode, string strBuyerName, Nullable<System.DateTime> dtCreatedAt, Nullable<int> intCreatedByCode, Nullable<System.DateTime> dtModifyAt, Nullable<int> intModifyBy, Nullable<bool> bolIsActive, string action)
        {
            var intBuyerCodeParameter = intBuyerCode.HasValue ?
                new ObjectParameter("intBuyerCode", intBuyerCode) :
                new ObjectParameter("intBuyerCode", typeof(int));
    
            var strBuyerNameParameter = strBuyerName != null ?
                new ObjectParameter("strBuyerName", strBuyerName) :
                new ObjectParameter("strBuyerName", typeof(string));
    
            var dtCreatedAtParameter = dtCreatedAt.HasValue ?
                new ObjectParameter("dtCreatedAt", dtCreatedAt) :
                new ObjectParameter("dtCreatedAt", typeof(System.DateTime));
    
            var intCreatedByCodeParameter = intCreatedByCode.HasValue ?
                new ObjectParameter("intCreatedByCode", intCreatedByCode) :
                new ObjectParameter("intCreatedByCode", typeof(int));
    
            var dtModifyAtParameter = dtModifyAt.HasValue ?
                new ObjectParameter("dtModifyAt", dtModifyAt) :
                new ObjectParameter("dtModifyAt", typeof(System.DateTime));
    
            var intModifyByParameter = intModifyBy.HasValue ?
                new ObjectParameter("intModifyBy", intModifyBy) :
                new ObjectParameter("intModifyBy", typeof(int));
    
            var bolIsActiveParameter = bolIsActive.HasValue ?
                new ObjectParameter("bolIsActive", bolIsActive) :
                new ObjectParameter("bolIsActive", typeof(bool));
    
            var actionParameter = action != null ?
                new ObjectParameter("Action", action) :
                new ObjectParameter("Action", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("procInsertUpdateBuyer", intBuyerCodeParameter, strBuyerNameParameter, dtCreatedAtParameter, intCreatedByCodeParameter, dtModifyAtParameter, intModifyByParameter, bolIsActiveParameter, actionParameter);
        }
    
        public virtual ObjectResult<procSelectBuyerManager_Result> procSelectBuyerManager()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procSelectBuyerManager_Result>("procSelectBuyerManager");
        }
    
        public virtual ObjectResult<procSelectUser_Result> procSelectUser()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procSelectUser_Result>("procSelectUser");
        }
    
        public virtual ObjectResult<procValidateUserLogins_Result> procValidateUserLogins(Nullable<int> strLoginName, string strUserPassword)
        {
            var strLoginNameParameter = strLoginName.HasValue ?
                new ObjectParameter("strLoginName", strLoginName) :
                new ObjectParameter("strLoginName", typeof(int));
    
            var strUserPasswordParameter = strUserPassword != null ?
                new ObjectParameter("strUserPassword", strUserPassword) :
                new ObjectParameter("strUserPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procValidateUserLogins_Result>("procValidateUserLogins", strLoginNameParameter, strUserPasswordParameter);
        }
    
        public virtual ObjectResult<procGetAllUsers_Result> procGetAllUsers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procGetAllUsers_Result>("procGetAllUsers");
        }
    
        public virtual ObjectResult<procGetAllBuyersByStaffType_Result> procGetAllBuyersByStaffType(string stafftype)
        {
            var stafftypeParameter = stafftype != null ?
                new ObjectParameter("stafftype", stafftype) :
                new ObjectParameter("stafftype", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procGetAllBuyersByStaffType_Result>("procGetAllBuyersByStaffType", stafftypeParameter);
        }
    
        public virtual int procInsertUpdateUser(string usercode, string xpertLoginID, string pwd, string status, string superAdmin, Nullable<bool> bolIsApprovalLimit, Nullable<bool> bolIsNewUser, Nullable<bool> bolIsNewBuyer, Nullable<bool> bolIsManageBuyer, string action)
        {
            var usercodeParameter = usercode != null ?
                new ObjectParameter("usercode", usercode) :
                new ObjectParameter("usercode", typeof(string));
    
            var xpertLoginIDParameter = xpertLoginID != null ?
                new ObjectParameter("xpertLoginID", xpertLoginID) :
                new ObjectParameter("xpertLoginID", typeof(string));
    
            var pwdParameter = pwd != null ?
                new ObjectParameter("pwd", pwd) :
                new ObjectParameter("pwd", typeof(string));
    
            var statusParameter = status != null ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(string));
    
            var superAdminParameter = superAdmin != null ?
                new ObjectParameter("SuperAdmin", superAdmin) :
                new ObjectParameter("SuperAdmin", typeof(string));
    
            var bolIsApprovalLimitParameter = bolIsApprovalLimit.HasValue ?
                new ObjectParameter("bolIsApprovalLimit", bolIsApprovalLimit) :
                new ObjectParameter("bolIsApprovalLimit", typeof(bool));
    
            var bolIsNewUserParameter = bolIsNewUser.HasValue ?
                new ObjectParameter("bolIsNewUser", bolIsNewUser) :
                new ObjectParameter("bolIsNewUser", typeof(bool));
    
            var bolIsNewBuyerParameter = bolIsNewBuyer.HasValue ?
                new ObjectParameter("bolIsNewBuyer", bolIsNewBuyer) :
                new ObjectParameter("bolIsNewBuyer", typeof(bool));
    
            var bolIsManageBuyerParameter = bolIsManageBuyer.HasValue ?
                new ObjectParameter("bolIsManageBuyer", bolIsManageBuyer) :
                new ObjectParameter("bolIsManageBuyer", typeof(bool));
    
            var actionParameter = action != null ?
                new ObjectParameter("Action", action) :
                new ObjectParameter("Action", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("procInsertUpdateUser", usercodeParameter, xpertLoginIDParameter, pwdParameter, statusParameter, superAdminParameter, bolIsApprovalLimitParameter, bolIsNewUserParameter, bolIsNewBuyerParameter, bolIsManageBuyerParameter, actionParameter);
        }
    
        public virtual ObjectResult<procSelectUserData_Result> procSelectUserData(string usercode)
        {
            var usercodeParameter = usercode != null ?
                new ObjectParameter("usercode", usercode) :
                new ObjectParameter("usercode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procSelectUserData_Result>("procSelectUserData", usercodeParameter);
        }
    
        public virtual ObjectResult<procSelectUserDetail_Result> procSelectUserDetail()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procSelectUserDetail_Result>("procSelectUserDetail");
        }
    
        public virtual int procDeleteUser(string intUserCode)
        {
            var intUserCodeParameter = intUserCode != null ?
                new ObjectParameter("intUserCode", intUserCode) :
                new ObjectParameter("intUserCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("procDeleteUser", intUserCodeParameter);
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
    
        public virtual ObjectResult<procSelectUserxpert_Result> procSelectUserxpert()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procSelectUserxpert_Result>("procSelectUserxpert");
        }
    
        public virtual ObjectResult<procGetAccessLevels_Result> procGetAccessLevels()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procGetAccessLevels_Result>("procGetAccessLevels");
        }
    }
}
