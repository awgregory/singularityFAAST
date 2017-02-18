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


        //  Returns Home Page/All Clients View
        [HttpGet]
        public ActionResult Index()
        {
            IList<Client> model = _clientServices.GetAllClients();

            return View(model);
        }


        //  Returns Client records that match search criteria
        [HttpPost]
        public ActionResult Index(SearchRequest searchRequest)
        {

            IList<Client> model = _clientServices.HandlesSearchRequest(searchRequest);

            return View(model);




            //if (string.IsNullOrWhiteSpace(searchRequest.SearchByName))
            //{

            //    IList<Client> model = _clientServices.GetAllClients();

            //    return View(model);
            //}

            //else
            //{

            //    IList<Client> model = _clientServices.GetClientsByName(searchRequest.SearchByName);

            //    return View(model);
            //}

        }



        //  Returns the Add New Client Page
        public ViewResult AddClient()
        {
            return View();
        }


        //  Collects the form data from AddClient view and saves
        [HttpPost]
        public RedirectToRouteResult AddClient(Client client)  
        {                                                       
            var services = _clientServices;

            services.SaveClient(client);

            return RedirectToAction("Index", "Client");
        }

        [HttpGet]
        public RedirectToRouteResult ViewClient(int id)
        {
            var services = _clientServices;


            services.SaveClient(id);

            return RedirectToAction("Index", "Client");
        }


        [HttpPost]
        public RedirectToRouteResult EditClient(int id)
        {
            var services = _clientServices;

            services.SaveClient(id);

            return RedirectToAction("Index", "Client");
        }

    }
}