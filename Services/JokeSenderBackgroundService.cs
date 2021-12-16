using TwilioJokeTeller.Models;
using TwilioJokeTeller.Interfaces;

namespace TwilioJokeTeller.Services
{
    public class JokeSenderBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITwilioSvc _twilioSvc;
        private static readonly Random rand = new Random();

        public JokeSenderBackgroundService(IServiceProvider serviceProvider, ITwilioSvc twilioSvc)
        {
            _serviceProvider = serviceProvider;
            _twilioSvc = twilioSvc;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<JokeTellerContext>();
                var recipients = context.VerifiedSubscribers.ToList();

                foreach (var subscriber in recipients)
                {
                    int jokeID = rand.Next(1,7);
                    var joke = await context.Jokes.FindAsync(jokeID);
                    
                    if(joke != null)
                    {
                        _twilioSvc.SendMessage(
                            ("Question: " + joke.Question + "\n\nAnswer: " + joke.Answer),
                            subscriber.PhoneNumber,
                            subscriber.CountryCode
                        );
                    }
                    else
                    {
                        Console.WriteLine("JOKE DOES NOT EXIST CHECK DATABASE");
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        }
    }
}