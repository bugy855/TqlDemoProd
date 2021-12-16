using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//just a test
namespace TwilioJokeTeller.Models
{
    public abstract class Subscriber
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID {get; set;}
        [Required]
        public string PhoneNumber {get; set;} = null!;
        [Required]
        public string CountryCode {get; set;} = null!;
    }
}