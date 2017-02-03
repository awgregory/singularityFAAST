using SingularityFAAST.Core.Entities;
using SingularityFAAST.Core.SearchRequests;
using SingularityFAAST.Services.Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SingularityFAAST.WebUI.Controllers
{
    public class ClientController : Controller
    {

        private readonly ClientServices _clientServices = new ClientServices();
        


        [HttpGet]
        public ActionResult Index()
        {
            IList<Client> model = _clientServices.GetAllClients();

            return View(model);
        }



        [HttpPost]
        public ActionResult Index(SearchRequest searchRequest)
        {
            if (string.IsNullOrWhiteSpace(searchRequest.SearchByName))
            {

                IList<Client> model = _clientServices.GetAllClients();

                return View(model);
            }

            else
            {

                IList<Client> model = _clientServices.GetClientsByName(searchRequest.SearchByName);

                return View(model);
            }
            
        }




        public ViewResult AddClient()
        {
            return View();
        }


        [HttpPost]
        public RedirectToRouteResult AddClient(Client client)  // Make ViewModel
        {
            var services = new ClientServices();

            services.SaveClient(client);

            return RedirectToAction("Index", "Client");
        }

    }
}