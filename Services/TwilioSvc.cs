using TwilioJokeTeller.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace TwilioJokeTeller.Services
{
    class TwilioSvc : ITwilioSvc
    {
        private IEnvValidationSvc _envValidationSvc;
        public TwilioSvc(IEnvValidationSvc envValidationSvc)
        {
            _envValidationSvc = envValidationSvc;
            TwilioClient.Init(_envValidationSvc.GetAccountSid(), _envValidationSvc.GetAuthToken());
        }

        public async void SendMessage(string _body, string phoneNumber, string countryCode)
        {
            var message = await MessageResource.CreateAsync(
                body: _body,
                from: new Twilio.Types.PhoneNumber(_envValidationSvc.GetNumber()),
                to: new Twilio.Types.PhoneNumber("+" + countryCode + phoneNumber)
            );
        }

        public bool ProcessResponse(string incomingMessage)
        {
            if(incomingMessage.IndexOf("OPTOUT") != -1)
            {
                return true;
            }
            
            return false;
        }
    }
}