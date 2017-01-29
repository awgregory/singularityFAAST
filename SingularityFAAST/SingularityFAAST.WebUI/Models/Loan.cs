using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SingularityFAAST.WebUI.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }

        [Required(ErrorMessage = "Please enter a device name")]
        [Display(Name = "Device Name")]
        public string DeviceName { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string LoanEndDate { get; set; }

        public string Notes { get; set; }

        public string Eligibility { get; set; }

        //public static List<Loan> manager = new List<Loan>
        //{
        //    new Loan { LoanId= 2345, DeviceName= "Green Weighted Vest", LastName="Hansen", FirstName="Terry", LoanEndDate = "10/14/2015", Notes = "", Eligibility = ""},
        //    new Loan { LoanId= 45, DeviceName= "Bubble Busy Box", LastName="Camacho", FirstName="Toni", LoanEndDate = "1/7/2016", Notes = "", Eligibility = ""},
        //    new Loan { LoanId= 5652, DeviceName= "Ipad Air 2", LastName="Clark", FirstName="Natalie", LoanEndDate = "10/14/2015", Notes = "", Eligibility = ""},
        //    new Loan { LoanId= 122, DeviceName= "4 alarm Pill Box", LastName="Gorbon", FirstName="Marianne", LoanEndDate = "11/7/2016", Notes = "", Eligibility = "N"},
        //    new Loan { LoanId= 358, DeviceName= "Musical Therapy Kit (10 Pieces", LastName="Ward", FirstName="Regina", LoanEndDate = "1/7/2016", Notes = "", Eligibility = ""},
        //    new Loan { LoanId= 814, DeviceName= "Medium Yellow Jelly Switch", LastName="Hornsby", FirstName="Pamela", LoanEndDate = "11/12/2015", Notes = "", Eligibility = ""},
        //    new Loan { LoanId= 2756, DeviceName= "Pound a Peg", LastName="Marshall", FirstName="Kanecha", LoanEndDate = "4/4/2016", Notes = "", Eligibility = ""},
        //    new Loan { LoanId= 2346, DeviceName= "Maltron Single Handed Keyboard", LastName="Cason", FirstName="Dean", LoanEndDate = "10/14/2015", Notes = "", Eligibility = ""},
        //    new Loan { LoanId= 564, DeviceName= "USB Head  Set", LastName="Smyth", FirstName="Bryan", LoanEndDate = "11/4/2016", Notes = "", Eligibility = "N"},
        //    new Loan { LoanId= 888, DeviceName= "Motorola Talkabout 2 way radio", LastName="Dubey", FirstName="Denise", LoanEndDate = "11/4/2015", Notes = "", Eligibility = ""},
        //};
    }
}