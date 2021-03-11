using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using lovepdf.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PdfSharp.Pdf.IO;

namespace lovepdf.Controllers
{
    public class HttpClientController : ApiControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<HttpClientController> _logger;
        public HttpClientController(IHttpClientFactory clientFactory, ILogger<HttpClientController> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        [HttpGet("github")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> dotnet_docs_pulls()
        {
            var client = _clientFactory.CreateClient("github");
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "repos/dotnet/AspNetCore.Docs/pulls");
            var response = await client.SendAsync(request);

            IEnumerable<GitHubPullRequest> pullRequests = null;
            var hasError = false;
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                pullRequests = await JsonSerializer.DeserializeAsync
                        <IEnumerable<GitHubPullRequest>>(responseStream);
                _logger.LogInformation( "Request to GitHub successed.", pullRequests );
            }
            else
            {
                hasError = true;
                pullRequests = Array.Empty<GitHubPullRequest>();
                _logger.LogInformation( "Request to GitHub failed.");
            }

            return new JsonResult(pullRequests);
        }
    }
}