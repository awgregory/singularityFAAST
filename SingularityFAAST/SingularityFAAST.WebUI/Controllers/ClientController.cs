using SingularityFAAST.Services.Services;
using SingularityFAAST.Core.Entities;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace SingularityFAAST.WebUI.Controllers
{
    public class ClientController : Controller
    {
        
        //private static int id = 0;

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





        public ViewResult AddClient()
        {

            return View();
        }

        [HttpPost]
        public ViewResult AddClient(Client client)
        {
            var services = new ClientServices();

            services.SaveClient(client);


            return View();
        }

    }
}