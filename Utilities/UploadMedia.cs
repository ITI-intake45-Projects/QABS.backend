
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Utilities
{
    public static class UploadMedia
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> AddImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var fileBytes = ms.ToArray();
            var base64Image = Convert.ToBase64String(fileBytes);

            var content = new MultipartFormDataContent
        {
            { new StringContent("685a95b61dca1bd3895ad6668eaa4691"), "key" },
            { new StringContent(base64Image), "image" }
        };

            var response = await client.PostAsync("https://api.imgbb.com/1/upload", content);
            if (!response.IsSuccessStatusCode)
                return string.Empty;

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonDocument.Parse(json);
            return result.RootElement.GetProperty("data").GetProperty("url").GetString() ?? string.Empty;
        }

        public static async Task<List<string>> AddImagesAsync(IFormFileCollection files)
        {
            var imagePaths = new List<string>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var url = await AddImageAsync(file);
                    if (!string.IsNullOrEmpty(url))
                    {
                        imagePaths.Add(url);
                    }
                }
            }

            return imagePaths;
        }
    }
}

