﻿using SingularityFAAST.Core.Entities;
using SingularityFAAST.Core.SearchRequests;
using SingularityFAAST.Core.ViewModels;
using SingularityFAAST.Services.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SingularityFAAST.Core.DataTransferObjects;

namespace SingularityFAAST.WebUI.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly ClientServices _clientServices = new ClientServices();
        private readonly int _pageSize = 5;


        //  Returns Index View
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            IList<Client> allClients = _clientServices.GetAllClients();

            var list = allClients
                .OrderBy(item => item.ClientID)
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize);

            var viewModel = new ClientIndexViewModel
            {
                Clients = list,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = allClients.Count
                }
            };

            return View(viewModel);
        }


        //  Search Function
        [HttpPost]
        public ActionResult Index(SearchRequest searchRequest) // Create client 'No Match'
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
            _clientServices.SaveClient(client);

            #region ModelStateValidation
            //if (ModelState.IsValid)
            //{
            //    _clientServices.SaveClient(client);

            //    //Need a Saved Ack here
            //}

            //else
            //{
            //    ModelState.AddModelError("", "Please Enter All Required Information");

            //    return RedirectToAction("AddClient", "Client");
            //}
            #endregion


            return RedirectToAction("Index", "Client");
        }





        //  Returns Edit Client Page
        [HttpGet]
        public ActionResult EditClient(int id)
        {
            var client = _clientServices.GetClientDetails(id); //gets the Client Object and populates it's DisabilityIds Property using a Linq query

            var disabilityList = _clientServices.GetAllDisabilities(); //gets list of all DisabilityCategory Objects

            IEnumerable<LoanMaster> associatedLoans = _clientServices.GetLoansByClientId(id); //GETS LOANS

            var viewModel = new EditClientViewModel(client, disabilityList, associatedLoans);

            return View(viewModel);  //Passes the DisabilityIds to view from within the Client Object within the viewModel
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




        [HttpGet]
        public ActionResult Delete(int id)
        {
            _clientServices.ChangeStatusDeleted(id);

            return RedirectToAction("Index", "Client");
        }


    }
}