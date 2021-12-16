namespace TwilioJokeTeller.Interfaces
{
 public interface IEnvValidationSvc
 {
    string GetAccountSid();
    string GetAuthToken();
    string GetNumber();
 }   
}