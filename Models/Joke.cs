using System.ComponentModel.DataAnnotations;

namespace TwilioJokeTeller.Models
{
    public class Joke
    {
        public int ID {get; set;}
        [Required]
        public string Question {get; set;} = null!;
        [Required]
        public string Answer {get; set;} = null!;
    }
}