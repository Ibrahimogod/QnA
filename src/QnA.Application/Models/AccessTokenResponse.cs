namespace QnA.Application.Models
{
    public class AccessTokenResponse
    {
        public string access_token { get; set; }

        public string token_type { get; set; } = "Bearer";

        public int expires_in { get; set; }
    }
}
