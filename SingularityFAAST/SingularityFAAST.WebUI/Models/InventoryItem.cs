using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingularityFAAST.WebUI.Models
{
    /**InventoryItem contains item number, name, serial number, retail cost, category, sub category (optional), manufacturer, model, purchase date,
        cost paid, location, description, accessories, additional files, and availability for and device at Hope Haven's FAAST Demonstration center**/

    public class InventoryItem
    {
        public string Number { get; set; }  //should auto generate new numbers for new items
        public string Name { get; set; }
        public double SerialNumber { get; set; }
        public decimal RetailCost { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal CostPaid { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Accessories { get; set; }

        //Attribute(s) for additional files or images.

        public bool Availability { get; set; }  //is this device checked out or not
    }
}