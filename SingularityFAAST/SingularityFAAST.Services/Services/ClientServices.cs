using SingularityFAAST.Core.Entities;
using SingularityFAAST.Core.SearchRequests;
using SingularityFAAST.DataAccess.Contexts;
using System;
using System.Collections.Generic;
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

        public IList<Client> GetClientsByName(string searchby)
        {
            IList<Client> allClients = GetAllClients();

            IList<Client> filteredClients = allClients.Where(client => 
                string.Equals(client.LastName, searchby, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredClients;
        }


        public IList<Client> HandlesSearchRequest(SearchRequest searchRequest)
        {
            IList<Client> filteredClients;

            switch (searchRequest.SearchByType)
            {
                case 1: filteredClients = GetClientsByName(searchRequest.SearchBy);
                    break;

                case 2:
                    filteredClients = GetClientsByName(searchRequest.SearchBy);
                    break;

                default:
                    filteredClients = new List<Client>(0);
                    break;

            }

            return filteredClients;
        }




        //left off 

        public void SaveClient(Client client)
        {
            using (var context = new SingularityDBContext())
            {
                client.DateCreated = DateTime.Now;  // Manipulating the client object is done before saving to Db

                context.Clients.Add(client);

                context.SaveChanges();

                if (client.DisabilityIds != null && client.DisabilityIds.Any())     // saves client before getting to this error, needs another If null test?
                {
                    var clientDisabilities = client.DisabilityIds
                        .Select(disabilityId => new ClientDisability    
                        { ClientId = client.ClientID, DisabilityCategoryId = disabilityId });  

                    context.ClientDisabilities.AddRange(clientDisabilities);

                    context.SaveChanges();

                    

                }
                
            }
            
        }

        

    }
}
