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

            //List had more built in Extension methods then Ienumerable  list vs Ienum,  What were they?



            //if (string.IsNullOrWhiteSpace(searchRequest.SearchByName))
            //{

            //    IList<Client> model = _clientServices.GetAllClients();

            //    return View(model);
            //}



        }



        //  Returns Add New Client Page
        public ViewResult AddClient()
        {
            return View();
        }



        //  Collects the form data from AddClient page and saves
        [HttpPost]
        public RedirectToRouteResult AddClient(Client client)  
        {                                                       
            var services = _clientServices;

            services.SaveClient(client);

            return RedirectToAction("Index", "Client");
        }



        
        [HttpGet]
        public ActionResult EditClient(int id)
        {
            var client = _clientServices.GetClientDetails(id); 

            return View(client);
        }



        [HttpPost]
        public ActionResult EditClient(Client client) //take in form values from views inputs
        {
            _clientServices.EditClientDetails(client);

            return View("EditClient") //does not need its own view, return back to client you were editing
        }


    }
}