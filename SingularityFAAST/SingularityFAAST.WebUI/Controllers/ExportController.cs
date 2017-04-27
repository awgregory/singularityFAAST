using System;
using System.Web.Mvc;
using SingularityFAAST.Services.Services;

namespace SingularityFAAST.WebUI.Controllers
{
    public class ExportController : Controller
    {
        private readonly ClientServices clientServices;
        private readonly ExportServices exportServices;
        private readonly ReportingServices reportingServices;

        public ExportController()
        {
            clientServices = new ClientServices();
            exportServices = new ExportServices();
            reportingServices = new ReportingServices();
        }

        public void ClientReport()
        {
            var clients = clientServices.GetAllClients();

            var excelContent = exportServices.CreateClientListCSV(clients);

            //download
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ClientListExport.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(excelContent);

            Response.Flush();
            Response.End();
        }

        public void LoanReport(DateTime startDate, DateTime endDate)
        {
            var loansReportData = reportingServices.CreateLoanReportViewModel(startDate, endDate);

            string excelContent = exportServices.CreateLoanReportCSV(loansReportData);

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=LoanExport.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(excelContent);

            Response.Flush();
            Response.End();
        }
    }
}