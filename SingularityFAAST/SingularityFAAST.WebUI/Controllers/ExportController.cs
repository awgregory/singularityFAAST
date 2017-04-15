using System.Web.Mvc;
using SingularityFAAST.Services.Services;

namespace SingularityFAAST.WebUI.Controllers
{
    public class ExportController : Controller
    {
        private readonly ClientServices clientServices;
        private readonly ExportServices exportServices;

        public ExportController()
        {
            clientServices = new ClientServices();
            exportServices = new ExportServices();
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
    }
}