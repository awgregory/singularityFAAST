using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using SingularityFAAST.Core.Entities;
using SingularityFAAST.Core.ViewModels.Reports;

namespace SingularityFAAST.Services.Services
{
    public class ExportServices
    {
        public string CreateClientListCSV(IList<Client> clients)
        {
            var htmlContent = new StringBuilder();

            htmlContent.AppendFormat(
                "{0}, {1}, {2}, {3}",
                "First Namae",
                "Last Name",
                "Email",
                "Primary Phone");

            htmlContent.AppendLine();

            foreach (var client in clients)
            {
                htmlContent.AppendLine(
                    client.FirstName + ", " +
                    client.LastName + ", " +
                    client.Email + ", " +
                    client.CellPhone
                );
            }

            return htmlContent.ToString();
        }

        public string CreateLoanReportCSV(LoanReportViewModel loansReportData)
        {
            var htmlContent = new StringBuilder();

            #region Number of Device Loans By Type of borrower
            htmlContent.AppendLine(
                "Number of Device Loans by Type of Borrower"
            );

            htmlContent.AppendLine();

            htmlContent.AppendLine(
                "Type of Individual or Entity" + ", " +
                "Number of Borrowers"
            );
            

            htmlContent.AppendLine(
                "Individuals with disabilities" + ", " +
                loansReportData.NumberOfDeviceBorrowerWithDisability);

            htmlContent.AppendLine(
                "Family Members + guardians and authorized representatives" + ", " +
                loansReportData.NumberOfBorrowerFamily);

            htmlContent.AppendLine(
                "Representatives of Education" + ", " +
                loansReportData.NumberOfBorrowerEducation);

            htmlContent.AppendLine(
                "Representatives of Employment" + ", " +
                loansReportData.NumberOfBorrowerEmployment);
            htmlContent.AppendLine(
                "Representatives of Health + Allied Health + Rehabilitation" + ", " +
                loansReportData.NumberOfBorrowerHealth);
            htmlContent.AppendLine(
                "Representatives of Community Living" + ", " +
                loansReportData.NumberOfBorrowerCommunityLiving);
            htmlContent.AppendLine(
                "Representatives of Technology" + ", " +
                loansReportData.NumberOfBorrowerTechnology);
            #endregion


            #region Types of Devices Loaned
            htmlContent.AppendLine(
                "Types of Devices Loaned"
            );

            htmlContent.AppendLine();

            htmlContent.AppendLine(
                "Type of AT Device" + ", " +
                "Number"
            );

            htmlContent.AppendLine(
                "Computers and Related Tech" + ", " +
                loansReportData.categoryCounts[0].Count);
            htmlContent.AppendLine(
                "Daily Living" + ", " +
                loansReportData.categoryCounts[1].Count);
            htmlContent.AppendLine(
                "Environmental Adaptations" + ", " +
                loansReportData.categoryCounts[2].Count);
            htmlContent.AppendLine(
                "Hearing" + ", " +
                loansReportData.categoryCounts[3].Count);
            htmlContent.AppendLine(
                "Learning + Cognition and Development" + ", " +
                loansReportData.categoryCounts[4].Count);
            htmlContent.AppendLine(
                "Mobility + Seating and Position" + ", " +
                loansReportData.categoryCounts[5].Count);
            htmlContent.AppendLine(
                "Recreational + Sports and Leisure" + ", " +
                loansReportData.categoryCounts[6].Count);
            htmlContent.AppendLine(
                "Speech Communication" + ", " +
                loansReportData.categoryCounts[7].Count);
            htmlContent.AppendLine(
                "Vehicle Mods and Transport" + ", " +
                loansReportData.categoryCounts[8].Count);
            htmlContent.AppendLine(
                "Vision" + ", " +
                loansReportData.categoryCounts[9].Count);
            #endregion


            #region Device Loan by Types of Purpose
            htmlContent.AppendLine(
                "Devices Loans By Type of Purpose"
            );

            htmlContent.AppendLine();

            htmlContent.AppendLine(
                "Primary Purpose of Device Loan" + ", " +
                "Number of Loans"
            );

            htmlContent.AppendLine(
                "Assist in decision making (device trial or evaluation)" + ", " +
                loansReportData.PurposeDecisionMaking );
            htmlContent.AppendLine(
                "Serve as loaner during device repair while waiting for funding" + ", " +
                loansReportData.PurposeServeAsLoaner);
            htmlContent.AppendLine(
                "Provide and accomodate on a short-term basis for a time-limited event/situation" + ", " +
                loansReportData.PurposeShortTerm);
            htmlContent.AppendLine(
                "Conduct training + self-education or other professional development activity" + ", " +
                loansReportData.PurposeConductTraining);
            #endregion

            return htmlContent.ToString();
        }
    }
}
