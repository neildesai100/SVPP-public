using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SVPP.Models
{
    public class Partner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string OwnerId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string First_Name { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
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
        [Display(Name = "Zip Code")]
        [StringLength(5, MinimumLength=5, ErrorMessage = "An invalid zip code has been entered. Should be 5 digits")]
        public string Zip_Code { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Display(Name = "Twitter Handle (optional)")]
        public string Twitter_Id { get; set; }

        [Required(ErrorMessage = "LinkedIn handle is required")]
        [Display(Name = "LinkedIn Handle")]
        public string Linkedin_Id { get; set; }

        [Required(ErrorMessage = "Employer is required")]
        public string Employer { get; set; }

        [Required(ErrorMessage = "Current job title is required")]
        [Display(Name = "Job Title")]
        public string Job_Title { get; set; }

        [Required(ErrorMessage = "A short bio is required")]
        [DataType(DataType.MultilineText)]
        public string Bio { get; set; }

        [Required(ErrorMessage = "An area of expertise is required")]
        [Display(Name = "Skill #1")]
        public string Skill_1 { get; set; }

        [Display(Name = "Skill #2 (optional)")]
        public string Skill_2 { get; set; }

        [Display(Name = "Skill #3 (optional)")]
        public string Skill_3 { get; set; }

        [Required(ErrorMessage = "A Preferred Focus Area is required")]
        [Display(Name = "Preferred Nonprofit Focus Area #1")]
        public string Preference_1 { get; set; }

        [Display(Name = "Preferred Nonprofit Focus Area #2 (optional)")]
        public string Preference_2 { get; set; }

        [Display(Name = "Preferred Nonprofit Focus Area #3 (optional)")]
        public string Preference_3 { get; set; }

        [Required(ErrorMessage = "Activity Status is required")]
        [Display(Name = "Active")]
        public bool Active { get; set; }
    }

}