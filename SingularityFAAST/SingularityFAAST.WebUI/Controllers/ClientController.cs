using SingularityFAAST.Core.Entities;
using SingularityFAAST.Core.SearchRequests;
using SingularityFAAST.Core.ViewModels;
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


        //  Search Function
        [HttpPost]
        public ActionResult Index(SearchRequest searchRequest)
        {

            IList<Client> model = _clientServices.HandlesSearchRequest(searchRequest);

            return View(model);

            //List had more built in Extension methods then Ienumerable  list vs Ienum,  What were they?



            // NULL Case
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



        //  Collects the form data from AddClient page and saves it
        [HttpPost]
        public RedirectToRouteResult AddClient(Client client)  
        {                                                       
            var services = _clientServices;

            services.SaveClient(client);

            return RedirectToAction("Index", "Client");
        }




        //[HttpGet]                                     Prior to ClientDisabilities Change
        //public ActionResult EditClient(int id)
        //{
        //    var client = _clientServices.GetClientDetails(id); 

        //    return View(client);
        //}



        // left off
        [HttpGet]
        public ActionResult EditClient(int id)
        {
            var client = _clientServices.GetClientDetails(id);

            var disabilityList = _clientServices.GetAllDisabilities(); //*need list of all disabilities

            var viewModel = new EditClientViewModel(client, disabilityList); //*create viewmodel from created client and new list

            return View(viewModel);     //*Return viewModel
        }



        [HttpPost]
        public ActionResult EditClient(Client client) 
        {
            _clientServices.EditClientDetails(client);


            return View(); //MVC Convention that it goes back to the original HttpGet which already had the id provided  
        }                               
                                       

                                       

    }
}