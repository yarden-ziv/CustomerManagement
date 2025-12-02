using CustomerManagement.Models.Dto;
using System.Text.Json;

namespace CustomerManagement.Services
{
    public class BankService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BankService> _logger;

        private const string ApiUrl =
            "https://www.xnes.co.il/ClosedSystemMiddlewareApi/api/generalinformation";

        public BankService(HttpClient httpClient, ILogger<BankService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<BankDto>> GetBanksAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiUrl);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var apiResult = JsonSerializer.Deserialize<BankApiResponse>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });


                return apiResult?.Data?.Banks ?? new List<BankDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching banks from API");
                return new List<BankDto>();
            }
        }

        public async Task<List<BranchDto>> GetBranchesAsync(int bankCode)
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiUrl);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var apiResult = JsonSerializer.Deserialize<BankApiResponse>(json);

                // Filter branches by bank
                return apiResult?.Data?.BankBranches?
    .FindAll(b => b.BankCode == bankCode)
    ?? new List<BranchDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching bank branches from API");
                return new List<BranchDto>();
            }
        }
    }

    // This class matches the JSON structure of the API response
    public class BankApiResponse
    {
        public string Status { get; set; }
        public int Code { get; set; }
        public BankApiData Data { get; set; }
    }

    public class BankApiData
    {
        public List<BankDto> Banks { get; set; }
        public List<BranchDto> BankBranches { get; set; }
    }

}
