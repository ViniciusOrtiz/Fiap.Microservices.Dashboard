using Azure.Messaging.ServiceBus;
using GeekBurguer.Dashboard.Api.Infra;
using GeekBurguer.Dashboard.Api.Repository;
using GeekBurguer.Dashboard.Api.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<IServiceProvider>(builder.Services.BuildServiceProvider());
builder.Services.AddScoped<ISalesRepository, SalesRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
string connectionString = "{Service Bus connection string}";
string queueName = "{Queue name}";

builder.Services.AddSingleton<ServiceBusClient>(new ServiceBusClient(connectionString));
builder.Services.AddSingleton<ServiceBusReceiver>(provider => provider.GetService<ServiceBusClient>().CreateReceiver(queueName));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapPost("/messages", async (HttpContext context) =>
{
    var message = await context.Request.ReadFromJsonAsync<ServiceBusReceivedMessage>();

    try
    {
        var messageProcessor = context.RequestServices.GetService<IMessageProcessor>();

        await messageProcessor.ProcessMessage(message);

        await context.Response.WriteAsync("Message processed successfully");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception: {ex.Message}");
        await context.Response.WriteAsync("An error occurred while processing the message");
    }
});


app.Run();