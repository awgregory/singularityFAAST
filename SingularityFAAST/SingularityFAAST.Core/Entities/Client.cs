using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SingularityFAAST.Core.Entities
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; }

        public IEnumerable<int> DisabilityIds { get; set; }

        public bool Active { get; set; }

        public DateTime DateCreated { get; set; }

        public string FirstName { get; set; }

        public string MiddleInitial { get; set; }

        public string LastName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string StateName { get; set; }

        public string StateCode { get; set; }

        public string Zip { get; set; }

        public string County { get; set; }

        public string CountyFIPS { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string HomePhone { get; set; }

        public string CellPhone { get; set; }

        public string WorkPhone { get; set; }

        public string Company { get; set; }

        public string Title { get; set; }

        public bool LoanEligibility { get; set; }

        public string Notes { get; set; }

        public int ClientCategoryId { get; set; }


        //Data Annotations Display would still require logic, DisplayFor is more static for single values
        public string ClientCategoryName   
        {
            get
            {
                string clientCategory;

                switch (ClientCategoryId)
                {
                    case 1:
                        clientCategory = "Individual with Disability";
                        break;
                    case 2:
                        clientCategory = "Family Member";
                        break;
                    case 3:
                        clientCategory = "Professional / Representative of Community Living";
                        break;
                    case 4:
                        clientCategory = "Professional / Representative of Education";
                        break;
                    case 5:
                        clientCategory = "Professional / Representative of Technology";
                        break;
                    case 6:
                        clientCategory = "Professional / Representative of Employment";
                        break;
                    case 7:
                        clientCategory = "Professional / Representative of Health, Allied Health, Rehabilitative Services";
                        break;
                    default:
                        clientCategory = "Individual with Disability";
                        break;


                }

                return clientCategory;


            }
        }




    }
}






