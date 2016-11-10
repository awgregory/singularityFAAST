using SingularityFAAST.Services.Services;
using SingularityFAAST.WebUI.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SingularityFAAST.WebUI.Controllers
{
    public class ClientController : Controller
    {
        
        private static int id = 0;

        private static List<Client> clients = new List<Client>()
        {
            new Client {ClientID = 1, LastName= "Stevens", FirstName="Bob",
                Address= "123 Main St", StateAbbr= "FL", ZipCode=32210,
                Category ="Individual with a Disability"},

            new Client {ClientID = 2, LastName= "Brown", FirstName="Rocco",
                Address= "64 Side St", StateAbbr= "FL", ZipCode=32256,
                Category ="Family guardian or authorized rep"},

            new Client {ClientID = 3, LastName= "Teske", FirstName="David",
                Address= "PO Box 909", StateAbbr= "NC", ZipCode=28711,
                Category ="Family guardian or authorized rep"},

            new Client {ClientID = 4, LastName= "Stayer", FirstName="Bruce",
                Address= "134 Deanna Drive", StateAbbr= "FL", ZipCode=33852,
                Category ="Family guardian or authorized rep"},
        };

        //GET: Client - switch the Model for Index.cshtml when switching back to this

        //public ActionResult Index()
        //{
        //    return View(clients);
        //}



        public ActionResult Index()
        {
            var services = new ClientServices();

            var model = services.GetAllClients();

            return View(model);
        }


        public ViewResult SearchClient()
        {
            return View();
        }

        public ViewResult AddClient()
        {
            return View();
        }
    }
}