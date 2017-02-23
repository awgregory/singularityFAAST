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
        
        public IList<Client> GetAllClients()
        {
            using (var context = new SingularityDBContext())
            {                
                var clients = context.Clients;
                
                var clientList = clients.ToList();
                
                return clientList;
            }
        }

 


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

        

        public IList<Client> GetClientByLastName(string searchBy)
        {
            IList<Client> allClients = GetAllClients();

            IList<Client> filteredClients = allClients.Where(client =>
                string.Equals(client.LastName, searchBy, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredClients;
        }

        private IList<Client> GetClientByFirstName(string searchBy)
        {
            IList<Client> allClients = GetAllClients();

            IList<Client> filteredClients = allClients.Where(client =>
                string.Equals(client.FirstName, searchBy, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredClients;
        }

        

        public IList<Client> GetClientById(string searchBy)
        {
            IList<Client> allClients = GetAllClients();

            IList<Client> filteredClients = allClients.Where(client =>
                client.ClientID == (Convert.ToInt32(searchBy))).ToList();

            return filteredClients;
        }


        private IList<Client> GetClientByEmail(string searchBy)
        {
            IList<Client> allClients = GetAllClients();

            IList<Client> filteredClients = allClients.Where(client =>
                string.Equals(client.Email, searchBy, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredClients;
        }



        

        public void SaveClient(Client client)
        {
            using (var context = new SingularityDBContext())
            {
                client.DateCreated = DateTime.Now;  // Manipulating the client object is done before saving to Db

                context.Clients.Add(client);

                context.SaveChanges();

                if (client.DisabilityIds != null && client.DisabilityIds.Any())  // Review the If statement
                {
                    var clientDisabilities = client.DisabilityIds
                        .Select(disabilityId => new ClientDisability    
                        { ClientId = client.ClientID, DisabilityCategoryId = disabilityId });  

                    context.ClientDisabilities.AddRange(clientDisabilities);

                    context.SaveChanges();
                }              
            }      
        }




        //public Client GetClientDetails(int id)
        //{
        //    using (var context = new SingularityDBContext())
        //    {
        //        var client = context.Clients.FirstOrDefault(x => x.ClientID == id); 

        //        return client;
        //    }
        //}



        // left off
        public Client GetClientDetails(int id)
        {
            using (var context = new SingularityDBContext())
            {
                var client = context.Clients.FirstOrDefault(x => x.ClientID == id); //default 0

                //retrieve disability IDs associated with client
                client.DisabilityIds = context.ClientDisabilities

                    //give me associate table entries where this client ID
                    .Where(cd => cd.ClientId == client.ClientID)

                    //give me back the IDs of the DisabilityCategories
                    .Select(i => i.DisabilityCategoryId)
                    .ToList();

                return client;
            }
        }


        //new helper method in services
        public IList<DisabilityCategory> GetAllDisabilities()
        {
            using (var context = new SingularityDBContext())
            {
                return context.DisabilityCategories.ToList();
            }
        }


        //Update existing client info
        public void EditClientDetails(Client client)
        {
            using (var context = new SingularityDBContext())
            {
                context.Clients.Attach(client);

                var entry = context.Entry(client);  

                entry.State = EntityState.Modified;

                context.SaveChanges();

            }
        }



    }
}
