using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Core;
using poc.AIAghent;

var configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();
    

var builder = Kernel.CreateBuilder();

builder.Services.AddAzureOpenAIChatCompletion(
    configuration["DeploymentName"],
    configuration["Endpoint"],
    configuration["ApiKey"]);

builder.Plugins.AddFromType<TimePlugin>();
builder.Plugins.AddFromType<ConversationSummaryPlugin>();

var kernel = builder.Build();

AgentFunctions agent = new(kernel);

await agent.GetItemsFromContext();