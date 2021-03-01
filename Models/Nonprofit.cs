using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SVPP.Validations;

namespace SVPP.Models
{
    public class Nonprofit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string OwnerId { get; set; }

        [Required(ErrorMessage = "Organization Name is required")]
        [Display(Name = "Organization Name")]
        public string Organization_Name { get; set; }

        [Required(ErrorMessage = "Primary Focus Area is required")]
        [Display(Name = "Focus Area #1")]
        public string Focus_Area1 { get; set; }
        [Display(Name = "Focus Area #2 (optional)")]
        public string Focus_Area2 { get; set; }
        [Display(Name = "Focus Area #3 (optional)")]
        public string Focus_Area3 { get; set; }

        [Required(ErrorMessage = "Date founded is required")]
        [Display(Name = "Date Founded")]
        [DataType(DataType.Date)]
        [CurrentDate(ErrorMessage = "Date must be after or equal to the current date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date_Founded { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Phone_Number { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Street Address")]
        public string Street_Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(2, MinimumLength=2, ErrorMessage = "An invalid state abbreviation has been entered. Should be 2 characters")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip Code is required")]
        [StringLength(5, MinimumLength=5, ErrorMessage = "An invalid zip code has been entered. Should be 5 digits")]
        [Display(Name = "Zip Code")]
        public string Zip_Code { get; set; }

        [Display(Name = "Website (optional)")]
        public string Website { get; set; }

        [Required(ErrorMessage = "Executive Name is required")]
        [Display(Name = "Executive Name")]
        public string Executive_Name { get; set; }

        [Required(ErrorMessage = "Contact Name is required")]
        [Display(Name = "Contact Name")]
        public string Contact_Name { get; set; }

        [Required(ErrorMessage = "Contact Title is required")]
        [Display(Name = "Contact Title")]
        public string Contact_Title { get; set; }

        [Required(ErrorMessage = "Contact Email is required")]
        [Display(Name = "Contact Email")]
        public string Contact_Email { get; set; }

        [Required(ErrorMessage = "Contact phone number is required")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Phone")]
        public string Contact_Phone { get; set; }

        [Display(Name = "Facebook URL (optional)")]
        public string Facebook_Id { get; set; }

        [Display(Name = "Twitter Handle (optional)")]
        public string Twitter_Id { get; set; }

        [Display(Name = "Instagram Handle (optional)")]
        public string Instagram_Id { get; set; }

        [Required(ErrorMessage = "A mission statement is required")]
        [Display(Name = "Mission Statement")]
        [DataType(DataType.MultilineText)]
        public string Mission { get; set; }

        [Required(ErrorMessage = "Programs provided is required")]
        [DataType(DataType.MultilineText)]
        public string Programs { get; set; }

        [Required(ErrorMessage = "Challenges organization is facing is required")]
        [DataType(DataType.MultilineText)]
        public string Challenges { get; set; }

        [Required(ErrorMessage = "Tax status is required")]
        [Display(Name = "Tax Status")]
        public string Tax_Status { get; set; }

        [Display(Name = "Tax ID (optional)")]
        public string Tax_Id { get; set; }

        [Required(ErrorMessage = "A desired area of mentor expertise is required")]
        [Display(Name = "Desired Skill #1")]
        public string Skill_1 { get; set; }

        [Display(Name = "Desired Skill #2 (Optional)")]
        public string Skill_2 { get; set; }

        [Display(Name = "Desired Skill #3 (Optional)")]
        public string Skill_3 { get; set; }

        [Required(ErrorMessage = "Activity Status is required")]
        [Display(Name = "Active")]
        public bool Active { get; set; }

    }
}