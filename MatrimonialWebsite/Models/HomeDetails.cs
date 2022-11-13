using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace MatrimonialWebsite.Models
{
    public class loginDetail
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }


    public class RegistrationDetails
    {
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; }


        [Required(ErrorMessage = "Age is required")]
        public string Age { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        public string mobile { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        public string cpassword { get; set; }
        public DataTable DT { get; set; }
        public string RegistrId { get; set; }
        public string id { get; set; }
        public string Name { get; set; }
        public string AppliedUserId { get; set; }
        public string MeetingDate { get; set; }
        public string MeetingTime { get; set; }

        public string AMorPM { get; set; }
        
    }
    public enum TimeAMorPM
    {
        AM, PM
    }

}