using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Utility.IdentityServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //[Required]
        public virtual string CardNumber { get; set; }
        //[Required]
        public virtual string SecurityNumber { get; set; }
       // [Required]
        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "Expiration should match a valid MM/YY value")]
        public virtual string Expiration { get; set; }
        //[Required]
        public string CardHolderName { get; set; }
        public virtual int CardType { get; set; }
       // [Required]
        public virtual string Street { get; set; }
       // [Required]
        public virtual string City { get; set; }
       // [Required]
        public virtual string State { get; set; }
       // [Required]
        public virtual string Country { get; set; }
        //[Required]
        public virtual string ZipCode { get; set; }
      //  [Required]
        public virtual string Name { get; set; }
       // [Required]
        public virtual string LastName { get; set; }
        [NotMapped]
        public string RoleId { get; set; }
    }
}
