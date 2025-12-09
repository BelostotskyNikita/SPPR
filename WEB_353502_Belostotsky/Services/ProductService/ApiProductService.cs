using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Text;
using WEB_353502_Belostotsky.Domain.Entities;
using WEB_353502_Belostotsky.Domain.Models;
using WEB_353502_Belostotsky.UI.Services.ProductService;

public class ApiProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly string _pageSize;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly ILogger<ApiProductService> _logger;

    public ApiProductService(HttpClient httpClient, IConfiguration configuration, ILogger<ApiProductService> logger)
    {
        _httpClient = httpClient;
        _pageSize = configuration.GetSection("ItemsPerPage").Value;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _logger = logger;
    }

    public async Task<ResponseData<ListModel<ClothingItem>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
    {
        var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}ClothingItems/");

        if (!string.IsNullOrEmpty(categoryNormalizedName))
        {
            urlString.Append($"{categoryNormalizedName}/");
        }

        if (pageNo > 1)
        {
            urlString.Append($"page{pageNo}");
        }

        if (!_pageSize.Equals("3"))
        {
            urlString.Append(QueryString.Create("pageSize", _pageSize));
        }

        var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

        if (response.IsSuccessStatusCode)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<ResponseData<ListModel<ClothingItem>>>(_serializerOptions);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"-----> Ошибка: {ex.Message}");
                return ResponseData<ListModel<ClothingItem>>.Error($"Ошибка: {ex.Message}");
            }
        }

        _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode}");
        return ResponseData<ListModel<ClothingItem>>.Error($"Данные не получены от сервера. Error: {response.StatusCode}");
    }

    public async Task<ResponseData<ClothingItem>> CreateProductAsync(ClothingItem product, IFormFile? formFile)
    {
        var uri = new Uri(_httpClient.BaseAddress.AbsoluteUri + "ClothingItems");
        var response = await _httpClient.PostAsJsonAsync(uri, product, _serializerOptions);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ResponseData<ClothingItem>>(_serializerOptions);
        }

        _logger.LogError($"-----> объект не создан. Error: {response.StatusCode}");
        return ResponseData<ClothingItem>.Error($"Объект не добавлен. Error: {response.StatusCode}");
    }
}
