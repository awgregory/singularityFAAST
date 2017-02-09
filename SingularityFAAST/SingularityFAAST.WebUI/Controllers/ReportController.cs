using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using SingularityFAAST.Core.Entities;
using SingularityFAAST.Services.Services;
using SingularityFAAST.Core.ViewModels.Reports;

namespace SingularityFAAST.WebUI.Controllers
{
    public class ReportController : Controller
    {
        private readonly ReportingServices _reportingServices;

        public ReportController()
        {
            _reportingServices = new ReportingServices();
        }

        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        #region Loans
        public ActionResult LoanReports()
        {
            var startDate = DateTime.UtcNow.AddYears(-1);
            var endDate = DateTime.UtcNow;

            LoanReportViewModel model = _reportingServices.CreateLoanReportViewModel(startDate, endDate);

            return View(model);
        }

        public ActionResult FilterLoansByDate(DateTime startDate, DateTime endDate)
        {
            // IList<Loan> loans = _reportingServices.GetDateFilteredLoans(startDate, endDate);

            //return View("LoansReport", loans);
            return null;
        }
        #endregion

        #region Inventory Reports

        #endregion

        #region Clients Reports
        //todo client reports
        #endregion
    }
}