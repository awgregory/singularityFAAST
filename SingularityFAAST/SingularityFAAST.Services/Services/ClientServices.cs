using SingularityFAAST.Core.Entities;
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

        //See left off notes

        public void SaveClient(Client client)
        {
            using (var context = new SingularityDBContext())
            {
                client.DateCreated = DateTime.Now;  // Manipulating the client object is done before saving to Db

                context.Clients.Add(client);

                context.SaveChanges();

                if (client.DisabilityIds.Any())   
                {
                    var clientDisabilities = client.DisabilityIds
                        .Select(disabilityId => new ClientDisability    //Where filters Select projects
                        { ClientId = client.ClientID, DisabilityCategoryId = disabilityId });

                    context.ClientDisabilities.AddRange(clientDisabilities);

                    context.SaveChanges();

                    

                }
                
            }

            
        }
    }
}
