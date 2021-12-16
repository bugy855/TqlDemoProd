namespace TwilioJokeTeller.Interfaces
{
    public interface ITwilioSvc
    {
        void SendMessage(string _body, string phoneNumber, string countryCode);
        bool ProcessResponse(string incomingMessage);
    }
}