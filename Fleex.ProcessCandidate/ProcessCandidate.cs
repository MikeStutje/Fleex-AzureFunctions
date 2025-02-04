using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Fleex.ProcessCandidate
{
    public class ProcessCandidate
    {
        private readonly ILogger<ProcessCandidate> _logger;

        public ProcessCandidate(ILogger<ProcessCandidate> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ProcessCandidate))]
        public async Task Run(
            [ServiceBusTrigger("candidate-queue", Connection = "AzureWebJobsServiceBus")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
