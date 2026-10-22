using System.Net.Http.Headers;

namespace MiniSupermarket.WinForms.Services
{
    public static class ApiClientService
    {
        // Sử dụng chung HttpClient với SessionManager (BaseAddress: https://localhost:7158/api/)
        public static HttpClient Client => SessionManager.ApiClientService.Client;

        // Hàm tự động đính kèm JWT Token vào Header sau khi đăng nhập thành công (Bước 3.1 - Buổi 6)
        public static void AttachToken(string token)
        {
            SessionManager.JwtToken = token ?? string.Empty;
            SessionManager.ApiClientService.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
        }
    }
}
