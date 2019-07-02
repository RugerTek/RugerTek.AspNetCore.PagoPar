using Newtonsoft.Json;

namespace RugerTek.AspNetCore.PagoPar.HttpClients.Models.PaymentMethods
{
    public class PaymentMethodsRequestModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("token_publico")]
        public string TokenPublico { get; set; }
    }
}
