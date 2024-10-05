using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace GuidGenerator
{
    public class GetGuid
    {
        private readonly ILogger<GetGuid> _logger;

        public GetGuid(ILogger<GetGuid> logger)
        {
            _logger = logger;
        }

        [Function("GetGuid")]
        public async Task<IActionResult>  Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            _logger.LogInformation("Started the GetGuid Function Call");

            string? numberOfGuidsText = req.Query["count"];
            int numberOfGuids = 1;
            List<string> guids = [];

            if (numberOfGuidsText is not null && int.TryParse(numberOfGuidsText, out numberOfGuids))
            {
                _logger.LogInformation("Number of Guids requested: {NumberOfGuids}", numberOfGuids);
            }
            else
            {
                _logger.LogInformation("Unknown number of Guids requested. Using 1");
                numberOfGuids = 1;
            }

            for (int i = 0; i < numberOfGuids; i++)
            {
                guids.Add(Guid.NewGuid().ToString());
            }

            return new OkObjectResult(guids);
        }
    }
}
