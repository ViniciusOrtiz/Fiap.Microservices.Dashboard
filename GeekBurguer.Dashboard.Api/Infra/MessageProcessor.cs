using Azure.Messaging.ServiceBus;
using GeekBurguer.Dashboard.Api.Models;
using Newtonsoft.Json;

namespace GeekBurguer.Dashboard.Api.Infra
{
    public class CustomMessageProcessor : IMessageProcessor
    {
        private readonly ServiceBusClient _client;
        private readonly DashboardContext _context;

        public CustomMessageProcessor(ServiceBusClient client, DashboardContext context)
        {
            _client = client;
            _context = context;
        }

        public async Task ProcessMessage(ServiceBusReceivedMessage message)
        {
            string json = message.Body.ToString();
            User user = JsonConvert.DeserializeObject<User>(json);

            if (user.Restrictions?.Length <= 2)
            {
                SaveUser(user);
            }

            List<User> usersWithRestrictions = GetUsersWithRestrictions();

            string jsonResponse = JsonConvert.SerializeObject(usersWithRestrictions);

            ServiceBusMessage responseMessage = new ServiceBusMessage(jsonResponse);

            string responseQueueName = "{Response queue name}";

            await using var sender = _client.CreateSender(responseQueueName);
            await sender.SendMessageAsync(responseMessage);
        }

        void SaveUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

        List<User> GetUsersWithRestrictions()
        {
            return _context.User
                .Where(u => u.Restrictions != null && u.Restrictions.Length <= 2)
                .Distinct()
                .ToList();
        }
    }
}