using ChromaVision.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChromaVision.Infrastructure.Repositories
{
    public class OpenAIService : IOpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _endpoint = "https://api.openai.com/v1/chat/completions";

        public OpenAIService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenAI:ApiKey"] ?? throw new ArgumentNullException("OpenAI API key is missing");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<List<string>> GenerateColorsFromDescriptionAsync(string description, int colorCount = 5, CancellationToken cancellationToken = default)
        {
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = $"You are a color palette generator. Generate a list of exactly {colorCount} hex color codes that match the following description. Return ONLY the hex codes in a JSON array with no additional text or explanation."
                    },
                    new
                    {
                        role = "user",
                        content = description
                    }
                },
                temperature = 0.7
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_endpoint, content, cancellationToken);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            var responseObject = JsonSerializer.Deserialize<OpenAIResponse>(responseString);

            var colorText = responseObject?.Choices?[0].Message.Content.Trim() ?? "[]";

            // Extract JSON array from the response if necessary
            if (colorText.Contains("[") && colorText.Contains("]"))
            {
                colorText = colorText.Substring(colorText.IndexOf('['), colorText.LastIndexOf(']') - colorText.IndexOf('[') + 1);
            }

            try
            {
                return JsonSerializer.Deserialize<List<string>>(colorText) ?? new List<string>();
            }
            catch
            {
                // Fallback parsing for non-standard responses
                return colorText
                    .Replace("[", "")
                    .Replace("]", "")
                    .Replace("\"", "")
                    .Replace("'", "")
                    .Split(',')
                    .Select(c => c.Trim())
                    .Where(c => !string.IsNullOrWhiteSpace(c))
                    .ToList();
            }
        }
    }

    public class OpenAIResponse
    {
        public List<Choice>? Choices { get; set; }
    }

    public class Choice
    {
        public Message Message { get; set; } = new Message();
    }

    public class Message
    {
        public string Content { get; set; } = string.Empty;
    }
}
