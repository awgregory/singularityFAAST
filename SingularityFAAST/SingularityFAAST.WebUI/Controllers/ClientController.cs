using SingularityFAAST.Services.Services;
using SingularityFAAST.Core.Entities;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System;

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
            if (string.IsNullOrWhiteSpace(searchRequest.SearchBy))
            {

                IList<Client> model = _clientServices.GetAllClients();

                return View(model);
            }

            else
            {

                IList<Client> model = _clientServices.GetClientsByName(searchRequest.SearchBy);

                return View(model);
            }
            
        }


        public class SearchRequest
        {
            public string SearchBy { get; set; }
            public DateTime SearchDate { get; set; }
            public string Honk { get; set; }
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