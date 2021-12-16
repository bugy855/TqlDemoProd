using Microsoft.AspNetCore.Mvc;
using TwilioJokeTeller.Models;
using TwilioJokeTeller.Interfaces;
using TwilioJokeTeller.Services;

namespace TwilioJokeTeller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriberController : ControllerBase
    {
        private readonly JokeTellerContext _context;
        private readonly ILogger<JokeSenderBackgroundService> _logger;
        private readonly ITwilioSvc _twilioSvc;
        private readonly IEnvValidationSvc _envValidationSvc;

        public SubscriberController(ILogger<JokeSenderBackgroundService> logger, JokeTellerContext context, ITwilioSvc twilioSvc, IEnvValidationSvc envValidationSvc)
        {
            _logger = logger;
            _context = context;
            _twilioSvc = twilioSvc;
            _envValidationSvc = envValidationSvc;
        }

        //Post: api/Subscriber/unverified-subscriber
        [HttpPost("unverified-subscriber")]
        public async Task<IActionResult> CreateUnverifiedSubscriber([FromForm] UnverifiedSubscriber unverifiedSubscriber)
        {
            UnverifiedSubscriber _unverifiedSubscriber = new UnverifiedSubscriber
            {
                PhoneNumber = unverifiedSubscriber.PhoneNumber,
                CountryCode = unverifiedSubscriber.CountryCode,
                SubscribeDate = unverifiedSubscriber.SubscribeDate
            };

            var subscriber = _context.Subscribers.Where(u=>u.PhoneNumber == _unverifiedSubscriber.PhoneNumber).FirstOrDefault();
            
            if (subscriber != null)
            {
                return NotFound(new {message = "Your phone number is already registered", error = true});
            }

            _context.Add(_unverifiedSubscriber);
            await _context.SaveChangesAsync();

            string message = "Go to this link to verify your subscription\n"
             + "https://tqldemomessager.azurewebsites.net/api/Subscriber/verify-subscriber/" 
             + _unverifiedSubscriber.ID.ToString();
            
            _twilioSvc.SendMessage(message,_unverifiedSubscriber.PhoneNumber,_unverifiedSubscriber.CountryCode);

            return Ok(new {message = "Verification link sent!", error = false});
        }

        //Get: api/Subscriber/unverified-subscriber/id
        [HttpPost("verify-subscriber/{id}")]
        public async Task<IActionResult> CreateVerifiedSubscriber(string id)
        {
            Guid searchID = new Guid(id);
            var unverifiedSubscriber = await _context.UnverifiedSubscribers.FindAsync(searchID);

            if (unverifiedSubscriber == null){
                return NotFound(new {message = "Your phone number was not found"});
            }

            VerifiedSubscriber verifiedSubscriber = new VerifiedSubscriber
            {
                PhoneNumber = unverifiedSubscriber.PhoneNumber,
                CountryCode = unverifiedSubscriber.CountryCode,
                VerificationDate = DateTime.Now
            };

            try
            {
                _context.VerifiedSubscribers.Add(verifiedSubscriber);
                await _context.SaveChangesAsync();

                _context.UnverifiedSubscribers.Remove(unverifiedSubscriber);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest(new {message = "Unable to add phone number"});
            }

            string message = "You have been verified!\n\nRespond to this message with OPTOUT to opt out of this service";
            _twilioSvc.SendMessage(message,verifiedSubscriber.PhoneNumber, verifiedSubscriber.CountryCode);

            return Ok(new {message = "Phone number verified!"});
        }
    }
}
