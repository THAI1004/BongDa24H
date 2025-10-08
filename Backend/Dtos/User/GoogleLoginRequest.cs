// DTOs/GoogleLoginRequest.cs
using Newtonsoft.Json;

public class GoogleLoginRequest
{
    [JsonProperty("idToken")]
    public string Code { get; set; }
    [JsonProperty("redirectUri")]
    public string RedirectUri { get; set; }
}