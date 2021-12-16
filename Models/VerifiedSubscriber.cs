using System.ComponentModel.DataAnnotations;
namespace TwilioJokeTeller.Models
{
    public class VerifiedSubscriber : Subscriber
    {
        [Required]
        public DateTime VerificationDate {get; set;}
    }
}