using Aspose.Zip;
using Microsoft.Reporting.WebForms;
using POApproval.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace POApproval.Report
{
    public partial class RptPO : System.Web.UI.Page
    {
        public int intPOCode;
        PODB PODB = new PODB();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string searchText = string.Empty;
                string strUser = string.Empty;
                if (Request.QueryString["searchText"] != null)
                {
                    searchText = Request.QueryString["searchText"].ToString();
                }
                if (Request.QueryString["strUser"] != null)
                {
                    strUser = Request.QueryString["strUser"].ToString();
                }
               

                using (var _context = new dbSASAApprovalEntities())
                {
                    
                        var summary = PODB.GetPOReport(searchText).ToList();

                        CustomerListReportViewer.LocalReport.ReportPath = Server.MapPath("~/Report/RDLC/RptPOList.rdlc");
                        CustomerListReportViewer.LocalReport.DataSources.Clear();
                        ReportDataSource rdc = new ReportDataSource("DataSet1", summary);
                        CustomerListReportViewer.LocalReport.DataSources.Add(rdc);



                        CustomerListReportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
                        CustomerListReportViewer.ShowReportBody = true;
                        CustomerListReportViewer.ShowPromptAreaButton = true;

                        CustomerListReportViewer.LocalReport.Refresh();
                        CustomerListReportViewer.DataBind();

                       

                    
                  



                }
            }
        }
        //protected void Export(object sender, EventArgs e)
        //{
        //    Warning[] warnings;
        //    string[] streamIds;
        //    string contentType;
        //    string encoding;
        //    string extension;

        //    //Export the RDLC Report to Byte Array.
        //    byte[] bytes = CustomerListReportViewer.LocalReport.Render(rbFormat.SelectedItem.Value, null, out contentType, out encoding, out extension, out streamIds, out warnings);

        //    //Download the RDLC Report in Word, Excel, PDF and Image formats.
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.Charset = "";
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.ContentType = contentType;
        //    Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
        //    Response.BinaryWrite(bytes);
        //    Response.Flush();
        //    Response.End();
        //}
        public void SetSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
            using (var _context = new dbSASAApprovalEntities())
            {
                var summary = _context.procRptPOSubReport(intPOCode).ToList();
                ReportDataSource datasource = new ReportDataSource("DataSet1", summary);
                e.DataSources.Add(datasource);
            }
        }
    }
}