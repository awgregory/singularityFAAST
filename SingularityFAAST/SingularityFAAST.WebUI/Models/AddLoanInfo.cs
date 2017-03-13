using SingularityFAAST.Core.Entities;
using System.Collections.Generic;


namespace SingularityFAAST.WebUI.Models 
{
    public class AddLoanInfo
    {
        //Constructor
        public AddLoanInfo(IEnumerable<Client> clients, IEnumerable<InventoryItem> inventoryItems)
        {
            ClientInfo = clients;
            InventoryItem = inventoryItems;
        }

        //public AddLoanInfo(Client client, IEnumerable<InventoryItem> inventoryItems)  //two params
        //{
        //    this.Client = client;       
        //    InventoryItem = inventoryItems;
        //}


        //Client
        //public Client Client { get; set; }  //for the single client 
        public IEnumerable<Client> ClientInfo { get; set; }

        //Inventory List
        public IEnumerable<InventoryItem> InventoryItem { get; set; }   //for the list of available items
    }
}

