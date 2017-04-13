using SingularityFAAST.Core.Entities;
using SingularityFAAST.Core.SearchRequests;
using SingularityFAAST.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SingularityFAAST.Services.Services
{
    public class ClientServices
    {
        #region GetAllClients
        public IList<Client> GetAllClients()
        {
            using (var context = new SingularityDBContext())
            {
                var clients = context.Clients;

                var clientList = clients.ToList();

                return clientList;
            }
        }
        #endregion


        #region GetAllLoanMasters
        public IList<LoanMaster> GetAllLoanMasters()
        {
            using (var context = new SingularityDBContext())
            {
                var loans = context.LoanMasters;

                var loanList = loans.ToList();

                return loanList;
            }
        }
        #endregion


        #region Search Function Services
        
        public IList<Client> HandlesSearchRequest(SearchRequest searchRequest)
        {
            IList<Client> filteredClients;

            switch (searchRequest.SearchByType)
            {
                case 1:
                    filteredClients = GetClientByLastName(searchRequest.SearchBy);
                    break;

                case 2:
                    filteredClients = GetClientByFirstName(searchRequest.SearchBy);
                    break;

                case 3:
                    filteredClients = GetClientById(searchRequest.SearchBy);
                    break;

                case 4:
                    filteredClients = GetClientByEmail(searchRequest.SearchBy);
                    break;

                default:
                    filteredClients = new List<Client>(0);
                    break;

            }
            return filteredClients;
        }



        public List<Client> GetClientByLastName(string searchBy)   
        {
            IList<Client> allClients = GetAllClients();


            List<Client> filteredClients = allClients.Where(c =>
                c.LastName.ToLower().Contains(searchBy.ToLower())).ToList();


            //filteredClients.AddRange(GetClientByFirstName(searchBy));

            //filteredClients.AddRange(GetClientByEmail(searchBy));

            return filteredClients;
        }


        #region Search code options
        //Can't use StringComparison.OrdinalIgnoreCase in Contains()

        //SO#444798
        //IList<Client> filteredClients = allClients.Where(c => c.LastName.IndexOf(searchBy, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

        //Original
        //IList<Client> filteredClients = allClients.Where(client =>
        //    string.Equals(client.LastName, searchBy, StringComparison.OrdinalIgnoreCase)).ToList();
        #endregion


        private IList<Client> GetClientByFirstName(string searchBy)
        {
            IList<Client> allClients = GetAllClients();

            IList<Client> filteredClients = allClients.Where(c =>
                c.FirstName.ToLower().Contains(searchBy.ToLower())).ToList();

            return filteredClients;
        }





        //Default Search Method
        public IList<Client> GetClientById(string searchBy)
        {
            IList<Client> allClients = GetAllClients();

            List<Client> filteredClients;

            int x = 0;

            if (Int32.TryParse(searchBy, out x))
            {
                filteredClients = allClients.Where(c =>
                c.ClientID.Equals(x)).ToList();
            }

            else
            {
                filteredClients = GetClientByLastName(searchBy);

                filteredClients.AddRange(GetClientByFirstName(searchBy));

                filteredClients.AddRange(GetClientByEmail(searchBy));

                //filteredClients = allClients.Take(0).ToList();
            }

            //This Threw format exception if string entered
            //filteredClients = allClients.Where(client =>
            //    client.ClientID == (Convert.ToInt32(searchBy))).ToList(); 


            return filteredClients;
        }


        private IList<Client> GetClientByEmail(string searchBy)
        {
            IList<Client> allClients = GetAllClients();
            IList<Client> filteredClients;

            int x = 0;

            if (Int32.TryParse(searchBy, out x))
            {
                filteredClients = allClients.Take(0).ToList();
            }

            else
            {
                filteredClients = allClients.Where(c =>
                    //c.Email.ToLower().Contains(searchBy.ToLower())).ToList();   Returns NullException

                    string.Equals(c.Email, searchBy, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return filteredClients;

        }

        #endregion


        #region SaveClient
        public void SaveClient(Client client)
        {
            using (var context = new SingularityDBContext())
            {
                client.DateCreated = DateTime.Now;  // Manipulating the client object is done before saving to Db

                context.Clients.Add(client);

                context.SaveChanges();

                // No Any() needed null check covers it

                if (client.DisabilityIds != null)

                // what was the thing that checked for null before Any()? default type for Enum?

                {
                    var clientDisabilities = client.DisabilityIds
                        .Select(disabilityId => new ClientDisability
                        { ClientId = client.ClientID, DisabilityCategoryId = disabilityId });

                    context.ClientDisabilities.AddRange(clientDisabilities);

                    context.SaveChanges();
                }
            }
        }

        #endregion




        #region GetClientDetails - For the EditClient Get Method
        //Gets the Client Object and populates it's DisabilityIds Property using a Linq query
        public Client GetClientDetails(int id) 
        {
            using (var context = new SingularityDBContext())
            {
                var client = context.Clients.FirstOrDefault(x => x.ClientID == id); //default 0 int, The default value for reference and nullable types is null.


                client.DisabilityIds = context.ClientDisabilities  //Access the ClientDisabilities Associate table entries
                    .Where(cd => cd.ClientId == client.ClientID) //filter down to entries that match this ClientID                
                    .Select(i => i.DisabilityCategoryId) //give me back the DisabilityCategoryId property for these entries
                    .ToList();  // They become the IEnumerable<int> DisabilityIds property values for this Client instance 


                return client;
            }
        }
        #endregion


        #region GetAllDisabilities - For the EditClient Get Method
        //Gets list of all the DisabilityCategory Objects (Gets all the Categories)
        public IList<DisabilityCategory> GetAllDisabilities()
        {
            using (var context = new SingularityDBContext())
            {
                return context.DisabilityCategories.ToList();
            }
        }
        #endregion


        #region GetLoansByClientId - For the EditClient Get Method
        // Gets List of all Loans Linked to CliendId for Display within Client Edit View
        public IEnumerable<LoanMaster> GetLoansByClientId(int id)
        {
            IList<LoanMaster> filteredLoans = GetAllLoanMasters().Where(loan => loan.ClientId == id).ToList();

            return filteredLoans;
        }
        #endregion


        #region EditClientDetails - For the EditClient Post Method
        //Update existing client info
        public void EditClientDetails(Client client, IEnumerable<int> DisabilityIds)
        {
            using (var context = new SingularityDBContext())
            {


                context.Clients.Attach(client);

                var entry = context.Entry(client);

                entry.State = EntityState.Modified;

                context.SaveChanges();



                if (DisabilityIds != null)  // null check required to cover posting on null DisabilityIds 

                {

                    var oldClientDisabilities = context.ClientDisabilities.Where(cd => cd.ClientId == client.ClientID);

                    context.ClientDisabilities.RemoveRange(oldClientDisabilities);

                    context.SaveChanges();


                    var clientDisabilities = DisabilityIds //Access the IEnumerable<int> of DisabilityIds
                        .Select(DisabilityId => new ClientDisability //Foreach create a new cd object
                        { ClientId = client.ClientID, DisabilityCategoryId = DisabilityId });

                    // After the ClientDisability Ojects are created and added to var, they get added to the table

                    context.ClientDisabilities.AddRange(clientDisabilities);

                    context.SaveChanges();
                }

                else
                {
                    var oldClientDisabilities = context.ClientDisabilities.Where(cd => cd.ClientId == client.ClientID);

                    context.ClientDisabilities.RemoveRange(oldClientDisabilities);

                    context.SaveChanges();
                }

            }
        }
        #endregion



        #region ChangeStatusDeleted
        public void ChangeStatusDeleted(int id)
        {
            using (var context = new SingularityDBContext())
            {
                var client = context.Clients.FirstOrDefault(x => x.ClientID == id);

                client.IsDeleted = true;

                context.SaveChanges();
            }
        }

        #endregion



        #region ReturnNextClientNumber
        //Used for AddClient Get Method to pre-populate the Client Number
  
        public int ReturnNextClientNumber()
        {
            using (var context = new SingularityDBContext())
            {
                //var items = context.Clients;

                int ClientID;

                IList<Client> allClients = GetAllClients();

                var itemCount = allClients.Count;
                //need to add a segment that actually checks the id # of the last item
                //  --> This proposed fix should fix any un-aligned numbers

              
                if (allClients.Count > 0)
                {
                    ClientID = itemCount + 1;                   
                }
                else
                {
                    ClientID = 1;
                }

                return ClientID;
            }
        }
        #endregion


    }
}
