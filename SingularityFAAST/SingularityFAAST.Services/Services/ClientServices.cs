using SingularityFAAST.Core.Entities;
using SingularityFAAST.DataAccess.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace SingularityFAAST.Services.Services
{
    public class ClientServices
    {
        //often - will create service layer specific model for what we are returning
        //we can safely use Client because it reisdes in Core
        public IList<Client> GetAllClients()
        {
            using (var context = new SingularityDBContext())
            {
                //var clients = context.Clients.Where(client => client.Age > 50);   Example

                //grab clients in Clients table - NOT A LIST YET
                var clients = context.Clients;

                //instantiate into list of concrete client objects (c# objs)
                var clientList = clients.ToList();

                //return them
                return clientList;
            }
        }

        public void SaveClient(Client client)
        {
            using (var context = new SingularityDBContext())
            {
                context.Clients.Add(client);   //that doesn't save just yet

                var rowsAffected = context.SaveChanges();
            }

            
        }
    }
}
