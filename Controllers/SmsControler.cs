using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using TwilioJokeTeller.Interfaces;
using TwilioJokeTeller.Models;

namespace TwilioJokeTeller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SmsController : TwilioController
    {
        private readonly ITwilioSvc _twilioSvc;
        private readonly JokeTellerContext _context;
        public SmsController(ITwilioSvc twilioSvc, JokeTellerContext context)
        {
            _context = context;
            _twilioSvc = twilioSvc;
        }

        //Post: api/Sms/verified-subscriber/optout
        [HttpPost("verified-subscriber/optout")]
        public async Task<IActionResult> DeleteVerifiedSubscriber()
        {
            var response = new MessagingResponse();
            string requestBody = Request.Form["Body"];
            string requestOrigin = Request.Form["From"];
            
            var subscriber = _context.VerifiedSubscribers.Where(s => ("+" + s.CountryCode + s.PhoneNumber) == requestOrigin).FirstOrDefault();
            
            if (subscriber == null)
            {
                response.Message("Please register at https://tqldemomessager.azurewebsites.net/");
                return TwiML(response);
            }
            
            if(requestBody.ToLower().Contains("optout"))
            {
                _context.Remove(subscriber);
                await _context.SaveChangesAsync();
                response.Message("You have successfuly opted out!");
                return TwiML(response);
            }
            
            response.Message("unknown command, please check spelling");
            return TwiML(response);
        }
    }
}