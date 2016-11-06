using System.ComponentModel.DataAnnotations;

namespace SingularityFAAST.WebUI.Models
{
    public class Client
    {
        public int ClientID { get; set; }

        [Required(ErrorMessage = "Please enter a last name")] 
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a first name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter an address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter a 2 digit state abbreviation")]
        [Display(Name = "State Abbr")]
        public string StateAbbr { get; set; }

        [Required(ErrorMessage = "Please enter a zip code")]
        [Display(Name = "Zip Code")]
        public int ZipCode { get; set; }

        [Required(ErrorMessage = "Please enter a category")]
        public string Category { get; set; }
    }
}