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


        //  Returns Index View
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
        }

        // NULL Case
        //if (string.IsNullOrWhiteSpace(searchRequest.SearchByName))
        //{

        //    IList<Client> model = _clientServices.GetAllClients();

        //    return View(model);
        //}




        //  Returns Add New Client Page
        public ViewResult AddClient()
        {
            return View();
        }



        //  Collects form data from AddClient page and saves it
        [HttpPost]
        public RedirectToRouteResult AddClient(Client client)  
        {                                                       
            var services = _clientServices; //why is the global _clientService readonly? why need this line?

            services.SaveClient(client);

            // Need a Saved Ack here

            return RedirectToAction("Index", "Client");
        }




        //[HttpGet]                                     Prior to ClientDisabilities Change
        //public ActionResult EditClient(int id)
        //{
        //    var client = _clientServices.GetClientDetails(id); 

        //    return View(client);
        //}




        //  Returns Edit Client Page
        [HttpGet]
        public ActionResult EditClient(int id)
        {
            var client = _clientServices.GetClientDetails(id); //gets this Client Object WITH the DisabilityIds Property

            var disabilityList = _clientServices.GetAllDisabilities(); //*gets list of ALL DisabilityCategory Objects

            var viewModel = new EditClientViewModel(client, disabilityList); //*viewmodel from created client and full list

            return View(viewModel);     //*Return viewModel
        }


        //  Collects form data from Edit Client page and saves it
        [HttpPost]
        public ActionResult EditClient(Client client, IEnumerable<int> DisabilityIds)   // Model Binder didn't know what to do with the DisabilityIds values until we gave it a parameter that matched the name attribute
        {
            _clientServices.EditClientDetails(client, DisabilityIds);  

            var clientId = client.ClientID;

            return RedirectToAction("EditClient", new { clientId });

            
            //sidenote: was working with default return View() before, MVC Convention that it goes back to the original HttpGet which already had the id provided  
        }                               
                              
        


        //[HttpPost]
        //public ActionResult Delete(int id)
        //{

        //}
                                       

    }
}