using SingularityFAAST.Core.Entities;
using SingularityFAAST.DataAccess.Contexts;
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

        public void SaveClient(Client client)
        {
            using (var context = new SingularityDBContext())
            {
                context.Clients.Add(client);

                var rowsAffected = context.SaveChanges();
            }

            
        }
    }
}
