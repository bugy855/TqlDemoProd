using TwilioJokeTeller.Interfaces;

namespace TwilioJokeTeller.Services
{
    public class EnvValidationSvc : IEnvValidationSvc
    {
        private string accountSid;
        private string authToken;
        private string number;

        public EnvValidationSvc()
        {
            accountSid = VerifyEnv(Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID"));
            authToken = VerifyEnv(Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN"));
            number = VerifyEnv(Environment.GetEnvironmentVariable("TWILIO_NUMBER"));
        }

        protected string VerifyEnv(string? env){
            if(env != null){
                return env;
            }
            Console.WriteLine("Ensure all env vars are assigned");
            System.Environment.Exit(1);
            return "";
        }

        public string GetAccountSid()
        {
            return accountSid;
        }

        public string GetAuthToken()
        {
            return authToken;
        }

        public string GetNumber()
        {
            return number;
        }
    }
}