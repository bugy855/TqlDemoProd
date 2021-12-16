using System.ComponentModel.DataAnnotations;
namespace TwilioJokeTeller.Models
{
    public class UnverifiedSubscriber : Subscriber
    {
        [Required]
        public DateTime SubscribeDate {get; set;}
    }
}