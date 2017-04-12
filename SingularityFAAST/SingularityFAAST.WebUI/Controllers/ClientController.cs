using SingularityFAAST.Core.Entities;
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


        #region Index Get Method - With Paging Logic
        
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
        #endregion


        #region Index Post Method - For Search Function

        [HttpPost]
        public ActionResult Index(SearchRequest searchRequest) // Create client 'No Match'
        {
            IList<Client> model = _clientServices.HandlesSearchRequest(searchRequest);

            return View(model);
        }

        #endregion


        #region AddClient Get Method
        //  Returns Add New Client Page
        public ViewResult AddClient()
        {
            return View();
        }
        #endregion


        #region AddClient Post Method
        //  Collects form data from AddClient page and saves it
        [HttpPost]
        public RedirectToRouteResult AddClient(Client client)
        {
            _clientServices.SaveClient(client);



            return RedirectToAction("Index", "Client");
        }

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

        #endregion


        


        #region EditClient Get Method
        //  Returns the populated EditClient View,  F12 into Services for details
        [HttpGet]
        public ActionResult EditClient(int id)
        {
            var client = _clientServices.GetClientDetails(id);

            var disabilityList = _clientServices.GetAllDisabilities();

            IEnumerable<LoanMaster> associatedLoans = _clientServices.GetLoansByClientId(id);

            var viewModel = new EditClientViewModel(client, disabilityList, associatedLoans);

            return View(viewModel);
        }
        #endregion


        #region EditClient Post Method
        // Model Binder didn't know what to do with the DisabilityIds values until we gave it a parameter that matched the name attribute
        [HttpPost]
        public ActionResult EditClient(Client client, IEnumerable<int> DisabilityIds)   
        {
            _clientServices.EditClientDetails(client, DisabilityIds);

            var clientId = client.ClientID;

            return RedirectToAction("EditClient", new { clientId });


            //sidenote: was working with default return View() before, MVC Convention that it goes back to the original HttpGet which already had the id provided  
        }

        #endregion





        #region Delete Get Method
        //Sets IsDeleted Column Value to true
        [HttpGet]
        public ActionResult Delete(int id)
        {
            _clientServices.ChangeStatusDeleted(id);

            return RedirectToAction("Index", "Client");
        }

        #endregion





    }
}