using System;
using System.Collections.Generic;
using System.Text;
using SingularityFAAST.Core.Entities;

namespace SingularityFAAST.Services.Services
{
    public class ExportServices
    {
        public string CreateClientListCSV(IList<Client> clients)
        {
            var htmlContent = new StringBuilder();

            htmlContent.AppendFormat(
                "{0}, {1}, {2}",
                "First Namae",
                "Last Name",
                "Email");

            htmlContent.AppendLine();

            foreach (var client in clients)
            {
                htmlContent.AppendLine(
                    client.FirstName + ", " +
                    client.LastName + ", " +
                    client.Email
                );
            }

            return htmlContent.ToString();
        }
    }
}
