using SingularityFAAST.Core.Entities;
using System.Collections.Generic;
using SingularityFAAST.Core.DataTransferObjects;


namespace SingularityFAAST.WebUI.Models //Core.ViewModels where Adrian's is
{
    public class AddLoanInfo
    {
        //Constructor
        public AddLoanInfo(Client client, IEnumerable<InventoryItem> inventoryItems)  //two params
        {
            this.Client = client;       
            InventoryItem = inventoryItems;
        }

        //Client
        public Client Client { get; set; }  //for the single client 

        //Inventory List
        public IEnumerable<InventoryItem> InventoryItem { get; set; }   //for the list of available items
    }
}

